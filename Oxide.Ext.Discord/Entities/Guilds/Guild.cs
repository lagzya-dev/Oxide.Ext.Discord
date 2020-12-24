using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Emojis;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Entities.Guilds.Integrations;
using Oxide.Ext.Discord.Entities.Guilds.Roles;
using Oxide.Ext.Discord.Entities.Invites;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Entities.Voice;
using Oxide.Ext.Discord.REST;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    public class Guild : GuildPreview
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("icon_Hash")]
        public string IconHash { get; set; }

        [JsonProperty("discovery_splash")]
        public string DiscoverySplash { get; set; }
  
        [JsonProperty("owner")]
        public bool? Owner { get; set; }

        [JsonProperty("permissions")]
        public string Permissions { get; set; }
  
        [JsonProperty("widget_enabled")]
        public bool? WidgetEnabled { get; set; }
  
        [JsonProperty("widget_channel_id")]
        public string WidgetChannelId { get; set; }
  
        [JsonProperty("emojis")]
        public List<Emoji> Emojis { get; set; }
  
        [JsonProperty("features")]
        public List<GuildFeatures> Features { get; set; }
  
        [JsonProperty("mfa_level")]
        public GuildMFALevel? MfaLevel { get; set; }
  
        [JsonProperty("application_id")]
        public string ApplicationId { get; set; }
        
        [JsonProperty("system_channel_flags")]
        public SystemChannelFlags SystemChannelFlags { get; set; }

        [JsonProperty("joined_at")]
        public string JoinedAt { get; set; }
  
        [JsonProperty("large")]
        public bool? Large { get; set; }
  
        [JsonProperty("unavailable")]
        public bool? Unavailable { get; set; }
  
        [JsonProperty("member_count")]
        public int? MemberCount { get; set; }
  
        [JsonProperty("voice_states")]
        public List<VoiceState> VoiceStates { get; set; }
  
        [JsonProperty("members")]
        public List<GuildMember> Members { get; set; }
          
        [JsonProperty("presences")]
        public List<StatusUpdate> Presences { get; set; }
  
        [JsonProperty("max_presences")]
        public int? MaxPresences { get; set; }
  
        [JsonProperty("max_members")]
        public int? MaxMembers { get; set; }
  
        [JsonProperty("vanity_url_code")]
        public string VanityUrlCode { get; set; }
  
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("premium_tier")]
        public GuildPremiumTier? PremiumTier { get; set; }
  
        [JsonProperty("premium_subscription_count")]
        public int? PremiumSubscriptionCount { get; set; }

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

        public static void GetGuild(DiscordClient client, string guildId, Action<Guild> callback = null)
        {
            client.REST.DoRequest($"/guilds/{guildId}", RequestMethod.GET, null, callback);
        }
        
        public static void GetGuildPreview(DiscordClient client, string guildId, Action<GuildPreview> callback = null)
        {
            client.REST.DoRequest($"/guilds/{guildId}/preview", RequestMethod.GET, null, callback);
        }

        public void ModifyGuild(DiscordClient client, Action<Guild> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}", RequestMethod.PATCH, this, callback);
        }

        public void DeleteGuild(DiscordClient client, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}", RequestMethod.DELETE, null, callback);
        }

        public void GetGuildChannels(DiscordClient client, Action<List<Channels.Channel>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/channels", RequestMethod.GET, null, callback);
        }

        public void CreateGuildChannel(DiscordClient client, ChannelCreate channel, Action<Channels.Channel> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/channels", RequestMethod.POST, channel, callback);
        }

        public void ModifyGuildChannelPositions(DiscordClient client, List<ObjectPosition> positions, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/channels", RequestMethod.PATCH, positions, callback);
        }

        public void GetGuildMember(DiscordClient client, string userId, Action<GuildMember> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/members/{userId}", RequestMethod.GET, null, callback);
        }

        public void ListGuildMembers(DiscordClient client, string afterSnowflake = "0", Action<List<GuildMember>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/members?limit=1000&after={afterSnowflake}", RequestMethod.GET, null, callback);
        }

        public void AddGuildMember(DiscordClient client, GuildMember member, string accessToken, List<Role> roles, Action<GuildMember> callback = null)
        {
            AddGuildMember(client, member.User.Id, new AddGuildMember
            {
                Deaf = member.Deaf,
                Mute = member.Mute,
                Nick = member.Nick,
                AccessToken = accessToken,
                Roles = roles.Select(r => r.Id).ToList()
            }, callback);
        }

        public void AddGuildMember(DiscordClient client, string userId, AddGuildMember member, Action<GuildMember> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/members/{userId}", RequestMethod.PUT, member, callback);
        }

        public void ModifyGuildMember(DiscordClient client, GuildMember member, List<string> roles, string channelId, Action callback = null) => this.ModifyGuildMember(client, member.User.Id, member.Nick, roles, member.Deaf, member.Mute, channelId, callback);

        public void ModifyGuildMember(DiscordClient client, string userId, string nick, List<string> roles, bool mute, bool deaf, string channelId, Action callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "nick", nick },
                { "roles", roles },
                { "mute", mute },
                { "deaf", deaf },
                { "channel_id", channelId }
            };

            client.REST.DoRequest($"/guilds/{Id}/members/{userId}", RequestMethod.PATCH, jsonObj, callback);
        }

        public void ModifyUsersNick(DiscordClient client, string userId, string nick, Action callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "nick", nick }
            };

            client.REST.DoRequest($"/guilds/{Id}/members/{userId}", RequestMethod.PATCH, jsonObj, callback);
        }

        public void ModifyCurrentUsersNick(DiscordClient client, string nick, Action<string> callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "nick", nick }
            };

            client.REST.DoRequest($"/guilds/{Id}/members/@me/nick", RequestMethod.PATCH, jsonObj, callback);
        }

        public void AddGuildMemberRole(DiscordClient client, DiscordUser user, Role role, Action callback = null) => AddGuildMemberRole(client, user.Id, role.Id, callback);

        public void AddGuildMemberRole(DiscordClient client, string userId, string roleId, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/members/{userId}/roles/{roleId}", RequestMethod.PUT, null, callback);
        }

        public void RemoveGuildMemberRole(DiscordClient client, DiscordUser user, Role role, Action callback = null) => RemoveGuildMemberRole(client, user.Id, role.Id, callback);

        public void RemoveGuildMemberRole(DiscordClient client, string userId, string roleId, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/members/{userId}/roles/{roleId}", RequestMethod.DELETE, null, callback);
        }

        public void RemoveGuildMember(DiscordClient client, GuildMember member, Action callback = null) => RemoveGuildMember(client, member.User.Id, callback);

        public void RemoveGuildMember(DiscordClient client, string userId, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/members/{userId}", RequestMethod.DELETE, null, callback);
        }

        public void GetGuildBans(DiscordClient client, Action<List<Ban>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/bans", RequestMethod.GET, null, callback);
        }

        public void CreateGuildBan(DiscordClient client, GuildMember member, GuildBan ban, Action callback = null) => CreateGuildBan(client, member.User.Id, ban, callback);

        public void CreateGuildBan(DiscordClient client, string userId, GuildBan ban, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/bans/{userId}", RequestMethod.PUT, ban, callback);
        }

        public void RemoveGuildBan(DiscordClient client, string userId, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/bans/{userId}", RequestMethod.DELETE, null, callback);
        }

        public void GetGuildRoles(DiscordClient client, Action<List<Role>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/roles", RequestMethod.GET, null, callback);
        }

        public void CreateGuildRole(DiscordClient client, Role role, Action<Role> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/roles", RequestMethod.POST, role, callback);
        }

        public void ModifyGuildRolePositions(DiscordClient client, List<ObjectPosition> positions, Action<List<Role>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/roles", RequestMethod.PATCH, positions, callback);
        }

        public void ModifyGuildRole(DiscordClient client, Role role, Action<Role> callback = null) => ModifyGuildRole(client, role.Id, role, callback);

        public void ModifyGuildRole(DiscordClient client, string roleId, Role role, Action<Role> callback = null)
        {
            client.REST.DoRequest<Role>($"/guilds/{Id}/roles/{roleId}", RequestMethod.PATCH, role, callback);
        }

        public void DeleteGuildRole(DiscordClient client, Role role, Action callback = null) => DeleteGuildRole(client, role.Id, callback);

        public void DeleteGuildRole(DiscordClient client, string roleId, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/roles/{roleId}", RequestMethod.DELETE, null, callback);
        }

        public void GetGuildPruneCount(DiscordClient client, GuildPruneGet prune, Action<int?> callback = null)
        {
            client.REST.DoRequest<JObject>($"/guilds/{Id}/prune?{prune.ToQueryString()}", RequestMethod.GET, null, (returnValue) =>
            {
                int? pruned = returnValue.GetValue("pruned").ToObject<int?>();
                callback?.Invoke(pruned);
            });
        }

        public void BeginGuildPrune(DiscordClient client, GuildPruneBegin prune, Action<int?> callback = null)
        {
            client.REST.DoRequest<JObject>($"/guilds/{Id}/prune?{prune.ToQueryString()}", RequestMethod.POST, null, (returnValue) =>
            {
                int? pruned = returnValue.GetValue("pruned").ToObject<int?>();
                callback?.Invoke(pruned);
            });
        }

        public void GetGuildVoiceRegions(DiscordClient client, Action<List<VoiceRegion>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/regions", RequestMethod.GET, null, callback);
        }

        public void GetGuildInvites(DiscordClient client, Action<List<InviteMetadata>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/invites", RequestMethod.GET, null, callback);
        }

        public void GetGuildIntegrations(DiscordClient client, Action<List<Integration>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/integrations", RequestMethod.GET, null, callback);
        }

        public void CreateGuildIntegration(DiscordClient client, Integration integration, Action callback = null) => CreateGuildIntegration(client, integration.Type, integration.Id, callback);

        public void CreateGuildIntegration(DiscordClient client, IntegrationType type, string id, Action callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "type", type },
                { "id", id }
            };

            client.REST.DoRequest($"/guilds/{id}/integrations", RequestMethod.POST, jsonObj, callback);
        }

        public void ModifyGuildIntegration(DiscordClient client, Integration integration, bool? enableEmoticons, Action callback = null) => ModifyGuildIntegration(client, integration.Id, integration.ExpireBehaviour, integration.ExpireGracePeriod, enableEmoticons, callback);

        public void ModifyGuildIntegration(DiscordClient client, string integrationId, int? expireBehaviour, int? expireGracePeroid, bool? enableEmoticons, Action callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "expire_behaviour", expireBehaviour },
                { "expire_grace_period", expireGracePeroid },
                { "enable_emoticons", enableEmoticons }
            };

            client.REST.DoRequest($"/guilds/{Id}/integrations/{integrationId}", RequestMethod.PATCH, jsonObj, callback);
        }

        public void DeleteGuildIntegration(DiscordClient client, Integration integration, Action callback = null) => DeleteGuildIntegration(client, integration.Id, callback);

        public void DeleteGuildIntegration(DiscordClient client, string integrationId, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/integrations/{integrationId}", RequestMethod.DELETE, null, callback);
        }

        public void SyncGuildIntegration(DiscordClient client, Integration integration, Action callback = null) => SyncGuildIntegration(client, integration.Id, callback);

        public void SyncGuildIntegration(DiscordClient client, string integrationId, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/integrations/{integrationId}/sync", RequestMethod.POST, null, callback);
        }
        
        public void GetGuildWidgetSettings(DiscordClient client, Action<GuildWidgetSettings> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/widget", RequestMethod.GET, null, callback);
        }
        
        public void ModifyGuildWidget(DiscordClient client, GuildWidget widget, Action<GuildWidget> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/widget", RequestMethod.PATCH, widget, callback);
        }

        public void GetGuildWidget(DiscordClient client, Action<GuildWidget> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/widget.json", RequestMethod.GET, null, callback);
        }

        public void GetGuildVanityUrl(DiscordClient client, Action<Invite> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/vanity-url", RequestMethod.GET, null, callback);
        }
        
        public void ListGuildEmojis(DiscordClient client, Action<List<Emoji>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/emojis", RequestMethod.GET, null, callback);
        }
        
        public void GetGuildEmoji(DiscordClient client, string emjoiId, Action<Emoji> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/emojis/{emjoiId}", RequestMethod.GET, null, callback);
        }
        
        public void CreateGuildEmoji(DiscordClient client, EmojiCreate emoji, Action<Emoji> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/emojis", RequestMethod.POST, emoji, callback);
        }
        
        public void UpdateGuildEmoji(DiscordClient client, string emojiId, EmojiUpdate emoji, Action<Emoji> callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/emojis/{emojiId}", RequestMethod.PATCH, emoji, callback);
        }
        
        public void DeleteGuildEmoji(DiscordClient client, string emojiId, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{Id}/emojis/{emojiId}", RequestMethod.DELETE, null, callback);
        }

        public void Update(Guild updatedGuild)
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
    }
}
