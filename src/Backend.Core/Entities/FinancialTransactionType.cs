using Backend.Core.Bases;

namespace Backend.Core.Entities;

public class FinancialTransactionType : BaseEntity
{
    public string Description { get; set; }
    public string Nature { get; set; }
    public string Signal { get; set; }

    /* EF Relation */
    public IList<FinancialTransaction> FinancialTransaction { get; set; }
}