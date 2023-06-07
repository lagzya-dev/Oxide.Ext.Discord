using System;
using System.Collections.Generic;
using System.Reflection;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Attributes;

namespace Oxide.Ext.Discord.Plugins.Setup
{
    public class PluginSetupData
    {
        public readonly Plugin Plugin;
        public readonly string PluginName;

        public readonly List<PluginHook> Hooks = new List<PluginHook>();
        public readonly List<PluginField> Fields = new List<PluginField>();

        public PluginSetupData(Plugin plugin)
        {
            Plugin = plugin;
            PluginName = Plugin.Name;
            MemberInfo[] methods = plugin.GetType().GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            for (int index = 0; index < methods.Length; index++)
            {
                MemberInfo member = methods[index];
                Attribute[] attributes = Attribute.GetCustomAttributes(member);
                if (attributes.Length == 0)
                {
                    continue;
                }
                
                switch (member)
                {
                    case MethodInfo hook:
                    {
                        if (!hook.IsPublic)
                        {
                            Hooks.Add(new PluginHook(hook, attributes));
                        }
                        else if (HasAttribute<HookMethodAttribute>(attributes) && attributes.Length > 1)
                        {
                            Hooks.Add(new PluginHook(hook, attributes));
                        }
                        break;
                    }

                    case FieldInfo field:
                        Fields.Add(new PluginField(field));
                        break;
                    
                    case PropertyInfo property:
                        Fields.Add(new PluginField(property));
                        break;
                }
            }
        }

        private bool HasAttribute<T>(Attribute[] attributes) where T : Attribute
        {
            for (int index = 0; index < attributes.Length; index++)
            {
                Attribute attribute = attributes[index];
                if (attribute is T)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<PluginHookResult<T>> GetHooksWithAttribute<T>() where T : BaseDiscordAttribute
        {
            for (int index = 0; index < Hooks.Count; index++)
            {
                PluginHook hook = Hooks[index];
                T attribute = hook.GetAttribute<T>();
                if (attribute != null)
                {
                    yield return new PluginHookResult<T>(hook.Name, attribute);
                }
            }
        }
        
        public PluginFieldResult<T> GetFieldWthAttribute<T>() where T : BaseDiscordAttribute
        {
            for (int index = 0; index < Fields.Count; index++)
            {
                PluginField field = Fields[index];
                T attribute = field.GetAttribute<T>();
                if (attribute != null)
                {
                    return new PluginFieldResult<T>(field.Member, attribute);
                }
            }

            return default(PluginFieldResult<T>);
        }
    }
}