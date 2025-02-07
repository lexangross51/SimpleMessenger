using SimpleMessenger.DataAccess.Models.Abstractions;

namespace SimpleMessenger.DataAccess.Storage.Abstractions;

public interface IRepository<TId, TEntity> 
    where TId : notnull
    where TEntity : IEntity<TId>
{
    Task<IEnumerable<TEntity>> GetAll(ISpecification? specification = default, CancellationToken token = default);

    Task Create(TEntity entity, CancellationToken token = default);
}