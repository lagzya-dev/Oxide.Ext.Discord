using System;
using System.Collections.Generic;
using System.Text;
using Oxide.Core.Plugins;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Types
{
    public abstract class BasePlaceholders<T> : BasePlaceholders
    {
        private readonly Hash<string, Action<StringBuilder, T, PlaceholderMatch>> _placeholders = new Hash<string, Action<StringBuilder, T, PlaceholderMatch>>();

        protected BasePlaceholders(Plugin plugin) : base(plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        }

        internal BasePlaceholders() : base(null) { }
        
        protected void RegisterPlaceholder(string name, Action<StringBuilder, T, PlaceholderMatch> placeholder)
        {
            _placeholders[name] = placeholder;
        }

        public override void Invoke(StringBuilder builder, PlaceholderMatch match, PlaceholderData data)
        {
            Invoke(builder, match, data.Get<T>(GetDataKey()));
        }

        private void Invoke(StringBuilder builder, PlaceholderMatch match, T data)
        {
            _placeholders[match.Name]?.Invoke(builder, data, match);
        }

        protected void Replace(StringBuilder builder, PlaceholderMatch match, string value)
        {
            builder.Remove(match.Index, match.Length);
            builder.Insert(match.Index, value);
        }

        public override IEnumerable<string> GetPlaceholders()
        {
            return _placeholders.Keys;
        }
    }
}