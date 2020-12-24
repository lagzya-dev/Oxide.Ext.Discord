using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Activities;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Gatway.Commands
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]

    public class StatusUpdate
    {
        [JsonProperty("status")]
        public PresenceStatus Status { get; set; } = PresenceStatus.Online;

        [JsonProperty("activities")]
        public List<Activity> Activities { get; set; }

        [JsonProperty("since")]
        public int? Since { get; set; }

        [JsonProperty("afk")]
        public bool Afk { get; set; }
    }
}
