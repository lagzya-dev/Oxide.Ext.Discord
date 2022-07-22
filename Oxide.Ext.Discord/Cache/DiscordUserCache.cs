using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Cache
{
    public static class DiscordUserCache
    {
        private static readonly ConcurrentDictionary<Snowflake, DiscordUser> InternalCache = new ConcurrentDictionary<Snowflake, DiscordUser>();
        public static readonly IReadOnlyDictionary<Snowflake, DiscordUser> Cache = new ReadOnlyDictionary<Snowflake, DiscordUser>(InternalCache);

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