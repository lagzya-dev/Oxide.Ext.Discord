namespace Oxide.Ext.Discord.Logging
{
    public interface IDiscordLoggingConfig
    {
        DiscordLogLevel ConsoleLogLevel { get; }
        DiscordLogLevel FileLogLevel { get; }
        string ConsoleLogFormat { get; }
        string FileLogFormat { get; }
    }
}