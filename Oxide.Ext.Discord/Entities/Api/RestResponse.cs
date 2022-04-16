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
        private string _data;

        private BotClient _client;

        /// <summary>
        /// Create new REST response with the given data
        /// </summary>
        /// <param name="client">BotClient for the response</param>
        /// <param name="data"></param>
        public void Init(BotClient client, string data)
        {
            _client = client;
            _data = data;
        }

        /// <summary>
        /// Parse the data to it's given object
        /// </summary>
        /// <typeparam name="T">Type to be parsed as</typeparam>
        /// <returns>Data string JSON parsed to object</returns>
        public T ParseData<T>() => !string.IsNullOrEmpty(_data) ? JsonConvert.DeserializeObject<T>(_data, _client.ClientSerializerSettings) : default(T);

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            _data = null;
            _client = null;
        }
    }
}
