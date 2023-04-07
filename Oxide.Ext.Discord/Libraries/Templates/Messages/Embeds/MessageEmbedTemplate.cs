using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds.Fields;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds
{
    /// <summary>
    /// Represents a Embed for <see cref="DiscordMessageTemplate"/>
    /// </summary>
    public class MessageEmbedTemplate : IEmbedTemplate
    {
        ///<inheritdoc/>
        [JsonProperty("Show Embed")]
        public bool Enabled { get; set; }

        ///<inheritdoc/>
        [JsonProperty("Embed Title")]
        public string Title { get; set; } = string.Empty;
        
        ///<inheritdoc/>
        [JsonProperty("Embed Title URL")]
        public string Url { get; set; } = string.Empty;
        
        ///<inheritdoc/>
        [JsonProperty("Embed Description")]
        public string Description { get; set; } = string.Empty;

        ///<inheritdoc/>
        [JsonProperty("Embed Hex Color")]
        public string Color { get; set; } = DiscordColor.Default.ToHex();
        
        ///<inheritdoc/>
        [JsonProperty("Embed Image URL")]
        public string ImageUrl { get; set; } = string.Empty;
        
        ///<inheritdoc/>
        [JsonProperty("Embed Thumbnail URL")]
        public string ThumbnailUrl { get; set; } = string.Empty;
        
        ///<inheritdoc/>
        [JsonProperty("Embed View Url")]
        public string VideoUrl { get; set; } = string.Empty;
        
        ///<inheritdoc/>
        [JsonProperty("Show Embed TimeStamp")]
        public bool TimeStamp { get; set; } = false;
        
        ///<inheritdoc/>
        [JsonProperty("Embed Fields")]
        public List<MessageEmbedFieldTemplate> Fields { get; set; }
        
        ///<inheritdoc/>
        [JsonProperty("Embed Footer")]
        public EmbedFooterTemplate Footer { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        [JsonConstructor]
        public MessageEmbedTemplate() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="titleUrl"></param>
        public MessageEmbedTemplate(string title = "", string description = "", string titleUrl = "")
        {
            Title = title;
            Description = description;
            Url = titleUrl;
        }
        
        /// <summary>
        /// Returns a <see cref="DiscordEmbed"/> for the given template
        /// </summary>
        /// <param name="data"></param>
        /// <param name="embed"></param>
        /// <returns></returns>
        public DiscordEmbed ToEntity(PlaceholderData data, DiscordEmbed embed = null)
        {
            return DiscordEmbedTemplate.ToEntity(this, data, embed);
        }
    }
}