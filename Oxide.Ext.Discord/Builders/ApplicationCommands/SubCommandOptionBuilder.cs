using System.Collections.Generic;
using Oxide.Ext.Discord.Builders.ApplicationCommands.BaseBuilders;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Builders.ApplicationCommands
{
    /// <summary>
    /// Represents a Subcommand Option Builder for SubCommands
    /// </summary>
    public class SubCommandOptionBuilder : BaseCommandOptionBuilder<SubCommandOptionBuilder, SubCommandBuilder>
    {
        internal SubCommandOptionBuilder(List<CommandOption> parent, CommandOptionType type, string name, string description, SubCommandBuilder builder) : base(parent, type, name, description, builder) { }
    }
}