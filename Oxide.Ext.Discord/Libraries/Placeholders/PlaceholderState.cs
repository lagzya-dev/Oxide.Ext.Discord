using System.Text.RegularExpressions;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Libraries.Placeholders
{
    /// <summary>
    /// Represents the current state for a matched placeholder
    /// </summary>
    public class PlaceholderState : BasePoolable
    {
        /// <summary>
        /// Placeholder Data for the state
        /// </summary>
        public PlaceholderData Data;
        
        /// <summary>
        /// Name of the placeholder
        /// </summary>
        public string Name;
        
        /// <summary>
        /// Format specified in the placeholder
        /// </summary>
        public string Format;
        
        /// <summary>
        /// Index in the string of the placeholder
        /// </summary>
        public ushort Index;
        
        /// <summary>
        /// Length of the placeholder
        /// </summary>
        public ushort Length;

        /// <summary>
        /// Creates a pooled <see cref="PlaceholderState"/>
        /// </summary>
        /// <param name="data">Data to be used in the state</param>
        /// <returns></returns>
        internal static PlaceholderState Create(PlaceholderData data)
        {
            PlaceholderState state = DiscordPool.Internal.Get<PlaceholderState>();
            state.Init(data);
            return state;
        }

        private void Init(PlaceholderData data) 
        {
            Data = data;
        }

        internal void UpdateState(Match match)
        {
            GroupCollection groups = match.Groups;
            Name = groups[1].Value;
            Group formatGroup = groups[2];
            Format = formatGroup.Success ? formatGroup.Value : null;
            Index = (ushort)match.Index;
            Length = (ushort)match.Length;
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            Data = null;
            Name = null;
            Format = null;
            Index = default(ushort);
            Length = default(ushort);
        }
    }
}