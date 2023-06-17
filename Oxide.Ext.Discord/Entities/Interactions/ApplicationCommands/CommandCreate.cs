using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Json.Converters;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/application-commands#create-global-application-command-json-params">Application Command Create</a>
    /// Represents <a href="https://discord.com/developers/docs/interactions/application-commands#create-guild-application-command-json-params">Application Command Create</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class CommandCreate : IDiscordValidation
    {
        /// <summary>
        /// 1-32 lowercase character name matching ^[-_\p{L}\p{N}\p{sc=Deva}\p{sc=Thai}]{1,32}$
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

        /// <summary>
        /// Set of permissions represented as a bit set
        /// </summary>
        [JsonProperty("default_member_permissions")]
        [JsonConverter(typeof(PermissionFlagsStringConverter))]
        public PermissionFlags DefaultMemberPermissions { get; set; }

        /// <summary>
        /// Indicates whether the command is available in DMs with the app, only for globally-scoped commands. By default, commands are visible.
        /// </summary>
        [JsonProperty("dm_permission")]
        public bool? DmPermission { get; set; }

        /// <summary>
        /// Indicates whether the command is age-restricted
        /// </summary>
        [JsonProperty("nsfw")]
        public bool? Nsfw { get; set; }
        
        /// <summary>
        /// The <see cref="ApplicationCommandType"/> of the command
        /// </summary>
        [JsonProperty("type")]
        public ApplicationCommandType Type { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CommandCreate() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public CommandCreate(string name, string description, ApplicationCommandType type = ApplicationCommandType.ChatInput, List<CommandOption> options = null)
        {
            Name = name;
            Description = description;
            Type = type;
            Options = options;
            NameLocalizations = new Hash<string, string>();
            DescriptionLocalizations = new Hash<string, string>();
        }
        
        /// <inheritdoc/>
        public void Validate()
        {
            InvalidApplicationCommandException.ThrowIfInvalidName(Name, false);
            InvalidApplicationCommandException.ThrowIfInvalidDescription(Description, Type);
        }
    }
}