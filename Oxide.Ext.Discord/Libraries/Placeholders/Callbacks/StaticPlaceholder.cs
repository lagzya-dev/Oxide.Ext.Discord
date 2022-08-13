using System.Text;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Callbacks
{
    internal class StaticPlaceholder : BasePlaceholder
    {
        private readonly string _value;
        
        public StaticPlaceholder(Plugin plugin, string value) : base(plugin) 
        {
            _value = value;
        }

        internal StaticPlaceholder(string value) : this(null, value) {}

        public override void Invoke(StringBuilder builder, PlaceholderMatch match, PlaceholderData data)
        {
            PlaceholderFormatting.Replace(builder, match, _value);
        }
    }
}