using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities.Guilds.Onboarding
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#guild-onboarding-object-prompt-types">Prompt Types</a>
    /// </summary>
    public enum OnboardingPromptType
    {
        /// <summary>
        /// Multiple Choice Prompt Type
        /// </summary>
        [Description("MULTIPLE_CHOICE")]
        MultipleChoice = 0,
        
        /// <summary>
        /// Dropdown Prompt Type
        /// </summary>
        [Description("DROPDOWN")]
        Dropdown = 1,
    }
}