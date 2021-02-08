using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Emojis;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Entities.Integrations;
using Oxide.Ext.Discord.Entities.Invites;
using Oxide.Ext.Discord.Entities.Roles;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Entities.Voice;
using Oxide.Ext.Discord.Helpers.Cdn;
using Oxide.Ext.Discord.Helpers.Converters;
using Oxide.Ext.Discord.REST;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#guild-object">Guild Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Guild : GuildCreate
    {
        /// <summary>
        /// Guild id
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// Icon hash
        /// </summary>
        [JsonProperty("icon_Hash")]
        public string IconHash { get; set; }
        
        /// <summary>
        /// Splash hash
        /// </summary>
        [JsonProperty("splash")]
        public string Splash { get; set; }

        /// <summary>
        /// Discovery splash hash
        /// Only present for guilds with the "DISCOVERABLE" feature
        /// </summary>
        [JsonProperty("discovery_splash")]
        public string DiscoverySplash { get; set; }
  
        /// <summary>
        /// True if the user is the owner of the guild
        /// </summary>
        [JsonProperty("owner")]
        public bool? Owner { get; set; }
        
        /// <summary>
        /// ID of owner
        /// </summary>
        [JsonProperty("owner_id")]
        public Snowflake OwnerId { get; set; }

        /// <summary>
        /// Total permissions for the user in the guild (excludes overrides)
        /// </summary>
        [JsonProperty("permissions")]
        public string Permissions { get; set; }
  
        /// <summary>
        /// True if the server widget is enabled
        /// </summary>
        [JsonProperty("widget_enabled")]
        public bool? WidgetEnabled { get; set; }
  
        /// <summary>
        /// The channel id that the widget will generate an invite to, or null if set to no invite
        /// </summary>
        [JsonProperty("widget_channel_id")]
        public Snowflake WidgetChannelId { get; set; }
  
        /// <summary>
        /// Custom guild emojis
        /// </summary>
        [JsonProperty("emojis")]
        public List<Emoji> Emojis { get; set; }
  
        /// <summary>
        /// Enabled guild features
        /// See <see cref="GuildFeatures"/>
        /// </summary>
        [JsonProperty("features")]
        public List<GuildFeatures> Features { get; set; }
  
        /// <summary>
        /// Required MFA level for the guild
        /// See <see cref="GuildMFALevel"/>
        /// </summary>
        [JsonProperty("mfa_level")]
        public GuildMFALevel? MfaLevel { get; set; }
  
        /// <summary>
        /// Application id of the guild creator if it is bot-created
        /// </summary>
        [JsonProperty("application_id")]
        public Snowflake ApplicationId { get; set; }
        
        /// <summary>
        /// System channel flags
        /// See <see cref="SystemChannelFlags"/>
        /// </summary>
        [JsonProperty("system_channel_flags")]
        public SystemChannelFlags SystemChannelFlags { get; set; }

        /// <summary>
        /// The id of the channel where Community guilds can display rules and/or guidelines
        /// </summary>
        [JsonProperty("rules_channel_id")]
        public Snowflake RulesChannelId { get; set; }
        
        /// <summary>
        /// When this guild was joined at
        /// </summary>
        [JsonProperty("joined_at")]
        public DateTime JoinedAt { get; set; }
  
        /// <summary>
        /// True if this is considered a large guild
        /// </summary>
        [JsonProperty("large")]
        public bool? Large { get; set; }
  
        /// <summary>
        /// True if this guild is unavailable due to an outage
        /// </summary>
        [JsonProperty("unavailable")]
        public bool? Unavailable { get; set; }
  
        /// <summary>
        /// Total number of members in this guild
        /// </summary>
        [JsonProperty("member_count")]
        public int? MemberCount { get; set; }
  
        /// <summary>
        /// States of members currently in voice channels; lacks the guild_id key
        /// </summary>
        [JsonProperty("voice_states")]
        public List<VoiceState> VoiceStates { get; set; }
  
        /// <summary>
        /// Users in the guild
        /// </summary>
        [JsonConverter(typeof(HashListConverter<GuildMember>))]
        [JsonProperty("members")]
        public Hash<string, GuildMember> Members { get; set; }
        
        /// <summary>
        /// Channels in the guild
        /// </summary>
        [JsonConverter(typeof(HashListConverter<Channel>))]
        [JsonProperty("channels")]
        public new Hash<string, Channel> Channels { get; set; }
          
        /// <summary>
        /// Presences of the members in the guild
        /// will only include non-offline members if the size is greater than large threshold
        /// </summary>
        [JsonProperty("presences")]
        public List<StatusUpdate> Presences { get; set; }
  
        /// <summary>
        /// The maximum number of presences for the guild (the default value, currently 25000, is in effect when null is returned)
        /// </summary>
        [JsonProperty("max_presences")]
        public int? MaxPresences { get; set; }
  
        /// <summary>
        /// The maximum number of members for the guild
        /// </summary>
        [JsonProperty("max_members")]
        public int? MaxMembers { get; set; }
  
        /// <summary>
        /// The vanity url code for the guild
        /// </summary>
        [JsonProperty("vanity_url_code")]
        public string VanityUrlCode { get; set; }
  
        /// <summary>
        /// The description for the guild, if the guild is discoverable
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        
        /// <summary>
        /// Banner hash
        /// </summary>
        [JsonProperty("banner")]
        public string Banner { get; set; }

        /// <summary>
        /// Premium tier (Server Boost level)
        /// </summary>
        [JsonProperty("premium_tier")]
        public GuildPremiumTier? PremiumTier { get; set; }
  
        /// <summary>
        /// The number of boosts this guild currently has
        /// </summary>
        [JsonProperty("premium_subscription_count")]
        public int? PremiumSubscriptionCount { get; set; }

        /// <summary>
        /// The preferred locale of a Community guild
        /// Used in server discovery and notices from Discord
        /// Defaults to "en-US"
        /// </summary>
        [JsonProperty("preferred_locale")]
        public string PreferredLocale { get; set; }
        
        /// <summary>
        /// The maximum amount of users in a video channel
        /// </summary>
        [JsonProperty("public_updates_channel_id")]
        public Snowflake PublicUpdatesChannelId { get; set; }
        
        /// <summary>
        /// The maximum amount of users in a video channel
        /// </summary>
        [JsonProperty("max_video_channel_users")]
        public int? MaxVideoChannelUsers { get; set; }
        
        /// <summary>
        /// Approximate number of members in this guild
        /// </summary>
        [JsonProperty("approximate_member_count")]
        public int? ApproximateMemberCount { get; set; }
        
        /// <summary>
        /// Approximate number of non-offline members in this guild
        /// </summary>
        [JsonProperty("approximate_presence_count")]
        public int? ApproximatePresenceCount { get; set; }

        /// <summary>
        /// Returns the guild Icon Url
        /// </summary>
        public string IconUrl => DiscordCdn.GetGuildIconUrl(Id, Icon);
        
        /// <summary>
        /// Returns the Guilds Splash Url
        /// </summary>
        public string SplashUrl => DiscordCdn.GetGuildSplashUrl(Id, Splash);
        
        /// <summary>
        /// Returns the guilds Discovery Splash
        /// </summary>
        public string DiscoverySplashUrl => DiscordCdn.GetGuildDiscoverySplashUrl(Id, DiscoverySplash);
        
        /// <summary>
        /// Return the guilds Banner Url
        /// </summary>
        public string BannerUrl => DiscordCdn.GetGuildBannerUrl(Id, Banner);

        /// <summary>
        /// Create a new guild.
        /// See <a href="https://discord.com/developers/docs/resources/guild#create-guild">Create Guild</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="create">Guild Create Object</param>
        /// <param name="callback">Callback with the created guild</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public static void CreateGuild(DiscordClient client, GuildCreate create, Action<Guild> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds", RequestMethod.POST, create, callback, onError);
        }

        /// <summary>
        /// Returns the guild object for the given id
        /// See <a href="https://discord.com/developers/docs/resources/guild#get-guild">Get Guild</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to lookup</param>
        /// <param name="callback">callback with the guild for the given ID</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public static void GetGuild(DiscordClient client, Snowflake guildId, Action<Guild> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{guildId}", RequestMethod.GET, null, callback, onError);
        }
        
        /// <summary>
        /// Returns the guild preview object for the given id.
        /// If the user is not in the guild, then the guild must be Discoverable.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to get preview for</param>
        /// <param name="callback">Callback with the guild preview for the ID</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public static void GetGuildPreview(DiscordClient client, Snowflake guildId, Action<GuildPreview> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{guildId}/preview", RequestMethod.GET, null, callback, onError);
        }

        /// <summary>
        /// Modify a guild's settings.
        /// Requires the MANAGE_GUILD permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#modify-guild">Modify Guild</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the updated guild</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void ModifyGuild(DiscordClient client, Action<Guild> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}", RequestMethod.PATCH, this, callback, onError);
        }

        /// <summary>
        /// Delete a guild permanently.
        /// User must be owner
        /// See <a href="https://discord.com/developers/docs/resources/guild#delete-guild">Delete Guild</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void DeleteGuild(DiscordClient client, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}", RequestMethod.DELETE, null, callback, onError);
        }

        /// <summary>
        /// Returns a list of guild channel objects.
        /// See <a href="https://discord.com/developers/docs/resources/guild#get-guild-channels">Get Guild Channels</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the list of channels</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void GetGuildChannels(DiscordClient client, Action<List<Channel>> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/channels", RequestMethod.GET, null, callback, onError);
        }

        /// <summary>
        /// Create a new channel object for the guild.
        /// Requires the MANAGE_CHANNELS permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#create-guild-channel">Create Guild Channel</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channel">Channel to create</param>
        /// <param name="callback">Callback with created channel</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void CreateGuildChannel(DiscordClient client, ChannelCreate channel, Action<Channel> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/channels", RequestMethod.POST, channel, callback, onError);
        }

        /// <summary>
        /// Modify the positions of a set of channel objects for the guild.
        /// Requires MANAGE_CHANNELS permission.
        /// Only channels to be modified are required, with the minimum being a swap between at least two channels.
        /// See <a href="https://discord.com/developers/docs/resources/guild#modify-guild-channel-positions">Modify Guild Channel Positions</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="positions">List new channel positions for each channel</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void ModifyGuildChannelPositions(DiscordClient client, List<ObjectPosition> positions, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/channels", RequestMethod.PATCH, positions, callback, onError);
        }

        /// <summary>
        /// Returns a guild member object for the specified user.
        /// See <a href="https://discord.com/developers/docs/resources/guild#get-guild-member">Get Guild Member</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">UserID to get guild member for</param>
        /// <param name="callback">Callback with guild member matching user Id</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void GetGuildMember(DiscordClient client, Snowflake userId, Action<GuildMember> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/members/{userId}", RequestMethod.GET, null, callback, onError);
        }

        /// <summary>
        /// Returns a list of guild member objects that are members of the guild.
        /// In the future, this endpoint will be restricted in line with our Privileged Intents
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="limit">max number of members to return (1-1000)</param>
        /// <param name="afterSnowflake">The highest user id in the previous page</param>
        /// <param name="callback"></param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void ListGuildMembers(DiscordClient client, int limit = 1000, string afterSnowflake = "0", Action<List<GuildMember>> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/members?limit={limit}&after={afterSnowflake}", RequestMethod.GET, null, callback, onError);
        }

        /// <summary>
        /// Adds a user to the guild, provided you have a valid oauth2 access token for the user with the guilds.join scope. 
        /// See <a href="https://discord.com/developers/docs/resources/guild#add-guild-member">Add Guild Member</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="member">Member to copy from</param>
        /// <param name="accessToken">Member access token</param>
        /// <param name="roles">List of roles to grant</param>
        /// <param name="callback">Callback with the added guild member</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void AddGuildMember(DiscordClient client, GuildMember member, string accessToken, List<Role> roles, Action<GuildMember> callback = null, Action<RestError> onError = null)
        {
            AddGuildMember(client, member.User.Id, new GuildMemberAdd
            {
                Deaf = member.Deaf,
                Mute = member.Mute,
                Nick = member.Nick,
                AccessToken = accessToken,
                Roles = roles.Select(r => r.Id).ToList()
            }, callback, onError);
        }

        /// <summary>
        /// Adds a user to the guild, provided you have a valid oauth2 access token for the user with the guilds.join scope. 
        /// See <a href="https://discord.com/developers/docs/resources/guild#add-guild-member">Add Guild Member</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID of the user to add</param>
        /// <param name="member">Member to copy from</param>
        /// <param name="callback">Callback with the added guild member</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void AddGuildMember(DiscordClient client, Snowflake userId, GuildMemberAdd member, Action<GuildMember> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/members/{userId}", RequestMethod.PUT, member, callback, onError);
        }

        /// <summary>
        /// Modify attributes of a guild member
        /// See <a href="https://discord.com/developers/docs/resources/guild#modify-guild-member">Modify Guild Member</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID of the user to update</param>
        /// <param name="update">Changes to make to the user</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void ModifyGuildMember(DiscordClient client, Snowflake userId, GuildMemberUpdate update, Action<GuildMember> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/members/{userId}", RequestMethod.PATCH, update, callback, onError);
        }

        /// <summary>
        /// Modify attributes of a guild member
        /// See <a href="https://discord.com/developers/docs/resources/guild#modify-guild-member">Modify Guild Member</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID of the user to update</param>
        /// <param name="nick">Nickname for the user</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void ModifyUsersNick(DiscordClient client, Snowflake userId, string nick, Action<GuildMember> callback = null, Action<RestError> onError = null)
        {
            GuildMemberUpdate update = new GuildMemberUpdate
            {
                Nick = nick
            };
            
            ModifyGuildMember(client, userId, update, callback, onError);
        }

        /// <summary>
        /// Modifies the nickname of the current user in a guild
        /// See <a href="https://discord.com/developers/docs/resources/guild#modify-current-user-nick">Modify Current User Nick</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="nick">New user nickname</param>
        /// <param name="callback">Callback with updated nickname</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void ModifyCurrentUsersNick(DiscordClient client, string nick, Action<string> callback = null, Action<RestError> onError = null)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                ["nick"] = nick
            };

            client.Bot.Rest.DoRequest($"/guilds/{Id}/members/@me/nick", RequestMethod.PATCH, data, callback, onError);
        }

        /// <summary>
        /// Adds a role to a guild member.
        /// Requires the MANAGE_ROLES permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#add-guild-member-role">Add Guild Member Role</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="user">User to add role to</param>
        /// <param name="role">Role to add</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void AddGuildMemberRole(DiscordClient client, DiscordUser user, Role role, Action callback = null, Action<RestError> onError = null) => AddGuildMemberRole(client, user.Id, role.Id, callback, onError);

        /// <summary>
        /// Adds a role to a guild member.
        /// Requires the MANAGE_ROLES permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#add-guild-member-role">Add Guild Member Role</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID to add role to</param>
        /// <param name="roleId">Role ID to add</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void AddGuildMemberRole(DiscordClient client, Snowflake userId, Snowflake roleId, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/members/{userId}/roles/{roleId}", RequestMethod.PUT, null, callback, onError);
        }

        /// <summary>
        /// Removes a role from a guild member.
        /// Requires the MANAGE_ROLES permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#remove-guild-member-role">Remove Guild Member Role</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="user">User to remove role form</param>
        /// <param name="role">Role to remove</param>
        /// <param name="callback">callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void RemoveGuildMemberRole(DiscordClient client, DiscordUser user, Role role, Action callback = null, Action<RestError> onError = null) => RemoveGuildMemberRole(client, user.Id, role.Id, callback, onError);

        /// <summary>
        /// Removes a role from a guild member.
        /// Requires the MANAGE_ROLES permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#remove-guild-member-role">Remove Guild Member Role</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID to remove role form</param>
        /// <param name="roleId">Role ID to remove</param>
        /// <param name="callback">callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void RemoveGuildMemberRole(DiscordClient client, Snowflake userId, Snowflake roleId, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/members/{userId}/roles/{roleId}", RequestMethod.DELETE, null, callback, onError);
        }

        /// <summary>
        /// Remove a member from a guild.
        /// Requires KICK_MEMBERS permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#remove-guild-member">Remove Guild Member</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="member">Guild Member to remove</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void RemoveGuildMember(DiscordClient client, GuildMember member, Action callback = null, Action<RestError> onError = null) => RemoveGuildMember(client, member.User.Id, callback, onError);

        /// <summary>
        /// Remove a member from a guild.
        /// Requires KICK_MEMBERS permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#remove-guild-member">Remove Guild Member</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID of the user to remove</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void RemoveGuildMember(DiscordClient client, Snowflake userId, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/members/{userId}", RequestMethod.DELETE, null, callback, onError);
        }

        /// <summary>
        /// Returns a list of ban objects for the users banned from this guild.
        /// See <a href="https://discord.com/developers/docs/resources/guild#get-guild-bans">Get Guild Bans</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the list of guild bans</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void GetGuildBans(DiscordClient client, Action<List<GuildBan>> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/bans", RequestMethod.GET, null, callback, onError);
        }

        /// <summary>
        /// Returns a guild ban for a specific user
        /// See <a href="https://discord.com/developers/docs/resources/guild#get-guild-ban">Get Guild Ban</a>
        /// Returns 404 if not found
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID to get guild ban for</param>
        /// <param name="callback">Callback with the guild ban for the user</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void GetGuildBan(DiscordClient client, Snowflake userId, Action<GuildBan> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/bans/{userId}", RequestMethod.GET, null, callback, onError);
        }

        /// <summary>
        /// Create a guild ban, and optionally delete previous messages sent by the banned user.
        /// Requires the BAN_MEMBERS permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#create-guild-ban">Create Guild Ban</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="member">Guild Member to ban</param>
        /// <param name="ban">User ban information</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void CreateGuildBan(DiscordClient client, GuildMember member, GuildBanCreate ban, Action callback = null, Action<RestError> onError = null) => CreateGuildBan(client, member.User.Id, ban, callback, onError);

        /// <summary>
        /// Create a guild ban, and optionally delete previous messages sent by the banned user.
        /// Requires the BAN_MEMBERS permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#create-guild-ban">Create Guild Ban</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID to ban</param>
        /// <param name="ban">User ban information</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void CreateGuildBan(DiscordClient client, Snowflake userId, GuildBanCreate ban, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/bans/{userId}", RequestMethod.PUT, ban, callback, onError);
        }

        /// <summary>
        /// Remove the ban for a user.
        /// Requires the BAN_MEMBERS permissions.
        /// See <a href="https://discord.com/developers/docs/resources/guild#remove-guild-ban">Remove Guild Ban</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID of the user to unban</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void RemoveGuildBan(DiscordClient client, Snowflake userId, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/bans/{userId}", RequestMethod.DELETE, null, callback, onError);
        }

        /// <summary>
        /// Returns a list of role objects for the guild.
        /// See <a href="https://discord.com/developers/docs/resources/guild#get-guild-roles">Get Guild Roles</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with a list of role objects</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void GetGuildRoles(DiscordClient client, Action<List<Role>> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/roles", RequestMethod.GET, null, callback, onError);
        }

        /// <summary>
        /// Create a new role for the guild.
        /// Requires the MANAGE_ROLES permission.
        /// Returns the new role object on success.
        /// See <a href="https://discord.com/developers/docs/resources/guild#create-guild-role">Create Guild Role</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="role">New role to create</param>
        /// <param name="callback">Callback with the created role</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void CreateGuildRole(DiscordClient client, Role role, Action<Role> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/roles", RequestMethod.POST, role, callback, onError);
        }

        /// <summary>
        /// Modify the positions of a set of role objects for the guild.
        /// Requires the MANAGE_ROLES permission.
        /// Returns a list of all of the guild's role objects on success.
        /// See <a href="https://discord.com/developers/docs/resources/guild#modify-guild-role-positions">Modify Guild Role Positions</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="positions">List of role with updated positions</param>
        /// <param name="callback">Callback with a list of all guild role objects</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void ModifyGuildRolePositions(DiscordClient client, List<ObjectPosition> positions, Action<List<Role>> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/roles", RequestMethod.PATCH, positions, callback, onError);
        }

        /// <summary>
        /// Modify a guild role.
        /// Requires the MANAGE_ROLES permission.
        /// Returns the updated role on success.
        /// See <a href="https://discord.com/developers/docs/resources/guild#modify-guild-role">Modify Guild Role</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="role">Role to update</param>
        /// <param name="callback">Callback with the updated role</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void ModifyGuildRole(DiscordClient client, Role role, Action<Role> callback = null, Action<RestError> onError = null) => ModifyGuildRole(client, role.Id, role, callback, onError);

        /// <summary>
        /// Modify a guild role.
        /// Requires the MANAGE_ROLES permission.
        /// Returns the updated role on success.
        /// See <a href="https://discord.com/developers/docs/resources/guild#modify-guild-role">Modify Guild Role</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="roleId">Role ID to update</param>
        /// <param name="role">Role to update</param>
        /// <param name="callback">Callback with the updated role</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void ModifyGuildRole(DiscordClient client, Snowflake roleId, Role role, Action<Role> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/roles/{roleId}", RequestMethod.PATCH, role, callback, onError);
        }

        /// <summary>
        /// Delete a guild role.
        /// Requires the MANAGE_ROLES permission
        /// See <a href="https://discord.com/developers/docs/resources/guild#delete-guild-role">Delete Guild Role</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="role">Role to Delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void DeleteGuildRole(DiscordClient client, Role role, Action callback = null, Action<RestError> onError = null) => DeleteGuildRole(client, role.Id, callback, onError);

        /// <summary>
        /// Delete a guild role.
        /// Requires the MANAGE_ROLES permission
        /// See <a href="https://discord.com/developers/docs/resources/guild#delete-guild-role">Delete Guild Role</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="roleId">Role ID to Delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void DeleteGuildRole(DiscordClient client, Snowflake roleId, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/roles/{roleId}", RequestMethod.DELETE, null, callback, onError);
        }

        /// <summary>
        /// Returns an object with one 'pruned' key indicating the number of members that would be removed in a prune operation.
        /// Requires the KICK_MEMBERS permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#get-guild-prune-count">Get Guild Prune Count</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="prune">Prune get request</param>
        /// <param name="callback">Callback with the number of members that would be pruned</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void GetGuildPruneCount(DiscordClient client, GuildPruneGet prune, Action<int?> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest<JObject>($"/guilds/{Id}/prune?{prune.ToQueryString()}", RequestMethod.GET, null, returnValue =>
            {
                int? pruned = returnValue.GetValue("pruned").ToObject<int?>();
                callback?.Invoke(pruned);
            }, onError);
        }

        /// <summary>
        /// Begin a prune operation.
        /// Requires the KICK_MEMBERS permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#begin-guild-prune">Begin Guild Prune</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="prune">Prune begin request</param>
        /// <param name="callback">Callback with number of pruned members</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void BeginGuildPrune(DiscordClient client, GuildPruneBegin prune, Action<int?> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest<JObject>($"/guilds/{Id}/prune?{prune.ToQueryString()}", RequestMethod.POST, null, returnValue =>
            {
                int? pruned = returnValue.GetValue("pruned").ToObject<int?>();
                callback?.Invoke(pruned);
            }, onError);
        }

        /// <summary>
        /// Returns a list of voice region objects for the guild.
        /// Unlike the similar /voice route, this returns VIP servers when the guild is VIP-enabled.
        /// See <a href="https://discord.com/developers/docs/resources/guild#get-guild-voice-regions">Get Guild Voice Regions</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with list of guild voice regions</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void GetGuildVoiceRegions(DiscordClient client, Action<List<VoiceRegion>> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/regions", RequestMethod.GET, null, callback, onError);
        }

        /// <summary>
        /// Returns a list of invite objects (with invite metadata) for the guild.
        /// Requires the MANAGE_GUILD permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#get-guild-invites">Get Guild Invites</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with a list of guild invites</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void GetGuildInvites(DiscordClient client, Action<List<InviteMetadata>> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/invites", RequestMethod.GET, null, callback, onError);
        }

        /// <summary>
        /// Returns a list of integration objects for the guild.
        /// Requires the MANAGE_GUILD permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#get-guild-integrations">Get Guild Integrations</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with a list of guild integrations</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void GetGuildIntegrations(DiscordClient client, Action<List<Integration>> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/integrations", RequestMethod.GET, null, callback, onError);
        }

        /// <summary>
        /// Attach an integration object from the current user to the guild.
        /// Requires the MANAGE_GUILD permission
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="integration">Integration to create in the guild</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void CreateGuildIntegration(DiscordClient client, Integration integration, Action callback = null, Action<RestError> onError = null) => CreateGuildIntegration(client, integration.Type, integration.Id, callback, onError);

        /// <summary>
        /// Attach an integration object from the current user to the guild.
        /// Requires the MANAGE_GUILD permission
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="type">Type of integration to create</param>
        /// <param name="id">Id of the integration</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void CreateGuildIntegration(DiscordClient client, IntegrationType type, Snowflake id, Action callback = null, Action<RestError> onError = null)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                ["type"] = type ,
                ["id"] = id
            };

            client.Bot.Rest.DoRequest($"/guilds/{id}/integrations", RequestMethod.POST, data, callback, onError);
        }

        /// <summary>
        /// Modify the behavior and settings of an integration object for the guild.
        /// Requires the MANAGE_GUILD permission
        /// See <a href="https://discord.com/developers/docs/resources/guild#modify-guild-integration">Modify Guild Integration</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="integrationId">ID of the integration</param>
        /// <param name="update">Update to make to the integration</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void ModifyGuildIntegration(DiscordClient client, Snowflake integrationId, IntegrationUpdate update, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/integrations/{integrationId}", RequestMethod.PATCH, update, callback, onError);
        }
        
        /// <summary>
        /// Delete the attached integration object for the guild.
        /// Deletes any associated webhooks and kicks the associated bot if there is one.
        /// Requires the MANAGE_GUILD permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#delete-guild-integration">Delete Guild Integration</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="integration">Integration to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void DeleteGuildIntegration(DiscordClient client, Integration integration, Action callback = null, Action<RestError> onError = null) => DeleteGuildIntegration(client, integration.Id, callback, onError);

        /// <summary>
        /// Delete the attached integration object for the guild.
        /// Deletes any associated webhooks and kicks the associated bot if there is one.
        /// Requires the MANAGE_GUILD permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#delete-guild-integration">Delete Guild Integration</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="integrationId">Integration ID to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void DeleteGuildIntegration(DiscordClient client, Snowflake integrationId, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/integrations/{integrationId}", RequestMethod.DELETE, null, callback, onError);
        }

        /// <summary>
        /// Sync an integration.
        /// Requires the MANAGE_GUILD permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#sync-guild-integration">Sync Guild Integration</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="integration">Integration to sync</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void SyncGuildIntegration(DiscordClient client, Integration integration, Action callback = null, Action<RestError> onError = null) => SyncGuildIntegration(client, integration.Id, callback, onError);

        /// <summary>
        /// Sync an integration.
        /// Requires the MANAGE_GUILD permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#sync-guild-integration">Sync Guild Integration</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="integrationId">Integration ID to sync</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void SyncGuildIntegration(DiscordClient client, Snowflake integrationId, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/integrations/{integrationId}/sync", RequestMethod.POST, null, callback, onError);
        }
        
        /// <summary>
        /// Returns a guild widget object.
        /// Requires the MANAGE_GUILD permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#get-guild-widget-settings">Get Guild Widget Settings</a>
        /// </summary>
        /// <param name="client">client to use</param>
        /// <param name="callback">Callback with guild widget settings</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void GetGuildWidgetSettings(DiscordClient client, Action<GuildWidgetSettings> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/widget", RequestMethod.GET, null, callback, onError);
        }
        
        /// <summary>
        /// Modify a guild widget object for the guild.
        /// Requires the MANAGE_GUILD permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#modify-guild-widget">Modify Guild Widget</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="widget">Updated widget</param>
        /// <param name="callback">Callback with update guild widget</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void ModifyGuildWidget(DiscordClient client, GuildWidget widget, Action<GuildWidget> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/widget", RequestMethod.PATCH, widget, callback, onError);
        }

        /// <summary>
        /// Returns the widget for the guild.
        /// See <a href="https://discord.com/developers/docs/resources/guild#get-guild-widget">Get Guild Widget</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with guild widget</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void GetGuildWidget(DiscordClient client, Action<GuildWidget> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/widget.json", RequestMethod.GET, null, callback, onError);
        }

        /// <summary>
        /// Returns a partial invite object for guilds with that feature enabled.
        /// Requires the MANAGE_GUILD permission.
        /// Code will be null if a vanity url for the guild is not set.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with invite </param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void GetGuildVanityUrl(DiscordClient client, Action<InviteMetadata> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/vanity-url", RequestMethod.GET, null, callback, onError);
        }
        
        /// <summary>
        /// Returns a list of emoji objects for the given guild.
        /// See <a href="https://discord.com/developers/docs/resources/emoji#list-guild-emojis">List Guild Emojis</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with list of guild emojis</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void ListGuildEmojis(DiscordClient client, Action<List<Emoji>> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/emojis", RequestMethod.GET, null, callback, onError);
        }
        
        /// <summary>
        /// Returns an emoji object for the given guild and emoji IDs.
        /// See <a href="https://discord.com/developers/docs/resources/emoji#get-guild-emoji">Get Guild Emoji</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="emjoiId">Emoji to lookup</param>
        /// <param name="callback">Callback with the guild emoji</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void GetGuildEmoji(DiscordClient client, string emjoiId, Action<Emoji> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/emojis/{emjoiId}", RequestMethod.GET, null, callback, onError);
        }
        
        /// <summary>
        /// Create a new emoji for the guild.
        /// Requires the MANAGE_EMOJIS permission.
        /// Returns the new emoji object on success.
        /// See <a href="https://discord.com/developers/docs/resources/emoji#create-guild-emoji">Create Guild Emoji</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="emoji">Emoji to create</param>
        /// <param name="callback">Callback with the created emoji</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void CreateGuildEmoji(DiscordClient client, EmojiCreate emoji, Action<Emoji> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/emojis", RequestMethod.POST, emoji, callback, onError);
        }
        
        /// <summary>
        /// Modify the given emoji.
        /// Requires the MANAGE_EMOJIS permission.
        /// Returns the updated emoji object on success.
        /// See <a href="https://discord.com/developers/docs/resources/emoji#modify-guild-emoji">Modify Guild Emoji</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="emojiId">Emoji ID to update</param>
        /// <param name="emoji">Emoji update</param>
        /// <param name="callback">Callback with the updated emoji</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void UpdateGuildEmoji(DiscordClient client, string emojiId, EmojiUpdate emoji, Action<Emoji> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/emojis/{emojiId}", RequestMethod.PATCH, emoji, callback, onError);
        }
        
        /// <summary>
        /// Delete the given emoji.
        /// Requires the MANAGE_EMOJIS permission.
        /// See <a href="https://discord.com/developers/docs/resources/emoji#delete-guild-emoji">Delete Guild Emoji</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="emojiId">Emoji ID to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="onError">Callback when an error occurs with error information</param>
        public void DeleteGuildEmoji(DiscordClient client, string emojiId, Action callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{Id}/emojis/{emojiId}", RequestMethod.DELETE, null, callback, onError);
        }

        #region Helpers
        internal void Update(Guild updatedGuild)
        {
            if (updatedGuild.Name != null)
                this.Name = updatedGuild.Name;
            if (updatedGuild.Icon != null)
                this.Icon = updatedGuild.Icon;
            if (updatedGuild.IconHash != null)
                this.IconHash = updatedGuild.IconHash;
            if (updatedGuild.Splash != null)
                this.Splash = updatedGuild.Splash;
            if (updatedGuild.DiscoverySplash != null)
                this.DiscoverySplash = updatedGuild.DiscoverySplash;
            if (updatedGuild.OwnerId != null)
                this.OwnerId = updatedGuild.OwnerId;
            if (updatedGuild.Region != null)
                this.Region = updatedGuild.Region;
            if (updatedGuild.AfkChannelId != null)
                this.AfkChannelId = updatedGuild.AfkChannelId;
            if (updatedGuild.AfkTimeout != null)
                this.AfkTimeout = updatedGuild.AfkTimeout;
            if (updatedGuild.WidgetEnabled != null)
                this.WidgetEnabled = updatedGuild.WidgetEnabled;
            if (updatedGuild.WidgetChannelId != null)
                this.WidgetChannelId = updatedGuild.WidgetChannelId;
            this.VerificationLevel = updatedGuild.VerificationLevel;
            this.DefaultMessageNotifications = updatedGuild.DefaultMessageNotifications;
            this.ExplicitContentFilter = updatedGuild.ExplicitContentFilter;
            if (updatedGuild.Roles != null)
                this.Roles = updatedGuild.Roles;
            if (updatedGuild.Emojis != null)
                this.Emojis = updatedGuild.Emojis;
            if (updatedGuild.Features != null)
                this.Features = updatedGuild.Features;
            if (updatedGuild.MfaLevel != null)
                this.MfaLevel = updatedGuild.MfaLevel;
            if (updatedGuild.ApplicationId != null)
                this.ApplicationId = updatedGuild.ApplicationId;
            if (updatedGuild.SystemChannelId != null)
                this.SystemChannelId = updatedGuild.SystemChannelId;
            this.SystemChannelFlags = updatedGuild.SystemChannelFlags;
            if (this.RulesChannelId != null)
                this.RulesChannelId = updatedGuild.RulesChannelId;
            if (updatedGuild.JoinedAt != null)
                this.JoinedAt = updatedGuild.JoinedAt;
            if (updatedGuild.Large != null)
                this.Large = updatedGuild.Large;
            if (updatedGuild.Unavailable != null)
                this.Unavailable = updatedGuild.Unavailable;
            if (updatedGuild.MemberCount != null)
                this.MemberCount = updatedGuild.MemberCount;
            if (updatedGuild.VoiceStates != null)
                this.VoiceStates = updatedGuild.VoiceStates;
            if (updatedGuild.Members != null)
                this.Members = updatedGuild.Members;
            if (updatedGuild.Channels != null)
                this.Channels = updatedGuild.Channels;
            if (updatedGuild.Presences != null)
                this.Presences = updatedGuild.Presences;
            if (updatedGuild.MaxPresences != null)
                this.MaxPresences = updatedGuild.MaxPresences;
            if (updatedGuild.MaxMembers != null)
                this.MaxMembers = updatedGuild.MaxMembers;
            if (updatedGuild.VanityUrlCode != null)
                this.VanityUrlCode = updatedGuild.VanityUrlCode;
            if (updatedGuild.Description != null)
                this.Description = updatedGuild.Description;
            if (updatedGuild.Banner != null)
                this.Banner = updatedGuild.Banner;
            if (updatedGuild.PremiumTier != null)
                this.PremiumTier = updatedGuild.PremiumTier;
            if (updatedGuild.PremiumSubscriptionCount != null)
                this.PremiumSubscriptionCount = updatedGuild.PremiumSubscriptionCount;
            if (updatedGuild.PreferredLocale != null)
                this.PreferredLocale = updatedGuild.PreferredLocale;
            if (updatedGuild.PublicUpdatesChannelId != null)
                this.PublicUpdatesChannelId = updatedGuild.PublicUpdatesChannelId;
            if (updatedGuild.MaxVideoChannelUsers != null)
                this.MaxVideoChannelUsers = updatedGuild.MaxVideoChannelUsers;
            if (updatedGuild.ApproximateMemberCount != null)
                this.ApproximateMemberCount = updatedGuild.ApproximateMemberCount;
            if (updatedGuild.ApproximatePresenceCount != null)
                this.ApproximatePresenceCount = updatedGuild.ApproximatePresenceCount;
        }

        /// <summary>
        /// Returns if the guild is avaiable to use
        /// </summary>
        public bool IsAvailable => Unavailable.HasValue && !Unavailable.Value;
        #endregion
    }
}
