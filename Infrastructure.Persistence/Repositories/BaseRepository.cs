using Application.Core.Interfaces.Persistence;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    /// <inheritdoc/>
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationContext _context;
        private readonly ISpecificationEvaluator _specificationEvaluator;
        public IUnitOfWork UnitOfWork => _context;

        protected BaseRepository(ApplicationContext context)
            : this(context, SpecificationEvaluator.Default)
        {
        }

        protected BaseRepository(ApplicationContext context, ISpecificationEvaluator specificationEvaluator)
        {
            this._context = context;
            this._specificationEvaluator = specificationEvaluator;
        }

        /// <inheritdoc/>
        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        /// <inheritdoc/>
        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <inheritdoc/>
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <inheritdoc/>
        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        /// <inheritdoc/>
        public virtual async Task<T> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            return await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<T> GetBySpecAsync<TSpec>(TSpec specification, 
            CancellationToken cancellationToken = default) where TSpec : ISpecification<T>, ISingleResultSpecification
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<TResult> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, 
            CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<List<T>> ListAsync(ISpecification<T> specification, 
            CancellationToken cancellationToken = default)
        {
            var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

            return specification.PostProcessingAction == null ? queryResult 
                : specification.PostProcessingAction(queryResult).ToList();
        }

        /// <inheritdoc/>
        public virtual async Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, 
            CancellationToken cancellationToken = default)
        {
            var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

            return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
        }

        /// <inheritdoc/>
        public virtual async Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification, true).CountAsync(cancellationToken);
        }

        /// <summary>
        /// Filters the entities  of <typeparamref name="T"/>, to those that match the encapsulated query logic of the
        /// <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <param name="evaluateCriteriaOnly"></param>
        /// <returns>The filtered entities as an <see cref="IQueryable{T}"/>.</returns>
        protected virtual IQueryable<T> ApplySpecification(ISpecification<T> specification, bool evaluateCriteriaOnly = false)
        {
            return _specificationEvaluator.GetQuery(_context.Set<T>().AsQueryable(), specification, evaluateCriteriaOnly);
        }

        /// <summary>
        /// Filters all entities of <typeparamref name="T" />, that matches the encapsulated query logic of the
        /// <paramref name="specification"/>, from the database.
        /// <para>
        /// Projects each entity into a new form, being <typeparamref name="TResult" />.
        /// </para>
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <returns>The filtered projected entities as an <see cref="IQueryable{T}"/>.</returns>
        protected virtual IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
        {
            if (specification is null) throw new ArgumentNullException(nameof(specification));
            if (specification.Selector is null) throw new SelectorNotFoundException();

            return _specificationEvaluator.GetQuery(_context.Set<T>().AsQueryable(), specification);
        }
    }
}
