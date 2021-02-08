using System;
using System.Timers;
using Newtonsoft.Json;
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
        public bool RequestReconnect;
        
        /// <summary>
        /// If we should attempt to resume our previous session after connecting
        /// </summary>
        public bool ShouldAttemptResume;
        
        /// <summary>
        /// Timer to use when attempting to reconnect to discord due to an error
        /// </summary>
        internal Timer ReconnectTimer;
        
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
            if (string.IsNullOrEmpty(url))
            {
                _client.UpdateGatewayUrl(Connect);
                return;
            }

            if (_socket != null)
            {
                throw new Exception("Socket is already running. Please disconnect before attempting to connect.");
                // Assume force-reconnect
                //Disconnect(false);
            }
            _client.DestroyHeartbeat();

            _socket = new WebSocket($"{url}/?{Entities.Gatway.Connect.Serialize()}");

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
            RequestReconnect = attemptReconnect;
            ShouldAttemptResume = shouldResume;
            
            if (ReconnectTimer != null)
            {
                ReconnectTimer.Stop();
                ReconnectTimer.Dispose();
                ReconnectTimer = null;
            }
            
            if (IsClosingOrClosed())
            {
                return;
            }

            if (requested)
            {
                _socket.CloseAsync(4199, "Discord server requested reconnect");
            }
            else
            {
                _socket.CloseAsync(CloseStatusCode.Normal);
            }

            DisposeSocket();
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
        /// Disposes of the previous websocket
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
        /// <param name="completed">Action once the action is completed and if it was successful</param>
        public void Send(GatewayCommandCode opCode, object data, Action<bool> completed = null)
        {
            if (!IsAlive())
            {
                return;
            }
            
            CommandPayload opcode = new CommandPayload
            {
                OpCode = opCode,
                Payload = data
            };
            
            _socket.SendAsync(JsonConvert.SerializeObject(opcode), completed);
        }

        /// <summary>
        /// Returns if the websocket is in the open state
        /// </summary>
        /// <returns>Returns if the websocket is in open state</returns>
        public bool IsAlive()
        {
            if (_socket == null)
            {
                return false;
            }
            
            return _socket.ReadyState == WebSocketState.Open;
        }
        
        /// <summary>
        /// Returns if the websocket is in the connecting state
        /// </summary>
        /// <returns>Returns if the websocket is in connecting state</returns>
        public bool IsConnecting()
        {
            if (_socket == null)
            {
                return false;
            }
            
            return _socket.ReadyState == WebSocketState.Connecting;
        }

        /// <summary>
        /// Returns if the websocket is null or is currently closing / closed
        /// </summary>
        /// <returns>Returns if the websocket is null or is currently closing / closed</returns>
        public bool IsClosingOrClosed()
        {
            if (_socket == null)
            {
                return true;
            }

            return _socket.ReadyState == WebSocketState.Closing || _socket.ReadyState == WebSocketState.Closed;
        }
        
        /// <summary>
        /// Returns if there is a reconnect timer active for the websocket
        /// </summary>
        /// <returns></returns>
        public bool IsReconnectTimerActive()
        {
            return ReconnectTimer != null && ReconnectTimer.Enabled;
        }

        /// <summary>
        /// Starts a reconnect timer to reconnect to perform the reconnect action after the timer has elapsed
        /// </summary>
        /// <param name="seconds">How many seconds until we fire the action</param>
        /// <param name="callback">Callback to run once the timer is completed</param>
        public void StartReconnectTimer(float seconds, Action callback)
        {
            if (IsReconnectTimerActive())
            {
                return;
            }
            
            ReconnectTimer = new Timer
            {
                Interval = seconds * 1000,
                AutoReset = false
            };
            ReconnectTimer.Elapsed += (_, __) =>
            {
                callback.Invoke();
            };
            
            ReconnectTimer.Start();
        }
    }
}
