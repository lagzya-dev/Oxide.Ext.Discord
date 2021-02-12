using System;

namespace Oxide.Ext.Discord.Attributes
{
    /// <summary>
    /// Used to identify guild bot commands
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class GuildCommandAttribute : Attribute
    {
        /// <summary>
        /// Name of the command
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Constructor for a guild command
        /// </summary>
        /// <param name="name">Name of the command</param>
        public GuildCommandAttribute(string name)
        {
            Name = name;
        }
    }
}
