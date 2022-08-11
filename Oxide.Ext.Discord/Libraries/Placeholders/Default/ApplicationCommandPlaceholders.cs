using System.Text;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Libraries.Placeholders.Types;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal class ApplicationCommandPlaceholders : PlaceholderCollection<DiscordApplicationCommand>
    {
        private void Id(StringBuilder builder, DiscordApplicationCommand command, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, command.Id);
        private void Name(StringBuilder builder, DiscordApplicationCommand command, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, command.Name);
        private void Mention(StringBuilder builder, DiscordApplicationCommand command, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, command.Name);
        
        public override void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterPlaceholderInternal<DiscordApplicationCommand>("command.id", GetDataKey(), Id);
            placeholders.RegisterPlaceholderInternal<DiscordApplicationCommand>("command.name", GetDataKey(), Name);
            placeholders.RegisterPlaceholderInternal<DiscordApplicationCommand>("command.mention", GetDataKey(), Mention);
        }
    }
}