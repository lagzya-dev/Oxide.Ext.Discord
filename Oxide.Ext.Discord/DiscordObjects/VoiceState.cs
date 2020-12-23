using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class VoiceState
    {
        public string guild_id { get; set; }

        public string channel_id { get; set; }

        public string user_id { get; set; }
        
        [JsonProperty("member")]
        public GuildMember Member { get; set; }

        public string session_id { get; set; }

        public bool deaf { get; set; }

        public bool mute { get; set; }

        public bool self_deaf { get; set; }

        public bool self_mute { get; set; }
        
        [JsonProperty("self_stream")]
        public bool SelfStream { get; set; }
        
        [JsonProperty("self_video")]
        public bool SelfVideo { get; set; }

        public bool suppress { get; set; }
    }
}
