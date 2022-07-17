using System;
using System.Threading.Tasks;

namespace Oxide.Ext.Discord.Interfaces.WebSockets
{
    public interface IWebSocketEventHandler
    {
        Task SocketOpened();

        Task SocketClosed(int code, string message);

        Task SocketErrored(Exception ex);

        Task SocketMessage(string message);
    }
}