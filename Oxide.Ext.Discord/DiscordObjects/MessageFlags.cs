using System;

namespace Oxide.Ext.Discord.DiscordObjects
{
    [Flags]
    public enum MessageFlags
    {
        CrossPosted = 1 << 0,
        IsCrossPost = 1 << 1,
        SuppressEmbeds = 1 << 2,
        SourceMessageDeleted = 1 << 3,
        Urgent = 1 << 4
    }
}