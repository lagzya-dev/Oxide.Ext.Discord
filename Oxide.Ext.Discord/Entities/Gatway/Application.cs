using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Teams;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Entities.Webhooks;
using Oxide.Ext.Discord.Helpers.Cdn;
using Oxide.Ext.Discord.REST;

namespace Oxide.Ext.Discord.Entities.Interactions
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/oauth2#application-object">Application Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Application
    {
        /// <summary>
        /// The id of the app
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        
        /// <summary>
        /// The name of the app
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// The icon hash of the app
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }
        
        /// <summary>
        /// The description of the app
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        
        /// <summary>
        /// An array of rpc origin urls, if rpc is enabled
        /// </summary>
        [JsonProperty("rpc_origins")]
        public List<string> RpcOrigins { get; set; }
        
        /// <summary>
        /// When false only app owner can join the app's bot to guilds
        /// </summary>
        [JsonProperty("bot_public")]
        public bool BotPublic { get; set; }
        
        /// <summary>
        /// When true the app's bot will only join upon completion of the full oauth2 code grant flow
        /// </summary>
        [JsonProperty("bot_require_code_grant")]
        public bool BotRequireCodeGrant { get; set; }
        
        /// <summary>
        /// Partial user object containing info on the owner of the application
        /// </summary>
        [JsonProperty("owner")]
        public DiscordUser Owner { get; set; }
        
        /// <summary>
        /// If this application is a game sold on Discord, this field will be the summary field for the store page of its primary sku
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }
        
        /// <summary>
        /// The base64 encoded key for the GameSDK's GetTicket
        /// </summary>
        [JsonProperty("verify_key")]
        public string Verify { get; set; }
        
        /// <summary>
        /// If the application belongs to a team, this will be a list of the members of that team
        /// </summary>
        [JsonProperty("team")]
        public Team Team { get; set; }
        
        /// <summary>
        /// If this application is a game sold on Discord, this field will be the guild to which it has been linked
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        
        /// <summary>
        /// If this application is a game sold on Discord, this field will be the id of the "Game SKU" that is created, if exists
        /// </summary>
        [JsonProperty("primary_sku_id")]
        public string PrimarySkuId { get; set; }
        
        /// <summary>
        /// If this application is a game sold on Discord, this field will be the URL slug that links to the store page
        /// </summary>
        [JsonProperty("slug")]
        public string Slug { get; set; }
        
        /// <summary>
        /// If this application is a game sold on Discord, this field will be the hash of the image on store embeds
        /// </summary>
        [JsonProperty("cover_image")]
        public string CoverImage { get; set; } 
        
        /// <summary>
        /// The application's public flags
        /// </summary>
        [JsonProperty("flags")]
        public int Flags { get; set; }

        /// <summary>
        /// Returns the URL for the applications Icon
        /// </summary>
        public string GetApplicationIconUrl => DiscordCdn.GetApplicationIconUrl(Id, Icon);
        
        /// <summary>
        /// Fetch all of the global commands for your application.
        /// Returns a list of ApplicationCommand.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#get-global-application-commands">Get Global Application Commands</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with list of application commands</param>
        public void GetGlobalApplicationCommands(DiscordClient client, Action<List<ApplicationCommand>> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/commands", RequestMethod.GET, null, callback, onError);
        }
        
        /// <summary>
        /// Create a new global command.
        /// New global commands will be available in all guilds after 1 hour.
        /// Note: Creating a command with the same name as an existing command for your application will overwrite the old command.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#create-global-application-command">Create Global Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="create">Command to create</param>
        /// <param name="callback">Callback with the created command</param>
        public void CreateGlobalApplicationCommand(DiscordClient client, ApplicationCommandCreate create, Action<ApplicationCommand> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/commands", RequestMethod.POST, create, callback, onError);
        }
        
        /// <summary>
        /// Edit a global command.
        /// Updates will be available in all guilds after 1 hour.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#edit-global-application-command">Edit Global Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="update">Command Update</param>
        /// <param name="callback">Callback with updated command</param>
        public void EditGlobalApplicationCommand(DiscordClient client, ApplicationCommandCreate update, Action<ApplicationCommand> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/commands", RequestMethod.PATCH, update, callback, onError);
        }
        
        /// <summary>
        /// Deletes a global command
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#delete-global-application-command">Delete Global Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="commandId">Command to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        public void DeleteGlobalApplicationCommand(DiscordClient client, string commandId, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/commands/{commandId}", RequestMethod.PATCH, null, callback, onError);
        }

        /// <summary>
        /// Fetch all of the guild commands for your application for a specific guild.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#get-guild-application-commands">Get Guild Application Commands</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">ID of the guild to get commands for</param>
        /// <param name="callback">Callback with a list of guild application commands</param>
        public void GetGuildApplicationCommands(DiscordClient client, string guildId, Action<List<ApplicationCommand>> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/guilds/{guildId}/commands", RequestMethod.GET, null, callback, onError);
        }
        
        /// <summary>
        /// Fetch all of the guild commands for your application for a specific guild.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#get-guild-application-commands">Get Guild Application Commands</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guild">Guild to get commands for</param>
        /// <param name="callback">Callback with a list of guild application commands</param>
        public void GetGuildApplicationCommands(DiscordClient client, Guild guild, Action<List<ApplicationCommand>> callback = null, Action<RestError> onError = null)
        {
            GetGuildApplicationCommands(client, guild.Id, callback, onError);
        }
        
        /// <summary>
        /// Create a new guild command.
        /// New guild commands will be available in the guild immediately.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#create-guild-application-command">Create Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to create the command in</param>
        /// <param name="create">Command to create</param>
        /// <param name="callback">Callback with the created command</param>
        public void CreateGuildApplicationCommands(DiscordClient client, string guildId, ApplicationCommandCreate create, Action<ApplicationCommand> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/guilds/{guildId}/commands", RequestMethod.POST, create, callback, onError);
        }
        
        /// <summary>
        /// Create a new guild command.
        /// New guild commands will be available in the guild immediately.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#create-guild-application-command">Create Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guild">Guild to create the command in</param>
        /// <param name="create">Command to create</param>
        /// <param name="callback">Callback with the created command</param>
        public void CreateGuildApplicationCommands(DiscordClient client, Guild guild, ApplicationCommandCreate create, Action<ApplicationCommand> callback = null, Action<RestError> onError = null)
        {
            CreateGuildApplicationCommands(client, guild.Id, create, callback, onError);
        }
        
        /// <summary>
        /// Edit a guild command.
        /// Updates for guild commands will be available immediately.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#edit-guild-application-command">Edit Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to update the command in</param>
        /// <param name="update">Command update</param>
        /// <param name="callback">Callback with updated command</param>
        public void EditGuildApplicationCommands(DiscordClient client, string guildId, ApplicationCommand update, Action<ApplicationCommand> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/guilds/{guildId}/commands/{update.Id}", RequestMethod.PATCH, update, callback, onError);
        }
        
        /// <summary>
        /// Edit a guild command.
        /// Updates for guild commands will be available immediately.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#edit-guild-application-command">Edit Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guild">Guild to update the command in</param>
        /// <param name="update">Command update</param>
        /// <param name="callback">Callback with updated command</param>
        public void EditGuildApplicationCommands(DiscordClient client, Guild guild, ApplicationCommand update, Action<ApplicationCommand> callback = null, Action<RestError> onError = null)
        {
            EditGuildApplicationCommands(client, guild.Id, update, callback, onError);
        }
        
        /// <summary>
        /// Delete a guild command.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#delete-guild-application-command">Delete Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to delete command from</param>
        /// <param name="commandId">Command ID to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        public void DeleteGuildApplicationCommands(DiscordClient client, string guildId, string commandId, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/applications/{Id}/guilds/{guildId}/commands/{commandId}", RequestMethod.DELETE, null, callback, onError);
        }
        
        /// <summary>
        /// Delete a guild command.
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#delete-guild-application-command">Delete Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guild">Guild to delete command from</param>
        /// <param name="delete">Command to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        public void DeleteGuildApplicationCommands(DiscordClient client, Guild guild, ApplicationCommand delete, Action callback = null, Action<RestError> onError = null)
        {
            DeleteGuildApplicationCommands(client, guild.Id, delete.Id, callback, onError);
        }
        
        /// <summary>
        /// Edits the initial Interaction response
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#edit-original-interaction-response">Edit Original Interaction Response</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="interactionToken">Interaction token to edit</param>
        /// <param name="message">Updated message</param>
        /// <param name="callback">Callback with the created message</param>
        public void EditOriginalInteractionResponse(DiscordClient client, string interactionToken, WebhookEditMessage message, Action<Message> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{interactionToken}/messages/@original", RequestMethod.PATCH, message, callback, onError);
        }

        /// <summary>
        /// Edits the initial Interaction response
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#edit-original-interaction-response">Edit Original Interaction Response</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="interaction">Interaction to edit</param>
        /// <param name="message">Updated message</param>
        /// <param name="callback">Callback with the created message</param>
        public void EditOriginalInteractionResponse(DiscordClient client, Interaction interaction, WebhookEditMessage message, Action<Message> callback = null, Action<RestError> onError = null) => EditOriginalInteractionResponse(client, interaction.Token, message, callback, onError);
        
        /// <summary>
        /// Deletes the initial Interaction response
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#delete-original-interaction-response">Delete Original Interaction Response</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="interactionToken">Interaction token to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        public void DeleteOriginalInteractionResponse(DiscordClient client, string interactionToken, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{interactionToken}/messages/@original", RequestMethod.DELETE, null, callback, onError);
        }

        /// <summary>
        /// Deletes the initial Interaction response
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#delete-original-interaction-response">Delete Original Interaction Response</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="interaction">Interaction to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        public void DeleteOriginalInteractionResponse(DiscordClient client, Interaction interaction, Action callback = null, Action<RestError> onError = null) => DeleteOriginalInteractionResponse(client, interaction.Token, callback, onError);
        
        /// <summary>
        /// Create a followup message for an Interaction
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#create-followup-message">Create Followup Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="interactionToken">Interaction token to follow up</param>
        /// <param name="message">Message to follow up with</param>
        /// <param name="callback">Callback with the message</param>
        public void CreateFollowUpMessage(DiscordClient client, string interactionToken, WebhookCreateMessage message, Action<Message> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{interactionToken}", RequestMethod.POST, message, callback, onError);
        }

        /// <summary>
        /// Create a followup message for an Interaction
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#create-followup-message">Create Followup Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="interaction">Interaction to follow up</param>
        /// <param name="message">Message to follow up with</param>
        /// <param name="callback">Callback with the message</param>
        public void CreateFollowUpMessage(DiscordClient client, Interaction interaction, WebhookCreateMessage message, Action<Message> callback = null, Action<RestError> onError = null) => CreateFollowUpMessage(client, interaction.Token, message, callback, onError);
        
        /// <summary>
        /// Edits a followup message for an Interaction
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#edit-followup-message">Edit Followup Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="interactionToken">Interaction token of the follow up message</param>
        /// <param name="messageId">Message ID of the follow up message</param>
        /// <param name="edit">Updated message</param>
        /// <param name="callback">Callback with the updated message</param>
        public void EditFollowUpMessage(DiscordClient client, string interactionToken, string messageId, WebhookEditMessage edit, Action<Message> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{interactionToken}/messages/{messageId}", RequestMethod.PATCH, edit, callback, onError);
        }

        /// <summary>
        /// Deletes a followup message for an Interaction
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#delete-followup-message">Delete Followup Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="interactionToken">Interaction token of the message to delete</param>
        /// <param name="messageId">Message ID to delete</param>
        /// <param name="callback">Callback with the updated message</param>
        public void DeleteFollowUpMessage(DiscordClient client, string interactionToken, string messageId, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{interactionToken}/messages/{messageId}", RequestMethod.DELETE, null, callback, onError);
        }
    }
}