import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { UserRoutingModule } from './user.route';
import { UserGuard } from './services/user.guard';
import { UserService } from './services/user.service';
import { UserAppComponent } from './user.app.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { SignInComponent } from './sign-in/sign-in.component';

@NgModule({
  declarations: [
    UserAppComponent,
    SignUpComponent,
    SignInComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    UserRoutingModule
  ],
  providers: [
    UserService,
    UserGuard
  ]
})
export class UserModule { }
