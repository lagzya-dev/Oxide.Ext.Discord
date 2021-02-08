using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Webhooks
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/webhook#execute-webhook-jsonform-params">Webhook Create Message</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class WebhookCreateMessage : WebhookEditMessage
    {
        /// <summary>
        /// Override the default username of the webhook
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Override the default avatar of the webhook
        /// </summary>
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// If this is a TTS message
        /// </summary>
        [JsonProperty("tts")]
        public bool Tts { get; set; }
    }
}
