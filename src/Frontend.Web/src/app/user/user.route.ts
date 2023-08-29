import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserAppComponent } from './user.app.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { UserGuard } from './services/user.guard';

const usuarioRouterConfig: Routes = [
    {
        path: '', component: UserAppComponent,
        children: [
            { path: 'sign-up', component: SignUpComponent, canActivate: [UserGuard], canDeactivate: [UserGuard] },
            { path: 'sign-in', component: SignInComponent, canActivate: [UserGuard] }
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(usuarioRouterConfig)
    ],
    exports: [RouterModule]
})
export class UserRoutingModule { }
