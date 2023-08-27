using Backend.Core.Entities;
using Backend.Core.Repositories;
using Backend.Infra.Bases;
using Backend.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Repositries;

public class SellerRepository : BaseRepository<Seller>, ISellerRepository, IDisposable
{
    private readonly SellerContext _context;
    public SellerRepository(SellerContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Seller?> FindByName(string name)
        => await _context.Sellers.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

    public void Dispose()
    {
        if (_context != null)
        {
            _context.Dispose();
        }
    }
}