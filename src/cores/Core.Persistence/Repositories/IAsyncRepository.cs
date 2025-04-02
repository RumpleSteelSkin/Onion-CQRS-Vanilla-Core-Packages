using System.Linq.Expressions;
using Core.Persistence.Entities;

// ReSharper disable TypeParameterCanBeVariant

namespace Core.Persistence.Repositories;

public interface IAsyncRepository<TEntity, TId> where TEntity : Entity<TId>
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities,
        CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities,
        CancellationToken cancellationToken = default);

    Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities,
        CancellationToken cancellationToken = default);

    Task<TEntity> HardDeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<ICollection<TEntity>> HardDeleteRangeAsync(ICollection<TEntity> entities,
        CancellationToken cancellationToken = default);

    Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
        bool ignoreQueryFilters = false, bool enableTracking = true,
        bool include = true, CancellationToken cancellationToken = default);

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false,
        bool enableTracking = true, bool include = true,
        CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filter = null, bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(TId id, bool ignoreQueryFilters = false, bool enableTracking = true,
        bool include = true,
        CancellationToken cancellationToken = default);

    Task<ICollection<TEntity?>> GetByIdsAsync(ICollection<TId>? ids, bool ignoreQueryFilters = false,
        bool enableTracking = true,
        bool include = true,
        CancellationToken cancellationToken = default);

    Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null, bool ignoreQueryFilters = false,
        bool include = true,
        CancellationToken cancellationToken = default);
}