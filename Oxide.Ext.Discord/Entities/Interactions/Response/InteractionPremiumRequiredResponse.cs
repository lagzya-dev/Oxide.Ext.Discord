using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities
{
    /// <summary>
    /// Response for premium Required
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InteractionPremiumRequiredResponse : BaseInteractionResponse<InteractionPremiumRequiredMessage>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public InteractionPremiumRequiredResponse() : base(InteractionResponseType.PremiumRequired, new InteractionPremiumRequiredMessage()) { }
    }
}