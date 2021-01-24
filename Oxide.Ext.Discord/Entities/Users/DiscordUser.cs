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
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordUser
    {
        [JsonProperty("id")]
        public Snowflake Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("discriminator")]
        public string Discriminator { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("bot")]
        public bool? Bot { get; set; }
        
        [JsonProperty("system")]
        public bool? System { get; set; }

        [JsonProperty("mfa_enabled")]
        public bool? MfaEnabled { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("verified")]
        public bool? Verified { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("flags")]
        public UserFlags? Flags { get; set; }
        
        [JsonProperty("premium_type")]
        public UserPremiumType? PremiumType { get; set; }
        
        [JsonProperty("public_flags")]
        public UserFlags? PublicFlags { get; set; }

        public string GetDefaultAvatarUrl => DiscordCdn.GetUserDefaultAvatarUrl(Id, Discriminator);
        public string GetAvatarUrl => DiscordCdn.GetUserAvatarUrl(Id, Avatar);

        public static void GetCurrentUser(DiscordClient client, Action<DiscordUser> callback = null)
        {
            client.Bot.Rest.DoRequest($"/users/@me", RequestMethod.GET, null, callback);
        }

        public static void GetUser(DiscordClient client, Snowflake userId, Action<DiscordUser> callback = null)
        {
            client.Bot.Rest.DoRequest($"/users/{userId}", RequestMethod.GET, null, callback);
        }

        public void ModifyCurrentUser(DiscordClient client, Action<DiscordUser> callback = null) => ModifyCurrentUser(client, Username, Avatar, callback);

        public void ModifyCurrentUser(DiscordClient client, string username = "", string avatarData = "", Action<DiscordUser> callback = null)
        {
            var jsonObj = new Dictionary<string, string>()
            {
                { "username", username },
                { "avatar", avatarData }
            };

            client.Bot.Rest.DoRequest($"/users/@me", RequestMethod.PATCH, jsonObj, callback);
        }

        public void GetCurrentUserGuilds(DiscordClient client, Action<List<Guild>> callback = null)
        {
            client.Bot.Rest.DoRequest($"/users/@me/guilds", RequestMethod.GET, null, callback);
        }

        public void LeaveGuild(DiscordClient client, Guild guild, Action callback = null) => LeaveGuild(client, guild.Id, callback);

        public void LeaveGuild(DiscordClient client, Snowflake guildId, Action callback = null)
        {
            client.Bot.Rest.DoRequest($"/users/@me/guilds/{guildId}", RequestMethod.DELETE, null, callback);
        }

        public void CreateDm(DiscordClient client, Action<Channel> callback = null) => CreateDm(client, Id, callback);

        public static void CreateDm(DiscordClient client, Snowflake userId, Action<Channel> callback = null)
        {
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                { "recipient_id", userId.ToString() }
            };

            client.Bot.Rest.DoRequest("/users/@me/channels", RequestMethod.POST, data, callback);
        }

        public void CreateGroupDm(DiscordClient client, string[] accessTokens, List<NickId> nicks, Action<Channel> callback = null)
        {
            var nickDict = nicks.ToDictionary(k => k.Id, v => v.Nick);

            var jsonObj = new Dictionary<string, object>()
            {
                { "access_tokens", accessTokens },
                { "nicks", nickDict }
            };

            client.Bot.Rest.DoRequest("/users/@me/channels", RequestMethod.POST, jsonObj, callback);
        }

        public void GetUserConnections(DiscordClient client, Action<List<Connection>> callback = null)
        {
            client.Bot.Rest.DoRequest("/users/@me/connections", RequestMethod.GET, null, callback);
        }

        public void GroupDmAddRecipient(DiscordClient client, Channel channel, string accessToken, Action callback = null) => GroupDmAddRecipient(client, channel.Id, accessToken, Username, callback);

        public void GroupDmAddRecipient(DiscordClient client, Snowflake channelId, string accessToken, string nick, Action callback = null)
        {
            var jsonObj = new Dictionary<string, string>()
            {
                { "access_token", accessToken },
                { "nick", nick }
            };

            client.Bot.Rest.DoRequest($"/channels/{channelId}/recipients/{Id}", RequestMethod.PUT, jsonObj, callback);
        }

        public void GroupDmRemoveRecipient(DiscordClient client, Channel channel) => GroupDmRemoveRecipient(client, channel.Id);

        public void GroupDmRemoveRecipient(DiscordClient client, Snowflake channelId, Action callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{channelId}/recipients/{Id}", RequestMethod.DELETE, null, callback);
        }

        public void Update(DiscordUser update)
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
