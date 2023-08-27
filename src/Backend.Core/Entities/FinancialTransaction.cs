using Backend.Core.Bases;

namespace Backend.Core.Entities;

public class FinancialTransaction : BaseEntity
{
    public DateTime Date { get; set; }
    public double Value { get; set; }
    public Seller? Seller { get; set; }
    public Product? Product { get; set; }
    public FinancialTransactionType? FinancialTransactionType { get; set; }
}