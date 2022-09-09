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
    public static class PluginPlaceholders
    {
        private static readonly Lang Lang = Interface.Oxide.GetLibrary<Lang>();
        
        public static void Name(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.Name);
        public static void Title(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.Title);
        public static void Author(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.Author);
        public static void Version(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.Version.ToString());
        public static void Description(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.Description);
        public static void FullName(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.FullName());
        public static void HookTime(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, TimeSpan.FromSeconds(plugin.TotalHookTime));
        public static void LangMessage(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, Lang.GetMessage(state.Format, plugin, state.Data.Get<IPlayer>()?.Id));

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "plugin", nameof(Plugin));
        }
        
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
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