using System;
using System.Reflection;
using Oxide.Core;
using Oxide.Core.Extensions;
using Oxide.Ext.Discord.Configuration;
using Oxide.Ext.Discord.Data.Users;
using Oxide.Ext.Discord.Factory;
using Oxide.Ext.Discord.Libraries.AppCommands;
using Oxide.Ext.Discord.Libraries.Command;
using Oxide.Ext.Discord.Libraries.Langs;
using Oxide.Ext.Discord.Libraries.Linking;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Libraries.Subscription;
using Oxide.Ext.Discord.Libraries.Templates.Commands;
using Oxide.Ext.Discord.Libraries.Templates.Embeds;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Libraries.Templates.Modals;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Plugins;

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
        
        internal static DiscordMessageTemplates DiscordMessageTemplates;
        internal static DiscordEmbedTemplates DiscordEmbedTemplates;
        internal static DiscordEmbedFieldTemplates DiscordEmbedFieldTemplates;
        internal static DiscordModalTemplates DiscordModalTemplates;
        internal static DiscordCommandLocalizations DiscordCommandLocalizations;
        
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
            DiscordConfig.LoadConfig();
            
            GlobalLogger = DiscordLoggerFactory.Instance.CreateExtensionLogger(string.IsNullOrEmpty(TestVersion) ? DiscordLogLevel.Warning : DiscordLogLevel.Verbose);
            GlobalLogger.Info("Using Discord Extension Version: {0}", FullExtensionVersion);

            AppDomain.CurrentDomain.UnhandledException += (sender, exception) =>
            {
                GlobalLogger.Exception("An unhandled exception was thrown!", exception?.ExceptionObject as Exception);
            };
            
            Manager.RegisterLibrary(nameof(DiscordPool), new DiscordPool(GlobalLogger));
            Manager.RegisterLibrary(nameof(DiscordAppCommand),  new DiscordAppCommand(GlobalLogger));
            Manager.RegisterLibrary(nameof(DiscordLink), new DiscordLink(GlobalLogger));
            Manager.RegisterLibrary(nameof(DiscordCommand), new DiscordCommand(DiscordConfig.Instance.Commands.CommandPrefixes, GlobalLogger));
            Manager.RegisterLibrary(nameof(DiscordSubscriptions), new DiscordSubscriptions(GlobalLogger));
            Manager.RegisterLibrary(nameof(DiscordLang), new DiscordLang(GlobalLogger));
            Manager.RegisterLibrary(nameof(DiscordPlaceholders), new DiscordPlaceholders(GlobalLogger));

            DiscordUserData.Load();
            
            DiscordMessageTemplates = new DiscordMessageTemplates(GlobalLogger);
            DiscordEmbedTemplates = new DiscordEmbedTemplates(GlobalLogger);
            DiscordEmbedFieldTemplates = new DiscordEmbedFieldTemplates(GlobalLogger);
            DiscordModalTemplates = new DiscordModalTemplates(GlobalLogger);
            DiscordCommandLocalizations = new DiscordCommandLocalizations(GlobalLogger);

            Manager.RegisterLibrary(nameof(DiscordMessageTemplates), DiscordMessageTemplates);
            Manager.RegisterLibrary(nameof(DiscordEmbedTemplates), DiscordEmbedTemplates);
            Manager.RegisterLibrary(nameof(DiscordEmbedFieldTemplates), DiscordEmbedFieldTemplates);
            Manager.RegisterLibrary(nameof(DiscordModalTemplates), DiscordModalTemplates);
            Manager.RegisterLibrary(nameof(DiscordCommandLocalizations), DiscordCommandLocalizations);
            
            Interface.Oxide.RootPluginManager.OnPluginAdded += DiscordClientFactory.Instance.OnPluginAdded;
            Interface.Oxide.RootPluginManager.OnPluginRemoved += DiscordClientFactory.Instance.OnPluginRemoved;
            
            Manager.RegisterPluginLoader(new DiscordExtPluginLoader());
        }

        /// <summary>
        /// Called when server is shutdown
        /// </summary>
        public override void OnShutdown()
        {
            DiscordClientFactory.Instance.OnShutdown();
            
            Interface.Oxide.RootPluginManager.OnPluginAdded -= DiscordClientFactory.Instance.OnPluginAdded;
            Interface.Oxide.RootPluginManager.OnPluginRemoved -=  DiscordClientFactory.Instance.OnPluginRemoved;

            GlobalLogger.Debug("Disconnected all clients - server shutdown.");
            
            DiscordUserData.Instance.Save(true);
            DiscordLoggerFactory.Instance.OnServerShutdown();
        }
    }
}
