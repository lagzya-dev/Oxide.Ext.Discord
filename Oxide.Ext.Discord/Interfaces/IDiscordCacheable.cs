using Oxide.Ext.Discord.Entities;

namespace Oxide.Ext.Discord.Interfaces
{
    public interface IDiscordCacheable<T>
    {
        Snowflake Id { get; set; }

        void Update(T update);
    }
}