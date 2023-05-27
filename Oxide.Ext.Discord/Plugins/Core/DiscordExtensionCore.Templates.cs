using System.Collections.Generic;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Entities.Interactions.Response;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Embeds;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Plugins.Core.Templates;

namespace Oxide.Ext.Discord.Plugins.Core
{
    internal partial class DiscordExtensionCore
    {
        private const string DiscordSuccess = "43b581";
        private const string DiscordDanger = "f04747";
        
        public void CreateTemplates()
        {
            DiscordMessageTemplate success = CreateTemplateEmbed("You have successfully deleted application command {command.name}", DiscordSuccess);
            DiscordExtension.DiscordMessageTemplates.RegisterLocalizedTemplateAsync(this, TemplateKeys.Commands.Delete.Success, success, new TemplateVersion(1, 0, 0), new TemplateVersion(1, 0, 0));
            
            DiscordMessageTemplate deleteError = CreateTemplateEmbed("An error occured deleting command {command.name}. Please try the command again. \nError: {error.message}", DiscordDanger);
            DiscordExtension.DiscordMessageTemplates.RegisterLocalizedTemplateAsync(this, TemplateKeys.Commands.Delete.Errors.DeleteCommandError, deleteError, new TemplateVersion(1, 0, 0), new TemplateVersion(1, 0, 0));
            
            DiscordMessageTemplate commandNotFound = CreateTemplateEmbed("We failed to find a matching command. Please make sure you select a command from the drop down.", DiscordDanger);
            DiscordExtension.DiscordMessageTemplates.RegisterLocalizedTemplateAsync(this, TemplateKeys.Commands.Delete.Errors.CommandIdNotFound, commandNotFound, new TemplateVersion(1, 0, 0), new TemplateVersion(1, 0, 0));
            
            DiscordMessageTemplate invalidSelection = CreateTemplateEmbed("You have selected an invalid command. Please try the command again and be sure to select an option from the drop down.", DiscordDanger);
            DiscordExtension.DiscordMessageTemplates.RegisterLocalizedTemplateAsync(this, TemplateKeys.Commands.Delete.Errors.InvalidSelection, invalidSelection, new TemplateVersion(1, 0, 0), new TemplateVersion(1, 0, 0));
        }
        
        public DiscordMessageTemplate CreateTemplateEmbed(string description, string color)
        {
            return new DiscordMessageTemplate
            {
                Embeds = new List<DiscordEmbedTemplate>
                {
                    new DiscordEmbedTemplate
                    {
                        Description = description,
                        Color = $"#{color}"
                    }
                },
            };
        }

        public void SendTemplateMessage(DiscordClient client, string key, DiscordInteraction interaction, PlaceholderData placeholders = null)
        {
            interaction.CreateTemplateResponse(client, this, InteractionResponseType.ChannelMessageWithSource, key, new InteractionCallbackData
            {
                Flags = interaction.GuildId.HasValue ? MessageFlags.Ephemeral : (MessageFlags?)null
            }, placeholders);
        }
    }
}