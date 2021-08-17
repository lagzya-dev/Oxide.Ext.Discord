using System.IO;
using Newtonsoft.Json;
using Oxide.Core.Configuration;

namespace Oxide.Ext.Discord.Configuration
{
    /// <summary>
    /// Represents Discord Extension Config
    /// </summary>
    public class DiscordConfig : ConfigFile
    {
        /// <summary>
        /// Discord Config commands options
        /// </summary>
        [JsonProperty("Commands")]
        public DiscordCommandsConfig Commands { get; set; }
        
        /// <summary>
        /// Constructor for discord config
        /// </summary>
        /// <param name="filename">Filename to use</param>
        public DiscordConfig(string filename) : base(filename)
        {
            
        }

        /// <summary>
        /// Load the config file and populate it.
        /// </summary>
        /// <param name="filename"></param>
        public override void Load(string filename = null)
        {
            DiscordConfig data = JsonConvert.DeserializeObject<DiscordConfig>(File.ReadAllText(filename ?? Filename));

            Commands = data.Commands ?? new DiscordCommandsConfig
            {
                CommandPrefixes = data.Commands?.CommandPrefixes ?? new []{'/', '!'}
            };
        }
    }
}