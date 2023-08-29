import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './financial-transaction/list/list.component';
import { NotFoundComponent } from './navigate/not-found/not-found.component';
import { AppGuard } from './app.guard';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: ListComponent },
  { path: 'users', loadChildren: () => import('./user/user.module').then(x => x.UserModule) },
  { path: 'financial-transactions', loadChildren: () => import('./financial-transaction/financial-transaction.module').then(x => x.FinancialTransactionModule), canLoad: [AppGuard] },

  { path: 'not-found', component: NotFoundComponent },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
