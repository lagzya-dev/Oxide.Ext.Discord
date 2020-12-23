using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects.SlashCommands
{
    public class ApplicationCommand : ApplicationCommandCreate
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("application_id")]
        public string ApplicationId { get; set; }
    }
}