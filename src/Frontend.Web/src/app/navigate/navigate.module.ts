import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoggedUserComponent } from './logged-user/logged-user.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { FooterComponent } from './footer/footer.component';
import { MenuComponent } from './menu/menu.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    LoggedUserComponent,
    NotFoundComponent,
    FooterComponent,
    MenuComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule,
    NgbModule
  ],
  exports:[
    MenuComponent,
    FooterComponent,
    NotFoundComponent,
    LoggedUserComponent
  ]
})
export class NavigateModule { }
