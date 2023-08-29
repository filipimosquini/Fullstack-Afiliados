import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ImportComponent } from './import/import.component';
import { ListComponent } from './list/list.component';
import { BrowserModule } from '@angular/platform-browser';



@NgModule({
  declarations: [
    ImportComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    BrowserModule
  ]
})
export class FinancialTransactionModule { }
