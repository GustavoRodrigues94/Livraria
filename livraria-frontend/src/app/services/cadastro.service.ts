import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { AdicionarAutorCommand } from '../shared/interfaces/commands/autor-commands/AdicionarAutorCommand';
import { CommandResult } from '../shared/interfaces/commands/CommandResult';
import { Observable } from 'rxjs';
import { AlterarAutorCommand } from '../shared/interfaces/commands/autor-commands/AlterarAutorCommand';
import { AutorViewModel } from '../shared/interfaces/view-models/AutorViewModel';
import { RemoverAutorCommand } from '../shared/interfaces/commands/autor-commands/RemoverAutorCommand';
import { AdicionarLivroCommand } from '../shared/interfaces/commands/livro-commands/AdicionarLivroCommand';
import { AlterarLivroCommand } from '../shared/interfaces/commands/livro-commands/AlterarLivroCommand';
import { LivroViewModel } from '../shared/interfaces/view-models/LivroViewModel';
import { RemoverLivroCommand } from '../shared/interfaces/commands/livro-commands/RemoverLivroCommand';

@Injectable({
  providedIn: 'root'
})
export class CadastroService {
  private apiUrl = `${environment.urlBase}/cadastro`

  constructor(private httpClient: HttpClient) { }

  adicionarAutor(command: AdicionarAutorCommand): Observable<CommandResult> {
    return this.httpClient.post<CommandResult>(`${this.apiUrl}/autor`, command);
  }

  alterarAutor(command: AlterarAutorCommand): Observable<CommandResult> {
    return this.httpClient.put<CommandResult>(`${this.apiUrl}/autor`, command);
  }

  removerAutor(command: RemoverAutorCommand): Observable<CommandResult> {
    return this.httpClient.delete<CommandResult>(`${this.apiUrl}/autor`, { body: command });
  }

  obterAutorPorId(autorId: number): Observable<AutorViewModel> {
    return this.httpClient.get<AutorViewModel>(`${this.apiUrl}/autor/${autorId}`);
  }

  obterAutores(): Observable<AutorViewModel[]> {
    return this.httpClient.get<AutorViewModel[]>(`${this.apiUrl}/autores`);
  }

  adicionarLivro(command: AdicionarLivroCommand): Observable<CommandResult> {
    return this.httpClient.post<CommandResult>(`${this.apiUrl}/livro`, command);
  }

  alterarLivro(command: AdicionarLivroCommand): Observable<CommandResult> {
    return this.httpClient.put<CommandResult>(`${this.apiUrl}/livro`, command);
  }

  removerLivro(command: RemoverLivroCommand): Observable<CommandResult> {
    return this.httpClient.delete<CommandResult>(`${this.apiUrl}/livro`, { body: command });
  }

  obterLivroPorId(livroId: number): Observable<LivroViewModel> {
    return this.httpClient.get<LivroViewModel>(`${this.apiUrl}/livro/${livroId}`);
  }

  obterLivros(): Observable<LivroViewModel[]> {
    return this.httpClient.get<LivroViewModel[]>(`${this.apiUrl}/livros`);
  }
}
