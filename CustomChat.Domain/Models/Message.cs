namespace CustomChat.Domain.Models
{
    public class Message : Entity
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
        public string Role { get; set; }
        public string Content { get; set; }

        public Message()
        {

        }

        public Message(int chatId, string role, string content)
        {
            ChatId = chatId;
            Role = role;
            Content = content;
        }
    }
}
