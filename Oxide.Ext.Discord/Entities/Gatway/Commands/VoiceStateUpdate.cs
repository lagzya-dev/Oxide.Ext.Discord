using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Commands
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]

    public class VoiceStateUpdate
    {
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }

        [JsonProperty("channel_id")]
        public Snowflake ChannelId { get; set; }

        [JsonProperty("self_mute")]
        public bool SelfMute { get; set; }

        [JsonProperty("self_deaf")]
        public bool SelfDeaf { get; set; }
    }
}