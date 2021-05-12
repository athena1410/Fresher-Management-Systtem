using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        bool HasActiveTransaction { get; }
        IExecutionStrategy CreateExecutionStrategy();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default);
        void Rollback();
    }
}
