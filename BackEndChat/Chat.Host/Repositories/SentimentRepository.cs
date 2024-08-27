using Chat.Host.Datas;
using Chat.Host.Datas.Entities;
using Chat.Host.Helpers.Abstractions;
using Chat.Host.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Chat.Host.Repositories
{
    public class SentimentRepository : ISentimentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SentimentRepository(IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task<int> AddSentimentAsync(string nameSentimento)
        {
            var entity = await _dbContext.Sentiments
                .AddAsync(new SentimentEntity()
            {
                Name = nameSentimento
            });
            
            await _dbContext.SaveChangesAsync();

            return entity.Entity.Id;
        }

        public async Task<SentimentEntity?> GetSentimentByIdAsync(int id)
        {
            return await _dbContext.Sentiments
                .FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<ICollection<SentimentEntity>> GetSentimentsAsync()
        {
            return await _dbContext.Sentiments.ToListAsync();
        }
    }
}
