import { Component, OnInit } from '@angular/core';
import { FinancialTransactionService } from '../services/financialTransaction.service';
import { ToastrService } from 'ngx-toastr';
import { FinancialTransaction } from '../models/financialTransaction';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  errors: any[] = [];
  public financialTransactions: FinancialTransaction[] = [];

  constructor(private service: FinancialTransactionService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.getFinancialTransactions()
    .subscribe({
      next: (financialTransactions) => { this.financialTransactions = financialTransactions },
      error: (e) => {
        this.financialTransactions = [];
        this.processFailRequest(e);
      },
      complete: () => {}
    });
  }

  private processFailRequest(fail: any){
    this.errors = fail.error.errors;
    this.toastr.error("An error has occurred", "Error performing this operation")
  }

}
