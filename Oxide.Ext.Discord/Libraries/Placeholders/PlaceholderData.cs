using Oxide.Core.Libraries.Covalence;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders
{
    /// <summary>
    /// Placeholder Data for placeholders
    /// </summary>
    public class PlaceholderData
    {
        private readonly Hash<string, object> _objects = new Hash<string, object>();
        internal const string TimestampName = "Timestamp";
        internal bool ShouldPool = true;

        internal PlaceholderData() { }

        internal void AddServer(IServer server)
        {
            if (server != null)
            {
                _objects[nameof(IServer)] = server;
            }
        }
        
        /// <summary>
        /// Add a <see cref="DiscordGuild"/> by <see cref="DiscordClient"/> and GuildId
        /// </summary>
        /// <param name="client">Discord Client for the guild</param>
        /// <param name="guildId">Guild ID of the guild</param>
        /// <returns>This</returns>
        public PlaceholderData AddGuild(DiscordClient client, Snowflake? guildId)
        {
            return AddGuild(client.Bot?.GetGuild(guildId));
        }
        
        /// <summary>
        /// Add a <see cref="DiscordGuild"/>
        /// </summary>
        /// <param name="guild">Guild to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddGuild(DiscordGuild guild)
        {
            if (guild != null)
            {
                _objects[nameof(DiscordGuild)] = guild;
            }
            
            return this;
        }

        /// <summary>
        /// Add a <see cref="GuildMember"/> by <see cref="DiscordClient"/>, GuildId, and UserId
        /// </summary>
        /// <param name="client">DiscordClient for the guild</param>
        /// <param name="guildId">Guild ID for the guild</param>
        /// <param name="memberId">Member UserId in the guild</param>
        /// <returns>This</returns>
        public PlaceholderData AddGuildMember(DiscordClient client, Snowflake guildId, Snowflake memberId)
        {
            return AddGuildMember(client.Bot?.GetGuild(guildId).Members[memberId]);
        } 
        
        /// <summary>
        /// Add a <see cref="GuildMember"/>
        /// </summary>
        /// <param name="member">Member to add</param>
        /// <returns></returns>
        public PlaceholderData AddGuildMember(GuildMember member)
        {
            if (member != null)
            {
                _objects[nameof(GuildMember)] = member;
                AddDiscordUser(member.User);
            }
            
            return this;
        }

        /// <summary>
        /// Adds a <see cref="DiscordUser"/>
        /// </summary>
        /// <param name="user">User to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddDiscordUser(DiscordUser user)
        {
            if (user != null)
            {
                _objects[nameof(DiscordUser)] = user;
                AddPlayer(user.Player);
            }
            
            return this;
        }

        /// <summary>
        /// Adds a <see cref="DiscordRole"/> by <see cref="DiscordClient"/>, GuildId, and RoleId
        /// </summary>
        /// <param name="client">Client for the guild</param>
        /// <param name="guildId">Guild ID of the guild</param>
        /// <param name="roleId">Role ID of the role</param>
        /// <returns>This</returns>
        public PlaceholderData AddDiscordRole(DiscordClient client, Snowflake guildId, Snowflake roleId)
        {
            return AddDiscordRole(client.Bot?.GetGuild(guildId)?.Roles[roleId]);
        }
        
        /// <summary>
        /// Adds a <see cref="DiscordRole"/>
        /// </summary>
        /// <param name="role">Role to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddDiscordRole(DiscordRole role)
        {
            if (role != null)
            {
                _objects[nameof(DiscordRole)] = role;
            }
            
            return this;
        }
        
        /// <summary>
        /// Adds a <see cref="DiscordChannel"/> by <see cref="DiscordClient"/>, ChannelId, and Optional GuildId
        /// </summary>
        /// <param name="client">Client for the channel</param>
        /// <param name="channelId">Channel ID of the channel</param>
        /// <param name="guildId">Guild ID of the channel if channel is in a guild</param>
        /// <returns>This</returns>
        public PlaceholderData AddDiscordChannel(DiscordClient client, Snowflake channelId, Snowflake? guildId = null)
        {
            return AddDiscordChannel(client.Bot?.GetChannel(channelId, guildId));
        }

        /// <summary>
        /// Adds a <see cref="DiscordChannel"/>
        /// </summary>
        /// <param name="channel">Channel to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddDiscordChannel(DiscordChannel channel)
        {
            if (channel != null)
            {
                _objects[nameof(DiscordChannel)] = channel;
            }
            
            return this;
        }
        
        /// <summary>
        /// Adds a <see cref="IPlayer"/>
        /// </summary>
        /// <param name="player">player to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddPlayer(IPlayer player)
        {
            if (player != null)
            {
                _objects[nameof(IPlayer)] = player;
            }
            
            return this;
        }

        /// <summary>
        /// Adds a Unix Timestamp
        /// </summary>
        /// <param name="timestamp">Unix timestamp</param>
        /// <returns>This</returns>
        public PlaceholderData AddTimestamp(ulong timestamp)
        {
            _objects[TimestampName] = timestamp;
            return this;
        }
        
        /// <summary>
        /// Adds type {T} to the placeholder. Type name is used as the data key
        /// </summary>
        /// <param name="obj">Object to add</param>
        /// <typeparam name="T">Type of the data</typeparam>
        /// <returns>This</returns>
        public PlaceholderData Add<T>(T obj)
        {
            return Add(typeof(T).Name, obj);
        }

        /// <summary>
        /// Adds the data with the given name
        /// </summary>
        /// <param name="name">Name of the data key</param>
        /// <param name="obj">Object to add</param>
        /// <typeparam name="T">Type of the data</typeparam>
        /// <returns>This</returns>
        public PlaceholderData Add<T>(string name, T obj)
        {
            _objects[name] = obj;
            return this;
        }

        /// <summary>
        /// Returns the object with the given type of T
        /// If the object is not found the default(T) is returned
        /// </summary>
        /// <param name="name">Name of the data key</param>
        /// <typeparam name="T">Type to return</typeparam>
        /// <returns>{T}</returns>
        public T Get<T>(string name)
        {
            if (_objects.TryGetValue(name, out object obj))
            {
                return (T)obj;
            }

            return default(T);
        }

        /// <summary>
        /// If you wish to keep the data for more than one placeholder use this to disable pooling
        /// </summary>
        public void DisablePooling()
        {
            ShouldPool = false;
        }
    }
}