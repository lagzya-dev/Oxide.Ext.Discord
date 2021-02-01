using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#guild-member-remove">Guild Member Remove</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildMemberRemove
    {
        /// <summary>
        /// The id of the guild
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        /// <summary>
        /// The user who was removed
        /// </summary>
        [JsonProperty("user")]
        public DiscordUser User { get; set; }
    }
}
