using System;
using System.Text;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Callbacks
{
    public class Placeholder : BasePlaceholder
    {
        private readonly Action<StringBuilder, PlaceholderMatch> _callback;
        
        public Placeholder(Plugin plugin, Action<StringBuilder, PlaceholderMatch> callback) : base(plugin) 
        {
            _callback = callback;
        }

        internal Placeholder(Action<StringBuilder, PlaceholderMatch> callback) : this(null, callback) {}

        public override void Invoke(StringBuilder builder, PlaceholderMatch match, PlaceholderData data)
        {
            _callback.Invoke(builder, match);
        }
    }
}