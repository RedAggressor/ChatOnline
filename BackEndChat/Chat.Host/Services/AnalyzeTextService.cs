using Azure;
using Azure.AI.TextAnalytics;
using Chat.Host.Configurations;
using Chat.Host.Services.Abstractions;
using Microsoft.Extensions.Options;

namespace Chat.Host.Services
{
    public class AnalyzeTextService : IAnalyzeTextService
    {
        private readonly AzureKeyCredential _azureCredential;
        private readonly TextAnalyticsClient _client;

        public AnalyzeTextService(IOptions<AnalyzeConfig> config) 
        {
            var _endponitPath = config.Value.Endpoint;
            var _uriEndpoint = new Uri(_endponitPath);
            _azureCredential = new AzureKeyCredential(config.Value.PrivateKey);
            _client = new TextAnalyticsClient(_uriEndpoint, _azureCredential);

            var _privateKey = config.Value.Endpoint;
            _azureCredential = new AzureKeyCredential(_privateKey);
        }
        
        public async Task<TextSentiment> AnalyzeMessage(string message)
        {
            var documents = new List<string>()
            {
                message
            };

            AnalyzeSentimentResultCollection? result = null;

            try
            {
                result = await _client
                    .AnalyzeSentimentBatchAsync(documents, options: new AnalyzeSentimentOptions()
                    {
                        IncludeOpinionMining = true
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var res = result!.Select(x => x.DocumentSentiment.Sentiment).ToList();

            return res[0]; // more specificе result ??

        }
    }
}
