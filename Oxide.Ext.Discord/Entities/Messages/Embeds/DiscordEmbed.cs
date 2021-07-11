using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Roles;

namespace Oxide.Ext.Discord.Entities.Messages.Embeds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/channel#embed-object">Embed Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordEmbed
    {
        /// <summary>
        /// Title of embed
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; private set; }

        /// <summary>
        /// Type of embed (always "rich" for webhook embeds)
        /// </summary>
        [Obsolete("Embed types should be considered deprecated and might be removed in a future API version")]
        [JsonProperty("type")]
        public string Type { get; private set; }

        /// <summary>
        /// Description of embed
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; private set; }

        /// <summary>
        /// Url of embed
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; private set; }

        /// <summary>
        /// Timestamp of embed content
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; private set; }

        /// <summary>
        /// Color code of the embed
        /// </summary>
        [JsonProperty("color")]
        public DiscordColor Color { get; private set; }

        /// <summary>
        /// Footer information
        /// <see cref="EmbedFooter"/>
        /// </summary>
        [JsonProperty("footer")]
        public EmbedFooter Footer { get; private set; }

        /// <summary>
        /// Image information
        /// <see cref="EmbedImage"/>
        /// </summary>
        [JsonProperty("image")]
        public EmbedImage Image { get; private set; }

        /// <summary>
        /// Thumbnail information
        /// <see cref="EmbedThumbnail"/>
        /// </summary>
        [JsonProperty("thumbnail")]
        public EmbedThumbnail Thumbnail { get; private set; }

        /// <summary>
        /// Video information
        /// <see cref="EmbedVideo"/>
        /// </summary>
        [JsonProperty("video")]
        public EmbedVideo Video { get; private set; }

        /// <summary>
        /// Provider information
        /// <see cref="EmbedProvider"/>
        /// </summary>
        [JsonProperty("provider")]
        public EmbedProvider Provider { get; private set; }

        /// <summary>
        /// Author information
        /// <see cref="EmbedAuthor"/>
        /// </summary>
        [JsonProperty("author")]
        public EmbedAuthor Author { get; private set; }

        /// <summary>
        /// Fields information
        /// <see cref="EmbedField"/>
        /// </summary>
        [JsonProperty("fields")]
        public List<EmbedField> Fields { get; private set; }

        /// <summary>
        /// Adds a title to the embed message
        /// </summary>
        /// <param name="title">Title to add</param>
        /// <returns>This</returns>
        public DiscordEmbed AddTitle(string title)
        {
            if (title != null && title.Length > 256)
            {
                throw new Exception("Title cannot be more than 256 characters");
            }
            
            Title = title;
            return this;
        }

        /// <summary>
        /// Adds a description to the embed message
        /// </summary>
        /// <param name="description">description to add</param>
        /// <returns>This</returns>
        public DiscordEmbed AddDescription(string description)
        {
            if (description != null && description.Length > 4096)
            {
                throw new Exception("Description cannot be more than 4096 characters");
            }
            
            Description = description;
            return this;
        }

        /// <summary>
        /// Adds a url to the embed message
        /// </summary>
        /// <param name="url"></param>
        /// <returns>This</returns>
        public DiscordEmbed AddUrl(string url)
        {
            Url = url;
            return this;
        }

        /// <summary>
        /// Adds an author to the embed message. The author will appear above the title
        /// </summary>
        /// <param name="name">Name of the author</param>
        /// <param name="iconUrl">Icon Url to use for the author</param>
        /// <param name="url">Url to go to when the authors name is clicked on</param>
        /// <param name="proxyIconUrl">Backup icon url. Can be left null if you only have one icon url</param>
        /// <returns>This</returns>
        public DiscordEmbed AddAuthor(string name, string iconUrl = null, string url = null, string proxyIconUrl = null)
        {
            if (name != null && name.Length > 256)
            {
                throw new Exception("Author name cannot be more than 256 characters");
            }
            
            Author = new EmbedAuthor(name, iconUrl, url, proxyIconUrl);
            return this;
        }

        /// <summary>
        /// Adds a footer to the embed message
        /// </summary>
        /// <param name="text">Text to be added to the footer</param>
        /// <param name="iconUrl">Icon url to add in the footer. Appears to the left of the text</param>
        /// <param name="proxyIconUrl">Backup icon url. Can be left null if you only have one icon url</param>
        /// <returns>This</returns>
        public DiscordEmbed AddFooter(string text, string iconUrl = null, string proxyIconUrl = null)
        {
            if (text != null && text.Length > 2048)
            {
                throw new Exception("Author name cannot be more than 2048 characters");
            }
            
            Footer = new EmbedFooter(text, iconUrl, proxyIconUrl);
            return this;
        }

        /// <summary>
        /// Adds an int based color to the embed. Color appears as a bar on the left side of the message
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public DiscordEmbed AddColor(uint color)
        {
            Color = new DiscordColor(color);
            return this;
        }

        /// <summary>
        /// Adds a hex based color. Color appears as a bar on the left side of the message
        /// </summary>
        /// <param name="color">Color in string hex format</param>
        /// <returns>This</returns>
        /// <exception cref="Exception">Exception thrown if color is outside of range</exception>
        public DiscordEmbed AddColor(string color)
        {
            Color = new DiscordColor(uint.Parse(color.TrimStart('#'), NumberStyles.AllowHexSpecifier));
            return this;
        }

        /// <summary>
        /// Adds a RGB based color. Color appears as a bar on the left side of the message
        /// </summary>
        /// <param name="red">Red value between 0 - 255</param>
        /// <param name="green">Green value between 0 - 255</param>
        /// <param name="blue">Blue value between 0 - 255</param>
        /// <returns>This</returns>
        /// <exception cref="Exception">Thrown if red, green, or blue is outside of range</exception>
        public DiscordEmbed AddColor(int red, int green, int blue)
        {
            Color = new DiscordColor(red, green, blue);
            return this;
        }

        /// <summary>
        /// Adds a blank field.
        /// If inline it will add a blank column.
        /// If not inline will add a blank row
        /// </summary>
        /// <param name="inline">If the field is inline</param>
        /// <returns>This</returns>
        public DiscordEmbed AddBlankField(bool inline)
        {
            if (Fields == null)
            {
                Fields = new List<EmbedField>();
            }

            if (Fields.Count >= 25)
            {
                throw new Exception("Embeds cannot have more than 25 fields");
            }
            
            Fields.Add(new EmbedField("\u200b", "\u200b", inline));
            return this;
        }

        /// <summary>
        /// Adds a new field with the name as the title and value as the value.
        /// If inline will add a new column. If row will add in a new row.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="inline"></param>
        /// <returns></returns>
        public DiscordEmbed AddField(string name, string value, bool inline)
        {
            if (Fields == null)
            {
                Fields = new List<EmbedField>();
            }

            if (Fields.Count >= 25)
            {
                throw new Exception("Embeds cannot have more than 25 fields");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Embed Fields cannot have a null or empty name");
            }
            
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Embed Fields cannot have a null or empty value");
            }

            if (name.Length > 256)
            {
                throw new Exception("Field name cannot be more than 256 characters");
            }
            
            if (value.Length > 1024 )
            {
                throw new Exception("Field name cannot be more than 1024  characters");
            }
            
            Fields.Add(new EmbedField(name, value, inline));
            return this;
        }

        /// <summary>
        /// Adds an image to the embed. The url should point to the url of the image.
        /// If using attachment image you can make the url: "attachment://{image name}.{image extension}
        /// </summary>
        /// <param name="url">Url for the image</param>
        /// <param name="width">width of the image</param>
        /// <param name="height">height of the image</param>
        /// <param name="proxyUrl">Backup url for the image</param>
        /// <returns></returns>
        public DiscordEmbed AddImage(string url, int? width = null, int? height = null, string proxyUrl = null)
        {
            Image = new EmbedImage(url, width, height, proxyUrl);
            return this;
        }

        /// <summary>
        /// Adds a thumbnail in the top right corner of the embed
        /// If using attachment image you can make the url: "attachment://{image name}.{image extension}
        /// </summary>
        /// <param name="url">Url for the image</param>
        /// <param name="width">width of the image</param>
        /// <param name="height">height of the image</param>
        /// <param name="proxyUrl">Backup url for the image</param>
        /// <returns></returns>
        public DiscordEmbed AddThumbnail(string url, int? width = null, int? height = null, string proxyUrl = null)
        {
            Thumbnail = new EmbedThumbnail(url, width, height, proxyUrl);
            return this;
        }

        /// <summary>
        /// Adds a video to the embed
        /// </summary>
        /// <param name="url">Url for the video</param>
        /// <param name="width">Width of the video</param>
        /// <param name="height">Height of the video</param>
        /// <param name="proxyUrl">Proxy Url for the video</param>
        /// <returns></returns>
        public DiscordEmbed AddVideo(string url, int? width = null, int? height = null, string proxyUrl = null)
        {
            Video = new EmbedVideo(url, width, height, proxyUrl);
            return this;
        }

        /// <summary>
        /// Adds a provider to the embed
        /// </summary>
        /// <param name="name">Name for the provider</param>
        /// <param name="url">Url for the provider</param>
        /// <returns></returns>
        public DiscordEmbed AddProvider(string name, string url)
        {
            Provider = new EmbedProvider(name, url);
            return this;
        }
    }
}