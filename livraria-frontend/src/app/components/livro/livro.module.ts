import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LivroRoutingModule } from './livro-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { LivroListComponent } from './livro-list/livro-list.component';
import { LivroFormComponent } from './livro-form/livro-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    LivroListComponent,
    LivroFormComponent
  ],
  imports: [
    CommonModule,
    LivroRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class LivroModule { }
