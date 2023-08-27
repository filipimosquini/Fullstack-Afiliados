using Backend.Core.Entities;
using Backend.Core.Repositories;
using Backend.Infra.Bases;
using Backend.Infra.Context;

namespace Backend.Infra.Repositries;

public class SellerRepository : BaseRepository<Seller>, ISellerRepository, IDisposable
{
    private readonly SellerContext _context;
    public SellerRepository(SellerContext context) : base(context)
    {
        _context = context;
    }

    public void Dispose()
    {
        if (_context != null)
        {
            _context.Dispose();
        }
    }
}