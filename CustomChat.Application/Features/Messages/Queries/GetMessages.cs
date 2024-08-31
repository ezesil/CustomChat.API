using CustomChat.Domain.Interfaces.Mediator;
using CustomChat.Domain.Interfaces.Repository;
using CustomChat.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomChat.Application.Features.Messages.Queries
{
    public class GetMessagesQuery : IQuery<GetMessagesResponse>
    {
        public int ChatId { get; init; }
    }
    public class GetMessagesHandler(IMessageRepository repository) : IQueryHandler<GetMessagesQuery, GetMessagesResponse>
    {
        public async Task<Result<GetMessagesResponse>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            return new GetMessagesResponse()
            {
                Messages = (await repository.GetAllFromChatId(request.ChatId)).ToArray()
            };
        }
    }
    public class GetMessagesResponse
    {
        public Message[] Messages { get; init; }
    }
}
