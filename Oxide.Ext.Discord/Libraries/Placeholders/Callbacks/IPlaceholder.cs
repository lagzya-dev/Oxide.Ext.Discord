using System.Text;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Callbacks
{
    internal interface IPlaceholder
    {
        string PluginName { get; }
        bool IsExtensionPlaceholder { get; }
        
        void Invoke(StringBuilder sb, PlaceholderState state);

        bool IsForPlugin(Plugin plugin);
    }
}