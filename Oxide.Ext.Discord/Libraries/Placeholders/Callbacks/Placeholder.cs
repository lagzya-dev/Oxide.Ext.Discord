using System;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Callbacks
{
    internal class Placeholder<TResult> : BasePlaceholder<TResult>
    {
        private readonly Func<TResult> _callback;
        
        public Placeholder(Plugin plugin, Func<TResult> callback) : base(plugin) 
        {
            _callback = callback;
        }

        public override TResult InvokeInternal(PlaceholderState state) => _callback.Invoke();
    }
}