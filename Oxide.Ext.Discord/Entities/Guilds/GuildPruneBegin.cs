using System.Text;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildPruneBegin : GuildPruneGet
    {
        [JsonProperty("compute_prune_count")]
        public bool ComputePruneCount { get; set; }
        
        public override string ToQueryString()
        {
            StringBuilder sb = new StringBuilder(base.ToQueryString());
            sb.Append($"&compute_prune_count={ComputePruneCount}");

            return sb.ToString();
        }
    }
}