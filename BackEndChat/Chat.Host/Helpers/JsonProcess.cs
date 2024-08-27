using Chat.Host.Helpers.Abstractions;
using Newtonsoft.Json;

namespace Chat.Host.Helpers
{
    public class JsonProcess : IJsonProcess
    {
        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public T Deserialize<T>(string json) 
        {
            return JsonConvert.DeserializeObject<T>(json)!;
        }
    }
}
