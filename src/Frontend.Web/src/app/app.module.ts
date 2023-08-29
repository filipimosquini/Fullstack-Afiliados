import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { ToastrModule } from 'ngx-toastr';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BaseInterceptor } from './bases/base.interceptor.service';
import { AppGuard } from './app.guard';
import { NavigateModule } from './navigate/navigate.module';
import { FinancialTransactionModule } from './financial-transaction/financial-transaction.module';

export const httpInterceptorProviders = [
  {provide: HTTP_INTERCEPTORS, useClass: BaseInterceptor, multi: true}
];
@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    NgbModule,
    ToastrModule.forRoot(),
    HttpClientModule,
    AppRoutingModule,
    NavigateModule,
    FinancialTransactionModule
  ],
  providers: [AppGuard, httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
