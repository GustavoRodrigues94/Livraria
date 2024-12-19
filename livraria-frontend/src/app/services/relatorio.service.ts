import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CommandResult } from '../shared/interfaces/commands/CommandResult';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RelatorioService {
  private apiUrl = `${environment.urlBase}/relatorio`

  constructor(private httpClient: HttpClient) { }

  gerarRelatorioLivrosAgrupadoAutor(): Observable<Blob> {
    return this.httpClient.get<Blob>(`${this.apiUrl}/livros-agrupado-autores`, { responseType: 'blob' as 'json' });
  }
}
