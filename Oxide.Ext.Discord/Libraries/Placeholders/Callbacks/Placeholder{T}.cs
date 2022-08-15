using System;
using System.Text;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Callbacks
{
    internal class Placeholder<T> : BasePlaceholder
    {
        private readonly string _dataKey;
        private readonly Action<StringBuilder, PlaceholderMatch, T> _callback;
        
        public Placeholder(string dataKey, Plugin plugin, Action<StringBuilder, PlaceholderMatch, T> callback) : base(plugin)
        {
            _dataKey = dataKey;
            _callback = callback;
        }

        internal Placeholder(string dataKey, Action<StringBuilder, PlaceholderMatch, T> callback) : this(dataKey, null, callback) {}

        public override void Invoke(StringBuilder builder, PlaceholderMatch match, PlaceholderData data)
        {
            T tData = data.Get<T>(_dataKey);
            if (tData != null)
            {
                Invoke(builder, match, tData);
            }
        }
        
        private void Invoke(StringBuilder builder, PlaceholderMatch match, T data)
        {
            _callback.Invoke(builder, match, data);
        }
    }
}