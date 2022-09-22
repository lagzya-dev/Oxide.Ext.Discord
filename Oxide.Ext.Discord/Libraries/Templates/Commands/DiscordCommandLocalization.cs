using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Libraries.Templates.Commands
{
    /// <summary>
    /// Command Localizations for Application Commands
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordCommandLocalization : BaseTemplate
    {
        /// <summary>
        /// Localized Command
        /// </summary>
        [JsonProperty("Command Localization")]
        public CommandLocalization Command { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        [JsonConstructor]
        public DiscordCommandLocalization() : base(TemplateType.Command, new TemplateVersion(1, 0, 0)) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="command"></param>
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
            if (DiscordExtension.DiscordLang.TryGetDiscordLocale(language, out string discordLocale))
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
    }
}