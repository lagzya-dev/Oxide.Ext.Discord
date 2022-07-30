using System;
using Oxide.Ext.Discord.Entities.Interactions;

namespace Oxide.Ext.Discord.Attributes.ApplicationCommands
{
    /// <summary>
    /// Discord Message Component Command Attribute for <see cref="InteractionType.MessageComponent"/>
    /// Callback Hook Format:
    /// <code>
    /// private void Callback(DiscordInteraction interaction)
    /// {
    ///     Puts("Callback Works!");
    /// }
    /// </code>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class DiscordMessageComponentCommandAttribute : Attribute
    {
        internal readonly string CustomId;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customId">CustomID to match on. Matching uses string.StartsWith</param>
        public DiscordMessageComponentCommandAttribute(string customId)
        {
            CustomId = customId;
        }
    }
}