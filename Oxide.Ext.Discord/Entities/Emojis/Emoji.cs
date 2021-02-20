using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Helpers.Cdn;
using Oxide.Ext.Discord.Helpers.Interfaces;
using Formatting = Oxide.Ext.Discord.Helpers.Formatting;

namespace Oxide.Ext.Discord.Entities.Emojis
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/emoji#emoji-object">Emoji Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Emoji : EmojiUpdate, IGetEntityId
    {
        /// <summary>
        /// Emoji id
        /// </summary>
        [JsonProperty("id")]
        public Snowflake? Id { get; set; }

        /// <summary>
        /// User that created this emoji
        /// </summary>
        [JsonProperty("user")]
        public DiscordUser User { get; set; }

        /// <summary>
        /// Whether this emoji must be wrapped in colons
        /// </summary>
        [JsonProperty("require_colons")]
        public bool? RequireColons { get; set; }

        /// <summary>
        /// Whether this emoji is managed
        /// </summary>
        [JsonProperty("managed")]
        public bool? Managed { get; set; }

        /// <summary>
        /// Whether this emoji is animated
        /// </summary>
        [JsonProperty("animated")]
        public bool? Animated { get; set; }
        
        /// <summary>
        /// Whether this emoji can be used, may be false due to loss of Server Boosts
        /// </summary>
        [JsonProperty("available")]
        public bool? Available { get; set; }
        
        /// <summary>
        /// Url to the emoji image
        /// </summary>
        public string Url => Id.HasValue ? DiscordCdn.GetCustomEmojiUrl(Id.Value, Animated.HasValue && Animated.Value ? ImageFormat.Gif : ImageFormat.Png) : null;

        /// <summary>
        /// Returns the ID for this entity
        /// </summary>
        /// <returns>ID for this entity</returns>
        public Snowflake GetEntityId()
        {
            return Id ?? default(Snowflake);
        }
        
        /// <summary>
        /// Returns an emoji object for the given emoji character
        /// </summary>
        /// <param name="emoji"></param>
        /// <returns></returns>
        public static Emoji FromCharacter(string emoji)
        {
            return new Emoji
            {
                Name = emoji
            };
        }
        
        /// <summary>
        /// Returns the data string to be used in the API request
        /// </summary>
        /// <returns></returns>
        public string ToDataString()
        {
            if (!Id.HasValue)
            {
                return Name;
            }

            return Formatting.CustomEmojiDataString(Id.Value, Name, Animated ?? false);
        }

        internal void Update(Emoji emoji)
        {
            if (emoji.Name != null)
            {
                Name = emoji.Name;
            }
            
            if (emoji.RequireColons != null)
            {
                RequireColons = emoji.RequireColons;
            }    
            
            if (emoji.Managed != null)
            {
                Managed = emoji.Managed;
            }    
            
            if (emoji.Animated != null)
            {
                Animated = emoji.Animated;
            }       
            
            if (emoji.Available != null)
            {
                Available = emoji.Available;
            }
        }
    }
}
