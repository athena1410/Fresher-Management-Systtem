using Application.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface for generic repository, contains CRUD operation of EF entity
    /// </summary>
    /// <typeparam name="TEntity">EF entity</typeparam>
    public interface IRepository<TEntity> : IReadRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Adds an entity in the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Add a new entity and gets it's Id.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Id of the entity</returns>
        Task<TKey> AddAndGetIdAsync<TKey>(TEntity entity, CancellationToken cancellationToken = default) where TKey : class;

        /// <summary>
        /// Updates an entity in the database
        /// </summary>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Removes an entity in the database
        /// </summary>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Removes the given entities in the database
        /// </summary>
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    }
}
