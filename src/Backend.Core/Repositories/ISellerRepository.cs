using Backend.Core.Bases.Interfaces;
using Backend.Core.Entities;

namespace Backend.Core.Repositories;

public interface ISellerRepository : IBaseRepository<Seller>
{
    Task<Seller?> FindByName(string name);
}