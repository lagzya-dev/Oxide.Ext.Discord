using System.Collections.Generic;

namespace Oxide.Ext.Discord.Plugins
{
    internal static class LangKeys
    {
        public const string Chat = nameof(Chat);
        public const string Version = nameof(Version);
        public const string ReconnectWebSocket = nameof(ReconnectWebSocket);
        public const string ResetWebSocket = nameof(ResetWebSocket);
        public const string ResetRestApi = nameof(ResetRestApi);
        public const string ClearPool = nameof(ClearPool);
        public const string ShowLog = nameof(ShowLog);
        public const string SetLog = nameof(SetLog);
        public const string InvalidLogEnum = nameof(InvalidLogEnum);

        public static class AppCommands
        {
            private const string Base = nameof(AppCommands) + ".";

            public const string DeCommand = Base + nameof(DeCommand);
            public const string DeCommandDescription = Base + nameof(DeCommandDescription);
            
            public const string AppCommandGroup = Base + nameof(AppCommandGroup);
            public const string AppCommandGroupDescription = Base + nameof(AppCommandGroupDescription);
            
            public const string DeleteAppCommand = Base + nameof(DeleteAppCommand);
            public const string DeleteAppCommandDescription = Base + nameof(DeleteAppCommandDescription);            
            public const string DeleteAppCommandArgument = Base + nameof(DeleteAppCommandArgument);
            public const string DeleteAppCommandArgumentDescription = Base + nameof(DeleteAppCommandArgumentDescription);
        }
    }
    
    internal static class Localization
    {
        internal static readonly Dictionary<string, Dictionary<string, string>> Languages = new Dictionary<string, Dictionary<string, string>>
        {
            ["en"] = new Dictionary<string, string>
            {
                [LangKeys.Chat] = "[Discord Extension] {0}",
                [LangKeys.Version] = "Server is running Discord Extension v{0}",
                [LangKeys.ReconnectWebSocket] = "Requested reconnect for all web sockets",
                [LangKeys.ResetWebSocket] = "All websockets have been reset",
                [LangKeys.ResetRestApi] = "All REST API's have been reset",
                [LangKeys.ClearPool] = "All Discord Pools have been cleared",
                [LangKeys.ShowLog] = "{0} log is currently set to {1}",
                [LangKeys.SetLog] = "{0} log has been set to {1}",
                [LangKeys.InvalidLogEnum] = "'{0}' is not a valid DiscordLogLevel enum. Valid values are Off, Error, Warning, Info, Debug, Verbose"
            }
        };
    }
}