using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oxide.Ext.Discord.Entities.AutoMod;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Channels.Stages;
using Oxide.Ext.Discord.Entities.Gateway;
using Oxide.Ext.Discord.Entities.Gateway.Events;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Entities.Voice;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.WebSockets.Handlers;

namespace Oxide.Ext.Discord.Json.Converters
{
    /// <summary>
    /// JSON converter for <see cref="EventPayload"/>
    /// </summary>
    public class EventPayloadConverter : JsonConverter
    {
        /// <summary>
        /// We do not write with this converter
        /// </summary>
        public override bool CanWrite => false;

        private const string EventCode = "op";
        private const string Sequence = "s";
        private const string DiscordCode = "t";
        private const string Data = "d";

        /// <summary>
        /// We do nto write with this converter
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the JSON into a pooled <see cref="EventPayload"/>
        /// Populates the Data field with the correct type during deserialization
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            EventPayload payload = DiscordPool.Internal.Get<EventPayload>();
            payload.OpCode = obj[EventCode].ToObject<GatewayEventCode>();
            payload.Sequence = obj[Sequence]?.ToObject<int?>();

            switch (payload.OpCode)
            {
                case GatewayEventCode.Dispatch:
                    payload.DispatchCode = obj[DiscordCode].ToObject<DiscordDispatchCode>();
                    payload.Data = HandleDispatch(payload, obj);
                    break;
                case GatewayEventCode.InvalidSession:
                    payload.Data = obj[Data]?.ToObject<bool?>() ?? false;
                    break;
                case GatewayEventCode.Hello:
                    payload.Data = obj[Data].ToObject<GatewayHelloEvent>();
                    break;
            }

            return payload;
        }

        private object HandleDispatch(EventPayload payload, JObject obj)
        {
            switch (payload.DispatchCode)
            {
                case DiscordDispatchCode.Ready:
                    return GetData<GatewayReadyEvent>(obj);
                
                case DiscordDispatchCode.Resumed:
                    return GetData<GatewayResumedEvent>(obj);
                
                case DiscordDispatchCode.ChannelCreated:
                case DiscordDispatchCode.ChannelUpdated:
                case DiscordDispatchCode.ChannelDeleted:
                case DiscordDispatchCode.ThreadCreated:
                case DiscordDispatchCode.ThreadUpdated:
                case DiscordDispatchCode.ThreadDeleted:
                    return GetData<DiscordChannel>(obj);
                
                case DiscordDispatchCode.ChannelPinsUpdate:
                    return GetData<ChannelPinsUpdatedEvent>(obj);
                
                case DiscordDispatchCode.GuildCreated:
                case DiscordDispatchCode.GuildUpdated:
                case DiscordDispatchCode.GuildDeleted:
                    return GetData<DiscordGuild>(obj);
                
                case DiscordDispatchCode.GuildBanAdded:
                case DiscordDispatchCode.GuildBanRemoved:
                    return GetData<GuildMemberBannedEvent>(obj);
                
                case DiscordDispatchCode.GuildEmojisUpdated:
                    return GetData<GuildEmojisUpdatedEvent>(obj);
                
                case DiscordDispatchCode.GuildStickersUpdate:
                    return GetData<GuildStickersUpdatedEvent>(obj);
                
                case DiscordDispatchCode.GuildIntegrationsUpdated:
                    return GetData<GuildIntegrationsUpdatedEvent>(obj);
                
                case DiscordDispatchCode.GuildMemberAdded:
                    return GetData<GuildMemberAddedEvent>(obj);
                
                case DiscordDispatchCode.GuildMemberRemoved:
                    return GetData<GuildMemberRemovedEvent>(obj);
                
                case DiscordDispatchCode.GuildMemberUpdated:
                    return GetData<GuildMemberUpdatedEvent>(obj);
                
                case DiscordDispatchCode.GuildMembersChunk:
                    return GetData<GuildMembersChunkEvent>(obj);
                
                case DiscordDispatchCode.GuildRoleCreated:
                    return GetData<GuildRoleCreatedEvent>(obj);
                
                case DiscordDispatchCode.GuildRoleUpdated:
                    return GetData<GuildRoleUpdatedEvent>(obj);
                
                case DiscordDispatchCode.GuildRoleDeleted:
                    return GetData<GuildRoleDeletedEvent>(obj);
                
                case DiscordDispatchCode.GuildScheduledEventCreate:
                case DiscordDispatchCode.GuildScheduledEventUpdate:
                case DiscordDispatchCode.GuildScheduledEventDelete:
                    return GetData<GuildScheduledEvent>(obj);
                
                case DiscordDispatchCode.GuildScheduledEventUserAdd:
                    return GetData<GuildScheduleEventUserAddedEvent>(obj);
                
                case DiscordDispatchCode.GuildScheduledEventUserRemove:
                    return GetData<GuildScheduleEventUserRemovedEvent>(obj);
                
                case DiscordDispatchCode.IntegrationCreated:
                    return GetData<IntegrationCreatedEvent>(obj);
                
                case DiscordDispatchCode.IntegrationUpdated:
                    return GetData<IntegrationUpdatedEvent>(obj);
                
                case DiscordDispatchCode.IntegrationDeleted:
                    return GetData<IntegrationDeletedEvent>(obj);
                
                case DiscordDispatchCode.MessageCreated:
                case DiscordDispatchCode.MessageUpdated:
                    return GetData<DiscordMessage>(obj);
                
                case DiscordDispatchCode.MessageDeleted:
                    return GetData<MessageDeletedEvent>(obj);
                
                case DiscordDispatchCode.MessageBulkDeleted:
                    return GetData<MessageBulkDeletedEvent>(obj);
                
                case DiscordDispatchCode.MessageReactionAdded:
                    return GetData<MessageReactionAddedEvent>(obj);
                
                case DiscordDispatchCode.MessageReactionRemoved:
                    return GetData<MessageReactionRemovedEvent>(obj);
                
                case DiscordDispatchCode.MessageReactionAllRemoved:
                    return GetData<MessageReactionRemovedAllEvent>(obj);
                
                case DiscordDispatchCode.MessageReactionEmojiRemoved:
                    return GetData<MessageReactionRemovedAllEmojiEvent>(obj);
                
                case DiscordDispatchCode.PresenceUpdated:
                    return GetData<PresenceUpdatedEvent>(obj);

                case DiscordDispatchCode.TypingStarted:
                    return GetData<TypingStartedEvent>(obj);
                
                case DiscordDispatchCode.UserUpdated:
                    return GetData<DiscordUser>(obj);
                
                case DiscordDispatchCode.VoiceStateUpdated:
                    return GetData<VoiceState>(obj);
                
                case DiscordDispatchCode.VoiceServerUpdated:
                    return GetData<VoiceServerUpdatedEvent>(obj);
                
                case DiscordDispatchCode.WebhooksUpdated:
                    return GetData<WebhooksUpdatedEvent>(obj);
                
                case DiscordDispatchCode.InviteCreated:
                    return GetData<InviteCreatedEvent>(obj);
                
                case DiscordDispatchCode.InviteDeleted:
                    return GetData<InviteDeletedEvent>(obj);
                
                case DiscordDispatchCode.ApplicationCommandsPermissionsUpdate:
                    return GetData<CommandPermissions>(obj);
                
                case DiscordDispatchCode.InteractionCreated:
                    return GetData<DiscordInteraction>(obj);
                
                case DiscordDispatchCode.ThreadListSync:
                    return GetData<ThreadListSyncEvent>(obj);
                
                case DiscordDispatchCode.ThreadMemberUpdated:
                    return GetData<ThreadMemberUpdateEvent>(obj);
                
                case DiscordDispatchCode.ThreadMembersUpdated:
                    return GetData<ThreadMembersUpdatedEvent>(obj);
                
                case DiscordDispatchCode.StageInstanceCreated:
                case DiscordDispatchCode.StageInstanceUpdated:
                case DiscordDispatchCode.StageInstanceDeleted:
                    return GetData<StageInstance>(obj);
                
                case DiscordDispatchCode.AutoModerationRuleCreate:
                case DiscordDispatchCode.AutoModerationRuleUpdate:
                case DiscordDispatchCode.AutoModerationRuleDelete:
                    return GetData<AutoModRule>(obj);
                
                case DiscordDispatchCode.AutoModerationActionExecution:
                    return GetData<AutoModActionExecutionEvent>(obj);
                
                case DiscordDispatchCode.PresenceReplace:
                case DiscordDispatchCode.Unknown:
                default:
                    return obj["d"];
            }
        }

        private static object GetData<T>(JObject obj) where T : class
        {
            return obj[Data].ToObject<T>();
        }

        /// <summary>
        /// Returns if this converter can convert the given type
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(EventPayload) == objectType;
        }
    }
}