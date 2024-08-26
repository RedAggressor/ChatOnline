using Chat.Host.Datas;
using Chat.Host.Datas.Entities;
using Chat.Host.Helpers.Abstractions;
using Chat.Host.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Chat.Host.Repositories
{
    public class MessageRepository: IMessageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MessageRepository(IDbContextWrapper<ApplicationDbContext> dbContext) 
        {
            _dbContext = dbContext.DbContext;
        }

        public async Task<int> AddMessageAsync(MessageEntity message) 
        {
            var entity = await _dbContext.Messages.AddAsync(message);

            await _dbContext.SaveChangesAsync();

            return entity.Entity.Id;
        }

        public async Task<MessageEntity> GetMessageByIdAsync(int id)
        {
            var entity = await _dbContext.Messages.FirstOrDefaultAsync(f=>f.Id == id);

            return entity!;
        }

        public async Task<ICollection<MessageEntity>> GetMessagesBySentimentIdAsync(int id)
        {
            return await _dbContext.Messages
                .Include(i => i.Sentiment)
                .Select(item => item)
                .Where(item => item.SentimentId == id)
                .ToListAsync();
        }

        public async Task<ICollection<MessageEntity>> GetMessagesAsync()
        {
            return await _dbContext.Messages
                .Include(i=>i.Sentiment)
                .ToListAsync();
        }
    }
}
