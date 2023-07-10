namespace Oxide.Ext.Discord.Exceptions.Libraries.Templates
{
    /// <summary>
    /// Thrown when duplicate templates have been registered for the same type, plugin, and name
    /// </summary>
    public class DuplicateTemplateException : BaseDiscordException
    {
        internal DuplicateTemplateException(string message) : base(message) { }
    }
}