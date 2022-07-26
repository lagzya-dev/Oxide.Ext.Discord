namespace Oxide.Ext.Discord.Exceptions.Entities.Interactions
{
    /// <summary>
    /// Exception for invalid Auto Complete choices
    /// </summary>
    public class InvalidAutoCompleteChoiceException : BaseDiscordException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        protected InvalidAutoCompleteChoiceException(string message) : base(message) { }

        internal static void ThrowIfInvalidName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidAutoCompleteChoiceException("Name must contain at least 1 character");
            }

            if (name.Length > 100)
            {
                throw new InvalidAutoCompleteChoiceException("Name cannot be greater than 100 characters");
            }
        }

        internal static void ThrowIfInvalidValue(object value)
        {
            if (value == null)
            {
                throw new InvalidAutoCompleteChoiceException("Value cannot be null");
            }
        }
    }
}