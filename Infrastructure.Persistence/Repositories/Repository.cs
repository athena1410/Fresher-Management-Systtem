using Application.Core.Interfaces.Repositories;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Domain.Entities;
using Application.Core.Interfaces;

namespace Infrastructure.Persistence.Repositories
{
    /// <inheritdoc/>
    public abstract class Repository<TDbContext, TEntity> : IRepository<TEntity>
        where TDbContext : DbContext, IUnitOfWork
        where TEntity : Entity
    {
        private readonly TDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly ISpecificationEvaluator _specificationEvaluator;
        public IUnitOfWork UnitOfWork => _context;
        public IQueryable<TEntity> Table => _dbSet.AsQueryable();

        protected Repository(TDbContext context)
            : this(context, SpecificationEvaluator.Default)
        {
        }

        protected Repository(TDbContext context, ISpecificationEvaluator specificationEvaluator)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
            this._specificationEvaluator = specificationEvaluator;
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            return await Task.FromResult(entity);
        }

        /// <inheritdoc/>
        public virtual async Task<TKey> AddAndGetIdAsync<TKey>(TEntity entity, CancellationToken cancellationToken) where TKey : class
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id as TKey;
        }

        /// <inheritdoc/>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        /// <inheritdoc/>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        /// <inheritdoc/>
        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await Task.CompletedTask;
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            return await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity> FirstOrDefaultAsync<TSpec>(TSpec specification,
            CancellationToken cancellationToken = default) where TSpec : ISpecification<TEntity>, ISingleResultSpecification
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<TResult> GetBySpecAsync<TResult>(ISpecification<TEntity, TResult> specification,
            CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await this._dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<List<TEntity>> GetAllAsync(ISpecification<TEntity> specification,
            CancellationToken cancellationToken = default)
        {
            var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

            return specification.PostProcessingAction == null ? queryResult
                : specification.PostProcessingAction(queryResult).ToList();
        }

        /// <inheritdoc/>
        public virtual async Task<List<TResult>> GetAllAsync<TResult>(ISpecification<TEntity, TResult> specification,
            CancellationToken cancellationToken = default)
        {
            var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

            return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
        }

        /// <inheritdoc/>
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await this._dbSet.Where(predicate).CountAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification, true).CountAsync(cancellationToken);
        }

        /// <summary>
        /// Filters the entities  of <typeparamref name="TEntity"/>, to those that match the encapsulated query logic of the
        /// <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <param name="evaluateCriteriaOnly"></param>
        /// <returns>The filtered entities as an <see cref="IQueryable{T}"/>.</returns>
        protected virtual IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification, bool evaluateCriteriaOnly = false)
        {
            return _specificationEvaluator.GetQuery(_context.Set<TEntity>().AsQueryable(), specification, evaluateCriteriaOnly);
        }

        /// <summary>
        /// Filters all entities of <typeparamref name="TEntity" />, that matches the encapsulated query logic of the
        /// <paramref name="specification"/>, from the database.
        /// <para>
        /// Projects each entity into a new form, being <typeparamref name="TResult" />.
        /// </para>
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <returns>The filtered projected entities as an <see cref="IQueryable{T}"/>.</returns>
        protected virtual IQueryable<TResult> ApplySpecification<TResult>(ISpecification<TEntity, TResult> specification)
        {
            if (specification is null) throw new ArgumentNullException(nameof(specification));
            if (specification.Selector is null) throw new SelectorNotFoundException();

            return _specificationEvaluator.GetQuery(_context.Set<TEntity>().AsQueryable(), specification);
        }
    }
}
