using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Libraries.Langs;

namespace Oxide.Ext.Discord.Libraries.Templates.Commands
{
    /// <summary>
    /// Command Localizations for Application Commands
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordCommandLocalization : IDiscordTemplate
    {
        private static readonly TemplateVersion InternalVersion = new TemplateVersion(1, 0, 0);
        
        /// <summary>
        /// Localized Command
        /// </summary>
        [JsonProperty("Command Localization")]
        public CommandLocalization Command { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        [JsonConstructor]
        public DiscordCommandLocalization() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="command">Command to create the localization for</param>
        /// <param name="lang">Oxide Lang for the command</param>
        public DiscordCommandLocalization(CommandCreate command, string lang) : this()
        {
            Command = new CommandLocalization(command, lang);
        }

        /// <summary>
        /// Apply localizations to <see cref="CommandCreate"/> for language
        /// </summary>
        /// <param name="create"></param>
        /// <param name="language"></param>
        public void ApplyCommandLocalization(CommandCreate create, string language)
        {
            if (DiscordLang.Instance.TryGetDiscordLocale(language, out string discordLocale))
            {
                language = discordLocale;
            }
            
            Command.ApplyCommandLocalization(create, language);
        }

        internal Task HandleApplyCommandLocalizationAsync(CommandCreate create, string language)
        {
            ApplyCommandLocalization(create, language);
            return Task.CompletedTask;
        }

        public TemplateVersion GetInternalVersion() => InternalVersion;
    }
}