using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Messages
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/channel#message-object-message-application-structure">Message Application Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageApplication
    {
        /// <summary>
        /// ID of the application
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        
        /// <summary>
        /// ID of the embed's image asset
        /// </summary>
        [JsonProperty("cover_image")]
        public string CoverImage { get; set; }      
        
        /// <summary>
        /// Application's description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        
        /// <summary>
        /// ID of the application's icon
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }
        
        /// <summary>
        /// Name of the application
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}