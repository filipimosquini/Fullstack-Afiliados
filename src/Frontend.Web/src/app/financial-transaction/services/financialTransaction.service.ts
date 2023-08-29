import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseService } from "app/bases/base.service";
import { Observable, catchError, map } from "rxjs";
import { FinancialTransaction } from "../models/financialTransaction";

@Injectable()
export class FinancialTransactionService extends BaseService {

  constructor(private http: HttpClient){
    super();
  }

  getFinancialTransactions() : Observable<FinancialTransaction[]>{
    return this.http
      .get(this.urlApi+'financial-transactions/', super.getJsonAuthHeader())
      .pipe(
        map(this.getResponseData),
        catchError(this.tratarErrosDoServidor))
  }

  // importFinancialTransactions(user: User) : Observable<FinancialTransaction>{
  //   return this.http
  //     .post(this.urlApi+'financial-transactions/import', user)
  //     .pipe(
  //       map(this.getResponseData),
  //       catchError(this.tratarErrosDoServidor))
  // }
}
