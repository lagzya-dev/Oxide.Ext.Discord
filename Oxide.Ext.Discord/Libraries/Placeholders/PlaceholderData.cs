using System;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Libraries.Placeholders.Default;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders
{
    /// <summary>
    /// Placeholder Data for placeholders
    /// </summary>
    public class PlaceholderData : IDisposable
    {
        private readonly Hash<string, object> _data = new Hash<string, object>();
        private bool _shouldPool = true;
        private bool _disposed;

        internal PlaceholderData() { }

        internal void AddServer(IServer server) => Add(nameof(IServer), server);
        
        /// <summary>
        /// Add a <see cref="DiscordApplicationCommand"/>
        /// </summary>
        /// <param name="command">Application Command to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddCommand(DiscordApplicationCommand command) => Add(command);

        /// <summary>
        /// Add a <see cref="DiscordGuild"/> by <see cref="DiscordClient"/> and GuildId
        /// </summary>
        /// <param name="client">Discord Client for the guild</param>
        /// <param name="guildId">Guild ID of the guild</param>
        /// <returns>This</returns>
        public PlaceholderData AddGuild(DiscordClient client, Snowflake? guildId) => AddGuild(client.Bot?.GetGuild(guildId));

        /// <summary>
        /// Add a <see cref="DiscordGuild"/>
        /// </summary>
        /// <param name="guild">Guild to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddGuild(DiscordGuild guild) => Add(guild);

        /// <summary>
        /// Add a <see cref="GuildMember"/> by <see cref="DiscordClient"/>, GuildId, and UserId
        /// </summary>
        /// <param name="client">DiscordClient for the guild</param>
        /// <param name="guildId">Guild ID for the guild</param>
        /// <param name="memberId">Member UserId in the guild</param>
        /// <returns>This</returns>
        public PlaceholderData AddGuildMember(DiscordClient client, Snowflake guildId, Snowflake memberId) => AddGuildMember(client.Bot?.GetGuild(guildId).Members[memberId]);

        /// <summary>
        /// Add a <see cref="GuildMember"/>
        /// </summary>
        /// <param name="member">Member to add</param>
        /// <returns></returns>
        public PlaceholderData AddGuildMember(GuildMember member)
        {
            AddUser(member?.User);
            return Add(member);
        }

        /// <summary>
        /// Adds a <see cref="DiscordUser"/>
        /// </summary>
        /// <param name="user">User to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddUser(DiscordUser user)
        {
            AddPlayer(user?.Player);
            return Add(user);
        }

        /// <summary>
        /// Adds a <see cref="DiscordRole"/> by <see cref="DiscordClient"/>, GuildId, and RoleId
        /// </summary>
        /// <param name="client">Client for the guild</param>
        /// <param name="guildId">Guild ID of the guild</param>
        /// <param name="roleId">Role ID of the role</param>
        /// <returns>This</returns>
        public PlaceholderData AddRole(DiscordClient client, Snowflake guildId, Snowflake roleId) => AddRole(client.Bot?.GetGuild(guildId)?.Roles[roleId]);

        /// <summary>
        /// Adds a <see cref="DiscordRole"/>
        /// </summary>
        /// <param name="role">Role to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddRole(DiscordRole role) => Add(role);

        /// <summary>
        /// Adds a <see cref="DiscordChannel"/> by <see cref="DiscordClient"/>, ChannelId, and Optional GuildId
        /// </summary>
        /// <param name="client">Client for the channel</param>
        /// <param name="channelId">Channel ID of the channel</param>
        /// <param name="guildId">Guild ID of the channel if channel is in a guild</param>
        /// <returns>This</returns>
        public PlaceholderData AddChannel(DiscordClient client, Snowflake channelId, Snowflake? guildId = null) => AddChannel(client.Bot?.GetChannel(channelId, guildId));

        /// <summary>
        /// Adds a <see cref="DiscordChannel"/>
        /// </summary>
        /// <param name="channel">Channel to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddChannel(DiscordChannel channel) => Add(channel);

        /// <summary>
        /// Adds a <see cref="IPlayer"/>
        /// </summary>
        /// <param name="player">player to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddPlayer(IPlayer player) => Add(nameof(IPlayer), player);

        /// <summary>
        /// Adds a <see cref="Plugin"/>
        /// </summary>
        /// <param name="plugin">Plugin to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddPlugin(Plugin plugin) => Add(nameof(Plugin), plugin);
        
        /// <summary>
        /// Adds a Unix Timestamp
        /// </summary>
        /// <param name="timestamp">Unix timestamp</param>
        /// <returns>This</returns>
        public PlaceholderData AddTimestamp(ulong timestamp) => Add(TimestampPlaceholders.TimestampName, timestamp);

        /// <summary>
        /// Adds a <see cref="Snowflake"/>
        /// </summary>
        /// <param name="id"><see cref="Snowflake"/> ID</param>
        /// <returns>This</returns>
        public PlaceholderData AddSnowflake(Snowflake id) => Add(nameof(Snowflake), id);
        
        /// <summary>
        /// Add a <see cref="RequestError"/>
        /// </summary>
        /// <param name="error">RequestError to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddRequestError(RequestError error) => Add(error);

        /// <summary>
        /// Adds type {T} to the placeholder. Type name is used as the data key
        /// </summary>
        /// <param name="obj">Object to add</param>
        /// <typeparam name="T">Type of the data</typeparam>
        /// <returns>This</returns>
        public PlaceholderData Add(object obj)
        {
            if (obj != null)
            {
                Add(obj.GetType().Name, obj);
            }
            
            return this;
        }

        /// <summary>
        /// Adds the data with the given name
        /// </summary>
        /// <param name="name">Name of the data key</param>
        /// <param name="obj">Object to add</param>
        /// <typeparam name="T">Type of the data</typeparam>
        /// <returns>This</returns>
        public PlaceholderData Add(string name, object obj)
        {
            if (obj != null)
            {
                _data[name] = obj;
            }
           
            return this;
        }

        /// <summary>
        /// Returns the object with the given type of {T}
        /// The key name used is <code>nameof(T)</code>
        /// </summary>
        /// <typeparam name="T">Type to return</typeparam>
        /// <returns>{T}</returns>
        public T Get<T>()
        {
            return Get<T>(nameof(T));
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
            if (_data.TryGetValue(name, out object obj))
            {
                return (T)obj;
            }

            return default(T);
        }

        public string GetKeys()
        {
            return string.Join(", ", _data.Keys);
        }

        /// <summary>
        /// Disable automatic pooling and handle manually by plugin
        /// </summary>
        public void ManualPool()
        {
            _shouldPool = false;
        }

        public void Dispose()
        {
            if (_shouldPool && !_disposed)
            {
                _disposed = true;
                DiscordPool.FreePlaceholderData(this);
            }
        }
    }
}