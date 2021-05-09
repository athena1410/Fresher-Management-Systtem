using System.Collections.Generic;

namespace Application.Core.Interfaces.Persistence
{
    /// <summary>
    /// Interface for generic repository, contains CRUD operation of EF entity
    /// </summary>
    /// <typeparam name="T">EF entity</typeparam>
    public interface IBaseRepository<T> : IReadRepository<T> where T : class
    {
        /// <summary>
        /// Adds an entity in the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(T entity);

        /// <summary>
        /// Updates an entity in the database
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Removes an entity in the database
        /// </summary>
        void Delete(T entity);

        /// <summary>
        /// Removes the given entities in the database
        /// </summary>
        void DeleteRange(IEnumerable<T> entities);
    }
}
