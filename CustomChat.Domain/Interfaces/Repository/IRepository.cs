using CustomChat.Domain.Models;

namespace CustomChat.Domain.Interfaces.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        void Update(T entity);
        void Delete(T entity);
        Task<T?> GetByIdAsync<Tin>(Tin id, CancellationToken cancellationToken = default);
    }
}
