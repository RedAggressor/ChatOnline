namespace Chat.Host.Hubs.Abstractions
{
    public interface IChatHub
    {
        public Task ReceiveMessage(string name, string message, string color = null!);
    }
}
