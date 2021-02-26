using System;
using System.Timers;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Logging;
using WebSocketSharp;

namespace Oxide.Ext.Discord.WebSockets
{
    /// <summary>
    /// Represents a websocket that connects to discord
    /// </summary>
    public class Socket
    {
        /// <summary>
        /// If we should attempt to reconnect to discord on disconnect
        /// </summary>
        public bool RequestedReconnect;
        
        /// <summary>
        /// If we should attempt to resume our previous session after connecting
        /// </summary>
        public bool ShouldAttemptResume;
        
        /// <summary>
        /// Timer to use when attempting to reconnect to discord due to an error
        /// </summary>
        internal Timer ReconnectTimer;

        internal SocketState SocketState = SocketState.Disconnected;
        internal int ReconnectRetries;

        private readonly BotClient _client;
        private WebSocket _socket;
        private SocketListener _listener;
        private readonly ILogger _logger;

        /// <summary>
        /// Socket used by the BotClient
        /// </summary>
        /// <param name="client">Client using the socket</param>
        /// <param name="logger">Logger for the bot client</param>
        public Socket(BotClient client, ILogger logger)
        {
            _client = client;
            _logger = logger;
            _listener = new SocketListener(_client, this, _logger);
        }

        /// <summary>
        /// Connect tot the websocket
        /// </summary>
        /// <exception cref="Exception">Thrown if the socket still exists. Must call disconnect before trying to connect</exception>
        public void Connect()
        {
            string url = Gateway.WebsocketUrl;
            
            //We haven't gotten the websocket url. Get url then attempt to connect.
            if (string.IsNullOrEmpty(url))
            {
                Gateway.UpdateGatewayUrl(_client, Connect);
                return;
            }

            if (IsConnected() || IsConnecting())
            {
                throw new Exception("Socket is already running. Please disconnect before attempting to connect.");
            }

            SocketState = SocketState.Connecting;

            RequestedReconnect = false;
            ShouldAttemptResume = false;

            _socket = new WebSocket(url);

            _socket.OnOpen += _listener.SocketOpened;
            _socket.OnClose += _listener.SocketClosed;
            _socket.OnError += _listener.SocketErrored;
            _socket.OnMessage += _listener.SocketMessage;
            _socket.ConnectAsync();
        }
        
        /// <summary>
        /// Disconnects the websocket from discord
        /// </summary>
        /// <param name="attemptReconnect">Should we attempt to reconnect to discord after disconnecting</param>
        /// <param name="shouldResume">Should we attempt to resume our previous session</param>
        /// <param name="requested">If discord requested that we reconnect to discord</param>
        public void Disconnect(bool attemptReconnect, bool shouldResume, bool requested = false)
        {
            RequestedReconnect = attemptReconnect;
            ShouldAttemptResume = shouldResume;
            
            if (ReconnectTimer != null)
            {
                ReconnectTimer.Stop();
                ReconnectTimer.Dispose();
                ReconnectTimer = null;
            }
            
            if (IsDisconnected())
            {
                return;
            }

            if (requested)
            {
                _socket.CloseAsync(4199, "Discord requested reconnect websocket reconnect");
            }
            else
            {
                _socket.CloseAsync(CloseStatusCode.Normal);
            }

            SocketClosed();
            SocketState = SocketState.Disconnected;
            if (RequestedReconnect)
            {
                Reconnect();
            }
        }

        /// <summary>
        /// Returns if the given websocket matches our current websocket.
        /// If socket is null we return false
        /// </summary>
        /// <param name="socket">Socket to compare</param>
        /// <returns>True if current socket is not null and socket matches current socket; False otherwise.</returns>
        internal bool IsCurrentSocket(WebSocket socket)
        {
            return _socket != null && _socket == socket;
        }

        /// <summary>
        /// Shutdowns the websocket completely. Used when bot is being shutdown
        /// </summary>
        public void Shutdown()
        {
            if (ReconnectTimer != null)
            {
                ReconnectTimer.Stop();
                ReconnectTimer.Dispose();
                ReconnectTimer = null;
            }
            
            Disconnect(false, false);
            _listener = null;
            _socket = null;
        }

        /// <summary>
        /// Called when a websocket is closed to remove previous socket
        /// </summary>
        public void SocketClosed()
        {
            if (_socket != null)
            {
                _socket.OnOpen -= _listener.SocketOpened;
                _socket.OnError -= _listener.SocketErrored;
                _socket.OnMessage -= _listener.SocketMessage;
                _socket = null;
            }
        }

        /// <summary>
        /// Send a command to discord over the websocket
        /// </summary>
        /// <param name="opCode">Command code to send</param>
        /// <param name="data">Data to send</param>
        /// <param name="completed">Action once the action is completed and if it was successful</param>
        public void Send(GatewayCommandCode opCode, object data, Action<bool> completed = null)
        {
            if (!IsConnected())
            {
                return;
            }
            
            CommandPayload opcode = new CommandPayload
            {
                OpCode = opCode,
                Payload = data
            };

            string payloadData = JsonConvert.SerializeObject(opcode, DiscordExtension.ExtensionSerializeSettings);
            _logger.Debug($"{nameof(Socket)}.{nameof(Send)} Payload: {payloadData}");
            
            _socket.SendAsync(payloadData, completed);
        }

        /// <summary>
        /// Returns if the websocket is in the open state
        /// </summary>
        /// <returns>Returns if the websocket is in open state</returns>
        public bool IsConnected()
        {
            return SocketState == SocketState.Connected;
        }
        
        /// <summary>
        /// Returns if the websocket is in the connecting state
        /// </summary>
        /// <returns>Returns if the websocket is in connecting state</returns>
        public bool IsConnecting()
        {
            return SocketState == SocketState.Connecting;
        }
        
        /// <summary>
        /// Returns if the websocket is in the connecting state
        /// </summary>
        /// <returns>Returns if the websocket is in connecting state</returns>
        public bool IsPendingReconnect()
        {
            return SocketState == SocketState.PendingReconnect;
        }

        /// <summary>
        /// Returns if the websocket is null or is currently closing / closed
        /// </summary>
        /// <returns>Returns if the websocket is null or is currently closing / closed</returns>
        public bool IsDisconnected()
        {
            return SocketState == SocketState.Disconnected;
        }

        /// <summary>
        /// Reconnects the socket to the gateway.
        /// </summary>
        public void Reconnect()
        {
            if (!_client.Initialized)
            {
                return;
            }
            
            if (SocketState != SocketState.Disconnected)
            {
                return;
            }

            SocketState = SocketState.PendingReconnect;

            //If we haven't hd any errors reconnect to the gateway
            if (ReconnectRetries == 0)
            {
                Interface.Oxide.NextTick(Connect);
            }

            //We had an error trying to reconnect. Perform Delayed Reconnects
            float delay = ReconnectRetries <= 3 ? 1f : 15f;
            
            //There has been more than 3 tries to reconnect. Discord suggests trying to update gateway url.
            bool updateGateway = ReconnectRetries > 3;
            
            ReconnectTimer = new Timer
            {
                Interval = delay * 1000,
                AutoReset = false
            };
            ReconnectTimer.Elapsed += (_, __) =>
            {
                if (updateGateway)
                {
                    Gateway.UpdateGatewayUrl(_client, Connect);
                }
                else
                {
                    Connect();
                }
                ReconnectTimer = null;
            };
            
            _logger.Warning($"Attempting to reconnect to Discord... [Retry={ReconnectRetries}]");
            ReconnectTimer.Start();
            ReconnectRetries++;
        }
    }
}
