import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AutorRoutingModule } from './autor-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { AutorListComponent } from './autor-list/autor-list.component';
import { AutorFormComponent } from './autor-form/autor-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AutorListComponent,
    AutorFormComponent
  ],
  imports: [
    CommonModule,
    AutorRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class AutorModule { }
