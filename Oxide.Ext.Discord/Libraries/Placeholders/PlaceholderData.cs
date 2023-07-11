using System;
using System.Collections.Generic;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Clients;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries.Placeholders.Default;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Pooling.Entities;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders
{
    /// <summary>
    /// Placeholder Data for placeholders
    /// </summary>
    public class PlaceholderData : BasePoolable
    {
        private readonly Hash<string, object> _data = new Hash<string, object>();
        private readonly List<IBoxed> _boxed = new List<IBoxed>();
        internal bool AutoPool { get; private set; } = true;

        internal PlaceholderData() { }

        internal void AddServer(IServer server) => Add(nameof(IServer), server);
        
        /// <summary>
        /// Add a <see cref="DiscordApplicationCommand"/>
        /// </summary>
        /// <param name="command">Application Command to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddCommand(DiscordApplicationCommand command) => Add(nameof(DiscordApplicationCommand), command);

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
        public PlaceholderData AddGuild(DiscordGuild guild) => Add(nameof(DiscordGuild), guild);
        
        public PlaceholderData RemoveGuild() => Remove(nameof(DiscordGuild));

        /// <summary>
        /// Add a <see cref="DiscordMessage"/>
        /// </summary>
        /// <param name="message">Message to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddMessage(DiscordMessage message)
        {
            if (message != null)
            {
                AddGuildMember(message.Member);
                AddUser(message.Author);
                Add(nameof(DiscordMessage), message);
            }
            
            return this;
        }
        
        public PlaceholderData RemoveMessage() => Remove(nameof(DiscordMessage));

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
            if (member != null)
            {
                AddUser(member.User);
                Add(nameof(GuildMember), member);
            }

            return this;
        }
        
        public PlaceholderData RemoveGuildMember() => Remove(nameof(GuildMember));

        /// <summary>
        /// Adds a <see cref="DiscordUser"/>
        /// </summary>
        /// <param name="user">User to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddUser(DiscordUser user)
        {
            if (user != null)
            {
                AddPlayer(user.Player);
                Add(nameof(DiscordUser), user);
            }

            return this;
        }
        
        public PlaceholderData RemoveUser() => Remove(nameof(DiscordUser));

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
        public PlaceholderData AddRole(DiscordRole role) => Add(nameof(DiscordRole), role);
        
        public PlaceholderData RemoveRole() => Remove(nameof(DiscordRole));

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
        public PlaceholderData AddChannel(DiscordChannel channel) => Add(nameof(DiscordChannel), channel);
        
        public PlaceholderData RemoveChannel() => Remove(nameof(DiscordChannel));
        
        /// <summary>
        /// Adds a <see cref="DiscordInteraction"/>
        /// </summary>
        /// <param name="interaction">Interaction to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddInteraction(DiscordInteraction interaction)
        {
            if (interaction != null)
            {
                AddGuildMember(interaction.Member);
                AddUser(interaction.User);
                AddMessage(interaction.Message);
                Add(nameof(DiscordInteraction), interaction);
            }

            return this;
        }

        /// <summary>
        /// Adds a <see cref="IPlayer"/>
        /// </summary>
        /// <param name="player">player to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddPlayer(IPlayer player) => Add(nameof(IPlayer), player);

        public PlaceholderData RemovePlayer() => Remove(nameof(IPlayer));

        /// <summary>
        /// Adds a target <see cref="IPlayer"/>
        /// </summary>
        /// <param name="player">player to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddTarget(IPlayer player) => Add(PlayerPlaceholders.TargetPlayerKey, player);

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
        public PlaceholderData AddTimestamp(DateTimeOffset timestamp) => AddTimestamp(timestamp.ToUnixTimeSeconds());
        
        /// <summary>
        /// Adds a Unix Timestamp
        /// </summary>
        /// <param name="timestamp">Unix timestamp</param>
        /// <returns>This</returns>
        public PlaceholderData AddTimestamp(long timestamp) => Add(TimestampPlaceholders.TimestampName, timestamp);

        /// <summary>
        /// Adds a <see cref="Snowflake"/>
        /// </summary>
        /// <param name="id"><see cref="Snowflake"/> ID</param>
        /// <returns>This</returns>
        public PlaceholderData AddSnowflake(Snowflake id) => Add(nameof(Snowflake), id);
        
        /// <summary>
        /// Add a <see cref="ResponseError"/>
        /// </summary>
        /// <param name="error">RequestError to add</param>
        /// <returns>This</returns>
        public PlaceholderData AddRequestError(ResponseError error) => Add(nameof(ResponseError), error);

        /// <summary>
        /// Adds the data with the given name
        /// </summary>
        /// <param name="name">Name of the data key</param>
        /// <param name="obj">Object to add</param>
        /// <returns>This</returns>
        public PlaceholderData Add<T>(string name, T obj)
        {
            if (typeof(T).IsValueType())
            {
                AddBoxed(name, DiscordPool.Internal.GetBoxed(obj));
                return this;
            }
            
            object value = obj;
            if (value != null)
            {
                _data[name] = value;
            }

            return this;
        }

        private void AddBoxed(string name, IBoxed boxed)
        {
            _data[name] = boxed;
            _boxed.Add(boxed);
        }

        public PlaceholderData Remove(string name)
        {
            if (_data.TryGetValue(name, out object value))
            {
                _data.Remove(name);
                if (value is IBoxed boxed)
                {
                    boxed.Dispose();
                }
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
            return Get<T>(typeof(T).Name);
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
                if (obj is T value)
                {
                    return value;
                }
                if (obj is Boxed<T> boxed)
                {
                    return boxed.Value;
                }

                DiscordExtension.GlobalLogger.Warning($"{nameof(PlaceholderData)}.{nameof(Get)} Failed to Convert Type: {{0}} to Type: {{1}}", obj.GetType().Name, typeof(T));
            }

            return default(T);
        }

        /// <summary>
        /// Returns comma seperated string of all the registered key
        /// Useful for debugging placeholders
        /// </summary>
        /// <returns></returns>
        public string GetKeys()
        {
            return string.Join(", ", _data.Keys);
        }

        /// <summary>
        /// Disable automatic pooling and handle manually by plugin
        /// </summary>
        public void ManualPool()
        {
            AutoPool = false;
        }

        /// <summary>
        /// Clones the current placeholder data into a new <see cref="PlaceholderData"/>
        /// </summary>
        /// <returns></returns>
        public PlaceholderData Clone()
        {
            PlaceholderData clone = PluginPool.GetPlaceholderData();
            foreach (KeyValuePair<string, object> data in _data)
            {
                if (data.Value is IBoxed boxed)
                {
                    clone.AddBoxed(data.Key, boxed.Copy());
                }
                else
                {
                    clone.Add(data.Key, data.Value);
                }
            }
            return clone;
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            for (int index = 0; index < _boxed.Count; index++)
            {
                _boxed[index].Dispose();
            }
            AutoPool = true;
            _data.Clear();
            _boxed.Clear();
        }

        internal void AutoDispose()
        {
            if (AutoPool)
            {
                Dispose();
            }
        }
    }
}