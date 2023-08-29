import { Component, Input } from '@angular/core';
import { FinancialTransactionDetail } from '../models/financialTransactionDetail';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-transaction-details',
  templateUrl: './transaction-details.component.html',
  styleUrls: ['./transaction-details.component.css']
})
export class TransactionDetailsComponent {

  @Input()
  financialTransactionDetails: FinancialTransactionDetail[];
  alertType: string = "success";

  constructor(private modalService: NgbModal) {}

  public openModal(content){
    this.modalService.open(content, { size: 'lg' });
  }
}
