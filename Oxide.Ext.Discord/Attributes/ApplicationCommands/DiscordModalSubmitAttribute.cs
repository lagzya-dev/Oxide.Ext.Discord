using System;

namespace Oxide.Ext.Discord.Attributes.ApplicationCommands
{
    public class DiscordModalSubmitAttribute : Attribute
    {
        public readonly string CustomId;

        public DiscordModalSubmitAttribute(string customId)
        {
            CustomId = customId;
        }
    }
}