namespace Oxide.Ext.Discord.REST
{
    using Newtonsoft.Json;

    public class RestResponse
    {
        public string Data { get; }

        public RestResponse(string data)
        {
            Data = data;
        }

        public T ParseData<T>() => !string.IsNullOrEmpty(Data) ? JsonConvert.DeserializeObject<T>(Data) : default(T);
    }
}
