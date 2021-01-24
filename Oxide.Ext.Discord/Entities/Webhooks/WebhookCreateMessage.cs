using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Webhooks
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class WebhookCreateMessage : WebhookEditMessage
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("tts")]
        public bool Tts { get; set; }

        [JsonProperty("file")]
        public string File { get; set; }
    }
}
