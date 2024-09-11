using Application.Domain.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface for generic repository, contains Read operation of EF entity
    /// </summary>
    /// <typeparam name="TEntity">EF entity</typeparam>
    public interface IReadRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Get instance of IUnitOfWork
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Used to get a IQueryable that is used to retrieve entities from entire table.
        /// </summary>
        /// <returns>IQueryable to be used to select entities from database</returns>
        public IQueryable<TEntity> Table { get; }

        /// <summary>
        /// Finds an entity with the given primary key value.
        /// </summary>
        /// <typeparam name="TKey">The type of primary key.</typeparam>
        /// <param name="id">The value of the primary key for the entity to be found.</param>
        /// <param name="cancellationToken">Cancellation of the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the <typeparamref name="TEntity" />, or <see langword="null"/>.
        /// </returns>
        Task<TEntity> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default) where TKey : notnull;

        /// <summary>
        /// Gets an entity with given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Entity or null</returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds an entity that matches the encapsulated query logic of the <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <param name="cancellationToken">Cancellation of the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the <typeparamref name="TEntity" />, or <see langword="null"/>.
        /// </returns>
        Task<TEntity> FirstOrDefaultAsync<TSpec>(TSpec specification, CancellationToken cancellationToken = default)
            where TSpec : ISingleResultSpecification<TEntity>, ISpecification<TEntity>;

        /// <summary>
        /// Finds an entity that matches the encapsulated query logic of the <paramref name="specification"/>.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <param name="cancellationToken">Cancellation of the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the <typeparamref name="TResult" />.
        /// </returns>
        Task<TResult> GetBySpecAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds all entities of <typeparamref name="TEntity" /> from the database.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
        /// </returns>
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Used to get all entities based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of all entities</returns>
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds all entities of <typeparamref name="TEntity" />, that matches the encapsulated query logic of the
        /// <paramref name="specification"/>, from the database.
        /// </summary>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <param name="cancellationToken">Cancellation of the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
        /// </returns>
        Task<List<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds all entities of <typeparamref name="TEntity" />, that matches the encapsulated query logic of the
        /// <paramref name="specification"/>, from the database.
        /// <para>
        /// Projects each entity into a new form, being <typeparamref name="TResult" />.
        /// </para>
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <param name="cancellationToken">Cancellation of the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a <see cref="List{TResult}" /> that contains elements from the input sequence.
        /// </returns>
        Task<List<TResult>> GetAllAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Count of entities</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a number that represents how many entities satisfy the encapsulated query logic
        /// of the <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <param name="cancellationToken">Cancellation of the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the
        /// number of elements in the input sequence.
        /// </returns>
        Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    }
}
