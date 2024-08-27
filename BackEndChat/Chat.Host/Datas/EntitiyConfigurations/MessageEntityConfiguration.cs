using Chat.Host.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Host.Datas.EntitiyConfigurations
{
    public class MessageEntityConfiguration : IEntityTypeConfiguration<MessageEntity>
    {
        public void Configure(EntityTypeBuilder<MessageEntity> builder)
        {
            builder.ToTable("Message");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseHiLo("messsage_hilo")
                .IsRequired();

            builder.Property(x => x.Message)
                .IsRequired();

            builder.HasOne(x => x.Sentiment)
                .WithMany()
                .HasForeignKey(x => x.SentimentId);
        }
    }
}
