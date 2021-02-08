using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Activities;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Gatway.Commands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#update-status">Update Status</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class StatusUpdate
    {
        /// <summary>
        /// The user's new status
        /// <see cref="StatusType"/>
        /// </summary>
        [JsonProperty("status")]
        public StatusType Status { get; set; } = StatusType.Online;

        /// <summary>
        /// Null, or the user's activities
        /// </summary>
        [JsonProperty("activities")]
        public List<Activity> Activities { get; set; }

        /// <summary>
        /// Unix time (in milliseconds) of when the client went idle, or null if the client is not idle
        /// </summary>
        [JsonProperty("since")]
        public int? Since { get; set; }

        /// <summary>
        /// Whether or not the client is afk
        /// </summary>
        [JsonProperty("afk")]
        public bool Afk { get; set; }
    }
}
