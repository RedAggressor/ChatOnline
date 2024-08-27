using Chat.Host.Datas.Entities;

namespace Chat.Host.Repositories.Abstractions
{
    public interface IMessageRepository
    {
        Task<int> AddMessageAsync(MessageEntity message);
        Task<MessageEntity> GetMessageByIdAsync(int id);
        Task<ICollection<MessageEntity>> GetMessagesBySentimentIdAsync(int id);
        Task<ICollection<MessageEntity>> GetMessagesAsync();
    }
}
