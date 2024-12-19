import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import { TipoDeCompra } from '../../../shared/enums/tipo-de-compra.enum';
import { BaseComponent } from '../../../shared/components/base.component';
import { CadastroService } from '../../../services/cadastro.service';
import { AutorViewModel } from '../../../shared/interfaces/view-models/AutorViewModel';
import { SnackbarService } from '../../../services/snackbar.service';
import { AdicionarLivroCommand } from '../../../shared/interfaces/commands/livro-commands/AdicionarLivroCommand';
import { CommandResult } from '../../../shared/interfaces/commands/CommandResult';
import { AlterarLivroCommand } from '../../../shared/interfaces/commands/livro-commands/AlterarLivroCommand';
import { LivroViewModel } from '../../../shared/interfaces/view-models/LivroViewModel';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';

@Component({
  selector: 'app-livro-form',
  standalone: false,
  templateUrl: './livro-form.component.html',
  styleUrls: ['./livro-form.component.scss']
})
export class LivroFormComponent extends BaseComponent implements OnInit {
  editando = false;
  livroForm!: FormGroup;
  livroId: number | null = null;
  autores: AutorViewModel[] = [];
  assuntosSelecionados: string[] = [];

  readonly separatorKeys = [ENTER, COMMA];

  tiposDeCompra = Object.values(TipoDeCompra);

  get valoresCompra(): FormArray {
    return this.livroForm.get('valoresCompra') as FormArray;
  }

  get tiposDeCompraDisponiveis(): TipoDeCompra[] {
    const valoresSelecionados = this.valoresCompra.controls.map((control) => control.get('tipo')?.value);
    return this.tiposDeCompra.filter((tipo) => !valoresSelecionados.includes(tipo));
  }

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private snackbarService: SnackbarService,
    private cadastroService: CadastroService) {
    super();
  }

  ngOnInit(): void {
    this.iniciarForm();
    this.obterAutores();

    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.editando = true;
        this.livroId = +id;
        this.obterLivroPorId(this.livroId);
      }
    });
  }

  iniciarForm() {
    this.livroForm = this.fb.group({
      titulo: ['', Validators.required],
      autores: [[], Validators.required],
      editora: ['', Validators.required],
      edicao: ['', Validators.required],
      anoPublicacao: ['', [Validators.required, Validators.pattern('^[0-9]{4}$')]],
      valoresCompra: this.fb.array([]),
    });
  }

  obterAutores() {
    this.addSubscription(
      this.cadastroService.obterAutores().subscribe({
        next: (autores: AutorViewModel[]) => { this.handleObterAutores(autores) },
        error: () => { this.snackbarService.abrirMensagemErro("Ocorreu um erro ao buscar autores"); },
        complete: () => { }
      }));
  }

  handleObterAutores(autores: AutorViewModel[]) {
    this.autores = autores;
  }

  obterLivroPorId(livroId: number) {
    this.addSubscription(
      this.cadastroService.obterLivroPorId(livroId).subscribe({
        next: (livro: LivroViewModel) => this.handleObterLivro(livro),
        error: () => this.snackbarService.abrirMensagemErro("Ocorreu um erro ao buscar o livro"),
        complete: () => { }
      })
    );
  }

  handleObterLivro(livro: LivroViewModel) {
    this.livroForm.patchValue({
      livroId: livro.livroId,
      titulo: livro.titulo,
      autores: livro.autores.map(a => a.autorId),
      editora: livro.editora,
      edicao: livro.edicao,
      anoPublicacao: livro.anoPublicacao,
    });

    livro.valoresCompra.forEach(vc => {
      this.valoresCompra.push(
        this.fb.group({
          tipo: [vc.tipo, Validators.required],
          valor: [vc.valor, [Validators.required, Validators.min(0)]],
        })
      );
    });

    this.assuntosSelecionados = livro.assuntos.map(a => a.descricao);
  }

  tipoJaSelecionado(tipo: TipoDeCompra): boolean {
    return this.valoresCompra.controls.some((control) => control.get('tipo')?.value === tipo);
  }

  adicionarAssunto(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    if (value && !this.assuntosSelecionados.includes(value)) {
      this.assuntosSelecionados.push(value);
    }
    event.chipInput!.clear();
  }

  removerAssunto(assunto: string): void {
    this.assuntosSelecionados = this.assuntosSelecionados.filter((a) => a !== assunto);
  }

  adicionarValorCompra(): void {
    this.valoresCompra.push(
      this.fb.group({
        tipo: [null, Validators.required],
        valor: [null, [Validators.required, Validators.min(0)]],
      })
    );
  }

  removerValorCompra(index: number): void {
    this.valoresCompra.removeAt(index);
  }

  adicionarAlterarLivro() {
    if (this.livroForm.invalid) return;

    this.editando ? this.alterarLivro() : this.adicionarLivro();
  }

  adicionarLivro() {
    const adicionarLivroCommand: AdicionarLivroCommand = this.mapFormToCommand() as AdicionarLivroCommand;

    this.addSubscription(
      this.cadastroService.adicionarLivro(adicionarLivroCommand).subscribe({
        next: (resposta: CommandResult) => { this.handleAdicionarAlterarLivro(resposta) },
        error: () => { this.snackbarService.abrirMensagemErro("Ocorreu um erro ao adicionar Livro"); },
        complete: () => { }
      }));
  }

  alterarLivro() {
    const alterarLivroCommand: AlterarLivroCommand = this.mapFormToCommand() as AlterarLivroCommand;

    this.addSubscription(
      this.cadastroService.alterarLivro(alterarLivroCommand).subscribe({
        next: (resposta: CommandResult) => { this.handleAdicionarAlterarLivro(resposta) },
        error: () => { this.snackbarService.abrirMensagemErro("Ocorreu um erro ao alterar Autor"); },
        complete: () => { }
      }));
  }

  handleAdicionarAlterarLivro(resposta: CommandResult) {
    if (!resposta.sucesso) {
      this.snackbarService.abrirMensagemErro(resposta.mensagem || "Ocorreu um erro ao adicionar/alterar Livro");
      return;
    }

    this.snackbarService.abrirMensagemSucesso(resposta.mensagem);
    this.router.navigate(['/livros']);
  }

  mapFormToCommand(): AdicionarLivroCommand | AlterarLivroCommand {
    return {
      livroId: this.livroId || 0,
      titulo: this.livroForm.value.titulo,
      autores: this.livroForm.value.autores.map((autorId: number) => autorId),
      editora: this.livroForm.value.editora,
      edicao: this.livroForm.value.edicao,
      anoPublicacao: this.livroForm.value.anoPublicacao,
      valoresCompra: this.livroForm.value.valoresCompra.map((vc: any) => ({
        tipo: vc.tipo as TipoDeCompra,
        valor: parseFloat(vc.valor)
      })),
      assuntos: this.assuntosSelecionados
    };
  }

  cancelar() {
    this.router.navigate(['/livros']);
  }
}
