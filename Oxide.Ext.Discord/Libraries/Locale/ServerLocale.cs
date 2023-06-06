using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Json.Converters;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.Locale
{
    [JsonConverter(typeof(ServerLocaleConverter))]
    public struct ServerLocale : IEquatable<ServerLocale>
    {
        public readonly string Id;
        public bool IsValid => !string.IsNullOrEmpty(Id);
        public DiscordLocale GetDiscordLocale() => DiscordLocales.Instance.GetDiscordLocale(this);

        public static readonly ServerLocale Default = new ServerLocale(DiscordLocales.DefaultServerLanguage); 
        
        private ServerLocale(string id) 
        {
            Id = id;
        }
        
        public static ServerLocale Parse(string locale)
        {
            ServerLocale serverLocale = new ServerLocale(locale);
            if (!DiscordLocales.Instance.Contains(serverLocale))
            {
                DiscordExtension.GlobalLogger.Warning("Parsed ServerLocale '{0}' which does not exist in DiscordLang. " +
                                                      "Please give this message to the Discord Extension Authors", locale);
            }

            return serverLocale;
        }

        internal static ServerLocale Create(string locale) => new ServerLocale(locale);

        public bool Equals(ServerLocale other) => Id == other.Id;

        public override bool Equals(object obj) => obj is ServerLocale other && Equals(other);

        public override int GetHashCode() => Id != null ? Id.GetHashCode() : 0;

        public static bool operator == (ServerLocale left, ServerLocale right) => left.Equals(right);
        
        public static bool operator != (ServerLocale left, ServerLocale right) => !(left == right);

        public override string ToString() => Id;
    }
}