using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities;

namespace Oxide.Ext.Discord.Helpers.Converters
{
    public class SnowflakeConverter : JsonConverter
    {
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

                    throw new JsonException();
                
                default:
                    throw new JsonException();
            }
        }
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Snowflake) value).ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Snowflake);
        }
    }
}