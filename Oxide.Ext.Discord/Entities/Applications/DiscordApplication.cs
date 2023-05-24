using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Applications.RoleConnection;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Teams;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Exceptions.Entities;
using Oxide.Ext.Discord.Exceptions.Entities.Applications;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Interfaces.Logging;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Promise;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Entities.Applications
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/application#application-object">Application Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordApplication : IDebugLoggable
    {
        /// <summary>
        /// The id of the app
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
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
        /// The url of the app's terms of service
        /// </summary>
        [JsonProperty("terms_of_service_url")]
        public string TermsOfServiceUrl { get; set; }
        
        /// <summary>
        /// The url of the app's privacy policy
        /// </summary>
        [JsonProperty("privacy_policy_url")]
        public string PrivacyPolicyUrl { get; set; }
        
        /// <summary>
        /// Partial user object containing info on the owner of the application
        /// </summary>
        [JsonProperty("owner")]
        public DiscordUser Owner { get; set; }

        /// <summary>
        /// The hex encoded key for verification in interactions and the GameSDK's GetTicket
        /// </summary>
        [JsonProperty("verify_key")]
        public string Verify { get; set; }
        
        /// <summary>
        /// If the application belongs to a team, this will be a list of the members of that team
        /// </summary>
        [JsonProperty("team")]
        public DiscordTeam Team { get; set; }
        
        /// <summary>
        /// If this application is a game sold on Discord, this field will be the guild to which it has been linked
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake? GuildId { get; set; }
        
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
        public ApplicationFlags? Flags { get; set; }
        
        /// <summary>
        /// Up to 5 tags describing the content and functionality of the application
        /// </summary>
        [JsonProperty("tags")]
        public List<string> Tags { get; set; } 
        
        /// <summary>
        /// Settings for the application's default in-app authorization link, if enabled
        /// </summary>
        [JsonProperty("install_params")]
        public InstallParams InstallParams { get; set; } 
        
        /// <summary>
        /// The application's default custom authorization link, if enabled
        /// </summary>
        [JsonProperty("custom_install_url")]
        public string CustomInstallUrl { get; set; } 
        
        /// <summary>
        /// The application's role connection verification entry point, which when configured will render the app as a verification method in the guild role verification configuration
        /// </summary>
        [JsonProperty("role_connections_verification_url")]
        public string RoleConnectionsVerificationUrl { get; set; } 

        /// <summary>
        /// Returns the URL for the applications Icon
        /// </summary>
        public string GetApplicationIconUrl => DiscordCdn.GetApplicationIconUrl(Id, Icon);
        
        /// <summary>
        /// Returns the URL for the application cover
        /// </summary>
        public string GetApplicationCoverUrl => DiscordCdn.GetApplicationIconUrl(Id, CoverImage);
        
        /// <summary>
        /// Returns if the given application has the passed in application flag
        /// If <see cref="Flags"/>  is null false is returned
        /// </summary>
        /// <param name="flag">Flag to compare against</param>
        /// <returns>True of application has flag; False Otherwise</returns>
        public bool HasApplicationFlag(ApplicationFlags flag)
        {
            return Flags.HasValue && (Flags.Value & flag) == flag;
        }
        
        /// <summary>
        /// Returns if the given application has any of the passed in application flags
        /// If <see cref="Flags"/> is null false is returned
        /// </summary>
        /// <param name="flag">Flag to compare against</param>
        /// <returns>True of application has flag; False Otherwise</returns>
        public bool HasAnyApplicationFlags(ApplicationFlags flag)
        {
            return Flags.HasValue && (Flags.Value & flag) != 0;
        }

        /// <summary>
        /// Fetch all of the global commands for your application.
        /// Returns a list of ApplicationCommand.
        /// See <a href="https://discord.com/developers/docs/interactions/application-commands#get-global-application-commands">Get Global Application Commands</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="withLocalizations">Include Command Localizations</param>
        /// <param name="callback">Callback with list of application commands</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IDiscordPromise<List<DiscordApplicationCommand>> GetGlobalCommands(DiscordClient client, bool withLocalizations = false)
        {
            return client.Bot.Rest.Get<List<DiscordApplicationCommand>>(client,$"applications/{Id}/commands?with_localizations={withLocalizations}");
        }

        /// <summary>
        /// Fetch global command by ID
        /// See <a href="https://discord.com/developers/docs/interactions/application-commands#get-global-application-command">Get Global Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="commandId">ID of command to get</param>
        /// <param name="callback">Callback with list of application commands</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IDiscordPromise<DiscordApplicationCommand> GetGlobalCommand(DiscordClient client, Snowflake commandId)
        {
            InvalidSnowflakeException.ThrowIfInvalid(commandId, nameof(commandId));
            return client.Bot.Rest.Get<DiscordApplicationCommand>(client,$"applications/{Id}/commands/{commandId}");
        }
        
        /// <summary>
        /// Create a new global command.
        /// New global commands will be available in all guilds after 1 hour.
        /// Note: Creating a command with the same name as an existing command for your application will overwrite the old command.
        /// See <a href="https://discord.com/developers/docs/interactions/application-commands#create-global-application-command">Create Global Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="create">Command to create</param>
        /// <param name="callback">Callback with the created command</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IDiscordPromise<DiscordApplicationCommand> CreateGlobalCommand(DiscordClient client, CommandCreate create)
        {
            if (create == null) throw new ArgumentNullException(nameof(create));
            return client.Bot.Rest.Post<DiscordApplicationCommand>(client,$"applications/{Id}/commands", create);
        }

        /// <summary>
        /// Takes a list of application commands, overwriting existing commands that are registered globally for this application. Updates will be available in all guilds after 1 hour.
        /// See <a href="https://discord.com/developers/docs/interactions/application-commands#bulk-overwrite-global-application-commands">Bulk Overwrite Global Application Commands</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="commands">List of commands to overwrite</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IDiscordPromise<List<DiscordApplicationCommand>> BulkOverwriteGlobalCommands(DiscordClient client, List<CommandBulkOverwrite> commands)
        {
            if (commands == null) throw new ArgumentNullException(nameof(commands));
            return client.Bot.Rest.Put<List<DiscordApplicationCommand>>(client,$"applications/{Id}/commands", commands);
        }

        /// <summary>
        /// Fetch all of the guild commands for your application for a specific guild.
        /// See <a href="https://discord.com/developers/docs/interactions/application-commands#get-guild-application-commands">Get Guild Application Commands</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">ID of the guild to get commands for</param>
        /// <param name="withLocalizations">Include Command Localizations</param>
        /// <param name="callback">Callback with a list of guild application commands</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IDiscordPromise<List<DiscordApplicationCommand>> GetGuildCommands(DiscordClient client, Snowflake guildId, bool withLocalizations = false)
        {
            InvalidSnowflakeException.ThrowIfInvalid(guildId, nameof(guildId));
            return client.Bot.Rest.Get<List<DiscordApplicationCommand>>(client,$"applications/{Id}/guilds/{guildId}/commands?with_localizations={withLocalizations}");
        }

        /// <summary>
        /// Get guild command by Id
        /// See <a href="https://discord.com/developers/docs/interactions/application-commands#get-guild-application-command">Get Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">ID of the guild to get commands for</param>
        /// <param name="commandId">ID of the command to get</param>
        /// <param name="callback">Callback with a list of guild application commands</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IDiscordPromise<DiscordApplicationCommand> GetGuildCommand(DiscordClient client, Snowflake guildId, Snowflake commandId)
        {
            InvalidSnowflakeException.ThrowIfInvalid(guildId, nameof(guildId));
            InvalidSnowflakeException.ThrowIfInvalid(commandId, nameof(commandId));
            return client.Bot.Rest.Get<DiscordApplicationCommand>(client,$"applications/{Id}/guilds/{guildId}/commands/{commandId}");
        }

        /// <summary>
        /// Create a new guild command.
        /// New guild commands will be available in the guild immediately.
        /// See <a href="https://discord.com/developers/docs/interactions/application-commands#create-guild-application-command">Create Guild Application Command</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to create the command in</param>
        /// <param name="create">Command to create</param>
        /// <param name="callback">Callback with the created command</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IDiscordPromise<DiscordApplicationCommand> CreateGuildCommand(DiscordClient client, Snowflake guildId, CommandCreate create)
        {
            if (create == null) throw new ArgumentNullException(nameof(create));
            InvalidSnowflakeException.ThrowIfInvalid(guildId, nameof(guildId));
            return client.Bot.Rest.Post<DiscordApplicationCommand>(client,$"applications/{Id}/guilds/{guildId}/commands", create);
        }

        /// <summary>
        /// Fetches command permissions for all commands for your application in a guild. Returns an array of GuildApplicationCommandPermissions objects.
        /// See <a href="https://discord.com/developers/docs/interactions/application-commands#get-guild-application-command-permissions">Get Guild Application Command Permissions</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to get the permissions from</param>
        /// <param name="callback">Callback with the list of permissions</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IDiscordPromise<List<GuildCommandPermissions>> GetGuildCommandPermissions(DiscordClient client, Snowflake guildId)
        {
            InvalidSnowflakeException.ThrowIfInvalid(guildId, nameof(guildId));
            return client.Bot.Rest.Get<List<GuildCommandPermissions>>(client,$"applications/{Id}/guilds/{guildId}/commands/permissions");
        }
        
        /// <summary>
        /// Returns all commands registered to this application
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the list of all commands</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IDiscordPromise<List<DiscordApplicationCommand>> GetAllCommands(DiscordClient client, bool withLocalizations = false)
        {
            bool globalDone = false;
            Hash<Snowflake, bool> guildsDone = new Hash<Snowflake, bool>();
            List<DiscordApplicationCommand> commands = new List<DiscordApplicationCommand>();
            DiscordPromise<List<DiscordApplicationCommand>> promise = DiscordPromise<List<DiscordApplicationCommand>>.Create();

            //TODO: Fix Design as a failure could call the promise multiple times
            GetGlobalCommands(client, withLocalizations).Done(globalCommands =>
            {
                commands.AddRange(globalCommands);
                globalDone = true;
                if (globalDone && guildsDone.Values.All(g => g))
                {
                    promise.Resolve(commands);
                }
            }, ex => promise.Fail(ex));
            
            foreach (Snowflake guildId in client.Bot.Servers.Keys)
            {
                guildsDone[guildId] = false;
                GetGuildCommands(client, guildId, withLocalizations).Done(guildCommands =>
                {
                    commands.AddRange(guildCommands);
                    guildsDone[guildId] = true;
                    if (globalDone && guildsDone.Values.All(g => g))
                    {
                        promise.Resolve(commands);
                    }
                }, ex => promise.Fail(ex));
            }
            
            return promise;
        }
        
        /// <summary>
        /// Returns a list of application role connection metadata objects for the given application.
        /// See <a href="https://discord.com/developers/docs/resources/application-role-connection-metadata#get-application-role-connection-metadata-records">Get Application Role Connection Metadata Records</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the list of <see cref="ApplicationRoleConnectionMetadata"/></param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IDiscordPromise<List<ApplicationRoleConnectionMetadata>> GetApplicationRoleConnectionMetadataRecords(DiscordClient client)
        {
            return client.Bot.Rest.Get<List<ApplicationRoleConnectionMetadata>>(client,$"applications/{Id}/role-connections/metadata");
        }

        /// <summary>
        /// Updates and returns a list of application role connection metadata objects for the given application.
        /// See <a href="https://discord.com/developers/docs/resources/application-role-connection-metadata#update-application-role-connection-metadata-records">Update Application Role Connection Metadata Records</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="records">The records to update on the application</param>
        /// <param name="callback">Callback with the updated list of <see cref="ApplicationRoleConnectionMetadata"/></param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IDiscordPromise<List<ApplicationRoleConnectionMetadata>> UpdateApplicationRoleConnectionMetadataRecords(DiscordClient client, List<ApplicationRoleConnectionMetadata> records)
        {
            DiscordApplicationException.ThrowIfInvalidApplicationRoleConnectionMetadataLength(records);
            return client.Bot.Rest.Put<List<ApplicationRoleConnectionMetadata>>(client,$"applications/{Id}/role-connections/metadata", records);
        }

        public void LogDebug(DebugLogger logger)
        {
            logger.AppendField("ID", Id);
            logger.AppendField("Name", Name);
            logger.AppendFieldEnum("Flags", Flags ?? ApplicationFlags.None);
        }
    }
}