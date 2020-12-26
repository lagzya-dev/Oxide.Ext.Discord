using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Emojis;
using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageReactionAdd
    {
        [JsonProperty("user_id")]
        public Snowflake UserId { get; set; }

        [JsonProperty("channel_id")]
        public Snowflake ChannelId { get; set; }

        [JsonProperty("message_id")]
        public Snowflake MessageId { get; set; }
        
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        
        [JsonProperty("member")]
        public GuildMember Member { get; set; }

        [JsonProperty("emoji")]
        public Emoji Emoji { get; set; }
    }
}
