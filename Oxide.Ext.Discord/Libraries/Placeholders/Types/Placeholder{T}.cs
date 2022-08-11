using System;
using System.Text;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Types
{
    internal class Placeholder<T> : BasePlaceholder
    {
        private readonly Action<StringBuilder, T, PlaceholderMatch> _callback;
        
        public Placeholder(string dataKey, Plugin plugin, Action<StringBuilder, T, PlaceholderMatch> callback) : base(dataKey, plugin) 
        {
            _callback = callback;
        }

        internal Placeholder(string dataKey, Action<StringBuilder, T, PlaceholderMatch> callback) : this(dataKey, null, callback) {}

        public override void Invoke(StringBuilder builder, PlaceholderMatch match, PlaceholderData data)
        {
            Invoke(builder, match, data.Get<T>(DataKey));
        }
        
        private void Invoke(StringBuilder builder, PlaceholderMatch match, T data)
        {
            _callback.Invoke(builder, data, match);
        }
    }
}