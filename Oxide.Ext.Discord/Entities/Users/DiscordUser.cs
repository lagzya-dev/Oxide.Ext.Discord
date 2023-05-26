using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Cache.Entities;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Entities.Users.Connections;
using Oxide.Ext.Discord.Exceptions.Entities;
using Oxide.Ext.Discord.Exceptions.Entities.Channels;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Interfaces.Logging;
using Oxide.Ext.Discord.Interfaces.Promises;
using Oxide.Ext.Discord.Libraries.Langs;
using Oxide.Ext.Discord.Libraries.Linking;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Promises;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Entities.Users
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/user#user-object">User Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordUser : ISnowflakeEntity, IDiscordCacheable<DiscordUser>, IDebugLoggable
    {
        #region Discord Fields
        /// <summary>
        /// The user's id
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }

        /// <summary>
        /// The user's username, not unique across the platform
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }
        
        [JsonProperty("global_name")]
        public string GlobalName { get; set; }

        /// <summary>
        /// The user's 4-digit discord-tag
        /// </summary>
        [JsonProperty("discriminator")]
        [Obsolete("This field will be removed by discord in a future API version")]
        public string Discriminator { get; set; }

        /// <summary>
        /// The user's avatar hash
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        /// <summary>
        /// Whether the user belongs to an OAuth2 application
        /// </summary>
        [JsonProperty("bot")]
        public bool? Bot { get; set; }

        /// <summary>
        /// Whether the user is an Official Discord System user (part of the urgent message system)
        /// </summary>
        [JsonProperty("system")]
        public bool? System { get; set; }

        /// <summary>
        /// Whether the user has two factor enabled on their account
        /// </summary>
        [JsonProperty("mfa_enabled")]
        public bool? MfaEnabled { get; set; }
        
        /// <summary>
        /// The user's banner, or null if unset
        /// </summary>
        [JsonProperty("banner")]
        public string Banner { get; set; }
        
        /// <summary>
        /// The user's banner color encoded as an integer representation of hexadecimal color code
        /// </summary>
        [JsonProperty("accent_color")]
        public DiscordColor? AccentColor { get; set; }
        
        /// <summary>
        /// The user's chosen language option
        /// </summary>
        [JsonProperty("locale")]
        public string Locale { get; set; }

        /// <summary>
        /// Whether the email on this account has been verified
        /// </summary>
        [JsonProperty("verified")]
        public bool? Verified { get; set; }

        /// <summary>
        /// The user's email
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// The flags on a user's account
        /// <see cref="UserFlags"/>
        /// </summary>
        [JsonProperty("flags")]
        public UserFlags? Flags { get; set; }

        /// <summary>
        /// The type of Nitro subscription on a user's account
        /// <see cref="UserPremiumType"/>
        /// </summary>
        [JsonProperty("premium_type")]
        public UserPremiumType? PremiumType { get; set; }

        /// <summary>
        /// The public flags on a user's account
        /// <see cref="UserFlags"/>
        /// </summary>
        [JsonProperty("public_flags")]
        public UserFlags? PublicFlags { get; set; }
        #endregion

        #region Helper Properties
        /// <summary>
        /// Returns a string to mention this users nickname in a message
        /// </summary>
        public string Mention => DiscordFormatting.MentionUser(Id);

        /// <summary>
        /// Default Avatar Url for the User
        /// </summary>
        public string GetDefaultAvatarUrl => DiscordCdn.GetUserDefaultAvatarUrl(Discriminator);

        /// <summary>
        /// Avatar Url for the user
        /// </summary>
        public string GetAvatarUrl => DiscordCdn.GetUserAvatarUrl(Id, Avatar);
        
        /// <summary>
        /// Banner Url for the user
        /// </summary>
        public string GetBannerUrl => DiscordCdn.GetUserBanner(Id, Banner);

        /// <summary>
        /// Returns the username#discriminator for the user
        /// </summary>
        public string FullUserName => Discriminator == "0" ? Username : $"{Username}#{Discriminator}";

        public string DisplayName => GlobalName ?? Username;

        /// <summary>
        /// Returns if the DiscordUser is a bot
        /// </summary>
        public bool IsBot => Bot.HasValue && Bot.Value;
        
        /// <summary>
        /// Returns if the DiscordUser is a system user
        /// </summary>
        public bool IsSystem => System.HasValue && System.Value;

        /// <summary>
        /// Returns the IPlayer for the discord user if linked; null otherwise
        /// </summary>
        public IPlayer Player => DiscordLink.Instance.GetPlayer(Id);
        #endregion

        #region API Methods
        /// <summary>
        /// Returns the currently logged in user account
        /// See <a href="https://discord.com/developers/docs/resources/user#get-current-user">Get Current User</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the logged in user</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static IPromise<DiscordUser> GetCurrentUser(DiscordClient client)
        {
            return client.Bot.Rest.Get<DiscordUser>(client,"users/@me");
        }

        /// <summary>
        /// Returns the user for the given user Id
        /// See <a href="https://discord.com/developers/docs/resources/user#get-user">Get User</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID to lookup</param>
        /// <param name="callback">Callback with the looked up user</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static IPromise<DiscordUser> GetUser(DiscordClient client, Snowflake userId)
        {
            InvalidSnowflakeException.ThrowIfInvalid(userId, nameof(userId));
            return client.Bot.Rest.Get<DiscordUser>(client,$"users/{userId}");
        }

        /// <summary>
        /// Send a message to a user in a direct message channel
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="message">Message to be created</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IPromise<DiscordMessage> SendDirectMessage(DiscordClient client, MessageCreate message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            return CreateDirectMessageChannel(client, Id)
                .Then(channel => channel.CreateMessage(client, message));
        }

        /// <summary>
        /// Send a message to a user in a direct message channel
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="message">Content of the message</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IPromise<DiscordMessage> SendDirectMessage(DiscordClient client, string message)
        {
            return SendDirectMessage(client, new MessageCreate{Content = message});
        }

        /// <summary>
        /// Send a message to a user in a direct message channel
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="embeds">Embeds to be send in the message</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IPromise<DiscordMessage> SendDirectMessage(DiscordClient client, List<DiscordEmbed> embeds)
        {
            if (embeds == null) throw new ArgumentNullException(nameof(embeds));
            return SendDirectMessage(client, new MessageCreate{Embeds = embeds});
        }

        /// <summary>
        /// Send a message in a DM to the user using a localized message template
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="plugin">Plugin for the template</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="language">Oxide language to use</param>
        /// <param name="message">Message to use (optional)</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        /// <param name="callback">Callback when the message is created</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IPromise<DiscordMessage> SendTemplateDirectMessage(DiscordClient client, Plugin plugin, string templateName, string language = DiscordLang.DefaultOxideLanguage, MessageCreate message = null, PlaceholderData placeholders = null)
        {
            MessageCreate template = DiscordExtension.DiscordMessageTemplates.GetLocalizedTemplate(plugin, templateName, language).ToMessage(placeholders, message);
            return SendDirectMessage(client, template);
        }
        
        /// <summary>
        /// Reply to a message using a global message template
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="plugin">Plugin for the template</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="message">Message to use (optional)</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        /// <param name="callback">Callback when the message is created</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IPromise<DiscordMessage> SendGlobalTemplateDirectMessage(DiscordClient client, Plugin plugin, string templateName, MessageCreate message = null, PlaceholderData placeholders = null)
        {
            MessageCreate template = DiscordExtension.DiscordMessageTemplates.GetGlobalTemplate(plugin, templateName).ToMessage(placeholders, message);
            return SendDirectMessage(client, template);
        }

        /// <summary>
        /// Modify the currently logged in user
        /// See <a href="https://discord.com/developers/docs/resources/user#modify-current-user">Modify Current User</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="current">The updated current user information</param>
        /// <param name="callback">Callback with the updated user</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IPromise<DiscordUser> ModifyCurrentUser(DiscordClient client, UserModifyCurrent current)
        {
            if (current == null) throw new ArgumentNullException(nameof(current));
            return client.Bot.Rest.Patch<DiscordUser>(client,"users/@me", current);
        }

        /// <summary>
        /// Returns the guilds for the currently logged in user
        /// See <a href="https://discord.com/developers/docs/resources/user#get-current-user-guilds">Get Current User Guilds</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="request">Request parameters for filtering guilds</param>
        /// <param name="callback">Callback with the list of guilds</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IPromise<List<DiscordGuild>> GetCurrentUserGuilds(DiscordClient client, UserGuildsRequest request = null)
        {
            return client.Bot.Rest.Get<List<DiscordGuild>>(client,$"users/@me/guilds{request?.ToQueryString()}");
        }

        /// <summary>
        /// Leave the guild that the currently logged in user is in
        /// See <a href="https://discord.com/developers/docs/resources/user#leave-guild">Leave Guild</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to leave</param>
        /// <param name="callback">callback when the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IPromise LeaveGuild(DiscordClient client, Snowflake guildId)
        {
            InvalidSnowflakeException.ThrowIfInvalid(guildId, nameof(guildId));
            return client.Bot.Rest.Delete(client,$"users/@me/guilds/{guildId}");
        }

        /// <summary>
        /// Create a Direct Message to the current User
        /// See <a href="https://discord.com/developers/docs/resources/user#create-dm">Create DM</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the direct message channel</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IPromise<DiscordChannel> CreateDirectMessageChannel(DiscordClient client) => CreateDirectMessageChannel(client, Id);

        /// <summary>
        /// Create a Direct Message to the current User
        /// See <a href="https://discord.com/developers/docs/resources/user#create-dm">Create DM</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID to send the message to</param>
        /// <param name="callback">Callback with the direct message channel</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static IPromise<DiscordChannel> CreateDirectMessageChannel(DiscordClient client, Snowflake userId)
        {
            InvalidSnowflakeException.ThrowIfInvalid(userId, nameof(userId));
            InvalidChannelException.ThrowIfChannelToSelf(userId, client);

            DiscordChannel channel = client.Bot.DirectMessagesByUserId[userId];
            if (channel != null)
            {
                return Promise<DiscordChannel>.Resolved(channel);
            }

            Dictionary<string, object> data = new Dictionary<string, object>
            {
                ["recipient_id"] = userId
            };

            return client.Bot.Rest.Post<DiscordChannel>(client, "users/@me/channels", data).Then(newChannel =>
            {
                client.Bot.AddDirectChannel(newChannel);
            });
        }

        /// <summary>
        /// Create a Group Direct Message
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="accessTokens">access tokens of users that have granted your app the gdm.join scope</param>
        /// <param name="nicks">a list of user ids to their respective nicknames</param>
        /// <param name="callback">Callback with the direct message channel</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IPromise<DiscordChannel> CreateGroupDm(DiscordClient client, string[] accessTokens, Hash<Snowflake, string> nicks)
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                ["access_tokens"] = accessTokens,
                ["nicks"] = nicks
            };

            return client.Bot.Rest.Post<DiscordChannel>(client,"users/@me/channels", data);
        }

        /// <summary>
        /// Returns a list of connection objects.
        /// Requires the connections OAuth2 scope.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the list of connections</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IPromise<List<Connection>> GetUserConnections(DiscordClient client)
        {
            return client.Bot.Rest.Get<List<Connection>>(client,"users/@me/connections");
        }

        /// <summary>
        /// Adds a recipient to a Group DM using their access token
        /// See <a href="https://discord.com/developers/docs/resources/channel#group-dm-add-recipient">Group DM Add Recipient</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channel">Channel to add recipient to</param>
        /// <param name="accessToken">Users access token</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IPromise GroupDmAddRecipient(DiscordClient client, DiscordChannel channel, string accessToken) => GroupDmAddRecipient(client, channel.Id, accessToken, Username);

        /// <summary>
        /// Adds a recipient to a Group DM using their access token
        /// See <a href="https://discord.com/developers/docs/resources/channel#group-dm-add-recipient">Group DM Add Recipient</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to add user to</param>
        /// <param name="accessToken">Users access token</param>
        /// <param name="nick">User nickname</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IPromise GroupDmAddRecipient(DiscordClient client, Snowflake channelId, string accessToken, string nick)
        {
            InvalidSnowflakeException.ThrowIfInvalid(channelId, nameof(channelId));
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                ["access_token"] = accessToken,
                ["nick"] = nick
            };

            return client.Bot.Rest.Put(client,$"channels/{channelId}/recipients/{Id}", data);
        }

        /// <summary>
        /// Removes a recipient from a Group DM
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channel">Channel to remove recipient from</param>
        /// <param name="callback">callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IPromise GroupDmRemoveRecipient(DiscordClient client, DiscordChannel channel) => GroupDmRemoveRecipient(client, channel.Id);

        /// <summary>
        /// Removes a recipient from a Group DM
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to remove recipient from</param>
        /// <param name="callback">callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public IPromise GroupDmRemoveRecipient(DiscordClient client, Snowflake channelId)
        {
            InvalidSnowflakeException.ThrowIfInvalid(channelId, nameof(channelId));
            return client.Bot.Rest.Delete(client,$"channels/{channelId}/recipients/{Id}");
        }
        #endregion
        
        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            DiscordUser update = EntityCache<DiscordUser>.Instance.Update(this);
            Update(update);
        }
        
        public void Update(DiscordUser update)
        {
            if (update.Username != null) Username = update.Username;
            if (update.GlobalName != null) GlobalName = update.GlobalName;
            if (update.Discriminator != null) Discriminator = update.Discriminator;
            if (update.Avatar != null) Avatar = update.Avatar;
            if (update.MfaEnabled.HasValue) MfaEnabled = update.MfaEnabled;
            if (update.Banner != null) Banner = update.Banner;
            if (update.AccentColor.HasValue) AccentColor = update.AccentColor;
            if (update.Locale != null) Locale = update.Locale;
            if (update.Verified.HasValue) Verified = update.Verified;
            if (update.Email != null) Email = update.Email;
            if (update.Flags.HasValue) Flags = update.Flags;
            if (update.PremiumType.HasValue) PremiumType = update.PremiumType;
            if (update.PublicFlags.HasValue) PublicFlags = update.PublicFlags;
        }
        
        public void LogDebug(DebugLogger logger)
        {
            logger.AppendField("Id", Id);
            logger.AppendField("Name", FullUserName);
            logger.AppendField("Bot", IsBot);
        }
    }
}