using Oxide.Ext.Discord.Exceptions;

namespace Oxide.Ext.Discord.Validations
{
    internal interface IDiscordValidation
    {
        /// <summary>
        /// Validates data being passed to the discord API.
        /// Throws an exception with base type of <see cref="BaseDiscordException"/> if the validation fails
        /// </summary>
        void Validate();
    }
}