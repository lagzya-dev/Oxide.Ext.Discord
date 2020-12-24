using System;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    [Flags]
    public enum SystemChannelFlags
    {
        SuppressJoinNotifications = 1 << 0,
        SuppressPremiumSubscriptions = 1 << 1
    }
}