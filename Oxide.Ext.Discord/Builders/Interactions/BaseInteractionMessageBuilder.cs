using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Builders.Messages.BaseBuilders;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Entities.Interactions.Response;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Messages.AllowedMentions;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Exceptions.Builders;

namespace Oxide.Ext.Discord.Builders.Interactions
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
        
        ///<inheritdoc/>
        public override TBuilder SuppressEmbeds()
        {
            InteractionResponseBuilderException.ThrowIfInteractionIsAutoComplete(Interaction.Type);
            return base.SuppressEmbeds();
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