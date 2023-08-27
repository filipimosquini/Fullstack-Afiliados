using Backend.Core.Bases;

namespace Backend.Core.Entities;

public class Seller : BaseEntity
{
    public string Name { get; set; }

    /* EF Relation */
    public IList<FinancialTransaction> FinancialTransaction { get; set; }
}