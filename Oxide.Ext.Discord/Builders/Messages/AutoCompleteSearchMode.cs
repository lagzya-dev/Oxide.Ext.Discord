namespace Oxide.Ext.Discord.Builders.Messages
{
    /// <summary>
    /// AutoComplete Search Mode for <see cref="InteractionAutoCompleteBuilder"/>
    /// </summary>
    public enum AutoCompleteSearchMode
    {
        /// <summary>
        /// Filter using StartsWith
        /// </summary>
        StartsWith,
        
        /// <summary>
        /// Filter using Contains
        /// </summary>
        Contains,
        
        /// <summary>
        /// Filter using EndsWith
        /// </summary>
        EndsWith
    }
}