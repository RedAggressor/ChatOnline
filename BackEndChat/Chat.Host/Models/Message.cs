namespace Chat.Host.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? MessageText { get; set; }
        public Sentiment? Sentiment { get; set; }
        public int SentimentId { get; set; }

    }
}
