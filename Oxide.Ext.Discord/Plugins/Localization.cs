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
        public const string WipePool = nameof(WipePool);
        public const string ShowLog = nameof(ShowLog);
        public const string SetLog = nameof(SetLog);
        public const string InvalidLogEnum = nameof(InvalidLogEnum);
        public const string ShowValidation = nameof(ShowValidation);
        public const string SetValidation = nameof(SetValidation);
        public const string InvalidValidation = nameof(InvalidValidation);
        public const string Enabled = nameof(Enabled);
        public const string Disabled = nameof(Disabled);
        public const string Help = nameof(Help);
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
                [LangKeys.WipePool] = "All Discord Pools have been wiped",
                [LangKeys.ShowLog] = "{0} log is currently set to {1}",
                [LangKeys.SetLog] = "{0} log has been set to {1}",
                [LangKeys.InvalidLogEnum] = "'{0}' is not a valid DiscordLogLevel enum. Valid values are Off, Error, Warning, Info, Debug, Verbose",
                [LangKeys.ShowValidation] = "Discord Validation is currently set to {0}",
                [LangKeys.SetValidation] = "Discord Validation has been set to {0}",
                [LangKeys.InvalidValidation] = "'{0}' is not a valid boolean value. Valid values are false, true, 0, or 1",
                [LangKeys.Enabled] = "Enabled",
                [LangKeys.Disabled] = "Disabled",
                [LangKeys.Help] = "Discord Extension v{0} Commands:\n" +
                                  " * de.version - displays the current Discord Extension version\n" +
                                  " * de.reset.reset - resets all rest handlers\n" +
                                  " * de.websocket.reset - resets all websockets\n" +
                                  " * de.websocket.reconnect - reconnects all websockets\n" +
                                  " * de.log.console - sets the console log level. Options (Verbose, Debug, Info, Warning, Error, Exception, Off)\n" +
                                  " * de.log.file - sets the file log level. Options (Verbose, Debug, Info, Warning, Error, Exception, Off)\n" +
                                  " * de.validation.enabled - sets if request validation is enabled\n" +
                                  " * de.debug - prints debug information about the state of the Discord Extension"
            }
        };
    }
}