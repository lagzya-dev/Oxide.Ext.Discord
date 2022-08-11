using System.Text;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Libraries.Placeholders.Types;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal class RolePlaceholders : PlaceholderCollection<DiscordRole>
    {
        private void Id(StringBuilder builder, DiscordRole role, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, role.Id);
        private void Name(StringBuilder builder, DiscordRole role, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, role.Name);
        private void Mention(StringBuilder builder, DiscordRole role, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, role.Mention);

        public override void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterPlaceholderInternal<DiscordRole>("role.id", GetDataKey(), Id);
            placeholders.RegisterPlaceholderInternal<DiscordRole>("role.name", GetDataKey(), Name);
            placeholders.RegisterPlaceholderInternal<DiscordRole>("role.mention", GetDataKey(), Mention);
        }
    }
}