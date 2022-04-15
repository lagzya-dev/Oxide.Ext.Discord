using Newtonsoft.Json;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Entities.Api
{
    /// <summary>
    /// Represents a REST response from discord
    /// </summary>
    public class RestResponse : BasePoolable
    {
        /// <summary>
        /// Data discord sent us
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// Create new REST response with the given data
        /// </summary>
        /// <param name="data"></param>
        public void Init(string data)
        {
            Data = data;
        }

        /// <summary>
        /// Parse the data to it's given object
        /// </summary>
        /// <typeparam name="T">Type to be parsed as</typeparam>
        /// <returns>Data string JSON parsed to object</returns>
        public T ParseData<T>() => !string.IsNullOrEmpty(Data) ? JsonConvert.DeserializeObject<T>(Data, DiscordExtension.ExtensionSerializeSettings) : default(T);
    }
}
