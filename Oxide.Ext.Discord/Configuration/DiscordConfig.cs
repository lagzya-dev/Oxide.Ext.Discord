using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Core.Configuration;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Configuration
{
    /// <summary>
    /// Represents Discord Extension Config
    /// </summary>
    internal class DiscordConfig : ConfigFile
    {
        /// <summary>
        /// Discord Config Command Options
        /// </summary>
        [JsonProperty("Commands")]
        public DiscordCommandsConfig Commands { get; set; }
        
        /// <summary>
        /// Discord Config Logging Options
        /// </summary>
        [JsonProperty("Logging")]
        public DiscordLoggingConfig Logging { get; set; }
        
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
            try
            {
                base.Load(filename);

                Commands = new DiscordCommandsConfig
                {
                    CommandPrefixes = Commands?.CommandPrefixes ?? new[] {'/', '!'}
                };
                Logging = new DiscordLoggingConfig
                {
                    HideDiscordErrorCodes = Logging?.HideDiscordErrorCodes ?? new HashSet<int>()
                };
            }
            catch (Exception ex)
            {
                DiscordExtension.GlobalLogger.Exception("Failed to load config file. Generating new Config", ex);
                Commands = new DiscordCommandsConfig
                {
                    CommandPrefixes = new[] {'/', '!'}
                };
                Logging = new DiscordLoggingConfig()
                {
                    HideDiscordErrorCodes = new HashSet<int>()
                };
                Save(filename);
            }
        }
    }
}