using Newtonsoft.Json;
using Oxide.Ext.Discord.Exceptions;
using Oxide.Ext.Discord.Validations;
namespace Oxide.Ext.Discord.Entities.Users
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/user#modify-current-user-json-params">Modify Current User Structure</a>
    /// </summary>
    public class UserModifyCurrent : IDiscordValidation
    {
        /// <summary>
        /// User's username, if changed may cause the user's discriminator to be randomized.
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }
        
        /// <summary>
        /// If passed, modifies the user's avatar
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        
        /// <inheritdoc/>
        public void Validate()
        {
            if (!string.IsNullOrEmpty(Username))
            {
                if (Username.IndexOfAny("@#:".ToCharArray()) != -1)
                {
                    throw new ModifyUserException($"Name '{Username}' Cannot contain any of the following characters [@,#,:]");
                }

                if (Username.Contains("```"))
                {
                    throw new ModifyUserException($"Name '{Username}' Cannot contain \"```\"");
                }

                if (Username.Contains("discord"))
                {
                    throw new ModifyUserException($"Name '{Username}' Cannot contain \"discord\"");
                }
            }
        }
    }
}