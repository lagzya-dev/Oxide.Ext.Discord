using System.Collections.Generic;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds.Fields;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds
{
    public interface IEmbedTemplate
    {
        /// <summary>
        /// If this embed is enabled
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// The Tile for the embed
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// This Title Url for the embed
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// The description of the embed
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The Hex Color for the embed
        /// </summary>
        string Color { get; set; }

        /// <summary>
        /// Image URL to show in the embed
        /// </summary>
        string ImageUrl { get; set; }

        /// <summary>
        /// Thumbnail url to show in the embed
        /// </summary>
        string ThumbnailUrl { get; set; }

        /// <summary>
        /// Video url to show in the embed
        /// </summary>
        string VideoUrl { get; set; }

        /// <summary>
        /// Show timestamp in the embed
        /// </summary>
        bool TimeStamp { get; set; }

        /// <summary>
        /// Fields for the embed
        /// </summary>
        List<MessageEmbedFieldTemplate> Fields { get; set; }

        /// <summary>
        /// Footer for the embed
        /// </summary>
        EmbedFooterTemplate Footer { get; set; }

        /// <summary>
        /// Converts the template to a <see cref="DiscordEmbed"/>
        /// </summary>
        /// <param name="data">Data to use</param>
        /// <param name="embed">Initial embed to use</param>
        /// <returns></returns>
        DiscordEmbed ToEntity(PlaceholderData data, DiscordEmbed embed = null);
    }
}