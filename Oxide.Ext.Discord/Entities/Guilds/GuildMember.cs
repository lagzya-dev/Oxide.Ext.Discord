using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Helpers.Converters;
using Oxide.Ext.Discord.Helpers.Interfaces;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildMember : IGetEntityId
    {
        [JsonProperty("user")]
        public DiscordUser User { get; set; }

        [JsonProperty("nick")]
        public string Nick { get; set; }

        [JsonProperty("roles")]
        public List<string> Roles { get; set; }

        [JsonProperty("joined_at")]
        public string JoinedAt { get; set; }

        [JsonProperty("deaf")]
        public bool Deaf { get; set; }

        [JsonProperty("mute")]
        public bool Mute { get; set; }

        public string GetEntityId()
        {
            return User.Id;
        }
    }
}
