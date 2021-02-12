using System;

namespace Oxide.Ext.Discord.Attributes
{
    /// <summary>
    /// Used to identify direct message bot commands
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DirectMessageCommandAttribute : Attribute
    {
        /// <summary>
        /// Name of the command
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Constructor for a direct message command
        /// </summary>
        /// <param name="name">Name of the command</param>
        public DirectMessageCommandAttribute(string name)
        {
            Name = name;
        }
    }
}
