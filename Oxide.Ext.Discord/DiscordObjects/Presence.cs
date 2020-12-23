using System.Collections.Generic;

namespace Oxide.Ext.Discord.DiscordObjects
{
    using Newtonsoft.Json;

    public class Presence
    {
        [JsonProperty("status")]
        public PresenceStatus Status { get; set; } = PresenceStatus.Online;

        [JsonProperty("activities")]
        public List<Activity> Activities { get; set; }

        [JsonProperty("since")]
        public int? Since { get; set; }

        [JsonProperty("afk")]
        public bool AFK { get; set; }
    }
}
