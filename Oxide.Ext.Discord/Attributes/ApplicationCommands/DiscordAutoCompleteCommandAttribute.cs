using Oxide.Ext.Discord.Entities.Interactions;

namespace Oxide.Ext.Discord.Attributes.ApplicationCommands
{
    public class DiscordAutoCompleteCommandAttribute : BaseApplicationCommandAttribute
    {
        public readonly string ArgumentName;
        
        public DiscordAutoCompleteCommandAttribute(string command, string argumentName) : base(InteractionType.ApplicationCommandAutoComplete, command) 
        {
            ArgumentName = argumentName;
        }
    }
}