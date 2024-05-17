#if CARBON
using System;
using System.Collections.Generic;
using System.Reflection;
using API.Assembly;
using Carbon;
using Oxide.Core;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Configuration;
using Oxide.Ext.Discord.Data;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Factory;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Libraries;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Plugins;

namespace Oxide.Ext.Discord
{
    /// <summary>
    /// Discord Extension that is loaded by Carbon
    /// </summary>
    public class DiscordExtension : ICarbonExtension
    {
        /// <summary>
        /// Test version information if using a test version
        /// </summary>
        internal const string TestVersion = "";
        
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
        internal static DiscordButtonTemplates DiscordButtonTemplates;
        internal static DiscordInputTextTemplates DiscordInputTextTemplates;
        internal static DiscordSelectMenuTemplates DiscordSelectMenuTemplates;
        internal static DiscordCommandLocalizations DiscordCommandLocalizations;
        
        internal static bool IsShuttingDown;

        /// <summary>
        /// Constructor for the extension
        /// </summary>
        public DiscordExtension()
        {
            AssemblyName assembly = Assembly.GetExecutingAssembly().GetName();
            ExtensionVersion = new VersionNumber(assembly.Version.Major, assembly.Version.Minor, assembly.Version.Build);
            FullExtensionVersion = $"{ExtensionVersion}{TestVersion}";
        }

        /// <summary>
        /// Called when mod is loaded
        /// </summary>
        public void OnLoaded(EventArgs args)
        {
            DiscordConfig.LoadConfig();
            
            GlobalLogger = DiscordLoggerFactory.Instance.CreateExtensionLogger(string.IsNullOrEmpty(TestVersion) ? DiscordLogLevel.Warning : DiscordLogLevel.Verbose);
            GlobalLogger.Info("Using Discord Extension Version: {0}", FullExtensionVersion);
            
            ThreadEx.Initialize();

            AppDomain.CurrentDomain.UnhandledException += (sender, exception) =>
            {
                GlobalLogger.Exception("An unhandled exception was thrown!", exception?.ExceptionObject as Exception);
            };

            new DiscordPool(GlobalLogger);
            new DiscordAppCommand(GlobalLogger);
            new DiscordLink(GlobalLogger);
            new DiscordCommand(DiscordConfig.Instance.Commands.CommandPrefixes, GlobalLogger);
            new DiscordSubscriptions(GlobalLogger);
            new DiscordLocales(GlobalLogger);
            new DiscordPlaceholders(GlobalLogger);
            
            DiscordMessageTemplates = new DiscordMessageTemplates(GlobalLogger);
            DiscordEmbedTemplates = new DiscordEmbedTemplates(GlobalLogger);
            DiscordEmbedFieldTemplates = new DiscordEmbedFieldTemplates(GlobalLogger);
            DiscordModalTemplates = new DiscordModalTemplates(GlobalLogger);
            DiscordCommandLocalizations = new DiscordCommandLocalizations(GlobalLogger);
            DiscordButtonTemplates = new DiscordButtonTemplates(GlobalLogger);
            DiscordInputTextTemplates = new DiscordInputTextTemplates(GlobalLogger);
            DiscordSelectMenuTemplates = new DiscordSelectMenuTemplates(GlobalLogger);

            EmojiCache.Instance.Build();

            Community.Runtime.Plugins.AddPlugin(new DiscordExtensionCore());
            
            //Interface.Oxide.OnFrame(PromiseTimer.Instance.Update);
            
            Interface.Oxide.Config.Compiler.PreprocessorDirectives.AddRange(GetPreProcessorDirectives());
            
            Interface.Oxide.RootPluginManager.OnPluginAdded += DiscordClientFactory.Instance.OnPluginLoaded;
            Interface.Oxide.RootPluginManager.OnPluginRemoved += DiscordClientFactory.Instance.OnPluginUnloaded;
        }

        /// <summary>
        /// Called when server is shutdown
        /// </summary>
        public void OnUnloaded(EventArgs args)
        {
            DiscordClientFactory.Instance.OnShutdown();
            GlobalLogger.Debug("Disconnected all clients - server shutdown.");
            DataHandler.Instance.Shutdown();
            DiscordLoggerFactory.Instance.OnServerShutdown();
        }

        private IEnumerable<string> GetPreProcessorDirectives()
        {
            yield return "DiscordExt";
            yield return "DiscordExt3_0";
        }

        public void Awake(EventArgs args)
        {
            
        }
    }
}
#endif