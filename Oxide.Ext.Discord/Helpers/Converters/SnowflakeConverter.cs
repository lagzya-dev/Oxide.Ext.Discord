using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities;

namespace Oxide.Ext.Discord.Helpers.Converters
{
    public class SnowflakeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Snowflake) value).ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (Snowflake.TryParse(reader.Value.ToString(), out Snowflake id))
            {
                return id;
            }

            return default(Snowflake);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Snowflake);
        }
    }
}