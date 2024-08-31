using CustomChat.Persistence.Context;
using CustomChat.Domain.Interfaces.Repository;
using CustomChat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomChat.Persistence.Repositories
{
    public class MessageRepository(ChatDbContext context) : Repository<Message>(context), IMessageRepository
    {
        public async Task<int> DeleteAllFromChatId(int chatId)
            => await context.Messages.Where(x => x.ChatId == chatId).ExecuteDeleteAsync();

        public async Task<IEnumerable<Message>> GetAllFromChatId(int chatId)
            => await context.Messages.Where(x => x.ChatId == chatId).ToListAsync();
    }
}
