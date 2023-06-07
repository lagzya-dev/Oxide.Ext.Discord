using System.Reflection;
using Oxide.Ext.Discord.Attributes;
using Oxide.Ext.Discord.Extensions;

namespace Oxide.Ext.Discord.Plugins.Setup
{
    public struct PluginFieldResult<T> where T : BaseDiscordAttribute
    {
        public readonly MemberInfo Member;
        public readonly T Attribute;
        public bool IsValid => Member != null && Attribute != null;

        public PluginFieldResult(MemberInfo member, T attribute)
        {
            Member = member;
            Attribute = attribute;
        }

        public void SetValue(object instance, object value) => Member.SetMemberValue(instance, value);
    }
}