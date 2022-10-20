using System.Threading.Tasks;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.WebSockets.Handlers;

namespace Oxide.Ext.Discord.Callbacks.Websockets
{
    public class WebsocketReconnectCallback : BaseAsyncCallback
    {
        private WebSocketReconnectHandler _reconnect;

        public static void Start(WebSocketReconnectHandler reconnect)
        {
            WebsocketReconnectCallback callback = DiscordPool.Get<WebsocketReconnectCallback>();
            callback.Init(reconnect);
            callback.Run();
        }

        private void Init(WebSocketReconnectHandler reconnect)
        {
            _reconnect = reconnect;
        }
        
        protected override Task HandleCallback()
        {
            return _reconnect.StartReconnect();
        }

        protected override string ExceptionData()
        {
            return $"Websocket: {_reconnect._webSocket.Handler.WebsocketId}";
        }

        protected override void EnterPool()
        {
            _reconnect = null;
        }
    }
}