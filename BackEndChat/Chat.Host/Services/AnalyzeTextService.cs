using Azure;
using Azure.AI.TextAnalytics;
using Chat.Host.Configurations;
using Chat.Host.Services.Abstractions;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Chat.Host.Services
{
    public class AnalyzeTextService : IAnalyzeTextService
    {
        //private const string _privateKey = config.Value.Endpoint;//"e6377110c7ed43fc95cc29016c9cb27e";
        //"https://azureanalyzesentiment.cognitiveservices.azure.com/";


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
