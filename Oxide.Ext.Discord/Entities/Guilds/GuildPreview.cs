using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildPreview : GuildCreate
    {
        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }
        
        [JsonProperty("splash")]
        public string Splash { get; set; }
        
        [JsonProperty("banner")]
        public string Banner { get; set; }
        
        [JsonProperty("rules_channel_id")]
        public string RulesChannelId { get; set; }
        
        [JsonProperty("public_updates_channel_id")]
        public string PublicUpdatesChannelId { get; set; }
        
        [JsonProperty("preferred_locale")]
        public string PreferredLocale { get; set; }
    }
}