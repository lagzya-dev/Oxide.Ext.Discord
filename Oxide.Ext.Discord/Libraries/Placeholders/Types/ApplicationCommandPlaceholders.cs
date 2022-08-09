using System.Text;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Types
{
    internal class ApplicationCommandPlaceholders : BasePlaceholders<DiscordApplicationCommand>
    {
        internal ApplicationCommandPlaceholders()
        {
            RegisterPlaceholder("command.id", Id);
            RegisterPlaceholder("command.name", Name);
            RegisterPlaceholder("command.mention", Mention);
        }
        
        private void Id(StringBuilder builder, DiscordApplicationCommand command, PlaceholderMatch match) => Replace(builder, match, command.Id);
        private void Name(StringBuilder builder, DiscordApplicationCommand command, PlaceholderMatch match) => Replace(builder, match, command.Name);
        private void Mention(StringBuilder builder, DiscordApplicationCommand command, PlaceholderMatch match) => Replace(builder, match, command.Name);

        protected override string GetDataKey()
        {
            return nameof(DiscordUser);
        }
    }
}