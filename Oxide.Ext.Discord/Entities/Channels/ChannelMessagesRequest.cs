using System.Text;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Channels
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/channel#get-channel-messages">Get Channel Messages Request</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ChannelMessagesRequest
    {
        /// <summary>
        /// Get messages around this message ID
        /// Before, after, and around keys are mutually exclusive, only one may be passed at a time
        /// </summary>
        public Snowflake? Around { get; set; }
        
        /// <summary>
        /// Get messages before this message ID
        /// Before, after, and around keys are mutually exclusive, only one may be passed at a time
        /// </summary>
        public Snowflake? Before { get; set; }
        
        /// <summary>
        /// Get messages after this message ID
        /// Before, after, and around keys are mutually exclusive, only one may be passed at a time
        /// </summary>
        public Snowflake? After { get; set; }
        
        /// <summary>
        /// Max number of messages to return (1-100)
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Returns the request as a query string
        /// </summary>
        /// <returns></returns>
        public string ToQueryString()
        {
            StringBuilder sb = new StringBuilder("?");
            
            //Per Documentation "The before, after, and around keys are mutually exclusive, only one may be passed at a time."
            if (Around.HasValue)
            {
                sb.Append($"around={Around}");
            }
            else if (Before.HasValue)
            {
                sb.Append($"before={Before}");
            }
            else if (After.HasValue)
            {
                sb.Append($"after={After}");
            }
            
            if (Limit.HasValue)
            {
                if (sb.Length != 1)
                {
                    sb.Append("&");
                }
                
                sb.Append($"limit={Limit}");
            }

            return sb.ToString();
        }
    }
}