using System;
using Newtonsoft.Json;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Exceptions.Entities;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Promise;

namespace Oxide.Ext.Discord.Entities.Channels.Stages
{
    /// <summary>
    /// Represents a channel <a href="https://discord.com/developers/docs/resources/stage-instance#stage-instance-object-stage-instance-structure">Stage Instance</a> within Discord.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class StageInstance : ISnowflakeEntity
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
        /// The privacy level of the Stage instance
        /// </summary>
        [JsonProperty("privacy_level")]
        public PrivacyLevel PrivacyLevel { get; set; }
        
        /// <summary>
        /// Whether or not Stage discovery is disabled (deprecated)   
        /// </summary>
        [Obsolete("Deprecated by discord")]
        [JsonProperty("discoverable_disabled")]
        public bool DiscoverableDisabled { get; set; }
        
        /// <summary>
        /// The id of the scheduled event for this Stage instance
        /// </summary>
        [JsonProperty("guild_scheduled_event_id")]
        public Snowflake? GuildScheduledEventId { get; set; }

        /// <summary>
        /// Creates a new Stage instance associated to a Stage channel.
        /// Requires the user to be a moderator of the Stage channel.
        /// See <a href="https://discord.com/developers/docs/resources/stage-instance#create-stage-instance">Create Stage Instance</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="create">Create Stage Instance Object</param>
        /// <param name="callback">Callback with the new stage instance</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static IDiscordPromise<StageInstance> CreateStageInstance(DiscordClient client, StageInstanceCreate create)
        {
            if (create == null) throw new ArgumentNullException(nameof(create));
            InvalidSnowflakeException.ThrowIfInvalid(create.ChannelId, nameof(create.ChannelId));
            return client.Bot.Rest.CreateRequest<StageInstance>(client,"stage-instances", RequestMethod.POST, create);
        }
        
        /// <summary>
        /// Gets the stage instance associated with the Stage channel, if it exists.
        /// See <a href="https://discord.com/developers/docs/resources/stage-instance#get-stage-instance">Get Stage Instance</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to get the stage instance for</param>
        /// <param name="callback">Callback with the new stage instance</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static IDiscordPromise<StageInstance> GetStageInstance(DiscordClient client, Snowflake channelId, Action<StageInstance> callback = null, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(channelId, nameof(channelId));
            return client.Bot.Rest.CreateRequest<StageInstance>(client,$"stage-instances/{channelId}", RequestMethod.GET);
        }

        /// <summary>
        /// Modifies fields of an existing Stage instance.
        /// Requires the user to be a moderator of the Stage channel.
        /// See <a href="https://discord.com/developers/docs/resources/stage-instance#modify-stage-instance">Update Stage Instance</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="update">Update for the stage instance</param>
        /// <param name="callback">Callback when the updated stage instance</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IDiscordPromise<StageInstance> ModifyStageInstance(DiscordClient client, StageInstanceUpdate update, Action<StageInstance> callback = null, Action<RequestError> error = null)
        {
            if (update == null) throw new ArgumentNullException(nameof(update));
            return client.Bot.Rest.CreateRequest<StageInstance>(client,$"stage-instances/{ChannelId}", RequestMethod.PATCH, update);
        }
        
        /// <summary>
        /// Deletes the Stage instance.
        /// Requires the user to be a moderator of the Stage channel.
        /// See <a href="https://discord.com/developers/docs/resources/stage-instance#delete-stage-instance">Delete Stage Instance</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback when the stage instance is deleted</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IDiscordPromise DeleteStageInstance(DiscordClient client, Action callback = null, Action<RequestError> error = null)
        {
            return client.Bot.Rest.CreateRequest(client,$"stage-instances/{ChannelId}", RequestMethod.DELETE);
        }

        internal StageInstance Update(StageInstance stage)
        {
            StageInstance previous = (StageInstance)MemberwiseClone();
            if (stage.Topic != null)
                Topic = stage.Topic;

            PrivacyLevel = stage.PrivacyLevel;
            
            return previous;
        }
    }
}