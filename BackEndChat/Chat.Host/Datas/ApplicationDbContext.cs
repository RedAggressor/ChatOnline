using Chat.Host.Datas.Entities;
using Chat.Host.Datas.EntitiyConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Chat.Host.Datas
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            :base(dbContextOptions)
        {
        }

        public DbSet<MessageEntity> Messages { get; set; } = null!;
        public DbSet<SentimentEntity> Sentiments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MessageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SentimentEntityConfiguration());
        }
    }
}
