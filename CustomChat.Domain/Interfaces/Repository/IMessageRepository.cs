using CustomChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomChat.Domain.Interfaces.Repository
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<int> DeleteAllFromChatId(int chatId);

        Task<IEnumerable<Message>> GetAllFromChatId(int chatId);
    }
}
