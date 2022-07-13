using System;
using System.Security.Authentication;
using System.Timers;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Exceptions.Entities.Websocket;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.WebSockets.Handlers;
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
        
        internal SocketState SocketState = SocketState.Disconnected;
        
        /// <summary>
        /// Timer to use when attempting to reconnect to discord due to an error
        /// </summary>
        private Timer _reconnectTimer;
        private int _reconnectRetries;

        private readonly BotClient _client;
        private WebSocket _socket;
        private SocketListener _listener;
        internal readonly SocketCommandHandler Commands;
        private readonly ILogger _logger;

        private readonly object _lock = new object();

        /// <summary>
        /// Socket used by the BotClient
        /// </summary>
        /// <param name="client">Client using the socket</param>
        /// <param name="logger">Logger for the bot client</param>
        public Socket(BotClient client, ILogger logger)
        {
            _client = client;
            _logger = logger;
            Commands = new SocketCommandHandler(client, this, logger);
            _listener = new SocketListener(_client, this, _logger, Commands);
        }

        /// <summary>
        /// Connect to the websocket
        /// </summary>
        /// <exception cref="Exception">Thrown if the socket still exists. Must call disconnect before trying to connect</exception>
        public void Connect()
        {
            string url = Gateway.WebsocketUrl;
            
            //We haven't gotten the websocket url. Get url then attempt to connect.
            //There has been more than 3 tries to reconnect. Discord suggests trying to update gateway url.
            if (string.IsNullOrEmpty(url) || (_reconnectRetries >= 3 && Gateway.LastUpdate + TimeSpan.FromMinutes(5) <= DateTime.UtcNow))
            {
                Gateway.UpdateGatewayUrl(_client, Connect, OnGatewayUrlUpdateFailed);
                return;
            }

            lock (_lock)
            {
                if (IsConnected() || IsConnecting())
                {
                    throw new WebsocketException("Socket is already running. Please disconnect before attempting to connect.");
                }

                SocketState = SocketState.Connecting;
            }

            RequestedReconnect = false;
            ShouldAttemptResume = false;

            _socket = new WebSocket(url);

            _socket.SslConfiguration.EnabledSslProtocols |= (SslProtocols)3072; //TLS 1.2
            _socket.OnOpen += _listener.SocketOpened;
            _socket.OnClose += _listener.SocketClosed;
            _socket.OnError += _listener.SocketErrored;
            _socket.OnMessage += _listener.SocketMessage;
            _socket.ConnectAsync();
        }

        private void OnGatewayUrlUpdateFailed(RequestError error)
        {
            _logger.Warning("Failed to update gateway url. Will retry in 15 seconds.");
            _reconnectRetries = Math.Max(3, _reconnectRetries);
            
            //Set as disconnected if we're in pending reconnect state we reconnect goes through.
            if (SocketState == SocketState.PendingReconnect)
            {
                SocketState = SocketState.Disconnected;
            }
            Reconnect();
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
            Commands.OnSocketDisconnected();

            if (_reconnectTimer != null)
            {
                _reconnectTimer.Stop();
                _reconnectTimer.Dispose();
                _reconnectTimer = null;
            }
            
            lock (_lock)
            {
                if (IsDisconnected())
                {
                    DisposeSocket();
                    return;
                }

                if (requested)
                {
                    _socket?.CloseAsync(4199, "Discord Requested Reconnect");
                }
                else
                {
                    _socket?.CloseAsync(CloseStatusCode.Normal);
                }

                DisposeSocket();
                SocketState = SocketState.Disconnected;
            }

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
            Disconnect(false, false);

            _listener?.OnSocketShutdown();
            _listener = null;
            _socket = null;
        }

        /// <summary>
        /// Called when a websocket is closed to remove previous socket
        /// </summary>
        public void DisposeSocket()
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
        public void Send(GatewayCommandCode opCode, object data)
        {
            CommandPayload payload = CommandPayload.CreatePayload(opCode, data);
            Commands.Enqueue(payload);
        }
        
        internal bool Send(CommandPayload payload)
        {
            if (_socket == null)
            {
                return false;
            }
            
            string payloadData = JsonConvert.SerializeObject(payload, _client.ClientSerializerSettings);
            _logger.Verbose($"{nameof(Socket)}.{nameof(Send)} Payload: {{0}}", payloadData);

            _socket.SendAsync(payloadData, null);
            return true;
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
        /// Returns if the socket is waiting to reconnect
        /// </summary>
        /// <returns>Returns if the socket is waiting to reconnect</returns>
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

            lock (_lock)
            {
                if (!IsDisconnected())
                {
                    return;
                }

                SocketState = SocketState.PendingReconnect;
            }
            
            //If we haven't had any errors reconnect to the gateway
            if (_reconnectRetries == 0)
            {
                _reconnectRetries++;
                Interface.Oxide.NextTick(Connect);
                return;
            }

            //We had an error trying to reconnect. Perform Delayed Reconnects
            float delay = _reconnectRetries <= 3 ? 1f : 15f;

            _reconnectTimer = new Timer
            {
                Interval = delay * 1000,
                AutoReset = false
            };

            _reconnectTimer.Elapsed += ReconnectWebsocket;

            _logger.Warning("Attempting to reconnect to Discord... [Retry={0}]", _reconnectRetries);
            _reconnectTimer.Start();
            _reconnectRetries++;
        }

        private void ReconnectWebsocket(object sender, ElapsedEventArgs e)
        {
            Connect();
            _reconnectTimer = null;
        }

        internal void ResetRetries()
        {
            _reconnectRetries = 0;
        }
    }
}