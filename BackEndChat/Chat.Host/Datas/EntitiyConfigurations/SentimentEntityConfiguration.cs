using Chat.Host.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Host.Datas.EntitiyConfigurations
{
    public class SentimentEntityConfiguration : IEntityTypeConfiguration<SentimentEntity>
    {
        public void Configure(EntityTypeBuilder<SentimentEntity> builder)
        {
            builder.ToTable("Sentiment");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseHiLo("message_sentiment_hilo")
                .IsRequired();

            builder.Property(p=>p.Name)                
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
