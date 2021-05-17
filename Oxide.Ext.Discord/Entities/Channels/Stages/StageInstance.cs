using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Api;

namespace Oxide.Ext.Discord.Entities.Channels.Stages
{
    /// <summary>
    /// Represents a channel <a href="https://discord.com/developers/docs/resources/stage-instance#auto-closing-stage-instance-structure">Stage Instance</a> within Discord.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class StageInstance
    {
        /// <summary>
        /// The ID of this Stage instance
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// The guild ID of the associated Stage channel
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        
        /// <summary>
        /// The ID of the associated Stage channel
        /// </summary>
        [JsonProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        
        /// <summary>
        /// The topic of the Stage instance (1-120 characters)
        /// </summary>
        [JsonProperty("topic")]
        public string Topic { get; set; }

        /// <summary>
        /// Creates a new Stage instance associated to a Stage channel.
        /// Requires the user to be a moderator of the Stage channel.
        /// See <a href="https://discord.com/developers/docs/resources/stage-instance#create-stage-instance">Create Stage Instance</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to create a stage in</param>
        /// <param name="topic">The topic for the stage instance</param>
        /// <param name="callback">Callback with the new stage instance</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void CreateStageInstance(DiscordClient client, Snowflake channelId, string topic, Action<StageInstance> callback = null, Action<RestError> error = null)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                ["channel_id"] = channelId,
                ["topic"] = topic
            };
            
            client.Bot.Rest.DoRequest($"/stage-instances", RequestMethod.POST, data, callback, error);
        }
        
        /// <summary>
        /// Gets the stage instance associated with the Stage channel, if it exists.
        /// See <a href="https://discord.com/developers/docs/resources/stage-instance#get-stage-instance">Get Stage Instance</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to get the stage instance for</param>
        /// <param name="callback">Callback with the new stage instance</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void GetStageInstance(DiscordClient client, Snowflake channelId, Action<StageInstance> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/stage-instances/{channelId}", RequestMethod.GET, null, callback, error);
        }
        
        /// <summary>
        /// Updates fields of an existing Stage instance.
        /// Requires the user to be a moderator of the Stage channel.
        /// See <a href="https://discord.com/developers/docs/resources/stage-instance#update-stage-instance">Update Stage Instance</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="topic">The new topic for the stage instance</param>
        /// <param name="callback">Callback when the updated stage instance</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void UpdateStageInstance(DiscordClient client, string topic, Action<StageInstance> callback = null, Action<RestError> error = null)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                ["topic"] = topic
            };
            
            client.Bot.Rest.DoRequest($"/stage-instances/{ChannelId}", RequestMethod.PATCH, data, callback, error);
        }
        
        /// <summary>
        /// Deletes the Stage instance.
        /// Requires the user to be a moderator of the Stage channel.
        /// See <a href="https://discord.com/developers/docs/resources/stage-instance#delete-stage-instance">Delete Stage Instance</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback when the stage instance is deleted</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteStageInstance(DiscordClient client, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/stage-instances/{ChannelId}", RequestMethod.DELETE, null, callback, error);
        }
    }
}