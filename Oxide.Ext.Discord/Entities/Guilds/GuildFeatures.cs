using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GuildFeatures
    {
        [Description("INVITE_SPLASH")] InviteSplash,
        [Description("VIP_REGIONS")] VipRegions,
        [Description("VANITY_URL")] VanityUrl,
        [Description("VERIFIED")] Verified,
        [Description("PARTNERED")] Partnered,
        [Description("COMMUNITY")] Community,
        [Description("COMMERCE")] Commerce,
        [Description("NEWS")] News,
        [Description("DISCOVERABLE")] Discoverable,
        [Description("FEATURABLE")] Featurable,
        [Description("ANIMATED_ICON")] AnimatedIcon,
        [Description("BANNER")] Banner,
        [Description("WELCOME_SCREEN_ENABLED")] WelcomeScreenEnabled,
    }
}