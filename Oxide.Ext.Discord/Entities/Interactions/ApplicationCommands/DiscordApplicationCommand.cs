using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Entities.Api;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#applicationcommand">ApplicationCommand</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordApplicationCommand
    {
        /// <summary>
        /// Unique id of the command
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// Unique id of the parent application
        /// </summary>
        [JsonProperty("application_id")]
        public Snowflake ApplicationId { get; set; }
        
        /// <summary>
        /// Guild ID of the command, if not global
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake? GuildId { get; set; }
        
        /// <summary>
        /// 1-32 lowercase character name matching ^[\w-]{1,32}$
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Description of the command (1-100 characters)
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        
        /// <summary>
        /// The parameters for the command
        /// See <see cref="CommandOption"/>
        /// </summary>
        [JsonProperty("options")]
        public List<CommandOption> Options { get; set; }
        
        /// <summary>
        /// Whether the command is enabled by default when the app is added to a guild
        /// </summary>
        [JsonProperty("default_permission")]
        public bool? DefaultPermissions { get; set; }

        /// <summary>
        /// Edit a global command.
        /// Updates will be available in all guilds after 1 hour.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#edit-global-application-command">Edit Global Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with updated command</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditGlobalApplicationCommand(DiscordClient client, Action<DiscordApplicationCommand> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/commands", RequestMethod.PATCH, this, callback, error);
        }
        
        /// <summary>
        /// Deletes a global command
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#delete-global-application-command">Delete Global Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteGlobalApplicationCommand(DiscordClient client, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{ApplicationId}/commands/{Id}", RequestMethod.PATCH, null, callback, error);
        }
    }
}