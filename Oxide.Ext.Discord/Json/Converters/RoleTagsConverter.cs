using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Permissions;

namespace Oxide.Ext.Discord.Json.Converters
{
    public class RoleTagsConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            RoleTags tags = new RoleTags
            {
                BotId = obj["bot_id"].ToObject<Snowflake?>(),
                IntegrationId = obj["integration_id"].ToObject<Snowflake?>(),
                SubscriptionListingId = obj["subscription_listing_id"].ToObject<Snowflake?>(),
                PremiumSubscriber = obj["premium_subscriber"] != null,
                AvailableForPurchase = obj["available_for_purchase"] != null
            };

            return tags;
        }

        public override bool CanConvert(Type objectType) => typeof(RoleTags) == objectType;
    }
}