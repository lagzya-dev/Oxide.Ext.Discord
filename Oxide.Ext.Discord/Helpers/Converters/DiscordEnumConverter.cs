using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Oxide.Ext.Discord.Helpers.Converters
{
    /// <summary>
    /// Handles deserializing JSON values as strings. If they value doesn't exist return the default value.
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    public class DiscordEnumConverter<TEnum> : StringEnumConverter where TEnum : struct, IConvertible
    {
        private readonly TEnum _defaultValue;

        /// <summary>
        /// Constructor that takes teh defaultValue to return if enum value is not found.
        /// </summary>
        /// <param name="defaultValue"></param>
        public DiscordEnumConverter(TEnum defaultValue)
        {
            _defaultValue = defaultValue;
        }
        
        /// <summary>
        /// Use StringEnumConverter to parse enum value if that fails return default value.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch
            {
                try
                {
                    DiscordExtension.GlobalLogger.Debug($"[{typeof(DiscordEnumConverter<TEnum>)}] Does not support enum value {reader.Value}");
                }
                catch (Exception)
                {
                    
                }
                
                return _defaultValue;
            }
        }
    }
}