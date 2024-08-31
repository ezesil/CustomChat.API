using Azure;
using Azure.AI.OpenAI;
using CustomChat.Domain.Extensions;
using CustomChat.Domain.Models;
using CustomChat.Domain.Interfaces.Repository;
using CustomChat.Domain.Interfaces.Mediator;

namespace CustomChat.Application.Features.Messages.Commands
{
    public class AskCommand : ICommand<AskResponse>
    {
        public int ChatId { get; init; }
        public string? Content { get; init; }
    }
    public class AskCommandHandler(
        IMessageRepository messageRepository,
        IUnitOfWork unitOfWork,
        OpenAIClient _client
        ) : ICommandHandler<AskCommand, AskResponse>
    {
        public async Task<Result<AskResponse>> Handle(AskCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Content))
                return Result.Error<AskResponse>($"El mensaje no puede ser nulo.");

            List<ChatRequestMessage> values = new List<ChatRequestMessage>();


            await messageRepository.AddAsync(
                new Message()
                {
                    Content = request.Content,
                    Role = "user",
                    ChatId = request.ChatId
                }, cancellationToken);

            await unitOfWork.SaveAsync();

            // Consultar todos los mensajes
            var allMessages = await messageRepository.GetAllFromChatId(request.ChatId);

            values.AddRange(allMessages?.Select(x => x.ToChatRequestMessage())!);


            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = "gpt-4o-mini" // Use DeploymentName for "model" with non-Azure clients
            };

            chatCompletionsOptions.Messages.Add(new ChatRequestSystemMessage(
                " "
                ));
            values.ForEach(chatCompletionsOptions.Messages.Add);

            Response<ChatCompletions> response = await _client.GetChatCompletionsAsync(chatCompletionsOptions);
            Message parsedResponse = response.Value.Choices[0].Message.ToMessage(request.ChatId);

            await messageRepository.AddAsync(parsedResponse);

            await unitOfWork.SaveAsync();

            return new AskResponse()
            {
                Message = parsedResponse,
            };
        }
    }
    public class AskResponse
    {
        public Message? Message { get; init; }
    }
}
