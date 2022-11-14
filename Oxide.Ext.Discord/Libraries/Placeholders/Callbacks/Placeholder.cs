using System;
using System.Text;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Callbacks
{
    internal class Placeholder : BasePlaceholder
    {
        private readonly Action<StringBuilder, PlaceholderState> _callback;
        
        public Placeholder(Plugin plugin, Action<StringBuilder, PlaceholderState> callback) : base(plugin) 
        {
            _callback = callback;
        }

        public override void Invoke(StringBuilder builder, PlaceholderState state) => _callback.Invoke(builder, state);
    }
}