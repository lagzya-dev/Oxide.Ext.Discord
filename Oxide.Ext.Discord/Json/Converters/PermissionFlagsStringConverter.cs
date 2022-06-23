using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Permissions;

namespace Oxide.Ext.Discord.Json.Converters
{
    /// <summary>
    /// Converts Permission Flags to and from a JSON string
    /// </summary>
    public class PermissionFlagsStringConverter : JsonConverter
    {
        /// <summary>
        /// Writes Permission Flags as a JSON string
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((ulong)value).ToString());
        }

        /// <summary>
        /// Converts the ulong JSON string to Permission Flags
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            ulong value = ulong.Parse(reader.Value.ToString());
            return (PermissionFlags)value;
        }

        /// <summary>
        /// Returns if the type equals PermissionFlags
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(PermissionFlags) == objectType;
        }
    }
}