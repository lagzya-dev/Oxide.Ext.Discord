using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Emojis;
using Oxide.Ext.Discord.Helpers.Interfaces;

namespace Oxide.Ext.Discord.Entities.Messages
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/channel#reaction-object">Reaction Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Reaction : IGetEntityId
    {
        /// <summary>
        /// Times this emoji has been used to react
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// Whether the current user reacted using this emoji
        /// </summary>
        [JsonProperty("me")]
        public bool Me { get; set; }

        /// <summary>
        /// Emoji information
        /// <see cref="Emoji"/>
        /// </summary>
        [JsonProperty("emoji")]
        public Emoji Emoji { get; set; }

        /// <summary>
        /// Returns the ID for this entity
        /// </summary>
        /// <returns>ID for this entity</returns>
        public Snowflake GetEntityId()
        {
            return Emoji.GetEntityId();
        }
    }
}
