using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Json.Converters
{
    /// <summary>
    /// Json Converter for <see cref="DiscordUser"/> Returns the user from the <see cref="DiscordUserCache"/> or Caches a new user.
    /// </summary>
    public class DiscordUserConverter : JsonConverter
    {
        /// <summary>
        /// We do not write with this converter
        /// </summary>
        public override bool CanWrite => false;

        /// <summary>
        /// We do not write with this converter
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the JSON and populates it on a cached <see cref="DiscordUser"/> or Caches a new DiscordUser
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            obj.ToObject<DiscordUser>();
            DiscordUser user = new DiscordUser();
            serializer.Populate(reader, user);
            return DiscordUserCache.Instance.GetOrCreate(user);
        }

        /// <summary>
        /// Returns if this converts is for this type
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(DiscordUser) == objectType;
        }
    }
}