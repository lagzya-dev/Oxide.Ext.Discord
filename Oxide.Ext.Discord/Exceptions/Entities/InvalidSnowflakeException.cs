using System.Collections.Generic;
using Oxide.Ext.Discord.Entities;

namespace Oxide.Ext.Discord.Exceptions.Entities
{
    /// <summary>
    /// Exception thrown when an invalid Snowflake ID is used in an API call
    /// </summary>
    public class InvalidSnowflakeException : BaseDiscordException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        private InvalidSnowflakeException(string message) : base(message) { }

        internal static InvalidSnowflakeException InvalidException(string paramName) => new InvalidSnowflakeException($"Invalid Snowflake ID. Parameter Name: {paramName}");
        
        internal static void ThrowIfInvalid(Snowflake snowflake, string paramName)
        {
            if (!snowflake.IsValid())
            {
                throw InvalidException(paramName);
            }
        }
        
        internal static void ThrowIfInvalid(Snowflake? snowflake, string paramName)
        {
            if (snowflake.HasValue && !snowflake.Value.IsValid())
            {
                throw new InvalidSnowflakeException($"Invalid Snowflake ID. Parameter Name: {paramName}");
            }
        }
        
        internal static void ThrowIfInvalid(ICollection<Snowflake> snowflakes, string paramName)
        {
            int index = 0;
            foreach (Snowflake snowflake in snowflakes)
            {
                if (!snowflake.IsValid())
                {
                    throw new InvalidSnowflakeException($"Invalid Snowflake ID. Parameter Name: {paramName}[{index}]");
                }
                index++;
            }
        }
        
        internal static void ThrowIfInvalid(Snowflake? snowflake, bool requireValue, string paramName)
        {
            if (requireValue && !snowflake.HasValue)
            {
                throw new InvalidSnowflakeException($"Snowflake is null when snowflake is required. Parameter Name: {paramName}");
            }

            if (snowflake.HasValue)
            {
                ThrowIfInvalid(snowflake.Value, paramName);
            }
        }
    }
}