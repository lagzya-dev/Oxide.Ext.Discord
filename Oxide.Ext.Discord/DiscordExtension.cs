using System;
using System.Linq;
using Oxide.Core;
using Oxide.Core.Extensions;
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
        public const string TestVersion = ".PreAlpha.1";
        
        /// <summary>
        /// Version number of the extension
        /// </summary>
        private static readonly VersionNumber ExtensionVersion = new VersionNumber(2, 0, 0);
        
        /// <summary>
        /// Global logger for areas that aren't part of a client connection
        /// </summary>
        public static ILogger GlobalLogger;
        
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
        public static string GetExtensionVersion => ExtensionVersion + TestVersion; 

        /// <summary>
        /// Called when mod is loaded
        /// </summary>
        public override void OnModLoad()
        {
            if (string.IsNullOrEmpty(TestVersion))
            {
                GlobalLogger = new Logger(LogLevel.Warning);
            }
            else
            {
                GlobalLogger = new Logger(LogLevel.Debug);
            }
            
            GlobalLogger.Warning($"Using Discord Extension Version: {GetExtensionVersion}");
            AppDomain.CurrentDomain.UnhandledException += (sender, exception) =>
            {
                GlobalLogger.Exception("An exception was thrown!", exception.ExceptionObject as Exception);
            };
            
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
