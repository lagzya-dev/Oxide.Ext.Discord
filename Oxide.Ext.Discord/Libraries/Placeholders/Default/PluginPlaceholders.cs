using System;
using System.Text;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="Plugin"/> placeholders
    /// </summary>
    public static class PluginPlaceholders
    {
        private static readonly Lang Lang = Interface.Oxide.GetLibrary<Lang>();
        
        /// <summary>
        /// <see cref="Plugin.Name"/> placeholder
        /// </summary>
        public static void Name(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.Name);
        
        /// <summary>
        /// <see cref="Plugin.Title"/> placeholder
        /// </summary>
        public static void Title(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.Title);
        
        /// <summary>
        /// <see cref="Plugin.Author"/> placeholder
        /// </summary>
        public static void Author(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.Author);
        
        /// <summary>
        /// <see cref="Plugin.Version"/> placeholder
        /// </summary>
        public static void Version(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.Version.ToString());
        
        /// <summary>
        /// <see cref="Plugin.Description"/> placeholder
        /// </summary>
        public static void Description(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.Description);
        
        /// <summary>
        /// <see cref="PluginExt.FullName"/> placeholder
        /// </summary>
        public static void FullName(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.FullName());
        
        /// <summary>
        /// <see cref="Plugin.TotalHookTime"/> placeholder
        /// </summary>
        public static void HookTime(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, TimeSpan.FromSeconds(plugin.TotalHookTime));
        
        /// <summary>
        /// Lang message for a plugin
        /// </summary>
        public static void LangMessage(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, Lang.GetMessage(state.Format, plugin, state.Data.Get<IPlayer>()?.Id));

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "plugin");
        }
        
        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey = nameof(Plugin))
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<Plugin>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<Plugin>(plugin, $"{placeholderPrefix}.title", dataKey, Title);
            placeholders.RegisterPlaceholder<Plugin>(plugin, $"{placeholderPrefix}.author", dataKey, Author);
            placeholders.RegisterPlaceholder<Plugin>(plugin, $"{placeholderPrefix}.version", dataKey, Version);
            placeholders.RegisterPlaceholder<Plugin>(plugin, $"{placeholderPrefix}.description", dataKey, Description);
            placeholders.RegisterPlaceholder<Plugin>(plugin, $"{placeholderPrefix}.fullname", dataKey, FullName);
            placeholders.RegisterPlaceholder<Plugin>(plugin, $"{placeholderPrefix}.hooktime", dataKey, HookTime);
            placeholders.RegisterPlaceholder<Plugin>(plugin, $"{placeholderPrefix}.lang", dataKey, LangMessage);
        }
    }
}