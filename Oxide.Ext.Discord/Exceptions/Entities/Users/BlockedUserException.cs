using System;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Exceptions.Entities.Users
{
    public class BlockedUserException : BaseDiscordException
    {
        public readonly DiscordUser User;
        public readonly DateTime BlockedUntil;

        public BlockedUserException(DiscordUser user, DateTime blockedUntil)
        {
            User = user;
            BlockedUntil = blockedUntil;
        }
    }
}