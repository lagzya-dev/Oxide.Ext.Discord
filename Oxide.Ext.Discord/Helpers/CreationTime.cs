using System;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Helpers
{
    public class CreationTime
    {
        public static DateTime? GetFromUser(DiscordUser user) => GetFromID(user.Id);

        public static DateTime? GetFromID(string ID)
        {
            long id = 0;
            long.TryParse(ID, out id);
            if (id == 0) return null;

            long ageInSeconds = (id >> 22) / 1000;

            return new DateTime(2015, 1, 1).AddSeconds(ageInSeconds);
        }
    }
}
