using System.Collections.Generic;
using Oxide.Ext.Discord.Entities;
using ProtoBuf;

namespace Oxide.Ext.Discord.Data.Users
{
    [ProtoContract]
    internal class DiscordUsersData : BaseDataFile<DiscordUsersData>
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

        public override string GetFileName() => "discord.users.data";
        public override int GetNumBackups() => 2;
    }
}