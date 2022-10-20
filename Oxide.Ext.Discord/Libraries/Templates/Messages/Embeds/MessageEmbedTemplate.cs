using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Extensions;
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
        public bool Enabled { get; set; }

        ///<inheritdoc/>
        public string Title { get; set; } = string.Empty;
        
        ///<inheritdoc/>
        public string Url { get; set; } = string.Empty;
        
        ///<inheritdoc/>
        public string Description { get; set; } = string.Empty;

        ///<inheritdoc/>
        public string Color { get; set; } = DiscordColor.Default.ToHex();
        
        ///<inheritdoc/>
        public string ImageUrl { get; set; } = string.Empty;
        
        ///<inheritdoc/>
        public string ThumbnailUrl { get; set; } = string.Empty;
        
        ///<inheritdoc/>
        public string VideoUrl { get; set; } = string.Empty;
        
        ///<inheritdoc/>
        public bool TimeStamp { get; set; }
        
        ///<inheritdoc/>
        public List<MessageEmbedFieldTemplate> Fields { get; set; }
        
        ///<inheritdoc/>
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
        
        public DiscordEmbed ToEntity(PlaceholderData data, DiscordEmbed embed = null)
        {
            return DiscordEmbedTemplate.ToEntity(this, data, embed);
        }
    }
}