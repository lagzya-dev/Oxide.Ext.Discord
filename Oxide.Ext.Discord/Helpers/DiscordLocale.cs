using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Helpers
{
    /// <summary>
    /// Converts discord locale codes into oxide locale codes
    /// </summary>
    public static class DiscordLocale
    {
        /// <summary>
        /// Default Oxide Lang (English)
        /// </summary>
        public const string DefaultOxideLanguage = "en";
        
        /// <summary>
        /// Returns the Oxide Server language
        /// </summary>
        public static string GameServerLanguage => Lang.GetServerLanguage();
        
        private static readonly Hash<string, string> DiscordToOxide = new Hash<string, string>();
        private static readonly Hash<string, string> OxideToDiscord = new Hash<string, string>();
        private static readonly Hash<string, Hash<string, Hash<string, string>>> PluginLangCache = new Hash<string, Hash<string, Hash<string, string>>>();
        
        private static readonly Lang Lang = Interface.Oxide.GetLibrary<Lang>();

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
        public static string GetOxideLanguage(string discordLocale)
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
        /// Returns the oxide locale for the given IPlayer
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/> to get the locale for</param>
        /// <returns>Locale for the given IPlayer</returns>
        public static string GetPlayerLanguage(IPlayer player)
        {
            return GetPlayerLanguage(player?.Id);
        }
        
        /// <summary>
        /// Returns the oxide locale for the given playerId
        /// </summary>
        /// <param name="playerId">PlayerId to get the locale for</param>
        /// <returns>Locale for the given playerId</returns>
        public static string GetPlayerLanguage(string playerId)
        {
            return Lang.GetLanguage(playerId);
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
                    DiscordExtension.GlobalLogger.Warning("Discord Extension failed to find discord locale for oxide language '{0}' for '{1}'. Please give this message to the Discord Extension Authors", language, plugin.FullName());
                    continue;
                }

                Hash<string, string> messages = GetLanguageMessages(plugin, language);
                if (!messages.ContainsKey(langKey))
                {
                    DiscordExtension.GlobalLogger.Warning("Failed to add localized message for lang key '{0}' for plugin '{1} because lang key doesn't exist for language {2}", langKey, plugin.FullName(), language);
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

            IPlayer player = interaction.User.Player;
            
            //Retrieves the plugin lang messages. If the messages are not found for a language then it will check in the following order
            // 1. Interaction.Locale - Discord User's Locale
            // 2. Oxide IPlayer Locale
            // 3. Interaction.GuildLocale - Application Command Guild Locale
            // 4. Oxide Lang Language
            // 5. English
            string message = GetLanguageMessages(plugin, GetOxideLanguage(interaction.Locale))?[langKey]
                             ?? (player != null ? GetLanguageMessages(plugin, Lang.GetLanguage(player.Id))?[langKey] : null)
                             ?? GetLanguageMessages(plugin, GetOxideLanguage(interaction.GuildLocale))?[langKey]
                             ?? GetLanguageMessages(plugin, Lang.GetServerLanguage())?[langKey]
                             ?? GetLanguageMessages(plugin, DefaultOxideLanguage)?[langKey];

            return !string.IsNullOrEmpty(message) ? message : langKey;
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

        private static Hash<string, string> GetLanguageMessages(Plugin plugin, string language)
        {
            Hash<string, Hash<string, string>> pluginCache = PluginLangCache[plugin.Id()];
            if (pluginCache == null)
            {
                pluginCache = new Hash<string, Hash<string, string>>();
                PluginLangCache[plugin.Id()] = pluginCache;
            }

            Hash<string, string> langCache = pluginCache[language];
            if (langCache == null)
            {
                langCache = new Hash<string, string>();
                pluginCache[language] = langCache;
                foreach (KeyValuePair<string, string> lang in Lang.GetMessages(language, plugin))
                {
                    langCache[lang.Key] = lang.Value;
                }
            }

            return langCache;
        }

        internal static void OnPluginUnloaded(Plugin plugin)
        {
            PluginLangCache.Remove(plugin.Id());
        }
    }
}