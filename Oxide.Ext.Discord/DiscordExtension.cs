using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Configuration;
using Oxide.Core.Extensions;
using Oxide.Ext.Discord.Configuration;
using Oxide.Ext.Discord.Libraries.Command;
using Oxide.Ext.Discord.Libraries.Linking;
using Oxide.Ext.Discord.Libraries.Subscription;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord
{
    /// <summary>
    /// Discord Extension that is loaded by Oxide
    /// </summary>
    public class DiscordExtension : Extension
    {
        /// <summary>
        /// Test version information if using test version
        /// </summary>
        public const string TestVersion = "";

        /// <summary>
        /// Discord Extension JSON Serialization settings
        /// </summary>
        internal static readonly JsonSerializerSettings ExtensionSerializeSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };
        
        /// <summary>
        /// Version number of the extension
        /// </summary>
        private static readonly VersionNumber ExtensionVersion = new VersionNumber(2, 1, 10);
        
        /// <summary>
        /// Global logger for areas that aren't part of a client connection
        /// </summary>
        public static ILogger GlobalLogger;

        internal static DiscordLink DiscordLink;
        internal static DiscordCommand DiscordCommand;
        internal static DiscordSubscriptions DiscordSubscriptions;
        internal static DiscordConfig DiscordConfig;

        /// <summary>
        /// Constructor for the extension
        /// </summary>
        /// <param name="manager">Oxide extension manager</param>
        public DiscordExtension(ExtensionManager manager) : base(manager)
        {
            
        }

        /// <summary>
        /// Name of the extension
        /// </summary>
        public override string Name => "Discord";

        /// <summary>
        /// Authors for the extension
        /// </summary>
        public override string Author => "PsychoTea & DylanSMR & Tricky & Kirollos & MJSU";

        /// <summary>
        /// Version number used by oxide
        /// </summary>
        public override VersionNumber Version => ExtensionVersion;

        /// <summary>
        /// Gets full extension version including test information
        /// </summary>
        public static string GetExtensionVersion => ExtensionVersion.ToString() + TestVersion; 

        /// <summary>
        /// Called when mod is loaded
        /// </summary>
        public override void OnModLoad()
        {
            GlobalLogger = string.IsNullOrEmpty(TestVersion) ? new Logger(DiscordLogLevel.Warning) : new Logger(DiscordLogLevel.Debug);
            
            GlobalLogger.Info($"Using Discord Extension Version: {GetExtensionVersion}");
            AppDomain.CurrentDomain.UnhandledException += (sender, exception) =>
            {
                GlobalLogger.Exception("An exception was thrown!", exception.ExceptionObject as Exception);
            };

            string configPath = Path.Combine(Interface.Oxide.InstanceDirectory, "discord.config.json");
            if (!File.Exists(configPath))
            {
                DiscordConfig = new DiscordConfig(configPath);
                DiscordConfig.Save();
            }

            DiscordConfig = ConfigFile.Load<DiscordConfig>(configPath);
            DiscordConfig.Save();

            DiscordLink = new DiscordLink(GlobalLogger);
            DiscordCommand = new DiscordCommand(DiscordConfig.Commands.CommandPrefixes);
            DiscordSubscriptions = new DiscordSubscriptions(GlobalLogger);

            Manager.RegisterLibrary(nameof(DiscordLink), DiscordLink);
            Manager.RegisterLibrary(nameof(DiscordCommand), DiscordCommand);
            Manager.RegisterLibrary(nameof(DiscordSubscriptions), DiscordSubscriptions);
            Interface.Oxide.RootPluginManager.OnPluginAdded += DiscordClient.OnPluginAdded;
            Interface.Oxide.RootPluginManager.OnPluginRemoved +=  DiscordClient.OnPluginRemoved;
        }

        /// <summary>
        /// Called when server is shutdown
        /// </summary>
        public override void OnShutdown()
        {
            foreach (DiscordClient client in DiscordClient.Clients.Values.ToList())
            {
                DiscordClient.CloseClient(client);
            }
            
            Interface.Oxide.RootPluginManager.OnPluginAdded -= DiscordClient.OnPluginAdded;
            Interface.Oxide.RootPluginManager.OnPluginRemoved -=  DiscordClient.OnPluginRemoved;

            GlobalLogger.Info("Disconnected all clients - server shutdown.");
        }
    }
}
