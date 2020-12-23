using System.ComponentModel;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public enum PresenceStatus
    {
        [Description("online")] Online,
        [Description("dnd")] DND,
        [Description("idle")] Idle,
        [Description("invisible")] Invisible,
        [Description("offline")] Offline
    }
}