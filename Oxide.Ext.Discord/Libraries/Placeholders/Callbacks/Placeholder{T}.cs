using System;
using System.Text;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Callbacks
{
    internal class Placeholder<T> : BasePlaceholder
    {
        private readonly string _dataKey;
        private readonly Action<StringBuilder, PlaceholderState, T> _callback;

        public Placeholder(string dataKey, Plugin plugin, Action<StringBuilder, PlaceholderState, T> callback) : base(plugin)
        {
            _dataKey = dataKey;
            _callback = callback;
        }

        public override void Invoke(StringBuilder builder, PlaceholderState state)
        {
            T data = state.Data.Get<T>(_dataKey);
            if (data != null)
            {
                _callback.Invoke(builder, state, data);
            }
        }
    }
}