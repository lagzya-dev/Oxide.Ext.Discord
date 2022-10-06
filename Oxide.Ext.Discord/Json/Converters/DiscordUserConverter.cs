using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Pooling.Entities;

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
            PooledDiscordUser pooledUser = DiscordPool.Get<PooledDiscordUser>();
            serializer.Populate(reader, pooledUser);
            DiscordUser user = DiscordUserCache.Instance.GetOrCreate(pooledUser);
            pooledUser.Dispose();
            return user;
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