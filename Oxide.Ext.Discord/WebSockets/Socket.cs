using Newtonsoft.Json;
using Oxide.Ext.Discord.Gateway;

namespace Oxide.Ext.Discord.WebSockets
{
    using System;
    using Oxide.Core;
    using Oxide.Ext.Discord.Exceptions;
    using WebSocketSharp;

    public class Socket
    {
        private readonly DiscordClient _client;

        private WebSocket _socket;

        private SocketListener _listener;

        public bool ShouldAttemptResume;

        public Socket(DiscordClient client)
        {
            _client = client;
        }

        public void Connect(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new NoURLException();
            }

            if (_socket != null)
            {
                // Assume force-reconnect
                Disconnect(false);
            }
            
            _client.DestroyHeartbeat();

            _socket = new WebSocket($"{url}/?v=6&encoding=json");

            _listener ??= new SocketListener(_client, this);
            _listener.Retries = 0;

            _socket.OnOpen += _listener.SocketOpened;
            _socket.OnClose += _listener.SocketClosed;
            _socket.OnError += _listener.SocketErrored;
            _socket.OnMessage += _listener.SocketMessage;
            _socket.ConnectAsync();
        }

        public void Disconnect(bool normal = true)
        {
            if (IsClosingOrClosed())
            {
                return;
            }

            _socket.CloseAsync(normal ? CloseStatusCode.Normal : CloseStatusCode.Abnormal);
        }

        public void ReconnectRequested()
        {
            if (IsClosingOrClosed())
            {
                return;
            }

            _socket?.CloseAsync(4199, "Discord server requested reconnect");
        }

        public void Dispose()
        {
            _listener = null;
            _socket = null;
        }

        public void Send(OpCodes opCode, object data, Action<bool> completed = null)
        {
            if (!IsAlive())
            {
                return;
            }
            
            SPayload opcode = new SPayload()
            {
                OP = opCode,
                Payload = data
            };
            
            _socket.SendAsync(JsonConvert.SerializeObject(opcode), completed);
        }

        public void Send(string message, Action<bool> completed = null)
        {
            if (IsAlive())
            {
                _socket.SendAsync(message, completed);
            }
        }

        public bool IsAlive()
        {
            if (_socket == null)
            {
                return false;
            }
            
            return _socket.ReadyState == WebSocketState.Open;
        }

        public bool IsClosingOrClosed()
        {
            if (_socket == null)
            {
                return false;
            }

            return _socket.ReadyState == WebSocketState.Closing || _socket.ReadyState == WebSocketState.Closed;
        }
    }
}
