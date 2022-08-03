using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Libraries.Templates;

namespace Oxide.Ext.Discord.Json.Converters
{
    internal class TemplateVersionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new TemplateVersion(reader.Value.ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(TemplateVersion) == objectType;
        }
    }
}