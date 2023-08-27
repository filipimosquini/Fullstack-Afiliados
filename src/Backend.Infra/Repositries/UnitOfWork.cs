using Backend.Core.Bases.Interfaces;
using Backend.Core.Repositories;
using Backend.Infra.Context;

namespace Backend.Infra.Repositries;

public class UnitOfWork : IUnitOfWork
{
    private IFinancialTransactionRepository _financialTransactionRepository;
    private IFinancialTransactionTypeRepository _financialTransactionTypeRepository;
    private IProductRepository _productRepository;
    private ISellerRepository _sellerRepository;

    private readonly SellerContext _context;

    public UnitOfWork(SellerContext context)
    {
        _context = context;
    }

    public IFinancialTransactionRepository FinancialTransactionRepository
    {
        get
        {
            if (_financialTransactionRepository == null)
            {
                _financialTransactionRepository = new FinancialTransactionRepository(_context);
            }
            return _financialTransactionRepository;
        }
    }

    public IFinancialTransactionTypeRepository FinancialTransactionTypeRepository
    {
        get
        {
            if (_financialTransactionTypeRepository == null)
            {
                _financialTransactionTypeRepository = new FinancialTransactionTypeRepository(_context);
            }
            return _financialTransactionTypeRepository;
        }
    }

    public IProductRepository ProductRepository
    {
        get
        {
            if (_productRepository == null)
            {
                _productRepository = new ProductRepository(_context);
            }
            return _productRepository;
        }
    }

    public ISellerRepository SellerRepository
    {
        get
        {
            if (_sellerRepository == null)
            {
                _sellerRepository = new SellerRepository(_context);
            }
            return _sellerRepository;
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}