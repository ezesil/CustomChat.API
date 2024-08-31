using CustomChat.Persistence.Context;
using CustomChat.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace CustomChat.Persistence.Repositories
{
    public sealed class UnitOfWork(ChatDbContext context) : IUnitOfWork
    {
        public async Task<IDbTransaction> BeginTransaction(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            return (await context.Database.BeginTransactionAsync(isolationLevel, cancellationToken)).GetDbTransaction();
        }

        public async Task Commit(CancellationToken cancellationToken = default)
        {
            await SaveAsync(cancellationToken);
            await context.Database.CommitTransactionAsync(cancellationToken);
            context.ChangeTracker.Clear();
        }

        public void Rollback()
        {
            context.Database.RollbackTransaction();
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return context.SaveChangesAsync(cancellationToken);
        }
    }
}
