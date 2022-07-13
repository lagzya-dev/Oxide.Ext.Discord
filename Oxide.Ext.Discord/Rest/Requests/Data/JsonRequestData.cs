using System.Text;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Rest.Requests.Data
{
    /// <summary>
    /// Represents Request Data to be passed into the REST API request
    /// </summary>
    public class JsonRequestData : BaseRequestData
    {
        private const string DefaultContentType = "application/json";

        /// <summary>
        /// Initializes the Request Data
        /// </summary>
        public void Init(DiscordClient client, object data)
        {
            Data = data;
            ContentType = DefaultContentType;
            if (Data != null)
            {
                StringContents = JsonConvert.SerializeObject(Data, client.Bot.ClientSerializerSettings);
                Contents = Encoding.UTF8.GetBytes(StringContents);
            }
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}