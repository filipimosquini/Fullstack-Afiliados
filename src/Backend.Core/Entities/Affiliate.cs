﻿using Backend.Core.Bases;

namespace Backend.Core.Entities;

public class Affiliate : BaseEntity
{
    public string Name { get; set; }

    /* EF Relation */
    public IList<FinancialTransaction> Transactions { get; set; }
}