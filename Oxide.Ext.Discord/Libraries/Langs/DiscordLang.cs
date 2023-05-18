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
using Oxide.Ext.Discord.Plugins;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Langs
{
    /// <summary>
    /// Converts discord locale codes into oxide locale codes
    /// </summary>
    public class DiscordLang : BaseDiscordLibrary<DiscordLang>
    {
        /// <summary>
        /// Default Oxide Lang (English)
        /// </summary>
        public const string DefaultOxideLanguage = "en";
        
        /// <summary>
        /// Returns the Oxide Server language
        /// </summary>
        public string GameServerLanguage => _lang.GetServerLanguage();
        
        private readonly Hash<string, string> _discordToOxide = new Hash<string, string>();
        private readonly Hash<string, string> _oxideToDiscord = new Hash<string, string>();
        private readonly Hash<LangId, Hash<string, string>> _pluginLangCache = new Hash<LangId, Hash<string, string>>();
        
        private readonly Lang _lang = Interface.Oxide.GetLibrary<Lang>();
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        internal DiscordLang(ILogger logger)
        {
            _logger = logger;
            AddLocale("en","en-US");
            AddLocale("bg","bg");
            AddLocale("zh","zh-CN");
            AddLocale("hr","hr");
            AddLocale("cs","cs");
            AddLocale("da","da");
            AddLocale("id","id-Id");
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
            
            _discordToOxide["en-GB"] = "en";
            _discordToOxide["zh-TW"] = "zh";
        }

        private void AddLocale(string oxide, string discord)
        {
            _discordToOxide[discord] = oxide;
            _oxideToDiscord[oxide] = discord;
        }

        /// <summary>
        /// Returns the oxide locale for a given discord locale
        /// </summary>
        /// <param name="discordLocale">Discord locale to get oxide locale for</param>
        /// <returns>Oxide locale if it exists; null otherwise</returns>
        public string GetOxideLanguage(string discordLocale)
        {
            return _discordToOxide[discordLocale];
        }
        
        /// <summary>
        /// Tries to get the oxide language from a discord locale
        /// </summary>
        /// <param name="discordLocale">Discord locale to get oxide language from</param>
        /// <param name="oxideLanguage">Oxide Language if found</param>
        /// <returns>True if locale exists; false otherwise</returns>
        public bool TryGetOxideLanguage(string discordLocale, out string oxideLanguage) => _discordToOxide.TryGetValue(discordLocale, out oxideLanguage);

        /// <summary>
        /// Returns the discord locale for a given oxide locale
        /// </summary>
        /// <param name="oxideLanguage">oxide locale to get discord locale for</param>
        /// <returns>Discord locale if it exists; null otherwise</returns>
        public string GetDiscordLocale(string oxideLanguage) => _oxideToDiscord[oxideLanguage];

        /// <summary>
        /// Tries to get the discord locale from a oxide language
        /// </summary>
        /// <param name="oxideLanguage">Oxide Language to get discord locale from</param>
        /// <param name="discordLocale">Discord locale if found</param>
        /// <returns>True if locale exists; false otherwise</returns>
        public bool TryGetDiscordLocale(string oxideLanguage, out string discordLocale) => _oxideToDiscord.TryGetValue(oxideLanguage, out discordLocale);

        /// <summary>
        /// Returns the oxide locale for the given IPlayer
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/> to get the locale for</param>
        /// <returns>Locale for the given IPlayer</returns>
        public string GetPlayerLanguage(IPlayer player) => GetPlayerLanguage(player?.Id);

        /// <summary>
        /// Returns the oxide locale for the given playerId
        /// </summary>
        /// <param name="playerId">PlayerId to get the locale for</param>
        /// <returns>Locale for the given playerId</returns>
        public string GetPlayerLanguage(string playerId) => _lang.GetLanguage(playerId);

        /// <summary>
        /// Returns the discord localization for a plugins oxide lang.
        /// This is used for application command localization
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="langKey"></param>
        /// <returns></returns>
        [Obsolete("GetCommandLocalization has been deprecated and will be removed in the future. Please upgrade to DiscordCommandLocalizations for Application Command localization")]
        public Hash<string, string> GetCommandLocalization(Plugin plugin, string langKey)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (langKey == null) throw new ArgumentNullException(nameof(langKey));
            
            Hash<string, string> localization = new Hash<string, string>();
            string[] languages = _lang.GetLanguages(plugin);
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
        [Obsolete("This feature is deprecated and will be removed in the future. Please switch to Discord Templates instead.")]
        public string GetDiscordInteractionLangMessage(Plugin plugin, DiscordInteraction interaction, string langKey)
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
                             ?? (player != null ? GetLanguageMessages(plugin, _lang.GetLanguage(player.Id))?[langKey] : null)
                             ?? GetLanguageMessages(plugin, GetOxideLanguage(interaction.GuildLocale))?[langKey]
                             ?? GetLanguageMessages(plugin, _lang.GetServerLanguage())?[langKey]
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
        [Obsolete("This feature is deprecated and will be removed in the future. Please switch to Discord Templates instead.")]
        public string GetDiscordInteractionLangMessage(Plugin plugin, DiscordInteraction interaction, string langKey, params object[] args)
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

        private Hash<string, string> GetLanguageMessages(Plugin plugin, string language)
        {
            LangId id = new LangId(plugin, language);
            Hash<string, string> langCache = _pluginLangCache[id];
            
            if (langCache == null)
            {
                langCache = new Hash<string, string>();
                _pluginLangCache[id] = langCache;
                foreach (KeyValuePair<string, string> lang in _lang.GetMessages(language, plugin))
                {
                    langCache[lang.Key] = lang.Value;
                }
            }

            return langCache;
        }

        ///<inheritdoc/>
        protected override void OnPluginLoaded(Plugin plugin) { }

        ///<inheritdoc/>
        protected override void OnPluginUnloaded(Plugin plugin)
        {
            PluginId pluginId = plugin.Id();
            _pluginLangCache.RemoveAll(p => pluginId == p.PluginId);
        }
    }
}