import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '../../../shared/components/base.component';
import { AdicionarAutorCommand } from '../../../shared/interfaces/commands/autor-commands/AdicionarAutorCommand';
import { CadastroService } from '../../../services/cadastro.service';
import { CommandResult } from '../../../shared/interfaces/commands/CommandResult';
import { SnackbarService } from '../../../services/snackbar.service';
import { AlterarAutorCommand } from '../../../shared/interfaces/commands/autor-commands/AlterarAutorCommand';
import { AutorViewModel } from '../../../shared/interfaces/view-models/AutorViewModel';

@Component({
  selector: 'app-autor-form',
  standalone: false,
  templateUrl: './autor-form.component.html',
  styleUrl: './autor-form.component.scss'
})
export class AutorFormComponent extends BaseComponent implements OnInit {
  autorForm!: FormGroup;
  editando = false;
  autorId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private snackbarService: SnackbarService,
    private cadastroService: CadastroService
  ) {
    super();
  }

  ngOnInit(): void {
    this.iniciarForm();

    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.editando = true;
        this.autorId = +id;
        this.obterAutorPorId(this.autorId);
      }
    });
  }

  iniciarForm() {
    this.autorForm = this.fb.group({
      autorId: [''],
      nome: ['', [Validators.required, Validators.minLength(3)]],
    });
  }

  obterAutorPorId(autorId: number) {
    this.addSubscription(
      this.cadastroService.obterAutorPorId(autorId).subscribe({
        next: (autor: AutorViewModel) => { this.handleObterAutorPorId(autor) },
        error: () => { this.snackbarService.abrirMensagemErro("Ocorreu um erro ao buscar Autor"); },
        complete: () => { }
      }));
  }

  handleObterAutorPorId(autor: AutorViewModel) {
    this.autorForm.patchValue(autor);
  }

  adicionarAlterarAutor() {
    if (this.autorForm.invalid) return;

    this.editando ? this.alterarAutor() : this.adicionarAutor();
  }

  adicionarAutor() {
    const adicionarAutorCommand = this.autorForm.getRawValue() as AdicionarAutorCommand;

    this.addSubscription(
      this.cadastroService.adicionarAutor(adicionarAutorCommand).subscribe({
        next: (resposta: CommandResult) => { this.handleAdicionarAlterarAutor(resposta) },
        error: () => { this.snackbarService.abrirMensagemErro("Ocorreu um erro ao adicionar Autor"); },
        complete: () => { }
      }));
  }

  alterarAutor() {
    const alterarAutorCommand = this.autorForm.getRawValue() as AlterarAutorCommand;

    this.addSubscription(
      this.cadastroService.alterarAutor(alterarAutorCommand).subscribe({
        next: (resposta: CommandResult) => { this.handleAdicionarAlterarAutor(resposta) },
        error: () => { this.snackbarService.abrirMensagemErro("Ocorreu um erro ao alterar Autor"); },
        complete: () => { }
      }));
  }

  handleAdicionarAlterarAutor(resposta: CommandResult) {
    if (!resposta.sucesso) {
      this.snackbarService.abrirMensagemErro("Ocorreu um erro ao adicionar/alterar Autor");
      return;
    }

    this.snackbarService.abrirMensagemSucesso(resposta.mensagem);
    this.router.navigate(['/autores']);
  }

  cancelar() {
    this.router.navigate(['/autores']);
  }
}
