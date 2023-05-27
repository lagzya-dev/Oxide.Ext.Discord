using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Exceptions.Entities.Messages;

namespace Oxide.Ext.Discord.Entities.Interactions.Response
{
    /// <summary>
    /// Represents a Base Message for an interaction
    /// </summary>
    public abstract class BaseInteractionMessage : BaseMessageCreate
    {
        ///<inheritdoc/>
        protected override void ValidateRequiredFields() { }

        ///<inheritdoc/>
        protected override void ValidateFlags()
        {
            InvalidMessageException.ThrowIfInvalidFlags(Flags, MessageFlags.SuppressEmbeds | MessageFlags.Ephemeral, "Invalid Message Flags Used for Channel Message. Only supported flags are MessageFlags.SuppressEmbeds and MessageFlags.Ephemeral");
        }
    }
}