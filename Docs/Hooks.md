# Hooks

Hooks within the discord extension are only called on plugins that are registered to receive them and also will only be called on plugins the information is related to. 
By default all plugins with a connected DiscordClient will be registered to receive hooks for the bot they're connect for.
A list of all hooks can be found in the DiscordHooks.cs file.
If a plugin wishes to receive hooks for a specific client they need to get access to that client either by the plugin passing the client in a hook or a plugin can lookup a client for a specific plugin.
To lookup a client for a plugin you can do the following:

```c#
DiscordClient client = DiscordClient.GetClient(plugin);
```

or

```c#
DiscordClient client = DiscordClient.GetClient(pluginName);
```

## Table of Contents

- [Hooks](#hooks)
  * [Discord Client](#discord-client)
    + [OnDiscordClientCreated](#ondiscordclientcreated)
    + [OnDiscordClientConnected](#ondiscordclientconnected)
    + [OnDiscordClientDisconnected](#ondiscordclientdisconnected)
  * [Discord Link](#discord-link)
    + [OnDiscordPlayerLinked](#ondiscordplayerlinked)
    + [OnDiscordPlayerUnlinked](#ondiscordplayerunlinked)
  * [Socket Hooks](#socket-hooks)
    + [OnDiscordWebsocketOpened](#ondiscordwebsocketopened)
    + [OnDiscordWebsocketClosed](#ondiscordwebsocketclosed)
    + [OnDiscordWebsocketErrored](#ondiscordwebsocketerrored)
    + [OnDiscordSetupHeartbeat](#ondiscordsetupheartbeat)
    + [OnDiscordHeartbeatSent](#ondiscordheartbeatsent)
  * [Discord Event Hooks](#discord-event-hooks)
    + [OnDiscordGatewayReady](#ondiscordgatewayready)
    + [OnDiscordGatewayResumed](#ondiscordgatewayresumed)
    + [OnDiscordDirectChannelCreated](#ondiscorddirectchannelcreated)
    + [OnDiscordGuildChannelCreated](#ondiscordguildchannelcreated)
    + [OnDiscordDirectChannelUpdated](#ondiscorddirectchannelupdated)
    + [OnDiscordGuildChannelUpdated](#ondiscordguildchannelupdated)
    + [OnDiscordDirectChannelDeleted](#ondiscorddirectchanneldeleted)
    + [OnDiscordGuildChannelDeleted](#ondiscordguildchanneldeleted)
    + [OnDiscordDirectChannelPinsUpdated](#ondiscorddirectchannelpinsupdated)
    + [OnDiscordGuildChannelPinsUpdated](#ondiscordguildchannelpinsupdated)
    + [OnDiscordGuildCreated](#ondiscordguildcreated)
    + [OnDiscordGuildUpdated](#ondiscordguildupdated)
    + [OnDiscordGuildUnavailable](#ondiscordguildunavailable)
    + [OnDiscordGuildDeleted](#ondiscordguilddeleted)
    + [OnDiscordGuildMemberBanned](#ondiscordguildmemberbanned)
    + [OnDiscordGuildMemberUnbanned](#ondiscordguildmemberunbanned)
    + [OnDiscordGuildEmojisUpdated](#ondiscordguildemojisupdated)
    + [OnDiscordGuildIntegrationsUpdated](#ondiscordguildintegrationsupdated)
    + [OnDiscordGuildMemberAdded](#ondiscordguildmemberadded)
    + [OnDiscordGuildMemberRemoved](#ondiscordguildmemberremoved)
    + [OnDiscordGuildMemberUpdated](#ondiscordguildmemberupdated)
    + [OnDiscordGuildMembersLoaded](#ondiscordguildmembersloaded)
    + [OnDiscordGuildMembersChunk](#ondiscordguildmemberschunk)
    + [OnDiscordGuildRoleCreated](#ondiscordguildrolecreated)
    + [OnDiscordGuildRoleUpdated](#ondiscordguildroleupdated)
    + [OnDiscordGuildRoleDeleted](#ondiscordguildroledeleted)
    + [OnDiscordDirectMessageCreated](#ondiscorddirectmessagecreated)
    + [OnDiscordGuildMessageCreated](#ondiscordguildmessagecreated)
    + [OnDiscordDirectMessageUpdated](#ondiscorddirectmessageupdated)
    + [OnDiscordGuildMessageUpdated](#ondiscordguildmessageupdated)
    + [OnDiscordDirectMessageDeleted](#ondiscorddirectmessagedeleted)
    + [OnDiscordGuildMessageDeleted](#ondiscordguildmessagedeleted)
    + [OnDiscordDirectMessagesBulkDeleted](#ondiscorddirectmessagesbulkdeleted)
    + [OnDiscordGuildMessagesBulkDeleted](#ondiscordguildmessagesbulkdeleted)
    + [OnDiscordDirectMessageReactionAdded](#ondiscorddirectmessagereactionadded)
    + [OnDiscordGuildMessageReactionAdded](#ondiscordguildmessagereactionadded)
    + [OnDiscordDirectMessageReactionRemoved](#ondiscorddirectmessagereactionremoved)
    + [OnDiscordGuildMessageReactionRemoved](#ondiscordguildmessagereactionremoved)
    + [OnDiscordDirectMessageReactionEmojiRemoved](#ondiscorddirectmessagereactionemojiremoved)
    + [OnDiscordGuildMessageReactionRemovedAll](#ondiscordguildmessagereactionremovedall)
    + [OnDiscordDirectMessageReactionRemovedAll](#ondiscorddirectmessagereactionremovedall)
    + [OnDiscordGuildMessageReactionEmojiRemoved](#ondiscordguildmessagereactionemojiremoved)
    + [OnDiscordGuildMemberPresenceUpdated](#ondiscordguildmemberpresenceupdated)
    + [OnDiscordDirectTypingStarted](#ondiscorddirecttypingstarted)
    + [OnDiscordGuildTypingStarted](#ondiscordguildtypingstarted)
    + [OnDiscordUserUpdated](#ondiscorduserupdated)
    + [OnDiscordDirectVoiceStateUpdated](#ondiscorddirectvoicestateupdated)
    + [OnDiscordGuildVoiceStateUpdated](#ondiscordguildvoicestateupdated)
    + [OnDiscordGuildVoiceServerUpdated](#ondiscordguildvoiceserverupdated)
    + [OnDiscordGuildWebhookUpdated](#ondiscordguildwebhookupdated)
    + [OnDiscordDirectInviteCreated](#ondiscorddirectinvitecreated)
    + [OnDiscordGuildInviteCreated](#ondiscordguildinvitecreated)
    + [OnDiscordDirectInviteDeleted](#ondiscorddirectinvitedeleted)
    + [OnDiscordGuildInviteDeleted](#ondiscordguildinvitedeleted)
    + [OnDiscordInteractionCreated](#ondiscordinteractioncreated)
    + [OnDiscordGuildIntegrationCreated](#ondiscordguildintegrationcreated)
    + [OnDiscordGuildIntegrationUpdated](#ondiscordguildintegrationupdated)
    + [OnDiscordGuildIntegrationDeleted](#ondiscordguildintegrationdeleted)
    + [OnDiscordApplicationCommandCreated](#ondiscordapplicationcommandcreated)
    + [OnDiscordApplicationCommandUpdated](#ondiscordapplicationcommandupdated)
    + [OnDiscordApplicationCommandDeleted](#ondiscordapplicationcommanddeleted)
    + [OnDiscordUnhandledCommand](#ondiscordunhandledcommand)

## Discord Client

These hooks will be called when an action occurs on the DiscordClient assigned to a plugin

### OnDiscordClientCreated
- Called when the DiscordClient is created on the bot and is ready to use. 
This is called after the Loaded() hook on the plugin.

```c#
void OnDiscordClientCreated()
{
    Puts("OnDiscordClientCreated Works!");
}
```

**Note:** If you need the client earlier than after the loaded hook you can use the following

```c#
DiscordClient.CreateClient(this)
```

### OnDiscordClientConnected
- Called when the Connect() is called on the DiscordClient

```c#
void OnDiscordClientConnected(Plugin owner, DiscordClient client)
{
    Puts("OnDiscordClientConnected Works!");
}
```

### OnDiscordClientDisconnected
- Called when the Disconnect() is called on the DiscordClient

```c#
void OnDiscordClientDisconnected(Plugin owner, DiscordClient client)
{
    Puts("OnDiscordClientDisconnected Works!");
}
```


## Discord Link

These hooks are called when a player is linked or unlinked using discord link.
It will be called for every plugins registered to receive hooks.  
**Note:** If your plugin supports discord link you should not supply any other hooks as the extension provides them for you.

### OnDiscordPlayerLinked
- Called when a player has linked their discord and player together using the DiscordLink library
```c#
void OnDiscordPlayerLinked(IPlayer player, DiscordUser discord) 
{
     Puts("Player has linked with discord");
}
```

### OnDiscordPlayerUnlinked
- Called when a player has unlinked their discord and player together using the DiscordLink library
```c#
void OnDiscordPlayerUnlinked(IPlayer player, DiscordUser discord) 
{
     Puts("Player has unlinked with discord");
}
```

## Socket Hooks

These are hooks that are only called for the plugin who created the client or registered to receive hooks for that client

### OnDiscordWebsocketOpened
- Called when the discord socket connects.
```c#
void OnDiscordWebsocketOpened()
{
    Puts("WebSocket Opened!");
}
```

### OnDiscordWebsocketClosed
- Called when the web socket is closed for any reason.
```c#
void OnDiscordWebsocketClosed(string reason, int code, bool clean)
{
    Puts("WebSocket closed!");
}
```

### OnDiscordWebsocketErrored
- Called when the web socket has an error.

```c#
void OnDiscordWebsocketErrored(Exception exception, string message)
{
    Puts($"WebSocket errored!");
}
```

### OnDiscordSetupHeartbeat
- Called when we receive the heartbeat interval from the websocket

```c#
void OnDiscordSetupHeartbeat(float heartbeatInterval)
{
    Puts("Heartbeat setup!");
}
```

### OnDiscordHeartbeatSent
- Called when a heartbeat is sent over the websocket to discord to keep the connection open

```c#
void OnDiscordHeartbeatSent(float heartbeatInterval)
{
    Puts("Heartbeat sent!");
}
```

## Discord Event Hooks

These are events that discord sends us over the websocket. 
If you wish to listen for this type of event add the hook into your plugins.  
**Note:** If you aren't receiving calls for a specific hooks make sure you have passed the required [Gateway Intents](GatewayIntents.md) for that hook.

### OnDiscordGatewayReady
- Called when the Discord Bot has successfully connected to the gateway and identified successfully.
- **Note:** Only partial guild information is available at this point. 
  - If you need full guild listen for [OnDiscordGuildCreated](#OnDiscordGuildCreated) hook
  - If you need full guild member list listen for [OnDiscordGuildMembersLoaded](#OnDiscordGuildMembersLoaded)

```c#
 void OnDiscordGatewayReady(GatewayReadyEvent ready)
 {
     Puts("OnDiscordGatewayReady Works!");
 }
```

### OnDiscordGatewayResumed
- Called when the websocket has reconnected to the websocket and resumed the previous session

```c#
void OnDiscordGatewayResumed(GatewayResumedEvent resume)
{
    Puts("OnDiscordGatewayResumed Works!");
}
```

### OnDiscordDirectChannelCreated

- Called when a direct message (DM) channel has been created.

```c#
void OnDiscordDirectChannelCreated(DiscordChannel channel)
{
    Puts("OnDiscordDirectChannelCreated Works!");
}
```

### OnDiscordGuildChannelCreated

- Called when a channel has been created in a guild.

```c#
void OnDiscordGuildChannelCreated(DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordGuildChannelCreated Works!");
}
```

### OnDiscordDirectChannelUpdated

- Called when a direct message (DM) channel has been updated.

```c#
void OnDiscordDirectChannelUpdated(DiscordChannel channel, DiscordChannel previous)
{
    Puts("OnDiscordDirectChannelUpdated Works!");
}
```

### OnDiscordGuildChannelUpdated

- Called when a channel has been updated in a guild.

```c#
void OnDiscordGuildChannelUpdated(DiscordChannel channel, DiscordChannel previous, DiscordGuild guild)
{
    Puts("OnDiscordGuildChannelUpdated Works!");
}
```

### OnDiscordDirectChannelDeleted

- Called when a direct message (DM) channel has been deleted.
  - Not sure if this is possible for DM channels

```c#
void OnDiscordDirectChannelDeleted(DiscordChannel channel)
{
    Puts("OnDiscordDirectChannelDeleted Works!");
}
```

### OnDiscordGuildChannelDeleted

- Called when a channel has been deleted in a guild.

```c#
void OnDiscordGuildChannelDeleted(DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordGuildChannelDeleted Works!");
}
```

### OnDiscordDirectChannelPinsUpdated

- Called when a direct message (DM) channel has it's pinned messages updated.
  - channel may be null if we haven't seen it before.

```c#
void OnDiscordDirectChannelPinsUpdated(ChannelPinsUpdatedEvent update, DiscordChannel channel)
{
    Puts("OnDiscordDirectChannelPinsUpdated Works!");
}
```

### OnDiscordGuildChannelPinsUpdated

- Called when a guild channel has it's pinned messages updated

```c#
void OnDiscordGuildChannelPinsUpdated(ChannelPinsUpdatedEvent update, DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordGuildChannelPinsUpdated Works!");
}
```

### OnDiscordGuildCreated

- Called when a discord server is fully loaded while connecting or the bot has joined a new discord server

```c#
void OnDiscordGuildCreated(DiscordGuild guild)
{
    Puts("OnDiscordGuildCreated Works!");
}
```

### OnDiscordGuildUpdated

- Called when any updates are made to a guild

```c#
void OnDiscordGuildUpdated(DiscordGuild guild, DiscordGuild previous)
{
    Puts("OnDiscordGuildUpdated Works!");
}
```

### OnDiscordGuildUnavailable

- Called when a guild become unavailable due to a network outage

```c#
void OnDiscordGuildUnavailable(DiscordGuild guild)
{
    Puts("OnDiscordGuildUnavailable Works!");
}
```

### OnDiscordGuildDeleted

- Called when a bot is removed from a discord server or that discord server was deleted

```c#
void OnDiscordGuildUnavailable(DiscordGuild guild)
{
    Puts("OnDiscordGuildUnavailable Works!");
}
```

### OnDiscordGuildMemberBanned

- Called when a guild member is banned

```c#
void OnDiscordGuildMemberBanned(GuildMemberBannedEvent ban, DiscordGuild guild)
{
    Puts("OnDiscordGuildBanAdded Works!");
}
```

### OnDiscordGuildMemberUnbanned

- Called when a guild member is unbanned

```c#
void OnDiscordGuildMemberUnbanned(GuildMemberBannedEvent ban, DiscordGuild guild)
{
    Puts("OnDiscordGuildBanAdded Works!");
}
```

### OnDiscordGuildEmojisUpdated

- Called when the custom emojis for a guild are created/updated/deleted

```c#
void OnDiscordGuildEmojisUpdated(GuildEmojisUpdatedEvent emojis, Hash&lt;Snowflake, DiscordEmoji&gt; previous, DiscordGuild guild)
{
    Puts("OnDiscordGuildEmojisUpdated Works!");
}
```

### OnDiscordGuildStickersUpdated

- Called when the guild stickers are updated

```c#
void OnDiscordGuildStickersUpdated(GuildStickersUpdatedEvent stickers, Hash&lt;Snowflake, DiscordSticker&gt; previous, DiscordGuild guild)
{
    Puts("OnDiscordGuildStickersUpdated Works!");
}
```

### OnDiscordGuildIntegrationsUpdated

- Called when a guild integration is updated

```c#
void OnDiscordGuildIntegrationsUpdated(GuildIntegrationsUpdatedEvent integration, DiscordGuild guild)
{
    Puts("OnDiscordGuildIntegrationsUpdated Works!");
}
```

### OnDiscordGuildMemberAdded

- Called when a guild member has been added to the guild

```c#
void OnDiscordGuildMemberRemoved(GuildMemberRemovedEvent member, DiscordGuild guild)
{
    Puts("OnDiscordGuildMemberRemoved Works!");
}
```

### OnDiscordGuildMemberRemoved

- Called when a guild member has been removed from the guild

```c#
void OnDiscordGuildMemberRemoved(GuildMemberRemovedEvent member, DiscordGuild guild)
{
    Puts("OnDiscordGuildMemberRemoved Works!");
}
```

### OnDiscordGuildMemberUpdated

- Called when a guild member has been updated
  - This also include when the DiscordUser is updated as well

```c#
void OnDiscordGuildMemberUpdated(GuildMemberUpdatedEvent member, DiscordGuild guild)
{
    Puts("OnDiscordGuildMemberUpdated Works!");
}
```

### OnDiscordGuildMembersLoaded

- Called when a guild has finished loading all guild members
  - This Discord Extension requests all guild members in the [OnDiscordGuildCreated](#ondiscordguildcreated) Hook

```c#
void OnDiscordGuildMembersLoaded(DiscordGuild guild)
{
    Puts("OnDiscordGuildMembersLoaded Works!");
}
```

### OnDiscordGuildMembersChunk

- Called in a response to a request for guild member chunks

```c#
void OnDiscordGuildMembersChunk(GuildMembersChunkEvent chunk, DiscordGuild guild)
{
    Puts("OnDiscordGuildMembersChunk Works!");
}
```

### OnDiscordGuildRoleCreated

- Called when a discord guild role is created

```c#
void OnDiscordGuildRoleCreated(Role role, DiscordGuild guild)
{
    Puts("OnDiscordGuildRoleCreated Works!");
}
```

### OnDiscordGuildRoleUpdated

- Called when a discord guild role is updated

```c#
void OnDiscordGuildRoleUpdated(Role role, Role previous, DiscordGuild guild)
{
    Puts("OnDiscordGuildRoleUpdated Works!");
}
```

### OnDiscordGuildRoleDeleted

- Called when a discord guild role is deleted

```c#
void OnDiscordGuildRoleDeleted(Role role, DiscordGuild guild)
{
    Puts("OnDiscordGuildRoleDeleted Works!");
}
```

### OnDiscordDirectMessageCreated

- Called when a message is created in a direct message channel
  - `channel` may be null if we haven't seen it yet

```c#
void OnDiscordDirectMessageCreated(DiscordMessage message, DiscordChannel channel)
{
    Puts("OnDiscordDirectMessageCreated Works!");
}
```

### OnDiscordGuildMessageCreated

- Called when a message is created in a guild channel

```c#
void OnDiscordGuildMessageCreated(DiscordMessage message, DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordGuildMessageCreated Works!");
}
```

### OnDiscordDirectMessageUpdated

- Called when a message is updated in a direct message channel
  - `channel` may be null if we haven't seen it yet

```c#
void OnDiscordDirectMessageUpdated(DiscordMessage message, DiscordChannel channel)
{
    Puts("OnDiscordDirectMessageUpdated Works!");
}
```

### OnDiscordGuildMessageUpdated

- Called when a message is updated in a guild channel

```c#
void OnDiscordDirectMessageUpdated(DiscordMessage message, DiscordChannel channel)
{
    Puts("OnDiscordDirectMessageUpdated Works!");
}
```

### OnDiscordDirectMessageDeleted

- Called when a message is deleted in a direct message channel
  - `channel` may be null if we haven't seen it yet

```c#
void OnDiscordDirectMessageDeleted(DiscordMessage message, DiscordChannel channel)
{
    Puts("OnDiscordDirectMessageDeleted Works!");
}
```

### OnDiscordGuildMessageDeleted

- Called when a message is deleted in a guild channel

```c#
void OnDiscordDirectMessageDeleted(DiscordMessage message, DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordDirectMessageDeleted Works!");
}
```

### OnDiscordDirectMessagesBulkDeleted

- Called when a message is deleted in a direct message channel
  - `channel` may be null if we haven't seen it yet

```c#
void OnDiscordDirectMessagesBulkDeleted(List&lt;Snowflake&gt; messageIds, DiscordChannel channel)
{
    Puts("OnDiscordDirectMessagesBulkDeleted Works!");
}
```

### OnDiscordGuildMessagesBulkDeleted

- Called when a message is deleted in a guild channel

```c#
void OnDiscordGuildMessagesBulkDeleted(List&lt;Snowflake&gt; messageIds, DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordGuildMessagesBulkDeleted Works!");
}
```

### OnDiscordDirectMessageReactionAdded

- Called when a reaction is added to a message in a direct message channel
  - `channel` may be null if we haven't seen it yet

```c#
void OnDiscordDirectMessageReactionAdded(MessageReactionAddedEvent reaction, DiscordChannel channel)
{
    Puts("OnDiscordDirectMessageReactionAdded Works!");
}
```

### OnDiscordGuildMessageReactionAdded

- Called when a reaction is added to a message in a guild channel

```c#
void OnDiscordGuildMessageReactionAdded(MessageReactionAddedEvent reaction, DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordGuildMessageReactionAdded Works!");
}
```

### OnDiscordDirectMessageReactionRemoved

- Called when a reaction is removed from a message in a direct message channel
  - `channel` may be null if we haven't seen it yet

```c#
void OnDiscordDirectMessageReactionRemoved(MessageReactionRemovedEvent reaction, DiscordChannel channel)
{
    Puts("OnDiscordDirectMessageReactionRemoved Works!");
}
```

### OnDiscordGuildMessageReactionRemoved

- Called when a reaction is removed from a message in a guild channel

```c#
void OnDiscordGuildMessageReactionRemoved(MessageReactionRemovedEvent reaction, DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordGuildMessageReactionRemoved Works!");
}
```

### OnDiscordDirectMessageReactionRemovedAll

- Called when all reactions are removed from a message in a direct message channel
  - `channel` may be null if we haven't seen it yet

```c#
void OnDiscordDirectMessageReactionRemovedAll(MessageReactionRemovedAllEmojiEvent reaction, DiscordChannel channel)
{
    Puts("OnDiscordDirectMessageReactionRemovedAll Works!");
}
```

### OnDiscordGuildMessageReactionRemovedAll

- Called when all reactions are removed from a message in a guild channel

```c#
void OnDiscordGuildMessageReactionRemovedAll(MessageReactionRemovedAllEmojiEvent reaction, DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordGuildMessageReactionRemovedAll Works!");
}
```

### OnDiscordDirectMessageReactionEmojiRemoved

- Called when all of a specific reactions is removed from a message in a direct message channel
  - `channel` may be null if we haven't seen it yet

```c#
void OnDiscordDirectMessageReactionEmojiRemoved(MessageReactionRemovedAllEmojiEvent reaction, DiscordChannel channel)
{
    Puts("OnDiscordDirectMessageReactionEmojiRemoved Works!");
}
```

### OnDiscordGuildMessageReactionEmojiRemoved

- Called when all of a specific reaction is removed from a message in a guild channel

```c#
void OnDiscordGuildMessageReactionEmojiRemoved(MessageReactionRemovedAllEmojiEvent reaction, DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordGuildMessageReactionEmojiRemoved Works!");
}
```

### OnDiscordGuildMemberPresenceUpdated

- Called when a guild members presence is updated

```c#
void OnDiscordGuildMemberPresenceUpdated(PresenceUpdatedEvent update, GuildMember member, DiscordGuild guild)
{
    Puts("OnDiscordGuildMemberPresenceUpdated Works!");
}
```

### OnDiscordDirectTypingStarted

- Called typing starts in a direct message channel
  - `channel` may be null if we haven't seen it yet

```c#
void OnDiscordDirectTypingStarted(TypingStartedEvent typing, DiscordChannel channel)
{
    Puts("OnDiscordDirectTypingStarted Works!");
}
```

### OnDiscordGuildTypingStarted

- Called typing starts in a guild channel

```c#
void OnDiscordGuildTypingStarted(TypingStartedEvent typing, DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordGuildTypingStarted Works!");
}
```

### OnDiscordUserUpdated

- Called when a discord user is updated

```c#
void OnDiscordUserUpdated(DiscordUser user)
{
    Puts("OnDiscordUserUpdated Works!");
}
```

### OnDiscordDirectVoiceStateUpdated

- Called when the voice state in a direct message channel is updated
  - `channel` may be null if we haven't seen it yet

```c#
void OnDiscordDirectVoiceStateUpdated(VoiceState voice, DiscordChannel channel)
{
    Puts("OnDiscordDirectVoiceStateUpdated Works!");
}
```

### OnDiscordGuildVoiceStateUpdated

- Called when the voice state in a guild channel is updated

```c#
void OnDiscordGuildVoiceStateUpdated(VoiceState voice, DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordGuildVoiceStateUpdated Works!");
}
```

### OnDiscordGuildVoiceServerUpdated

- Called when the voice server in a guild channel is updated

```c#
void OnDiscordGuildVoiceServerUpdated(VoiceServerUpdatedEvent voice, DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordGuildVoiceServerUpdated Works!");
}
```

### OnDiscordGuildWebhookUpdated

- Called when a webhook ins a guild is updated

```c#
void OnDiscordGuildWebhookUpdated(WebhooksUpdatedEvent webhook, DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordGuildWebhookUpdated Works!");
}
```

### OnDiscordDirectInviteCreated

- Called when an invite to a direct message channel is created
  - `channel` may be null if we haven't seen it yet

```c#
void OnDiscordDirectInviteCreated(InviteCreatedEvent invite, DiscordChannel channel)
{
    Puts("OnDiscordDirectInviteCreated Works!");
}
```

### OnDiscordGuildInviteCreated

- Called when an invite to a guild channel is created

```c#
void OnDiscordGuildInviteCreated(InviteCreatedEvent invite, DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordGuildInviteCreated Works!");
}
```

### OnDiscordDirectInviteDeleted

- Called when an invite to a direct message channel is deleted
  - `channel` may be null if we haven't seen it yet

```c#
void OnDiscordDirectInviteDeleted(InviteCreatedEvent invite, DiscordChannel channel)
{
    Puts("OnDiscordDirectInviteDeleted Works!");
}
```

### OnDiscordGuildInviteDeleted

- Called when an invite to a guild channel is deleted

```c#
void OnDiscordGuildInviteDeleted(InviteCreatedEvent invite, DiscordChannel channel, DiscordGuild guild)
{
    Puts("OnDiscordGuildInviteDeleted Works!");
}
```

### OnDiscordGuildThreadCreated

- Called when a guild thread is created

```c#
void OnDiscordGuildThreadCreated(DiscordChannel thread, DiscordGuild guild) 
{
     Puts("OnDiscordGuildThreadCreated Works!");
}
```

### OnDiscordGuildThreadUpdated

- Called when a guild thread is updated

```c#
void OnDiscordGuildThreadUpdated(DiscordChannel thread, DiscordChannel previous, DiscordGuild guild) 
{
     Puts("OnDiscordGuildThreadUpdated Works!");
}
```

### OnDiscordGuildThreadDeleted

- Called when a guild thread is deleted

```c#
void OnDiscordGuildThreadDeleted(DiscordChannel thread, DiscordGuild guild) 
{
     Puts("OnDiscordGuildThreadDeleted Works!");
}
```

### OnDiscordGuildThreadListSynced

- Called when a guild thread list is synced

```c#
void OnDiscordGuildThreadListSynced(ThreadListSyncEvent sync, DiscordGuild guild) 
{
     Puts("OnDiscordGuildThreadListSynced Works!");
}
```

### OnDiscordGuildThreadMemberUpdated

- Called when a thread member is updated

```c#
void OnDiscordGuildThreadMemberUpdated(ThreadMember member, DiscordChannel thread, DiscordGuild guild) 
{
     Puts("OnDiscordGuildThreadMemberUpdated Works!");
}
```

### OnDiscordGuildThreadMembersUpdated

- Called when thread members are updated

```c#
void OnDiscordGuildThreadMembersUpdated(ThreadMembersUpdatedEvent members, DiscordGuild guild) 
{
     Puts("OnDiscordGuildThreadMembersUpdated Works!");
}
```

### OnDiscordStageInstanceCreated

- Called when a stage intance is created

```c#
void OnDiscordStageInstanceCreated(StageInstance stage, DiscordGuild guild) 
{
     Puts("OnDiscordStageInstanceCreated Works!");
}
```

### OnDiscordStageInstanceUpdated

- Called when a stage instance is updated

```c#
void OnDiscordStageInstanceUpdated(StageInstance stage, StageInstance previous, DiscordGuild guild) 
{
     Puts("OnDiscordStageInstanceUpdated Works!");
}
```

### OnDiscordStageInstanceDeleted

- Called when a stage instance is deleted

```c#
void OnDiscordStageInstanceDeleted(StageInstance stage, DiscordGuild guild) 
{
     Puts("OnDiscordStageInstanceDeleted Works!");
}
```

### OnDiscordInteractionCreated

- Called when a users uses a slash command, user command, message command, or a message component

```c#
void OnDiscordInteractionCreated(DiscordInteraction interaction) 
{
     Puts("OnDiscordInteractionCreated Works!");
}
```

### OnDiscordGuildIntegrationCreated

- Called when an integration is created on a guild

```c#
void OnDiscordGuildIntegrationCreated(IntegrationCreatedEvent integration, DiscordGuild guild)
{
    Puts("OnDiscordGuildIntegrationCreated Works!");
}
```

### OnDiscordGuildIntegrationUpdated

- Called when an integration is updated on a guild

```c#
void OnDiscordGuildIntegrationUpdated(IntegrationUpdatedEvent interaction, DiscordGuild guild)
{
    Puts("OnDiscordGuildIntegrationUpdated Works!");
}
```

### OnDiscordGuildIntegrationDeleted

- Called when an integration is deleted on a guild

```c#
void OnDiscordIntegrationDeleted(IntegrationDeletedEvent interaction, DiscordGuild guild)
{
    Puts("OnDiscordIntegrationDeleted Works!");
}
```

### OnDiscordApplicationCommandCreated

- Called when a application command is created for a guild

```c#
void OnDiscordApplicationCommandCreated(ApplicationCommandEvent commandEvent, DiscordGuild guild)
{
    Puts("OnDiscordApplicationCommandCreated Works!");
}
```

### OnDiscordApplicationCommandUpdated

- Called when a application command is updated for a guild

```c#
void OnDiscordApplicationCommandUpdated(ApplicationCommandEvent commandEvent, DiscordGuild guild)
{
    Puts("OnDiscordApplicationCommandUpdated Works!");
}
```

### OnDiscordApplicationCommandDeleted

- Called when a application command is deleted for a guild

```c#
void OnDiscordApplicationCommandDeleted(ApplicationCommandEvent commandEvent, DiscordGuild guild)
{
    Puts("OnDiscordApplicationCommandDeleted Works!");
}
```

### OnDiscordUnhandledCommand

- Called when we receive an event we do not handle yet.
  - If you need this event you can listen to it using this hook until we support it
  - Please create an issue on uMod if this error ever occurs

```c#
void OnDiscordUnhandledCommand(EventPayload payload)
{
    Puts("OnDiscordUnhandledCommand Works!");
}
```