using System.Text;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Types
{
    internal class RolePlaceholders : BasePlaceholders<DiscordRole>
    {
        public RolePlaceholders()
        {
            RegisterPlaceholder("role.id", Id);
            RegisterPlaceholder("role.name", Name);
            RegisterPlaceholder("role.mention", Mention);
        }
        
        private void Id(StringBuilder builder, DiscordRole role, PlaceholderMatch match) => Replace(builder, match, role.Id);
        private void Name(StringBuilder builder, DiscordRole role, PlaceholderMatch match) => Replace(builder, match, role.Name);
        private void Mention(StringBuilder builder, DiscordRole role, PlaceholderMatch match) => Replace(builder, match, role.Mention);

        protected override string GetDataKey()
        {
            return nameof(DiscordUser);
        }
    }
}