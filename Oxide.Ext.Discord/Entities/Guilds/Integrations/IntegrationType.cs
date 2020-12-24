using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities.Guilds.Integrations
{
    public enum IntegrationType
    {
        [Description("twitch")] Twitch,
        [Description("youtube")] Youtube,
        [Description("discord")] Discord,
    }
}