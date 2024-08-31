using CustomChat.Domain.Interfaces.Mediator;
using CustomChat.Domain.Interfaces.Repository;
using CustomChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomChat.Application.Features.Messages.Commands
{
    public class EditMessageCommand : ICommand
    {
        public int MessageId { get; init; }
        public int ChatId { get; init; }
        public string Role { get; init; } = string.Empty;
        public string Content { get; init; } = string.Empty;
    }
    public class EditMessageCommandHandler(IMessageRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<EditMessageCommand>
    {
        public async Task<Result> Handle(EditMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Message()
            {
                MessageId = request.MessageId,
                ChatId = request.ChatId,
                Role = request.Role,
                Content = request.Content
            };

            repository.Update(message);
            await unitOfWork.SaveAsync();

            return Result.Ok("Mensaje editado con exito.");
        }
    }
}
