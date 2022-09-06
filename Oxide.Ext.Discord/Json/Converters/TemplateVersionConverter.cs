using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Libraries.Templates;

namespace Oxide.Ext.Discord.Json.Converters
{
    internal class TemplateVersionConverter : JsonConverter
    {
        private static readonly Regex VersionRegex = new Regex(@"^([\d]+).([\d]+).([\d]+)$", RegexOptions.Compiled);
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = reader.Value.ToString();
            Match match = VersionRegex.Match(value);
            if (!match.Success)
            {
                throw new JsonSerializationException($"{value} is not a valid template version for {reader.Path}.");
            }

            if (!ushort.TryParse(match.Groups[1].Value, out ushort major))
            {
                throw new JsonSerializationException($"{match.Groups[1].Value} is not a valid major template version number for {reader.Path}.");
            }
            
            if (!ushort.TryParse(match.Groups[2].Value, out ushort minor))
            {
                throw new JsonSerializationException($"{match.Groups[2].Value} is not a valid minor template version number for {reader.Path}.");
            }
            
            if (!ushort.TryParse(match.Groups[3].Value, out ushort revision))
            {
                throw new JsonSerializationException($"{match.Groups[3].Value} is not a valid revision template version number for {reader.Path}.");
            }
            
            return new TemplateVersion(major, minor, revision);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(TemplateVersion) == objectType;
        }
    }
}