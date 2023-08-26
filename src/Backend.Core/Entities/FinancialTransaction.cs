using Backend.Core.Bases;

namespace Backend.Core.Entities;

public class FinancialTransaction : BaseEntity
{
    public DateTime Date { get; set; }
    public double Value { get; set; }
    public Affiliate? Affiliate { get; set; }
    public Product? Product { get; set; }
    public FinancialTransactionType? TransactionType { get; set; }
}