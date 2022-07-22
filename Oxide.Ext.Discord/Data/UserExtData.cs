using System;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Plugins;
using ProtoBuf;

namespace Oxide.Ext.Discord.Data
{
    [ProtoContract]
    internal class UserExtData
    {
        [ProtoMember(1)]
        public Snowflake UserId { get; set; }
        
        [ProtoMember(2)]
        public Snowflake DmChannelId { get; set; }
        
        [ProtoMember(3)]
        public DateTime? DmBlockedDate { get; set; }

        public UserExtData(Snowflake userId)
        {
            UserId = userId;
        }

        public DiscordChannel CreateDmChannel()
        {
            return new DiscordChannel
            {
                Id = DmChannelId,
                Type = ChannelType.Dm,
                Recipients = new Hash<Snowflake, DiscordUser>
                {
                    [UserId] = GetUser()
                }
            };
        }

        public void SetDmBlock()
        {
            DmBlockedDate = DateTime.UtcNow;
            DataHandler.OnDataChanged();
        }

        public void ClearBlockIfExpired()
        {
            if (DmBlockedDate.HasValue && DmBlockedDate.Value < DateTime.UtcNow)
            {
                DmBlockedDate = null;
                DataHandler.OnDataChanged();
            }
        }

        public DateTime? GetBlockedUntil()
        {
            return DmBlockedDate.HasValue ? DmBlockedDate.Value + TimeSpan.FromHours(DiscordExtension.DiscordConfig.Users.DmBlockedDuration) : (DateTime?)null;
        }
        
        public bool IsDmBlocked()
        {
            return DmBlockedDate.HasValue && GetBlockedUntil() > DateTime.UtcNow;
        }

        public DiscordUser GetUser()
        {
            return DiscordUserCache.GetOrCreate(UserId);
        }
    }
}