using System.Linq;
using System.Reflection;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Attributes.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Interactions;

namespace Oxide.Ext.Discord.Extensions
{
    public static class PluginExt
    {
        public static void RegisterApplicationCommands(this Plugin plugin, DiscordApplication app)
        {
            foreach (MethodInfo method in plugin.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                object[] customAttributes = method.GetCustomAttributes(typeof(DiscordSlashCommandAttribute), false);
                if (customAttributes.Length != 0)
                {
                    foreach (DiscordSlashCommandAttribute command in customAttributes.Cast<DiscordSlashCommandAttribute>())
                    {
                        DiscordExtension.DiscordAppCommand.AddApplicationCommand(plugin, app, InteractionType.ApplicationCommand, method.Name, command.Command, command.Group, command.SubCommand);
                    }
                }
                
                customAttributes = method.GetCustomAttributes(typeof(DiscordAutoCompleteCommandAttribute), false);
                foreach (DiscordAutoCompleteCommandAttribute command in customAttributes.Cast<DiscordAutoCompleteCommandAttribute>())
                {
                    DiscordExtension.DiscordAppCommand.AddApplicationCommand(plugin, app, command.Type, method.Name, command.Command);
                }

                customAttributes = method.GetCustomAttributes(typeof(DiscordMessageComponentCommandAttribute), false);
                foreach (DiscordMessageComponentCommandAttribute command in customAttributes.Cast<DiscordMessageComponentCommandAttribute>())
                {
                    DiscordExtension.DiscordAppCommand.AddMessageComponentCommand(plugin, app, command.CustomId, method.Name);
                }
            }
        }
    }
}