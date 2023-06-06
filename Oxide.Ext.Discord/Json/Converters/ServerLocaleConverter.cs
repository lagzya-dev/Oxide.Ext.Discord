using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Libraries.Locale;
using Oxide.Ext.Discord.Libraries.Templates;

namespace Oxide.Ext.Discord.Json.Converters
{
    internal class ServerLocaleConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                    string value = reader.Value.ToString();
                    return !string.IsNullOrEmpty(value) ? ServerLocale.Parse(value) : default(ServerLocale);

                case JsonToken.Null:
                    return Nullable.GetUnderlyingType(objectType) != null ? (object)null : default(ServerLocale);

                default:
                    throw new JsonException($"Token type {reader.TokenType} does not match ServerLocale valid types of string. Path: {reader.Path}");
            }
        }

        public override bool CanConvert(Type objectType) => typeof(ServerLocale) == objectType;
    }
}