using Chat.Host.Datas.Entities;

namespace Chat.Host.Repositories.Abstractions
{
    public interface ISentimentRepository
    {
        Task<int> AddSentimentAsync(string nameSentimento);
        Task<SentimentEntity?> GetSentimentByIdAsync(int id);
        Task<ICollection<SentimentEntity>> GetSentimentsAsync();
    }
}
