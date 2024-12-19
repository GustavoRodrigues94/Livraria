import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'livros', loadChildren: () => import('./components/livro/livro.module').then(m => m.LivroModule) },
  { path: 'autores', loadChildren: () => import('./components/autor/autor.module').then(m => m.AutorModule) },
  { path: '', redirectTo: 'livros', pathMatch: 'full' },
  { path: '**', redirectTo: 'livros' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
