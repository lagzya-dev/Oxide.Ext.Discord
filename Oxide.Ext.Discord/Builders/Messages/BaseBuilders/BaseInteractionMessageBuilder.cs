using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Messages.AllowedMentions;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Exceptions.Builders;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.MessageComponents;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Builders.Messages.BaseBuilders
{
    /// <summary>
    /// Represents a builder for <see cref="BaseInteractionMessage"/>
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class BaseInteractionMessageBuilder<TMessage, TBuilder> : BaseMessageBuilder<TMessage, TBuilder>
        where TMessage : BaseInteractionMessage
        where TBuilder : BaseInteractionMessageBuilder<TMessage, TBuilder>
    {
        /// <summary>
        /// Interaction this interaction builder is for
        /// </summary>
        protected readonly DiscordInteraction Interaction;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="interaction">Interaction this builder is for</param>
        /// <param name="message">Message to be created</param>
        public BaseInteractionMessageBuilder(DiscordInteraction interaction, TMessage message) : base(message)
        {
            Interaction = interaction ?? throw new ArgumentNullException(nameof(interaction));
        }
        
        /// <summary>
        /// Marks the response as Ephemeral so only the person who executed the command can see
        /// </summary>
        /// <returns></returns>
        public TBuilder AsEphemeral()
        {
            InteractionResponseBuilderException.ThrowIfInteractionIsAutoComplete(Interaction.Type);
            Message.Flags |= MessageFlags.Ephemeral;
            return Builder;
        }

        /// <summary>
        /// Adds an empty array of Auto Complete Choices
        /// </summary>
        /// <returns>This</returns>
        public TBuilder AddEmptyAutoCompleteChoices()
        {
            if (Message.Choices == null)
            {
                Message.Choices = new List<CommandOptionChoice>();
            }
            return Builder;
        }
        
        /// <summary>
        /// Adds a <see cref="CommandOptionChoice"/> to the response
        /// </summary>
        /// <param name="name">Name of the choice</param>
        /// <param name="value">Value of the choice</param>
        /// <param name="nameLocalizations">Name localizations for the choice</param>
        /// <returns>This</returns>
        public TBuilder AddAutoCompleteChoice(string name, object value, Hash<string, string> nameLocalizations = null)
        {
            return AddAutoCompleteChoice(new CommandOptionChoice(name, value, nameLocalizations));
        }
        
        /// <summary>
        /// Adds a <see cref="CommandOptionChoice"/> to the response
        /// </summary>
        /// <param name="choice">Choice to be added</param>
        /// <returns>This</returns>
        public TBuilder AddAutoCompleteChoice(CommandOptionChoice choice)
        {
            if (choice == null) throw new ArgumentNullException(nameof(choice));
            InteractionResponseBuilderException.ThrowIfInteractionIsNotAutoComplete(Interaction.Type);
            AddEmptyAutoCompleteChoices();
            
            InvalidCommandOptionChoiceException.ThrowIfMaxChoices(Message.Choices.Count + 1);
            Message.Choices.Add(choice);
            return Builder;
        }
        
        /// <summary>
        /// Adds a collection of <see cref="CommandOptionChoice"/> to the response
        /// </summary>
        /// <param name="choices">Choices to be added</param>
        /// <returns>This</returns>
        public TBuilder AddAutoCompleteChoices(ICollection<CommandOptionChoice> choices)
        {
            if (choices == null) throw new ArgumentNullException(nameof(choices));
            InteractionResponseBuilderException.ThrowIfInteractionIsNotAutoComplete(Interaction.Type);
            AddEmptyAutoCompleteChoices();
            
            InvalidCommandOptionChoiceException.ThrowIfMaxChoices(Message.Choices.Count + choices.Count);
            Message.Choices.AddRange(choices);
            return Builder;
        }

        /// <summary>
        /// Adds a custom ID for the modal
        /// </summary>
        /// <param name="customId">Custom ID for the modal</param>
        public TBuilder AddModalCustomId(string customId)
        {
            InteractionResponseBuilderException.ThrowIfInteractionIsAutoComplete(Interaction.Type);
            InteractionResponseBuilderException.ThrowIfInteractionIsModalSubmit(Interaction.Type);
            InvalidMessageComponentException.ThrowIfInvalidCustomId(customId);
            Message.CustomId = customId;
            return Builder;
        }
        
        /// <summary>
        /// Adds a custom ID for the modal
        /// </summary>
        /// <param name="title">Title for the Modal</param>
        public TBuilder AddModalTitle(string title)
        {
            InteractionResponseBuilderException.ThrowIfInteractionIsAutoComplete(Interaction.Type);
            InteractionResponseBuilderException.ThrowIfInteractionIsModalSubmit(Interaction.Type);
            InvalidMessageComponentException.ThrowIfInvalidModalTitle(title);
            Message.Title = title;
            return Builder;
        }

        #region Overrides
        ///<inheritdoc/>
        public override TBuilder AsTts(bool enabled = true)
        {
            InteractionResponseBuilderException.ThrowIfInteractionIsAutoComplete(Interaction.Type);
            return base.AsTts(enabled);
        }

        ///<inheritdoc/>
        public override TBuilder AddContent(string content)
        {
            InteractionResponseBuilderException.ThrowIfInteractionIsAutoComplete(Interaction.Type);
            return base.AddContent(content);
        }

        ///<inheritdoc/>
        public override TBuilder AddEmbed(DiscordEmbed embed)
        {
            InteractionResponseBuilderException.ThrowIfInteractionIsAutoComplete(Interaction.Type);
            return base.AddEmbed(embed);
        }

        ///<inheritdoc/>
        public override TBuilder AddEmbeds(ICollection<DiscordEmbed> embeds)
        {
            InteractionResponseBuilderException.ThrowIfInteractionIsAutoComplete(Interaction.Type);
            return base.AddEmbeds(embeds);
        }

        ///<inheritdoc/>
        public override TBuilder AddAllowedMentions(AllowedMention mention)
        {
            InteractionResponseBuilderException.ThrowIfInteractionIsAutoComplete(Interaction.Type);
            return base.AddAllowedMentions(mention);
        }

        ///<inheritdoc/>
        public override TBuilder SuppressEmbeds()
        {
            InteractionResponseBuilderException.ThrowIfInteractionIsAutoComplete(Interaction.Type);
            return base.SuppressEmbeds();
        }

        ///<inheritdoc/>
        public override TBuilder AddActionRow(ActionRowComponent component)
        {
            InteractionResponseBuilderException.ThrowIfInteractionIsAutoComplete(Interaction.Type);
            return Builder.AddActionRow(component);
        }

        ///<inheritdoc/>
        public override TBuilder AddComponents(ICollection<ActionRowComponent> components)
        {
            InteractionResponseBuilderException.ThrowIfInteractionIsAutoComplete(Interaction.Type);
            return base.AddComponents(components);
        }

        ///<inheritdoc/>
        public override TBuilder AddAttachment(string filename, byte[] data, string contentType, string description = null)
        {
            InteractionResponseBuilderException.ThrowIfInteractionIsAutoComplete(Interaction.Type);
            return base.AddAttachment(filename, data, contentType, description);
        }
        #endregion
    }
}