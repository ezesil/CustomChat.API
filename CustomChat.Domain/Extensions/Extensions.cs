using Azure.AI.OpenAI;
using CustomChat.Domain.Models;

namespace CustomChat.Domain.Extensions
{
    public static class MessageExtensions
    {
        public static ChatRequestMessage ToChatRequestMessage(this Message e)
        {
            if (e.Role == ChatRole.User)
            {
                return new ChatRequestUserMessage(e.Content);
            }
            else if (e.Role == ChatRole.Assistant)
            {
                return new ChatRequestAssistantMessage(e.Content);
            }
            else if (e.Role == ChatRole.System)
            {
                return new ChatRequestSystemMessage(e.Content);
            }
            else
            {
                return new ChatRequestSystemMessage("");
            }
        }
    }

    public static class ChatRequestMessageExtensions
    {
        public static Message ToChatRequestMessage(this ChatRequestMessage e, int chatId)
        {
            if (e.Role == ChatRole.User)
            {
                var temp = e as ChatRequestUserMessage;
                return new Message(chatId, temp.Role.ToString(), temp.Content);
            }
            else if (e.Role == ChatRole.Assistant)
            {
                var temp = e as ChatRequestAssistantMessage;
                return new Message(chatId, temp.Role.ToString(), temp.Content);
            }
            else if (e.Role == ChatRole.System)
            {
                var temp = e as ChatRequestSystemMessage;
                return new Message(chatId, temp.Role.ToString(), temp.Content);
            }
            else
            {
                var temp = e as ChatRequestSystemMessage;
                return new Message(chatId, temp.Role.ToString(), "");
            }
        }
    }

    public static class ChatResponseMessageExtensions
    {
        public static Message ToMessage(this ChatResponseMessage e, int chatId)
        {
            return new Message(chatId, e.Role.ToString(), e.Content);
        }
    }
}
