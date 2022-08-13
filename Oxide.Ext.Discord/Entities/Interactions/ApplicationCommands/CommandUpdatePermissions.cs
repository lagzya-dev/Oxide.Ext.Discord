using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    public class CommandUpdatePermissions
    {
        [JsonProperty("permissions")]
        public List<CommandPermissions> Permissions { get; set; }
    }
}