using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class MessageSticker
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("pack_id")]
        public string PackId { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("tags")]
        public string Tags { get; set; }
        
        [JsonProperty("asset")]
        public string Asset { get; set; }
        
        [JsonProperty("preview_asset")]
        public string PreviewAsset { get; set; }
        
        [JsonProperty("format_type")]
        public MessageStickerFormatType FormatType { get; set; }
    }
}