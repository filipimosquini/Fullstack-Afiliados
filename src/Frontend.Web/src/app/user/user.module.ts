import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create/create.component';
import { LoggedInComponent } from './logged-in/logged-in.component';
import { BrowserModule } from '@angular/platform-browser';



@NgModule({
  declarations: [
    CreateComponent,
    LoggedInComponent
  ],
  imports: [
    CommonModule,
    BrowserModule
  ]
})
export class UserModule { }
