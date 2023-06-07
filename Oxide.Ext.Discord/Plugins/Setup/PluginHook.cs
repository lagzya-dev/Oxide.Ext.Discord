using System;
using System.Collections.Generic;
using System.Reflection;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Attributes;

namespace Oxide.Ext.Discord.Plugins.Setup
{
    public struct PluginHook
    {
        public readonly string Name;
        public readonly List<Attribute> Attributes;

        public PluginHook(MethodInfo info, Attribute[] attributes)
        {
            Name = info.Name;
            Attributes = new List<Attribute>(0);
            for (int index = 0; index < attributes.Length; index++)
            {
                Attribute attribute = attributes[index];
                switch (attribute)
                {
                    case HookMethodAttribute hook:
                        Name = hook.Name;
                        break;
                    
                    case BaseDiscordAttribute _:
                        Attributes.Add(attribute);
                        break;
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