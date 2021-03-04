using System.Collections.Generic;
using System.Timers;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Entities.Gatway.Commands;

namespace Oxide.Ext.Discord.WebSockets.Handlers
{
    /// <summary>
    /// Handles command queueing when the websocket is down
    /// </summary>
    public class SocketCommandHandler
    {
        private readonly Socket _webSocket;
        private readonly List<CommandPayload> _pendingCommands = new List<CommandPayload>();
        private Timer _sendTimer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="webSocket">Websocket to handle commands for</param>
        public SocketCommandHandler(Socket webSocket)
        {
            _webSocket = webSocket;
        }

        /// <summary>
        /// Enqueue a payload to be sent over the websocket
        /// If the websocket is connected it will be sent immediately
        /// If the websocket is not connected it will be queued until it is back online
        /// </summary>
        /// <param name="command">Command to send over the websocket</param>
        public void Enqueue(CommandPayload command)
        {
            if (_webSocket.IsConnected())
            {
                _webSocket.Send(command);
                return;
            }

            if (command.OpCode == GatewayCommandCode.StatusUpdate || command.OpCode == GatewayCommandCode.VoiceStateUpdate)
            {
                _pendingCommands.RemoveAll(p => p.OpCode == GatewayCommandCode.StatusUpdate || p.OpCode == GatewayCommandCode.VoiceStateUpdate);
            }
            
            _pendingCommands.Add(command);
            StartSendTimer();
        }

        private void StartSendTimer()
        {
            if (_sendTimer != null && _sendTimer.Enabled)
            {
                return;
            }

            _sendTimer = new Timer
            {
                Interval = 1000
            };
            _sendTimer.Elapsed += SendMessage;
        }

        private void SendMessage(object sender, ElapsedEventArgs e)
        {
            if (_pendingCommands.Count == 0)
            {
                _sendTimer.Stop();
                _sendTimer.Dispose();
                _sendTimer = null;
                return;
            }
            
            if (!_webSocket.IsConnected())
            {
                return;
            }

            CommandPayload payload = _pendingCommands[0];
            _webSocket.Send(payload);
            _pendingCommands.RemoveAt(0);
        }
    }
}