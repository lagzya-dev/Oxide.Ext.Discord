namespace Oxide.Ext.Discord.WebSockets
{
    /// <summary>
    /// Represents our current state for the websocket
    /// </summary>
    public enum SocketState : byte
    {
        /// <summary>
        /// Socket is in the process of connecting
        /// </summary>
        Connecting,
        
        /// <summary>
        /// Socket is connect and functioning normally
        /// </summary>
        Connected,

        /// <summary>
        /// Websocket is currently disconnecting from a connected web socket
        /// </summary>
        Disconnecting,
        
        /// <summary>
        /// Websocket is currently disconnect and not waiting to connect
        /// </summary>
        Disconnected
    }
}