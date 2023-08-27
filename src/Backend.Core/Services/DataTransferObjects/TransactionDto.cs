namespace Backend.Core.Services.DataTransferObjects;

public class TransactionDto
{
    public int FinancialTransactionType { get; set; }
    public DateTime? Date { get; set; }
    public string Product { get; set; }
    public double Value { get; set; }
    public string Seller { get; set; }
}