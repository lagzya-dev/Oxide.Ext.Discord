using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#guild-ban-add">Guild Ban Add</a> Event
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#guild-ban-remove">Guild Ban Remove</a> Event
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildBanEvent
    {
        /// <summary>
        /// ID of the guild
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        
        /// <summary>
        /// The banned / unbanned user
        /// </summary>
        [JsonProperty("user")]
        public DiscordUser User { get; set; }
    }
}
