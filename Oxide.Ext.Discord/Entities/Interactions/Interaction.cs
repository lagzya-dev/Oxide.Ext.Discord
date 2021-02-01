using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.REST;

namespace Oxide.Ext.Discord.Entities.Interactions
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#interaction">Interaction Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Interaction
    {
        /// <summary>
        /// Id of the interaction
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        
        /// <summary>
        /// The type of interaction
        /// See <see cref="InteractionType"/>
        /// </summary>
        [JsonProperty("type")]
        public InteractionType Type { get; set; }
        
        /// <summary>
        /// The command data payload
        /// See <see cref="ApplicationCommandInteractionData"/>
        /// </summary>
        [JsonProperty("data")]
        public ApplicationCommandInteractionData Data { get; set; }
        
        /// <summary>
        /// The guild it was sent from
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }    
        
        /// <summary>
        /// The channel it was sent from
        /// </summary>
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        
        /// <summary>
        /// Guild member data for the invoking user
        /// </summary>
        [JsonProperty("member")]
        public GuildMember Member { get; set; }
        
        /// <summary>
        /// A continuation token for responding to the interaction
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; } 
        
        /// <summary>
        /// Read-only property, always 1
        /// </summary>
        [JsonProperty("version")]
        public int Version { get; set; } 
        
        /// <summary>
        /// Create a response to an Interaction from the gateway.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="response">Response to respond with</param>
        /// <param name="callback">Callback once the action is completed</param>
        public void CreateResponse(DiscordClient client, InteractionResponse response, Action callback = null)
        {
            client.Bot.Rest.DoRequest($"/interactions/{Id}/{Token}/callback", RequestMethod.POST, response, callback);
        }
    }
}