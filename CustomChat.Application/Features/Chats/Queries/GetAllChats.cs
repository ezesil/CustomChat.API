using CustomChat.Domain.Interfaces.Mediator;
using CustomChat.Domain.Interfaces.Repository;
using CustomChat.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomChat.Application.Features.Chats.Queries
{
    public class GetAllChatsQuery : IQuery<GetAllChatsQueryResponse>
    {
    }
    public class GetAllChatsQueryHandler(IChatRepository chatsRepository) : IQueryHandler<GetAllChatsQuery, GetAllChatsQueryResponse>
    {
        public async Task<Result<GetAllChatsQueryResponse>> Handle(GetAllChatsQuery request, CancellationToken cancellationToken)
        {
            return new GetAllChatsQueryResponse()
            {
                Chats = (await chatsRepository.GetAll(cancellationToken)).ToList()
            };
        }
    }
    public class GetAllChatsQueryResponse
    {
        public List<Chat>? Chats { get; init; }
    }
}
