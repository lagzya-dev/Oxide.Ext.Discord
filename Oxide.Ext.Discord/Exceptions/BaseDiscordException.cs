using System;
namespace Oxide.Ext.Discord.Exceptions
{
    public class BaseDiscordException : Exception
    {
        public BaseDiscordException(string message) : base(message)
        {
            
        }
    }
}