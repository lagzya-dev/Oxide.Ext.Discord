using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Exceptions.Entities;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-structure">ApplicationCommand</a>
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
        /// The type of command, defaults to 1
        /// </summary>
        [JsonProperty("type")]
        public ApplicationCommandType? Type { get; set; }
        
        /// <summary>
        /// ID of the parent application
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
        /// Localization dictionary for the name field. Values follow the same restrictions as name
        /// </summary>
        [JsonProperty("name_localizations")]
        public Hash<string, string> NameLocalizations { get; set; }
        
        /// <summary>
        /// Description of the command (1-100 characters)
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        
        /// <summary>
        /// Localization dictionary for the description field. Values follow the same restrictions as description
        /// </summary>
        [JsonProperty("description_localizations")]
        public Hash<string, string> DescriptionLocalizations { get; set; }
        
        /// <summary>
        /// The parameters for the command
        /// See <see cref="CommandOption"/>
        /// </summary>
        [JsonProperty("options")]
        public List<CommandOption> Options { get; set; }

        [JsonProperty("default_member_permissions")]
        private string _defaultMemberPermissions;

        /// <summary>
        /// Set of permissions represented as a bit set
        /// </summary>
        public PermissionFlags DefaultMemberPermissions
        {
            get => !string.IsNullOrEmpty(_defaultMemberPermissions) ? (PermissionFlags)ulong.Parse(_defaultMemberPermissions) : default(PermissionFlags);
            set => _defaultMemberPermissions = ((ulong)value).ToString();
        }
        
        /// <summary>
        /// Indicates whether the command is available in DMs with the app, only for globally-scoped commands. By default, commands are visible.
        /// </summary>
        [JsonProperty("dm_permission")]
        public bool? DmPermission { get; set; }

        /// <summary>
        /// Auto incrementing version identifier updated during substantial record changes
        /// </summary>
        [JsonProperty("version")]
        public Snowflake Version { get; set; }
        
        /// <summary>
        /// Edit a command.
        /// Updates will be available in all guilds after 1 hour.
        /// See <a href="https://discord.com/developers/docs/interactions/application-commands#edit-global-application-command">Edit Global Application Command</a>
        /// See <a href="https://discord.com/developers/docs/interactions/application-commands#edit-guild-application-command">Edit Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="update">Command Update</param>
        /// <param name="callback">Callback with updated command</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void Edit(DiscordClient client, CommandUpdate update, Action<DiscordApplicationCommand> callback = null, Action<RequestError> error = null)
        {
            if (update == null) throw new ArgumentNullException(nameof(update));
            if (GuildId.HasValue)
            {
                client.Bot.Rest.DoRequest(client,$"/applications/{ApplicationId}/guilds/{GuildId}/commands/{Id}", RequestMethod.PATCH, update, callback, error);
                return;
            }
            
            client.Bot.Rest.DoRequest(client,$"/applications/{ApplicationId}/commands/{Id}", RequestMethod.PATCH, update, callback, error);
        }
        
        /// <summary>
        /// Deletes a command
        /// See <a href="https://discord.com/developers/docs/interactions/application-commands#delete-global-application-command">Delete Global Application Command</a>
        /// See <a href="https://discord.com/developers/docs/interactions/application-commands#delete-guild-application-command">Delete Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void Delete(DiscordClient client, Action callback = null, Action<RequestError> error = null)
        {
            if (GuildId.HasValue)
            {
                client.Bot.Rest.DoRequest(client,$"/applications/{ApplicationId}/guilds/{GuildId}/commands/{Id}", RequestMethod.DELETE, null, callback, error);
                return;
            }
            
            client.Bot.Rest.DoRequest(client,$"/applications/{ApplicationId}/commands/{Id}", RequestMethod.DELETE, null, callback, error);
        }

        /// <summary>
        /// Fetches command permissions for a specific command for your application in a guild. Returns a GuildApplicationCommandPermissions object.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID of the guild to get permissions for</param>
        /// <param name="callback">Callback with the permissions for the command</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GetPermissions(DiscordClient client, Snowflake guildId, Action<GuildCommandPermissions> callback = null, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(guildId, nameof(guildId));
            client.Bot.Rest.DoRequest(client,$"/applications/{ApplicationId}/guilds/{guildId}/commands/{Id}/permissions", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Edits command permissions for a specific command for your application in a guild.
        /// Warning: This endpoint will overwrite existing permissions for the command in that guild
        /// Warning: Deleting or renaming a command will permanently delete all permissions for that command
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID of the guild to edit permissions for</param>
        /// <param name="permissions">List of permissions for the command</param>
        /// <param name="callback">Callback with the list of permissions</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditPermissions(DiscordClient client, Snowflake guildId, List<CommandPermissions> permissions, Action callback = null, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(guildId, nameof(guildId));
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                ["permissions"] = permissions
            };
            
            client.Bot.Rest.DoRequest(client,$"/applications/{ApplicationId}/guilds/{guildId}/commands/{Id}/permissions", RequestMethod.PUT, data, callback, error);
        }
    }
}