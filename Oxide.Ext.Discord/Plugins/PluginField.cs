using System;
using System.Collections.Generic;
using System.Reflection;
using Oxide.Ext.Discord.Attributes;

namespace Oxide.Ext.Discord.Plugins
{
    public struct PluginField
    {
        public readonly MemberInfo Member;
        public readonly List<Attribute> Attributes;

        public PluginField(MemberInfo member)
        {
            Member = member;
            Attributes = new List<Attribute>(0);
            Attribute[] attributes = Attribute.GetCustomAttributes(member);
            for (int index = 0; index < attributes.Length; index++)
            {
                Attribute attribute = attributes[index];
                if (attribute is BaseDiscordAttribute)
                {
                    Attributes.Add(attribute);
                }
            }
        }
        
        public T GetAttribute<T>() where T : Attribute
        {
            for (int index = 0; index < Attributes.Count; index++)
            {
                Attribute attribute = Attributes[index];
                if (attribute is T tAttribute)
                {
                    return tAttribute;
                }
            }

            return default(T);
        }
    }
}