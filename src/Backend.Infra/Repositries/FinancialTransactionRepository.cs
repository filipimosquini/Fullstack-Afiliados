using Backend.Core.Entities;
using Backend.Core.Repositories;
using Backend.Infra.Bases;
using Backend.Infra.Context;

namespace Backend.Infra.Repositries;

public class FinancialTransactionRepository : BaseRepository<FinancialTransaction>, IFinancialTransactionRepository, IDisposable
{
    private readonly SellerContext _context;
    public FinancialTransactionRepository(SellerContext context) : base(context)
    {
        _context = context;
    }

    public async Task RemoveAll()
    {
        _context.FinancialTransactions.RemoveRange(_context.FinancialTransactions);
        await _context.SaveChangesAsync();
    }


    public void Dispose()
    {
        if (_context != null)
        {
            _context.Dispose();
        }
    }
}