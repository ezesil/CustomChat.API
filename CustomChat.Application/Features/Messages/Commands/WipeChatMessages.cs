using CustomChat.Domain.Interfaces.Mediator;
using CustomChat.Domain.Interfaces.Repository;
using CustomChat.Domain.Models;

namespace CustomChat.Application.Features.Messages.Commands
{
    public class WipeChatMessagesCommand : ICommand
    {
        public int ChatId { get; init; }
    }
    public class WipeChatMessagesHandler(IMessageRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<WipeChatMessagesCommand>
    {
        public async Task<Result> Handle(WipeChatMessagesCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteAllFromChatId(request.ChatId);
            await unitOfWork.SaveAsync();

            return Result.Ok($"All messages deleted from chat number {request.ChatId}.");
        }
    }
}
