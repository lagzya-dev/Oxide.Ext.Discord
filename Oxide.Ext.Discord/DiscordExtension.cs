using System.IO;
using System.Linq;
using System.Reflection;
using Oxide.Core;
using Oxide.Core.Configuration;
using Oxide.Core.Extensions;
using Oxide.Ext.Discord.Configuration;
using Oxide.Ext.Discord.Data.Users;
using Oxide.Ext.Discord.Libraries.AppCommands;
using Oxide.Ext.Discord.Libraries.Command;
using Oxide.Ext.Discord.Libraries.Langs;
using Oxide.Ext.Discord.Libraries.Linking;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Subscription;
using Oxide.Ext.Discord.Libraries.Templates.Commands;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
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

        internal static DiscordAppCommand DiscordAppCommand;
        internal static DiscordLink DiscordLink;
        internal static DiscordCommand DiscordCommand;
        internal static DiscordSubscriptions DiscordSubscriptions;
        internal static DiscordLang DiscordLang;
        internal static DiscordMessageTemplates DiscordMessageTemplates;
        internal static DiscordEmbedTemplates DiscordEmbedTemplates;
        internal static DiscordEmbedFieldTemplates DiscordEmbedFieldTemplates;
        internal static DiscordModalTemplates DiscordModalTemplates;
        internal static DiscordCommandLocalizations DiscordCommandLocalizations;
        internal static DiscordPlaceholders DiscordPlaceholders;
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
            
            GlobalLogger = DiscordLoggerFactory.GetExtensionLogger(string.IsNullOrEmpty(TestVersion) ? DiscordLogLevel.Warning : DiscordLogLevel.Verbose);
            GlobalLogger.Info("Using Discord Extension Version: {0}", FullExtensionVersion);

            AppDomain.CurrentDomain.UnhandledException += (sender, exception) =>
            {
                GlobalLogger.Exception("An unhandled exception was thrown!", exception?.ExceptionObject as System.Exception);
            };
            
            DiscordUserData.Load();

            DiscordAppCommand = new DiscordAppCommand(GlobalLogger);
            DiscordLink = new DiscordLink(GlobalLogger);
            DiscordCommand = new DiscordCommand(DiscordConfig.Commands.CommandPrefixes, GlobalLogger);
            DiscordSubscriptions = new DiscordSubscriptions(GlobalLogger);
            DiscordLang = new DiscordLang(GlobalLogger);
            DiscordMessageTemplates = new DiscordMessageTemplates(GlobalLogger);
            DiscordEmbedTemplates = new DiscordEmbedTemplates(GlobalLogger);
            DiscordEmbedFieldTemplates = new DiscordEmbedFieldTemplates(GlobalLogger);
            DiscordModalTemplates = new DiscordModalTemplates(GlobalLogger);
            DiscordCommandLocalizations = new DiscordCommandLocalizations(GlobalLogger);
            DiscordPlaceholders = new DiscordPlaceholders(GlobalLogger);
            
            Manager.RegisterLibrary(nameof(DiscordAppCommand), DiscordAppCommand);
            Manager.RegisterLibrary(nameof(DiscordLink), DiscordLink);
            Manager.RegisterLibrary(nameof(DiscordCommand), DiscordCommand);
            Manager.RegisterLibrary(nameof(DiscordSubscriptions), DiscordSubscriptions);
            Manager.RegisterLibrary(nameof(DiscordMessageTemplates), DiscordMessageTemplates);
            Manager.RegisterLibrary(nameof(DiscordEmbedTemplates), DiscordEmbedTemplates);
            Manager.RegisterLibrary(nameof(DiscordEmbedFieldTemplates), DiscordEmbedFieldTemplates);
            Manager.RegisterLibrary(nameof(DiscordModalTemplates), DiscordModalTemplates);
            Manager.RegisterLibrary(nameof(DiscordCommandLocalizations), DiscordCommandLocalizations);
            Manager.RegisterLibrary(nameof(DiscordPlaceholders), DiscordPlaceholders);
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
            
            DiscordUserData.Instance.Save(true);
            DiscordLoggerFactory.OnServerShutdown();
        }
    }
}
