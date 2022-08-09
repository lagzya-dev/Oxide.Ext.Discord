using Oxide.Core.Libraries.Covalence;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders
{
    public class PlaceholderData
    {
        private readonly Hash<string, object> _objects = new Hash<string, object>();
        internal const string TimestampName = "Timestamp";
        internal bool ShouldPool = true;

        internal PlaceholderData() { }

        internal PlaceholderData AddServer(IServer server)
        {
            if (server != null)
            {
                _objects[nameof(IServer)] = server;
            }
            
            return this;
        }
        
        public PlaceholderData AddGuild(DiscordClient client, Snowflake? guildId)
        {
            return AddGuild(client.Bot?.GetGuild(guildId));
        }
        
        public PlaceholderData AddGuild(DiscordGuild guild)
        {
            if (guild != null)
            {
                _objects[nameof(DiscordGuild)] = guild;
            }
            
            return this;
        }

        public PlaceholderData AddGuild(DiscordClient client, Snowflake guildId, Snowflake memberId)
        {
            return AddGuildMember(client.Bot?.GetGuild(guildId).Members[memberId]);
        } 
        
        public PlaceholderData AddGuildMember(GuildMember member)
        {
            if (member != null)
            {
                _objects[nameof(GuildMember)] = member;
                AddDiscordUser(member.User);
            }
            
            return this;
        }

        public PlaceholderData AddDiscordUser(DiscordUser user)
        {
            if (user != null)
            {
                _objects[nameof(DiscordUser)] = user;
                AddPlayer(user.Player);
            }
            
            return this;
        }

        public PlaceholderData AddDiscordRole(DiscordClient client, Snowflake guildId, Snowflake roleId)
        {
            return AddDiscordRole(client.Bot?.GetGuild(guildId)?.Roles[roleId]);
        }
        
        public PlaceholderData AddDiscordRole(DiscordRole role)
        {
            if (role != null)
            {
                _objects[nameof(DiscordRole)] = role;
            }
            
            return this;
        }
        
        public PlaceholderData AddDiscordChannel(DiscordClient client, Snowflake channelId, Snowflake? guildId = null)
        {
            return AddDiscordChannel(client.Bot?.GetChannel(channelId, guildId));
        }

        public PlaceholderData AddDiscordChannel(DiscordChannel channel)
        {
            if (channel != null)
            {
                _objects[nameof(DiscordChannel)] = channel;
            }
            
            return this;
        }
        
        public PlaceholderData AddPlayer(IPlayer player)
        {
            if (player != null)
            {
                _objects[nameof(IPlayer)] = player;
            }
            
            return this;
        }

        public PlaceholderData AddTimestamp(ulong timestamp)
        {
            _objects[TimestampName] = timestamp;
            return this;
        }
        
        public PlaceholderData Add<T>(T obj)
        {
            _objects[typeof(T).Name] = obj;
            return this;
        }

        public PlaceholderData Add<T>(string name, T obj)
        {
            _objects[name] = obj;
            return this;
        }

        public T Get<T>(string name)
        {
            if (_objects.TryGetValue(name, out object obj))
            {
                return (T)obj;
            }

            return default(T);
        }

        public void DisablePooling()
        {
            ShouldPool = false;
        }
    }
}