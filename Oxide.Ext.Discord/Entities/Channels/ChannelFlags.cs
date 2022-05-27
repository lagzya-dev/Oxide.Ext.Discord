using System;

namespace Oxide.Ext.Discord.Entities.Channels
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/channel#channel-object-channel-flags">Channel Flags</a>
    /// </summary>
    [Flags]
    public enum ChannelFlags
    {
        /// <summary>
        /// This thread is pinned to the top of its parent forum channel
        /// </summary>
        Pinned = 1 << 1
    }
}