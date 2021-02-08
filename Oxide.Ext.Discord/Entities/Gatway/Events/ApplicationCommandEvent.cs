using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#application-command-extra-fields">Application Command Gateway Event</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ApplicationCommandEvent : ApplicationCommand
    {
        /// <summary>
        /// ID of the guild the command is in
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
    }
}