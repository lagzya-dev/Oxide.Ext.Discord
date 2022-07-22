using System.Collections.Generic;
using Oxide.Ext.Discord.Entities;
using ProtoBuf;

namespace Oxide.Ext.Discord.Data
{
    [ProtoContract]
    internal class BotExtData
    {
        [ProtoMember(1)]
        public Dictionary<Snowflake, UserExtData> Users { get; set; } = new Dictionary<Snowflake, UserExtData>();
        
        public UserExtData GetUserData(Snowflake userId)
        {
            if (!Users.TryGetValue(userId, out UserExtData data))
            {
                data = new UserExtData(userId);
                Users[userId] = data;
                DataHandler.OnDataChanged();
            }

            return data;
        }
    }
}