using System;
using System.Threading.Tasks;

namespace Oxide.Ext.Discord.Interfaces.WebSockets
{
    public interface IWebSocketEventHandler
    {
        void SocketOpened();

        void SocketClosed(int code, string message);

        void SocketErrored(Exception ex);

        Task SocketMessage(string message);
    }
}