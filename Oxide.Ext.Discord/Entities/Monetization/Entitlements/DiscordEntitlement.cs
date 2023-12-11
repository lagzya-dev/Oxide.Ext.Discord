using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Clients;
using Oxide.Ext.Discord.Exceptions.Entities;
using Oxide.Ext.Discord.Interfaces.Promises;

namespace Oxide.Ext.Discord.Entities.Monetization.Entitlements
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/monetization/entitlements#entitlement-object-entitlement-structure">Entitlement Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordEntitlement
    {
        /// <summary>
        /// ID of the entitlement
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// ID of the SKU
        /// </summary>
        [JsonProperty("sku_id")]
        public Snowflake SkuId { get; set; }
        
        /// <summary>
        /// ID of the user that is granted access to the entitlement's sku
        /// </summary>
        [JsonProperty("user_id")]
        public Snowflake? UserId { get; set; }
        
        /// <summary>
        /// ID of the guild that is granted access to the entitlement's sku
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake? GuildId { get; set; }
        
        /// <summary>
        /// ID of the parent application
        /// </summary>
        [JsonProperty("application_id")]
        public Snowflake ApplicationId { get; set; }
        
        /// <summary>
        ///  	Type of entitlement
        /// </summary>
        [JsonProperty("type")]
        public EntitlementType Type { get; set; }
        
        /// <summary>
        /// Not applicable for App Subscriptions. Subscriptions are not consumed and will be false
        /// </summary>
        [JsonProperty("consumed")]
        public bool Consumed { get; set; }
        
        /// <summary>
        /// Start date at which the entitlement is valid. Not present when using test entitlements.
        /// </summary>
        [JsonProperty("starts_at")]
        public DateTime StartsAt { get; set; }
        
        /// <summary>
        /// Date at which the entitlement is no longer valid. Not present when using test entitlements.
        /// </summary>
        [JsonProperty("ends_at")]
        public DateTime EndsAt { get; set; }

        /// <summary>
        /// Returns all entitlements for a given app, active and expired.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="applicationId">Application ID to get entitlement for</param>
        /// <param name="getEntitlements">Query string options for the request</param>
        public static IPromise<List<DiscordEntitlement>> GetEntitlements(DiscordClient client, Snowflake applicationId, GetEntitlements getEntitlements = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(applicationId, true, nameof(applicationId));
            return client.Bot.Rest.Get<List<DiscordEntitlement>>(client, $"applications/{applicationId}/application-object/entitlements{getEntitlements?.ToQueryString()}");
        }

        /// <summary>
        /// Creates a test entitlement to a given SKU for a given guild or user. Discord will act as though that user or guild has entitlement to your premium offering.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="create">Create request</param>
        public IPromise<DiscordEntitlement> CreateTestEntitlement(DiscordClient client, CreateTestEntitlement create)
        {
            return client.Bot.Rest.Post<DiscordEntitlement>(client, $"applications/{ApplicationId}/entitlements", create);
        }

        /// <summary>
        /// Deletes a currently-active test entitlement. Discord will act as though that user or guild no longer has entitlement to your premium offering.
        /// </summary>
        /// <param name="client">Client to use</param>
        public IPromise DeleteTestEntitlement(DiscordClient client)
        {
            return client.Bot.Rest.Delete(client, $"/applications/{ApplicationId}/entitlements/{Id}");
        }
    }
}