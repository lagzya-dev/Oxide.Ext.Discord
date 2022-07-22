using System.Collections.Generic;
using Oxide.Ext.Discord.Entities;
using ProtoBuf;

namespace Oxide.Ext.Discord.Data
{
    [ProtoContract]
    internal class ExtData
    {
        [ProtoMember(1)]
        public Dictionary<Snowflake, BotExtData> Bots { get; set; } = new Dictionary<Snowflake, BotExtData>();

        public BotExtData GetBotData(Snowflake botId)
        {
            if (!Bots.TryGetValue(botId, out BotExtData data))
            {
                data = new BotExtData();
                Bots[botId] = data;
                DataHandler.OnDataChanged();
            }

            return data;
        }
    }
}