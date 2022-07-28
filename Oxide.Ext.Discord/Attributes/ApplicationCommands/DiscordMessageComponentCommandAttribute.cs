using System;

namespace Oxide.Ext.Discord.Attributes.ApplicationCommands
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DiscordMessageComponentCommandAttribute : Attribute
    {
        public readonly string CustomId;

        public DiscordMessageComponentCommandAttribute(string customId) 
        {
            CustomId = customId;
        }
    }
}