using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class GuildBan
    {
        //Between 0 - 7
        [JsonProperty("delete_message_days")]
        public int? DeleteMessageDays { get; set; }
        
        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}