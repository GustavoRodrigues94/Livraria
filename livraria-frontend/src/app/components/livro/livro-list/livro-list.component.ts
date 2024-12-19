import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../../shared/components/base.component';
import { Router } from '@angular/router';
import { CadastroService } from '../../../services/cadastro.service';
import { SnackbarService } from '../../../services/snackbar.service';
import { LivroViewModel } from '../../../shared/interfaces/view-models/LivroViewModel';
import { RemoverLivroCommand } from '../../../shared/interfaces/commands/livro-commands/RemoverLivroCommand';
import { CommandResult } from '../../../shared/interfaces/commands/CommandResult';
import { RelatorioService } from '../../../services/relatorio.service';

@Component({
  selector: 'app-livro-list',
  standalone: false,
  templateUrl: './livro-list.component.html',
  styleUrl: './livro-list.component.scss'
})
export class LivroListComponent extends BaseComponent implements OnInit {
  livros: LivroViewModel[] = [];

  constructor(
    private router: Router,
    private snackbarService: SnackbarService,
    private relatorioService: RelatorioService,
    private cadastroService: CadastroService) {
    super();
  }

  ngOnInit(): void {
    this.obterLivros();
  }

  obterLivros() {
    this.addSubscription(
      this.cadastroService.obterLivros().subscribe({
        next: (livros: LivroViewModel[]) => this.handleObterLivros(livros),
        error: () => this.snackbarService.abrirMensagemErro("Ocorreu um erro ao buscar livros"),
        complete: () => { }
      })
    );
  }

  handleObterLivros(livros: LivroViewModel[]) {
    this.livros = livros;
  }

  adicionarLivro() {
    this.router.navigate(['/livros/novo'])
  }

  editarLivro(livroId: number) {
    this.router.navigate([`/livros/editar/${livroId}`])
  }

  removerLivro(livroId: number) {
    const removerLivroCommand: RemoverLivroCommand = { livroId: livroId };

    this.addSubscription(
      this.cadastroService.removerLivro(removerLivroCommand).subscribe({
        next: (resposta: CommandResult) => {
          this.snackbarService.abrirMensagemSucesso(resposta.mensagem || "Livro deletado com sucesso!");
          this.obterLivros();
        },
        error: () => { this.snackbarService.abrirMensagemErro("Ocorreu um erro ao deletar o livro."); },
        complete: () => { }
      })
    );
  }

  gerarRelatorio() {
    this.addSubscription(
      this.relatorioService.gerarRelatorioLivrosAgrupadoAutor().subscribe({
        next: (relatorio: Blob) => this.handleObterRelatorio(relatorio),
        error: () => this.snackbarService.abrirMensagemErro("Ocorreu um erro ao gerar relatório"),
        complete: () => { }
      })
    );
  }

  handleObterRelatorio(relatorio: Blob): void {
    const url = window.URL.createObjectURL(relatorio);
    const link = document.createElement('a');
    link.href = url;
    link.download = 'RelatorioLivros.pdf';
    link.click();
  }
}
