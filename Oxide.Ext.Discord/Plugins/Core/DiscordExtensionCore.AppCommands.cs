using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Attributes.ApplicationCommands;
using Oxide.Ext.Discord.Builders.ApplicationCommands;
using Oxide.Ext.Discord.Builders.Interactions;
using Oxide.Ext.Discord.Clients;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Factory;
using Oxide.Ext.Discord.Libraries.AppCommands;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Commands;
using Oxide.Ext.Discord.Plugins.Core.AppCommands;
using Oxide.Ext.Discord.Plugins.Core.Templates;
using Oxide.Ext.Discord.Plugins.Setup;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Plugins.Core
{
    internal partial class DiscordExtensionCore
    {
        private readonly Hash<Snowflake, CommandCache> _commandCache = new Hash<Snowflake, CommandCache>();

        public void RegisterApplicationCommands(BotClient client)
        {
            ApplicationCommandBuilder builder = new ApplicationCommandBuilder(AppCommandKeys.DeCommand, "Discord Extension Commands", ApplicationCommandType.ChatInput)
                                                .AddDefaultPermissions(PermissionFlags.Administrator)
                                                    .AddSubCommandGroup(AppCommandKeys.AppCommandGroup, "Application Commands")
                                                        .AddSubCommand(AppCommandKeys.DeleteAppCommand, "Delete a registered application command")
                                                            .AddOption(CommandOptionType.String, AppCommandKeys.DeleteAppCommandArgument, "Application Command To Delete")
                                                            .AutoComplete()
                                                            .Required()
                                                            .Build()
                                                        .Build()
                                                    .Build();

            CommandCreate create = builder.Build();
            DiscordCommandLocalization localization = builder.BuildCommandLocalization();
            DiscordExtension.DiscordCommandLocalizations.RegisterCommandLocalizationAsync(this, null, localization, new TemplateVersion(1, 0, 0), new TemplateVersion(1, 0, 0)).Then(() =>
            {
                DiscordExtension.DiscordCommandLocalizations.ApplyCommandLocalizationsAsync(this, create, null).Then(() =>
                {
                    client.Application.CreateGlobalCommand(client.GetFirstClient(), builder.Build());
                    DiscordAppCommand.Instance.RegisterApplicationCommands(new PluginSetup(this), client.Connection);
                });
            });
        }

        // ReSharper disable once UnusedMember.Local
        [HookMethod(nameof(HandleDeleteApplicationCommand))]
        [DiscordApplicationCommand(AppCommandKeys.DeCommand, AppCommandKeys.DeleteAppCommand, AppCommandKeys.AppCommandGroup)]
        private void HandleDeleteApplicationCommand(DiscordInteraction interaction, InteractionDataParsed parsed)
        {
            DiscordClient client = BotClientFactory.Instance.GetByApplicationId(interaction.ApplicationId)?.GetFirstClient();
            if (client == null)
            {
                return;
            }
            
            string argString = parsed.Args.GetString(AppCommandKeys.DeleteAppCommandArgument);
            string[] args = argString.Split(':');

            if (!Snowflake.TryParse(args.Length == 1 ? args[0] : args[1], out Snowflake commandId))
            {
                SendTemplateMessage(client, TemplateKeys.Commands.Delete.Errors.InvalidSelection, interaction);
                return;
            }
            
            Snowflake guildId = default(Snowflake);
            if (args.Length == 2 && !Snowflake.TryParse(args[0], out guildId))
            {
                SendTemplateMessage(client, TemplateKeys.Commands.Delete.Errors.InvalidSelection, interaction);
                return;
            }

            if (guildId.IsValid())
            {
                client.Bot.Application.GetGuildCommand(client, guildId, commandId)
                      .Then(command => DeleteGetSuccess(client, interaction, command))
                      .Catch<ResponseError>(ex => DeleteGetError(client, interaction, ex));
            }
            else
            {
                client.Bot.Application.GetGlobalCommand(client, commandId)                     
                      .Then(command => DeleteGetSuccess(client, interaction, command))
                      .Catch<ResponseError>(ex => DeleteGetError(client, interaction, ex));
            }
        }

        public void DeleteGetSuccess(DiscordClient client, DiscordInteraction interaction, DiscordApplicationCommand command)
        {
            command.Delete(client).Then(() =>
            {
                SendTemplateMessage(client, TemplateKeys.Commands.Delete.Success, interaction, GetPlaceholderData().AddCommand(command));
            }).Catch(error =>
            {
                SendTemplateMessage(client, TemplateKeys.Commands.Delete.Errors.DeleteCommandError, interaction, GetPlaceholderData().AddCommand(command));
            });
        }

        public void DeleteGetError(DiscordClient client, DiscordInteraction interaction, ResponseError error)
        {
            SendTemplateMessage(client, TemplateKeys.Commands.Delete.Success, interaction, GetPlaceholderData().AddRequestError(error));
        }

        // ReSharper disable once UnusedMember.Local
        [HookMethod(nameof(HandleDeleteApplicationAutoComplete))]
        [DiscordAutoCompleteCommand(AppCommandKeys.DeCommand, AppCommandKeys.DeleteAppCommandArgument, AppCommandKeys.DeleteAppCommand, AppCommandKeys.AppCommandGroup)]
        private void HandleDeleteApplicationAutoComplete(DiscordInteraction interaction, InteractionDataOption focused)
        {
            string search = focused.GetValue<string>();
            CommandCache cache = _commandCache[interaction.ApplicationId];
            InteractionAutoCompleteBuilder builder = new InteractionAutoCompleteBuilder(interaction);
            BotClient client = BotClientFactory.Instance.GetByApplicationId(interaction.ApplicationId);
            if (client == null)
            {
                return;
            }
            
            if (cache != null && !cache.IsExpired)
            {
                AddCommands(builder, search, cache.Commands);
                interaction.CreateResponse(client.GetFirstClient(), builder.Build());
                return;
            }

            CacheCommands(client, commandCache =>
            {
                AddCommands(builder, search, commandCache.Commands);
                interaction.CreateResponse(client.GetFirstClient(), builder.Build());
            });
        }
        
        private static void AddCommands(InteractionAutoCompleteBuilder builder, string search, List<DiscordApplicationCommand> commands)
        {
            for (int index = 0; index < commands.Count && builder.CanAddChoice(); index++)
            {
                DiscordApplicationCommand command = commands[index];
                if (!string.IsNullOrEmpty(search) && command.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) == -1)
                {
                    continue;
                }

                if (command.GuildId.HasValue && command.GuildId.Value.IsValid())
                {
                    builder.AddAutoCompleteChoice($"[Guild] {command.Name}", $"{command.GuildId.Value}:{command.Id}");
                }
                else
                {
                    builder.AddAutoCompleteChoice($"[Global] {command.Name}", command.Id.ToString());
                }
            }
        }

        private void CacheCommands(BotClient bot, Action<CommandCache> callback)
        {
            bot.Application.GetAllCommands(bot.GetFirstClient())
               .Then(commands =>
               {
                   CommandCache cache = new CommandCache(commands);
                   _commandCache[bot.Application.Id] = cache;
                   callback.Invoke(cache);
               });
        }

        private PlaceholderData GetPlaceholderData()
        {
            return DiscordPlaceholders.Instance.CreateData(this);
        }
    }
}