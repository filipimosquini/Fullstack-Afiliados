namespace Backend.Core.Services.DataTransferObjects;

public class ImportedTransactionsDetailsDto
{
    public string Product { get; set; }
    public double Value { get; set; }
    public string FinancialTransactionTypeDescription { get; set; }
    public string FinancialTransactionTypeNature { get; set; }
    public string FinancialTransactionTypeSignal { get; set; }

}