using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities;

namespace Oxide.Ext.Discord.Helpers.Converters
{
    /// <summary>
    /// Converts a snowflake to and from it's JSON string value
    /// </summary>
    public class SnowflakeConverter : JsonConverter
    {
        /// <summary>
        /// Reads the JSON string and converts it to a snowflake
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    return new Snowflake(ulong.Parse(reader.Value.ToString()));

                case JsonToken.String:
                    if (Snowflake.TryParse(reader.Value.ToString(), out Snowflake snowflake))
                    {
                        return snowflake;
                    }

                    throw new JsonException("Snowflake string JSON token failed to parse to snowflake");
                
                default:
                    throw new JsonException("Token type does not match snowflake valid types of string or integer");
            }
        }
        
        /// <summary>
        /// Writes a snowflake as a JSON string
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Snowflake) value).ToString());
        }

        /// <summary>
        /// Returns if we can convert this type
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Snowflake);
        }
    }
}