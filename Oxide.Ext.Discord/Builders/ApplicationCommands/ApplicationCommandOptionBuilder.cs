using System.Collections.Generic;
using Oxide.Ext.Discord.Builders.ApplicationCommands.BaseBuilders;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Builders.ApplicationCommands
{
    /// <summary>
    /// Represents a Subcommand Option Builder for Application Commands
    /// </summary>
    public class ApplicationCommandOptionBuilder : BaseCommandOptionBuilder<ApplicationCommandOptionBuilder, ApplicationCommandBuilder>
    {
        internal ApplicationCommandOptionBuilder(List<CommandOption> parent, CommandOptionType type, string name, string description, ApplicationCommandBuilder builder) : base(parent, type, name, description, builder) { }
    }
}