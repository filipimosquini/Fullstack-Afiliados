import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ImportComponent } from './import/import.component';
import { ListComponent } from './list/list.component';
import { BrowserModule } from '@angular/platform-browser';
import { FinancialTransactionService } from './services/financialTransaction.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TransactionDetailsComponent } from './transaction-details/transaction-details.component';



@NgModule({
  declarations: [
    ImportComponent,
    ListComponent,
    TransactionDetailsComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    NgbModule
  ],
  providers: [
    FinancialTransactionService
  ]
})
export class FinancialTransactionModule { }
