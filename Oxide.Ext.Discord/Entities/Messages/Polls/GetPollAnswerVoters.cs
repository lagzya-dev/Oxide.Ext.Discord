using Newtonsoft.Json;
using Oxide.Ext.Discord.Builders;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Libraries;

namespace Oxide.Ext.Discord.Entities
{

    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/poll#get-answer-voters-query-string-params">Get Answer Voters Query String Params</a>
    /// </summary>
    public class GetPollAnswerVoters : IDiscordQueryString
    {
        /// <summary>
        /// Get users after this user ID
        /// </summary>
        [JsonProperty("after")]
        public Snowflake? After { get; set; } 
        
        /// <summary>
        /// Max number of users to return (1-100)
        /// </summary>
        [JsonProperty("limit")]
        public int? Limit { get; set; } 
        
        ///<inheritdoc/>
        public string ToQueryString()
        {
            QueryStringBuilder builder = QueryStringBuilder.Create(DiscordPool.Internal);
            
            if (After.HasValue)
            {
                builder.Add("after", After.Value.ToString());
            }
            
            if (Limit.HasValue)
            {
                builder.Add("limit", Limit.Value.ToString());
            }
            
            return builder.ToStringAndFree();
        }
    }
}