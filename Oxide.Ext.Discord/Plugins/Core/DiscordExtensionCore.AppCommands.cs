using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Attributes.ApplicationCommands;
using Oxide.Ext.Discord.Builders.ApplicationCommands;
using Oxide.Ext.Discord.Builders.Interactions;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Plugins.Core.AppCommands;
using Oxide.Ext.Discord.Plugins.Core.Templates;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Plugins.Core
{
    internal partial class DiscordExtensionCore
    {
        private readonly Hash<Snowflake, CommandCache> _commandCache = new Hash<Snowflake, CommandCache>();

        public void RegisterApplicationCommands(BotClient client)
        {
            ApplicationCommandBuilder builder = new ApplicationCommandBuilder(AppCommandKeys.DeCommand, "Discord Extension Commands", ApplicationCommandType.ChatInput)
                                                .AddDefaultPermissions(PermissionFlags.None)
                                                .AddNameLocalizations(this, LangKeys.AppCommands.DeCommand)
                                                .AddDescriptionLocalizations(this, LangKeys.AppCommands.DeCommandDescription)

                                                .AddSubCommandGroup(AppCommandKeys.AppCommandGroup, "Application Commands")
                                                .AddNameLocalizations(this, LangKeys.AppCommands.AppCommandGroup)
                                                .AddDescriptionLocalizations(this, LangKeys.AppCommands.AppCommandGroupDescription)

                                                .AddSubCommand(AppCommandKeys.DeleteAppCommand, "Delete a registered application command")
                                                .AddNameLocalizations(this, LangKeys.AppCommands.DeleteAppCommand)
                                                .AddDescriptionLocalizations(this, LangKeys.AppCommands.DeleteAppCommandDescription)
                                                .AddOption(CommandOptionType.String, AppCommandKeys.DeleteAppCommandArgument, "Application Command To Delete")
                                                .AutoComplete()
                                                .Required()
                                                .AddNameLocalizations(this, LangKeys.AppCommands.DeleteAppCommandArgument)
                                                .AddDescriptionLocalizations(this, LangKeys.AppCommands.DeleteAppCommandArgumentDescription)
                                                .Build()
                                                .Build()
                                                .Build();

            client.Application.CreateGlobalCommand(client.GetFirstClient(), builder.Build());
            DiscordExtension.DiscordAppCommand.RegisterApplicationCommands(client.Application, this);
        }

        [HookMethod(nameof(HandleDeleteApplicationCommand))]
        [DiscordApplicationCommand(AppCommandKeys.DeCommand, AppCommandKeys.DeleteAppCommand, AppCommandKeys.AppCommandGroup)]
        private void HandleDeleteApplicationCommand(DiscordInteraction interaction, InteractionDataParsed parsed)
        {
            DiscordClient client = BotClient.GetByApplicationId(interaction.ApplicationId)?.GetFirstClient();
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
                client.Bot.Application.GetGuildCommand(client, guildId, commandId, command => DeleteGetSuccess(client, interaction, command), error => DeleteGetError(client, interaction, error));
            }
            else
            {
                client.Bot.Application.GetGlobalCommand(client, commandId, command => DeleteGetSuccess(client, interaction, command), error => DeleteGetError(client, interaction, error));
            }
        }

        public void DeleteGetSuccess(DiscordClient client, DiscordInteraction interaction, DiscordApplicationCommand command)
        {
            command.Delete(client, () =>
            {
                SendTemplateMessage(client, TemplateKeys.Commands.Delete.Success, interaction, DiscordExtension.DiscordPlaceholders.CreateData().AddCommand(command));
            }, error =>
            {
                SendTemplateMessage(client, TemplateKeys.Commands.Delete.Errors.DeleteCommandError, interaction, DiscordExtension.DiscordPlaceholders.CreateData().AddCommand(command));
            });
        }

        public void DeleteGetError(DiscordClient client, DiscordInteraction interaction, RequestError error)
        {
            SendTemplateMessage(client, TemplateKeys.Commands.Delete.Success, interaction, DiscordExtension.DiscordPlaceholders.CreateData().AddRequestError(error));
        }

        [HookMethod(nameof(HandleDeleteApplicationAutoComplete))]
        [DiscordAutoCompleteCommand(AppCommandKeys.DeCommand, AppCommandKeys.DeleteAppCommandArgument, AppCommandKeys.DeleteAppCommand, AppCommandKeys.AppCommandGroup)]
        private void HandleDeleteApplicationAutoComplete(DiscordInteraction interaction, InteractionDataOption focused)
        {
            string search = (string)focused.Value;
            CommandCache cache = _commandCache[interaction.ApplicationId];
            InteractionAutoCompleteBuilder builder = new InteractionAutoCompleteBuilder(interaction);
            BotClient client = BotClient.GetByApplicationId(interaction.ApplicationId);
            if (client == null)
            {
                return;
            }
            
            if (cache != null && !cache.IsExpired)
            {
                AddCommands(builder, search, cache.Commands);
                interaction.CreateInteractionResponse(client.GetFirstClient(), builder.Build());
                return;
            }

            CacheCommands(client, commandCache =>
            {
                AddCommands(builder, search, commandCache.Commands);
                interaction.CreateInteractionResponse(client.GetFirstClient(), builder.Build());
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
            bot.Application.GetAllCommands(bot.GetFirstClient(), commands =>
            {
                CommandCache cache = new CommandCache(commands);
                _commandCache[bot.Application.Id] = cache;
                callback.Invoke(cache);
            });
        }
    }
}