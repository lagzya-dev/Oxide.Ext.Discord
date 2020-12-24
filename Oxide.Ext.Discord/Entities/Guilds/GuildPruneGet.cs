using System.Collections.Generic;
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
        public List<string> IncludeRoles { get; set; }
        
        public virtual string ToQueryString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"days={Days}");

            if (IncludeRoles != null)
            {
                sb.Append($"&include_roles={string.Join(",", IncludeRoles.ToArray())}");
            }
            
            return sb.ToString();
        }
    }
}