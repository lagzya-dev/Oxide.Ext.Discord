using System.Text;
using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Types
{
    internal class GuildPlaceholders : BasePlaceholders<DiscordGuild>
    {
        public GuildPlaceholders()
        {
            RegisterPlaceholder("guild.id", Id);
            RegisterPlaceholder("guild.name", Name);
            RegisterPlaceholder("guild.description", Description);
            RegisterPlaceholder("guild.members.count", MemberCount);
        }

        private void Id(StringBuilder builder, DiscordGuild guild, PlaceholderMatch match) => Replace(builder, match, guild.Id);
        private void Name(StringBuilder builder, DiscordGuild guild, PlaceholderMatch match) => Replace(builder, match, guild.Name);
        private void Description(StringBuilder builder, DiscordGuild guild, PlaceholderMatch match) => Replace(builder, match, guild.Description);
        private void MemberCount(StringBuilder builder, DiscordGuild guild, PlaceholderMatch match) => Replace(builder, match, guild.Members.Count.ToString(match.Format));

        protected override string GetDataKey()
        {
            return nameof(DiscordGuild);
        }
    }
}