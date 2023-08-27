using Backend.Core.Repositories;

namespace Backend.Core.Bases.Interfaces;

public interface IUnitOfWork 
{
    IFinancialTransactionRepository FinancialTransactionRepository { get; }
    IFinancialTransactionTypeRepository FinancialTransactionTypeRepository { get; }
    IProductRepository ProductRepository { get; }
    ISellerRepository SellerRepository { get; }

    Task SaveAsync();
}