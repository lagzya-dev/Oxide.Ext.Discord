using System.Runtime.Serialization;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Helpers.Converters;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#guild-object-guild-features">Guild Features</a>
    /// </summary>
    [JsonConverter(typeof(DiscordEnumConverter<GuildFeatures>), Unknown)]
    public enum GuildFeatures
    {
        /// <summary>
        /// Discord Extension doesn't currently support a guild features
        /// </summary>
        Unknown,
        
        /// <summary>
        /// Guild has access to set an invite splash background
        /// </summary>
        [EnumMember(Value = "INVITE_SPLASH")] 
        InviteSplash,
        
        /// <summary>
        /// Guild has access to set 384kbps bitrate in voice (previously VIP voice servers)
        /// </summary>
        [EnumMember(Value = "VIP_REGIONS")] 
        VipRegions,
        
        /// <summary>
        /// Guild has access to set a vanity URL
        /// </summary>
        [EnumMember(Value = "VANITY_URL")] 
        VanityUrl,
        
        /// <summary>
        /// Guild is verified
        /// </summary>
        [EnumMember(Value = "VERIFIED")] 
        Verified,
        
        /// <summary>
        /// Guild is partnered
        /// </summary>
        [EnumMember(Value = "PARTNERED")] 
        Partnered,
        
        /// <summary>
        /// Guild can enable welcome screen and discovery, and receives community updates
        /// </summary>
        [EnumMember(Value = "COMMUNITY")] 
        Community,
        
        /// <summary>
        /// Guild has access to use commerce features (i.e. create store channels)
        /// </summary>
        [EnumMember(Value = "COMMERCE")] 
        Commerce,
        
        /// <summary>
        /// Guild has access to create news channels
        /// </summary>
        [EnumMember(Value = "NEWS")] 
        News,
        
        /// <summary>
        /// Guild is lurkable and able to be discovered in the directory
        /// </summary>
        [EnumMember(Value = "DISCOVERABLE")] 
        Discoverable,
        
        /// <summary>
        /// Guild is able to be featured in the directory
        /// </summary>
        [EnumMember(Value = "FEATURABLE")] 
        Featurable,
        
        /// <summary>
        /// Guild has access to set an animated guild icon
        /// </summary>
        [EnumMember(Value = "ANIMATED_ICON")] 
        AnimatedIcon,
        
        /// <summary>
        /// Guild has access to set a guild banner image
        /// </summary>
        [EnumMember(Value = "BANNER")] 
        Banner,
        
        /// <summary>
        /// Guild has enabled the welcome screen
        /// </summary>
        [EnumMember(Value = "WELCOME_SCREEN_ENABLED")] 
        WelcomeScreenEnabled,
        
        /// <summary>
        /// Guild has enabled ticketed events
        /// </summary>
        [EnumMember(Value = "TICKETED_EVENTS_ENABLED")] 
        TicketedEventsEnabled,
        
        /// <summary>
        /// Guild has enabled monetization
        /// </summary>
        [EnumMember(Value = "MONETIZATION_ENABLED")] 
        MonetizationEnabled,
        
        /// <summary>
        /// Guild has increased custom sticker slots
        /// </summary>
        [EnumMember(Value = "MORE_STICKERS")] 
        MoreStickers,
        
        /// <summary>
        /// Guild has access to the three day archive time for threads
        /// </summary>
        [EnumMember(Value = "THREE_DAY_THREAD_ARCHIVE")] 
        ThreeDayThreadArchive,
        
        /// <summary>
        /// guild has access to the seven day archive time for threads
        /// </summary>
        [EnumMember(Value = "SEVEN_DAY_THREAD_ARCHIVE")] 
        SevenDayThreadArchive,
        
        /// <summary>
        /// 	guild has access to create private threads
        /// </summary>
        [EnumMember(Value = "SEVEN_DAY_THREAD_ARCHIVE")] 
        PrivateThreads,
    }
}