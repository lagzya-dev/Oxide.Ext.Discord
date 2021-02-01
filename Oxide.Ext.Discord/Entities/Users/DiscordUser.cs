using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Users.Connections;
using Oxide.Ext.Discord.Helpers.Cdn;
using Oxide.Ext.Discord.REST;

namespace Oxide.Ext.Discord.Entities.Users
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/user#user-object">User Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordUser
    {
        /// <summary>
        /// The user's id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The user's username, not unique across the platform
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// The user's 4-digit discord-tag
        /// </summary>
        [JsonProperty("discriminator")]
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

        /// <summary>
        /// Default Avatar Url for the User
        /// </summary>
        public string GetDefaultAvatarUrl => DiscordCdn.GetUserDefaultAvatarUrl(Id, Discriminator);
        
        /// <summary>
        /// Avatar Url for the user
        /// </summary>
        public string GetAvatarUrl => DiscordCdn.GetUserAvatarUrl(Id, Avatar);

        /// <summary>
        /// Returns the currently logged in user account
        /// See <a href="https://discord.com/developers/docs/resources/user#get-current-user">Get Current User</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the logged in user</param>
        public static void GetCurrentUser(DiscordClient client, Action<DiscordUser> callback = null)
        {
            client.Bot.Rest.DoRequest($"/users/@me", RequestMethod.GET, null, callback);
        }

        /// <summary>
        /// Returns the user for the given user Id
        /// See <a href="https://discord.com/developers/docs/resources/user#get-user">Get User</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID to lookup</param>
        /// <param name="callback">Callback with the looked up user</param>
        public static void GetUser(DiscordClient client, string userId, Action<DiscordUser> callback = null)
        {
            client.Bot.Rest.DoRequest($"/users/{userId}", RequestMethod.GET, null, callback);
        }

        /// <summary>
        /// Modify the currently logged in user with the currently set UserName and Avatar
        /// See <a href="https://discord.com/developers/docs/resources/user#modify-current-user">Modify Current User</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the updated user</param>
        public void ModifyCurrentUser(DiscordClient client, Action<DiscordUser> callback = null) => ModifyCurrentUser(client, Username, Avatar, callback);

        /// <summary>
        /// Modify the currently logged in user
        /// See <a href="https://discord.com/developers/docs/resources/user#modify-current-user">Modify Current User</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="username">Username to set</param>
        /// <param name="avatarData">Avatar data to set</param>
        /// <param name="callback">Callback with the updated user</param>
        public void ModifyCurrentUser(DiscordClient client, string username = "", string avatarData = "", Action<DiscordUser> callback = null)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                ["username"] = username,
                ["avatar"] = avatarData
            };

            client.Bot.Rest.DoRequest($"/users/@me", RequestMethod.PATCH, data, callback);
        }

        /// <summary>
        /// Returns the guilds for the currently logged in user
        /// See <a href="https://discord.com/developers/docs/resources/user#get-current-user-guilds">Get Current User Guilds</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the list of guilds</param>
        public void GetCurrentUserGuilds(DiscordClient client, Action<List<Guild>> callback = null)
        {
            client.Bot.Rest.DoRequest($"/users/@me/guilds", RequestMethod.GET, null, callback);
        }

        /// <summary>
        /// Leave the guild that the currently logged in user is in
        /// See <a href="https://discord.com/developers/docs/resources/user#leave-guild">Leave Guild</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guild">Guild to leave</param>
        /// <param name="callback">callback when the action is completed</param>
        public void LeaveGuild(DiscordClient client, Guild guild, Action callback = null) => LeaveGuild(client, guild.Id, callback);
        
        /// <summary>
        /// Leave the guild that the currently logged in user is in
        /// See <a href="https://discord.com/developers/docs/resources/user#leave-guild">Leave Guild</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to leave</param>
        /// <param name="callback">callback when the action is completed</param>
        public void LeaveGuild(DiscordClient client, string guildId, Action callback = null)
        {
            client.Bot.Rest.DoRequest($"/users/@me/guilds/{guildId}", RequestMethod.DELETE, null, callback);
        }

        /// <summary>
        /// Create a Direct Message to the current User
        /// See <a href="https://discord.com/developers/docs/resources/user#create-dm">Create DM</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the direct message channel</param>
        public void CreateDirectMessage(DiscordClient client, Action<Channel> callback = null) => CreateDirectMessage(client, Id, callback);

        /// <summary>
        /// Create a Direct Message to the current User
        /// See <a href="https://discord.com/developers/docs/resources/user#create-dm">Create DM</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID to send the message to</param>
        /// <param name="callback">Callback with the direct message channel</param>
        public static void CreateDirectMessage(DiscordClient client, string userId, Action<Channel> callback = null)
        {
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                ["recipient_id"] = userId 
            };

            client.Bot.Rest.DoRequest("/users/@me/channels", RequestMethod.POST, data, callback);
        }

        /// <summary>
        /// Create a Group Direct Message
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="accessTokens">access tokens of users that have granted your app the gdm.join scope</param>
        /// <param name="nicks">a list of user ids to their respective nicknames</param>
        /// <param name="callback">Callback with the direct message channel</param>
        public void CreateGroupDm(DiscordClient client, string[] accessTokens, List<NickId> nicks, Action<Channel> callback = null)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                ["access_tokens"] = accessTokens,
                ["nicks"] = nicks.ToDictionary(k => k.Id, v => v.Nick) 
            };

            client.Bot.Rest.DoRequest("/users/@me/channels", RequestMethod.POST, data, callback);
        }

        /// <summary>
        /// Returns a list of connection objects.
        /// Requires the connections OAuth2 scope.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the list of connections</param>
        public void GetUserConnections(DiscordClient client, Action<List<Connection>> callback = null)
        {
            client.Bot.Rest.DoRequest("/users/@me/connections", RequestMethod.GET, null, callback);
        }

        internal void Update(DiscordUser update)
        {
            if (update.Avatar != null)
                Avatar = update.Avatar;
            if (update.Bot != null)
                Bot = update.Bot;
            if (update.Discriminator != null)
                Discriminator = update.Discriminator;
            if (update.Email != null)
                Email = update.Email;
            if (update.Locale != null)
                Locale = update.Locale;
            if (update.MfaEnabled != null)
                MfaEnabled = update.MfaEnabled;
            if (update.PremiumType != null)
                PremiumType = update.PremiumType;
            if (update.Username != null)
                Username = update.Username;
            if (update.Verified != null)
                Verified = update.Verified;
        }
    }
}
