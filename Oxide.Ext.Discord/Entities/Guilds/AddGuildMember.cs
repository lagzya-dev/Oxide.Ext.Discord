using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    public class AddGuildMember
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        
        [JsonProperty("nick")]
        public string Nick { get; set; }
        
        [JsonProperty("roles")]
        public List<string> Roles { get; set; }
        
        [JsonProperty("mute")]
        public bool Mute { get; set; }
        
        [JsonProperty("deaf")]
        public bool Deaf { get; set; }
    }
}