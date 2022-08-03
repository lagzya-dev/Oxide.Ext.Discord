using System.Text.RegularExpressions;

namespace Oxide.Ext.Discord.Exceptions.Libraries
{
    /// <summary>
    /// Exception for Discord Templates
    /// </summary>
    public class DiscordTemplateException : BaseDiscordException
    {
        private static readonly Regex FileNameRegex = new Regex(@"^[\w \.]+$", RegexOptions.Compiled);
        
        private DiscordTemplateException(string message) : base(message) { }

        internal static void ThrowIfInvalidTemplateName(string name)
        {
            if (!FileNameRegex.IsMatch(name))
            {
                throw new DiscordTemplateException($"{name} is not a valid template name. Only Letters, Numbers, _, ., and spaces are allowed");
            }
        }
    }
}