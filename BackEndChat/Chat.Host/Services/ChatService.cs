using Azure.AI.TextAnalytics;
using Chat.Host.Datas.Entities;
using Chat.Host.Repositories.Abstractions;
using Chat.Host.Services.Abstractions;

namespace Chat.Host.Services
{
    public class ChatService : IChatService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ISentimentRepository _sentimentRepository;
        private readonly IAnalyzeTextService _analyzeSentiment;

        public ChatService(
            IMessageRepository messageRepository,
            ISentimentRepository sentimentRepository,
            IAnalyzeTextService analyzeSentiment)
        {
            _messageRepository = messageRepository;
            _sentimentRepository = sentimentRepository;
            _analyzeSentiment = analyzeSentiment;
        }

        public async Task<string> ProcessedMessageAsync(string message)
        {
            TextSentiment sentimentResult = TextSentiment.Neutral;

            try 
            {
                sentimentResult = await _analyzeSentiment.AnalyzeMessage(message);

                var listSentiment = await _sentimentRepository.GetSentimentsAsync();

                var sentimentEntity = listSentiment.FirstOrDefault(f => f.Name == sentimentResult.ToString());

                await _messageRepository.AddMessageAsync(new MessageEntity()
                {
                    Message = message,
                    SentimentId = sentimentEntity.Id,
                });
            }
            catch (Exception ex)
            { 
                // _logger.Error(ex.Message);
            }            

            return SetColor(sentimentResult);
        }

        private string SetColor(TextSentiment textSentiment) =>
            textSentiment switch
            {
                TextSentiment.Positive => "green",
                TextSentiment.Negative => "red",
                _ => "grey"
            };
    }
}
