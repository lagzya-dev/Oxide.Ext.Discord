using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#get-guild-prune-count">Guild Prune Get</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildPruneGet
    {
        /// <summary>
        /// Number of days to count prune for (1 - 30)
        /// </summary>
        [JsonProperty("days")]
        public int Days { get; set; }
        
        /// <summary>
        /// List of roles to include
        /// </summary>
        [JsonProperty("include_roles")]
        public List<Snowflake> IncludeRoles { get; set; }
        
        /// <summary>
        /// Returns the query string for the Guild Prune Get endpoint
        /// </summary>
        /// <returns>Guild Prune Get Query String</returns>
        public virtual string ToQueryString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("days=");
            sb.Append(Days.ToString());

            if (IncludeRoles != null)
            {
                sb.Append("&include_roles=");
                for (int index = 0; index < IncludeRoles.Count; index++)
                {
                    Snowflake role = IncludeRoles[index];
                    sb.Append(role.ToString());
                    if (index != IncludeRoles.Count - 1)
                    {
                        sb.Append(",");
                    }
                }
            }
            
            return sb.ToString();
        }
    }
}