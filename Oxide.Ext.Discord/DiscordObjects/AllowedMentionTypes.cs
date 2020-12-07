using System.ComponentModel;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public enum AllowedMentionTypes
    {
        [Description("roles")] Roles,
        [Description("users")] Users,
        [Description("everyone")] Everyone,
    }
}