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

        public override void Invoke(StringBuilder builder, PlaceholderState state) => PlaceholderFormatting.Replace(builder, state, _value);
    }
}