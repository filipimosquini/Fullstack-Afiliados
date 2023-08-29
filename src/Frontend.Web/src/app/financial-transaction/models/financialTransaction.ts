import { FinancialTransactionDetail } from "./financialTransactionDetail"

export interface FinancialTransaction{
  id: number;
  sellerName: string
  total: number
  details: FinancialTransactionDetail[]
}
