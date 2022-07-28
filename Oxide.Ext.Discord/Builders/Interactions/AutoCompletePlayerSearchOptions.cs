using System;

namespace Oxide.Ext.Discord.Builders.Interactions
{
    /// <summary>
    /// AutoComplete Player Search Options for <see cref="InteractionAutoCompleteBuilder"/>
    /// </summary>
    [Flags]
    public enum AutoCompletePlayerSearchOptions : byte
    {
        /// <summary>
        /// Defaults search options
        /// </summary>
        Default = 0,
        
        /// <summary>
        /// Include Clan Name in search
        /// </summary>
        IncludeClanName = 1 << 0,
        
        /// <summary>
        /// Include Steam ID
        /// </summary>
        IncludeSteamId = 1 << 1
    }
}