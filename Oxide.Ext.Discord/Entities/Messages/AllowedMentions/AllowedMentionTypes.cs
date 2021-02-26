using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Oxide.Ext.Discord.Entities.Messages.AllowedMentions
{
    /// <summary>
    ///  Represents a <a href="https://discord.com/developers/docs/resources/channel#allowed-mentions-object-allowed-mention-types">Allowed Mention Types</a> for a message
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AllowedMentionTypes
    {
        /// <summary>
        /// Controls role mentions
        /// </summary>
        [Description("roles")] 
        Roles,
        
        /// <summary>
        /// 	Controls user mentions
        /// </summary>
        [Description("users")] 
        Users,
        
        /// <summary>
        /// Controls @everyone and @here mentions
        /// </summary>
        [Description("everyone")] 
        Everyone,
    }
}