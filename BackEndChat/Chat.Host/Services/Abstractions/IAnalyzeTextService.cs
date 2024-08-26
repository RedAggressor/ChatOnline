using Azure.AI.TextAnalytics;

namespace Chat.Host.Services.Abstractions
{
    public interface IAnalyzeTextService
    {
        Task<TextSentiment> AnalyzeMessage(string message);
    }
}
