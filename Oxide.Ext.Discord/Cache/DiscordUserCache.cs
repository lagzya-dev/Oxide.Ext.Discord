using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Cache
{
    /// <summary>
    /// <see cref="DiscordUser"/> Cache 
    /// </summary>
    public static class DiscordUserCache
    {
        private static readonly ConcurrentDictionary<Snowflake, DiscordUser> InternalCache = new ConcurrentDictionary<Snowflake, DiscordUser>();
        
        /// <summary>
        /// Readonly Cache of <see cref="DiscordUser"/>
        /// </summary>
        public static readonly IReadOnlyDictionary<Snowflake, DiscordUser> Cache = new ReadOnlyDictionary<Snowflake, DiscordUser>(InternalCache);

        /// <summary>
        /// Returns a cached <see cref="DiscordUser"/> for the given user ID or creates a new <see cref="DiscordUser"/> with that ID
        /// </summary>
        /// <param name="userId">User ID to lookup in the cache</param>
        /// <returns>Cached <see cref="DiscordUser"/></returns>
        public static DiscordUser GetOrCreate(Snowflake userId)
        {
            if (!InternalCache.TryGetValue(userId, out DiscordUser user))
            {
                user = new DiscordUser
                {
                    Id = userId
                };
                InternalCache[userId] = user;
            }

            return user;
        }

        /// <summary>
        /// Returns a cached <see cref="DiscordUser"/> for the given user or returns the passed in DiscordUser that is now cached
        /// </summary>
        /// <param name="user">User to lookup in the cache</param>
        /// <returns>Cached <see cref="DiscordUser"/></returns>
        public static DiscordUser GetOrCreate(DiscordUser user)
        {
            if (!InternalCache.TryGetValue(user.Id, out DiscordUser existingUser))
            {
                InternalCache[user.Id] = user;
                existingUser = user;
            }
            else
            {
                existingUser.Update(user);
            }

            return existingUser;
        }
    }
}