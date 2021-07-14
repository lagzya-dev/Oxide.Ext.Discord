using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;

namespace Oxide.Ext.Discord.Helpers.Converters
{
    /// <summary>
    /// Converter for list of message components
    /// </summary>
    public class MessageComponentsConverter : JsonConverter
    {
        /// <summary>
        /// Write default JSON values
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Populate the correct types in components instead of just the BaseComponent
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            List<BaseComponent> components = new List<BaseComponent>();
            if(reader.TokenType == JsonToken.StartArray)
            {
                JArray array = JArray.Load(reader);
                foreach (JToken token in array)
                {
                    MessageComponentType type = (MessageComponentType)Enum.Parse(typeof(MessageComponentType), token["type"].ToString());
                    switch (type)
                    {
                        case MessageComponentType.Button:
                            components.Add(token.ToObject<ButtonComponent>());
                            break;
                        
                        case MessageComponentType.SelectMenu:
                            components.Add(token.ToObject<SelectMenuComponent>());
                            break;
                    }
                }
            }

            return components;
        }

        /// <summary>
        /// Returns if this can convert the value
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<BaseComponent>);
        }

        /// <summary>
        /// Don't allow writing
        /// </summary>
        public override bool CanWrite => false;
    }
}