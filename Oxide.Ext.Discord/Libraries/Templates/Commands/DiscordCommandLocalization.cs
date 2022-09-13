using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Libraries.Templates.Commands
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordCommandLocalization : BaseTemplate
    {
        [JsonProperty("Command Localization")]
        public CommandLocalization Command { get; set; }

        public DiscordCommandLocalization() : base(new TemplateVersion(1, 0, 0)) { }

        public DiscordCommandLocalization(CommandCreate command) : this()
        {
            Command = new CommandLocalization(command);
        }

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