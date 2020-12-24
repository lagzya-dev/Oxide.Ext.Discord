using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
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
        public string Id { get; set; }

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
            client.REST.DoRequest($"/users/@me", RequestMethod.GET, null, callback);
        }

        public static void GetUser(DiscordClient client, string userId, Action<DiscordUser> callback = null)
        {
            client.REST.DoRequest($"/users/{userId}", RequestMethod.GET, null, callback);
        }

        public void ModifyCurrentUser(DiscordClient client, Action<DiscordUser> callback = null) => ModifyCurrentUser(client, this.Username, this.Avatar, callback);

        public void ModifyCurrentUser(DiscordClient client, string username = "", string avatarData = "", Action<DiscordUser> callback = null)
        {
            var jsonObj = new Dictionary<string, string>()
            {
                { "username", username },
                { "avatar", avatarData }
            };

            client.REST.DoRequest($"/users/@me", RequestMethod.PATCH, jsonObj, callback);
        }

        public void GetCurrentUserGuilds(DiscordClient client, Action<List<Guild>> callback = null)
        {
            client.REST.DoRequest($"/users/@me/guilds", RequestMethod.GET, null, callback);
        }

        public void LeaveGuild(DiscordClient client, Guild guild, Action callback = null) => LeaveGuild(client, guild.Id, callback);

        public void LeaveGuild(DiscordClient client, string guildId, Action callback = null)
        {
            client.REST.DoRequest($"/users/@me/guilds/{guildId}", RequestMethod.DELETE, null, callback);
        }

        //No longer works for bots
        public void GetUserDMs(DiscordClient client, Action<List<Channels.Channel>> callback = null)
        {
            client.REST.DoRequest($"/users/@me/channels", RequestMethod.GET, null, callback);
        }

        public void CreateDm(DiscordClient client, Action<Channels.Channel> callback = null)
        {
            var jsonObj = new Dictionary<string, string>()
            {
                { "recipient_id", this.Id }
            };

            client.REST.DoRequest("/users/@me/channels", RequestMethod.POST, jsonObj, callback);
        }

        public void CreateGroupDm(DiscordClient client, string[] accessTokens, List<NickId> nicks, Action<Channels.Channel> callback = null)
        {
            var nickDict = nicks.ToDictionary(k => k.Id, v => v.Nick);

            var jsonObj = new Dictionary<string, object>()
            {
                { "access_tokens", accessTokens },
                { "nicks", nickDict }
            };

            client.REST.DoRequest($"/users/@me/channels", RequestMethod.POST, jsonObj, callback);
        }

        public void GetUserConnections(DiscordClient client, Action<List<Connection>> callback = null)
        {
            client.REST.DoRequest($"/users/@me/connections", RequestMethod.GET, null, callback);
        }

        public void GroupDmAddRecipient(DiscordClient client, Channels.Channel channel, string accessToken, Action callback = null) => GroupDmAddRecipient(client, channel.Id, accessToken, this.Username, callback);

        public void GroupDmAddRecipient(DiscordClient client, string channelId, string accessToken, string nick, Action callback = null)
        {
            var jsonObj = new Dictionary<string, string>()
            {
                { "access_token", accessToken },
                { "nick", nick }
            };

            client.REST.DoRequest($"/channels/{channelId}/recipients/{Id}", RequestMethod.PUT, jsonObj, callback);
        }

        public void GroupDmRemoveRecipient(DiscordClient client, Channels.Channel channel) => GroupDmRemoveRecipient(client, channel.Id);

        public void GroupDmRemoveRecipient(DiscordClient client, string channelId, Action callback = null)
        {
            client.REST.DoRequest($"/channels/{channelId}/recipients/{Id}", RequestMethod.DELETE, null, callback);
        }

        public void Update(DiscordUser updateduser)
        {
            if (updateduser.Avatar != null)
                this.Avatar = updateduser.Avatar;
            if (updateduser.Bot != null)
                this.Bot = updateduser.Bot;
            if (updateduser.Discriminator != null)
                this.Discriminator = updateduser.Discriminator;
            if (updateduser.Email != null)
                this.Email = updateduser.Email;
            if (updateduser.Locale != null)
                this.Locale = updateduser.Locale;
            if (updateduser.MfaEnabled != null)
                this.MfaEnabled = updateduser.MfaEnabled;
            if (updateduser.PremiumType != null)
                this.PremiumType = updateduser.PremiumType;
            if (updateduser.Username != null)
                this.Username = updateduser.Username;
            if (updateduser.Verified != null)
                this.Verified = updateduser.Verified;
        }
    }
}
