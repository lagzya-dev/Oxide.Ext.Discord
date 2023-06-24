## Oxide.Ext.Discord.Entities.Gateway.Events namespace

| public type | description |
| --- | --- |
| class [AutoModActionExecutionEvent](./AutoModActionExecutionEvent.md) | Represents [Auto Moderation Action Execution Event](https://discord.com/developers/docs/topics/gateway#auto-moderation-action-execution-auto-moderation-action-execution-event-fields) |
| class [ChannelPinsUpdatedEvent](./ChannelPinsUpdatedEvent.md) | Represents [Channel Pins Update](https://discord.com/developers/docs/topics/gateway#channel-pins-update) |
| enum [GatewayEventCode](./GatewayEventCode.md) | Represents [Gateway Opcodes](https://discord.com/developers/docs/topics/opcodes-and-status-codes#gateway-gateway-opcodes) |
| class [GatewayHelloEvent](./GatewayHelloEvent.md) | Represents [Hello](https://discord.com/developers/docs/topics/gateway#hello) Sent on connection to the websocket. Defines the heartbeat interval that the client should heartbeat to. |
| class [GatewayReadyEvent](./GatewayReadyEvent.md) | Represents [Ready](https://discord.com/developers/docs/topics/gateway#ready) The ready event is dispatched when a client has completed the initial handshake with the gateway (for new sessions) |
| class [GatewayResumedEvent](./GatewayResumedEvent.md) | Represents [Resumed](https://discord.com/developers/docs/topics/gateway#resumed) The resumed event is dispatched when a client has sent a resume payload to the gateway (for resuming existing sessions). |
| class [GuildEmojisUpdatedEvent](./GuildEmojisUpdatedEvent.md) | Represents [Guild Emojis Update](https://discord.com/developers/docs/topics/gateway#guild-emojis-update) |
| class [GuildIntegrationsUpdatedEvent](./GuildIntegrationsUpdatedEvent.md) | Represents [Guild Integrations Update](https://discord.com/developers/docs/topics/gateway#guild-integrations-update) |
| class [GuildMemberAddedEvent](./GuildMemberAddedEvent.md) | Represents [Guild Member Add](https://discord.com/developers/docs/topics/gateway#guild-member-add) |
| class [GuildMemberBannedEvent](./GuildMemberBannedEvent.md) | Represents [Guild Ban Add](https://discord.com/developers/docs/topics/gateway#guild-ban-add) Event Represents [Guild Ban Remove](https://discord.com/developers/docs/topics/gateway#guild-ban-remove) Event |
| class [GuildMemberRemovedEvent](./GuildMemberRemovedEvent.md) | Represents [Guild Member Remove](https://discord.com/developers/docs/topics/gateway#guild-member-remove) |
| class [GuildMembersChunkEvent](./GuildMembersChunkEvent.md) | Represents [Guild Members Chunk](https://discord.com/developers/docs/topics/gateway#guild-members-chunk) |
| class [GuildMemberUpdatedEvent](./GuildMemberUpdatedEvent.md) | Represents [Guild Member Update](https://discord.com/developers/docs/topics/gateway#guild-member-update) |
| class [GuildRoleCreatedEvent](./GuildRoleCreatedEvent.md) | Represents [Guild Role Create](https://discord.com/developers/docs/topics/gateway#guild-role-create) |
| class [GuildRoleDeletedEvent](./GuildRoleDeletedEvent.md) | Represents [Guild Role Delete](https://discord.com/developers/docs/topics/gateway#guild-role-delete) |
| class [GuildRoleUpdatedEvent](./GuildRoleUpdatedEvent.md) | Represents [Guild Role Update](https://discord.com/developers/docs/topics/gateway#guild-role-update) |
| class [GuildScheduleEventUserAddedEvent](./GuildScheduleEventUserAddedEvent.md) | Represents a [Guild Scheduled Event User Add Event Fields](https://discord.com/developers/docs/topics/gateway#guild-scheduled-event-user-add-guild-scheduled-event-user-add-event-fields) |
| class [GuildScheduleEventUserRemovedEvent](./GuildScheduleEventUserRemovedEvent.md) | Represents a [Guild Scheduled Event User Remove Event Fields](https://discord.com/developers/docs/topics/gateway#guild-scheduled-event-user-remove) |
| class [GuildStickersUpdatedEvent](./GuildStickersUpdatedEvent.md) | Represents [Guild Stickers Update](https://discord.com/developers/docs/topics/gateway#guild-stickers-update) |
| class [IntegrationCreatedEvent](./IntegrationCreatedEvent.md) | Represents a [Integration Create Structure](https://discord.com/developers/docs/topics/gateway#integration-create-integration-create-event-additional-fields) |
| class [IntegrationDeletedEvent](./IntegrationDeletedEvent.md) | Represents a [Integration Delete Structure](https://discord.com/developers/docs/topics/gateway#integration-delete-integration-delete-event-fields) |
| class [IntegrationUpdatedEvent](./IntegrationUpdatedEvent.md) | Represents a [Integration Update Structure](https://discord.com/developers/docs/topics/gateway#integration-update-integration-update-event-additional-fields) |
| class [InviteCreatedEvent](./InviteCreatedEvent.md) | Represents [Invite Create](https://discord.com/developers/docs/topics/gateway#invite-create) |
| class [InviteDeletedEvent](./InviteDeletedEvent.md) | Represents [Invite Delete](https://discord.com/developers/docs/topics/gateway#invite-delete) |
| class [MessageBulkDeletedEvent](./MessageBulkDeletedEvent.md) | Represents [Message Delete Bulk](https://discord.com/developers/docs/topics/gateway#message-delete-bulk) |
| class [MessageDeletedEvent](./MessageDeletedEvent.md) | Represents [Message Delete](https://discord.com/developers/docs/topics/gateway#message-delete) |
| class [MessageReactionAddedEvent](./MessageReactionAddedEvent.md) | Represents [Message Reaction Add](https://discord.com/developers/docs/topics/gateway#message-reaction-add) |
| class [MessageReactionRemovedAllEmojiEvent](./MessageReactionRemovedAllEmojiEvent.md) | Represents [Message Reaction Remove All](https://discord.com/developers/docs/topics/gateway#message-reaction-remove-emoji-message-reaction-remove-emoji) |
| class [MessageReactionRemovedAllEvent](./MessageReactionRemovedAllEvent.md) | Represents [Message Reaction Remove All](https://discord.com/developers/docs/topics/gateway#message-reaction-remove-all) |
| class [MessageReactionRemovedEvent](./MessageReactionRemovedEvent.md) | Represents [Message Reaction Remove](https://discord.com/developers/docs/topics/gateway#message-reaction-remove) |
| class [PresenceUpdatedEvent](./PresenceUpdatedEvent.md) | Represents [Presence Update](https://discord.com/developers/docs/topics/gateway#presence-update) |
| class [ThreadListSyncEvent](./ThreadListSyncEvent.md) | Represents [Thread List Sync](https://discord.com/developers/docs/topics/gateway#thread-list-sync-thread-list-sync-event-fields) |
| class [ThreadMembersUpdatedEvent](./ThreadMembersUpdatedEvent.md) | Represents [Thread Members Update Structure](https://discord.com/developers/docs/topics/gateway#thread-members-update-thread-members-update-event-fields) |
| class [ThreadMemberUpdateEvent](./ThreadMemberUpdateEvent.md) | Represents [Thread Member Update Structure](https://discord.com/developers/docs/topics/gateway#thread-member-update) |
| class [TypingStartedEvent](./TypingStartedEvent.md) | Represents [Typing Start](https://discord.com/developers/docs/topics/gateway#typing-start) |
| class [VoiceServerUpdatedEvent](./VoiceServerUpdatedEvent.md) | Represents [Voice Server Update](https://discord.com/developers/docs/topics/gateway#voice-server-update) |
| class [WebhooksUpdatedEvent](./WebhooksUpdatedEvent.md) | Represents [Webhooks Update](https://discord.com/developers/docs/topics/gateway#webhooks-update) |

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
