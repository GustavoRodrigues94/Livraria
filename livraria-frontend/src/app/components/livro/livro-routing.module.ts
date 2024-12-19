import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LivroListComponent } from './livro-list/livro-list.component';
import { LivroFormComponent } from './livro-form/livro-form.component';

const routes: Routes = [
  { path: '', component: LivroListComponent },
  { path: 'novo', component: LivroFormComponent },
  { path: 'editar/:id', component: LivroFormComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LivroRoutingModule {}
