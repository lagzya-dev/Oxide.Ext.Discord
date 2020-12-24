using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities.Messages.AllowedMentions
{
    public enum AllowedMentionTypes
    {
        [Description("roles")] Roles,
        [Description("users")] Users,
        [Description("everyone")] Everyone,
    }
}