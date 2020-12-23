using System.Linq;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;
    using Oxide.Ext.Discord.REST;

    public class Guild : GuildCreate
    {
        public string id { get; set; }
        
        [JsonProperty("icon_Hash")]
        public string IconHash { get; set; }

        public string splash { get; set; }

        public string discovery_splash { get; set; }

        public bool? owner { get; set; }

        public string owner_id { get; set; }

        public string permissions { get; set; }

        public bool? widget_enabled { get; set; }

        public string widget_channel_id { get; set; }

        public List<Emoji> emojis { get; set; }

        public List<string> features { get; set; }

        public GuildMFALevel? mfa_level { get; set; }

        public string application_id { get; set; }
        
        [JsonProperty("system_channel_flags")]
        public SystemChannelFlags SystemChannelFlags { get; set; }

        public string rules_channel_id { get; set; }

        public string joined_at { get; set; }

        public bool? large { get; set; }

        public bool? unavailable { get; set; }

        public int? member_count { get; set; }

        public List<VoiceState> voice_states { get; set; }

        public List<GuildMember> members { get; set; }
        
        public List<Presence> presences { get; set; }

        public int? max_presences { get; set; }

        public int? max_members { get; set; }

        public string vanity_url_code { get; set; }

        public string description { get; set; }

        public string banner { get; set; }

        public GuildPremiumTier? premium_tier { get; set; }

        public int? premium_subscription_count { get; set; }

        public string preferred_locale { get; set; }

        public string public_updates_channel_id { get; set; }
        
        [JsonProperty("max_video_channel_users")]
        public int? MaxVideoChannelUsers { get; set; }
        
        [JsonProperty("approximate_member_count")]
        public int? ApproximateMemberCount { get; set; }
        
        [JsonProperty("approximate_presence_count")]
        public int? ApproximatePresenceCount { get; set; }

        public static void CreateGuild(DiscordClient client, GuildCreate create, Action<Guild> callback = null)
        {
            client.REST.DoRequest($"/guilds", RequestMethod.POST, create, callback);
        }

        public static void GetGuild(DiscordClient client, string guildID, Action<Guild> callback = null)
        {
            client.REST.DoRequest($"/guilds/{guildID}", RequestMethod.GET, null, callback);
        }
        
        public static void GetGuildPreview(DiscordClient client, string guildID, Action<Guild> callback = null)
        {
            client.REST.DoRequest($"/guilds/{guildID}/preview", RequestMethod.GET, null, callback);
        }

        public void ModifyGuild(DiscordClient client, Action<Guild> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}", RequestMethod.PATCH, this, callback);
        }

        public void DeleteGuild(DiscordClient client, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}", RequestMethod.DELETE, null, callback);
        }

        public void GetGuildChannels(DiscordClient client, Action<List<Channel>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/channels", RequestMethod.GET, null, callback);
        }

        public void CreateGuildChannel(DiscordClient client, ChannelCreate channel, Action<Channel> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/channels", RequestMethod.POST, channel, callback);
        }

        public void ModifyGuildChannelPositions(DiscordClient client, List<ObjectPosition> positions, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/channels", RequestMethod.PATCH, positions, callback);
        }

        public void GetGuildMember(DiscordClient client, string userID, Action<GuildMember> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/members/{userID}", RequestMethod.GET, null, callback);
        }

        public void ListGuildMembers(DiscordClient client, string afterSnowflake = "0", Action<List<GuildMember>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/members?limit=1000&after={afterSnowflake}", RequestMethod.GET, null, callback);
        }

        public void AddGuildMember(DiscordClient client, GuildMember member, string accessToken, List<Role> roles, Action<GuildMember> callback = null)
        {
            AddGuildMember(client, member.user.id, new AddGuildMember
            {
                Deaf = member.deaf,
                Mute = member.mute,
                Nick = member.nick,
                AccessToken = accessToken,
                Roles = roles.Select(r => r.id).ToList()
            }, callback);
        }

        public void AddGuildMember(DiscordClient client, string userId, AddGuildMember member, Action<GuildMember> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/members/{userId}", RequestMethod.PUT, member, callback);
        }

        public void ModifyGuildMember(DiscordClient client, GuildMember member, List<string> roles, string channelId, Action callback = null) => this.ModifyGuildMember(client, member.user.id, member.nick, roles, member.deaf, member.mute, channelId, callback);

        public void ModifyGuildMember(DiscordClient client, string userID, string nick, List<string> roles, bool mute, bool deaf, string channelId, Action callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "nick", nick },
                { "roles", roles },
                { "mute", mute },
                { "deaf", deaf },
                { "channel_id", channelId }
            };

            client.REST.DoRequest($"/guilds/{id}/members/{userID}", RequestMethod.PATCH, jsonObj, callback);
        }

        public void ModifyUsersNick(DiscordClient client, string userID, string nick, Action callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "nick", nick }
            };

            client.REST.DoRequest($"/guilds/{id}/members/{userID}", RequestMethod.PATCH, jsonObj, callback);
        }

        public void ModifyCurrentUsersNick(DiscordClient client, string nick, Action<string> callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "nick", nick }
            };

            client.REST.DoRequest($"/guilds/{id}/members/@me/nick", RequestMethod.PATCH, jsonObj, callback);
        }

        public void AddGuildMemberRole(DiscordClient client, User user, Role role, Action callback = null) => AddGuildMemberRole(client, user.id, role.id, callback);

        public void AddGuildMemberRole(DiscordClient client, string userID, string roleID, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/members/{userID}/roles/{roleID}", RequestMethod.PUT, null, callback);
        }

        public void RemoveGuildMemberRole(DiscordClient client, User user, Role role, Action callback = null) => RemoveGuildMemberRole(client, user.id, role.id, callback);

        public void RemoveGuildMemberRole(DiscordClient client, string userID, string roleID, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/members/{userID}/roles/{roleID}", RequestMethod.DELETE, null, callback);
        }

        public void RemoveGuildMember(DiscordClient client, GuildMember member, Action callback = null) => RemoveGuildMember(client, member.user.id, callback);

        public void RemoveGuildMember(DiscordClient client, string userID, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/members/{userID}", RequestMethod.DELETE, null, callback);
        }

        public void GetGuildBans(DiscordClient client, Action<List<Ban>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/bans", RequestMethod.GET, null, callback);
        }

        public void CreateGuildBan(DiscordClient client, GuildMember member, GuildBan ban, Action callback = null) => CreateGuildBan(client, member.user.id, ban, callback);

        public void CreateGuildBan(DiscordClient client, string userID, GuildBan ban, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/bans/{userID}", RequestMethod.PUT, ban, callback);
        }

        public void RemoveGuildBan(DiscordClient client, string userID, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/bans/{userID}", RequestMethod.DELETE, null, callback);
        }

        public void GetGuildRoles(DiscordClient client, Action<List<Role>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/roles", RequestMethod.GET, null, callback);
        }

        public void CreateGuildRole(DiscordClient client, Role role, Action<Role> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/roles", RequestMethod.POST, role, callback);
        }

        public void ModifyGuildRolePositions(DiscordClient client, List<ObjectPosition> positions, Action<List<Role>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/roles", RequestMethod.PATCH, positions, callback);
        }

        public void ModifyGuildRole(DiscordClient client, Role role, Action<Role> callback = null) => ModifyGuildRole(client, role.id, role, callback);

        public void ModifyGuildRole(DiscordClient client, string roleID, Role role, Action<Role> callback = null)
        {
            client.REST.DoRequest<Role>($"/guilds/{id}/roles/{roleID}", RequestMethod.PATCH, role, callback);
        }

        public void DeleteGuildRole(DiscordClient client, Role role, Action callback = null) => DeleteGuildRole(client, role.id, callback);

        public void DeleteGuildRole(DiscordClient client, string roleID, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/roles/{roleID}", RequestMethod.DELETE, null, callback);
        }

        public void GetGuildPruneCount(DiscordClient client, GuildPruneGet prune, Action<int?> callback = null)
        {
            client.REST.DoRequest<JObject>($"/guilds/{id}/prune?{prune.ToQueryString()}", RequestMethod.GET, null, (returnValue) =>
            {
                int? pruned = returnValue.GetValue("pruned").ToObject<int?>();
                callback?.Invoke(pruned);
            });
        }

        public void BeginGuildPrune(DiscordClient client, GuildPruneBegin prune, Action<int?> callback = null)
        {
            client.REST.DoRequest<JObject>($"/guilds/{id}/prune?{prune.ToQueryString()}", RequestMethod.POST, null, (returnValue) =>
            {
                int? pruned = returnValue.GetValue("pruned").ToObject<int?>();
                callback?.Invoke(pruned);
            });
        }

        public void GetGuildVoiceRegions(DiscordClient client, Action<List<VoiceRegion>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/regions", RequestMethod.GET, null, callback);
        }

        public void GetGuildInvites(DiscordClient client, Action<List<Invite>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/invites", RequestMethod.GET, null, callback);
        }

        public void GetGuildIntegrations(DiscordClient client, Action<List<Integration>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/integrations", RequestMethod.GET, null, callback);
        }

        public void CreateGuildIntegration(DiscordClient client, Integration integration, Action callback = null) => CreateGuildIntegration(client, integration.type, integration.id, callback);

        public void CreateGuildIntegration(DiscordClient client, string type, string id, Action callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "type", type },
                { "id", id }
            };

            client.REST.DoRequest($"/guilds/{id}/integrations", RequestMethod.POST, jsonObj, callback);
        }

        public void ModifyGuildIntegration(DiscordClient client, Integration integration, bool? enableEmoticons, Action callback = null) => ModifyGuildIntegration(client, integration.id, integration.expire_behaviour, integration.expire_grace_peroid, enableEmoticons, callback);

        public void ModifyGuildIntegration(DiscordClient client, string integrationID, int? expireBehaviour, int? expireGracePeroid, bool? enableEmoticons, Action callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "expire_behaviour", expireBehaviour },
                { "expire_grace_period", expireGracePeroid },
                { "enable_emoticons", enableEmoticons }
            };

            client.REST.DoRequest($"/guilds/{id}/integrations/{integrationID}", RequestMethod.PATCH, jsonObj, callback);
        }

        public void DeleteGuildIntegration(DiscordClient client, Integration integration, Action callback = null) => DeleteGuildIntegration(client, integration.id, callback);

        public void DeleteGuildIntegration(DiscordClient client, string integrationID, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/integrations/{integrationID}", RequestMethod.DELETE, null, callback);
        }

        public void SyncGuildIntegration(DiscordClient client, Integration integration, Action callback = null) => SyncGuildIntegration(client, integration.id, callback);

        public void SyncGuildIntegration(DiscordClient client, string integrationID, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/integrations/{integrationID}/sync", RequestMethod.POST, null, callback);
        }
        
        public void GetGuildWidgetSettings(DiscordClient client, Action<GuildWidgetSettings> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/widget", RequestMethod.GET, null, callback);
        }
        
        public void ModifyGuildWidget(DiscordClient client, GuildWidget widget, Action<GuildWidget> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/widget", RequestMethod.PATCH, widget, callback);
        }

        public void GetGuildWidget(DiscordClient client, Action<GuildWidget> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/widget.json", RequestMethod.GET, null, callback);
        }

        public void GetGuildVanityURL(DiscordClient client, Action<Invite> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/vanity-url", RequestMethod.GET, null, callback);
        }
        
        public void ListGuildEmojis(DiscordClient client, Action<List<Emoji>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/emojis", RequestMethod.GET, null, callback);
        }
        
        public void GetGuildEmoji(DiscordClient client, string emjoiId, Action<Emoji> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/emojis/{emjoiId}", RequestMethod.GET, null, callback);
        }
        
        public void CreateGuildEmoji(DiscordClient client, EmojiCreate emoji, Action<Emoji> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/emojis", RequestMethod.POST, emoji, callback);
        }
        
        public void UpdateGuildEmoji(DiscordClient client, string emojiId, EmojiUpdate emoji, Action<Emoji> callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/emojis/{emojiId}", RequestMethod.PATCH, emoji, callback);
        }
        
        public void DeleteGuildEmoji(DiscordClient client, string emojiId, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/emojis/{emojiId}", RequestMethod.DELETE, null, callback);
        }

        public void Update(Guild updatedGuild)
        {
            if (updatedGuild.name != null)
                this.name = updatedGuild.name;
            if (updatedGuild.icon != null)
                this.icon = updatedGuild.icon;
            if (updatedGuild.IconHash != null)
                this.IconHash = updatedGuild.IconHash;
            if (updatedGuild.splash != null)
                this.splash = updatedGuild.splash;
            if (updatedGuild.discovery_splash != null)
                this.discovery_splash = updatedGuild.discovery_splash;
            if (updatedGuild.owner_id != null)
                this.owner_id = updatedGuild.owner_id;
            if (updatedGuild.region != null)
                this.region = updatedGuild.region;
            if (updatedGuild.afk_channel_id != null)
                this.afk_channel_id = updatedGuild.afk_channel_id;
            if (updatedGuild.afk_timeout != null)
                this.afk_timeout = updatedGuild.afk_timeout;
            if (updatedGuild.widget_enabled != null)
                this.widget_enabled = updatedGuild.widget_enabled;
            if (updatedGuild.widget_channel_id != null)
                this.widget_channel_id = updatedGuild.widget_channel_id;
            this.verification_level = updatedGuild.verification_level;
            this.default_message_notifications = updatedGuild.default_message_notifications;
            this.explicit_content_filter = updatedGuild.explicit_content_filter;
            if (updatedGuild.roles != null)
                this.roles = updatedGuild.roles;
            if (updatedGuild.emojis != null)
                this.emojis = updatedGuild.emojis;
            if (updatedGuild.features != null)
                this.features = updatedGuild.features;
            if (updatedGuild.mfa_level != null)
                this.mfa_level = updatedGuild.mfa_level;
            if (updatedGuild.application_id != null)
                this.application_id = updatedGuild.application_id;
            if (updatedGuild.system_channel_id != null)
                this.system_channel_id = updatedGuild.system_channel_id;
            this.SystemChannelFlags = updatedGuild.SystemChannelFlags;
            if (this.rules_channel_id != null)
                this.rules_channel_id = updatedGuild.rules_channel_id;
            if (updatedGuild.joined_at != null)
                this.joined_at = updatedGuild.joined_at;
            if (updatedGuild.large != null)
                this.large = updatedGuild.large;
            if (updatedGuild.unavailable != null)
                this.unavailable = updatedGuild.unavailable;
            if (updatedGuild.member_count != null)
                this.member_count = updatedGuild.member_count;
            if (updatedGuild.voice_states != null)
                this.voice_states = updatedGuild.voice_states;
            if (updatedGuild.members != null)
                this.members = updatedGuild.members;
            if (updatedGuild.channels != null)
                this.channels = updatedGuild.channels;
            if (updatedGuild.presences != null)
                this.presences = updatedGuild.presences;
            if (updatedGuild.max_presences != null)
                this.max_presences = updatedGuild.max_presences;
            if (updatedGuild.max_members != null)
                this.max_members = updatedGuild.max_members;
            if (updatedGuild.vanity_url_code != null)
                this.vanity_url_code = updatedGuild.vanity_url_code;
            if (updatedGuild.description != null)
                this.description = updatedGuild.description;
            if (updatedGuild.banner != null)
                this.banner = updatedGuild.banner;
            if (updatedGuild.premium_tier != null)
                this.premium_tier = updatedGuild.premium_tier;
            if (updatedGuild.premium_subscription_count != null)
                this.premium_subscription_count = updatedGuild.premium_subscription_count;
            if (updatedGuild.preferred_locale != null)
                this.preferred_locale = updatedGuild.preferred_locale;
            if (updatedGuild.public_updates_channel_id != null)
                this.public_updates_channel_id = updatedGuild.public_updates_channel_id;
            if (updatedGuild.MaxVideoChannelUsers != null)
                this.MaxVideoChannelUsers = updatedGuild.MaxVideoChannelUsers;
            if (updatedGuild.ApproximateMemberCount != null)
                this.ApproximateMemberCount = updatedGuild.ApproximateMemberCount;
            if (updatedGuild.ApproximatePresenceCount != null)
                this.ApproximatePresenceCount = updatedGuild.ApproximatePresenceCount;

        }
    }
}
