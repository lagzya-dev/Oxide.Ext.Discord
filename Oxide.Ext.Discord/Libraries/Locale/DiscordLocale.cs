using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Json.Converters;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.Locale
{
    [JsonConverter(typeof(DiscordLocaleConverter))]
    public struct DiscordLocale : IEquatable<DiscordLocale>
    {
        public readonly string Id;
        public bool IsValid => !string.IsNullOrEmpty(Id);

        public ServerLocale GetServerLocale() => DiscordLocales.Instance.GetServerLanguage(this);
        
        private DiscordLocale(string id) 
        {
            Id = id;
        }

        public static DiscordLocale Parse(string locale)
        {
            DiscordLocale discordLocale = new DiscordLocale(locale);
            if (!DiscordLocales.Instance.Contains(discordLocale))
            {
                DiscordExtension.GlobalLogger.Warning("Parsed DiscordLocale '{0}' which does not exist in DiscordLang. " +
                                                      "Please give this message to the Discord Extension Authors", locale);
            }

            return discordLocale;
        }

        internal static DiscordLocale Create(string locale) => new DiscordLocale(locale);
        
        public bool Equals(DiscordLocale other) => Id == other.Id;

        public override bool Equals(object obj) => obj is DiscordLocale other && Equals(other);

        public override int GetHashCode() => Id != null ? Id.GetHashCode() : 0;

        public static bool operator == (DiscordLocale left, DiscordLocale right) => left.Equals(right);
        
        public static bool operator != (DiscordLocale left, DiscordLocale right) => !(left == right);
        
        public override string ToString() => Id;
    }
}