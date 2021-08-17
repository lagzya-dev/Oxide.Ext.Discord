using System;

namespace Oxide.Ext.Discord.Entities.Users
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/user#user-object-user-flags">User Flags</a>
    /// </summary>
    [Flags]
    public enum UserFlags
    {
        /// <summary>
        /// Default value for flags, when none are given to an account.
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Flag given to users who are a Discord employee.
        /// </summary>
        DiscordEmployee = 1 << 0,
        
        /// <summary>
        /// Flag given to users who are owners of a partnered Discord server.
        /// </summary>
        PartneredServerOwner = 1 << 1,
        
        /// <summary>
        /// Flag given to users in HypeSquad events.
        /// </summary>
        HyperSquadEvents = 1 << 2,
        
        /// <summary>
        /// Flag given to users who have participated in the 𝐁ug report program and are level 1.
        /// </summary>
        BugHunterLevel1 = 1 << 3,
        
        /// <summary>
        /// Flag given to users who are in the HypeSquad House of Bravery.
        /// </summary>
        HouseBravery = 1 << 6,
        
        /// <summary>
        /// Flag given to users who are in the HypeSquad House of Brilliance.
        /// </summary>
        HouseBrilliance = 1 << 7,
        
        /// <summary>
        /// Flag given to users who are in the HypeSquad House of Balance.
        /// </summary>
        HouseBalance = 1 << 8,
        
        /// <summary>
        /// Flag given to users who subscribed to Nitro before games were added.
        /// </summary>
        EarlySupporter = 1 << 9,
        
        /// <summary>
        /// Flag given to users who are part of a team.
        /// </summary>
        TeamUser = 1 << 10,

        /// <summary>
        /// Flag given to users who have participated in the 𝐁ug report program and are level 2.
        /// </summary>
        BugHunterLevel2 = 1 << 14,
        
        /// <summary>
        /// Flag given to users who are verified bots.
        /// </summary>
        VerifiedBot = 1 << 16,
        
        /// <summary>
        ///  Flag given to users that developed bots and early verified their accounts.
        /// </summary>
        EarlyVerifiedBotDeveloper = 1 << 17,
        
        /// <summary>
        ///  Flag given to users that are discord certified moderators
        /// </summary>
        DiscordCertifiedModerator = 1 << 18
    }
}