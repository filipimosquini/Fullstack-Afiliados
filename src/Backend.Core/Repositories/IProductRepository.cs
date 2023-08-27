using Backend.Core.Bases.Interfaces;
using Backend.Core.Entities;

namespace Backend.Core.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product?> FindByDescription(string description);
}