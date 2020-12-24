using Newtonsoft.Json;

namespace Oxide.Ext.Discord.REST
{
    public class RestResponse
    {
        public string Data { get; }

        public RestResponse(string data)
        {
            Data = data;
        }

        public T ParseData<T>() => JsonConvert.DeserializeObject<T>(Data);
    }
}
