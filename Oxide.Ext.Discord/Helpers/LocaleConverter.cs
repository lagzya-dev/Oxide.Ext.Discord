using System.Collections.Generic;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Helpers
{
    /// <summary>
    /// Converts discord locale codes into oxide locale codes
    /// </summary>
    public static class LocaleConverter
    {
        private static readonly Hash<string, string> DiscordToOxide = new Hash<string, string>();
        private static readonly Hash<string, string> OxideToDiscord = new Hash<string, string>();
        
        private static Lang _lang;
        private static Lang Lang => _lang ?? (_lang = Interface.Oxide.GetLibrary<Lang>());

        static LocaleConverter()
        {
            AddLocale("en","en-US");
            AddLocale("bg","bg");
            AddLocale("zh","zh-CN");
            AddLocale("hr","hr");
            AddLocale("cs","cs");
            AddLocale("da","da");
            AddLocale("nl","nl");
            AddLocale("fi","fi");
            AddLocale("fr","fr");
            AddLocale("de","de");
            AddLocale("el","el");
            AddLocale("hi","hi");
            AddLocale("hu","hu");
            AddLocale("it","it");
            AddLocale("ja","ja");
            AddLocale("ko","ko");
            AddLocale("lt","lt");
            AddLocale("no","no");
            AddLocale("pl","pl");
            AddLocale("pt","pt-BR");
            AddLocale("ro","ro");
            AddLocale("ru","ru");
            AddLocale("es","es-ES");
            AddLocale("sv","sv-SE");
            AddLocale("th","th");
            AddLocale("tr","tr");
            AddLocale("uk","uk");
            AddLocale("vi","vi");
            
            DiscordToOxide["en-GB"] = "en";
            DiscordToOxide["zh-TW"] = "zh";
        }

        private static void AddLocale(string oxide, string discord)
        {
            DiscordToOxide[discord] = oxide;
            OxideToDiscord[oxide] = discord;
        }

        /// <summary>
        /// Returns the oxide locale for a given discord locale
        /// </summary>
        /// <param name="discordLocale">Discord locale to get oxide locale for</param>
        /// <returns>Oxide locale if it exists; null otherwise</returns>
        public static string GetOxideLocale(string discordLocale)
        {
            return !string.IsNullOrEmpty(discordLocale) ? DiscordToOxide[discordLocale] : string.Empty;
        }
        
        /// <summary>
        /// Returns the discord locale for a given oxide locale
        /// </summary>
        /// <param name="oxideLocale">oxide locale to get discord locale for</param>
        /// <returns>Discord locale if it exists; null otherwise</returns>
        public static string GetDiscordLocale(string oxideLocale)
        {
            return !string.IsNullOrEmpty(oxideLocale) ? OxideToDiscord[oxideLocale] : string.Empty;
        }

        /// <summary>
        /// Returns the discord localization for a plugins oxide lang.
        /// This is used for application command localization
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="langKey"></param>
        /// <returns></returns>
        public static Hash<string, string> GetCommandLocalization(Plugin plugin, string langKey)
        {
            Hash<string, string> localization = new Hash<string, string>();
            string[] languages = Lang.GetLanguages(plugin);
            for (int index = 0; index < languages.Length; index++)
            {
                string language = languages[index];
                string discordLocale = GetDiscordLocale(language);
                if (string.IsNullOrEmpty(discordLocale))
                {
                    DiscordExtension.GlobalLogger.Warning("Discord Extension failed to find discord locale for oxide language '{0}' for '{1}'. Please give this message to the Discord Extension Authors", language, plugin.Name);
                    continue;
                }

                Dictionary<string, string> messages = Lang.GetMessages(language, plugin);
                if (!messages.ContainsKey(langKey))
                {
                    DiscordExtension.GlobalLogger.Warning("Failed to add localized message for lang key '{0}' for plugin '{1} because lang key doesn't exist for language {2}", langKey, plugin.Name, language);
                    continue;
                }

                string message = messages[langKey];
                localization[discordLocale] = message;
            }

            return localization;
        }
    }
}