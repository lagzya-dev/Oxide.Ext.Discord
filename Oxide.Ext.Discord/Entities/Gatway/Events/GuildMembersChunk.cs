using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildMembersChunk
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("members")]
        public List<GuildMember> Members { get; set; }

        [JsonProperty("chunk_index")]
        public int ChunkIndex { get; set; }	
        
        [JsonProperty("chunk_count")]
        public int ChunkCount { get; set; }	
        
        [JsonProperty("not_found")]
        public List<string> NotFound { get; set; }
        
        [JsonProperty("presences")]
        public List<StatusUpdate> Presences { get; set; }      
        
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
    }
}
