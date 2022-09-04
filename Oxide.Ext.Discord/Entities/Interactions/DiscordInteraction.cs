using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Builders.Interactions;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Interactions.Response;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Exceptions.Entities;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Json.Converters;
using Oxide.Ext.Discord.Libraries.AppCommands.Commands;
using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Entities.Interactions
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-structure">Interaction Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordInteraction
    {
        /// <summary>
        /// Id of the interaction
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }

        /// <summary>
        /// ID of the application this interaction is for
        /// </summary>
        [JsonProperty("application_id")]
        public Snowflake ApplicationId { get; set; }

        /// <summary>
        /// The type of interaction
        /// See <see cref="InteractionType"/>
        /// </summary>
        [JsonProperty("type")]
        public InteractionType Type { get; set; }

        /// <summary>
        /// Interaction data payload
        /// See <see cref="InteractionData"/>
        /// </summary>
        [JsonProperty("data")]
        public InteractionData Data { get; set; }

        /// <summary>
        /// Guild that the interaction was sent from
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake? GuildId { get; set; }

        /// <summary>
        /// Channel that the interaction was sent from
        /// </summary>
        [JsonProperty("channel_id")]
        public Snowflake? ChannelId { get; set; }

        /// <summary>
        /// Guild member data for the invoking user, including permissions
        /// </summary>
        [JsonProperty("member")]
        public GuildMember Member { get; set; }
        
        [JsonProperty("user")]
        private DiscordUser _user { get; set; }

        /// <summary>
        /// User object for the invoking user, if invoked in a DM
        /// </summary>
        public DiscordUser User => _user ?? Member?.User;

        /// <summary>
        /// Continuation token for responding to the interaction
        /// Interaction tokens are valid for 15 minutes and can be used to send followup messages but you must send an initial response within 3 seconds of receiving the event.
        /// If the 3 second deadline is exceeded, the token will be invalidated.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Read-only property, always 1
        /// </summary>
        [JsonProperty("version")]
        public int Version { get; set; }

        /// <summary>
        /// For components, the message they were attached to
        /// </summary>
        [JsonProperty("message")]
        public DiscordMessage Message { get; set; }

        /// <summary>
        /// Bitwise set of permissions the app or bot has within the channel the interaction was sent from
        /// </summary>
        [JsonConverter(typeof(PermissionFlagsStringConverter))]
        [JsonProperty("app_permissions")]
        public PermissionFlags? AppPermissions { get; set; }
        
        /// <summary>
        /// The selected language of the invoking user
        /// <a href="https://discord.com/developers/docs/dispatch/field-values#predefined-field-values-accepted-locales">Discord Locale Values</a>
        /// </summary>
        [JsonProperty("locale")]
        public string Locale { get; set; }

        /// <summary>
        /// The guild's preferred locale, if invoked in a guild
        /// <a href="https://discord.com/developers/docs/dispatch/field-values#predefined-field-values-accepted-locales">Discord Locale Values</a>
        /// </summary>
        [JsonProperty("guild_locale")]
        public string GuildLocale { get; set; }

        private InteractionDataParsed _parsed;

        /// <summary>
        /// Returns the interaction parsed args to make it easier to process that interaction.
        /// </summary>
        public InteractionDataParsed Parsed => _parsed ?? (_parsed = new InteractionDataParsed(this));

        private InteractionDataOption _focused;

        /// <summary>
        /// Returns the Focused option for Auto Complete
        /// </summary>
        public InteractionDataOption Focused => _focused ?? (_focused = GetFocusedOption());

        /// <summary>
        /// The UTC DateTime this interaction was created
        /// </summary>
        public readonly DateTime CreatedDate = DateTime.UtcNow;

        /// <summary>
        /// If CreateInteractionResponse has been successfully called for this interaction
        /// </summary>
        private bool _hasResponded;

        /// <summary>
        /// Returns a localized string for this interaction
        /// </summary>
        /// <param name="plugin">Plugin the localization is for</param>
        /// <param name="langKey">Lang Key to return</param>
        /// <returns>Localized string if it is found; Empty string otherwise</returns>
        public string GetLangMessage(Plugin plugin, string langKey) => DiscordLocale.GetDiscordInteractionLangMessage(plugin, this, langKey);
        
        /// <summary>
        /// Returns a localized string for this interaction
        /// </summary>
        /// <param name="plugin">Plugin the localization is for</param>
        /// <param name="langKey">Lang Key to return</param>
        /// <param name="args">Localization args</param>
        /// <returns>Localized string if it is found; Empty string otherwise</returns>
        public string GetLangMessage(Plugin plugin, string langKey, params object[] args) => DiscordLocale.GetDiscordInteractionLangMessage(plugin, this, langKey, args);
        
        private InteractionDataOption GetFocusedOption()
        {
            List<InteractionDataOption> options = Data.Options;
            if (options == null)
            {
                return null;
            }
            
            for (int index = 0; index < options.Count; index++)
            {
                InteractionDataOption option = options[index];
                if (option.Type == CommandOptionType.SubCommand || option.Type == CommandOptionType.SubCommandGroup)
                {
                    options = option.Options;
                    index = 0;
                    continue;
                }

                if (option.Focused.HasValue && option.Focused.Value)
                {
                    return option;
                }
            }

            return null;
        }

        internal AppCommandId GetCommandId()
        {
            string command = Data.Name;
            string group = null;
            string subCommand = null;
            string argument = Focused?.Name;
            
            List<InteractionDataOption> options = Data.Options;
            if (options != null)
            {
                for (int index = 0; index < options.Count; index++)
                {
                    InteractionDataOption option = options[index];
                    if (option.Type == CommandOptionType.SubCommandGroup)
                    {
                        group = option.Name;
                        options = option.Options;
                        index = 0;
                        continue;
                    }
                
                    if (option.Type == CommandOptionType.SubCommand)
                    {
                        subCommand = option.Name;
                        break;
                    }
                }
            }

            return new AppCommandId(command, group, subCommand, argument);
        }
        
        /// <summary>
        /// Returns a <see cref="InteractionResponseBuilder"/> for this interaction
        /// </summary>
        /// <returns></returns>
        public InteractionResponseBuilder GetResponseBuilder()
        {
            InvalidInteractionResponseException.ThrowIfAlreadyResponded(_hasResponded);
            return new InteractionResponseBuilder(this);
        }
        
        /// <summary>
        /// Returns a <see cref="InteractionFollowupBuilder"/> for this interaction
        /// </summary>
        /// <returns></returns>
        public InteractionFollowupBuilder GetFollowupBuilder()
        {
            InvalidInteractionResponseException.ThrowIfNotResponded(_hasResponded);
            return new InteractionFollowupBuilder(this);
        }
        
        /// <summary>
        /// Returns a <see cref="InteractionAutoCompleteBuilder"/> for this interaction
        /// </summary>
        /// <returns></returns>
        public InteractionAutoCompleteBuilder GetAutoCompleteBuilder()
        {
            InvalidInteractionResponseException.ThrowIfAlreadyResponded(_hasResponded);
            return new InteractionAutoCompleteBuilder(this);
        }
        
        /// <summary>
        /// Returns a <see cref="InteractionAutoCompleteBuilder"/> for this interaction
        /// </summary>
        /// <returns></returns>
        public InteractionModalBuilder GetModalBuilder()
        {
            InvalidInteractionResponseException.ThrowIfAlreadyResponded(_hasResponded);
            return new InteractionModalBuilder(this);
        }

        /// <summary>
        /// Create a response to an Interaction from the gateway.
        /// See <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response">Create Interaction Response</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="response">Response to respond with</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateInteractionResponse(DiscordClient client, BaseInteractionResponse response, Action callback = null, Action<RequestError> error = null)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));
            InvalidInteractionResponseException.ThrowIfAlreadyResponded(_hasResponded);
            InvalidInteractionResponseException.ThrowIfInitialResponseTimeElapsed(CreatedDate);

            _hasResponded = true;
            client.Bot.Rest.CreateRequest(client, $"interactions/{Id}/{Token}/callback", RequestMethod.POST, response, callback, error);
        }

        /// <summary>
        /// Create a response to an Interaction from the gateway.
        /// See <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response">Create Interaction Response</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="type">Type of the interaction response</param>
        /// <param name="response">Interaction Callback Message Data</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateInteractionResponse(DiscordClient client, InteractionResponseType type, InteractionCallbackData response, Action callback = null, Action<RequestError> error = null)
        {
            InteractionResponse data = new InteractionResponse(type, response);
            CreateInteractionResponse(client, data, callback, error);
        }

        public void CreateTemplateInteractionResponse(DiscordClient client, Plugin plugin, InteractionResponseType type, string templateKey, InteractionCallbackData message = null, PlaceholderData placeholders = null, Action callback = null, Action<RequestError> error = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateKey)) throw new ArgumentNullException(nameof(templateKey));
            
            DiscordExtension.DiscordMessageTemplates.GetMessageTemplateInternal(plugin, templateKey, this).OnSuccess(template =>
            {
                template.ToPlaceholderMessageAsyncInternal(placeholders, message).OnSuccess(response =>
                {
                    CreateInteractionResponse(client, type, response, callback, error);
                });
            });
        }
        
        public void CreateTemplateModalResponse(DiscordClient client, Plugin plugin, string templateKey, InteractionModalMessage message = null, PlaceholderData placeholders = null, Action callback = null, Action<RequestError> error = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateKey)) throw new ArgumentNullException(nameof(templateKey));
            
            DiscordExtension.DiscordModalTemplates.GetModalTemplateInternal(plugin, templateKey, this).OnSuccess(template =>
            {
                template.ToPlaceholderMessageAsyncInternal(placeholders, message).OnSuccess(response =>
                {
                    CreateInteractionResponse(client, response, callback, error);
                });
            });
        }

        /// <summary>
        /// Create a response to an Interaction from the gateway.
        /// See <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response">Create Interaction Response</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="type">Type of the interaction response</param>
        /// <param name="builder">Builder for this response</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateInteractionResponse(DiscordClient client, InteractionResponseType type, InteractionResponseBuilder builder, Action callback = null, Action<RequestError> error = null)
        {
            InteractionResponse data = new InteractionResponse(type, builder.Build());
            CreateInteractionResponse(client, data, callback, error);
        }
        
        /// <summary>
        /// Create a Auto Complete response to an Interaction
        /// See <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response">Create Interaction Response</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="message">Message for this response</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateInteractionResponse(DiscordClient client, InteractionAutoCompleteMessage message, Action callback = null, Action<RequestError> error = null)
        {
            InteractionAutoCompleteResponse data = new InteractionAutoCompleteResponse(message);
            CreateInteractionResponse(client, data, callback, error);
        }
        
        /// <summary>
        /// Create a Auto Complete response to an Interaction
        /// See <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response">Create Interaction Response</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="builder">Auto Complete Builder for this response</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateInteractionResponse(DiscordClient client, InteractionAutoCompleteBuilder builder, Action callback = null, Action<RequestError> error = null)
        {
            CreateInteractionResponse(client, builder.Build(), callback, error);
        }
        
        /// <summary>
        /// Create a Modal response to an Interaction
        /// See <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response">Create Interaction Response</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="message">Message for this response</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateInteractionResponse(DiscordClient client, InteractionModalMessage message, Action callback = null, Action<RequestError> error = null)
        {
            InteractionModalResponse data = new InteractionModalResponse(message);
            CreateInteractionResponse(client, data, callback, error);
        }
        
        /// <summary>
        /// Create a Modal response to an Interaction
        /// See <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response">Create Interaction Response</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="builder">Modal Builder for this response</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateInteractionResponse(DiscordClient client, InteractionModalBuilder builder, Action callback = null, Action<RequestError> error = null)
        {
            CreateInteractionResponse(client, builder.Build(), callback, error);
        }

        /// <summary>
        /// Edits the initial Interaction response
        /// See <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#edit-original-interaction-response">Edit Original Interaction Response</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// /// <param name="message">Updated message</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditOriginalInteractionResponse(DiscordClient client, DiscordMessage message, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            InvalidInteractionResponseException.ThrowIfNotResponded(_hasResponded);
            InvalidInteractionResponseException.ThrowIfMaxResponseTimeElapsed(CreatedDate);
            client.Bot.Rest.CreateRequest(client, $"webhooks/{ApplicationId}/{Token}/messages/@original", RequestMethod.PATCH, message, callback, error);
        }
        
        public void EditTemplateOriginalInteractionResponse(DiscordClient client, Plugin plugin, string templateKey, DiscordMessage message = null, PlaceholderData placeholders = null, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateKey)) throw new ArgumentNullException(nameof(templateKey));
            
            DiscordExtension.DiscordMessageTemplates.GetMessageTemplateInternal(plugin, templateKey, this).OnSuccess(template =>
            {
                template.ToPlaceholderMessageAsyncInternal(placeholders, message).OnSuccess(response =>
                {
                    EditOriginalInteractionResponse(client, response, callback, error);
                });
            });
        }

        /// <summary>
        /// Deletes the initial Interaction response
        /// See <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#delete-original-interaction-response">Delete Original Interaction Response</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteOriginalInteractionResponse(DiscordClient client, Action callback = null, Action<RequestError> error = null)
        {
            InvalidInteractionResponseException.ThrowIfNotResponded(_hasResponded);
            InvalidInteractionResponseException.ThrowIfMaxResponseTimeElapsed(CreatedDate);
            client.Bot.Rest.CreateRequest(client, $"webhooks/{ApplicationId}/{Token}/messages/@original", RequestMethod.DELETE, null, callback, error);
        }

        /// <summary>
        /// Create a followup message for an Interaction
        /// See <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#create-followup-message">Create Followup Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="message">Message to follow up with</param>
        /// <param name="callback">Callback with the message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateFollowUpMessage(DiscordClient client, CommandFollowupCreate message, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            InvalidInteractionResponseException.ThrowIfNotResponded(_hasResponded);
            InvalidInteractionResponseException.ThrowIfMaxResponseTimeElapsed(CreatedDate);
            client.Bot.Rest.CreateRequest(client, $"webhooks/{ApplicationId}/{Token}", RequestMethod.POST, message, callback, error);
        }
        
        /// <summary>
        /// Create a followup message for an Interaction
        /// See <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#create-followup-message">Create Followup Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="builder">Builder for the follow up</param>
        /// <param name="callback">Callback with the message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateFollowUpMessage(DiscordClient client, InteractionFollowupBuilder builder, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            CreateFollowUpMessage(client, builder.Build(), callback, error);
        }

        /// <summary>
        /// Edits a followup message for an Interaction
        /// See <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#edit-followup-message">Edit Followup Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageId">Message ID of the follow up message</param>
        /// <param name="edit">Updated message</param>
        /// <param name="callback">Callback with the updated message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditFollowUpMessage(DiscordClient client, Snowflake messageId, CommandFollowupUpdate edit, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            InvalidInteractionResponseException.ThrowIfNotResponded(_hasResponded);
            InvalidInteractionResponseException.ThrowIfMaxResponseTimeElapsed(CreatedDate);
            InvalidSnowflakeException.ThrowIfInvalid(messageId, nameof(messageId));
            client.Bot.Rest.CreateRequest(client, $"webhooks/{ApplicationId}/{Token}/messages/{messageId}", RequestMethod.PATCH, edit, callback, error);
        }

        /// <summary>
        /// Deletes a followup message for an Interaction
        /// See <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#delete-followup-message">Delete Followup Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageId">Message ID to delete</param>
        /// <param name="callback">Callback with the updated message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteFollowUpMessage(DiscordClient client, Snowflake messageId, Action callback = null, Action<RequestError> error = null)
        {
            InvalidInteractionResponseException.ThrowIfNotResponded(_hasResponded);
            InvalidInteractionResponseException.ThrowIfMaxResponseTimeElapsed(CreatedDate);
            InvalidSnowflakeException.ThrowIfInvalid(messageId, nameof(messageId));
            client.Bot.Rest.CreateRequest(client, $"webhooks/{ApplicationId}/{Token}/messages/{messageId}", RequestMethod.DELETE, null, callback, error);
        }
    }
}