using CustomChat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomChat.Persistence.Context
{
    public class ChatDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }

        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().HasKey(x => x.MessageId);

            modelBuilder.Entity<Chat>().HasKey(x => x.ChatId);

            modelBuilder.Entity<Chat>()
            .HasMany(chat => chat.Messages)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
