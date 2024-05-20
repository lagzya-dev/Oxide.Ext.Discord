using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Json;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries
{
    /// <summary>
    /// Represents a Server Locale
    /// </summary>
    [JsonConverter(typeof(ServerLocaleConverter))]
    public struct ServerLocale : IEquatable<ServerLocale>
    {
        /// <summary>
        /// ID of the Locale
        /// </summary>
        public readonly string Id;
        
        /// <summary>
        /// Returns if the Locale is valid
        /// </summary>
        public bool IsValid => !string.IsNullOrEmpty(Id);
        
        /// <summary>
        /// Returns if the Locale is the default server language "en"
        /// </summary>
        public bool IsDefault => IsValid && this == Default;
        
        /// <summary>
        /// Returns the <see cref="DiscordLocale"/> for this server locale
        /// </summary>
        /// <returns></returns>
        public DiscordLocale GetDiscordLocale() => DiscordLocales.Instance.GetDiscordLocale(this);
        
        /// <summary>
        /// The default locale for servers
        /// </summary>
        public static readonly ServerLocale Default = new ServerLocale(DiscordLocales.DefaultServerLanguage);

        private static DateTime _lastError;
        private static readonly List<string> LocaleError = new List<string>();
        
        private ServerLocale(string id) 
        {
            Id = id;
        }
        
        /// <summary>
        /// Parses a locale returning a <see cref="ServerLocale"/>
        /// </summary>
        /// <param name="locale"></param>
        /// <returns></returns>
        public static ServerLocale Parse(string locale)
        {
            ServerLocale serverLocale = new ServerLocale(locale);
            if (!DiscordLocales.Instance.Contains(serverLocale))
            {
                if (!LocaleError.Contains(locale) || _lastError + TimeSpan.FromMinutes(5) < DateTime.UtcNow)
                {
                    LocaleError.Remove(locale);
                    LocaleError.Add(locale);
                    _lastError = DateTime.UtcNow;
                    DiscordExtension.GlobalLogger.Warning("Parsed ServerLocale '{0}' which does not exist in DiscordLang. " +
                                                          "Please give this message to the Discord Extension Authors", locale);
                }

            }

            return serverLocale;
        }

        internal static ServerLocale Create(string locale) => new ServerLocale(locale);

        ///<inheritdoc/>
        public bool Equals(ServerLocale other) => Id == other.Id;

        ///<inheritdoc/>
        public override bool Equals(object obj) => obj is ServerLocale other && Equals(other);

        ///<inheritdoc/>
        public override int GetHashCode() => Id != null ? Id.GetHashCode() : 0;
        
        /// <summary>
        /// Returns if two Server Locales are equal to each other
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator == (ServerLocale left, ServerLocale right) => left.Equals(right);
        
        /// <summary>
        /// Returns if two Server Locales are not equal to each other
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator != (ServerLocale left, ServerLocale right) => !(left == right);

        /// <summary>
        /// Returns the ID of the ServerLocale
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Id;
    }
}