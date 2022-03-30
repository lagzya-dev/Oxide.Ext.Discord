using Newtonsoft.Json;
using Oxide.Ext.Discord.Exceptions;
using Oxide.Ext.Discord.Validations;
namespace Oxide.Ext.Discord.Entities.Webhooks
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/webhook#create-webhook-json-params">Webhook Create Structure</a>
    /// </summary>
    public class WebhookCreate : IDiscordValidation
    {
        /// <summary>
        /// Name of the webhook (1-80 characters)
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Image for the default webhook avatar
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        
        /// <inheritdoc/>
        public void Validate()
        {
            if (string.IsNullOrEmpty(Name) || Name.Length > 80)
            {
                throw new WebhookCreateException($"Name '{Name}' cannot be less than 1 character or more than 80 characters");
            }

            if (Name.IndexOfAny("@#:".ToCharArray()) != -1)
            {
                throw new WebhookCreateException($"Name '{Name}' Cannot contain any of the following characters [@,#,:]");
            }

            if (Name.Contains("```"))
            {
                throw new WebhookCreateException($"Name '{Name}' Cannot contain \"```\"");
            }

            if (Name.Contains("discord"))
            {
                throw new WebhookCreateException($"Name '{Name}' Cannot contain \"discord\"");
            }
        }
    }
}