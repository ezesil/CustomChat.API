using CustomChat.Persistence.Context;
using CustomChat.Domain.Interfaces.Repository;
using CustomChat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomChat.Persistence.Repositories
{
    public class ChatRepository(ChatDbContext context) : Repository<Chat>(context), IChatRepository
    {
        public async Task<IEnumerable<Chat>> GetAll(CancellationToken cancellationToken = default)
        {
            return await context.Chats.ToListAsync();
        }
    }
}
