using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.REST;

namespace Oxide.Ext.Discord.DiscordObjects.Interaction
{
    public class Interaction
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("type")]
        public InteractionType Type { get; set; }
        
        [JsonProperty("data")]
        public ApplicationCommandInteractionData Data { get; set; }
        
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }    
        
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        
        [JsonProperty("member")]
        public GuildMember GuildMember { get; set; }
        
        [JsonProperty("token")]
        public string Token { get; set; } 
        
        [JsonProperty("version")]
        public int Version { get; set; } 
        
        public void CreateResponse(DiscordClient client, InteractionResponse response, Action callback = null)
        {
            client.REST.DoRequest($"/interactions/{Id}/{Token}/callback", RequestMethod.POST, null, callback);
        }
        
        public void EditResponse(DiscordClient client, InteractionResponse response, Action callback = null)
        {
            client.REST.DoRequest($"/interactions/{Id}/{Token}/callback", RequestMethod.POST, null, callback);
        }
    }
}