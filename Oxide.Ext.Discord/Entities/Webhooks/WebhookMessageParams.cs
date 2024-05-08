using Oxide.Ext.Discord.Builders;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Libraries;

namespace Oxide.Ext.Discord.Entities
{
    /// <summary>
    /// Represents webhook message query string parameters 
    /// </summary>
    public class WebhookMessageParams : IDiscordQueryString
    {
        /// <summary>
        /// If the message exists in a thread
        /// This field is optional and defaults to null
        /// </summary>
        public Snowflake? ThreadId { get; set; }
        
        /// <inheritdoc/>
        public string ToQueryString()
        {
            QueryStringBuilder builder = QueryStringBuilder.Create(DiscordPool.Internal);

            if (ThreadId.HasValue)
            {
                builder.Add("thread_id", ThreadId.Value.ToString());
            }
            
            return builder.ToStringAndFree();
        }
    }
}