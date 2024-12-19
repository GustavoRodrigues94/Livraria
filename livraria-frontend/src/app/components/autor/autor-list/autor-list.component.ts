import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { BaseComponent } from '../../../shared/components/base.component';
import { Router } from '@angular/router';
import { AutorViewModel } from '../../../shared/interfaces/view-models/AutorViewModel';
import { CadastroService } from '../../../services/cadastro.service';
import { SnackbarService } from '../../../services/snackbar.service';
import { RemoverAutorCommand } from '../../../shared/interfaces/commands/autor-commands/RemoverAutorCommand';
import { CommandResult } from '../../../shared/interfaces/commands/CommandResult';

@Component({
  selector: 'app-autor-list',
  standalone: false,
  templateUrl: './autor-list.component.html',
  styleUrls: ['./autor-list.component.scss']
})
export class AutorListComponent extends BaseComponent implements OnInit {
  dataSource = new MatTableDataSource<AutorViewModel>();
  datatableColunas: string[] = ['nome', 'acoes'];

  constructor(
    private router: Router,
    private snackbarService: SnackbarService,
    private cadastroService: CadastroService
  ) {
    super();
  }

  ngOnInit(): void {
    this.obterAutores();
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
    this.dataSource.data = autores;
  }

  adicionarAutor() {
    this.router.navigate(['autores/novo']);
  }

  editarAutor(autor: AutorViewModel) {
    this.router.navigate([`autores/editar/${autor.autorId}`])
  }

  removerAutor(autor: AutorViewModel) {
    const removerAutorCommand: RemoverAutorCommand = { autorId: autor.autorId };

    this.addSubscription(
      this.cadastroService.removerAutor(removerAutorCommand).subscribe({
        next: (resposta: CommandResult) => {
          this.snackbarService.abrirMensagemSucesso(resposta.mensagem || "Autor deletado com sucesso!");
          this.obterAutores();
        },
        error: () => { this.snackbarService.abrirMensagemErro("Ocorreu um erro ao deletar o autor."); },
        complete: () => { }
      })
    );
  }
}
