using CustomChat.Domain.Interfaces.Repository;
using CustomChat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomChat.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly DbSet<T> _dbSet;
        public Repository(DbContext dbContext) => _dbSet = dbContext.Set<T>();

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
            => (await _dbSet.AddAsync(entity, cancellationToken)).Entity;

        public void Delete(T entity) => _dbSet.Remove(entity);

        public async Task<T?> GetByIdAsync<Tin>(Tin id, CancellationToken cancellationToken = default)
            => await _dbSet.FindAsync([id, cancellationToken], cancellationToken);

        public void Update(T entity) => _dbSet.Update(entity);
    }
}
