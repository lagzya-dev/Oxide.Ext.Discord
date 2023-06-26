# DiscordExtHooks class

Represents all hooks available in the discord extension

```csharp
public static class DiscordExtHooks
```

## Public Members

| name | description |
| --- | --- |
| static readonly [HookGatewayIntent](#HookGatewayIntent-field) | A mapping of Hooks required Gateway Intent |
| const [OnDiscordApplicationCommandPermissionsUpdated](#OnDiscordApplicationCommandPermissionsUpdated-field) |  |
| const [OnDiscordAutoModActionExecuted](#OnDiscordAutoModActionExecuted-field) |  |
| const [OnDiscordAutoModRuleCreated](#OnDiscordAutoModRuleCreated-field) |  |
| const [OnDiscordAutoModRuleDeleted](#OnDiscordAutoModRuleDeleted-field) |  |
| const [OnDiscordAutoModRuleUpdated](#OnDiscordAutoModRuleUpdated-field) |  |
| const [OnDiscordBotFullyLoaded](#OnDiscordBotFullyLoaded-field) |  |
| const [OnDiscordClientCreated](#OnDiscordClientCreated-field) |  |
| const [OnDiscordDirectChannelCreated](#OnDiscordDirectChannelCreated-field) |  |
| const [OnDiscordDirectChannelDeleted](#OnDiscordDirectChannelDeleted-field) |  |
| const [OnDiscordDirectChannelPinsUpdated](#OnDiscordDirectChannelPinsUpdated-field) |  |
| const [OnDiscordDirectChannelUpdated](#OnDiscordDirectChannelUpdated-field) |  |
| const [OnDiscordDirectInviteCreated](#OnDiscordDirectInviteCreated-field) |  |
| const [OnDiscordDirectInviteDeleted](#OnDiscordDirectInviteDeleted-field) |  |
| const [OnDiscordDirectMessageCreated](#OnDiscordDirectMessageCreated-field) |  |
| const [OnDiscordDirectMessageDeleted](#OnDiscordDirectMessageDeleted-field) |  |
| const [OnDiscordDirectMessageReactionAdded](#OnDiscordDirectMessageReactionAdded-field) |  |
| const [OnDiscordDirectMessageReactionEmojiRemoved](#OnDiscordDirectMessageReactionEmojiRemoved-field) |  |
| const [OnDiscordDirectMessageReactionRemoved](#OnDiscordDirectMessageReactionRemoved-field) |  |
| const [OnDiscordDirectMessageReactionRemovedAll](#OnDiscordDirectMessageReactionRemovedAll-field) |  |
| const [OnDiscordDirectMessagesBulkDeleted](#OnDiscordDirectMessagesBulkDeleted-field) |  |
| const [OnDiscordDirectMessageUpdated](#OnDiscordDirectMessageUpdated-field) |  |
| const [OnDiscordDirectTypingStarted](#OnDiscordDirectTypingStarted-field) |  |
| const [OnDiscordDirectVoiceStateUpdated](#OnDiscordDirectVoiceStateUpdated-field) |  |
| const [OnDiscordGatewayReady](#OnDiscordGatewayReady-field) |  |
| const [OnDiscordGatewayResumed](#OnDiscordGatewayResumed-field) |  |
| const [OnDiscordGuildChannelCreated](#OnDiscordGuildChannelCreated-field) |  |
| const [OnDiscordGuildChannelDeleted](#OnDiscordGuildChannelDeleted-field) |  |
| const [OnDiscordGuildChannelPinsUpdated](#OnDiscordGuildChannelPinsUpdated-field) |  |
| const [OnDiscordGuildChannelUpdated](#OnDiscordGuildChannelUpdated-field) |  |
| const [OnDiscordGuildCreated](#OnDiscordGuildCreated-field) |  |
| const [OnDiscordGuildDeleted](#OnDiscordGuildDeleted-field) |  |
| const [OnDiscordGuildEmojisUpdated](#OnDiscordGuildEmojisUpdated-field) |  |
| const [OnDiscordGuildIntegrationCreated](#OnDiscordGuildIntegrationCreated-field) |  |
| const [OnDiscordGuildIntegrationDeleted](#OnDiscordGuildIntegrationDeleted-field) |  |
| const [OnDiscordGuildIntegrationsUpdated](#OnDiscordGuildIntegrationsUpdated-field) |  |
| const [OnDiscordGuildIntegrationUpdated](#OnDiscordGuildIntegrationUpdated-field) |  |
| const [OnDiscordGuildInviteCreated](#OnDiscordGuildInviteCreated-field) |  |
| const [OnDiscordGuildInviteDeleted](#OnDiscordGuildInviteDeleted-field) |  |
| const [OnDiscordGuildMemberAdded](#OnDiscordGuildMemberAdded-field) |  |
| const [OnDiscordGuildMemberAvatarUpdated](#OnDiscordGuildMemberAvatarUpdated-field) |  |
| const [OnDiscordGuildMemberBanned](#OnDiscordGuildMemberBanned-field) |  |
| const [OnDiscordGuildMemberBoosted](#OnDiscordGuildMemberBoosted-field) |  |
| const [OnDiscordGuildMemberBoostEnded](#OnDiscordGuildMemberBoostEnded-field) |  |
| const [OnDiscordGuildMemberBoostExtended](#OnDiscordGuildMemberBoostExtended-field) |  |
| const [OnDiscordGuildMemberDeafened](#OnDiscordGuildMemberDeafened-field) |  |
| const [OnDiscordGuildMemberMuted](#OnDiscordGuildMemberMuted-field) |  |
| const [OnDiscordGuildMemberNicknameUpdated](#OnDiscordGuildMemberNicknameUpdated-field) |  |
| const [OnDiscordGuildMemberPresenceUpdated](#OnDiscordGuildMemberPresenceUpdated-field) |  |
| const [OnDiscordGuildMemberRemoved](#OnDiscordGuildMemberRemoved-field) |  |
| const [OnDiscordGuildMemberRoleAdded](#OnDiscordGuildMemberRoleAdded-field) |  |
| const [OnDiscordGuildMemberRoleRemoved](#OnDiscordGuildMemberRoleRemoved-field) |  |
| const [OnDiscordGuildMembersChunk](#OnDiscordGuildMembersChunk-field) |  |
| const [OnDiscordGuildMembersLoaded](#OnDiscordGuildMembersLoaded-field) |  |
| const [OnDiscordGuildMemberTimeout](#OnDiscordGuildMemberTimeout-field) |  |
| const [OnDiscordGuildMemberTimeoutEnded](#OnDiscordGuildMemberTimeoutEnded-field) |  |
| const [OnDiscordGuildMemberUnbanned](#OnDiscordGuildMemberUnbanned-field) |  |
| const [OnDiscordGuildMemberUndeafened](#OnDiscordGuildMemberUndeafened-field) |  |
| const [OnDiscordGuildMemberUnmuted](#OnDiscordGuildMemberUnmuted-field) |  |
| const [OnDiscordGuildMemberUpdated](#OnDiscordGuildMemberUpdated-field) |  |
| const [OnDiscordGuildMessageCreated](#OnDiscordGuildMessageCreated-field) |  |
| const [OnDiscordGuildMessageDeleted](#OnDiscordGuildMessageDeleted-field) |  |
| const [OnDiscordGuildMessageReactionAdded](#OnDiscordGuildMessageReactionAdded-field) |  |
| const [OnDiscordGuildMessageReactionEmojiRemoved](#OnDiscordGuildMessageReactionEmojiRemoved-field) |  |
| const [OnDiscordGuildMessageReactionRemoved](#OnDiscordGuildMessageReactionRemoved-field) |  |
| const [OnDiscordGuildMessageReactionRemovedAll](#OnDiscordGuildMessageReactionRemovedAll-field) |  |
| const [OnDiscordGuildMessagesBulkDeleted](#OnDiscordGuildMessagesBulkDeleted-field) |  |
| const [OnDiscordGuildMessageUpdated](#OnDiscordGuildMessageUpdated-field) |  |
| const [OnDiscordGuildRoleCreated](#OnDiscordGuildRoleCreated-field) |  |
| const [OnDiscordGuildRoleDeleted](#OnDiscordGuildRoleDeleted-field) |  |
| const [OnDiscordGuildRoleUpdated](#OnDiscordGuildRoleUpdated-field) |  |
| const [OnDiscordGuildScheduledEventCreated](#OnDiscordGuildScheduledEventCreated-field) |  |
| const [OnDiscordGuildScheduledEventDeleted](#OnDiscordGuildScheduledEventDeleted-field) |  |
| const [OnDiscordGuildScheduledEventUpdated](#OnDiscordGuildScheduledEventUpdated-field) |  |
| const [OnDiscordGuildScheduledEventUserAdded](#OnDiscordGuildScheduledEventUserAdded-field) |  |
| const [OnDiscordGuildScheduledEventUserRemoved](#OnDiscordGuildScheduledEventUserRemoved-field) |  |
| const [OnDiscordGuildStickersUpdated](#OnDiscordGuildStickersUpdated-field) |  |
| const [OnDiscordGuildThreadCreated](#OnDiscordGuildThreadCreated-field) |  |
| const [OnDiscordGuildThreadDeleted](#OnDiscordGuildThreadDeleted-field) |  |
| const [OnDiscordGuildThreadListSynced](#OnDiscordGuildThreadListSynced-field) |  |
| const [OnDiscordGuildThreadMembersUpdated](#OnDiscordGuildThreadMembersUpdated-field) |  |
| const [OnDiscordGuildThreadMemberUpdated](#OnDiscordGuildThreadMemberUpdated-field) |  |
| const [OnDiscordGuildThreadUpdated](#OnDiscordGuildThreadUpdated-field) |  |
| const [OnDiscordGuildTypingStarted](#OnDiscordGuildTypingStarted-field) |  |
| const [OnDiscordGuildUnavailable](#OnDiscordGuildUnavailable-field) |  |
| const [OnDiscordGuildUpdated](#OnDiscordGuildUpdated-field) |  |
| const [OnDiscordGuildVoiceServerUpdated](#OnDiscordGuildVoiceServerUpdated-field) |  |
| const [OnDiscordGuildVoiceStateUpdated](#OnDiscordGuildVoiceStateUpdated-field) |  |
| const [OnDiscordGuildWebhookUpdated](#OnDiscordGuildWebhookUpdated-field) |  |
| const [OnDiscordHeartbeatSent](#OnDiscordHeartbeatSent-field) |  |
| const [OnDiscordInteractionCreated](#OnDiscordInteractionCreated-field) |  |
| const [OnDiscordPlayerLinked](#OnDiscordPlayerLinked-field) |  |
| const [OnDiscordPlayerUnlink](#OnDiscordPlayerUnlink-field) |  |
| const [OnDiscordPlayerUnlinked](#OnDiscordPlayerUnlinked-field) |  |
| const [OnDiscordSetupHeartbeat](#OnDiscordSetupHeartbeat-field) |  |
| const [OnDiscordStageInstanceCreated](#OnDiscordStageInstanceCreated-field) |  |
| const [OnDiscordStageInstanceDeleted](#OnDiscordStageInstanceDeleted-field) |  |
| const [OnDiscordStageInstanceUpdated](#OnDiscordStageInstanceUpdated-field) |  |
| const [OnDiscordUnhandledCommand](#OnDiscordUnhandledCommand-field) |  |
| const [OnDiscordUserUpdated](#OnDiscordUserUpdated-field) |  |
| const [OnDiscordWebsocketClosed](#OnDiscordWebsocketClosed-field) |  |
| const [OnDiscordWebsocketErrored](#OnDiscordWebsocketErrored-field) |  |
| const [OnDiscordWebsocketOpened](#OnDiscordWebsocketOpened-field) |  |
| static [IsDiscordHook](#IsDiscordHook-method)(…) | Returns true if the hook is a Discord Extension Hook |
| static [IsGlobalHook](#IsGlobalHook-method)(…) | Returns true if the hook is a Discord Extension Global Hook |
| static [IsPluginHook](#IsPluginHook-method)(…) | Returns true if the hook is a Discord Extension Plugin Hook |

## See Also

* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [DiscordExtHooks.cs](../../../../Oxide.Ext.Discord/Constants/DiscordExtHooks.cs)
   
   
# IsGlobalHook method

Returns true if the hook is a Discord Extension Global Hook

```csharp
public static bool IsGlobalHook(string hook)
```

| parameter | description |
| --- | --- |
| hook | Name of the hook |

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IsPluginHook method

Returns true if the hook is a Discord Extension Plugin Hook

```csharp
public static bool IsPluginHook(string hook)
```

| parameter | description |
| --- | --- |
| hook | Name of the hook |

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IsDiscordHook method

Returns true if the hook is a Discord Extension Hook

```csharp
public static bool IsDiscordHook(string hook)
```

| parameter | description |
| --- | --- |
| hook | Name of the hook |

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# HookGatewayIntent field

A mapping of Hooks required Gateway Intent

```csharp
public static readonly Hash<string, GatewayIntents> HookGatewayIntent;
```

## See Also

* enum [GatewayIntents](../Entities/Gateway/GatewayIntents.md)
* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordClientCreated field

```csharp
public const string OnDiscordClientCreated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordBotFullyLoaded field

```csharp
public const string OnDiscordBotFullyLoaded;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordWebsocketOpened field

```csharp
public const string OnDiscordWebsocketOpened;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordWebsocketClosed field

```csharp
public const string OnDiscordWebsocketClosed;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordWebsocketErrored field

```csharp
public const string OnDiscordWebsocketErrored;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordSetupHeartbeat field

```csharp
public const string OnDiscordSetupHeartbeat;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordHeartbeatSent field

```csharp
public const string OnDiscordHeartbeatSent;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordPlayerLinked field

```csharp
public const string OnDiscordPlayerLinked;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordPlayerUnlink field

```csharp
public const string OnDiscordPlayerUnlink;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordPlayerUnlinked field

```csharp
public const string OnDiscordPlayerUnlinked;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGatewayReady field

```csharp
public const string OnDiscordGatewayReady;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGatewayResumed field

```csharp
public const string OnDiscordGatewayResumed;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectChannelCreated field

```csharp
public const string OnDiscordDirectChannelCreated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildChannelCreated field

```csharp
public const string OnDiscordGuildChannelCreated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectChannelUpdated field

```csharp
public const string OnDiscordDirectChannelUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildChannelUpdated field

```csharp
public const string OnDiscordGuildChannelUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectChannelDeleted field

```csharp
public const string OnDiscordDirectChannelDeleted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildChannelDeleted field

```csharp
public const string OnDiscordGuildChannelDeleted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectChannelPinsUpdated field

```csharp
public const string OnDiscordDirectChannelPinsUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildChannelPinsUpdated field

```csharp
public const string OnDiscordGuildChannelPinsUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildCreated field

```csharp
public const string OnDiscordGuildCreated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildUpdated field

```csharp
public const string OnDiscordGuildUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildUnavailable field

```csharp
public const string OnDiscordGuildUnavailable;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildDeleted field

```csharp
public const string OnDiscordGuildDeleted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberBanned field

```csharp
public const string OnDiscordGuildMemberBanned;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberUnbanned field

```csharp
public const string OnDiscordGuildMemberUnbanned;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildEmojisUpdated field

```csharp
public const string OnDiscordGuildEmojisUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildStickersUpdated field

```csharp
public const string OnDiscordGuildStickersUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildIntegrationsUpdated field

```csharp
public const string OnDiscordGuildIntegrationsUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberAdded field

```csharp
public const string OnDiscordGuildMemberAdded;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberRemoved field

```csharp
public const string OnDiscordGuildMemberRemoved;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberUpdated field

```csharp
public const string OnDiscordGuildMemberUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberNicknameUpdated field

```csharp
public const string OnDiscordGuildMemberNicknameUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberAvatarUpdated field

```csharp
public const string OnDiscordGuildMemberAvatarUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberDeafened field

```csharp
public const string OnDiscordGuildMemberDeafened;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberUndeafened field

```csharp
public const string OnDiscordGuildMemberUndeafened;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberMuted field

```csharp
public const string OnDiscordGuildMemberMuted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberUnmuted field

```csharp
public const string OnDiscordGuildMemberUnmuted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberTimeout field

```csharp
public const string OnDiscordGuildMemberTimeout;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberTimeoutEnded field

```csharp
public const string OnDiscordGuildMemberTimeoutEnded;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberBoosted field

```csharp
public const string OnDiscordGuildMemberBoosted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberBoostExtended field

```csharp
public const string OnDiscordGuildMemberBoostExtended;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberBoostEnded field

```csharp
public const string OnDiscordGuildMemberBoostEnded;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberRoleAdded field

```csharp
public const string OnDiscordGuildMemberRoleAdded;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberRoleRemoved field

```csharp
public const string OnDiscordGuildMemberRoleRemoved;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMembersLoaded field

```csharp
public const string OnDiscordGuildMembersLoaded;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMembersChunk field

```csharp
public const string OnDiscordGuildMembersChunk;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildRoleCreated field

```csharp
public const string OnDiscordGuildRoleCreated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildRoleUpdated field

```csharp
public const string OnDiscordGuildRoleUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildRoleDeleted field

```csharp
public const string OnDiscordGuildRoleDeleted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildScheduledEventCreated field

```csharp
public const string OnDiscordGuildScheduledEventCreated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildScheduledEventUpdated field

```csharp
public const string OnDiscordGuildScheduledEventUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildScheduledEventDeleted field

```csharp
public const string OnDiscordGuildScheduledEventDeleted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildScheduledEventUserAdded field

```csharp
public const string OnDiscordGuildScheduledEventUserAdded;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildScheduledEventUserRemoved field

```csharp
public const string OnDiscordGuildScheduledEventUserRemoved;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectMessageCreated field

```csharp
public const string OnDiscordDirectMessageCreated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMessageCreated field

```csharp
public const string OnDiscordGuildMessageCreated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectMessageUpdated field

```csharp
public const string OnDiscordDirectMessageUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMessageUpdated field

```csharp
public const string OnDiscordGuildMessageUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectMessageDeleted field

```csharp
public const string OnDiscordDirectMessageDeleted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMessageDeleted field

```csharp
public const string OnDiscordGuildMessageDeleted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectMessagesBulkDeleted field

```csharp
public const string OnDiscordDirectMessagesBulkDeleted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMessagesBulkDeleted field

```csharp
public const string OnDiscordGuildMessagesBulkDeleted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectMessageReactionAdded field

```csharp
public const string OnDiscordDirectMessageReactionAdded;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMessageReactionAdded field

```csharp
public const string OnDiscordGuildMessageReactionAdded;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectMessageReactionRemoved field

```csharp
public const string OnDiscordDirectMessageReactionRemoved;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMessageReactionRemoved field

```csharp
public const string OnDiscordGuildMessageReactionRemoved;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectMessageReactionRemovedAll field

```csharp
public const string OnDiscordDirectMessageReactionRemovedAll;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMessageReactionRemovedAll field

```csharp
public const string OnDiscordGuildMessageReactionRemovedAll;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectMessageReactionEmojiRemoved field

```csharp
public const string OnDiscordDirectMessageReactionEmojiRemoved;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMessageReactionEmojiRemoved field

```csharp
public const string OnDiscordGuildMessageReactionEmojiRemoved;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildMemberPresenceUpdated field

```csharp
public const string OnDiscordGuildMemberPresenceUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectTypingStarted field

```csharp
public const string OnDiscordDirectTypingStarted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildTypingStarted field

```csharp
public const string OnDiscordGuildTypingStarted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordUserUpdated field

```csharp
public const string OnDiscordUserUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectVoiceStateUpdated field

```csharp
public const string OnDiscordDirectVoiceStateUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildVoiceStateUpdated field

```csharp
public const string OnDiscordGuildVoiceStateUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildVoiceServerUpdated field

```csharp
public const string OnDiscordGuildVoiceServerUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildWebhookUpdated field

```csharp
public const string OnDiscordGuildWebhookUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectInviteCreated field

```csharp
public const string OnDiscordDirectInviteCreated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildInviteCreated field

```csharp
public const string OnDiscordGuildInviteCreated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordDirectInviteDeleted field

```csharp
public const string OnDiscordDirectInviteDeleted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildInviteDeleted field

```csharp
public const string OnDiscordGuildInviteDeleted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordApplicationCommandPermissionsUpdated field

```csharp
public const string OnDiscordApplicationCommandPermissionsUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordInteractionCreated field

```csharp
public const string OnDiscordInteractionCreated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildIntegrationCreated field

```csharp
public const string OnDiscordGuildIntegrationCreated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildIntegrationUpdated field

```csharp
public const string OnDiscordGuildIntegrationUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildIntegrationDeleted field

```csharp
public const string OnDiscordGuildIntegrationDeleted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildThreadCreated field

```csharp
public const string OnDiscordGuildThreadCreated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildThreadUpdated field

```csharp
public const string OnDiscordGuildThreadUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildThreadDeleted field

```csharp
public const string OnDiscordGuildThreadDeleted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildThreadListSynced field

```csharp
public const string OnDiscordGuildThreadListSynced;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildThreadMemberUpdated field

```csharp
public const string OnDiscordGuildThreadMemberUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordGuildThreadMembersUpdated field

```csharp
public const string OnDiscordGuildThreadMembersUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordStageInstanceCreated field

```csharp
public const string OnDiscordStageInstanceCreated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordStageInstanceUpdated field

```csharp
public const string OnDiscordStageInstanceUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordStageInstanceDeleted field

```csharp
public const string OnDiscordStageInstanceDeleted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordAutoModRuleCreated field

```csharp
public const string OnDiscordAutoModRuleCreated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordAutoModRuleUpdated field

```csharp
public const string OnDiscordAutoModRuleUpdated;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordAutoModRuleDeleted field

```csharp
public const string OnDiscordAutoModRuleDeleted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordAutoModActionExecuted field

```csharp
public const string OnDiscordAutoModActionExecuted;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnDiscordUnhandledCommand field

```csharp
public const string OnDiscordUnhandledCommand;
```

## See Also

* class [DiscordExtHooks](./DiscordExtHooks.md)
* namespace [Oxide.Ext.Discord.Constants](./ConstantsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
