using System;
using System.Collections.Generic;
using System.Reflection;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Attributes;
using Oxide.Ext.Discord.Constants;

namespace Oxide.Ext.Discord.Plugins.Setup
{
    public class PluginSetup
    {
        public readonly Plugin Plugin;
        public readonly string PluginName;

        public readonly List<string> PluginHooks = new List<string>(0);
        public readonly List<string> GlobalHooks = new List<string>(0);
        public readonly List<PluginCallback> Callbacks = new List<PluginCallback>(0);
        public readonly List<PluginField> Fields = new List<PluginField>(0);

        public PluginSetup(Plugin plugin)
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
                        if (ParseHook(hook, attributes, out string name))
                        {
                            if (DiscordExtHooks.IsDiscordHook(name))
                            {
                                if (DiscordExtHooks.IsGlobalHook(name))
                                {
                                    GlobalHooks.Add(name);
                                }
                                else
                                {
                                    PluginHooks.Add(name);
                                }
                            }
                           
                            if (IsCallbackMethod(attributes))
                            {
                                Callbacks.Add(new PluginCallback(name, attributes));
                            }
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

        private bool ParseHook(MethodInfo info, Attribute[] attributes, out string name)
        {
            name = !info.IsPublic ? info.Name : null;
            HookMethodAttribute hook = GetAttribute<HookMethodAttribute>(attributes);
            if (hook != null)
            {
                name = hook.Name;
            }

            return name != null;
        }

        private T GetAttribute<T>(Attribute[] attributes) where T : Attribute
        {
            for (int index = 0; index < attributes.Length; index++)
            {
                Attribute attribute = attributes[index];
                if (attribute is T type)
                {
                    return type;
                }
            }

            return null;
        }

        private bool IsCallbackMethod(Attribute[] attributes)
        {
            switch (attributes.Length)
            {
                case 0:
                case 1 when attributes[0] is HookMethodAttribute:
                    return false;
                default:
                    return true;
            }
        }

        public IEnumerable<PluginHookResult<T>> GetHooksWithAttribute<T>() where T : BaseDiscordAttribute
        {
            for (int index = 0; index < Callbacks.Count; index++)
            {
                PluginCallback callback = Callbacks[index];
                T attribute = callback.GetAttribute<T>();
                if (attribute != null)
                {
                    yield return new PluginHookResult<T>(callback.Name, attribute);
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