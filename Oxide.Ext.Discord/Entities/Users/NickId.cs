using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Users
{
    /// <summary>
    /// Represents a User ID and Nick Name
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class NickId
    {
        /// <summary>
        /// User ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// User Nickname
        /// </summary>
        [JsonProperty("nick")]
        public string Nick { get; set; }
    }
}
