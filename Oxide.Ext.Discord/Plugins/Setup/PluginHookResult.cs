using Oxide.Ext.Discord.Attributes;

namespace Oxide.Ext.Discord.Plugins.Setup
{
    internal struct PluginHookResult<T> where T : BaseDiscordAttribute
    {
        public readonly string Name;
        public readonly T Attribute;
        public bool IsValid => !string.IsNullOrEmpty(Name) && Attribute != null;

        public PluginHookResult(string name, T attribute)
        {
            Name = name;
            Attribute = attribute;
        }
    }
}