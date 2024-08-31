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
    public class GetChatQuery : IQuery<GetChatQueryResponse>
    {
        public int Id { get; init; }
    }
    public class GetChatQueryHandler(IChatRepository chatsRepository, IMessageRepository messageRepository) : IQueryHandler<GetChatQuery, GetChatQueryResponse>
    {
        public async Task<Result<GetChatQueryResponse>> Handle(GetChatQuery request, CancellationToken cancellationToken)
        {
            var chat = await chatsRepository.GetByIdAsync(request.Id);

            if (chat == null) return Result.Error<GetChatQueryResponse>($"El chat no existe. ID de chat: {request.Id}.");

            chat.Messages = (await messageRepository.GetAllFromChatId(chat.ChatId)).ToList();

            return new GetChatQueryResponse()
            {
                Chat = chat
            };
        }
    }
    public class GetChatQueryResponse
    {
        public Chat? Chat { get; init; }
    }
}
