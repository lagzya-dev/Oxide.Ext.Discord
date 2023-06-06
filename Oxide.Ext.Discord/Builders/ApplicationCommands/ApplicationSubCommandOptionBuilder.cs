using System.Collections.Generic;
using Oxide.Ext.Discord.Builders.ApplicationCommands.BaseBuilders;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Libraries.Locale;

namespace Oxide.Ext.Discord.Builders.ApplicationCommands
{
    /// <summary>
    /// Represents a Subcommand Option Builder for SubCommands
    /// </summary>
    public class ApplicationSubCommandOptionBuilder : BaseCommandOptionBuilder<ApplicationSubCommandOptionBuilder, ApplicationSubCommandBuilder>
    {
        internal ApplicationSubCommandOptionBuilder(List<CommandOption> parent, CommandOptionType type, string name, string description, ApplicationSubCommandBuilder builder, ServerLocale defaultLanguage) : base(parent, type, name, description, builder, defaultLanguage) { }
    }
}