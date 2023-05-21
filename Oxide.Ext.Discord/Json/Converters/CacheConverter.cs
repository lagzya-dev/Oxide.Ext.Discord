using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Interfaces;

namespace Oxide.Ext.Discord.Json.Converters
{
    internal class CacheConverter<T> : JsonConverter where T : class, IDiscordCacheable, new()
    {
        private readonly Type _type = typeof(T);
        
        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            Snowflake id = new Snowflake(obj.Value<string>("id"));
            T entity = EntityCache<T>.Instance.GetOrCreate(id);
            using (JsonReader objReader = obj.CreateReader())
            {
                serializer.Populate(objReader, entity);
                return entity;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();
        

        public override bool CanConvert(Type objectType) => objectType == _type;
    }
}