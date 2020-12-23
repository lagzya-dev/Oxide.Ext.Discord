using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class AllowedMentions
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("parse")]
        public List<AllowedMentions> Parse { get; set; }
        
        [JsonProperty("roles")]
        public List<string> Roles { get; set; }
        
        [JsonProperty("users")]
        public List<string> Users { get; set; }
        
        [JsonProperty("replied_user")]
        public bool RepliedUser { get; set; }
    }
}