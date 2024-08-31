using CustomChat.Domain.Interfaces.Mediator;
using CustomChat.Domain.Interfaces.Repository;
using CustomChat.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomChat.Application.Features.Chats.Commands
{
    public class CreateChatCommand : ICommand<CreateChatCommandResponse>
    {
    }
    public class CreateChatCommandHandler(IChatRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<CreateChatCommand, CreateChatCommandResponse>
    {
        public async Task<Result<CreateChatCommandResponse>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var createdChat = await repository.AddAsync(new Chat() { Name = "" });
            await unitOfWork.SaveAsync();

            return Result.Ok(new CreateChatCommandResponse() { CreatedChatId = createdChat.ChatId }, "Chat creado con exito.");
        }
    }
    public class CreateChatCommandResponse
    {
        public int CreatedChatId { get; init; }
    }
}
