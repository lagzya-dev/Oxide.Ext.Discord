using System.Collections.Generic;
using Oxide.Ext.Discord.Entities;
using ProtoBuf;

namespace Oxide.Ext.Discord.Data.Users
{
    [ProtoContract]
    internal class BotData
    {
        [ProtoMember(1)]
        public Dictionary<Snowflake, UserData> Users { get; set; } = new Dictionary<Snowflake, UserData>();
        
        public UserData GetUserData(Snowflake userId)
        {
            if (!Users.TryGetValue(userId, out UserData data))
            {
                data = new UserData(userId);
                Users[userId] = data;
                DiscordUsersData.Instance.OnDataChanged();
            }

            return data;
        }
    }
}