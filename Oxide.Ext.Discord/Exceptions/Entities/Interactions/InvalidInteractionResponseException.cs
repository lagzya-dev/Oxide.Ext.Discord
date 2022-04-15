using System;
using Oxide.Ext.Discord.Entities.Interactions;

namespace Oxide.Ext.Discord.Exceptions.Entities.Interactions
{
    /// <summary>
    /// Error thrown when an interaction response is invalid
    /// </summary>
    public class InvalidInteractionResponseException : BaseDiscordException
    {
        private static readonly TimeSpan MaxInitialResponseDuration = TimeSpan.FromSeconds(3);
        private static readonly TimeSpan MaxTokenLife = TimeSpan.FromMinutes(15);
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        private InvalidInteractionResponseException(string message) : base(message) { }

        internal static void ThrowIfAlreadyResponded(bool responded)
        {
            if (responded)
            {
                throw new InvalidInteractionResponseException($"This interaction has already been responded too and can't be responded to again. Please use {nameof(DiscordInteraction)}.{nameof(DiscordInteraction.CreateFollowUpMessage)} to create a follow up message");
            }
        }
        
        internal static void ThrowIfNotResponded(bool responded)
        {
            if (!responded)
            {
                throw new InvalidInteractionResponseException($"You cannot use this endpoint because {nameof(DiscordInteraction)}.{nameof(DiscordInteraction.CreateInteractionResponse)} hasn't been called yet");
            }
        }
        
        internal static void ThrowIfInitialResponseTimeElapsed(DateTime createdDate)
        {
            if (DateTime.UtcNow - createdDate > MaxInitialResponseDuration)
            {
                throw new InvalidInteractionResponseException($"This interaction has expired as it took longer than {MaxInitialResponseDuration.TotalSeconds:0} seconds to respond to the interaction");
            }
        }
        
        internal static void ThrowIfMaxResponseTimeElapsed(DateTime createdDate)
        {
            if (DateTime.UtcNow - createdDate > MaxTokenLife)
            {
                throw new InvalidInteractionResponseException($"This interaction has expired as it has been longer than {MaxTokenLife.TotalMinutes:0} minutes");
            }
        }
    }
}