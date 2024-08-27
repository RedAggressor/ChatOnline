namespace Chat.Host.Helpers.Abstractions
{
    public interface IJsonProcess
    {
        string Serialize<T>(T data);
        T Deserialize<T>(string json);
    }
}
