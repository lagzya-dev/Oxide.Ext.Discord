namespace Oxide.Ext.Discord.Constants
{
    /// <summary>
    /// Represents all hooks available in the discord extension
    /// </summary>
    public static class DiscordHooks
    {
        #region Bot Client Hooks
        /// <code>
        /// void DiscordOnClientConnected(Plugin owner, DiscordClient client)
        /// {
        ///     Puts("DiscordOnClientConnected Works!");
        /// }
        /// </code>
        public const string DiscordOnClientConnected = nameof(DiscordOnClientConnected);
        
        /// <code>
        /// void DiscordOnClientDisconnected(Plugin owner, DiscordClient client)
        /// {
        ///     Puts("DiscordOnClientDisconnected Works!");
        /// }
        /// </code>
        public const string DiscordOnClientDisconnected = nameof(DiscordOnClientDisconnected);
        
        /// <code>
        /// void OnDiscordClientCreated()
        /// {
        ///     Puts("OnDiscordClientCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordClientCreated = nameof(OnDiscordClientCreated);
        #endregion

        #region Socket Hooks
        /// <code>
        /// void OnDiscordWebsocketOpened()
        /// {
        ///     Puts("OnDiscordWebsocketOpened Works!");
        /// }
        /// </code>
        public const string OnDiscordWebsocketOpened = nameof(OnDiscordWebsocketOpened);
        
        /// <code>
        /// void OnDiscordWebsocketClosed(string reason, ushort code, bool wasClean)
        /// {
        ///     Puts("OnDiscordWebsocketClosed Works!");
        /// }
        /// </code>
        public const string OnDiscordWebsocketClosed = nameof(OnDiscordWebsocketClosed);
        
        /// <code>
        /// void OnDiscordWebsocketErrored(Exception ex, string message)
        /// {
        ///     Puts("OnDiscordWebsocketErrored Works!");
        /// }
        /// </code>
        public const string OnDiscordWebsocketErrored = nameof(OnDiscordWebsocketErrored);
        
        /// <code>
        /// void OnDiscordSetupHeartbeat(float heartbeat)
        /// {
        ///     Puts("OnDiscordHeartbeatSent Works!");
        /// }
        /// </code>
        public const string OnDiscordSetupHeartbeat = nameof(OnDiscordSetupHeartbeat);
        
        /// <code>
        /// void OnDiscordHeartbeatSent()
        /// {
        ///     Puts("OnDiscordHeartbeatSent Works!");
        /// }
        /// </code>
        public const string OnDiscordHeartbeatSent = nameof(OnDiscordHeartbeatSent);
        #endregion

        #region Link Hooks
        /// <code>
        /// void OnDiscordPlayerLinked(IPlayer player, DiscordUser discord)
        /// {
        ///     Puts("OnDiscordPlayerLinked Works!");
        /// }
        /// </code>
        public const string OnDiscordPlayerLinked = nameof(OnDiscordPlayerLinked);
        
        /// <code>
        /// void OnDiscordPlayerUnlinked(IPlayer player, DiscordUser discord)
        /// {
        ///     Puts("OnDiscordPlayerUnlinked Works!");
        /// }
        /// </code>
        public const string OnDiscordPlayerUnlinked = nameof(OnDiscordPlayerUnlinked);
        #endregion

        #region Discord Event Hooks
        /// <code>
        /// void OnDiscordGatewayReady(GatewayReadyEvent ready)
        /// {
        ///     Puts("OnDiscordGatewayReady Works!");
        /// }
        /// </code>
        public const string OnDiscordGatewayReady = nameof(OnDiscordGatewayReady);
        
        /// <code>
        /// void OnDiscordGatewayResumed(GatewayResumedEvent resume)
        /// {
        ///     Puts("OnDiscordGatewayResumed Works!");
        /// }
        /// </code>
        public const string OnDiscordGatewayResumed = nameof(OnDiscordGatewayResumed);
        
        /// <code>
        /// void OnDiscordDirectChannelCreated(Channel channel)
        /// {
        ///     Puts("OnDiscordDirectChannelCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectChannelCreated = nameof(OnDiscordDirectChannelCreated);
        
        /// <code>
        /// void OnDiscordGuildChannelCreated(Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildChannelCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildChannelCreated = nameof(OnDiscordGuildChannelCreated);
        
        /// <code>
        /// Note: previous will be null if previous channel not found
        /// void OnDiscordDirectChannelUpdated(Channel channel, Channel previous)
        /// {
        ///     Puts("OnDiscordDirectChannelUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectChannelUpdated = nameof(OnDiscordDirectChannelUpdated);
        
        /// <code>
        /// void OnDiscordGuildChannelUpdated(Channel channel, Channel previous, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildChannelUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildChannelUpdated = nameof(OnDiscordGuildChannelUpdated);
        
        /// <code>
        /// Note: Not sure if this will ever happen but we handle it if it does
        /// void OnDiscordDirectChannelDeleted(Channel channel)
        /// {
        ///     Puts("OnDiscordDirectChannelDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectChannelDeleted = nameof(OnDiscordDirectChannelDeleted);
        
        /// <code>
        /// void OnDiscordGuildChannelDeleted(Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildChannelDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildChannelDeleted = nameof(OnDiscordGuildChannelDeleted);
        
        /// <code>
        /// Note: Channel will be null if we haven't seen it yet
        /// void OnDiscordDirectChannelPinsUpdated(ChannelPinsUpdatedEvent update, Channel channel)
        /// {
        ///     Puts("OnDiscordDirectChannelPinsUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectChannelPinsUpdated = nameof(OnDiscordDirectChannelPinsUpdated);
        
        /// <code>
        /// void OnDiscordGuildChannelPinsUpdated(ChannelPinsUpdatedEvent update, Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildChannelPinsUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildChannelPinsUpdated = nameof(OnDiscordGuildChannelPinsUpdated);
        
        /// <code>
        /// void OnDiscordGuildCreated(Guild guild)
        /// {
        ///     Puts("OnDiscordGuildCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildCreated = nameof(OnDiscordGuildCreated);
        
        /// <code>
        /// Note: previous will be null if guild previously not loaded
        /// void OnDiscordGuildUpdated(Guild guild, Guild previous)
        /// {
        ///     Puts("OnDiscordGuildUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildUpdated = nameof(OnDiscordGuildUpdated);
        
        /// <code>
        /// void OnDiscordGuildUnavailable(Guild guild)
        /// {
        ///     Puts("OnDiscordGuildUnavailable Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildUnavailable = nameof(OnDiscordGuildUnavailable);
        
        /// <code>
        /// void OnDiscordGuildDeleted(Guild guild)
        /// {
        ///     Puts("OnDiscordGuildDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildDeleted = nameof(OnDiscordGuildDeleted);
        
        /// <code>
        /// void OnDiscordGuildMemberBanned(GuildMemberBannedEvent ban, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberBanned Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberBanned = nameof(OnDiscordGuildMemberBanned);
        
        /// <code>
        /// void OnDiscordGuildMemberUnbanned(GuildMemberBannedEvent ban, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildBanRemoved Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberUnbanned = nameof(OnDiscordGuildMemberUnbanned);
        
        /// <code>
        /// void OnDiscordGuildEmojisUpdated(GuildEmojisUpdatedEvent emojis, Hash&lt;Snowflake, Emoji&gt; previous, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildEmojisUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildEmojisUpdated = nameof(OnDiscordGuildEmojisUpdated);
        
        /// <code>
        /// void OnDiscordGuildIntegrationsUpdated(GuildIntegrationsUpdatedEvent integration, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildIntegrationsUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildIntegrationsUpdated = nameof(OnDiscordGuildIntegrationsUpdated);
        
        /// <code>
        /// void OnDiscordGuildMemberAdded(GuildMemberAddedEvent member, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberAdded Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberAdded = nameof(OnDiscordGuildMemberAdded);
        
        /// <code>
        /// void OnDiscordGuildMemberRemoved(GuildMemberRemovedEvent member, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberRemoved Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberRemoved = nameof(OnDiscordGuildMemberRemoved);
        
        /// <code>
        /// void OnDiscordGuildMemberUpdated(GuildMemberUpdatedEvent member, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberUpdated = nameof(OnDiscordGuildMemberUpdated);
        
        /// <code>
        /// void OnDiscordGuildMembersLoaded(Guild guild)
        /// {
        ///     Puts("OnDiscordGuildMembersLoaded Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMembersLoaded = nameof(OnDiscordGuildMembersLoaded);
        
        /// <code>
        /// void OnDiscordGuildMembersChunk(GuildMembersChunkEvent chunk, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildMembersChunk Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMembersChunk = nameof(OnDiscordGuildMembersChunk);
        
        /// <code>
        /// void OnDiscordGuildRoleCreated(Role role, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildRoleCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildRoleCreated = nameof(OnDiscordGuildRoleCreated);
        
        /// <code>
        /// void OnDiscordGuildRoleUpdated(Role role, Role previous, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildRoleUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildRoleUpdated = nameof(OnDiscordGuildRoleUpdated);
        
        /// <code>
        /// void OnDiscordGuildRoleDeleted(Role role, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildRoleDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildRoleDeleted = nameof(OnDiscordGuildRoleDeleted);
        
        /// <code>
        /// void OnDiscordCommand(DiscordMessage message, string cmd, string[] args)
        /// {
        ///     Puts("OnDiscordCommand Works!");
        /// }
        /// </code>
        public const string OnDiscordCommand = nameof(OnDiscordCommand);
        
        /// <code>
        /// Note: Channel may be null if we haven't seen it yet
        /// void OnDiscordDirectMessageCreated(DiscordMessage message, Channel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessageCreated = nameof(OnDiscordDirectMessageCreated);
        
        /// <code>
        /// void OnDiscordGuildMessageCreated(DiscordMessage message, Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildMessageCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessageCreated = nameof(OnDiscordGuildMessageCreated);
        
        /// <code>
        /// void OnDiscordDirectMessageUpdated(DiscordMessage message, Channel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessageUpdated = nameof(OnDiscordDirectMessageUpdated);
        
        /// <code>
        /// void OnDiscordDirectMessageUpdated(DiscordMessage message, Channel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessageUpdated = nameof(OnDiscordGuildMessageUpdated);
        
        /// <code>
        /// void OnDiscordDirectMessageDeleted(DiscordMessage message, Channel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessageDeleted = nameof(OnDiscordDirectMessageDeleted);
        
        /// <code>
        /// void OnDiscordDirectMessageDeleted(DiscordMessage message, Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordDirectMessageDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessageDeleted = nameof(OnDiscordGuildMessageDeleted);
        
        /// <code>
        /// void OnDiscordDirectMessagesBulkDeleted(List&lt;Snowflake&gt; messageIds, Channel channel)
        /// {
        ///     Puts("OnDiscordDirectMessagesBulkDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessagesBulkDeleted = nameof(OnDiscordDirectMessagesBulkDeleted);
        
        /// <code>
        /// void OnDiscordGuildMessagesBulkDeleted(List&lt;Snowflake&gt; messageIds, Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildMessagesBulkDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessagesBulkDeleted = nameof(OnDiscordDirectMessagesBulkDeleted);
        
        /// <code>
        /// void OnDiscordDirectMessageReactionAdded(MessageReactionAddedEvent reaction, Channel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageReactionAdded Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessageReactionAdded = nameof(OnDiscordDirectMessageReactionAdded);
        
        /// <code>
        /// void OnDiscordGuildMessageReactionAdded(MessageReactionAddedEvent reaction, Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildMessageReactionAdded Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessageReactionAdded = nameof(OnDiscordGuildMessageReactionAdded);
        
        /// <code>
        /// void OnDiscordDirectMessageReactionRemoved(MessageReactionRemovedEvent reaction, Channel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageReactionRemoved Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessageReactionRemoved = nameof(OnDiscordDirectMessageReactionRemoved);
        
        /// <code>
        /// void OnDiscordGuildMessageReactionRemoved(MessageReactionRemovedEvent reaction, Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildMessageReactionRemoved Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessageReactionRemoved = nameof(OnDiscordGuildMessageReactionRemoved);
        
        /// <code>
        /// void OnDiscordDirectMessageReactionRemovedAll(MessageReactionRemovedAllEmojiEvent reaction, Channel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageReactionRemovedAll Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessageReactionRemovedAll = nameof(OnDiscordDirectMessageReactionRemoved);
        
        /// <code>
        /// void OnDiscordGuildMessageReactionRemovedAll(MessageReactionRemovedAllEmojiEvent reaction, Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildMessageReactionRemovedAll Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessageReactionRemovedAll = nameof(OnDiscordGuildMessageReactionRemoved);
        
        /// <code>
        /// void OnDiscordDirectMessageReactionEmojiRemoved(MessageReactionRemovedAllEmojiEvent reaction, Channel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageReactionEmojiRemoved Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessageReactionEmojiRemoved = nameof(OnDiscordDirectMessageReactionEmojiRemoved);
        
        /// <code>
        /// void OnDiscordGuildMessageReactionEmojiRemoved(MessageReactionRemovedAllEmojiEvent reaction, Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildMessageReactionEmojiRemoved Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessageReactionEmojiRemoved = nameof(OnDiscordGuildMessageReactionEmojiRemoved);
        
        /// <code>
        /// void OnDiscordGuildMemberPresenceUpdated(PresenceUpdatedEvent update, GuildMember member, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberPresenceUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberPresenceUpdated = nameof(OnDiscordGuildMemberPresenceUpdated);
        
        /// <code>
        /// void OnDiscordDirectTypingStarted(TypingStartedEvent typing, Channel channel)
        /// {
        ///     Puts("OnDiscordDirectTypingStarted Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectTypingStarted = nameof(OnDiscordDirectTypingStarted);
        
        /// <code>
        /// void OnDiscordGuildTypingStarted(TypingStartedEvent typing, Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildTypingStarted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildTypingStarted = nameof(OnDiscordGuildTypingStarted);
        
        /// <code>
        /// void OnDiscordUserUpdated(DiscordUser user)
        /// {
        ///     Puts("OnDiscordUserUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordUserUpdated = nameof(OnDiscordUserUpdated);
        
        /// <code>
        /// void OnDiscordDirectVoiceStateUpdated(VoiceState voice, Channel channel)
        /// {
        ///     Puts("OnDiscordDirectVoiceStateUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectVoiceStateUpdated = nameof(OnDiscordDirectVoiceStateUpdated);
        
        /// <code>
        /// void OnDiscordGuildVoiceStateUpdated(VoiceState voice, Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildVoiceStateUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildVoiceStateUpdated = nameof(OnDiscordGuildVoiceStateUpdated);
        
        /// <code>
        /// void OnDiscordGuildVoiceServerUpdated(VoiceServerUpdatedEvent voice, Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildVoiceServerUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildVoiceServerUpdated = nameof(OnDiscordGuildVoiceServerUpdated);
        
        /// <code>
        /// void OnDiscordGuildWebhookUpdated(WebhooksUpdatedEvent webhook, Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildWebhookUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildWebhookUpdated = nameof(OnDiscordGuildWebhookUpdated);
        
        /// <code>
        /// void OnDiscordDirectInviteCreated(InviteCreatedEvent invite, Channel channel)
        /// {
        ///     Puts("OnDiscordDirectInviteCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectInviteCreated = nameof(OnDiscordDirectInviteCreated);
        
        /// <code>
        /// void OnDiscordGuildInviteCreated(InviteCreatedEvent invite, Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildInviteCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildInviteCreated = nameof(OnDiscordGuildInviteCreated);
        
        /// <code>
        /// void OnDiscordDirectInviteDeleted(InviteCreatedEvent invite, Channel channel)
        /// {
        ///     Puts("OnDiscordDirectInviteDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectInviteDeleted = nameof(OnDiscordDirectInviteDeleted);
        
        /// <code>
        /// void OnDiscordGuildInviteDeleted(InviteCreatedEvent invite, Channel channel, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildInviteDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildInviteDeleted = nameof(OnDiscordGuildInviteDeleted);
        
        /// <code>
        /// void OnDiscordInteractionCreated(DiscordInteraction interaction)
        /// {
        ///     Puts("OnDiscordInteractionCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordInteractionCreated = nameof(OnDiscordInteractionCreated);
        
        /// <code>
        /// void OnDiscordGuildIntegrationCreated(IntegrationCreatedEvent integration, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildIntegrationCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildIntegrationCreated = nameof(OnDiscordGuildIntegrationCreated);
        
        /// <code>
        /// void OnDiscordGuildIntegrationUpdated(IntegrationUpdatedEvent interaction, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildIntegrationUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildIntegrationUpdated = nameof(OnDiscordGuildIntegrationUpdated);
        
        /// <code>
        /// void OnDiscordIntegrationDeleted(IntegrationDeletedEvent interaction, Guild guild)
        /// {
        ///     Puts("OnDiscordIntegrationDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordIntegrationDeleted = nameof(OnDiscordIntegrationDeleted);
        
        /// <code>
        /// void OnDiscordApplicationCommandCreated(ApplicationCommandEvent commandEvent, Guild guild)
        /// {
        ///     Puts("OnDiscordApplicationCommandCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordApplicationCommandCreated = nameof(OnDiscordApplicationCommandCreated);
        
        /// <code>
        /// void OnDiscordApplicationCommandUpdated(ApplicationCommandEvent commandEvent, Guild guild)
        /// {
        ///     Puts("OnDiscordApplicationCommandUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordApplicationCommandUpdated = nameof(OnDiscordApplicationCommandUpdated);
        
        /// <code>
        /// void OnDiscordApplicationCommandDeleted(ApplicationCommandEvent commandEvent, Guild guild)
        /// {
        ///     Puts("OnDiscordApplicationCommandDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordApplicationCommandDeleted = nameof(OnDiscordApplicationCommandDeleted);
        
        /// <code>
        /// void OnDiscordGuildThreadCreated(Channel thread, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildThreadCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildThreadCreated = nameof(OnDiscordGuildThreadCreated);
        
        /// <code>
        /// void OnDiscordGuildThreadUpdated(Channel thread, Channel previous, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildThreadUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildThreadUpdated = nameof(OnDiscordGuildThreadUpdated);

        /// <code>
        /// void OnDiscordGuildThreadDeleted(Channel thread, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildThreadDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildThreadDeleted = nameof(OnDiscordGuildThreadDeleted);
        
        /// <code>
        /// void OnDiscordGuildThreadListSynced(ThreadListSyncEvent sync, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildThreadListSynced Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildThreadListSynced = nameof(OnDiscordGuildThreadListSynced);
        
        /// <code>
        /// void OnDiscordGuildThreadMemberUpdated(ThreadMember member, Channel thread, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildThreadMemberUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildThreadMemberUpdated = nameof(OnDiscordGuildThreadMemberUpdated);
        
        /// <code>
        /// void OnDiscordGuildThreadMembersUpdated(ThreadMembersUpdatedEvent members, Guild guild)
        /// {
        ///     Puts("OnDiscordGuildThreadMembersUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildThreadMembersUpdated = nameof(OnDiscordGuildThreadMembersUpdated);
        
        /// <code>
        /// void OnDiscordUnhandledCommand(EventPayload payload)
        /// {
        ///     Puts("OnDiscordUnhandledCommand Works!");
        /// }
        /// </code>
        public const string OnDiscordUnhandledCommand = nameof(OnDiscordUnhandledCommand);        
        
        /// <code>
        /// void OnDiscordStageInstanceCreated(StageInstance stage, Guild guild)
        /// {
        ///     Puts("OnDiscordStageInstanceCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordStageInstanceCreated = nameof(OnDiscordStageInstanceCreated);
        
        /// <code>
        /// void OnDiscordStageInstanceUpdated(StageInstance stage, StageInstance previous, Guild guild)
        /// {
        ///     Puts("OnDiscordStageInstanceUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordStageInstanceUpdated = nameof(OnDiscordStageInstanceUpdated);
        
        /// <code>
        /// void OnDiscordStageInstanceDeleted(StageInstance stage, Guild guild)
        /// {
        ///     Puts("OnDiscordStageInstanceDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordStageInstanceDeleted = nameof(OnDiscordStageInstanceDeleted);
        #endregion
    }
}