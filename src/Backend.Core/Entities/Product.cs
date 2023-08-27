using Backend.Core.Bases;

namespace Backend.Core.Entities;

public class Product : BaseEntity
{
    public string Description { get; set; }

    /* EF Relation */
    public IList<FinancialTransaction> FinancialTransaction { get; set; }
}