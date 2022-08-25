using System;
using System.Text;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal static class PluginPlaceholders
    {
        private static readonly Lang Lang = Interface.Oxide.GetLibrary<Lang>();
        
        private static void Name(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.Name);
        private static void Title(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.Title);
        private static void Author(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.Author);
        private static void Version(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.Version.ToString());
        private static void Description(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.Description);
        private static void FullName(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, plugin.FullName());
        private static void HookTime(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, TimeSpan.FromSeconds(plugin.TotalHookTime));
        private static void LangMessage(StringBuilder builder, PlaceholderState state, Plugin plugin) => PlaceholderFormatting.Replace(builder, state, Lang.GetMessage(state.Format, plugin, state.Data.Get<IPlayer>()?.Id));

        public static void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterInternalPlaceholder<Plugin>("plugin.name", Name);
            placeholders.RegisterInternalPlaceholder<Plugin>("plugin.title", Title);
            placeholders.RegisterInternalPlaceholder<Plugin>("plugin.author", Author);
            placeholders.RegisterInternalPlaceholder<Plugin>("plugin.version", Version);
            placeholders.RegisterInternalPlaceholder<Plugin>("plugin.description", Description);
            placeholders.RegisterInternalPlaceholder<Plugin>("plugin.fullname", FullName);
            placeholders.RegisterInternalPlaceholder<Plugin>("plugin.hooktime", HookTime);
            placeholders.RegisterInternalPlaceholder<Plugin>("plugin.lang", LangMessage);
        }
    }
}