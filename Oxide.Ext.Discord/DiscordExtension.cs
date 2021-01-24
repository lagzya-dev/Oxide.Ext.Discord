using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Core;
using Oxide.Core.Extensions;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord
{
    public class DiscordExtension : Extension
    {
        private static readonly VersionNumber ExtensionVersion = new VersionNumber(2, 0, 0);
        public const string TestVersion = ".PreAlpha.1";
        
        public static ILogger GlobalLogger;
        
        public DiscordExtension(ExtensionManager manager) : base(manager)
        {
            
        }

        ////public override bool SupportsReloading => true;

        public override string Name => "Discord";

        public override string Author => "PsychoTea & DylanSMR & Tricky";

        public override VersionNumber Version => ExtensionVersion;

        public static string GetExtensionVersion => ExtensionVersion + TestVersion; 

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
