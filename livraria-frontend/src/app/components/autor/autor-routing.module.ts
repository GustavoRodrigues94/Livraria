import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutorListComponent } from './autor-list/autor-list.component';
import { AutorFormComponent } from './autor-form/autor-form.component';

const routes: Routes = [
  { path: '', component: AutorListComponent },
  { path: 'novo', component: AutorFormComponent },
  { path: 'editar/:id', component: AutorFormComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AutorRoutingModule {}
