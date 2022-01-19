namespace Oxide.Ext.Discord.Exceptions
{
    public class InvalidFileNameException : BaseDiscordException
    {
        public InvalidFileNameException(string fileName) : base($"'{fileName}' is not a valid filename for discord. " +
                                                                "Valid filename characters are alphanumeric with underscores, dashes, or dots") 
        { }
    }
}