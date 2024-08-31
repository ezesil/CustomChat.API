using System.Data;


namespace CustomChat.Domain.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        Task<IDbTransaction> BeginTransaction(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);
        Task Commit(CancellationToken cancellationToken = default);
        void Rollback();
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}
