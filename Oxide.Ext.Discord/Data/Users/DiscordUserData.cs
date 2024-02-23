using System.Collections.Generic;
using Oxide.Ext.Discord.Entities;
using ProtoBuf;

namespace Oxide.Ext.Discord.Data
{
    [ProtoContract]
    internal sealed class DiscordUserData : BaseDataFile<DiscordUserData>
    {
        [ProtoMember(1)]
        private Dictionary<Snowflake, BotData> Bots { get; set; } = new Dictionary<Snowflake, BotData>();

        public bool TryGetBotData(Snowflake id, out BotData data) => Bots.TryGetValue(id, out data);
        
        public BotData GetBotData(Snowflake botId)
        {
            if (!Bots.TryGetValue(botId, out BotData data))
            {
                data = new BotData();
                Bots[botId] = data;
                OnDataChanged();
            }

            return data;
        }
    }
}