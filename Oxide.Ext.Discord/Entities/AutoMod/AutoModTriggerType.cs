namespace Oxide.Ext.Discord.Entities.AutoMod
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/auto-moderation#auto-moderation-rule-object-trigger-types">Auto Mod Trigger Types</a>
    /// </summary>
    public enum AutoModTriggerType
    {
        /// <summary>
        /// Check if content contains words from a user defined list of keywords
        /// </summary>
        Keyword = 1,
        
        /// <summary>
        /// Check if content contains any harmful links
        /// </summary>
        HarmfulLink = 2,
        
        /// <summary>
        /// Check if content represents generic spam
        /// </summary>
        Spam = 3,
        
        /// <summary>
        /// Check if content contains words from internal pre-defined wordsets
        /// </summary>
        KeywordPreset = 4,
    }
}