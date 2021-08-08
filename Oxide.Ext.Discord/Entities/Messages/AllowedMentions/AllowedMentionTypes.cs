using System.Runtime.Serialization;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Helpers.Converters;

namespace Oxide.Ext.Discord.Entities.Messages.AllowedMentions
{
    /// <summary>
    ///  Represents a <a href="https://discord.com/developers/docs/resources/channel#allowed-mentions-object-allowed-mention-types">Allowed Mention Types</a> for a message
    /// </summary>
    [JsonConverter(typeof(DiscordEnumConverter<AllowedMentionTypes>), Unknown)]
    public enum AllowedMentionTypes
    {
        /// <summary>
        /// Discord Extension doesn't currently support this allowed mention type.
        /// </summary>
        Unknown,
        
        /// <summary>
        /// Controls role mentions
        /// </summary>
        [EnumMember(Value = "roles")] 
        Roles,
        
        /// <summary>
        /// 	Controls user mentions
        /// </summary>
        [EnumMember(Value = "users")] 
        Users,
        
        /// <summary>
        /// Controls @everyone and @here mentions
        /// </summary>
        [EnumMember(Value = "everyone")] 
        Everyone,
    }
}