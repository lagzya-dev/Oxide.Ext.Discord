using System.Collections.Generic;
using Oxide.Ext.Discord.Entities;
using ProtoBuf;

namespace Oxide.Ext.Discord.Data
{
    [ProtoContract]
    internal class DiscordUserData : BaseDataFile<DiscordUserData>
    {
        [ProtoMember(1)]
        public Dictionary<Snowflake, BotData> Bots { get; set; } = new Dictionary<Snowflake, BotData>();

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

        protected override string GetFileName() => "discord.users.data";
        protected override int GetNumBackups() => 2;
    }
}