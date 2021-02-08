using System.Text;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#begin-guild-prune">Guild Prune Begin</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildPruneBegin : GuildPruneGet
    {
        /// <summary>
        /// Whether 'pruned' is returned, discouraged for large guilds
        /// </summary>
        [JsonProperty("compute_prune_count")]
        public bool ComputePruneCount { get; set; }
        
        /// <summary>
        /// Returns Guild Prune Begin query string for the API Endpoint
        /// </summary>
        /// <returns>Guild Prune Begin Query String</returns>
        public override string ToQueryString()
        {
            StringBuilder sb = new StringBuilder(base.ToQueryString());
            sb.Append($"&compute_prune_count={ComputePruneCount}");

            return sb.ToString();
        }
    }
}