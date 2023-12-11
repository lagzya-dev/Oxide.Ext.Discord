# Oxide.Ext.Discord assembly

## Oxide.Ext.Discord namespace

| public type | description |
| --- | --- |
| class [DiscordExtension](./Oxide.Ext.Discord/DiscordExtension.md) | Discord Extension that is loaded by Oxide |

## Oxide.Ext.Discord.Attributes namespace

| public type | description |
| --- | --- |
| abstract class [BaseCommandAttribute](./Oxide.Ext.Discord/Attributes/BaseCommandAttribute.md) | Represents a base attribute for commands |
| abstract class [BaseDiscordAttribute](./Oxide.Ext.Discord/Attributes/BaseDiscordAttribute.md) | Base Attribute for all Discord Extension Attributes |
| class [DirectMessageCommandAttribute](./Oxide.Ext.Discord/Attributes/DirectMessageCommandAttribute.md) | Used to identify direct message bot commands |
| class [GuildCommandAttribute](./Oxide.Ext.Discord/Attributes/GuildCommandAttribute.md) | Used to identify guild bot commands |

## Oxide.Ext.Discord.Attributes.ApplicationCommands namespace

| public type | description |
| --- | --- |
| abstract class [BaseApplicationCommandAttribute](./Oxide.Ext.Discord/Attributes/ApplicationCommands/BaseApplicationCommandAttribute.md) | Base attribute for Application Commands |
| class [DiscordApplicationCommandAttribute](./Oxide.Ext.Discord/Attributes/ApplicationCommands/DiscordApplicationCommandAttribute.md) | Discord Application Command Attribute for ApplicationCommand Callback Hook Format: |
| class [DiscordAutoCompleteCommandAttribute](./Oxide.Ext.Discord/Attributes/ApplicationCommands/DiscordAutoCompleteCommandAttribute.md) | Discord Auto Complete Command Attribute for ApplicationCommandAutoComplete Callback Hook Format: |
| class [DiscordMessageComponentCommandAttribute](./Oxide.Ext.Discord/Attributes/ApplicationCommands/DiscordMessageComponentCommandAttribute.md) | Discord Message Component Command Attribute for MessageComponent Callback Hook Format: |
| class [DiscordModalSubmitAttribute](./Oxide.Ext.Discord/Attributes/ApplicationCommands/DiscordModalSubmitAttribute.md) | Discord Message Component Command Attribute for ModalSubmit Callback Hook Format: |

## Oxide.Ext.Discord.Attributes.Pooling namespace

| public type | description |
| --- | --- |
| class [DiscordPoolAttribute](./Oxide.Ext.Discord/Attributes/Pooling/DiscordPoolAttribute.md) | Attribute for setting [`DiscordPluginPool`](./Oxide.Ext.Discord/Pooling/DiscordPluginPool.md) on a plugin |

## Oxide.Ext.Discord.Builders namespace

| public type | description |
| --- | --- |
| class [DiscordEmbedBuilder](./Oxide.Ext.Discord/Builders/DiscordEmbedBuilder.md) | Builds a new DiscordEmbed |
| class [QueryStringBuilder](./Oxide.Ext.Discord/Builders/QueryStringBuilder.md) | Builder used to build query strings for urls |

## Oxide.Ext.Discord.Builders.Ansi namespace

| public type | description |
| --- | --- |
| class [AnsiBuilder](./Oxide.Ext.Discord/Builders/Ansi/AnsiBuilder.md) | Builder for ANSI colored text |
| enum [BackgroundColor](./Oxide.Ext.Discord/Builders/Ansi/BackgroundColor.md) | Ansi Background colors |
| [Flags] enum [FontStyle](./Oxide.Ext.Discord/Builders/Ansi/FontStyle.md) | Font Styles for ANSI text |
| enum [TextColor](./Oxide.Ext.Discord/Builders/Ansi/TextColor.md) | Text Colors for Ansi Text |

## Oxide.Ext.Discord.Builders.ApplicationCommands namespace

| public type | description |
| --- | --- |
| class [ApplicationCommandBuilder](./Oxide.Ext.Discord/Builders/ApplicationCommands/ApplicationCommandBuilder.md) | Builder to use when building application commands |
| class [ApplicationCommandGroupBuilder](./Oxide.Ext.Discord/Builders/ApplicationCommands/ApplicationCommandGroupBuilder.md) | Builder for Sub Command Groups |
| class [ApplicationCommandOptionBuilder](./Oxide.Ext.Discord/Builders/ApplicationCommands/ApplicationCommandOptionBuilder.md) | Represents a Subcommand Option Builder for SubCommands |
| class [ApplicationSubCommandBuilder](./Oxide.Ext.Discord/Builders/ApplicationCommands/ApplicationSubCommandBuilder.md) | Application Sub Command Builder |

## Oxide.Ext.Discord.Builders.Interactions namespace

| public type | description |
| --- | --- |
| abstract class [BaseInteractionMessageBuilder&lt;TMessage,TBuilder&gt;](./Oxide.Ext.Discord/Builders/Interactions/BaseInteractionMessageBuilder%7BTMessage,TBuilder%7D.md) | Represents a builder for [`BaseInteractionMessage`](./Oxide.Ext.Discord/Entities/Interactions/Response/BaseInteractionMessage.md) |
| class [InteractionAutoCompleteBuilder](./Oxide.Ext.Discord/Builders/Interactions/InteractionAutoCompleteBuilder.md) | Builder for Auto Complete Interaction |
| class [InteractionFollowupBuilder](./Oxide.Ext.Discord/Builders/Interactions/InteractionFollowupBuilder.md) | Represents a builder for [`CommandFollowupCreate`](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/CommandFollowupCreate.md) |
| class [InteractionModalBuilder](./Oxide.Ext.Discord/Builders/Interactions/InteractionModalBuilder.md) | Builds a Modal Interaction Response Message |
| class [InteractionResponseBuilder](./Oxide.Ext.Discord/Builders/Interactions/InteractionResponseBuilder.md) | Represents a builder for [`InteractionCallbackData`](./Oxide.Ext.Discord/Entities/Interactions/Response/InteractionCallbackData.md) |

## Oxide.Ext.Discord.Builders.Interactions.AutoComplete namespace

| public type | description |
| --- | --- |
| enum [AutoCompleteSearchMode](./Oxide.Ext.Discord/Builders/Interactions/AutoComplete/AutoCompleteSearchMode.md) | AutoComplete Search Mode for [`InteractionAutoCompleteBuilder`](./Oxide.Ext.Discord/Builders/Interactions/InteractionAutoCompleteBuilder.md) |
| [Flags] enum [PlayerDisplayNameMode](./Oxide.Ext.Discord/Builders/Interactions/AutoComplete/PlayerDisplayNameMode.md) | Player Name Formatting options for [`PlayerNameFormatter`](./Oxide.Ext.Discord/Builders/Interactions/AutoComplete/PlayerNameFormatter.md) |
| class [PlayerNameFormatter](./Oxide.Ext.Discord/Builders/Interactions/AutoComplete/PlayerNameFormatter.md) | Formatter for player names |

## Oxide.Ext.Discord.Builders.MessageComponents namespace

| public type | description |
| --- | --- |
| class [MessageComponentBuilder](./Oxide.Ext.Discord/Builders/MessageComponents/MessageComponentBuilder.md) | Builder for Message Components |
| class [SelectMenuComponentBuilder](./Oxide.Ext.Discord/Builders/MessageComponents/SelectMenuComponentBuilder.md) | Builder for Select Menus |

## Oxide.Ext.Discord.Builders.Messages namespace

| public type | description |
| --- | --- |
| class [DiscordMessageBuilder](./Oxide.Ext.Discord/Builders/Messages/DiscordMessageBuilder.md) | Represents a builder for [`MessageCreate`](./Oxide.Ext.Discord/Entities/Messages/MessageCreate.md) |
| class [WebhookMessageBuilder](./Oxide.Ext.Discord/Builders/Messages/WebhookMessageBuilder.md) | Represents a builder for [`WebhookMessageBuilder`](./Oxide.Ext.Discord/Builders/Messages/WebhookMessageBuilder.md) |

## Oxide.Ext.Discord.Builders.Messages.BaseBuilders namespace

| public type | description |
| --- | --- |
| abstract class [BaseChannelMessageBuilder&lt;TMessage,TBuilder&gt;](./Oxide.Ext.Discord/Builders/Messages/BaseBuilders/BaseChannelMessageBuilder%7BTMessage,TBuilder%7D.md) | Represents a builder for [`MessageCreate`](./Oxide.Ext.Discord/Entities/Messages/MessageCreate.md) |
| abstract class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./Oxide.Ext.Discord/Builders/Messages/BaseBuilders/BaseMessageBuilder%7BTMessage,TBuilder%7D.md) | Represents a builder for [`BaseMessageCreate`](./Oxide.Ext.Discord/Entities/Messages/BaseMessageCreate.md) |
| abstract class [BaseWebhookMessageBuilder&lt;TMessage,TBuilder&gt;](./Oxide.Ext.Discord/Builders/Messages/BaseBuilders/BaseWebhookMessageBuilder%7BTMessage,TBuilder%7D.md) | Represents a builder for [`WebhookCreateMessage`](./Oxide.Ext.Discord/Entities/Webhooks/WebhookCreateMessage.md) |

## Oxide.Ext.Discord.Cache namespace

| public type | description |
| --- | --- |
| class [DiscordPluginCache](./Oxide.Ext.Discord/Cache/DiscordPluginCache.md) | Represents a cache for Loaded and Loadable plugins |
| class [EnumCache&lt;T&gt;](./Oxide.Ext.Discord/Cache/EnumCache%7BT%7D.md) | Represents a cache of enum strings |
| class [ServerPlayerCache](./Oxide.Ext.Discord/Cache/ServerPlayerCache.md) | Cache for server IPlayer |
| class [StringCache&lt;T&gt;](./Oxide.Ext.Discord/Cache/StringCache%7BT%7D.md) | Caches strings from {T} ToString method |

## Oxide.Ext.Discord.Cache.Emoji namespace

| public type | description |
| --- | --- |
| class [EmojiCache](./Oxide.Ext.Discord/Cache/Emoji/EmojiCache.md) |  |

## Oxide.Ext.Discord.Cache.Entities namespace

| public type | description |
| --- | --- |
| class [EntityCache&lt;T&gt;](./Oxide.Ext.Discord/Cache/Entities/EntityCache%7BT%7D.md) | Cache for {T} |

## Oxide.Ext.Discord.Callbacks namespace

| public type | description |
| --- | --- |
| abstract class [BaseAsyncCallback](./Oxide.Ext.Discord/Callbacks/BaseAsyncCallback.md) | Represents a base callback to be used when needing a lambda callback so no delegate or class is generated This class is pooled to prevent allocations |
| abstract class [BaseCallback](./Oxide.Ext.Discord/Callbacks/BaseCallback.md) | Represents a base callback to be used when needing a lambda callback so no delegate or class is generated This class is pooled to prevent allocations |
| abstract class [BaseCallback&lt;T&gt;](./Oxide.Ext.Discord/Callbacks/BaseCallback%7BT%7D.md) | Represents a base callback to be used when needing a lambda callback so no delegate or class is generated This class is pooled to prevent allocations |
| abstract class [BaseNextTickCallback](./Oxide.Ext.Discord/Callbacks/BaseNextTickCallback.md) | Represents a callback that calls next tick |

## Oxide.Ext.Discord.Clients namespace

| public type | description |
| --- | --- |
| class [BotClient](./Oxide.Ext.Discord/Clients/BotClient.md) | Represents a bot that is connected to discord |
| class [DiscordClient](./Oxide.Ext.Discord/Clients/DiscordClient.md) | Represents the object a plugin uses to connects to discord |

## Oxide.Ext.Discord.Connections namespace

| public type | description |
| --- | --- |
| class [BotConnection](./Oxide.Ext.Discord/Connections/BotConnection.md) | Bot Connection Settings |
| class [BotTokenData](./Oxide.Ext.Discord/Connections/BotTokenData.md) | Represents the parsed Bot Token data |

## Oxide.Ext.Discord.Constants namespace

| public type | description |
| --- | --- |
| class [DiscordEncoding](./Oxide.Ext.Discord/Constants/DiscordEncoding.md) | Encoding format the Discord Uses |
| static class [DiscordEndpoints](./Oxide.Ext.Discord/Constants/DiscordEndpoints.md) | Discord API endpoint settings |
| static class [DiscordExtHooks](./Oxide.Ext.Discord/Constants/DiscordExtHooks.md) | Represents all hooks available in the discord extension |
| static class [RateLimitHeaders](./Oxide.Ext.Discord/Constants/RateLimitHeaders.md) | Represents [Header Format](https://discord.com/developers/docs/topics/rate-limits#header-format) |

## Oxide.Ext.Discord.Entities namespace

| public type | description |
| --- | --- |
| struct [DiscordColor](./Oxide.Ext.Discord/Entities/DiscordColor.md) | Represents a Discord Color |
| struct [Snowflake](./Oxide.Ext.Discord/Entities/Snowflake.md) | Represents an ID in discord. |

## Oxide.Ext.Discord.Entities.Activities namespace

| public type | description |
| --- | --- |
| class [ActivityAssets](./Oxide.Ext.Discord/Entities/Activities/ActivityAssets.md) | Represents [Activity Assets](https://discord.com/developers/docs/topics/gateway#activity-object-activity-assets) |
| class [ActivityButton](./Oxide.Ext.Discord/Entities/Activities/ActivityButton.md) | Represents [Activity Buttons](https://discord.com/developers/docs/topics/gateway#activity-object-activity-buttons) |
| [Flags] enum [ActivityFlags](./Oxide.Ext.Discord/Entities/Activities/ActivityFlags.md) | Represents [Activity Flags](https://discord.com/developers/docs/topics/gateway#activity-object-activity-flags) |
| class [ActivityParty](./Oxide.Ext.Discord/Entities/Activities/ActivityParty.md) | Represents [Activity Party](https://discord.com/developers/docs/topics/gateway#activity-object-activity-party) |
| class [ActivitySecrets](./Oxide.Ext.Discord/Entities/Activities/ActivitySecrets.md) | Represents [Activity Secrets](https://discord.com/developers/docs/topics/gateway#activity-object-activity-secrets) |
| class [ActivityTimestamps](./Oxide.Ext.Discord/Entities/Activities/ActivityTimestamps.md) | Represents [Activity Timestamps](https://discord.com/developers/docs/topics/gateway#activity-object-activity-timestamps) |
| enum [ActivityType](./Oxide.Ext.Discord/Entities/Activities/ActivityType.md) | Represents [Activity Types](https://discord.com/developers/docs/topics/gateway#activity-object-activity-types) |
| class [DiscordActivity](./Oxide.Ext.Discord/Entities/Activities/DiscordActivity.md) | Represents [Activity Structure](https://discord.com/developers/docs/topics/gateway-events#activity-object) |

## Oxide.Ext.Discord.Entities.Api namespace

| public type | description |
| --- | --- |
| enum [DiscordHttpStatusCode](./Oxide.Ext.Discord/Entities/Api/DiscordHttpStatusCode.md) | Represents possible HTTP Codes sent from discord |
| class [RateLimitResponse](./Oxide.Ext.Discord/Entities/Api/RateLimitResponse.md) | Represents a rate limit response from an API request |
| enum [RequestErrorType](./Oxide.Ext.Discord/Entities/Api/RequestErrorType.md) | Represents a Discord Request Error Type |
| class [RequestResponse](./Oxide.Ext.Discord/Entities/Api/RequestResponse.md) | Represents a REST response from discord |
| class [ResponseError](./Oxide.Ext.Discord/Entities/Api/ResponseError.md) | Error object that is returned to the caller when a request fails |
| class [ResponseErrorMessage](./Oxide.Ext.Discord/Entities/Api/ResponseErrorMessage.md) | Represents an [error from the discord API](https://discord.com/developers/docs/reference#error-messages) |

## Oxide.Ext.Discord.Entities.Applications namespace

| public type | description |
| --- | --- |
| [Flags] enum [ApplicationFlags](./Oxide.Ext.Discord/Entities/Applications/ApplicationFlags.md) | Represents [Application Flags](https://discord.com/developers/docs/resources/application#application-object-application-flags) |
| class [DiscordApplication](./Oxide.Ext.Discord/Entities/Applications/DiscordApplication.md) | Represents [Application Structure](https://discord.com/developers/docs/resources/application#application-object) |
| class [InstallParams](./Oxide.Ext.Discord/Entities/Applications/InstallParams.md) | Represents a [Install Params Structure](https://discord.com/developers/docs/resources/application#install-params-object) |

## Oxide.Ext.Discord.Entities.Applications.RoleConnection namespace

| public type | description |
| --- | --- |
| class [ApplicationRoleConnectionMetadata](./Oxide.Ext.Discord/Entities/Applications/RoleConnection/ApplicationRoleConnectionMetadata.md) | Represents [Application Role Connection Metadata Structure](https://discord.com/developers/docs/resources/application-role-connection-metadata#application-role-connection-metadata-object-application-role-connection-metadata-structure) |
| enum [ApplicationRoleConnectionMetadataType](./Oxide.Ext.Discord/Entities/Applications/RoleConnection/ApplicationRoleConnectionMetadataType.md) | Represents [Application Role Connection Metadata Type](Application Role Connection Metadata Structure) |

## Oxide.Ext.Discord.Entities.AutoMod namespace

| public type | description |
| --- | --- |
| class [AutoModAction](./Oxide.Ext.Discord/Entities/AutoMod/AutoModAction.md) | Represents [Auto Mod Action](https://discord.com/developers/docs/resources/auto-moderation#auto-moderation-action-object) |
| class [AutoModActionMetadata](./Oxide.Ext.Discord/Entities/AutoMod/AutoModActionMetadata.md) | Represents [Auto Mod Action Metadata](https://discord.com/developers/docs/resources/auto-moderation#auto-moderation-action-object-action-metadata) |
| enum [AutoModActionType](./Oxide.Ext.Discord/Entities/AutoMod/AutoModActionType.md) | Represents [Auto Mod Action Types](https://discord.com/developers/docs/resources/auto-moderation#auto-moderation-action-object-action-types) |
| enum [AutoModEventType](./Oxide.Ext.Discord/Entities/AutoMod/AutoModEventType.md) | Represents [Auto Mod Event Type](https://discord.com/developers/docs/resources/auto-moderation#auto-moderation-rule-object-event-types) |
| enum [AutoModKeywordPresetType](./Oxide.Ext.Discord/Entities/AutoMod/AutoModKeywordPresetType.md) | Represents [Auto Mod Keyword Preset Types](https://discord.com/developers/docs/resources/auto-moderation#auto-moderation-rule-object-keyword-preset-types) |
| class [AutoModRule](./Oxide.Ext.Discord/Entities/AutoMod/AutoModRule.md) | Represents [Auto Mod Rule](https://discord.com/developers/docs/resources/auto-moderation#auto-moderation-rule-object) |
| class [AutoModRuleCreate](./Oxide.Ext.Discord/Entities/AutoMod/AutoModRuleCreate.md) | Represents [Auto Mod Rule Create](https://discord.com/developers/docs/resources/auto-moderation#create-auto-moderation-rule-json-params) |
| class [AutoModRuleModify](./Oxide.Ext.Discord/Entities/AutoMod/AutoModRuleModify.md) | Represents [Auto Mod Rule Modify](https://discord.com/developers/docs/resources/auto-moderation#modify-auto-moderation-rule-json-params) |
| class [AutoModTriggerMetadata](./Oxide.Ext.Discord/Entities/AutoMod/AutoModTriggerMetadata.md) | Represents [Auto Mod Trigger Metadata](https://discord.com/developers/docs/resources/auto-moderation#auto-moderation-rule-object-trigger-metadata) |
| enum [AutoModTriggerType](./Oxide.Ext.Discord/Entities/AutoMod/AutoModTriggerType.md) | Represents [Auto Mod Trigger Types](https://discord.com/developers/docs/resources/auto-moderation#auto-moderation-rule-object-trigger-types) |

## Oxide.Ext.Discord.Entities.Channels namespace

| public type | description |
| --- | --- |
| class [ChannelCreate](./Oxide.Ext.Discord/Entities/Channels/ChannelCreate.md) | Represents a [Guild Channel Create Structure](https://discord.com/developers/docs/resources/guild#create-guild-channel-json-params) |
| [Flags] enum [ChannelFlags](./Oxide.Ext.Discord/Entities/Channels/ChannelFlags.md) | Represents [Channel Flags](https://discord.com/developers/docs/resources/channel#channel-object-channel-flags) |
| class [ChannelInvite](./Oxide.Ext.Discord/Entities/Channels/ChannelInvite.md) | Represents a [Channel Invite Structure](https://discord.com/developers/docs/resources/channel#create-channel-invite-json-params) |
| class [ChannelMention](./Oxide.Ext.Discord/Entities/Channels/ChannelMention.md) | Represents a [Channel Mention Structure](https://discord.com/developers/docs/resources/channel#channel-mention-object-channel-mention-structure) in a message |
| class [ChannelMessagesRequest](./Oxide.Ext.Discord/Entities/Channels/ChannelMessagesRequest.md) | Represents [Get Channel Messages Request](https://discord.com/developers/docs/resources/channel#get-channel-messages) |
| enum [ChannelType](./Oxide.Ext.Discord/Entities/Channels/ChannelType.md) | Represents a [Types of Channels](https://discord.com/developers/docs/resources/channel#channel-object-channel-types) |
| class [DiscordChannel](./Oxide.Ext.Discord/Entities/Channels/DiscordChannel.md) | Represents a guild or DM [Channel Structure](https://discord.com/developers/docs/resources/channel#channel-object) within Discord. |
| class [FollowedChannel](./Oxide.Ext.Discord/Entities/Channels/FollowedChannel.md) | Represents a [Followed Channel Structure](https://discord.com/developers/docs/resources/channel#followed-channel-object-followed-channel-structure) from an API response |
| enum [ForumLayoutTypes](./Oxide.Ext.Discord/Entities/Channels/ForumLayoutTypes.md) | Represents a [Forum Layout Types](https://discord.com/developers/docs/resources/channel#channel-object-forum-layout-types) |
| class [ForumTag](./Oxide.Ext.Discord/Entities/Channels/ForumTag.md) | Represents a [Forum Tag Structure](https://discord.com/developers/docs/resources/channel#forum-tag-object) An object that represents a tag that is able to be applied to a thread in a `GUILD_FORUM` or `GUILD_MEDIA` channel. |
| class [GroupDmChannelUpdate](./Oxide.Ext.Discord/Entities/Channels/GroupDmChannelUpdate.md) | Represents a [Group DM Channel Update Structure](https://discord.com/developers/docs/resources/channel#modify-channel-json-params-group-dm) |
| class [GuildChannelUpdate](./Oxide.Ext.Discord/Entities/Channels/GuildChannelUpdate.md) | Represents a [Guild Channel Update Structure](https://discord.com/developers/docs/resources/channel#modify-channel-json-params-guild-channel) |
| class [Overwrite](./Oxide.Ext.Discord/Entities/Channels/Overwrite.md) | Represents a [Overwrite Structure](https://discord.com/developers/docs/resources/channel#overwrite-object-overwrite-structure) |
| enum [PermissionType](./Oxide.Ext.Discord/Entities/Channels/PermissionType.md) | Represents the type of a permission |
| enum [SortOrderType](./Oxide.Ext.Discord/Entities/Channels/SortOrderType.md) | Represents [Sort Order Types](https://discord.com/developers/docs/resources/channel#channel-object-sort-order-types) in Discord |
| enum [VideoQualityMode](./Oxide.Ext.Discord/Entities/Channels/VideoQualityMode.md) | Represents a [Video Quality Mode](https://discord.com/developers/docs/resources/channel#channel-object-video-quality-modes) |

## Oxide.Ext.Discord.Entities.Channels.Stages namespace

| public type | description |
| --- | --- |
| enum [PrivacyLevel](./Oxide.Ext.Discord/Entities/Channels/Stages/PrivacyLevel.md) | Represents a [Stage Privacy Level](https://discord.com/developers/docs/resources/stage-instance#stage-instance-object-privacy-level) within Discord. |
| class [StageInstance](./Oxide.Ext.Discord/Entities/Channels/Stages/StageInstance.md) | Represents a channel [Stage Instance](https://discord.com/developers/docs/resources/stage-instance#stage-instance-object-stage-instance-structure) within Discord. |
| class [StageInstanceCreate](./Oxide.Ext.Discord/Entities/Channels/Stages/StageInstanceCreate.md) | Represents a  href="https://discord.com/developers/docs/resources/stage-instance#create-stage-instance-json-params"&gt;Stage Instance Create Structure |
| class [StageInstanceUpdate](./Oxide.Ext.Discord/Entities/Channels/Stages/StageInstanceUpdate.md) | Represents a [Modify Stage Instance](https://discord.com/developers/docs/resources/stage-instance#modify-stage-instance-json-params) Structure |

## Oxide.Ext.Discord.Entities.Channels.Threads namespace

| public type | description |
| --- | --- |
| class [GetThreadMember](./Oxide.Ext.Discord/Entities/Channels/Threads/GetThreadMember.md) | Represents [Get Thread Member Query String Params](https://discord.com/developers/docs/resources/channel#get-thread-member-query-string-params) |
| class [ListThreadMembers](./Oxide.Ext.Discord/Entities/Channels/Threads/ListThreadMembers.md) | Represents [List Thread Member Query String Params](https://discord.com/developers/docs/resources/channel#list-thread-members-query-string-params) |
| class [ThreadArchivedLookup](./Oxide.Ext.Discord/Entities/Channels/Threads/ThreadArchivedLookup.md) | Represents a [Thread Archive Lookup Structure](https://discord.com/developers/docs/resources/channel#list-public-archived-threads-query-string-params) within Discord. Represents a [Thread Archive Lookup Structure](https://discord.com/developers/docs/resources/channel#list-private-archived-threads-query-string-params) within Discord. Represents a [Thread Archive Lookup Structure](https://discord.com/developers/docs/resources/channel#list-joined-private-archived-threads-query-string-params) within Discord. |
| class [ThreadChannelUpdate](./Oxide.Ext.Discord/Entities/Channels/Threads/ThreadChannelUpdate.md) | Represents a [Thread Channel Update Structure](https://discord.com/developers/docs/resources/channel#modify-channel-json-params-thread) |
| class [ThreadCreate](./Oxide.Ext.Discord/Entities/Channels/Threads/ThreadCreate.md) | Represents a [Thread Create Structure](https://discord.com/developers/docs/resources/channel#start-thread-without-message-json-params) within Discord. |
| class [ThreadCreateFromMessage](./Oxide.Ext.Discord/Entities/Channels/Threads/ThreadCreateFromMessage.md) | Represents a [Thread Create From Message](https://discord.com/developers/docs/resources/channel#start-thread-from-message-json-params) Structure |
| class [ThreadForumCreate](./Oxide.Ext.Discord/Entities/Channels/Threads/ThreadForumCreate.md) | Represents a [Start Thread in Forum Channel](https://discord.com/developers/docs/resources/channel#start-thread-in-forum-channel-jsonform-params) Structure |
| class [ThreadList](./Oxide.Ext.Discord/Entities/Channels/Threads/ThreadList.md) | Represents a [Thread List Structure](https://discord.com/developers/docs/resources/channel#list-active-threads) within Discord. Represents a [Thread List Public Archived Structure](https://discord.com/developers/docs/resources/channel#list-public-archived-threads-response-body) within Discord. Represents a [Thread List Private Archived Structure](https://discord.com/developers/docs/resources/channel#list-private-archived-threads-response-body) within Discord. Represents a [Thread List Private Archived Structure](https://discord.com/developers/docs/resources/guild#list-active-threads) within Discord. |
| class [ThreadMember](./Oxide.Ext.Discord/Entities/Channels/Threads/ThreadMember.md) | Represents a guild or DM [Thread Member Structure](https://discord.com/developers/docs/resources/channel#thread-member-object) within Discord. |
| class [ThreadMetadata](./Oxide.Ext.Discord/Entities/Channels/Threads/ThreadMetadata.md) | Represents a guild or DM [Thread Metadata Structure](https://discord.com/developers/docs/resources/channel#thread-metadata-object-thread-metadata-structure) within Discord. |

## Oxide.Ext.Discord.Entities.Emojis namespace

| public type | description |
| --- | --- |
| class [DefaultReaction](./Oxide.Ext.Discord/Entities/Emojis/DefaultReaction.md) | Represents [Default Reaction Structure](https://discord.com/developers/docs/resources/channel#followed-channel-object) |
| class [DiscordEmoji](./Oxide.Ext.Discord/Entities/Emojis/DiscordEmoji.md) | Represents [Emoji Structure](https://discord.com/developers/docs/resources/emoji#emoji-object) |
| class [EmojiCreate](./Oxide.Ext.Discord/Entities/Emojis/EmojiCreate.md) | Represents [Emoji Create Structure](https://discord.com/developers/docs/resources/emoji#create-guild-emoji-json-params) |
| class [EmojiUpdate](./Oxide.Ext.Discord/Entities/Emojis/EmojiUpdate.md) | Represents [Emoji Update Structure](https://discord.com/developers/docs/resources/emoji#modify-guild-emoji-json-params) |

## Oxide.Ext.Discord.Entities.Gateway namespace

| public type | description |
| --- | --- |
| class [CommandPayload](./Oxide.Ext.Discord/Entities/Gateway/CommandPayload.md) | Represents a command payload |
| class [EventPayload](./Oxide.Ext.Discord/Entities/Gateway/EventPayload.md) | Represents [Gateway Payload Structure](https://discord.com/developers/docs/topics/gateway#payloads) |
| [Flags] enum [GatewayIntents](./Oxide.Ext.Discord/Entities/Gateway/GatewayIntents.md) | Represents [Gateway Intents](https://discord.com/developers/docs/topics/gateway#gateway-intents) These are used to indicate which events your bot / application wants to listen to / have access to |

## Oxide.Ext.Discord.Entities.Gateway.Commands namespace

| public type | description |
| --- | --- |
| class [ClientStatus](./Oxide.Ext.Discord/Entities/Gateway/Commands/ClientStatus.md) | Represents [Client Status Structure](https://discord.com/developers/docs/topics/gateway#client-status-object) |
| class [ConnectionProperties](./Oxide.Ext.Discord/Entities/Gateway/Commands/ConnectionProperties.md) | Represents [Identify Connection Properties](https://discord.com/developers/docs/topics/gateway#identify-identify-connection-properties) |
| enum [GatewayCommandCode](./Oxide.Ext.Discord/Entities/Gateway/Commands/GatewayCommandCode.md) | Represents [Gateway Opcodes](https://discord.com/developers/docs/topics/opcodes-and-status-codes#gateway-gateway-opcodes) |
| class [GuildMembersRequestCommand](./Oxide.Ext.Discord/Entities/Gateway/Commands/GuildMembersRequestCommand.md) | Represents [Request Guild Members](https://discord.com/developers/docs/topics/gateway-events#request-guild-members) |
| class [IdentifyCommand](./Oxide.Ext.Discord/Entities/Gateway/Commands/IdentifyCommand.md) | Represents [Identify](https://discord.com/developers/docs/topics/gateway#identify) Command |
| class [ResumeSessionCommand](./Oxide.Ext.Discord/Entities/Gateway/Commands/ResumeSessionCommand.md) | Represents [Resume](https://discord.com/developers/docs/topics/gateway#resume) |
| class [UpdatePresenceCommand](./Oxide.Ext.Discord/Entities/Gateway/Commands/UpdatePresenceCommand.md) | Represents [Update Status](https://discord.com/developers/docs/topics/gateway#update-presence) |
| class [UpdateVoiceStatusCommand](./Oxide.Ext.Discord/Entities/Gateway/Commands/UpdateVoiceStatusCommand.md) | Represents [Update Voice State](https://discord.com/developers/docs/topics/gateway#update-voice-state) |
| class [WebSocketCommand](./Oxide.Ext.Discord/Entities/Gateway/Commands/WebSocketCommand.md) | Represents a command to be sent over the web socket |

## Oxide.Ext.Discord.Entities.Gateway.Events namespace

| public type | description |
| --- | --- |
| class [AutoModActionExecutionEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/AutoModActionExecutionEvent.md) | Represents [Auto Moderation Action Execution Event](https://discord.com/developers/docs/topics/gateway#auto-moderation-action-execution-auto-moderation-action-execution-event-fields) |
| class [ChannelPinsUpdatedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/ChannelPinsUpdatedEvent.md) | Represents [Channel Pins Update](https://discord.com/developers/docs/topics/gateway#channel-pins-update) |
| enum [GatewayEventCode](./Oxide.Ext.Discord/Entities/Gateway/Events/GatewayEventCode.md) | Represents [Gateway Opcodes](https://discord.com/developers/docs/topics/opcodes-and-status-codes#gateway-gateway-opcodes) |
| class [GatewayHelloEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GatewayHelloEvent.md) | Represents [Hello](https://discord.com/developers/docs/topics/gateway#hello) Sent on connection to the websocket. Defines the heartbeat interval that the client should heartbeat to. |
| class [GatewayReadyEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GatewayReadyEvent.md) | Represents [Ready](https://discord.com/developers/docs/topics/gateway#ready) The ready event is dispatched when a client has completed the initial handshake with the gateway (for new sessions) |
| class [GatewayResumedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GatewayResumedEvent.md) | Represents [Resumed](https://discord.com/developers/docs/topics/gateway#resumed) The resumed event is dispatched when a client has sent a resume payload to the gateway (for resuming existing sessions). |
| class [GuildEmojisUpdatedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GuildEmojisUpdatedEvent.md) | Represents [Guild Emojis Update](https://discord.com/developers/docs/topics/gateway#guild-emojis-update) |
| class [GuildIntegrationsUpdatedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GuildIntegrationsUpdatedEvent.md) | Represents [Guild Integrations Update](https://discord.com/developers/docs/topics/gateway#guild-integrations-update) |
| class [GuildMemberAddedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GuildMemberAddedEvent.md) | Represents [Guild Member Add](https://discord.com/developers/docs/topics/gateway#guild-member-add) |
| class [GuildMemberBannedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GuildMemberBannedEvent.md) | Represents [Guild Ban Add](https://discord.com/developers/docs/topics/gateway#guild-ban-add) Event Represents [Guild Ban Remove](https://discord.com/developers/docs/topics/gateway#guild-ban-remove) Event |
| class [GuildMemberRemovedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GuildMemberRemovedEvent.md) | Represents [Guild Member Remove](https://discord.com/developers/docs/topics/gateway#guild-member-remove) |
| class [GuildMembersChunkEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GuildMembersChunkEvent.md) | Represents [Guild Members Chunk](https://discord.com/developers/docs/topics/gateway#guild-members-chunk) |
| class [GuildMemberUpdatedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GuildMemberUpdatedEvent.md) | Represents [Guild Member Update](https://discord.com/developers/docs/topics/gateway#guild-member-update) |
| class [GuildRoleCreatedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GuildRoleCreatedEvent.md) | Represents [Guild Role Create](https://discord.com/developers/docs/topics/gateway#guild-role-create) |
| class [GuildRoleDeletedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GuildRoleDeletedEvent.md) | Represents [Guild Role Delete](https://discord.com/developers/docs/topics/gateway#guild-role-delete) |
| class [GuildRoleUpdatedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GuildRoleUpdatedEvent.md) | Represents [Guild Role Update](https://discord.com/developers/docs/topics/gateway#guild-role-update) |
| class [GuildScheduleEventUserAddedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GuildScheduleEventUserAddedEvent.md) | Represents a [Guild Scheduled Event User Add Event Fields](https://discord.com/developers/docs/topics/gateway#guild-scheduled-event-user-add-guild-scheduled-event-user-add-event-fields) |
| class [GuildScheduleEventUserRemovedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GuildScheduleEventUserRemovedEvent.md) | Represents a [Guild Scheduled Event User Remove Event Fields](https://discord.com/developers/docs/topics/gateway#guild-scheduled-event-user-remove) |
| class [GuildStickersUpdatedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/GuildStickersUpdatedEvent.md) | Represents [Guild Stickers Update](https://discord.com/developers/docs/topics/gateway#guild-stickers-update) |
| class [IntegrationCreatedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/IntegrationCreatedEvent.md) | Represents a [Integration Create Structure](https://discord.com/developers/docs/topics/gateway#integration-create-integration-create-event-additional-fields) |
| class [IntegrationDeletedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/IntegrationDeletedEvent.md) | Represents a [Integration Delete Structure](https://discord.com/developers/docs/topics/gateway#integration-delete-integration-delete-event-fields) |
| class [IntegrationUpdatedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/IntegrationUpdatedEvent.md) | Represents a [Integration Update Structure](https://discord.com/developers/docs/topics/gateway#integration-update-integration-update-event-additional-fields) |
| class [InviteCreatedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/InviteCreatedEvent.md) | Represents [Invite Create](https://discord.com/developers/docs/topics/gateway#invite-create) |
| class [InviteDeletedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/InviteDeletedEvent.md) | Represents [Invite Delete](https://discord.com/developers/docs/topics/gateway#invite-delete) |
| class [MessageBulkDeletedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/MessageBulkDeletedEvent.md) | Represents [Message Delete Bulk](https://discord.com/developers/docs/topics/gateway#message-delete-bulk) |
| class [MessageDeletedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/MessageDeletedEvent.md) | Represents [Message Delete](https://discord.com/developers/docs/topics/gateway#message-delete) |
| class [MessageReactionAddedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/MessageReactionAddedEvent.md) | Represents [Message Reaction Add](https://discord.com/developers/docs/topics/gateway#message-reaction-add) |
| class [MessageReactionRemovedAllEmojiEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/MessageReactionRemovedAllEmojiEvent.md) | Represents [Message Reaction Remove All](https://discord.com/developers/docs/topics/gateway#message-reaction-remove-emoji-message-reaction-remove-emoji) |
| class [MessageReactionRemovedAllEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/MessageReactionRemovedAllEvent.md) | Represents [Message Reaction Remove All](https://discord.com/developers/docs/topics/gateway#message-reaction-remove-all) |
| class [MessageReactionRemovedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/MessageReactionRemovedEvent.md) | Represents [Message Reaction Remove](https://discord.com/developers/docs/topics/gateway#message-reaction-remove) |
| class [PresenceUpdatedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/PresenceUpdatedEvent.md) | Represents [Presence Update](https://discord.com/developers/docs/topics/gateway#presence-update) |
| class [ThreadListSyncEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/ThreadListSyncEvent.md) | Represents [Thread List Sync](https://discord.com/developers/docs/topics/gateway#thread-list-sync-thread-list-sync-event-fields) |
| class [ThreadMembersUpdatedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/ThreadMembersUpdatedEvent.md) | Represents [Thread Members Update Structure](https://discord.com/developers/docs/topics/gateway#thread-members-update-thread-members-update-event-fields) |
| class [ThreadMemberUpdateEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/ThreadMemberUpdateEvent.md) | Represents [Thread Member Update Structure](https://discord.com/developers/docs/topics/gateway#thread-member-update) |
| class [TypingStartedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/TypingStartedEvent.md) | Represents [Typing Start](https://discord.com/developers/docs/topics/gateway#typing-start) |
| class [VoiceServerUpdatedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/VoiceServerUpdatedEvent.md) | Represents [Voice Server Update](https://discord.com/developers/docs/topics/gateway#voice-server-update) |
| class [WebhooksUpdatedEvent](./Oxide.Ext.Discord/Entities/Gateway/Events/WebhooksUpdatedEvent.md) | Represents [Webhooks Update](https://discord.com/developers/docs/topics/gateway#webhooks-update) |

## Oxide.Ext.Discord.Entities.Guilds namespace

| public type | description |
| --- | --- |
| enum [DefaultNotificationLevel](./Oxide.Ext.Discord/Entities/Guilds/DefaultNotificationLevel.md) | Represents [Default Message Notification Level](https://discord.com/developers/docs/resources/guild#guild-object-default-message-notification-level) |
| class [DiscordGuild](./Oxide.Ext.Discord/Entities/Guilds/DiscordGuild.md) | Represents [Guild Structure](https://discord.com/developers/docs/resources/guild#guild-object) |
| enum [ExplicitContentFilterLevel](./Oxide.Ext.Discord/Entities/Guilds/ExplicitContentFilterLevel.md) | Represents [Explicit Content Filter Level](https://discord.com/developers/docs/resources/guild#guild-object-explicit-content-filter-level) |
| class [GuildBan](./Oxide.Ext.Discord/Entities/Guilds/GuildBan.md) | Represents [Guild Ban Structure](https://discord.com/developers/docs/resources/guild#ban-object-ban-structure) |
| class [GuildBanCreate](./Oxide.Ext.Discord/Entities/Guilds/GuildBanCreate.md) | Represents [Guild Ban Create Structure](https://discord.com/developers/docs/resources/guild#create-guild-ban) |
| class [GuildBansRequest](./Oxide.Ext.Discord/Entities/Guilds/GuildBansRequest.md) | Represents a [Get Guild Bans Query String Params](https://discord.com/developers/docs/resources/guild#get-guild-bans-query-string-params) |
| class [GuildChannelPosition](./Oxide.Ext.Discord/Entities/Guilds/GuildChannelPosition.md) | Represents [Modify Guild Channel Position](https://discord.com/developers/docs/resources/guild#modify-guild-channel-positions) |
| class [GuildCreate](./Oxide.Ext.Discord/Entities/Guilds/GuildCreate.md) | Represents [Create Guild Structure](https://discord.com/developers/docs/resources/guild#create-guild) |
| class [GuildCurrentUserVoiceStateUpdate](./Oxide.Ext.Discord/Entities/Guilds/GuildCurrentUserVoiceStateUpdate.md) | Represents a [Modify Current User Voice State](https://discord.com/developers/docs/resources/guild#modify-current-user-voice-state-json-params) |
| enum [GuildFeatures](./Oxide.Ext.Discord/Entities/Guilds/GuildFeatures.md) | Represents [Guild Features](https://discord.com/developers/docs/resources/guild#guild-object-guild-features) |
| class [GuildListMembers](./Oxide.Ext.Discord/Entities/Guilds/GuildListMembers.md) | Represents a [List Guild Members](https://discord.com/developers/docs/resources/guild#list-guild-members-query-string-params) Stucture |
| class [GuildMember](./Oxide.Ext.Discord/Entities/Guilds/GuildMember.md) | Represents [Guild Member Structure](https://discord.com/developers/docs/resources/guild#guild-member-object-guild-member-structure) |
| class [GuildMemberAdd](./Oxide.Ext.Discord/Entities/Guilds/GuildMemberAdd.md) | Represents [Guild Member Add](https://discord.com/developers/docs/resources/guild#add-guild-member-json-params) |
| [Flags] enum [GuildMemberFlags](./Oxide.Ext.Discord/Entities/Guilds/GuildMemberFlags.md) | Represents [Guild Member Flags](https://discord.com/developers/docs/resources/guild#guild-member-object-guild-member-flags) |
| class [GuildMemberUpdate](./Oxide.Ext.Discord/Entities/Guilds/GuildMemberUpdate.md) | Represents [Guild Member Update Structure](https://discord.com/developers/docs/resources/guild#modify-guild-member-json-params) |
| enum [GuildMfaLevel](./Oxide.Ext.Discord/Entities/Guilds/GuildMfaLevel.md) | Represents [MFA Level](https://discord.com/developers/docs/resources/guild#guild-object-mfa-level) |
| enum [GuildNavigationType](./Oxide.Ext.Discord/Entities/Guilds/GuildNavigationType.md) | Represents [Guild Navigation Types](https://discord.com/developers/docs/reference#message-formatting-guild-navigation-types) |
| enum [GuildNsfwLevel](./Oxide.Ext.Discord/Entities/Guilds/GuildNsfwLevel.md) | Represents [Guild NSFW Level](https://discord.com/developers/docs/resources/guild#guild-nsfw-level) |
| enum [GuildPremiumTier](./Oxide.Ext.Discord/Entities/Guilds/GuildPremiumTier.md) | Represents [Verification Level](https://discord.com/developers/docs/resources/guild#guild-object-verification-level) |
| class [GuildPreview](./Oxide.Ext.Discord/Entities/Guilds/GuildPreview.md) | Represents [Guild Preview Structure](https://discord.com/developers/docs/resources/guild#guild-preview-object) |
| class [GuildPruneBegin](./Oxide.Ext.Discord/Entities/Guilds/GuildPruneBegin.md) | Represents [Guild Prune Begin](https://discord.com/developers/docs/resources/guild#begin-guild-prune) |
| class [GuildPruneGet](./Oxide.Ext.Discord/Entities/Guilds/GuildPruneGet.md) | Represents [Guild Prune Get](https://discord.com/developers/docs/resources/guild#get-guild-prune-count) |
| class [GuildPruneResult](./Oxide.Ext.Discord/Entities/Guilds/GuildPruneResult.md) | Represents [Guild Prune Count Response](https://discord.com/developers/docs/resources/guild#get-guild-prune-count) Represents [Guild Prune Begin Response](https://discord.com/developers/docs/resources/guild#begin-guild-prune) |
| class [GuildRolePosition](./Oxide.Ext.Discord/Entities/Guilds/GuildRolePosition.md) | Represents [Guild Role Position](https://discord.com/developers/docs/resources/guild#modify-guild-role-positions) |
| class [GuildSearchMembers](./Oxide.Ext.Discord/Entities/Guilds/GuildSearchMembers.md) | Represents [Search Guild Members](https://discord.com/developers/docs/resources/guild#search-guild-members-query-string-params) Structure |
| class [GuildUpdate](./Oxide.Ext.Discord/Entities/Guilds/GuildUpdate.md) | Represents [Update Guild Structure](https://discord.com/developers/docs/resources/guild#modify-guild) |
| class [GuildUpdateMfaLevel](./Oxide.Ext.Discord/Entities/Guilds/GuildUpdateMfaLevel.md) | Represents [Guild MFA Level Update](https://discord.com/developers/docs/resources/guild#modify-guild-mfa-level-json-params) |
| class [GuildUserVoiceStateUpdate](./Oxide.Ext.Discord/Entities/Guilds/GuildUserVoiceStateUpdate.md) | Represents a [Modify User Voice State](https://discord.com/developers/docs/resources/guild#modify-user-voice-state-json-params) |
| enum [GuildVerificationLevel](./Oxide.Ext.Discord/Entities/Guilds/GuildVerificationLevel.md) | Represents [Verification Level](https://discord.com/developers/docs/resources/guild#guild-object-verification-level) |
| class [GuildWelcomeScreen](./Oxide.Ext.Discord/Entities/Guilds/GuildWelcomeScreen.md) | Represents [Welcome Screen Structure](https://discord.com/developers/docs/resources/guild#welcome-screen-object) |
| class [GuildWelcomeScreenChannel](./Oxide.Ext.Discord/Entities/Guilds/GuildWelcomeScreenChannel.md) | Represents [Welcome Screen Channel Structure](https://discord.com/developers/docs/resources/guild#welcome-screen-object-welcome-screen-channel-structure) |
| class [GuildWidget](./Oxide.Ext.Discord/Entities/Guilds/GuildWidget.md) | Represents |
| class [GuildWidgetSettings](./Oxide.Ext.Discord/Entities/Guilds/GuildWidgetSettings.md) | Represents [Guild Widget Settings Structure](https://discord.com/developers/docs/resources/guild#guild-widget-object-guild-widget-structure) |
| [Flags] enum [SystemChannelFlags](./Oxide.Ext.Discord/Entities/Guilds/SystemChannelFlags.md) | Represents [System Channel Flags](https://discord.com/developers/docs/resources/guild#guild-object-system-channel-flags) |
| class [WelcomeScreenUpdate](./Oxide.Ext.Discord/Entities/Guilds/WelcomeScreenUpdate.md) | Represents [https://discord.com/developers/docs/resources/guild#modify-guild-welcome-screen](https://discord.com/developers/docs/resources/guild#modify-guild-welcome-screen) |

## Oxide.Ext.Discord.Entities.Guilds.Onboarding namespace

| public type | description |
| --- | --- |
| class [GuildOnboarding](./Oxide.Ext.Discord/Entities/Guilds/Onboarding/GuildOnboarding.md) | Represents [Guild Onboarding Structure](https://discord.com/developers/docs/resources/guild#guild-onboarding-object-guild-onboarding-structure) |
| enum [GuildOnboardingMode](./Oxide.Ext.Discord/Entities/Guilds/Onboarding/GuildOnboardingMode.md) | Represents [Guild Onboarding Mode Structure](https://discord.com/developers/docs/resources/guild#onboarding-mode) |
| class [GuildOnboardingUpdate](./Oxide.Ext.Discord/Entities/Guilds/Onboarding/GuildOnboardingUpdate.md) | Represents [Guild Onboarding Update Structure]() |
| class [OnboardingPrompt](./Oxide.Ext.Discord/Entities/Guilds/Onboarding/OnboardingPrompt.md) | Represents [Onboarding Prompt Structure](https://discord.com/developers/docs/resources/guild#guild-onboarding-object-onboarding-prompt-structure) |
| class [OnboardingPromptOption](./Oxide.Ext.Discord/Entities/Guilds/Onboarding/OnboardingPromptOption.md) | Represents [Prompt Option Structure](https://discord.com/developers/docs/resources/guild#guild-onboarding-object-prompt-option-structure) |
| enum [OnboardingPromptType](./Oxide.Ext.Discord/Entities/Guilds/Onboarding/OnboardingPromptType.md) | Represents [Prompt Types](https://discord.com/developers/docs/resources/guild#guild-onboarding-object-prompt-types) |

## Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents namespace

| public type | description |
| --- | --- |
| class [GuildScheduledEvent](./Oxide.Ext.Discord/Entities/Guilds/ScheduledEvents/GuildScheduledEvent.md) | Represents [Guild Scheduled Event Structure](https://discord.com/developers/docs/resources/guild-scheduled-event) |
| class [ScheduledEventCreate](./Oxide.Ext.Discord/Entities/Guilds/ScheduledEvents/ScheduledEventCreate.md) | Represents [Guild Scheduled Event Create](https://discord.com/developers/docs/resources/guild-scheduled-event#create-guild-scheduled-event) within discord |
| class [ScheduledEventEntityMetadata](./Oxide.Ext.Discord/Entities/Guilds/ScheduledEvents/ScheduledEventEntityMetadata.md) | Represents [Guild Scheduled Event Entity Metadata](https://discord.com/developers/docs/resources/guild-scheduled-event#guild-scheduled-event-object-guild-scheduled-event-entity-metadata) |
| enum [ScheduledEventEntityType](./Oxide.Ext.Discord/Entities/Guilds/ScheduledEvents/ScheduledEventEntityType.md) | Represents [Scheduled Entity Type](https://discord.com/developers/docs/resources/guild-scheduled-event#guild-scheduled-event-object-guild-scheduled-event-entity-types) |
| class [ScheduledEventLookup](./Oxide.Ext.Discord/Entities/Guilds/ScheduledEvents/ScheduledEventLookup.md) | Represents a [Scheduled Event Lookup Structure](https://discord.com/developers/docs/resources/guild-scheduled-event#list-scheduled-events-for-guild-query-string-params) within Discord. |
| enum [ScheduledEventPrivacyLevel](./Oxide.Ext.Discord/Entities/Guilds/ScheduledEvents/ScheduledEventPrivacyLevel.md) | Represents [Guild Scheduled Event Privacy Level](https://discord.com/developers/docs/resources/guild-scheduled-event#guild-scheduled-event-object-guild-scheduled-event-privacy-level) |
| enum [ScheduledEventStatus](./Oxide.Ext.Discord/Entities/Guilds/ScheduledEvents/ScheduledEventStatus.md) | Represents [Guild Scheduled Event Status](https://discord.com/developers/docs/resources/guild-scheduled-event#guild-scheduled-event-object-guild-scheduled-event-status) |
| class [ScheduledEventUpdate](./Oxide.Ext.Discord/Entities/Guilds/ScheduledEvents/ScheduledEventUpdate.md) | Represents [Guild Scheduled Event Update](https://discord.com/developers/docs/resources/guild-scheduled-event#modify-guild-scheduled-event) within discord |
| class [ScheduledEventUser](./Oxide.Ext.Discord/Entities/Guilds/ScheduledEvents/ScheduledEventUser.md) | Represents [Guild Scheduled Event User Object](https://discord.com/developers/docs/resources/guild-scheduled-event#guild-scheduled-event-user-object-guild-scheduled-event-user-structure) within discord |
| class [ScheduledEventUsersLookup](./Oxide.Ext.Discord/Entities/Guilds/ScheduledEvents/ScheduledEventUsersLookup.md) | Represents a [Scheduled Event Lookup Structure](https://discord.com/developers/docs/resources/guild-scheduled-event#list-scheduled-events-for-guild-query-string-params) within Discord. Provide a user id to before and after for pagination. Users will always be returned in ascending order by user_id. If both before and after are provided, only before is respected. Fetching users in-between before and after is not supported. |

## Oxide.Ext.Discord.Entities.Images namespace

| public type | description |
| --- | --- |
| struct [DiscordImageData](./Oxide.Ext.Discord/Entities/Images/DiscordImageData.md) | Represents [Discord Image Data](https://discord.com/developers/docs/reference#image-data) |
| enum [DiscordImageFormat](./Oxide.Ext.Discord/Entities/Images/DiscordImageFormat.md) | Represents [Image Formats](https://discord.com/developers/docs/reference#image-formatting-image-formats) |
| enum [DiscordImageSize](./Oxide.Ext.Discord/Entities/Images/DiscordImageSize.md) | Represents an image size |

## Oxide.Ext.Discord.Entities.Integrations namespace

| public type | description |
| --- | --- |
| class [Integration](./Oxide.Ext.Discord/Entities/Integrations/Integration.md) | Represents [Integration Structure](https://discord.com/developers/docs/resources/guild#integration-object) |
| class [IntegrationAccount](./Oxide.Ext.Discord/Entities/Integrations/IntegrationAccount.md) | Represents [Integration Account Structure](https://discord.com/developers/docs/resources/guild#integration-account-object) |
| class [IntegrationApplication](./Oxide.Ext.Discord/Entities/Integrations/IntegrationApplication.md) | Represents [Integration Application Structure](https://discord.com/developers/docs/resources/guild#integration-application-object) |
| enum [IntegrationExpireBehaviors](./Oxide.Ext.Discord/Entities/Integrations/IntegrationExpireBehaviors.md) | Represents [Integration Expire Behaviors](https://discord.com/developers/docs/resources/guild#integration-object-integration-expire-behaviors) |
| enum [IntegrationType](./Oxide.Ext.Discord/Entities/Integrations/IntegrationType.md) | Represents Integrations types |
| class [IntegrationUpdate](./Oxide.Ext.Discord/Entities/Integrations/IntegrationUpdate.md) | Represents [Integration Update Structure](https://discord.com/developers/docs/resources/guild#modify-guild-integration-json-params) |

## Oxide.Ext.Discord.Entities.Interactions namespace

| public type | description |
| --- | --- |
| class [DiscordInteraction](./Oxide.Ext.Discord/Entities/Interactions/DiscordInteraction.md) | Represents [Interaction Structure](https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-structure) |
| class [InteractionData](./Oxide.Ext.Discord/Entities/Interactions/InteractionData.md) | Represents [ApplicationCommandInteractionData](https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-data) |
| class [InteractionDataArgs](./Oxide.Ext.Discord/Entities/Interactions/InteractionDataArgs.md) | Args supplied for the interaction |
| class [InteractionDataOption](./Oxide.Ext.Discord/Entities/Interactions/InteractionDataOption.md) | Represents [Application Command Interaction Data Option](https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-interaction-data-option-structure) |
| class [InteractionDataParsed](./Oxide.Ext.Discord/Entities/Interactions/InteractionDataParsed.md) | Parses Interaction Data to make it easier to process for application commands |
| class [InteractionDataResolved](./Oxide.Ext.Discord/Entities/Interactions/InteractionDataResolved.md) | Represents [Application Command Interaction Data Option](https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-resolved-data-structure) |
| enum [InteractionResponseType](./Oxide.Ext.Discord/Entities/Interactions/InteractionResponseType.md) | Represents [InteractionResponseType](https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-response-object-interaction-callback-type) |
| enum [InteractionType](./Oxide.Ext.Discord/Entities/Interactions/InteractionType.md) | Represents [InteractionType](https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-type) |

## Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands namespace

| public type | description |
| --- | --- |
| enum [ApplicationCommandType](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/ApplicationCommandType.md) | Represents [Application Command Type](https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-types) |
| class [CommandBulkOverwrite](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/CommandBulkOverwrite.md) | Represents [Bulk Overwrite Guild Application Commands](https://discord.com/developers/docs/interactions/application-commands#bulk-overwrite-guild-application-commands-bulk-application-command-json-params) |
| class [CommandCreate](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/CommandCreate.md) | Represents [Application Command Create](https://discord.com/developers/docs/interactions/application-commands#create-global-application-command-json-params) Represents [Application Command Create](https://discord.com/developers/docs/interactions/application-commands#create-guild-application-command-json-params) |
| class [CommandFollowupCreate](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/CommandFollowupCreate.md) | Represents a [Command Followup](https://discord.com/developers/docs/interactions/receiving-and-responding#create-followup-message) within discord. |
| class [CommandFollowupUpdate](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/CommandFollowupUpdate.md) | Represents a [Command Followup]() within discord. |
| class [CommandOption](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/CommandOption.md) | Represents [ApplicationCommandOption](https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-option-structure) |
| class [CommandOptionChoice](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/CommandOptionChoice.md) | Represents [ApplicationCommandOptionChoice](https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-option-choice-structure) If you specify choices for an option, they are the only valid values for a user to pick |
| enum [CommandOptionType](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/CommandOptionType.md) | Represents [ApplicationCommandOptionType](https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-option-type) |
| class [CommandPermissions](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/CommandPermissions.md) | Represents [ApplicationCommandPermissions](https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-application-command-permissions-structure) |
| enum [CommandPermissionType](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/CommandPermissionType.md) | Represents [ApplicationCommandPermissionType](https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-application-command-permission-type) |
| class [CommandUpdate](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/CommandUpdate.md) | Represents [Application Command Update](https://discord.com/developers/docs/interactions/application-commands#edit-global-application-command-json-params) |
| class [CommandUpdatePermissions](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/CommandUpdatePermissions.md) | Represents [Edit Application Command Permissions](https://discord.com/developers/docs/interactions/application-commands#edit-application-command-permissions-json-params) |
| class [DiscordApplicationCommand](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/DiscordApplicationCommand.md) | Represents [ApplicationCommand](https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-structure) |
| class [GuildCommandPermissions](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/GuildCommandPermissions.md) | Represents [ApplicationCommandPermissions](https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-guild-application-command-permissions-structure) |

## Oxide.Ext.Discord.Entities.Interactions.MessageComponents namespace

| public type | description |
| --- | --- |
| class [ActionRowComponent](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/ActionRowComponent.md) | Represents [Action Row Component](https://discord.com/developers/docs/interactions/message-components#actionrow) within discord |
| abstract class [BaseComponent](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/BaseComponent.md) | Represents [Message Component](https://discord.com/developers/docs/interactions/message-components#component-object) within discord |
| abstract class [BaseInteractableComponent](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/BaseInteractableComponent.md) | Represent a MessageComponent that can be interacted with |
| class [ButtonComponent](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/ButtonComponent.md) | Represents a [Button Component](https://discord.com/developers/docs/interactions/message-components#buttons) within discord. |
| enum [ButtonStyle](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/ButtonStyle.md) | Represents [Button Styles](https://discord.com/developers/docs/interactions/message-components#buttons-button-styles) within Discord.. |
| class [InputTextComponent](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/InputTextComponent.md) | Represents a [Input Text Component](https://discord.com/developers/docs/interactions/message-components#input-text) within discord. |
| enum [InputTextStyles](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/InputTextStyles.md) | Represents a [Input Text Styles](https://discord.com/developers/docs/interactions/message-components#input-text-styles) within discord. |
| enum [MessageComponentType](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/MessageComponentType.md) | Represents a [Message Component Type](https://discord.com/developers/docs/interactions/message-components#component-types) within Discord.. |

## Oxide.Ext.Discord.Entities.Interactions.MessageComponents.SelectMenus namespace

| public type | description |
| --- | --- |
| abstract class [BaseSelectMenuComponent](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/SelectMenus/BaseSelectMenuComponent.md) | Represents a [Select Menus Component](https://discord.com/developers/docs/interactions/message-components#select-menus) within discord. |
| class [ChannelSelectComponent](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/SelectMenus/ChannelSelectComponent.md) | Represents a [Select Menus Component](https://discord.com/developers/docs/interactions/message-components#select-menus) within discord. |
| class [MentionableSelectComponent](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/SelectMenus/MentionableSelectComponent.md) | Represents a [Select Menus Component](https://discord.com/developers/docs/interactions/message-components#select-menus) within discord. |
| class [RoleSelectComponent](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/SelectMenus/RoleSelectComponent.md) | Represents a [Select Menus Component](https://discord.com/developers/docs/interactions/message-components#select-menus) within discord. |
| class [SelectMenuDefaultValue](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/SelectMenus/SelectMenuDefaultValue.md) | Represents a [Select Default Value Structure](https://discord.com/developers/docs/interactions/message-components#select-menu-object-select-default-value-structure) within discord. |
| enum [SelectMenuDefaultValueType](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/SelectMenus/SelectMenuDefaultValueType.md) | Represents a [Select Menus Default Value Type](https://discord.com/developers/docs/interactions/message-components#select-menu-object-select-default-value-structure) within discord. |
| class [SelectMenuOption](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/SelectMenus/SelectMenuOption.md) | Represents a [Select Menu Option Structure](https://discord.com/developers/docs/interactions/message-components#select-option-structure) within discord. |
| class [StringSelectComponent](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/SelectMenus/StringSelectComponent.md) | Represents a [Select Menus Component](https://discord.com/developers/docs/interactions/message-components#select-menus) within discord. |
| class [UserSelectComponent](./Oxide.Ext.Discord/Entities/Interactions/MessageComponents/SelectMenus/UserSelectComponent.md) | Represents a [Select Menus Component](https://discord.com/developers/docs/interactions/message-components#select-menus) within discord. |

## Oxide.Ext.Discord.Entities.Interactions.Response namespace

| public type | description |
| --- | --- |
| abstract class [BaseInteractionMessage](./Oxide.Ext.Discord/Entities/Interactions/Response/BaseInteractionMessage.md) | Represents a Base Message for an interaction |
| abstract class [BaseInteractionResponse](./Oxide.Ext.Discord/Entities/Interactions/Response/BaseInteractionResponse.md) | Represents a Base Interaction response |
| abstract class [BaseInteractionResponse&lt;T&gt;](./Oxide.Ext.Discord/Entities/Interactions/Response/BaseInteractionResponse%7BT%7D.md) | Represents a Base Interaction Response with generic data {T} |
| class [InteractionAutoCompleteMessage](./Oxide.Ext.Discord/Entities/Interactions/Response/InteractionAutoCompleteMessage.md) | Interaction Auto Complete Response Message |
| class [InteractionAutoCompleteResponse](./Oxide.Ext.Discord/Entities/Interactions/Response/InteractionAutoCompleteResponse.md) | Represents an Auto Complete response in Discord |
| class [InteractionCallbackData](./Oxide.Ext.Discord/Entities/Interactions/Response/InteractionCallbackData.md) | Represents [Interaction Application Command Callback Data Structure](https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-response-object-interaction-callback-data-structure) |
| class [InteractionModalMessage](./Oxide.Ext.Discord/Entities/Interactions/Response/InteractionModalMessage.md) | Represents an Interaction Modal Message |
| class [InteractionModalResponse](./Oxide.Ext.Discord/Entities/Interactions/Response/InteractionModalResponse.md) | Represents an Interaction Modal Response |
| class [InteractionResponse](./Oxide.Ext.Discord/Entities/Interactions/Response/InteractionResponse.md) | Represents [Interaction Response](https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-response-object) |

## Oxide.Ext.Discord.Entities.Invites namespace

| public type | description |
| --- | --- |
| class [DiscordInvite](./Oxide.Ext.Discord/Entities/Invites/DiscordInvite.md) | Represents an [Invite Structure](https://discord.com/developers/docs/resources/invite#invite-object) that when used, adds a user to a guild or group DM channel. |
| class [InviteLookup](./Oxide.Ext.Discord/Entities/Invites/InviteLookup.md) | Represents a [Scheduled Event Lookup Structure](https://discord.com/developers/docs/resources/guild-scheduled-event#list-scheduled-events-for-guild-query-string-params) within Discord. |
| class [InviteMetadata](./Oxide.Ext.Discord/Entities/Invites/InviteMetadata.md) | Represents [Invite Metadata Structure](https://discord.com/developers/docs/resources/invite#invite-metadata-object-invite-metadata-structure) |
| class [InviteStageInstance](./Oxide.Ext.Discord/Entities/Invites/InviteStageInstance.md) | Represents an [Invite Stage Instance](https://discord.com/developers/docs/resources/invite#invite-stage-instance-object) |
| enum [TargetUserType](./Oxide.Ext.Discord/Entities/Invites/TargetUserType.md) | Represents [Target User Types](https://discord.com/developers/docs/resources/invite#invite-object-target-user-types) |

## Oxide.Ext.Discord.Entities.Messages namespace

| public type | description |
| --- | --- |
| class [AllowedMentions](./Oxide.Ext.Discord/Entities/Messages/AllowedMentions.md) | Represents a [Allowed Mention Types](https://discord.com/developers/docs/resources/channel#allowed-mentions-object) |
| enum [AllowedMentionTypes](./Oxide.Ext.Discord/Entities/Messages/AllowedMentionTypes.md) | Represents a [Allowed Mention Types](https://discord.com/developers/docs/resources/channel#allowed-mentions-object-allowed-mention-types) for a message |
| [Flags] enum [AttachmentFlags](./Oxide.Ext.Discord/Entities/Messages/AttachmentFlags.md) | Represents a [Attachment flags for an attachment](https://discord.com/developers/docs/resources/channelattachment-flags) |
| abstract class [BaseMessageCreate](./Oxide.Ext.Discord/Entities/Messages/BaseMessageCreate.md) | Represents a base message in discord |
| class [DiscordMessage](./Oxide.Ext.Discord/Entities/Messages/DiscordMessage.md) | Represents a [Message Structure](https://discord.com/developers/docs/resources/channel#message-object) sent in a channel within Discord.. |
| class [MessageActivity](./Oxide.Ext.Discord/Entities/Messages/MessageActivity.md) | Represents a [Message Activity Structure](https://discord.com/developers/docs/resources/channel#message-object-message-activity-structure) |
| enum [MessageActivityType](./Oxide.Ext.Discord/Entities/Messages/MessageActivityType.md) | Represents a [Message Activity Types](https://discord.com/developers/docs/resources/channel#message-object-message-activity-types) |
| class [MessageAttachment](./Oxide.Ext.Discord/Entities/Messages/MessageAttachment.md) | Represents a message [Attachment Structure](https://discord.com/developers/docs/resources/channel#attachment-object) |
| class [MessageCreate](./Oxide.Ext.Discord/Entities/Messages/MessageCreate.md) | Represents a [Message Create Structure](https://discord.com/developers/docs/resources/channel#create-message-jsonform-params) to be created in discord |
| class [MessageFileAttachment](./Oxide.Ext.Discord/Entities/Messages/MessageFileAttachment.md) | Represents a file attachment for a discord message |
| [Flags] enum [MessageFlags](./Oxide.Ext.Discord/Entities/Messages/MessageFlags.md) | Represents [Message Flags](https://discord.com/developers/docs/resources/channel#message-object-message-flags) for a message |
| class [MessageInteraction](./Oxide.Ext.Discord/Entities/Messages/MessageInteraction.md) | Represents a [Message Interaction Structure](https://discord.com/developers/docs/interactions/receiving-and-responding#message-interaction-object) within Discord. |
| class [MessageReaction](./Oxide.Ext.Discord/Entities/Messages/MessageReaction.md) | Represents a [Reaction Structure](https://discord.com/developers/docs/resources/channel#reaction-object) |
| class [MessageReference](./Oxide.Ext.Discord/Entities/Messages/MessageReference.md) | Represents a [Message Reference Structure](https://discord.com/developers/docs/resources/channel#message-reference-object-message-reference-structure) for a message |
| enum [MessageType](./Oxide.Ext.Discord/Entities/Messages/MessageType.md) | Represents [Message Types](https://discord.com/developers/docs/resources/channel#message-object-message-types) |
| class [MessageUpdate](./Oxide.Ext.Discord/Entities/Messages/MessageUpdate.md) | Represents a [Message Update Structure](https://discord.com/developers/docs/resources/channel#edit-message-jsonform-params) sent in a channel within Discord.. |
| class [ReactionCountDetails](./Oxide.Ext.Discord/Entities/Messages/ReactionCountDetails.md) | Represents a [Reaction Count Details Structure](https://discord.com/developers/docs/resources/channel#reaction-count-details-object) |

## Oxide.Ext.Discord.Entities.Messages.Embeds namespace

| public type | description |
| --- | --- |
| class [DiscordEmbed](./Oxide.Ext.Discord/Entities/Messages/Embeds/DiscordEmbed.md) | Represents [Embed Structure](https://discord.com/developers/docs/resources/channel#embed-object) |
| class [EmbedAuthor](./Oxide.Ext.Discord/Entities/Messages/Embeds/EmbedAuthor.md) | Represents [Embed Author Structure](https://discord.com/developers/docs/resources/channel#embed-object-embed-author-structure) |
| class [EmbedField](./Oxide.Ext.Discord/Entities/Messages/Embeds/EmbedField.md) | Represents [Embed Field Structure](https://discord.com/developers/docs/resources/channel#embed-object-embed-field-structure) |
| class [EmbedFooter](./Oxide.Ext.Discord/Entities/Messages/Embeds/EmbedFooter.md) | Represents [Embed Footer Structure](https://discord.com/developers/docs/resources/channel#embed-object-embed-footer-structure) |
| class [EmbedImage](./Oxide.Ext.Discord/Entities/Messages/Embeds/EmbedImage.md) | Represents [Embed Image Structure](https://discord.com/developers/docs/resources/channel#embed-object-embed-image-structure) |
| class [EmbedProvider](./Oxide.Ext.Discord/Entities/Messages/Embeds/EmbedProvider.md) | Represents [Embed Provider Structure](https://discord.com/developers/docs/resources/channel#embed-object-embed-provider-structure) |
| class [EmbedThumbnail](./Oxide.Ext.Discord/Entities/Messages/Embeds/EmbedThumbnail.md) | Represents [Embed Thumbnail Structure](https://discord.com/developers/docs/resources/channel#embed-object-embed-thumbnail-structure) |
| class [EmbedVideo](./Oxide.Ext.Discord/Entities/Messages/Embeds/EmbedVideo.md) | Represents [Embed Video Structure](https://discord.com/developers/docs/resources/channel#embed-object-embed-video-structure) |

## Oxide.Ext.Discord.Entities.Monetization namespace

| public type | description |
| --- | --- |
| class [DiscordEntitlement](./Oxide.Ext.Discord/Entities/Monetization/DiscordEntitlement.md) | Represents a [Entitlement Structure]() |
| enum [EntitlementType](./Oxide.Ext.Discord/Entities/Monetization/EntitlementType.md) | Represents a [Entitlement Type]() |

## Oxide.Ext.Discord.Entities.Permissions namespace

| public type | description |
| --- | --- |
| class [DiscordRole](./Oxide.Ext.Discord/Entities/Permissions/DiscordRole.md) | Represents [Role Structure](https://discord.com/developers/docs/topics/permissions#role-object) |
| [Flags] enum [PermissionFlags](./Oxide.Ext.Discord/Entities/Permissions/PermissionFlags.md) | Represents [Permission Flags](https://discord.com/developers/docs/topics/permissions#permissions-bitwise-permission-flags) for user or role |
| [Flags] enum [RoleFlags](./Oxide.Ext.Discord/Entities/Permissions/RoleFlags.md) | Represents [Role Flags](https://discord.com/developers/docs/topics/permissions#role-flags) |
| class [RoleSubscription](./Oxide.Ext.Discord/Entities/Permissions/RoleSubscription.md) | Represents [Role Subscription Structure](https://discord.com/developers/docs/resources/channel#role-subscription-data-object-role-subscription-data-object-structure) |
| class [RoleTags](./Oxide.Ext.Discord/Entities/Permissions/RoleTags.md) | Represents [Role Tags Structure](https://discord.com/developers/docs/topics/permissions#role-object-role-tags-structure) |

## Oxide.Ext.Discord.Entities.Stickers namespace

| public type | description |
| --- | --- |
| class [DiscordSticker](./Oxide.Ext.Discord/Entities/Stickers/DiscordSticker.md) | Represents a [Discord Sticker Structure](https://discord.com/developers/docs/resources/sticker#sticker-object) |
| class [DiscordStickerPack](./Oxide.Ext.Discord/Entities/Stickers/DiscordStickerPack.md) | Represents a [Sticker Pack Object](https://discord.com/developers/docs/resources/sticker#sticker-pack-object) |
| class [GuildStickerCreate](./Oxide.Ext.Discord/Entities/Stickers/GuildStickerCreate.md) | Represents a [Sticker Pack Object](https://discord.com/developers/docs/resources/sticker#sticker-pack-object) |
| enum [StickerFormatType](./Oxide.Ext.Discord/Entities/Stickers/StickerFormatType.md) | Represents [Sticker Format Types](https://discord.com/developers/docs/resources/sticker#sticker-format-types) |
| enum [StickerType](./Oxide.Ext.Discord/Entities/Stickers/StickerType.md) | Represents a [Sticker Types](https://discord.com/developers/docs/resources/sticker#sticker-types) |

## Oxide.Ext.Discord.Entities.Teams namespace

| public type | description |
| --- | --- |
| class [DiscordTeam](./Oxide.Ext.Discord/Entities/Teams/DiscordTeam.md) | Represents a [Team Object](https://discord.com/developers/docs/topics/teams#data-models-team-object) |
| class [TeamMember](./Oxide.Ext.Discord/Entities/Teams/TeamMember.md) | Represents [Team Members Object](https://discord.com/developers/docs/topics/teams#data-models-team-members-object) |
| enum [TeamMembershipState](./Oxide.Ext.Discord/Entities/Teams/TeamMembershipState.md) | Represents [Membership State Enum](https://discord.com/developers/docs/topics/teams#data-models-membership-state-enum) |
| enum [TeamRole](./Oxide.Ext.Discord/Entities/Teams/TeamRole.md) | Represents [Team Role Types](https://discord.com/developers/docs/topics/teams#team-member-roles-team-member-role-types) |

## Oxide.Ext.Discord.Entities.Users namespace

| public type | description |
| --- | --- |
| class [DiscordUser](./Oxide.Ext.Discord/Entities/Users/DiscordUser.md) | Represents [User Structure](https://discord.com/developers/docs/resources/user#user-object) |
| [Flags] enum [UserFlags](./Oxide.Ext.Discord/Entities/Users/UserFlags.md) | Represents [User Flags](https://discord.com/developers/docs/resources/user#user-object-user-flags) |
| class [UserGuildsRequest](./Oxide.Ext.Discord/Entities/Users/UserGuildsRequest.md) | Represents a [Users Guild Request](https://discord.com/developers/docs/resources/user#get-current-user-guilds-query-string-params) |
| class [UserModifyCurrent](./Oxide.Ext.Discord/Entities/Users/UserModifyCurrent.md) | Represents a [Modify Current User Structure](https://discord.com/developers/docs/resources/user#modify-current-user-json-params) |
| enum [UserPremiumType](./Oxide.Ext.Discord/Entities/Users/UserPremiumType.md) | Represents Discord User [Premium Types](https://discord.com/developers/docs/resources/user#user-object-premium-types) |
| enum [UserStatusType](./Oxide.Ext.Discord/Entities/Users/UserStatusType.md) | Represents Discord User [Status Types](https://discord.com/developers/docs/topics/gateway#update-status-status-types) |

## Oxide.Ext.Discord.Entities.Users.Connections namespace

| public type | description |
| --- | --- |
| class [Connection](./Oxide.Ext.Discord/Entities/Users/Connections/Connection.md) | Represents a Discord Users [Connection](https://discord.com/developers/docs/resources/user#connection-object) |
| enum [ConnectionType](./Oxide.Ext.Discord/Entities/Users/Connections/ConnectionType.md) | Represents a [Connection Type](https://discord.com/developers/docs/resources/user#connection-object-connection-structure) for a connection |
| enum [ConnectionVisibilityType](./Oxide.Ext.Discord/Entities/Users/Connections/ConnectionVisibilityType.md) | Represents connection [Visibility Types](https://discord.com/developers/docs/resources/user#connection-object-visibility-types) |

## Oxide.Ext.Discord.Entities.Voice namespace

| public type | description |
| --- | --- |
| class [VoiceRegion](./Oxide.Ext.Discord/Entities/Voice/VoiceRegion.md) | Represents [Voice Region Structure](https://discord.com/developers/docs/resources/voice#voice-region-object) |
| class [VoiceState](./Oxide.Ext.Discord/Entities/Voice/VoiceState.md) | Represents [Voice State Structure](https://discord.com/developers/docs/resources/voice#voice-state-object) |

## Oxide.Ext.Discord.Entities.Webhooks namespace

| public type | description |
| --- | --- |
| class [DiscordWebhook](./Oxide.Ext.Discord/Entities/Webhooks/DiscordWebhook.md) | Represents [Webhook Structure](https://discord.com/developers/docs/resources/webhook#webhook-object) |
| class [WebhookCreate](./Oxide.Ext.Discord/Entities/Webhooks/WebhookCreate.md) | Represents a [Webhook Create Structure](https://discord.com/developers/docs/resources/webhook#create-webhook-json-params) |
| class [WebhookCreateMessage](./Oxide.Ext.Discord/Entities/Webhooks/WebhookCreateMessage.md) | Represents [Webhook Create Message](https://discord.com/developers/docs/resources/webhook#execute-webhook-jsonform-params) |
| class [WebhookEdit](./Oxide.Ext.Discord/Entities/Webhooks/WebhookEdit.md) | Represents a [Webhook Create Structure](https://discord.com/developers/docs/resources/webhook#create-webhook-json-params) |
| class [WebhookEditMessage](./Oxide.Ext.Discord/Entities/Webhooks/WebhookEditMessage.md) | Represents [Webhook Edit Message Structure](https://discord.com/developers/docs/resources/webhook#edit-webhook-message-jsonform-params) |
| class [WebhookExecuteParams](./Oxide.Ext.Discord/Entities/Webhooks/WebhookExecuteParams.md) | Represents parameters to execute a webhook |
| class [WebhookMessageParams](./Oxide.Ext.Discord/Entities/Webhooks/WebhookMessageParams.md) | Represents webhook message query string parameters |
| enum [WebhookSendType](./Oxide.Ext.Discord/Entities/Webhooks/WebhookSendType.md) | Use to control which webhook execute url to call |
| enum [WebhookType](./Oxide.Ext.Discord/Entities/Webhooks/WebhookType.md) | Represents [Webhook Types](https://discord.com/developers/docs/resources/webhook#webhook-object-webhook-types) |

## Oxide.Ext.Discord.Exceptions namespace

| public type | description |
| --- | --- |
| abstract class [BaseDiscordException](./Oxide.Ext.Discord/Exceptions/BaseDiscordException.md) | Represents a base discord extension |
| class [DiscordClientException](./Oxide.Ext.Discord/Exceptions/DiscordClientException.md) | Exceptions for the [`DiscordClient`](./Oxide.Ext.Discord/Clients/DiscordClient.md) |
| class [TokenMismatchException](./Oxide.Ext.Discord/Exceptions/TokenMismatchException.md) | Represents a bot token mismatch |

## Oxide.Ext.Discord.Exceptions.Builders namespace

| public type | description |
| --- | --- |
| class [ApplicationCommandBuilderException](./Oxide.Ext.Discord/Exceptions/Builders/ApplicationCommandBuilderException.md) | Represents an error when building Application Commands |
| class [InteractionResponseBuilderException](./Oxide.Ext.Discord/Exceptions/Builders/InteractionResponseBuilderException.md) | Represents an Interaction Response Builder Exception |
| class [MessageComponentBuilderException](./Oxide.Ext.Discord/Exceptions/Builders/MessageComponentBuilderException.md) | Represents an exception in Message Component Builder |

## Oxide.Ext.Discord.Exceptions.Entities namespace

| public type | description |
| --- | --- |
| class [InvalidSnowflakeException](./Oxide.Ext.Discord/Exceptions/Entities/InvalidSnowflakeException.md) | Exception thrown when an invalid Snowflake ID is used in an API call |

## Oxide.Ext.Discord.Exceptions.Entities.Applications namespace

| public type | description |
| --- | --- |
| class [ApplicationRoleConnectionMetadataException](./Oxide.Ext.Discord/Exceptions/Entities/Applications/ApplicationRoleConnectionMetadataException.md) | Exceptions for [`ApplicationRoleConnectionMetadata`](./Oxide.Ext.Discord/Entities/Applications/RoleConnection/ApplicationRoleConnectionMetadata.md) |
| class [DiscordApplicationException](./Oxide.Ext.Discord/Exceptions/Entities/Applications/DiscordApplicationException.md) | Exceptions for [`DiscordApplication`](./Oxide.Ext.Discord/Entities/Applications/DiscordApplication.md) |

## Oxide.Ext.Discord.Exceptions.Entities.AutoMod namespace

| public type | description |
| --- | --- |
| class [AutoModTriggerMetadataException](./Oxide.Ext.Discord/Exceptions/Entities/AutoMod/AutoModTriggerMetadataException.md) | Exceptions for [`AutoModTriggerMetadata`](./Oxide.Ext.Discord/Entities/AutoMod/AutoModTriggerMetadata.md) |

## Oxide.Ext.Discord.Exceptions.Entities.Channels namespace

| public type | description |
| --- | --- |
| class [InvalidChannelException](./Oxide.Ext.Discord/Exceptions/Entities/Channels/InvalidChannelException.md) | Represents using an invalid channel |
| class [InvalidChannelInviteException](./Oxide.Ext.Discord/Exceptions/Entities/Channels/InvalidChannelInviteException.md) | Represents an error in channel invite |
| class [InvalidForumTagException](./Oxide.Ext.Discord/Exceptions/Entities/Channels/InvalidForumTagException.md) | Represents an exception for channel threads |
| class [InvalidThreadException](./Oxide.Ext.Discord/Exceptions/Entities/Channels/InvalidThreadException.md) | Represents an exception for channel threads |

## Oxide.Ext.Discord.Exceptions.Entities.Emojis namespace

| public type | description |
| --- | --- |
| class [InvalidEmojiException](./Oxide.Ext.Discord/Exceptions/Entities/Emojis/InvalidEmojiException.md) | Error thrown when an emoji string fails validation |

## Oxide.Ext.Discord.Exceptions.Entities.Guild namespace

| public type | description |
| --- | --- |
| class [InvalidGuildBanException](./Oxide.Ext.Discord/Exceptions/Entities/Guild/InvalidGuildBanException.md) | Represents an error in channel ban |
| class [InvalidGuildException](./Oxide.Ext.Discord/Exceptions/Entities/Guild/InvalidGuildException.md) | Represents an exception in guild |
| class [InvalidGuildListMembersException](./Oxide.Ext.Discord/Exceptions/Entities/Guild/InvalidGuildListMembersException.md) | Represents an exception in guid list request |
| class [InvalidGuildMemberException](./Oxide.Ext.Discord/Exceptions/Entities/Guild/InvalidGuildMemberException.md) | Represents an exception in guild member |
| class [InvalidGuildPruneException](./Oxide.Ext.Discord/Exceptions/Entities/Guild/InvalidGuildPruneException.md) | Represents an exception in guild prune requests |
| class [InvalidGuildRoleException](./Oxide.Ext.Discord/Exceptions/Entities/Guild/InvalidGuildRoleException.md) | Represents an exception for invalid guild roles |
| class [InvalidGuildSearchMembersException](./Oxide.Ext.Discord/Exceptions/Entities/Guild/InvalidGuildSearchMembersException.md) | Represents an exception in guild member search requests |

## Oxide.Ext.Discord.Exceptions.Entities.Guild.ScheduledEvents namespace

| public type | description |
| --- | --- |
| class [InvalidGuildScheduledEventException](./Oxide.Ext.Discord/Exceptions/Entities/Guild/ScheduledEvents/InvalidGuildScheduledEventException.md) | Represents an exception in guild scheduled events |
| class [InvalidGuildScheduledEventLookupException](./Oxide.Ext.Discord/Exceptions/Entities/Guild/ScheduledEvents/InvalidGuildScheduledEventLookupException.md) | Represents an exception in guild schedule event lookup requests |

## Oxide.Ext.Discord.Exceptions.Entities.Images namespace

| public type | description |
| --- | --- |
| class [InvalidImageDataException](./Oxide.Ext.Discord/Exceptions/Entities/Images/InvalidImageDataException.md) | Represents an exception in discord image data |

## Oxide.Ext.Discord.Exceptions.Entities.Interactions namespace

| public type | description |
| --- | --- |
| class [InteractionArgException](./Oxide.Ext.Discord/Exceptions/Entities/Interactions/InteractionArgException.md) | Represents an error when an interaction arg does not match the requested type |
| class [InvalidAutoCompleteChoiceException](./Oxide.Ext.Discord/Exceptions/Entities/Interactions/InvalidAutoCompleteChoiceException.md) | Exception for invalid Auto Complete choices |
| class [InvalidInteractionResponseException](./Oxide.Ext.Discord/Exceptions/Entities/Interactions/InvalidInteractionResponseException.md) | Error thrown when an interaction response is invalid |

## Oxide.Ext.Discord.Exceptions.Entities.Interactions.ApplicationCommands namespace

| public type | description |
| --- | --- |
| class [InvalidApplicationCommandException](./Oxide.Ext.Discord/Exceptions/Entities/Interactions/ApplicationCommands/InvalidApplicationCommandException.md) | Represents an invalid application command |
| class [InvalidCommandOptionChoiceException](./Oxide.Ext.Discord/Exceptions/Entities/Interactions/ApplicationCommands/InvalidCommandOptionChoiceException.md) | Represents an error in application command option choice |
| class [InvalidCommandOptionException](./Oxide.Ext.Discord/Exceptions/Entities/Interactions/ApplicationCommands/InvalidCommandOptionException.md) | Represents an error in application command option |

## Oxide.Ext.Discord.Exceptions.Entities.Interactions.MessageComponents namespace

| public type | description |
| --- | --- |
| class [InvalidMessageComponentException](./Oxide.Ext.Discord/Exceptions/Entities/Interactions/MessageComponents/InvalidMessageComponentException.md) | Represents an invalid message component |
| class [InvalidSelectMenuComponentException](./Oxide.Ext.Discord/Exceptions/Entities/Interactions/MessageComponents/InvalidSelectMenuComponentException.md) | Represents an exception for select menu components |

## Oxide.Ext.Discord.Exceptions.Entities.Messages namespace

| public type | description |
| --- | --- |
| class [InvalidEmbedException](./Oxide.Ext.Discord/Exceptions/Entities/Messages/InvalidEmbedException.md) | Represents an invalid embed |
| class [InvalidFileNameException](./Oxide.Ext.Discord/Exceptions/Entities/Messages/InvalidFileNameException.md) | Exception throw when an attachment filename contains invalid characters |
| class [InvalidMessageException](./Oxide.Ext.Discord/Exceptions/Entities/Messages/InvalidMessageException.md) | Represents an invalid message |

## Oxide.Ext.Discord.Exceptions.Entities.Permissions namespace

| public type | description |
| --- | --- |
| class [InvalidDiscordColorException](./Oxide.Ext.Discord/Exceptions/Entities/Permissions/InvalidDiscordColorException.md) | Represents an invalid discord color |

## Oxide.Ext.Discord.Exceptions.Entities.Stickers namespace

| public type | description |
| --- | --- |
| class [InvalidGuildStickerException](./Oxide.Ext.Discord/Exceptions/Entities/Stickers/InvalidGuildStickerException.md) | Represents an exception in guild stickers |

## Oxide.Ext.Discord.Exceptions.Entities.Users namespace

| public type | description |
| --- | --- |
| class [BlockedUserException](./Oxide.Ext.Discord/Exceptions/Entities/Users/BlockedUserException.md) | Exception when a user has blocked receving messages from a bot |
| class [InvalidUserException](./Oxide.Ext.Discord/Exceptions/Entities/Users/InvalidUserException.md) | Represents an exception when modifying a user with invalid data |

## Oxide.Ext.Discord.Exceptions.Entities.Webhooks namespace

| public type | description |
| --- | --- |
| class [InvalidWebhookException](./Oxide.Ext.Discord/Exceptions/Entities/Webhooks/InvalidWebhookException.md) | Represents a Webhook Create Exception |

## Oxide.Ext.Discord.Exceptions.Entities.Websocket namespace

| public type | description |
| --- | --- |
| class [DiscordWebSocketException](./Oxide.Ext.Discord/Exceptions/Entities/Websocket/DiscordWebSocketException.md) | Represents an exception that occured with the websocket |

## Oxide.Ext.Discord.Exceptions.Libraries.DiscordLocale namespace

| public type | description |
| --- | --- |
| class [DiscordLocaleNotFoundException](./Oxide.Ext.Discord/Exceptions/Libraries/DiscordLocale/DiscordLocaleNotFoundException.md) | Exception thrown when Discord Locale is not found |
| class [ServerLocaleNotFoundException](./Oxide.Ext.Discord/Exceptions/Libraries/DiscordLocale/ServerLocaleNotFoundException.md) | Exception thrown when Server Locale is not found |

## Oxide.Ext.Discord.Exceptions.Libraries.Placeholders namespace

| public type | description |
| --- | --- |
| class [InvalidPlaceholderDataException](./Oxide.Ext.Discord/Exceptions/Libraries/Placeholders/InvalidPlaceholderDataException.md) | Exception thrown if [`PlaceholderDataKey`](./Oxide.Ext.Discord/Libraries/Placeholders/PlaceholderDataKey.md) is not valid |
| class [InvalidPlaceholderException](./Oxide.Ext.Discord/Exceptions/Libraries/Placeholders/InvalidPlaceholderException.md) | Exception thrown if [`PlaceholderKey`](./Oxide.Ext.Discord/Libraries/Placeholders/PlaceholderKey.md) is not valid |

## Oxide.Ext.Discord.Exceptions.Libraries.Promise namespace

| public type | description |
| --- | --- |
| class [PromiseCancelledException](./Oxide.Ext.Discord/Exceptions/Libraries/Promise/PromiseCancelledException.md) | Exception when a promised is cancelled |
| class [PromiseException](./Oxide.Ext.Discord/Exceptions/Libraries/Promise/PromiseException.md) | Exceptions for promises |

## Oxide.Ext.Discord.Exceptions.Libraries.Templates namespace

| public type | description |
| --- | --- |
| class [DiscordTemplateException](./Oxide.Ext.Discord/Exceptions/Libraries/Templates/DiscordTemplateException.md) | Exception for Discord Templates |
| class [DuplicateTemplateException](./Oxide.Ext.Discord/Exceptions/Libraries/Templates/DuplicateTemplateException.md) | Thrown when duplicate templates have been registered for the same type, plugin, and name |
| class [InvalidTemplateVersionException](./Oxide.Ext.Discord/Exceptions/Libraries/Templates/InvalidTemplateVersionException.md) | Thrown when the minimum template version is higher than the current template version |

## Oxide.Ext.Discord.Exceptions.Pooling namespace

| public type | description |
| --- | --- |
| class [InvalidPoolException](./Oxide.Ext.Discord/Exceptions/Pooling/InvalidPoolException.md) | An exception when something is invalid with a pool |

## Oxide.Ext.Discord.Extensions namespace

| public type | description |
| --- | --- |
| static class [DateTimeOffsetExt](./Oxide.Ext.Discord/Extensions/DateTimeOffsetExt.md) | DateTimeOffset Extensions |
| static class [DiscordColorExt](./Oxide.Ext.Discord/Extensions/DiscordColorExt.md) | Extensions for Discord Color |
| static class [DiscordUserExt](./Oxide.Ext.Discord/Extensions/DiscordUserExt.md) | Adds extension methods to Discord User to allow sending server chat commands to the player |
| static class [HashExt](./Oxide.Ext.Discord/Extensions/HashExt.md) | Hash extensions |
| static class [IEnumerableExt](./Oxide.Ext.Discord/Extensions/IEnumerableExt.md) | Represents Extension to IEnumerable |
| static class [MathExt](./Oxide.Ext.Discord/Extensions/MathExt.md) | Extensions for math operations |
| static class [PlaceholderKeyExt](./Oxide.Ext.Discord/Extensions/PlaceholderKeyExt.md) | Extensions for placeholder keys |
| static class [PlayerExt](./Oxide.Ext.Discord/Extensions/PlayerExt.md) | IPlayer Extensions for sending Discord Message to an IPlayer |
| static class [PluginExt](./Oxide.Ext.Discord/Extensions/PluginExt.md) | Extension methods for plugins |
| static class [StreamExt](./Oxide.Ext.Discord/Extensions/StreamExt.md) | Stream Extension Methods |
| static class [StringBuilderExt](./Oxide.Ext.Discord/Extensions/StringBuilderExt.md) | StringBuilder extension methods |

## Oxide.Ext.Discord.Factory namespace

| public type | description |
| --- | --- |
| class [DiscordClientFactory](./Oxide.Ext.Discord/Factory/DiscordClientFactory.md) | Factory for creating [`DiscordClient`](./Oxide.Ext.Discord/Clients/DiscordClient.md) |
| class [SnowflakeIdFactory](./Oxide.Ext.Discord/Factory/SnowflakeIdFactory.md) | Generates a unique snowflake ID |

## Oxide.Ext.Discord.Helpers namespace

| public type | description |
| --- | --- |
| static class [DiscordCdn](./Oxide.Ext.Discord/Helpers/DiscordCdn.md) | Represents Discord [CDN Endpoints](https://discord.com/developers/docs/reference#image-formatting-cdn-endpoints) |
| static class [DiscordFormatting](./Oxide.Ext.Discord/Helpers/DiscordFormatting.md) | Represents [Message text formatting options](https://discord.com/developers/docs/reference#message-formatting-formats) |
| static class [ServerFormatting](./Oxide.Ext.Discord/Helpers/ServerFormatting.md) | Server Text Formatting |
| static class [TimeHelpers](./Oxide.Ext.Discord/Helpers/TimeHelpers.md) | Helper methods relating to time |
| enum [TimestampStyles](./Oxide.Ext.Discord/Helpers/TimestampStyles.md) | Available flags for timestamp formatting |

## Oxide.Ext.Discord.Interfaces namespace

| public type | description |
| --- | --- |
| interface [IDiscordCacheable&lt;T&gt;](./Oxide.Ext.Discord/Interfaces/IDiscordCacheable%7BT%7D.md) | Represents entities that are cachable by the DiscordExtension |
| interface [IDiscordPlugin](./Oxide.Ext.Discord/Interfaces/IDiscordPlugin.md) | Represents a plugin that uses the Discord Extension |
| interface [IDiscordQueryString](./Oxide.Ext.Discord/Interfaces/IDiscordQueryString.md) | Interface for Discord Query Strings |
| interface [IFileAttachments](./Oxide.Ext.Discord/Interfaces/IFileAttachments.md) | Represents and interface for entities that can upload files |
| interface [ISnowflakeEntity](./Oxide.Ext.Discord/Interfaces/ISnowflakeEntity.md) | Interface used to get the entity ID from an entity |

## Oxide.Ext.Discord.Interfaces.Entities.Messages namespace

| public type | description |
| --- | --- |
| interface [IDiscordMessageTemplate](./Oxide.Ext.Discord/Interfaces/Entities/Messages/IDiscordMessageTemplate.md) | Interfaces for [`DiscordMessageTemplates`](./Oxide.Ext.Discord/Libraries/Templates/Messages/DiscordMessageTemplates.md) Messages |

## Oxide.Ext.Discord.Interfaces.Logging namespace

| public type | description |
| --- | --- |
| interface [IDebugLoggable](./Oxide.Ext.Discord/Interfaces/Logging/IDebugLoggable.md) | Represents an object that supports debug logging |

## Oxide.Ext.Discord.Interfaces.Promises namespace

| public type | description |
| --- | --- |
| interface [IPendingPromise](./Oxide.Ext.Discord/Interfaces/Promises/IPendingPromise.md) | Represents a promise the is still pending waiting to be resolved |
| interface [IPendingPromise&lt;TPromised&gt;](./Oxide.Ext.Discord/Interfaces/Promises/IPendingPromise%7BTPromised%7D.md) | Represents a promise waiting to be resolved |
| interface [IPromise](./Oxide.Ext.Discord/Interfaces/Promises/IPromise.md) | Implements a non-generic C# promise, this is a promise that simply resolves without delivering a value. https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Promise |
| interface [IPromise&lt;TPromised&gt;](./Oxide.Ext.Discord/Interfaces/Promises/IPromise%7BTPromised%7D.md) | Implements a C# promise. https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Promise |
| interface [IRejectable](./Oxide.Ext.Discord/Interfaces/Promises/IRejectable.md) | Interface for a promise that can be rejected. |

## Oxide.Ext.Discord.Interfaces.Templates namespace

| public type | description |
| --- | --- |
| interface [IBulkTemplate&lt;T&gt;](./Oxide.Ext.Discord/Interfaces/Templates/IBulkTemplate%7BT%7D.md) | Represents a Template that supports bulk operations |

## Oxide.Ext.Discord.Interfaces.Types namespace

| public type | description |
| --- | --- |
| interface [IReadonlySet&lt;T&gt;](./Oxide.Ext.Discord/Interfaces/Types/IReadonlySet%7BT%7D.md) | Represents a ReadOnly interface for ISet |

## Oxide.Ext.Discord.Interfaces.WebSockets namespace

| public type | description |
| --- | --- |
| interface [IWebSocketEventHandler](./Oxide.Ext.Discord/Interfaces/WebSockets/IWebSocketEventHandler.md) | Represents a Handler for Websocket Events |

## Oxide.Ext.Discord.Json.Converters namespace

| public type | description |
| --- | --- |
| class [DiscordColorConverter](./Oxide.Ext.Discord/Json/Converters/DiscordColorConverter.md) | Handles the JSON Serialization / Deserialization for DiscordColor |
| class [DiscordEnumConverter](./Oxide.Ext.Discord/Json/Converters/DiscordEnumConverter.md) | Handles deserializing JSON values as strings. If the value doesn't exist return the default value. |
| class [DiscordImageDataConverter](./Oxide.Ext.Discord/Json/Converters/DiscordImageDataConverter.md) | Represents the JsonConverter for [`DiscordImageData`](./Oxide.Ext.Discord/Entities/Images/DiscordImageData.md) |
| class [EventPayloadConverter](./Oxide.Ext.Discord/Json/Converters/EventPayloadConverter.md) | JSON converter for [`EventPayload`](./Oxide.Ext.Discord/Entities/Gateway/EventPayload.md) |
| class [HashListConverter&lt;TValue&gt;](./Oxide.Ext.Discord/Json/Converters/HashListConverter%7BTValue%7D.md) | Converts to and from a list in JSON to a hash |
| class [MessageComponentsConverter](./Oxide.Ext.Discord/Json/Converters/MessageComponentsConverter.md) | Converter for list of message components |
| class [PermissionFlagsStringConverter](./Oxide.Ext.Discord/Json/Converters/PermissionFlagsStringConverter.md) | Converts Permission Flags to and from a JSON string |
| class [RoleTagsConverter](./Oxide.Ext.Discord/Json/Converters/RoleTagsConverter.md) | Handles converting [`RoleTags`](./Oxide.Ext.Discord/Entities/Permissions/RoleTags.md) This type contains special deserialization types |
| class [SnowflakeConverter](./Oxide.Ext.Discord/Json/Converters/SnowflakeConverter.md) | Converts a snowflake to and from it's JSON string value |
| class [TemplateComponentsConverter](./Oxide.Ext.Discord/Json/Converters/TemplateComponentsConverter.md) | Converter for list of message components |
| class [UnixDateTimeConverter](./Oxide.Ext.Discord/Json/Converters/UnixDateTimeConverter.md) | Converts a DateTimeOffset to and from a json long |

## Oxide.Ext.Discord.Json.Serialization namespace

| public type | description |
| --- | --- |
| class [DiscordJsonReader](./Oxide.Ext.Discord/Json/Serialization/DiscordJsonReader.md) | This is a pooled JSON reader that can read as string, deserialize object, or populate a given object async |
| class [DiscordJsonWriter](./Oxide.Ext.Discord/Json/Serialization/DiscordJsonWriter.md) | This is a pooled JSON writer that can write JSON to a stream |

## Oxide.Ext.Discord.Libraries namespace

| public type | description |
| --- | --- |
| abstract class [BaseDiscordLibrary](./Oxide.Ext.Discord/Libraries/BaseDiscordLibrary.md) | Represents the base class for Discord Libraries |
| abstract class [BaseDiscordLibrary&lt;TLibrary&gt;](./Oxide.Ext.Discord/Libraries/BaseDiscordLibrary%7BTLibrary%7D.md) | Base Discord Library for Oxide Libraries |

## Oxide.Ext.Discord.Libraries.AppCommands namespace

| public type | description |
| --- | --- |
| class [DiscordAppCommand](./Oxide.Ext.Discord/Libraries/AppCommands/DiscordAppCommand.md) | Application Command Oxide Library handler Routes Application Commands to their respective hook method handlers instead of having to manually handle it. |

## Oxide.Ext.Discord.Libraries.Command namespace

| public type | description |
| --- | --- |
| class [DiscordCommand](./Oxide.Ext.Discord/Libraries/Command/DiscordCommand.md) | Represents a library for discord commands |

## Oxide.Ext.Discord.Libraries.Linking namespace

| public type | description |
| --- | --- |
| class [DiscordLink](./Oxide.Ext.Discord/Libraries/Linking/DiscordLink.md) | Represents a library for discord linking |
| interface [IDiscordLinkPlugin](./Oxide.Ext.Discord/Libraries/Linking/IDiscordLinkPlugin.md) | Represents a plugin that supports Discord Link library |
| struct [PlayerId](./Oxide.Ext.Discord/Libraries/Linking/PlayerId.md) | Represents a [`DiscordLink`](./Oxide.Ext.Discord/Libraries/Linking/DiscordLink.md) Player ID |

## Oxide.Ext.Discord.Libraries.Locale namespace

| public type | description |
| --- | --- |
| struct [DiscordLocale](./Oxide.Ext.Discord/Libraries/Locale/DiscordLocale.md) | Represents a Locale in Discord |
| class [DiscordLocales](./Oxide.Ext.Discord/Libraries/Locale/DiscordLocales.md) | Converts discord locale codes into oxide locale codes |
| struct [ServerLocale](./Oxide.Ext.Discord/Libraries/Locale/ServerLocale.md) | Represents a Server Locale |

## Oxide.Ext.Discord.Libraries.Placeholders namespace

| public type | description |
| --- | --- |
| class [DiscordPlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/DiscordPlaceholders.md) | Discord Placeholders Library |
| class [PlaceholderData](./Oxide.Ext.Discord/Libraries/Placeholders/PlaceholderData.md) | Placeholder Data for placeholders |
| struct [PlaceholderDataKey](./Oxide.Ext.Discord/Libraries/Placeholders/PlaceholderDataKey.md) | Represents a Placeholder Data Key This is the key used to store a value into Placeholder Data |
| struct [PlaceholderKey](./Oxide.Ext.Discord/Libraries/Placeholders/PlaceholderKey.md) | Represents a Placeholder Key. This is the key for placeholder usage and lookup |
| class [PlaceholderState](./Oxide.Ext.Discord/Libraries/Placeholders/PlaceholderState.md) | Represents the current state for a matched placeholder |

## Oxide.Ext.Discord.Libraries.Placeholders.Default namespace

| public type | description |
| --- | --- |
| static class [ApplicationCommandPlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/ApplicationCommandPlaceholders.md) | [`DiscordApplicationCommand`](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/DiscordApplicationCommand.md) placeholders |
| static class [ChannelPlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/ChannelPlaceholders.md) | [`DiscordChannel`](./Oxide.Ext.Discord/Entities/Channels/DiscordChannel.md) Placeholders |
| static class [DateTimePlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/DateTimePlaceholders.md) | DateTime placeholders |
| static class [GuildPlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/GuildPlaceholders.md) | [`DiscordGuild`](./Oxide.Ext.Discord/Entities/Guilds/DiscordGuild.md) placeholders |
| static class [InteractionPlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/InteractionPlaceholders.md) | [`DiscordInteraction`](./Oxide.Ext.Discord/Entities/Interactions/DiscordInteraction.md) placeholders |
| static class [MemberPlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/MemberPlaceholders.md) | [`GuildMember`](./Oxide.Ext.Discord/Entities/Guilds/GuildMember.md) placeholders |
| static class [MessagePlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/MessagePlaceholders.md) | [`DiscordMessage`](./Oxide.Ext.Discord/Entities/Messages/DiscordMessage.md) placeholders |
| static class [PlayerPlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/PlayerPlaceholders.md) | IPlayer placeholders |
| static class [PluginPlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/PluginPlaceholders.md) | Plugin placeholders |
| static class [ResponseErrorPlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/ResponseErrorPlaceholders.md) | [`ResponseError`](./Oxide.Ext.Discord/Entities/Api/ResponseError.md) placeholders |
| static class [RolePlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/RolePlaceholders.md) | [`DiscordRole`](./Oxide.Ext.Discord/Entities/Permissions/DiscordRole.md) placeholders |
| static class [ServerPlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/ServerPlaceholders.md) | IServer placeholders |
| static class [SnowflakePlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/SnowflakePlaceholders.md) | [`Snowflake`](./Oxide.Ext.Discord/Entities/Snowflake.md) placeholders |
| static class [TimeSpanPlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/TimeSpanPlaceholders.md) | TimeSpan placeholders |
| static class [TimestampPlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/TimestampPlaceholders.md) | Timestamp placeholders |
| static class [UserPlaceholders](./Oxide.Ext.Discord/Libraries/Placeholders/Default/UserPlaceholders.md) | [`DiscordUser`](./Oxide.Ext.Discord/Entities/Users/DiscordUser.md) placeholders |

## Oxide.Ext.Discord.Libraries.Placeholders.Keys namespace

| public type | description |
| --- | --- |
| class [AppCommandKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/AppCommandKeys.md) | Placeholder Keys for [`DiscordApplicationCommand`](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/DiscordApplicationCommand.md) |
| class [ChannelKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/ChannelKeys.md) | Placeholder Keys for [`DiscordChannel`](./Oxide.Ext.Discord/Entities/Channels/DiscordChannel.md) |
| class [DateTimeKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/DateTimeKeys.md) | Placeholder Keys for DateTime |
| static class [DefaultKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/DefaultKeys.md) | Default Discord Extension provided Placeholder Keys |
| class [GuildKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/GuildKeys.md) | Placeholder Keys for [`DiscordGuild`](./Oxide.Ext.Discord/Entities/Guilds/DiscordGuild.md) |
| class [InteractionKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/InteractionKeys.md) | Placeholder Keys for [`DiscordInteraction`](./Oxide.Ext.Discord/Entities/Interactions/DiscordInteraction.md) |
| class [MemberKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/MemberKeys.md) | Placeholder Keys for [`GuildMember`](./Oxide.Ext.Discord/Entities/Guilds/GuildMember.md) |
| class [MessageKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/MessageKeys.md) | Placeholder Keys for [`DiscordMessage`](./Oxide.Ext.Discord/Entities/Messages/DiscordMessage.md) |
| class [PlayerKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/PlayerKeys.md) | Placeholder Keys for IPlayer |
| class [PluginKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/PluginKeys.md) | Placeholder Keys for Plugin |
| class [ResponseErrorKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/ResponseErrorKeys.md) | Placeholder Keys for [`ResponseError`](./Oxide.Ext.Discord/Entities/Api/ResponseError.md) |
| class [RoleKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/RoleKeys.md) | Placeholder Keys for [`DiscordRole`](./Oxide.Ext.Discord/Entities/Permissions/DiscordRole.md) |
| class [ServerKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/ServerKeys.md) | Placeholder Keys for IServer |
| class [SnowflakeKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/SnowflakeKeys.md) | Placeholder Keys for [`Snowflake`](./Oxide.Ext.Discord/Entities/Snowflake.md) |
| class [TimespanKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/TimespanKeys.md) | Placeholder Keys for TimeSpan |
| class [TimestampKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/TimestampKeys.md) | Placeholder Keys for Int64 |
| class [UserKeys](./Oxide.Ext.Discord/Libraries/Placeholders/Keys/UserKeys.md) | Placeholder Keys for [`DiscordUser`](./Oxide.Ext.Discord/Entities/Users/DiscordUser.md) |

## Oxide.Ext.Discord.Libraries.Pooling namespace

| public type | description |
| --- | --- |
| class [DiscordPool](./Oxide.Ext.Discord/Libraries/Pooling/DiscordPool.md) | Discord Pool Library |

## Oxide.Ext.Discord.Libraries.Subscription namespace

| public type | description |
| --- | --- |
| class [DiscordSubscription](./Oxide.Ext.Discord/Libraries/Subscription/DiscordSubscription.md) | Represents a channel subscription for a plugin |
| class [DiscordSubscriptions](./Oxide.Ext.Discord/Libraries/Subscription/DiscordSubscriptions.md) | Represents Discord Subscriptions Oxide Library Allows for plugins to subscribe to discord channels |

## Oxide.Ext.Discord.Libraries.Templates namespace

| public type | description |
| --- | --- |
| abstract class [BaseMessageTemplateLibrary&lt;TTemplate&gt;](./Oxide.Ext.Discord/Libraries/Templates/BaseMessageTemplateLibrary%7BTTemplate%7D.md) | Library for Discord Message templates |
| abstract class [BaseTemplateLibrary&lt;TTemplate&gt;](./Oxide.Ext.Discord/Libraries/Templates/BaseTemplateLibrary%7BTTemplate%7D.md) | Oxide Library for Discord Templates |
| class [BulkTemplateRegistration&lt;T&gt;](./Oxide.Ext.Discord/Libraries/Templates/BulkTemplateRegistration%7BT%7D.md) | Used for bulk template registration |
| enum [TemplateType](./Oxide.Ext.Discord/Libraries/Templates/TemplateType.md) | Represents available template type |
| struct [TemplateVersion](./Oxide.Ext.Discord/Libraries/Templates/TemplateVersion.md) | Version of a specific template |

## Oxide.Ext.Discord.Libraries.Templates.AutoComplete namespace

| public type | description |
| --- | --- |
| class [DiscordAutoCompleteChoiceTemplate](./Oxide.Ext.Discord/Libraries/Templates/AutoComplete/DiscordAutoCompleteChoiceTemplate.md) | Template for Discord Auto Completes |
| class [DiscordAutoCompleteChoiceTemplates](./Oxide.Ext.Discord/Libraries/Templates/AutoComplete/DiscordAutoCompleteChoiceTemplates.md) | Auto Complete Choice Templates Library |

## Oxide.Ext.Discord.Libraries.Templates.Commands namespace

| public type | description |
| --- | --- |
| class [ArgumentLocalization](./Oxide.Ext.Discord/Libraries/Templates/Commands/ArgumentLocalization.md) | Localization for Application Command Arguments |
| class [ChoicesLocalization](./Oxide.Ext.Discord/Libraries/Templates/Commands/ChoicesLocalization.md) | Localization for Select Menu Choices |
| class [CommandLocalization](./Oxide.Ext.Discord/Libraries/Templates/Commands/CommandLocalization.md) | Localization for Application Commands |
| class [DiscordCommandLocalization](./Oxide.Ext.Discord/Libraries/Templates/Commands/DiscordCommandLocalization.md) | Command Localizations for Application Commands |
| class [DiscordCommandLocalizations](./Oxide.Ext.Discord/Libraries/Templates/Commands/DiscordCommandLocalizations.md) | Library for localizing [`DiscordApplicationCommand`](./Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/DiscordApplicationCommand.md)s |

## Oxide.Ext.Discord.Libraries.Templates.Components namespace

| public type | description |
| --- | --- |
| abstract class [BaseComponentTemplate](./Oxide.Ext.Discord/Libraries/Templates/Components/BaseComponentTemplate.md) | Base Template for Message Components |
| class [ButtonTemplate](./Oxide.Ext.Discord/Libraries/Templates/Components/ButtonTemplate.md) | Template for Button Components |
| class [InputTextTemplate](./Oxide.Ext.Discord/Libraries/Templates/Components/InputTextTemplate.md) | Input Text Message Component Template |
| class [SelectMenuOptionTemplate](./Oxide.Ext.Discord/Libraries/Templates/Components/SelectMenuOptionTemplate.md) | Template for Select Menu Options |
| class [SelectMenuTemplate](./Oxide.Ext.Discord/Libraries/Templates/Components/SelectMenuTemplate.md) | Represents a template for select menus |

## Oxide.Ext.Discord.Libraries.Templates.Embeds namespace

| public type | description |
| --- | --- |
| class [DiscordEmbedFieldTemplate](./Oxide.Ext.Discord/Libraries/Templates/Embeds/DiscordEmbedFieldTemplate.md) | Discord Template for Embed Field |
| class [DiscordEmbedFieldTemplates](./Oxide.Ext.Discord/Libraries/Templates/Embeds/DiscordEmbedFieldTemplates.md) | Modal Templates Library |
| class [DiscordEmbedTemplate](./Oxide.Ext.Discord/Libraries/Templates/Embeds/DiscordEmbedTemplate.md) | Discord Template for embed |
| class [DiscordEmbedTemplates](./Oxide.Ext.Discord/Libraries/Templates/Embeds/DiscordEmbedTemplates.md) | Modal Templates Library |
| class [EmbedFooterTemplate](./Oxide.Ext.Discord/Libraries/Templates/Embeds/EmbedFooterTemplate.md) | Discord Template for Embed Footer |

## Oxide.Ext.Discord.Libraries.Templates.Emojis namespace

| public type | description |
| --- | --- |
| class [EmojiTemplate](./Oxide.Ext.Discord/Libraries/Templates/Emojis/EmojiTemplate.md) | Discord Template for Emoji |

## Oxide.Ext.Discord.Libraries.Templates.Messages namespace

| public type | description |
| --- | --- |
| class [DiscordMessageTemplate](./Oxide.Ext.Discord/Libraries/Templates/Messages/DiscordMessageTemplate.md) | Discord Message Template for sending localized Discord Messages |
| class [DiscordMessageTemplates](./Oxide.Ext.Discord/Libraries/Templates/Messages/DiscordMessageTemplates.md) | Modal Templates Library |

## Oxide.Ext.Discord.Libraries.Templates.Modals namespace

| public type | description |
| --- | --- |
| class [DiscordModalTemplate](./Oxide.Ext.Discord/Libraries/Templates/Modals/DiscordModalTemplate.md) | Template used for Modal Message Component |
| class [DiscordModalTemplates](./Oxide.Ext.Discord/Libraries/Templates/Modals/DiscordModalTemplates.md) | Modal Templates Library |

## Oxide.Ext.Discord.Logging namespace

| public type | description |
| --- | --- |
| class [DebugLogger](./Oxide.Ext.Discord/Logging/DebugLogger.md) | Debug Logger used for logging debug information |
| class [DiscordLogger](./Oxide.Ext.Discord/Logging/DiscordLogger.md) | Represents a discord extension logger |
| class [DiscordLoggerFactory](./Oxide.Ext.Discord/Logging/DiscordLoggerFactory.md) | Factory for creating DiscordLoggers |
| enum [DiscordLogLevel](./Oxide.Ext.Discord/Logging/DiscordLogLevel.md) | Represents the log level for a logger |
| interface [IDiscordLoggingConfig](./Oxide.Ext.Discord/Logging/IDiscordLoggingConfig.md) | Interface for Discord Logging Configuration |
| interface [ILogger](./Oxide.Ext.Discord/Logging/ILogger.md) | Represents an interface for a logger |
| static class [LoggerExt](./Oxide.Ext.Discord/Logging/LoggerExt.md) |  |

## Oxide.Ext.Discord.Network namespace

| public type | description |
| --- | --- |
| class [DiscordStreamContent](./Oxide.Ext.Discord/Network/DiscordStreamContent.md) | Stream content that is sent over HTTP This is used because StreamContent disposes the underlying stream when disposed and we don't want that since we cache our stream |

## Oxide.Ext.Discord.Plugins namespace

| public type | description |
| --- | --- |
| struct [PluginId](./Oxide.Ext.Discord/Plugins/PluginId.md) | Represents a Plugin ID |

## Oxide.Ext.Discord.Plugins.Setup namespace

| public type | description |
| --- | --- |
| class [PluginSetup](./Oxide.Ext.Discord/Plugins/Setup/PluginSetup.md) | Build Discord Extension Setup Data for a plugin |

## Oxide.Ext.Discord.Pooling namespace

| public type | description |
| --- | --- |
| abstract class [BasePool&lt;TPooled,TPool&gt;](./Oxide.Ext.Discord/Pooling/BasePool%7BTPooled,TPool%7D.md) | Represents a BasePool in Discord |
| abstract class [BasePoolable](./Oxide.Ext.Discord/Pooling/BasePoolable.md) | Represents a poolable object |
| class [DiscordPluginPool](./Oxide.Ext.Discord/Pooling/DiscordPluginPool.md) | Built in pooling for discord entities |
| interface [IPool](./Oxide.Ext.Discord/Pooling/IPool.md) | Represents a pool |
| interface [IPool&lt;T&gt;](./Oxide.Ext.Discord/Pooling/IPool%7BT%7D.md) | Represents a pool of type T |
| class [PoolSettings](./Oxide.Ext.Discord/Pooling/PoolSettings.md) | Settings for the pools |
| struct [PoolSize](./Oxide.Ext.Discord/Pooling/PoolSize.md) | Represents size constraints for a pool |

## Oxide.Ext.Discord.Pooling.Pools namespace

| public type | description |
| --- | --- |
| class [DiscordArrayPool&lt;T&gt;](./Oxide.Ext.Discord/Pooling/Pools/DiscordArrayPool%7BT%7D.md) | Represents a [`DiscordArrayPool`](./Oxide.Ext.Discord/Pooling/Pools/DiscordArrayPool%7BT%7D.md) |

## Oxide.Ext.Discord.Promises namespace

| public type | description |
| --- | --- |
| class [BasePromise](./Oxide.Ext.Discord/Promises/BasePromise.md) | Represents the base class for all promises |
| class [Promise](./Oxide.Ext.Discord/Promises/Promise.md) | Implements a non-generic C# promise, this is a promise that simply resolves without delivering a value. https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Promise |
| class [Promise&lt;TPromised&gt;](./Oxide.Ext.Discord/Promises/Promise%7BTPromised%7D.md) | Implements a C# promise. https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Promise |
| enum [PromiseState](./Oxide.Ext.Discord/Promises/PromiseState.md) | Specifies the state of a promise. |
| struct [RejectHandler](./Oxide.Ext.Discord/Promises/RejectHandler.md) | Represents a handler invoked when the promise is rejected. |

## Oxide.Ext.Discord.Promises.Timer namespace

| public type | description |
| --- | --- |
| class [PromiseTimer](./Oxide.Ext.Discord/Promises/Timer/PromiseTimer.md) | Timer Implementation using promises |

## Oxide.Ext.Discord.RateLimits namespace

| public type | description |
| --- | --- |
| abstract class [BaseRateLimit](./Oxide.Ext.Discord/RateLimits/BaseRateLimit.md) | Represents a base rate limit for websocket and rest api requests |
| class [RestRateLimit](./Oxide.Ext.Discord/RateLimits/RestRateLimit.md) | Represents a rate limit for rest requests |
| class [WebsocketRateLimit](./Oxide.Ext.Discord/RateLimits/WebsocketRateLimit.md) | Represents a WebSocket Rate Limit |

## Oxide.Ext.Discord.Rest namespace

| public type | description |
| --- | --- |
| class [RestHandler](./Oxide.Ext.Discord/Rest/RestHandler.md) | Represents a REST handler for a bot |

## Oxide.Ext.Discord.Rest.Buckets namespace

| public type | description |
| --- | --- |
| class [Bucket](./Oxide.Ext.Discord/Rest/Buckets/Bucket.md) | Contains bucket information for a REST API Bucket |
| struct [BucketId](./Oxide.Ext.Discord/Rest/Buckets/BucketId.md) | Represents an ID for a bucket |

## Oxide.Ext.Discord.Rest.Requests namespace

| public type | description |
| --- | --- |
| abstract class [BaseRequest](./Oxide.Ext.Discord/Rest/Requests/BaseRequest.md) | Represents a base request class for REST API calls |
| class [Request](./Oxide.Ext.Discord/Rest/Requests/Request.md) | Represents a Request that does not return data |
| class [Request&lt;T&gt;](./Oxide.Ext.Discord/Rest/Requests/Request%7BT%7D.md) | Represents a REST API request that returns {T} data |
| enum [RequestCompletedStatus](./Oxide.Ext.Discord/Rest/Requests/RequestCompletedStatus.md) | Represents the completed status for the request |
| class [RequestHandler](./Oxide.Ext.Discord/Rest/Requests/RequestHandler.md) | Represent a Discord API request |
| enum [RequestStatus](./Oxide.Ext.Discord/Rest/Requests/RequestStatus.md) | Discord API Request Status |

## Oxide.Ext.Discord.Trie namespace

| public type | description |
| --- | --- |
| class [UkkonenTrie&lt;T&gt;](./Oxide.Ext.Discord/Trie/UkkonenTrie%7BT%7D.md) | A Ukkonen Suffix Trie |

## Oxide.Ext.Discord.Types namespace

| public type | description |
| --- | --- |
| abstract class [Singleton&lt;T&gt;](./Oxide.Ext.Discord/Types/Singleton%7BT%7D.md) | Represents a singleton of type {T} |

## Oxide.Ext.Discord.WebSockets namespace

| public type | description |
| --- | --- |
| class [DiscordWebSocket](./Oxide.Ext.Discord/WebSockets/DiscordWebSocket.md) | Represents a websocket that connects to discord |
| enum [DiscordWebsocketCloseCode](./Oxide.Ext.Discord/WebSockets/DiscordWebsocketCloseCode.md) | Represents [Socket Close Event Codes](https://discord.com/developers/docs/topics/opcodes-and-status-codes#gateway-gateway-close-event-codes) |
| enum [SocketState](./Oxide.Ext.Discord/WebSockets/SocketState.md) | Represents our current state for the websocket |

## Oxide.Ext.Discord.WebSockets.Handlers namespace

| public type | description |
| --- | --- |
| enum [DiscordDispatchCode](./Oxide.Ext.Discord/WebSockets/Handlers/DiscordDispatchCode.md) | Represents the [Gateway Event Codes](https://discord.com/developers/docs/topics/gateway#commands-and-events-gateway-events) |
| class [DiscordHeartbeatHandler](./Oxide.Ext.Discord/WebSockets/Handlers/DiscordHeartbeatHandler.md) | Handles the heartbeating for the websocket connection |
| class [WebSocketCommandHandler](./Oxide.Ext.Discord/WebSockets/Handlers/WebSocketCommandHandler.md) | Handles command queueing when the websocket is down |
| class [WebSocketEventHandler](./Oxide.Ext.Discord/WebSockets/Handlers/WebSocketEventHandler.md) | Handles websocket events |

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
