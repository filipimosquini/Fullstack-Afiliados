using Backend.Core.Entities;
using Backend.Core.Repositories;
using Backend.Infra.Bases;
using Backend.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Repositries;

public class ProductRepository : BaseRepository<Product>, IProductRepository, IDisposable
{
    private readonly SellerContext _context;
    public ProductRepository(SellerContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Product?> FindByDescription(string description)
        => await _context.Products.FirstOrDefaultAsync(x => x.Description.ToLower() == description.ToLower());

    public void Dispose()
    {
        if (_context != null)
        {
            _context.Dispose();
        }
    }
}