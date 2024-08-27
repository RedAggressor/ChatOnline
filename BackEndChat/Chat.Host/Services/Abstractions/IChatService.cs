namespace Chat.Host.Services.Abstractions
{
    public interface IChatService
    {
        Task<string> ProcessedMessageAsync(string message);
    }
}
