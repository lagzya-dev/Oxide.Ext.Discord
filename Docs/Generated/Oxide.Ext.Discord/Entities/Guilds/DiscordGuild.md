# DiscordGuild class

Represents [Guild Structure](https://discord.com/developers/docs/resources/guild#guild-object)

```csharp
public class DiscordGuild : ISnowflakeEntity
```

## Public Members

| name | description |
| --- | --- |
| [DiscordGuild](DiscordGuild/DiscordGuild.md)() | The default constructor. |
| [AfkChannelId](DiscordGuild/AfkChannelId.md) { get; set; } | ID of afk channel |
| [AfkTimeout](DiscordGuild/AfkTimeout.md) { get; set; } | Afk timeout in seconds |
| [ApplicationId](DiscordGuild/ApplicationId.md) { get; set; } | Application id of the guild creator if it is bot-created |
| [ApproximateMemberCount](DiscordGuild/ApproximateMemberCount.md) { get; set; } | Approximate number of members in this guild |
| [ApproximatePresenceCount](DiscordGuild/ApproximatePresenceCount.md) { get; set; } | Approximate number of non-offline members in this guild |
| [Banner](DiscordGuild/Banner.md) { get; set; } | Banner hash |
| [BannerUrl](DiscordGuild/BannerUrl.md) { get; } | Return the guilds Banner Url |
| [Channels](DiscordGuild/Channels.md) { get; set; } | Channels in the guild |
| [DefaultMessageNotifications](DiscordGuild/DefaultMessageNotifications.md) { get; set; } | Default message notification level |
| [Description](DiscordGuild/Description.md) { get; set; } | The description of a guild |
| [DiscoverySplash](DiscordGuild/DiscoverySplash.md) { get; set; } | Discovery splash hash Only present for guilds with the "DISCOVERABLE" feature |
| [DiscoverySplashUrl](DiscordGuild/DiscoverySplashUrl.md) { get; } | Returns the guilds Discovery Splash |
| [Emojis](DiscordGuild/Emojis.md) { get; set; } | Custom guild emojis |
| [EveryoneRole](DiscordGuild/EveryoneRole.md) { get; } | Returns the everyone role for the guild. |
| [ExplicitContentFilter](DiscordGuild/ExplicitContentFilter.md) { get; set; } | Explicit content filter level |
| [Features](DiscordGuild/Features.md) { get; set; } | Enabled guild features See [`GuildFeatures`](./GuildFeatures.md) |
| [HasLoadedAllMembers](DiscordGuild/HasLoadedAllMembers.md) { get; } | Returns true if all guild members have been loaded |
| [Icon](DiscordGuild/Icon.md) { get; set; } | Base64 128x128 image for the guild icon |
| [IconHash](DiscordGuild/IconHash.md) { get; set; } | Icon hash |
| [IconUrl](DiscordGuild/IconUrl.md) { get; } | Returns the guild Icon Url |
| [Id](DiscordGuild/Id.md) { get; set; } | Guild id |
| [IsAvailable](DiscordGuild/IsAvailable.md) { get; } | Returns if the guild is available to use |
| [JoinedAt](DiscordGuild/JoinedAt.md) { get; set; } | When this guild was joined at |
| [Large](DiscordGuild/Large.md) { get; set; } | True if this is considered a large guild |
| [LeftMembers](DiscordGuild/LeftMembers.md) { get; } | Members who have left the guild This list will contain members who have left the guild since the initial bot connection |
| [MaxMembers](DiscordGuild/MaxMembers.md) { get; set; } | The maximum number of members for the guild |
| [MaxPresences](DiscordGuild/MaxPresences.md) { get; set; } | The maximum number of presences for the guild (the default value, currently 25000, is in effect when null is returned) |
| [MaxStageVideoChannelUsers](DiscordGuild/MaxStageVideoChannelUsers.md) { get; set; } | The maximum amount of users in a stage video channel |
| [MaxVideoChannelUsers](DiscordGuild/MaxVideoChannelUsers.md) { get; set; } | The maximum amount of users in a video channel |
| [MemberCount](DiscordGuild/MemberCount.md) { get; set; } | Total number of members in this guild |
| [Members](DiscordGuild/Members.md) { get; set; } | Users in the guild |
| [MfaLevel](DiscordGuild/MfaLevel.md) { get; set; } | Required MFA level for the guild See [`GuildMfaLevel`](./GuildMfaLevel.md) |
| [Name](DiscordGuild/Name.md) { get; set; } | Name of the guild (2-100 characters) |
| [NsfwLevel](DiscordGuild/NsfwLevel.md) { get; set; } | Guild NSFW level [NSFW Information](https://support.discord.com/hc/en-us/articles/1500005389362-NSFW-Server-Designation) |
| [Owner](DiscordGuild/Owner.md) { get; set; } | True if the user is the owner of the guild |
| [OwnerId](DiscordGuild/OwnerId.md) { get; set; } | ID of owner |
| [Permissions](DiscordGuild/Permissions.md) { get; set; } | Total permissions for the user in the guild (excludes overrides) |
| [PreferredLocale](DiscordGuild/PreferredLocale.md) { get; set; } | The preferred locale of a Community guild Used in server discovery and notices from Discord Defaults to "en-US" |
| [PremiumProgressBarEnabled](DiscordGuild/PremiumProgressBarEnabled.md) { get; set; } | The scheduled events in the guild |
| [PremiumSubscriptionCount](DiscordGuild/PremiumSubscriptionCount.md) { get; set; } | The number of boosts this guild currently has |
| [PremiumTier](DiscordGuild/PremiumTier.md) { get; set; } | Premium tier (Server Boost level) |
| [Presences](DiscordGuild/Presences.md) { get; set; } | Presences of the members in the guild will only include non-offline members if the size is greater than large threshold |
| [PublicUpdatesChannelId](DiscordGuild/PublicUpdatesChannelId.md) { get; set; } | The maximum amount of users in a video channel |
| [Roles](DiscordGuild/Roles.md) { get; set; } | Roles in the guild |
| [RulesChannelId](DiscordGuild/RulesChannelId.md) { get; set; } | The id of the channel where Community guilds can display rules and/or guidelines |
| [SafetyAlertsChannelId](DiscordGuild/SafetyAlertsChannelId.md) { get; set; } | The ID of the channel where admins and moderators of Community guilds receive safety alerts from Discord |
| [ScheduledEvents](DiscordGuild/ScheduledEvents.md) { get; set; } | The scheduled events in the guild [`DiscordSticker`](../Stickers/DiscordSticker.md) |
| [Splash](DiscordGuild/Splash.md) { get; set; } | Splash hash |
| [SplashUrl](DiscordGuild/SplashUrl.md) { get; } | Returns the Guilds Splash Url |
| [StageInstances](DiscordGuild/StageInstances.md) { get; set; } | Stage instances in the guild [`StageInstance`](../Channels/Stages/StageInstance.md) |
| [Stickers](DiscordGuild/Stickers.md) { get; set; } | Custom guild stickers [`DiscordSticker`](../Stickers/DiscordSticker.md) |
| [SystemChannelFlags](DiscordGuild/SystemChannelFlags.md) { get; set; } | System channel flags See [`SystemChannelFlags`](./DiscordGuild/SystemChannelFlags.md) |
| [SystemChannelId](DiscordGuild/SystemChannelId.md) { get; set; } | The id of the channel where guild notices such as welcome messages and boost events are posted |
| [Threads](DiscordGuild/Threads.md) { get; set; } | All active threads in the guild that current user has permission to view |
| [Unavailable](DiscordGuild/Unavailable.md) { get; set; } | True if this guild is unavailable due to an outage |
| [VanityUrlCode](DiscordGuild/VanityUrlCode.md) { get; set; } | The vanity url code for the guild |
| [VerificationLevel](DiscordGuild/VerificationLevel.md) { get; set; } | Verification level |
| [VoiceStates](DiscordGuild/VoiceStates.md) { get; set; } | States of members currently in voice channels; lacks the guild_id key |
| [WelcomeScreen](DiscordGuild/WelcomeScreen.md) { get; set; } | The welcome screen of a Community guild Shown to new members, returned in an Invite's guild object |
| [WidgetChannelId](DiscordGuild/WidgetChannelId.md) { get; set; } | The channel id that the widget will generate an invite to, or null if set to no invite |
| [WidgetEnabled](DiscordGuild/WidgetEnabled.md) { get; set; } | True if the server widget is enabled |
| [AddMember](DiscordGuild/AddMember.md)(…) | Adds a user to the guild, provided you have a valid oauth2 access token for the user with the guilds.join scope. See [Add Guild Member](https://discord.com/developers/docs/resources/guild#add-guild-member) |
| [AddMemberRole](DiscordGuild/AddMemberRole.md)(…) | Adds a role to a guild member. Requires the MANAGE_ROLES permission. See [Add Guild Member Role](https://discord.com/developers/docs/resources/guild#add-guild-member-role) (2 methods) |
| [BeginPrune](DiscordGuild/BeginPrune.md)(…) | Begin a prune operation. Requires the KICK_MEMBERS permission. See [Begin Guild Prune](https://discord.com/developers/docs/resources/guild#begin-guild-prune) |
| [CreateAutoModRule](DiscordGuild/CreateAutoModRule.md)(…) |  |
| [CreateBan](DiscordGuild/CreateBan.md)(…) | Create a guild ban, and optionally delete previous messages sent by the banned user. Requires the BAN_MEMBERS permission. See [Create Guild Ban](https://discord.com/developers/docs/resources/guild#create-guild-ban) (2 methods) |
| [CreateChannel](DiscordGuild/CreateChannel.md)(…) | Create a new channel object for the guild. Requires the MANAGE_CHANNELS permission. See [Create Guild Channel](https://discord.com/developers/docs/resources/guild#create-guild-channel) |
| [CreateEmoji](DiscordGuild/CreateEmoji.md)(…) | Create a new emoji for the guild. Requires the MANAGE_EMOJIS permission. Returns the new emoji object on success. See [Create Guild Emoji](https://discord.com/developers/docs/resources/emoji#create-guild-emoji) |
| [CreateRole](DiscordGuild/CreateRole.md)(…) | Create a new role for the guild. Requires the MANAGE_ROLES permission. Returns the new role object on success. See [Create Guild Role](https://discord.com/developers/docs/resources/guild#create-guild-role) |
| [CreateSticker](DiscordGuild/CreateSticker.md)(…) | Create a new sticker for the guild. Requires the MANAGE_EMOJIS_AND_STICKERS permission. Returns the new sticker object on success. See [Create Guild Sticker](https://discord.com/developers/docs/resources/sticker#create-guild-sticker) |
| [Delete](DiscordGuild/Delete.md)(…) | Delete a guild permanently. User must be owner See [Delete Guild](https://discord.com/developers/docs/resources/guild#delete-guild) |
| [DeleteEmoji](DiscordGuild/DeleteEmoji.md)(…) | Delete the given emoji. Requires the MANAGE_EMOJIS permission. See [Delete Guild Emoji](https://discord.com/developers/docs/resources/emoji#delete-guild-emoji) |
| [DeleteIntegration](DiscordGuild/DeleteIntegration.md)(…) | Delete the attached integration object for the guild. Deletes any associated webhooks and kicks the associated bot if there is one. Requires the MANAGE_GUILD permission. See [Delete Guild Integration](https://discord.com/developers/docs/resources/guild#delete-guild-integration) (2 methods) |
| [DeleteRole](DiscordGuild/DeleteRole.md)(…) | Delete a guild role. Requires the MANAGE_ROLES permission See [Delete Guild Role](https://discord.com/developers/docs/resources/guild#delete-guild-role) (2 methods) |
| [DeleteSticker](DiscordGuild/DeleteSticker.md)(…) | Delete the given sticker. Requires the MANAGE_EMOJIS_AND_STICKERS permission. |
| [Edit](DiscordGuild/Edit.md)(…) | Modify a guild's settings. Requires the MANAGE_GUILD permission. See [Modify Guild](https://discord.com/developers/docs/resources/guild#modify-guild) |
| [EditChannelPositions](DiscordGuild/EditChannelPositions.md)(…) | Modify the positions of a set of channel objects for the guild. Requires MANAGE_CHANNELS permission. Only channels to be modified are required, with the minimum being a swap between at least two channels. See [Modify Guild Channel Positions](https://discord.com/developers/docs/resources/guild#modify-guild-channel-positions) |
| [EditCurrentMember](DiscordGuild/EditCurrentMember.md)(…) | Modifies the current members nickname in the guild See [Modify Current Member](https://discord.com/developers/docs/resources/guild#modify-current-member) |
| [EditCurrentUserVoiceState](DiscordGuild/EditCurrentUserVoiceState.md)(…) | Modifies the current user's voice state. See [Update Current User Voice State](https://discord.com/developers/docs/resources/guild#modify-current-user-voice-state) |
| [EditEmoji](DiscordGuild/EditEmoji.md)(…) | Modify the given emoji. Requires the MANAGE_EMOJIS permission. Returns the updated emoji object on success. See [Modify Guild Emoji](https://discord.com/developers/docs/resources/emoji#modify-guild-emoji) |
| [EditMember](DiscordGuild/EditMember.md)(…) | Modify attributes of a guild member See [Modify Guild Member](https://discord.com/developers/docs/resources/guild#modify-guild-member) |
| [EditMemberNick](DiscordGuild/EditMemberNick.md)(…) | Modify attributes of a guild member See [Modify Guild Member](https://discord.com/developers/docs/resources/guild#modify-guild-member) |
| [EditMfaLevel](DiscordGuild/EditMfaLevel.md)(…) | Modify a guild's MFA level. Requires guild ownership. See [Modify Guild MFA Level](https://discord.com/developers/docs/resources/guild#modify-guild-mfa-level) |
| [EditRole](DiscordGuild/EditRole.md)(…) | Modify a guild role. Requires the MANAGE_ROLES permission. Returns the updated role on success. See [Modify Guild Role](https://discord.com/developers/docs/resources/guild#modify-guild-role) (2 methods) |
| [EditRolePositions](DiscordGuild/EditRolePositions.md)(…) | Modify the positions of a set of role objects for the guild. Requires the MANAGE_ROLES permission. Returns a list of all of the guild's role objects on success. See [Modify Guild Role Positions](https://discord.com/developers/docs/resources/guild#modify-guild-role-positions) |
| [EditSticker](DiscordGuild/EditSticker.md)(…) | Modify the given sticker. Requires the MANAGE_EMOJIS_AND_STICKERS permission. Returns the updated sticker object on success. See [Modify Guild Sticker](https://discord.com/developers/docs/resources/sticker#modify-guild-sticker) |
| [EditUserVoiceState](DiscordGuild/EditUserVoiceState.md)(…) | Modifies another user's voice state. See [Update Users Voice State](https://discord.com/developers/docs/resources/guild#modify-user-voice-state) |
| [EditWelcomeScreen](DiscordGuild/EditWelcomeScreen.md)(…) | Modify the guild's Welcome Screen. Requires the MANAGE_GUILD permission. Returns the updated Welcome Screen object. |
| [EditWidget](DiscordGuild/EditWidget.md)(…) | Modify a guild widget object for the guild. Requires the MANAGE_GUILD permission. See [Modify Guild Widget](https://discord.com/developers/docs/resources/guild#modify-guild-widget) |
| [GetAutoModRule](DiscordGuild/GetAutoModRule.md)(…) |  |
| [GetBan](DiscordGuild/GetBan.md)(…) | Returns a guild ban for a specific user See [Get Guild Ban](https://discord.com/developers/docs/resources/guild#get-guild-ban) Returns 404 if not found |
| [GetBans](DiscordGuild/GetBans.md)(…) | Returns a list of ban objects for the users banned from this guild. See [Get Guild Bans](https://discord.com/developers/docs/resources/guild#get-guild-bans) |
| [GetBoosterRole](DiscordGuild/GetBoosterRole.md)() | Returns the booster role for the guild if it exists |
| [GetChannel](DiscordGuild/GetChannel.md)(…) | Returns a channel with the given name (Case Insensitive) (2 methods) |
| [GetChannels](DiscordGuild/GetChannels.md)(…) | Returns a list of guild channel objects. Does not include threads See [Get Guild Channels](https://discord.com/developers/docs/resources/guild#get-guild-channels) |
| [GetEmoji](DiscordGuild/GetEmoji.md)(…) | Finds guild emoji by name (2 methods) |
| [GetIntegrations](DiscordGuild/GetIntegrations.md)(…) | Returns a list of integration objects for the guild. Requires the MANAGE_GUILD permission. See [Get Guild Integrations](https://discord.com/developers/docs/resources/guild#get-guild-integrations) |
| [GetInvites](DiscordGuild/GetInvites.md)(…) | Returns a list of invite objects (with invite metadata) for the guild. Requires the MANAGE_GUILD permission. See [Get Guild Invites](https://discord.com/developers/docs/resources/guild#get-guild-invites) |
| [GetMember](DiscordGuild/GetMember.md)(…) | Returns a GuildMember with the given username (Case Insensitive) (3 methods) |
| [GetOnboarding](DiscordGuild/GetOnboarding.md)(…) | Returns the [`GuildOnboarding`](./Onboarding/GuildOnboarding.md) for the guild. |
| [GetParentChannel](DiscordGuild/GetParentChannel.md)(…) | Returns the parent channel for a channel if it exists |
| [GetPruneCount](DiscordGuild/GetPruneCount.md)(…) | Returns an object with one 'pruned' key indicating the number of members that would be removed in a prune operation. Requires the KICK_MEMBERS permission. See [Get Guild Prune Count](https://discord.com/developers/docs/resources/guild#get-guild-prune-count) |
| [GetRole](DiscordGuild/GetRole.md)(…) | Returns a Role with the given name (Case Insensitive) |
| [GetRoles](DiscordGuild/GetRoles.md)(…) | Returns a list of role objects for the guild. See [Get Guild Roles](https://discord.com/developers/docs/resources/guild#get-guild-roles) |
| [GetSticker](DiscordGuild/GetSticker.md)(…) | Returns a sticker object for the given guild and sticker IDs. Includes the user field if the bot has the MANAGE_EMOJIS_AND_STICKERS permission. See [Get Guild Sticker](https://discord.com/developers/docs/resources/sticker#get-guild-sticker) |
| [GetUserPermissions](DiscordGuild/GetUserPermissions.md)(…) | Returns the user permissions for the given user ID |
| [GetVanityUrl](DiscordGuild/GetVanityUrl.md)(…) | Returns a partial invite object for guilds with that feature enabled. Requires the MANAGE_GUILD permission. Code will be null if a vanity url for the guild is not set. |
| [GetVoiceRegions](DiscordGuild/GetVoiceRegions.md)(…) | Returns a list of voice region objects for the guild. Unlike the similar /voice route, this returns VIP servers when the guild is VIP-enabled. See [Get Guild Voice Regions](https://discord.com/developers/docs/resources/guild#get-guild-voice-regions) |
| [GetWelcomeScreen](DiscordGuild/GetWelcomeScreen.md)(…) | Returns the Welcome Screen object for the guild. Requires the `MANAGE_GUILD` permission. |
| [GetWidget](DiscordGuild/GetWidget.md)(…) | Returns the widget for the guild. See [Get Guild Widget](https://discord.com/developers/docs/resources/guild#get-guild-widget) |
| [GetWidgetSettings](DiscordGuild/GetWidgetSettings.md)(…) | Returns a guild widget object. Requires the MANAGE_GUILD permission. See [Get Guild Widget Settings](https://discord.com/developers/docs/resources/guild#get-guild-widget-settings) |
| [ListActiveThreads](DiscordGuild/ListActiveThreads.md)(…) | Returns all active threads in the guild, including public and private threads. Threads are ordered by their id, in descending order. See [List Active Threads](https://discord.com/developers/docs/resources/guild#list-active-threads) |
| [ListAutoModRules](DiscordGuild/ListAutoModRules.md)(…) |  |
| [Listembers](DiscordGuild/Listembers.md)(…) | Returns a list of guild member objects that are members of the guild. In the future, this endpoint will be restricted in line with our Privileged Intents |
| [ListEmojis](DiscordGuild/ListEmojis.md)(…) | Returns a list of emoji objects for the given guild. See [List Guild Emojis](https://discord.com/developers/docs/resources/emoji#list-guild-emojis) |
| [ListStickers](DiscordGuild/ListStickers.md)(…) | Returns an array of sticker objects for the given guild. Includes user fields if the bot has the MANAGE_EMOJIS_AND_STICKERS permission. See [List Guild Stickers](https://discord.com/developers/docs/resources/sticker#list-guild-stickers) |
| [RemoveBan](DiscordGuild/RemoveBan.md)(…) | Remove the ban for a user. Requires the BAN_MEMBERS permissions. See [Remove Guild Ban](https://discord.com/developers/docs/resources/guild#remove-guild-ban) |
| [RemoveMember](DiscordGuild/RemoveMember.md)(…) | Remove a member from a guild. Requires KICK_MEMBERS permission. See [Remove Guild Member](https://discord.com/developers/docs/resources/guild#remove-guild-member) (2 methods) |
| [RemoveMemberRole](DiscordGuild/RemoveMemberRole.md)(…) | Removes a role from a guild member. Requires the MANAGE_ROLES permission. See [Remove Guild Member Role](https://discord.com/developers/docs/resources/guild#remove-guild-member-role) (2 methods) |
| [SearchMembers](DiscordGuild/SearchMembers.md)(…) | Searches for guild members by username or nickname |
| static [Create](DiscordGuild/Create.md)(…) | Create a new guild. See [Create Guild](https://discord.com/developers/docs/resources/guild#create-guild) |
| static [Get](DiscordGuild/Get.md)(…) | Returns the guild object for the given id See [Get Guild](https://discord.com/developers/docs/resources/guild#get-guild) |
| static [GetGuildPreview](DiscordGuild/GetGuildPreview.md)(…) | Returns the guild preview object for the given id. If the user is not in the guild, then the guild must be Discoverable. |

## See Also

* interface [ISnowflakeEntity](../../Interfaces/ISnowflakeEntity.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordGuild.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Guilds/DiscordGuild.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
