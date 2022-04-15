namespace Oxide.Ext.Discord.Exceptions.Entities.Websocket
{
    /// <summary>
    /// Represents an exception that occured with the websocket
    /// </summary>
    public class WebsocketException : BaseDiscordException
    {
        internal WebsocketException(string message) : base(message) { }
    }
}