using Oxide.Ext.Discord.Entities;

namespace Oxide.Ext.Discord.Interfaces
{
    public interface IDiscordCacheable
    {
        Snowflake Id { get; set; }
    }
}