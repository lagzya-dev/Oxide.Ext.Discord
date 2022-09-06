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
        
        internal static void ThrowIfInvalidResponseType(InteractionType type, InteractionResponseType responseType)
        {
            switch (type)
            {
                case InteractionType.Ping when responseType != InteractionResponseType.Pong:
                    throw new InvalidInteractionResponseException("You can only response to InteractionType.Ping with InteractionResponseType.Pong");
                case InteractionType.ApplicationCommand when responseType != InteractionResponseType.ChannelMessageWithSource && responseType != InteractionResponseType.DeferredChannelMessageWithSource && responseType != InteractionResponseType.Modal:
                    throw new InvalidInteractionResponseException("You can only response to InteractionType.ApplicationCommand with InteractionResponseType.ChannelMessageWithSource, InteractionResponseType.DeferredChannelMessageWithSource, or InteractionResponseType.Modal");
                case InteractionType.MessageComponent when responseType != InteractionResponseType.UpdateMessage && responseType != InteractionResponseType.DeferredUpdateMessage && responseType != InteractionResponseType.Modal:
                    throw new InvalidInteractionResponseException("You can only response to InteractionType.ApplicationCommand with InteractionResponseType.ChannelMessageWithSource, InteractionResponseType.DeferredChannelMessageWithSource, or InteractionResponseType.Modal");
                case InteractionType.ApplicationCommandAutoComplete when responseType != InteractionResponseType.ApplicationCommandAutocompleteResult:
                    throw new InvalidInteractionResponseException("You can only response to InteractionType.ApplicationCommandAutoComplete with InteractionResponseType.ApplicationCommandAutocompleteResult");
                case InteractionType.ModalSubmit when (responseType == InteractionResponseType.Modal || responseType == InteractionResponseType.Pong):
                    throw new InvalidInteractionResponseException("You can only response to InteractionType.ApplicationCommand with InteractionResponseType.ChannelMessageWithSource or InteractionResponseType.DeferredChannelMessageWithSource");
            }
        }
    }
}