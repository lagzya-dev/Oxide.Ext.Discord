using Newtonsoft.Json;
using System;
using Oxide.Ext.Discord.Rest;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#applicationcommand">ApplicationCommand</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ApplicationCommand : ApplicationCommandCreate
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
        /// Edit a global command.
        /// Updates will be available in all guilds after 1 hour.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#edit-global-application-command">Edit Global Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with updated command</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditGlobalApplicationCommand(DiscordClient client, Action<ApplicationCommand> callback = null, Action<RestError> error = null)
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