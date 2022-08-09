using System.Text;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Types
{
    internal class MemberPlaceholders : BasePlaceholders<GuildMember>
    {
        public MemberPlaceholders()
        {
            RegisterPlaceholder("member.id", Id);
            RegisterPlaceholder("member.name", Name);
            RegisterPlaceholder("member.mention", Mention);
            RegisterPlaceholder("member.communicationdisableduntil", CommunicationDisabledUntil);
        }
        
        private void Id(StringBuilder builder, GuildMember member, PlaceholderMatch match) => Replace(builder, match, member.Id);
        private void Name(StringBuilder builder, GuildMember member, PlaceholderMatch match) => Replace(builder, match, member.DisplayName);
        private void Mention(StringBuilder builder, GuildMember member, PlaceholderMatch match) => Replace(builder, match, member.User.Mention);
        private void CommunicationDisabledUntil(StringBuilder builder, GuildMember member, PlaceholderMatch match)
        {
            string format = member.CommunicationDisabledUntil.HasValue ? member.CommunicationDisabledUntil.Value.ToString(match.Format) : string.Empty;
            Replace(builder, match, format);
        }

        protected override string GetDataKey()
        {
            return nameof(DiscordUser);
        }
    }
}