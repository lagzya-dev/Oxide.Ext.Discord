using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildPruneGet
    {
        [JsonProperty("days")]
        public int Days { get; set; }
        
        [JsonProperty("include_roles")]
        public List<Snowflake> IncludeRoles { get; set; }
        
        public virtual string ToQueryString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"days={Days}");

            if (IncludeRoles != null)
            {
                sb.Append($"&include_roles={string.Join(",", IncludeRoles.Select(r => r.ToString()).ToArray())}");
            }
            
            return sb.ToString();
        }
    }
}