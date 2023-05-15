using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Gateway.Commands;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Entities.Gateway
{
    /// <summary>
    /// Represents Discord Gatway Connection Url
    /// See <a href="https://discord.com/developers/docs/topics/gateway#get-gateway">Get Gateway</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class Gateway
    {
        /// <summary>
        /// Gatway URL to use
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
        
        /// <summary>
        /// Saved Gateway URL
        /// Example: wss://gateway.discord.gg/?v=8&amp;encoding=json
        /// </summary>
        public static string WebsocketUrl { get; private set; }
        
        public static DateTime LastUpdate { get; private set; }

        internal static readonly List<int> Shard = new List<int> {0, 1};
        internal static readonly ConnectionProperties Properties = new ConnectionProperties();

        /// <summary>
        /// Returns an object with a single valid WSS URL, which the client can use for Connecting.
        /// Clients should cache this value and only call this endpoint to retrieve a new URL if they are unable to properly establish a connection using the cached version of the URL.
        /// See <a href="https://discord.com/developers/docs/topics/gateway#get-gateway">Get Gateway</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the Gateway response</param>
        /// <param name="error">API error callback</param>
        private static void GetGateway(BotClient client, Action<Gateway> callback, Action<RequestError> error = null)
        {
            client.Rest.CreateRequest(client.GetFirstClient(),"gateway", RequestMethod.GET, null, callback, error);
        }

        public static void UpdateGatewayUrl(BotClient client, Action callback, Action<RequestError> error = null)
        {
            GetGateway(client, gateway =>
            {
                WebsocketUrl = $"{gateway.Url}/?{DiscordEndpoints.Websocket.WebsocketArgs}";
                LastUpdate = DateTime.UtcNow;
                client.Logger.Debug($"{nameof(Gateway)}.{nameof(UpdateGatewayUrl)} Updated Gateway Url: {{0}}", WebsocketUrl);
                callback.Invoke();
            }, error);
        }
    }
}
