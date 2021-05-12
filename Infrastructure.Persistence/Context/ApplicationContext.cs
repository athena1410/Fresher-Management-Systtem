using Application.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Interfaces;
using Application.Domain.Entities;

namespace Infrastructure.Persistence.Context
{
    public class ApplicationContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "dbo";
        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<EntryTest> EntryTests { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<TraineeCandidateProfile> TraineeCandidateProfiles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.HasSequence<int>("CandidateSequence").StartsAt(1).IncrementsBy(1);
        }

        public IExecutionStrategy CreateExecutionStrategy() => Database.CreateExecutionStrategy();

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) 
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await base.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void Rollback()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
