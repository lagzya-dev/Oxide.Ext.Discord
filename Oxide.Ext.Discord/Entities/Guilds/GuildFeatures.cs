using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#guild-object-guild-features">Guild Features</a>
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GuildFeatures
    {
        /// <summary>
        /// Guild has access to set an invite splash background
        /// </summary>
        [Description("INVITE_SPLASH")] 
        InviteSplash,
        
        /// <summary>
        /// Guild has access to set 384kbps bitrate in voice (previously VIP voice servers)
        /// </summary>
        [Description("VIP_REGIONS")] 
        VipRegions,
        
        /// <summary>
        /// Guild has access to set a vanity URL
        /// </summary>
        [Description("VANITY_URL")] 
        VanityUrl,
        
        /// <summary>
        /// Guild is verified
        /// </summary>
        [Description("VERIFIED")] 
        Verified,
        
        /// <summary>
        /// Guild is partnered
        /// </summary>
        [Description("PARTNERED")] 
        Partnered,
        
        /// <summary>
        /// Guild can enable welcome screen and discovery, and receives community updates
        /// </summary>
        [Description("COMMUNITY")] 
        Community,
        
        /// <summary>
        /// Guild has access to use commerce features (i.e. create store channels)
        /// </summary>
        [Description("COMMERCE")] 
        Commerce,
        
        /// <summary>
        /// Guild has access to create news channels
        /// </summary>
        [Description("NEWS")] 
        News,
        
        /// <summary>
        /// Guild is lurkable and able to be discovered in the directory
        /// </summary>
        [Description("DISCOVERABLE")] 
        Discoverable,
        
        /// <summary>
        /// Guild is able to be featured in the directory
        /// </summary>
        [Description("FEATURABLE")] 
        Featurable,
        
        /// <summary>
        /// Guild has access to set an animated guild icon
        /// </summary>
        [Description("ANIMATED_ICON")] 
        AnimatedIcon,
        
        /// <summary>
        /// Guild has access to set a guild banner image
        /// </summary>
        [Description("BANNER")] 
        Banner,
        
        /// <summary>
        /// Guild has enabled the welcome screen
        /// </summary>
        [Description("WELCOME_SCREEN_ENABLED")] 
        WelcomeScreenEnabled,
    }
}