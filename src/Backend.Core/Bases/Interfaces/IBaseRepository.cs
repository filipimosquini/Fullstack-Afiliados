namespace Backend.Core.Bases.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    IUnitOfWork UnitOfWork { get; }
    TEntity Add(TEntity entity);
    void AddRange(ICollection<TEntity> entity);
    TEntity FindById(params object[] keyValues);
    TEntity Update(TEntity entity);
    void UpdateRange(ICollection<TEntity> entities);
    TEntity Remove(TEntity entity);

    Task<TEntity> AddAsync(TEntity entity);
    Task AddRangeAsync(ICollection<TEntity> entity);
    Task<TEntity> FindById(object id);
}