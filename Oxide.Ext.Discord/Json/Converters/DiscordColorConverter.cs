using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Permissions;

namespace Oxide.Ext.Discord.Json.Converters
{
    /// <summary>
    /// Handles the JSON Serialization / Deserialization for DiscordColor
    /// </summary>
    public class DiscordColorConverter : JsonConverter
    {
        /// <summary>
        /// Writes to JSON
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DiscordColor color = (DiscordColor) value;
            writer.WriteValue(color.Color);
        }

        /// <summary>
        /// Reads from JSON
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                if (!IsNullable(objectType))
                {
                    throw new JsonException($"Cannot convert null value to {objectType}. Path: {reader.Path}");
                }

                return null;
            }

            if (reader.TokenType == JsonToken.Integer)
            {
                return new DiscordColor(uint.Parse(reader.Value.ToString()));
            }
            
            throw new JsonException($"Unexpected token {reader.TokenType} when parsing discord color. Path: {reader.Path}");
        }

        /// <summary>
        /// Check if can convert
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            return objectType != null && (IsNullable(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType) == typeof(DiscordColor);
        }
        
        private bool IsNullable(Type objectType)
        {
            return objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}