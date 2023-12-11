namespace Oxide.Ext.Discord.Entities.Monetization.Skus
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/monetization/skus#sku-object-sku-types">Discord SKU Types</a>
    /// </summary>
    public enum DiscordSkuType
    {
        /// <summary>
        /// Represents a recurring subscription
        /// </summary>
        Subscription = 5,
        
        /// <summary>
        /// System-generated group for each SUBSCRIPTION SKU created
        /// </summary>
        SubscriptionGroup = 6
    }
}