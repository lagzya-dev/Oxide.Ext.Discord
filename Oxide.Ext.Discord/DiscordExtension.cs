namespace Oxide.Ext.Discord
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Oxide.Core;
    using Oxide.Core.Extensions;

    public class DiscordExtension : Extension
    {
        private static readonly VersionNumber ExtensionVersion = new VersionNumber(1, 0, 7);
        private const string TestVersion = "Beta.2";
        
        public DiscordExtension(ExtensionManager manager) : base(manager)
        {
            
        }

        ////public override bool SupportsReloading => true;

        public override string Name => "Discord";

        public override string Author => "PsychoTea & DylanSMR & Tricky";

        public override VersionNumber Version => ExtensionVersion;

        public static string GetExtensionVersion => ExtensionVersion + "." + TestVersion; 

        public override void OnModLoad()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, exception) =>
            {
                Interface.Oxide.LogException("An exception was thrown!", exception.ExceptionObject as Exception);
            };
        }

        public override void OnShutdown()
        {
            // new List prevents against InvalidOperationException
            foreach (var client in new List<DiscordClient>(Discord.Clients))
            {
                Discord.CloseClient(client);
            }

            Interface.Oxide.LogInfo("[Discord Extension] Disconnected all clients - server shutdown.");
        }
    }
}
