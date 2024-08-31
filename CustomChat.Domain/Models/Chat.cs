namespace CustomChat.Domain.Models
{
    public class Chat : Entity
    {
        public int ChatId { get; set; }
        public string? Name { get; set; } = "";
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
