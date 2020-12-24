using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InviteDeleted
    {
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
