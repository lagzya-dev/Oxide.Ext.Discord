using System.Collections.Generic;
using Newtonsoft.Json;
namespace Oxide.Ext.Discord.Entities.Applications
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/application#install-params-object">Install Params Structure</a>
    /// </summary>
    public class InstallParams
    {
        /// <summary>
        /// The scopes to add the application to the server with
        /// </summary>
        [JsonProperty("scopes")]
        public List<string> Scopes { get; set; } 
        
        /// <summary>
        /// The permissions to request for the bot role
        /// </summary>
        [JsonProperty("permissions")]
        public string Permissions { get; set; } 
    }
}