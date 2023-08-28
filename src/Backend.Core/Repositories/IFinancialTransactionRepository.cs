using Backend.Core.Bases.Interfaces;
using Backend.Core.Entities;

namespace Backend.Core.Repositories;

public interface IFinancialTransactionRepository : IBaseRepository<FinancialTransaction>
{
    Task<IEnumerable<FinancialTransaction>> GetImportedTransactionsAsync();
    Task RemoveAll();
}