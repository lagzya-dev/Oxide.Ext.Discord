using System;
using System.Collections.Generic;
using System.Reflection;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Attributes;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Plugins
{
    /// <summary>
    /// Build Discord Extension Setup Data for a plugin
    /// </summary>
    public class PluginSetup
    {
        internal readonly Plugin Plugin;
        internal readonly string PluginName;

        internal readonly List<string> PluginHooks = new List<string>(0);
        internal readonly List<string> GlobalHooks = new List<string>(0);
        private readonly List<PluginCallback> _callbacks = new List<PluginCallback>(0);
        private readonly List<PluginField> _fields = new List<PluginField>(0);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="plugin">Plugin the data is for</param>
        /// <param name="logger">Logger</param>
        public PluginSetup(Plugin plugin, ILogger logger)
        {
            Plugin = plugin ?? throw new ArgumentNullException(nameof(plugin));
            PluginName = Plugin.Name;
            MemberInfo[] methods = plugin.GetType().GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            for (int index = 0; index < methods.Length; index++)
            {
                MemberInfo member = methods[index];
                Attribute[] attributes = Attribute.GetCustomAttributes(member);
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
                                    logger.Verbose("Adding Global Hook: {0}.{1}", Plugin.Name, name);
                                }
                                else
                                {
                                    PluginHooks.Add(name);
                                    logger.Verbose("Adding Plugin Hook: {0}.{1}", Plugin.Name, name);
                                }
                            }
                           
                            if (IsCallbackMethod(attributes))
                            {
                                logger.Verbose("Adding Callback Hook: {0}.{1}", Plugin.Name, name);
                                _callbacks.Add(new PluginCallback(name, hook, attributes));
                            }
                        }
                        break;
                    }

                    case FieldInfo _:
                    case PropertyInfo _:
                        if (IsFieldAttribute(attributes))
                        {
                            logger.Verbose("Adding Plugin Field: {0}.{1}", Plugin.Name, member.Name);
                            _fields.Add(new PluginField(member, attributes));
                        }
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
            for (int index = 0; index < attributes.Length; index++)
            {
                Attribute attribute = attributes[index];
                switch (attribute)
                {
                    case BaseApplicationCommandAttribute _:
                    case BaseCommandAttribute _:
                        return true;
                }
            }

            return false;
        }

        private bool IsFieldAttribute(Attribute[] attributes)
        {
            for (int index = 0; index < attributes.Length; index++)
            {
                Attribute attribute = attributes[index];
                switch (attribute)
                {
                    case DiscordPoolAttribute _:
                        return true;
                }
            }

            return false;
        }

        internal IEnumerable<PluginHookResult<T>> GetCallbacksWithAttribute<T>() where T : BaseDiscordAttribute
        {
            for (int index = 0; index < _callbacks.Count; index++)
            {
                PluginCallback callback = _callbacks[index];
                foreach (T attribute in callback.GetAttributes<T>())
                {
                    yield return new PluginHookResult<T>(callback, attribute);
                }
            }
        }
        
        internal PluginFieldResult<T> GetFieldWthAttribute<T>() where T : BaseDiscordAttribute
        {
            for (int index = 0; index < _fields.Count; index++)
            {
                PluginField field = _fields[index];
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