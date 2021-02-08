namespace Oxide.Ext.Discord.Entities.Channels
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/channel#channel-object-channel-types">Types of Channels</a>
    /// </summary>
    public enum ChannelType
    {
        /// <summary>
        /// A text channel within a server
        /// </summary>
        GuildText = 0,
        
        /// <summary>
        /// A direct message between users
        /// </summary>
        Dm = 1,
        
        /// <summary>
        /// A voice channel within a server
        /// </summary>
        GuildVoice = 2,
        
        /// <summary>
        /// A direct message between multiple users
        /// </summary>
        GroupDm = 3,
        
        /// <summary>
        /// An organizational category that contains up to 50 channels
        /// </summary>
        GuildCategory = 4,
        
        /// <summary>
        /// A channel that users can follow and crosspost into their own server
        /// </summary>
        GuildNews = 5,
        
        /// <summary>
        /// A channel in which game developers can sell their game on Discord
        /// </summary>
        GuildStore = 6
    }
}
