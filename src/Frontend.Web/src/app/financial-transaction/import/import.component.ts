import { Component, EventEmitter, Output } from '@angular/core';
import { FinancialTransactionService } from '../services/financialTransaction.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-import-transactions-file',
  templateUrl: './import.component.html',
  styleUrls: ['./import.component.css']
})
export class ImportComponent {

  errors: any[] = [];
  file: any = null;
  contentType: string = null;

  @Output()
  complete: EventEmitter<any> = new EventEmitter();

  @Output()
  completeWithErrors: EventEmitter<any> = new EventEmitter();

  constructor(private service: FinancialTransactionService,
              private toastr: ToastrService,
              private modalService: NgbModal) {

  }

  public openModal(content) {
    this.modalService.open(content, { size: 'lg' });
  }

  private closeModal(){
    this.file = null;
    this.contentType = null;
    this.modalService.dismissAll();
  }

  private processRequestSuccessfully(response: any) {
    this.errors = [];

    this.complete.emit(true);

    this.closeModal();

    this.toastr.success("File imported successfully", "Success!")
  }

  private processFailRequest(fail: any) {

    this.errors = fail.error.errors;
    this.completeWithErrors.emit(this.errors);

    this.closeModal();

    this.toastr.error("An error has occurred", "Error performing this operation");
  }

  disableUploadButton(){
    return !this.contentType && !this.file;
  }

  handleHeader(readerEvent: any) {
    var binaryString = readerEvent.target.result;
    this.file = btoa(binaryString);
  }

  onChange(file) {
    this.contentType = file[0].type;
    var reader = new FileReader();
    reader.readAsBinaryString(file[0]);
    reader.onload = this.handleHeader.bind(this);
  }

  uploadFile() {
    this.service.uploadFinancialTransactionsFile(this.file, this.contentType)
      .subscribe({
        next: (v) => {
          this.processRequestSuccessfully(v)
        },
        error: (e) => {
          this.processFailRequest(e)
        },
        complete: () => console.info('complete')
      });

  }
}
