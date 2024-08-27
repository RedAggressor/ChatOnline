using Chat.Host.Datas.Entities;

namespace Chat.Host.Datas
{
    public static class DBInitializer
    {
        public static async Task Initialize(ApplicationDbContext dbContext)
        {
            await dbContext.Database.EnsureCreatedAsync();
                        
            if (!dbContext.Sentiments.Any())
            {
                await dbContext.Sentiments.AddRangeAsync(GetCongiguratSentiments());

                await dbContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<SentimentEntity> GetCongiguratSentiments()
        {
            return new List<SentimentEntity>
            {
                new SentimentEntity { Name = "Positive" },
                new SentimentEntity { Name = "Neutral" },                
                new SentimentEntity { Name = "Negative" }
            };
        }
    }
}
