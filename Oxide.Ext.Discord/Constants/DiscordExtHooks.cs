using System;
using System.Collections.Generic;
using System.Reflection;
using Oxide.Ext.Discord.Entities;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Constants
{
    /// <summary>
    /// Represents all hooks available in the discord extension
    /// </summary>
    public static class DiscordExtHooks
    {
        /// <summary>
        /// Hooks that are called on Discord Plugins
        /// </summary>
        private static readonly HashSet<string> AllHooks = new HashSet<string>();
        
        /// <summary>
        /// Hooks that are called on Discord Plugins
        /// </summary>
        private static readonly HashSet<string> PluginHooks = new HashSet<string>();
        
        /// <summary>
        /// Hooks that are call globally
        /// </summary>
        private static readonly HashSet<string> GlobalHooks = new HashSet<string>
        {
            OnDiscordPlayerLinked,
            OnDiscordPlayerUnlink,
            OnDiscordPlayerUnlinked,
        };

        /// <summary>
        /// A mapping of Gateway Intent to Hooks
        /// </summary>
        private static readonly Hash<GatewayIntents, List<string>> GatewayIntentHooks = new Hash<GatewayIntents, List<string>>
        {
            [GatewayIntents.Guilds] = new List<string>
            {
                OnDiscordGuildCreated,
                OnDiscordGuildUpdated,
                OnDiscordGuildDeleted,
                OnDiscordGuildUnavailable,
                OnDiscordGuildRoleCreated,
                OnDiscordGuildRoleUpdated,
                OnDiscordGuildRoleDeleted,
                OnDiscordGuildChannelCreated,
                OnDiscordGuildChannelUpdated,
                OnDiscordGuildChannelDeleted,
                OnDiscordGuildChannelPinsUpdated,
                OnDiscordGuildThreadCreated,
                OnDiscordGuildThreadUpdated,
                OnDiscordGuildThreadListSynced,
                OnDiscordGuildThreadMemberUpdated,
                OnDiscordGuildThreadMembersUpdated,
                OnDiscordStageInstanceCreated,
                OnDiscordStageInstanceUpdated,
                OnDiscordStageInstanceDeleted
            },
            [GatewayIntents.GuildMembers] = new List<string>
            {
                OnDiscordGuildMemberAdded,
                OnDiscordGuildMemberUpdated,
                OnDiscordGuildMemberRemoved,
                OnDiscordGuildMemberRoleAdded,
                OnDiscordGuildMemberRoleRemoved,
                OnDiscordGuildMemberBoosted,
                OnDiscordGuildMemberBoostExtended,
                OnDiscordGuildMemberBoostEnded,
                OnDiscordGuildMemberNicknameUpdated,
                OnDiscordGuildMemberAvatarUpdated,
                OnDiscordGuildMemberDeafened,
                OnDiscordGuildMemberUndeafened,
                OnDiscordGuildMemberMuted,
                OnDiscordGuildMemberUnmuted,
                OnDiscordGuildMemberTimeout,
                OnDiscordGuildMemberTimeoutEnded,
                OnDiscordGuildThreadMembersUpdated
            },
            [GatewayIntents.GuildModeration] = new List<string>
            {
                OnDiscordGuildMemberBanned,
                OnDiscordGuildMemberUnbanned
            },
            [GatewayIntents.GuildEmojisAndStickers] = new List<string>
            {
                OnDiscordGuildEmojisUpdated,
                OnDiscordGuildStickersUpdated
            },
            [GatewayIntents.GuildIntegrations] = new List<string>
            {
                OnDiscordGuildIntegrationsUpdated,
                OnDiscordGuildIntegrationCreated,
                OnDiscordGuildIntegrationUpdated,
                OnDiscordGuildIntegrationDeleted
            },
            [GatewayIntents.GuildWebhooks] = new List<string>
            {
                OnDiscordGuildWebhookUpdated,
            },
            [GatewayIntents.GuildInvites] = new List<string>
            {
                OnDiscordGuildInviteCreated,
                OnDiscordGuildIntegrationDeleted,
            },
            [GatewayIntents.GuildVoiceStates] = new List<string>
            {
                OnDiscordGuildVoiceStateUpdated
            },
            [GatewayIntents.GuildPresences] = new List<string>
            {
                OnDiscordGuildMemberPresenceUpdated
            },
            [GatewayIntents.GuildMessages] = new List<string>
            {
                OnDiscordGuildMessageCreated,
                OnDiscordGuildMessageUpdated,
                OnDiscordGuildMessageDeleted,
                OnDiscordGuildMessagesBulkDeleted,
            },
            [GatewayIntents.GuildMessageReactions] = new List<string>
            {
                OnDiscordGuildMessageReactionAdded,
                OnDiscordGuildMessageReactionRemoved,
                OnDiscordGuildMessageReactionRemovedAll,
                OnDiscordGuildMessageReactionEmojiRemoved,
            },
            [GatewayIntents.GuildMessageTyping] = new List<string>
            {
                OnDiscordGuildTypingStarted
            },
            [GatewayIntents.DirectMessages] = new List<string>
            {
                OnDiscordDirectMessageCreated,
                OnDiscordDirectMessageUpdated,
                OnDiscordDirectMessageDeleted,
                OnDiscordDirectMessagesBulkDeleted,
            },
            [GatewayIntents.DirectMessageReactions] = new List<string>
            {
                OnDiscordDirectMessageReactionAdded,
                OnDiscordDirectMessageReactionRemoved,
                OnDiscordDirectMessageReactionRemovedAll,
                OnDiscordDirectMessageReactionEmojiRemoved,
            },
            [GatewayIntents.DirectMessageTyping] = new List<string>
            {
                OnDiscordDirectTypingStarted
            },
            [GatewayIntents.GuildScheduledEvents] = new List<string>
            {
                OnDiscordGuildScheduledEventCreated,
                OnDiscordGuildScheduledEventUpdated,
                OnDiscordGuildScheduledEventDeleted,
                OnDiscordGuildScheduledEventUserAdded,
                OnDiscordGuildScheduledEventUserRemoved,
            },
            [GatewayIntents.AutoModerationConfiguration] = new List<string>
            {
                OnDiscordAutoModRuleCreated,
                OnDiscordAutoModRuleUpdated,
                OnDiscordAutoModRuleDeleted,
            },
            [GatewayIntents.AutoModerationExecution] = new List<string>
            {
                OnDiscordAutoModActionExecuted
            },
        };
        
        /// <summary>
        /// A mapping of Hooks required Gateway Intent
        /// </summary>
        public static readonly Hash<string, GatewayIntents> HookGatewayIntent = new Hash<string, GatewayIntents>();

        static DiscordExtHooks()
        {
            Type stringType = typeof(string);
            foreach (FieldInfo field in typeof(DiscordExtHooks).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            {
                if (field.IsLiteral && !field.IsInitOnly && field.FieldType == stringType)
                {
                    string hook = (string)field.GetRawConstantValue();
                    AllHooks.Add(hook);
                    if (!GlobalHooks.Contains(hook))
                    {
                        PluginHooks.Add(hook);
                    }
                }
            }

            foreach (KeyValuePair<GatewayIntents, List<string>> intentHooks in GatewayIntentHooks)
            {
                foreach (string hook in intentHooks.Value)
                {
                    HookGatewayIntent[hook] |= intentHooks.Key;
                }
            }
        }

        /// <summary>
        /// Returns true if the hook is a Discord Extension Global Hook
        /// </summary>
        /// <param name="hook">Name of the hook</param>
        /// <returns></returns>
        public static bool IsGlobalHook(string hook) => GlobalHooks.Contains(hook);
        
        /// <summary>
        /// Returns true if the hook is a Discord Extension Plugin Hook
        /// </summary>
        /// <param name="hook">Name of the hook</param>
        /// <returns></returns>
        public static bool IsPluginHook(string hook) => PluginHooks.Contains(hook);
        
        /// <summary>
        /// Returns true if the hook is a Discord Extension Hook
        /// </summary>
        /// <param name="hook">Name of the hook</param>
        /// <returns></returns>
        public static bool IsDiscordHook(string hook) => AllHooks.Contains(hook);

        #region Bot Client Hooks
        /// <code>
        /// void OnDiscordClientCreated()
        /// {
        ///     Puts("OnDiscordClientCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordClientCreated = nameof(OnDiscordClientCreated);
        
        /// <code>
        /// void OnDiscordBotFullyLoaded()
        /// {
        ///     Puts("OnDiscordBotFullyLoaded Works!");
        /// }
        /// </code>
        public const string OnDiscordBotFullyLoaded = nameof(OnDiscordBotFullyLoaded);
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
        /// void OnDiscordPlayerUnlink(IPlayer player, DiscordUser discord)
        /// {
        ///     Puts("OnDiscordPlayerUnlink Works!");
        /// }
        /// </code>
        public const string OnDiscordPlayerUnlink = nameof(OnDiscordPlayerUnlink);
        
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
        /// void OnDiscordGatewayReconnected()
        /// {
        ///     Puts("OnDiscordGatewayReconnected Works!");
        /// }
        /// </code>
        public const string OnDiscordGatewayReconnected = nameof(OnDiscordGatewayReconnected);
        
        /// <code>
        /// void OnDiscordDirectChannelCreated(Channel channel)
        /// {
        ///     Puts("OnDiscordDirectChannelCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectChannelCreated = nameof(OnDiscordDirectChannelCreated);
        
        /// <code>
        /// void OnDiscordGuildChannelCreated(Channel channel, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildChannelCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildChannelCreated = nameof(OnDiscordGuildChannelCreated);
        
        /// <code>
        /// Note: previous will be null if previous channel not found
        /// void OnDiscordDirectChannelUpdated(Channel channel, DiscordChannel previous)
        /// {
        ///     Puts("OnDiscordDirectChannelUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectChannelUpdated = nameof(OnDiscordDirectChannelUpdated);
        
        /// <code>
        /// void OnDiscordGuildChannelUpdated(DiscordChannel channel, DiscordChannel previous, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildChannelUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildChannelUpdated = nameof(OnDiscordGuildChannelUpdated);
        
        /// <code>
        /// Note: Not sure if this will ever happen but we handle it if it does
        /// void OnDiscordDirectChannelDeleted(DiscordChannel channel)
        /// {
        ///     Puts("OnDiscordDirectChannelDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectChannelDeleted = nameof(OnDiscordDirectChannelDeleted);
        
        /// <code>
        /// void OnDiscordGuildChannelDeleted(DiscordChannel channel, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildChannelDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildChannelDeleted = nameof(OnDiscordGuildChannelDeleted);
        
        /// <code>
        /// Note: Channel will be null if we haven't seen it yet
        /// void OnDiscordDirectChannelPinsUpdated(ChannelPinsUpdatedEvent update, DiscordChannel channel)
        /// {
        ///     Puts("OnDiscordDirectChannelPinsUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectChannelPinsUpdated = nameof(OnDiscordDirectChannelPinsUpdated);
        
        /// <code>
        /// void OnDiscordEntitlementCreated(DiscordEntitlement entitlement, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordEntitlementCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordEntitlementCreated = nameof(OnDiscordEntitlementCreated);
        
        /// <code>
        /// void OnDiscordEntitlementUpdated(DiscordEntitlement entitlement, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordEntitlementUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordEntitlementUpdated = nameof(OnDiscordEntitlementUpdated);
        
        /// <code>
        /// void OnDiscordEntitlementDeleted(DiscordEntitlement entitlement, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordEntitlementDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordEntitlementDeleted = nameof(OnDiscordEntitlementDeleted);
        
        /// <code>
        /// void OnDiscordGuildChannelPinsUpdated(ChannelPinsUpdatedEvent update, DiscordChannel channel, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildChannelPinsUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildChannelPinsUpdated = nameof(OnDiscordGuildChannelPinsUpdated);
        
        /// <code>
        /// void OnDiscordGuildCreated(GuildDiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildCreated = nameof(OnDiscordGuildCreated);
        
        /// <code>
        /// Note: previous will be null if guild previously not loaded
        /// void OnDiscordGuildUpdated(DiscordGuild guild, DiscordGuild previous)
        /// {
        ///     Puts("OnDiscordGuildUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildUpdated = nameof(OnDiscordGuildUpdated);
        
        /// <code>
        /// void OnDiscordGuildUnavailable(DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildUnavailable Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildUnavailable = nameof(OnDiscordGuildUnavailable);
        
        /// <code>
        /// void OnDiscordGuildDeleted(DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildDeleted = nameof(OnDiscordGuildDeleted);
        
        /// <code>
        /// void OnDiscordGuildMemberBanned(GuildMemberBannedEvent ban, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberBanned Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberBanned = nameof(OnDiscordGuildMemberBanned);
        
        /// <code>
        /// void OnDiscordGuildMemberUnbanned(GuildMemberBannedEvent ban, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildBanRemoved Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberUnbanned = nameof(OnDiscordGuildMemberUnbanned);
        
        /// <code>
        /// void OnDiscordGuildEmojisUpdated(GuildEmojisUpdatedEvent emojis, Hash&lt;Snowflake, DiscordEmoji&gt; previous, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildEmojisUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildEmojisUpdated = nameof(OnDiscordGuildEmojisUpdated);        
        
        /// <code>
        /// void OnDiscordGuildStickersUpdated(GuildStickersUpdatedEvent stickers, Hash&lt;Snowflake, DiscordSticker&gt; previous, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildStickersUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildStickersUpdated = nameof(OnDiscordGuildStickersUpdated);
        
        /// <code>
        /// void OnDiscordGuildIntegrationsUpdated(GuildIntegrationsUpdatedEvent integration, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildIntegrationsUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildIntegrationsUpdated = nameof(OnDiscordGuildIntegrationsUpdated);
        
        /// <code>
        /// void OnDiscordGuildMemberAdded(GuildMemberAddedEvent member, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberAdded Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberAdded = nameof(OnDiscordGuildMemberAdded);
        
        /// <code>
        /// void OnDiscordGuildMemberRemoved(GuildMemberRemovedEvent member, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberRemoved Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberRemoved = nameof(OnDiscordGuildMemberRemoved);
        
        /// <code>
        /// void OnDiscordGuildMemberUpdated(GuildMemberUpdatedEvent member, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberUpdated = nameof(OnDiscordGuildMemberUpdated);
        
        /// <code>
        /// void OnDiscordGuildMemberNicknameUpdated(GuildMember member, string oldNickname, string newNickname, DateTime? lastNicknameUpdate, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberNicknameUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberNicknameUpdated = nameof(OnDiscordGuildMemberNicknameUpdated);
        
        /// <code>
        /// void OnDiscordGuildMemberAvatarUpdated(GuildMember member, string oldAvatar, string newAvatar, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberAvatarUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberAvatarUpdated = nameof(OnDiscordGuildMemberAvatarUpdated);
        
        /// <code>
        /// void OnDiscordGuildMemberDeafened(GuildMember member, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberDeafened Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberDeafened = nameof(OnDiscordGuildMemberDeafened);
        
        /// <code>
        /// void OnDiscordGuildMemberUndeafened(GuildMember member, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberUndeafened Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberUndeafened = nameof(OnDiscordGuildMemberUndeafened);
        
        /// <code>
        /// void OnDiscordGuildMemberMuted(GuildMember member, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberMuted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberMuted = nameof(OnDiscordGuildMemberMuted);
        
        /// <code>
        /// void OnDiscordGuildMemberUnmuted(GuildMember member, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberUnmuted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberUnmuted = nameof(OnDiscordGuildMemberUnmuted);
        
        /// <code>
        /// void OnDiscordGuildMemberTimeout(GuildMember member, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberTimeout Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberTimeout = nameof(OnDiscordGuildMemberTimeout);
        
        /// <code>
        /// void OnDiscordGuildMemberTimeoutEnded(GuildMember member, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberTimeoutEnded Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberTimeoutEnded = nameof(OnDiscordGuildMemberTimeoutEnded);
        
        /// <code>
        /// void OnDiscordGuildMemberBoosted(GuildMember member, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberBoosted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberBoosted = nameof(OnDiscordGuildMemberBoosted);
        
        /// <code>
        /// void OnDiscordGuildMemberBoostExtended(GuildMember member, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberBoostExtended Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberBoostExtended = nameof(OnDiscordGuildMemberBoostExtended);
        
        /// <code>
        /// void OnDiscordGuildMemberBoostEnded(GuildMember member, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberBoostEnded Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberBoostEnded = nameof(OnDiscordGuildMemberBoostEnded);
        
        /// <code>
        /// void OnDiscordGuildMemberRoleAdded(GuildMember member, Snowflake roleId, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberRoleAdded Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberRoleAdded = nameof(OnDiscordGuildMemberRoleAdded);
        
        /// <code>
        /// void OnDiscordGuildMemberRoleRemoved(GuildMember member, Snowflake roleId, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberRoleRemoved Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberRoleRemoved = nameof(OnDiscordGuildMemberRoleRemoved);
        
        /// <code>
        /// void OnDiscordGuildMembersLoaded(DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMembersLoaded Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMembersLoaded = nameof(OnDiscordGuildMembersLoaded);
        
        /// <code>
        /// void OnDiscordGuildMembersChunk(GuildMembersChunkEvent chunk, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMembersChunk Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMembersChunk = nameof(OnDiscordGuildMembersChunk);
        
        /// <code>
        /// void OnDiscordGuildRoleCreated(DiscordRole role, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildRoleCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildRoleCreated = nameof(OnDiscordGuildRoleCreated);
        
        /// <code>
        /// void OnDiscordGuildRoleUpdated(DiscordRole role, DiscordRole previous, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildRoleUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildRoleUpdated = nameof(OnDiscordGuildRoleUpdated);
        
        /// <code>
        /// void OnDiscordGuildRoleDeleted(DiscordRole role, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildRoleDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildRoleDeleted = nameof(OnDiscordGuildRoleDeleted);
        
        /// <code>
        /// void OnDiscordGuildScheduledEventCreated(GuildScheduledEvent guildEvent, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildScheduledEventCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildScheduledEventCreated = nameof(OnDiscordGuildScheduledEventCreated);
        
        /// <code>
        /// void OnDiscordGuildScheduledEventUpdated(GuildScheduledEvent guildEvent, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildScheduledEventUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildScheduledEventUpdated = nameof(OnDiscordGuildScheduledEventUpdated);
        
        /// <code>
        /// void OnDiscordGuildScheduledEventDeleted(GuildScheduledEvent guildEvent, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildScheduledEventDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildScheduledEventDeleted = nameof(OnDiscordGuildScheduledEventDeleted);
        
        /// <code>
        /// void OnDiscordGuildScheduledEventUserAdded(GuildScheduleEventUserAddedEvent added, GuildScheduledEvent, scheduledEvent, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildScheduledEventUserAdded Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildScheduledEventUserAdded = nameof(OnDiscordGuildScheduledEventUserAdded);
        
        /// <code>
        /// void OnDiscordGuildScheduledEventUserRemoved(GuildScheduleEventUserRemovedEvent removed, GuildScheduledEvent, scheduledEvent, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildScheduledEventUserRemoved Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildScheduledEventUserRemoved = nameof(OnDiscordGuildScheduledEventUserRemoved);

        /// <code>
        /// Note: Channel may be null if we haven't seen it yet
        /// void OnDiscordDirectMessageCreated(DiscordMessage message, DiscordChannel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessageCreated = nameof(OnDiscordDirectMessageCreated);
        
        /// <code>
        /// void OnDiscordGuildMessageCreated(DiscordMessage message, DiscordChannel channel, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMessageCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessageCreated = nameof(OnDiscordGuildMessageCreated);
        
        /// <code>
        /// void OnDiscordDirectMessageUpdated(DiscordMessage message, DiscordChannel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessageUpdated = nameof(OnDiscordDirectMessageUpdated);
        
        /// <code>
        /// void OnDiscordDirectMessageUpdated(DiscordMessage message, DiscordChannel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessageUpdated = nameof(OnDiscordGuildMessageUpdated);
        
        /// <code>
        /// void OnDiscordDirectMessageDeleted(DiscordMessage message, DiscordChannel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessageDeleted = nameof(OnDiscordDirectMessageDeleted);
        
        /// <code>
        /// void OnDiscordDirectMessageDeleted(DiscordMessage message, DiscordChannel channel, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordDirectMessageDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessageDeleted = nameof(OnDiscordGuildMessageDeleted);
        
        /// <code>
        /// void OnDiscordDirectMessagesBulkDeleted(List&lt;Snowflake&gt; messageIds, DiscordChannel channel)
        /// {
        ///     Puts("OnDiscordDirectMessagesBulkDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessagesBulkDeleted = nameof(OnDiscordDirectMessagesBulkDeleted);
        
        /// <code>
        /// void OnDiscordGuildMessagesBulkDeleted(List&lt;Snowflake&gt; messageIds, DiscordChannel channel, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMessagesBulkDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessagesBulkDeleted = nameof(OnDiscordDirectMessagesBulkDeleted);
        
        /// <code>
        /// void OnDiscordDirectMessageReactionAdded(MessageReactionAddedEvent reaction, DiscordChannel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageReactionAdded Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessageReactionAdded = nameof(OnDiscordDirectMessageReactionAdded);
        
        /// <code>
        /// void OnDiscordGuildMessageReactionAdded(MessageReactionAddedEvent reaction, DiscordChannel channel, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMessageReactionAdded Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessageReactionAdded = nameof(OnDiscordGuildMessageReactionAdded);
        
        /// <code>
        /// void OnDiscordDirectMessageReactionRemoved(MessageReactionRemovedEvent reaction, DiscordChannel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageReactionRemoved Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessageReactionRemoved = nameof(OnDiscordDirectMessageReactionRemoved);
        
        /// <code>
        /// void OnDiscordGuildMessageReactionRemoved(MessageReactionRemovedEvent reaction, DiscordChannel channel, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMessageReactionRemoved Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessageReactionRemoved = nameof(OnDiscordGuildMessageReactionRemoved);
        
        /// <code>
        /// void OnDiscordDirectMessageReactionRemovedAll(MessageReactionRemovedAllEmojiEvent reaction, DiscordChannel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageReactionRemovedAll Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessageReactionRemovedAll = nameof(OnDiscordDirectMessageReactionRemoved);
        
        /// <code>
        /// void OnDiscordGuildMessageReactionRemovedAll(MessageReactionRemovedAllEmojiEvent reaction, DiscordChannel channel, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMessageReactionRemovedAll Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessageReactionRemovedAll = nameof(OnDiscordGuildMessageReactionRemoved);
        
        /// <code>
        /// void OnDiscordDirectMessageReactionEmojiRemoved(MessageReactionRemovedAllEmojiEvent reaction, DiscordChannel channel)
        /// {
        ///     Puts("OnDiscordDirectMessageReactionEmojiRemoved Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectMessageReactionEmojiRemoved = nameof(OnDiscordDirectMessageReactionEmojiRemoved);
        
        /// <code>
        /// void OnDiscordGuildMessageReactionEmojiRemoved(MessageReactionRemovedAllEmojiEvent reaction, DiscordChannel channel, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMessageReactionEmojiRemoved Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMessageReactionEmojiRemoved = nameof(OnDiscordGuildMessageReactionEmojiRemoved);
        
        /// <code>
        /// void OnDiscordGuildMemberPresenceUpdated(PresenceUpdatedEvent update, GuildMember member, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildMemberPresenceUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildMemberPresenceUpdated = nameof(OnDiscordGuildMemberPresenceUpdated);
        
        /// <code>
        /// void OnDiscordDirectTypingStarted(TypingStartedEvent typing, DiscordChannel channel)
        /// {
        ///     Puts("OnDiscordDirectTypingStarted Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectTypingStarted = nameof(OnDiscordDirectTypingStarted);
        
        /// <code>
        /// void OnDiscordGuildTypingStarted(TypingStartedEvent typing, DiscordChannel channel, DiscordGuild guild)
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
        /// void OnDiscordDirectVoiceStateUpdated(VoiceState voice, DiscordChannel channel)
        /// {
        ///     Puts("OnDiscordDirectVoiceStateUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectVoiceStateUpdated = nameof(OnDiscordDirectVoiceStateUpdated);
        
        /// <code>
        /// void OnDiscordGuildVoiceStateUpdated(VoiceState voice, DiscordChannel channel, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildVoiceStateUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildVoiceStateUpdated = nameof(OnDiscordGuildVoiceStateUpdated);
        
        /// <code>
        /// void OnDiscordGuildVoiceServerUpdated(VoiceServerUpdatedEvent voice, DiscordChannel channel, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildVoiceServerUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildVoiceServerUpdated = nameof(OnDiscordGuildVoiceServerUpdated);
        
        /// <code>
        /// void OnDiscordGuildWebhookUpdated(WebhooksUpdatedEvent webhook, DiscordChannel channel, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildWebhookUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildWebhookUpdated = nameof(OnDiscordGuildWebhookUpdated);
        
        /// <code>
        /// void OnDiscordDirectInviteCreated(InviteCreatedEvent invite, DiscordChannel channel)
        /// {
        ///     Puts("OnDiscordDirectInviteCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectInviteCreated = nameof(OnDiscordDirectInviteCreated);
        
        /// <code>
        /// void OnDiscordGuildInviteCreated(InviteCreatedEvent invite, DiscordChannel channel, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildInviteCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildInviteCreated = nameof(OnDiscordGuildInviteCreated);
        
        /// <code>
        /// void OnDiscordDirectInviteDeleted(InviteCreatedEvent invite, DiscordChannel channel)
        /// {
        ///     Puts("OnDiscordDirectInviteDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordDirectInviteDeleted = nameof(OnDiscordDirectInviteDeleted);
        
        /// <code>
        /// void OnDiscordGuildInviteDeleted(InviteCreatedEvent invite, DiscordChannel channel, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildInviteDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildInviteDeleted = nameof(OnDiscordGuildInviteDeleted);
        
        /// <code>
        /// void OnDiscordApplicationCommandPermissionsUpdated(CommandPermissions permissions)
        /// {
        ///     Puts("OnDiscordInteractionCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordApplicationCommandPermissionsUpdated = nameof(OnDiscordApplicationCommandPermissionsUpdated);   
        
        /// <code>
        /// void OnDiscordInteractionCreated(DiscordInteraction interaction)
        /// {
        ///     Puts("OnDiscordInteractionCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordInteractionCreated = nameof(OnDiscordInteractionCreated);
        
        /// <code>
        /// void OnDiscordGuildIntegrationCreated(IntegrationCreatedEvent integration, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildIntegrationCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildIntegrationCreated = nameof(OnDiscordGuildIntegrationCreated);
        
        /// <code>
        /// void OnDiscordGuildIntegrationUpdated(IntegrationUpdatedEvent interaction, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildIntegrationUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildIntegrationUpdated = nameof(OnDiscordGuildIntegrationUpdated);
        
        /// <code>
        /// void OnDiscordGuildIntegrationDeleted(IntegrationDeletedEvent interaction, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildIntegrationDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildIntegrationDeleted = nameof(OnDiscordGuildIntegrationDeleted);

        /// <code>
        /// void OnDiscordGuildThreadCreated(DiscordChannel thread, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildThreadCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildThreadCreated = nameof(OnDiscordGuildThreadCreated);
        
        /// <code>
        /// void OnDiscordGuildThreadUpdated(DiscordChannel thread, DiscordChannel previous, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildThreadUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildThreadUpdated = nameof(OnDiscordGuildThreadUpdated);

        /// <code>
        /// void OnDiscordGuildThreadDeleted(DiscordChannel thread, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildThreadDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildThreadDeleted = nameof(OnDiscordGuildThreadDeleted);
        
        /// <code>
        /// void OnDiscordGuildThreadListSynced(ThreadListSyncEvent sync, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildThreadListSynced Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildThreadListSynced = nameof(OnDiscordGuildThreadListSynced);
        
        /// <code>
        /// void OnDiscordGuildThreadMemberUpdated(ThreadMember member, DiscordChannel thread, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildThreadMemberUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildThreadMemberUpdated = nameof(OnDiscordGuildThreadMemberUpdated);
        
        /// <code>
        /// void OnDiscordGuildThreadMembersUpdated(ThreadMembersUpdatedEvent members, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordGuildThreadMembersUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordGuildThreadMembersUpdated = nameof(OnDiscordGuildThreadMembersUpdated);

        /// <code>
        /// void OnDiscordStageInstanceCreated(StageInstance stage, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordStageInstanceCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordStageInstanceCreated = nameof(OnDiscordStageInstanceCreated);
        
        /// <code>
        /// void OnDiscordStageInstanceUpdated(StageInstance stage, StageInstance previous, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordStageInstanceUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordStageInstanceUpdated = nameof(OnDiscordStageInstanceUpdated);
        
        /// <code>
        /// void OnDiscordStageInstanceDeleted(StageInstance stage, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordStageInstanceDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordStageInstanceDeleted = nameof(OnDiscordStageInstanceDeleted);
        
        /// <code>
        /// void OnDiscordAutoModRuleCreated(AutoModRule rule, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordAutoModRuleCreated Works!");
        /// }
        /// </code>
        public const string OnDiscordAutoModRuleCreated = nameof(OnDiscordAutoModRuleCreated);
        
        /// <code>
        /// void OnDiscordAutoModRuleUpdated(AutoModRule rule, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordAutoModRuleUpdated Works!");
        /// }
        /// </code>
        public const string OnDiscordAutoModRuleUpdated = nameof(OnDiscordAutoModRuleUpdated);
        
        /// <code>
        /// void OnDiscordAutoModRuleDeleted(AutoModRule rule, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordAutoModRuleDeleted Works!");
        /// }
        /// </code>
        public const string OnDiscordAutoModRuleDeleted = nameof(OnDiscordAutoModRuleDeleted);
        
        /// <code>
        /// void OnDiscordAutoModActionExecuted(AutoModActionExecutionEvent rule, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordAutoModActionExecuted Works!");
        /// }
        /// </code>
        public const string OnDiscordAutoModActionExecuted = nameof(OnDiscordAutoModActionExecuted);
        
        /// <code>
        /// void OnDiscordPollVoteAdded(MessagePollVoteAddedEvent vote, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordPollVoteAdded Works!");
        /// }
        /// </code>
        public const string OnDiscordPollVoteAdded = nameof(OnDiscordPollVoteAdded);
        
        /// <code>
        /// void OnDiscordAutoModActionExecuted(MessagePollVoteRemovedEvent vote, DiscordGuild guild)
        /// {
        ///     Puts("OnDiscordPollVoteRemoved Works!");
        /// }
        /// </code>
        public const string OnDiscordPollVoteRemoved = nameof(OnDiscordPollVoteRemoved);
        
        /// <code>
        /// void OnDiscordUnhandledCommand(EventPayload payload)
        /// {
        ///     Puts("OnDiscordUnhandledCommand Works!");
        /// }
        /// </code>
        public const string OnDiscordUnhandledCommand = nameof(OnDiscordUnhandledCommand);   
        #endregion
    }
}