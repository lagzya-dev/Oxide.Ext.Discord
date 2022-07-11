using System.IO;
using System.Linq;
using System.Reflection;
using Oxide.Core;
using Oxide.Core.Configuration;
using Oxide.Core.Extensions;
using Oxide.Ext.Discord.Configuration;
using Oxide.Ext.Discord.Libraries.Command;
using Oxide.Ext.Discord.Libraries.Linking;
using Oxide.Ext.Discord.Libraries.Subscription;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Plugins;
using AppDomain = System.AppDomain;

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
        internal const string TestVersion = ".Beta.1";
        
        internal const string Authors = "PsychoTea & DylanSMR & Tricky & Kirollos & MJSU";

        /// <summary>
        /// Version number of the extension
        /// </summary>
        internal static VersionNumber ExtensionVersion;
        
        /// <summary>
        /// Gets full extension version including test version
        /// </summary>
        internal static string FullExtensionVersion { get; private set; }
        
        /// <summary>
        /// Global logger for areas that aren't part of a client connection
        /// </summary>
        internal static ILogger GlobalLogger;

        internal static DiscordLink DiscordLink;
        internal static DiscordCommand DiscordCommand;
        internal static DiscordSubscriptions DiscordSubscriptions;
        internal static DiscordConfig DiscordConfig;

        internal static bool IsShuttingDown;

        /// <summary>
        /// Constructor for the extension
        /// </summary>
        /// <param name="manager">Oxide extension manager</param>
        public DiscordExtension(ExtensionManager manager) : base(manager)
        {
            AssemblyName assembly = Assembly.GetExecutingAssembly().GetName();
            ExtensionVersion = new VersionNumber(assembly.Version.Major, assembly.Version.Minor, assembly.Version.Build);
            FullExtensionVersion = $"{ExtensionVersion}{TestVersion}";
        }

        /// <summary>
        /// Name of the extension
        /// </summary>
        public override string Name => "Discord";

        /// <summary>
        /// Authors for the extension
        /// </summary>
        public override string Author => Authors;

        /// <summary>
        /// Version number used by oxide
        /// </summary>
        public override VersionNumber Version => ExtensionVersion;

        /// <summary>
        /// Called when mod is loaded
        /// </summary>
        public override void OnModLoad()
        {
            string configPath = Path.Combine(Interface.Oxide.InstanceDirectory, "discord.config.json");
            if (!File.Exists(configPath))
            {
                DiscordConfig = new DiscordConfig(configPath);
                DiscordConfig.Save();
            }

            DiscordConfig = ConfigFile.Load<DiscordConfig>(configPath);
            DiscordConfig.Save();
            
            GlobalLogger = string.IsNullOrEmpty(TestVersion) ? new DiscordLogger(DiscordLogLevel.Warning) : new DiscordLogger(DiscordLogLevel.Verbose);
            GlobalLogger.Info("Using Discord Extension Version: {0}", FullExtensionVersion);
            
            AppDomain.CurrentDomain.UnhandledException += (sender, exception) =>
            {
                GlobalLogger.Exception("An unhandled exception was thrown!", exception?.ExceptionObject as System.Exception);
            };

            DiscordLink = new DiscordLink(GlobalLogger);
            DiscordCommand = new DiscordCommand(DiscordConfig.Commands.CommandPrefixes);
            DiscordSubscriptions = new DiscordSubscriptions(GlobalLogger);

            Manager.RegisterLibrary(nameof(DiscordLink), DiscordLink);
            Manager.RegisterLibrary(nameof(DiscordCommand), DiscordCommand);
            Manager.RegisterLibrary(nameof(DiscordSubscriptions), DiscordSubscriptions);
            Interface.Oxide.RootPluginManager.OnPluginAdded += DiscordClient.OnPluginAdded;
            Interface.Oxide.RootPluginManager.OnPluginRemoved += DiscordClient.OnPluginRemoved;
            
            Manager.RegisterPluginLoader(new DiscordExtPluginLoader());
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

            GlobalLogger.Debug("Disconnected all clients - server shutdown.");
            
            DiscordLogger.FileLogger.OnServerShutdown();
        }
    }
}
