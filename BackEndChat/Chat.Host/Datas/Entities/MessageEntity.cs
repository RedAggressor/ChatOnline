namespace Chat.Host.Datas.Entities
{
    public class MessageEntity
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public int SentimentId { get; set; }
        public SentimentEntity Sentiment { get; set; } = null!;
    }
}
