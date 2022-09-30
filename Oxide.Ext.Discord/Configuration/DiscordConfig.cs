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
        /// Discord Command Options
        /// </summary>
        [JsonProperty("Commands")]
        public DiscordCommandsConfig Commands { get; set; }
        
        /// <summary>
        /// Discord Logging Options
        /// </summary>
        [JsonProperty("Logging")]
        public DiscordLoggingConfig Logging { get; set; }
        
        /// <summary>
        /// Discord Logging Options
        /// </summary>
        [JsonProperty("Users")]
        public DiscordUsersConfig Users { get; set; }
        
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
                ApplyDefaults();
            }
            catch (Exception ex)
            {
                DiscordExtension.GlobalLogger.Exception("Failed to load config file. Generating new Config", ex);
                ApplyDefaults();
                Save(filename);
            }
        }

        private void ApplyDefaults()
        {
            Commands = new DiscordCommandsConfig
            {
                CommandPrefixes = Commands?.CommandPrefixes ?? new[] {'/', '!'}
            };
            Logging = new DiscordLoggingConfig
            {
                HideDiscordErrorCodes = Logging?.HideDiscordErrorCodes ?? new HashSet<int>(),
                ConsoleLogLevel = Logging?.ConsoleLogLevel ?? DiscordLogLevel.Info,
                FileLogLevel = Logging?.FileLogLevel ?? DiscordLogLevel.Off,
                ConsoleLogFormat = Logging?.ConsoleLogFormat ?? "[DiscordExtension] [{0}]: {1}",
                FileLogFormat = Logging?.FileLogFormat ?? "{0:HH:mm:ss} [{1}]: {2}"
            };
            Users = new DiscordUsersConfig
            {
                DmBlockedDuration = Users?.DmBlockedDuration ?? 24f
            };
        }
    }
}