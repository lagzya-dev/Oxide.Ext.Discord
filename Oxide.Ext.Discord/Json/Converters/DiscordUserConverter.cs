using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Json.Converters
{
    public class DiscordUserConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DiscordUser jsonUser = new DiscordUser();
            serializer.Populate(reader, jsonUser);
            return DiscordUserCache.GetOrCreate(jsonUser);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DiscordUser) == objectType;
        }
    }
}