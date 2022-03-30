using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Webhooks;
using Oxide.Ext.Discord.Exceptions;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#create-followup-message">Command Followup</a> within discord.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class CommandFollowupCreate : WebhookCreateMessage
    {
        /// <inheritdoc/>
        protected override void ValidateFlags()
        {
            if (Flags.HasValue && (Flags.Value & ~(MessageFlags.SuppressEmbeds | MessageFlags.Ephemeral)) != 0)
            {
                throw new InvalidMessageException("Invalid Message Flags Used for Interaction Message. Only supported flags are MessageFlags.SuppressEmbeds and MessageFlags.Ephemeral");
            }
        }
    }
}