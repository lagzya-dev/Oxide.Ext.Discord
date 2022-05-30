using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Helpers
{
    /// <summary>
    /// Converts discord locale codes into oxide locale codes
    /// </summary>
    public static class DiscordLocale
    {
        private const string DefaultOxideLang = "en";
        private static readonly Hash<string, string> DiscordToOxide = new Hash<string, string>();
        private static readonly Hash<string, string> OxideToDiscord = new Hash<string, string>();
        
        private static Lang _lang;
        private static Lang Lang => _lang ?? (_lang = Interface.Oxide.GetLibrary<Lang>());

        static DiscordLocale()
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
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (langKey == null) throw new ArgumentNullException(nameof(langKey));
            
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

        /// <summary>
        /// Retrieves the lang message for a Discord Interaction
        /// </summary>
        /// <param name="plugin">Plugin the lang is from</param>
        /// <param name="interaction">The interaction to be localized</param>
        /// <param name="langKey">The lang key to lookup</param>
        /// <returns>Localized message if found; Empty string otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if any of the input arguments are null</exception>
        public static string GetDiscordInteractionLangMessage(Plugin plugin, DiscordInteraction interaction, string langKey)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (interaction == null) throw new ArgumentNullException(nameof(interaction));
            if (langKey == null) throw new ArgumentNullException(nameof(langKey));

            IPlayer player = interaction.GetUser()?.Player;
            
            //Retrieves the plugin lang messages. If the messages are not found for a language then it will check in the following order
            // 1. Interaction.Locale - Discord User's Locale
            // 2. Oxide IPlayer Locale
            // 3. Interaction.GuildLocale - Application Command Guild Locale
            // 4. Oxide Lang Language
            // 5. English
            Dictionary<string, string> messages = GetLanguageMessages(plugin, GetOxideLocale(interaction.Locale))
                                                  ?? (player != null ? GetLanguageMessages(plugin, _lang.GetLanguage(player.Id)) : null)
                                                  ?? GetLanguageMessages(plugin, GetOxideLocale(interaction.GuildLocale))
                                                  ?? GetLanguageMessages(plugin, _lang.GetServerLanguage())
                                                  ?? GetLanguageMessages(plugin, DefaultOxideLang);

            return messages == null ? langKey : messages[langKey];
        }

        /// <summary>
        /// Retrieves the lang message for a Discord Interaction
        /// </summary>
        /// <param name="plugin">Plugin the lang is from</param>
        /// <param name="interaction">The interaction to be localized</param>
        /// <param name="langKey">The lang key to lookup</param>
        /// <param name="args">Localization formatting args</param>
        /// <returns>Localized message if found; Empty string otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if any of the input arguments are null</exception>
        public static string GetDiscordInteractionLangMessage(Plugin plugin, DiscordInteraction interaction, string langKey, params object[] args)
        {
            string message = GetDiscordInteractionLangMessage(plugin, interaction, langKey);
            if (string.IsNullOrEmpty(message))
            {
                return langKey;
            }

            try
            {
                return string.Format(message, args);
            }
            catch(Exception ex)
            {
                DiscordExtension.GlobalLogger.Exception("Plugin {0} Lang Key '{1}'\nMessage:{2}\nArgs:{3}", plugin, langKey, message, string.Join(", ", args.Select(a => a.ToString()).ToArray()), ex);
                return message;
            }
        }

        private static Dictionary<string, string> GetLanguageMessages(Plugin plugin, string language)
        {
            if (!string.IsNullOrEmpty(language))
            {
                return _lang.GetMessages(language, plugin);
            }

            return null;
        }
    }
}