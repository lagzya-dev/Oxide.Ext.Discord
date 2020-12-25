using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Teams;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Entities.Webhooks;
using Oxide.Ext.Discord.Helpers.Cdn;
using Oxide.Ext.Discord.REST;

namespace Oxide.Ext.Discord.Entities.Interactions
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Application
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("icon")]
        public string Icon { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("rpc_origins")]
        public List<string> RpcOrigins { get; set; }
        
        [JsonProperty("bot_public")]
        public bool BotPublic { get; set; }
        
        [JsonProperty("bot_require_code_grant")]
        public bool BotRequireCodeGrant { get; set; }
        
        [JsonProperty("owner")]
        public DiscordUser Owner { get; set; }
        
        [JsonProperty("summary")]
        public string Summary { get; set; }
        
        [JsonProperty("verify_key")]
        public string Verify { get; set; }
        
        [JsonProperty("team")]
        public Team Team { get; set; }
        
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        
        [JsonProperty("primary_sku_id")]
        public string PrimarySkuId { get; set; }
        
        [JsonProperty("slug")]
        public string Slug { get; set; }
        
        [JsonProperty("cover_image")]
        public string CoverImage { get; set; } 
        
        [JsonProperty("flags")]
        public int Flags { get; set; }

        public string GetApplicationIconUrl => DiscordCdn.GetApplicationIconUrl(Id, Icon);
        
        public void GetGlobalApplicationCommands(DiscordClient client, Action<List<ApplicationCommand>> callback = null)
        {
            client.REST.DoRequest($"/applications/{Id}/commands", RequestMethod.GET, null, callback);
        }
        
        public void CreateGlobalApplicationCommand(DiscordClient client, ApplicationCommandCreate create, Action<ApplicationCommand> callback = null)
        {
            client.REST.DoRequest($"/applications/{Id}/commands", RequestMethod.POST, create, callback);
        }
        
        public void EditGlobalApplicationCommand(DiscordClient client, ApplicationCommandCreate update, Action<ApplicationCommand> callback = null)
        {
            client.REST.DoRequest($"/applications/{Id}/commands", RequestMethod.PATCH, update, callback);
        }
        
        public void DeleteGlobalApplicationCommand(DiscordClient client, string commandId, Action<ApplicationCommand> callback = null)
        {
            client.REST.DoRequest($"/applications/{Id}/commands/{commandId}", RequestMethod.PATCH, null, callback);
        }
        
        public void DeleteGlobalApplicationCommand(DiscordClient client, ApplicationCommand delete, Action<ApplicationCommand> callback = null)
        {
            DeleteGlobalApplicationCommand(client, delete.Id, callback);
        }

        public void GetGuildApplicationCommands(DiscordClient client, string guildId, Action<List<ApplicationCommand>> callback = null)
        {
            client.REST.DoRequest($"/applications/{Id}/guilds/{guildId}/commands", RequestMethod.GET, null, callback);
        }
        
        public void GetGuildApplicationCommands(DiscordClient client, Guild guild, Action<List<ApplicationCommand>> callback = null)
        {
            GetGuildApplicationCommands(client, guild.Id, callback);
        }
        
        public void CreateGuildApplicationCommands(DiscordClient client, string guildId, ApplicationCommandCreate create, Action<ApplicationCommand> callback = null)
        {
            client.REST.DoRequest($"/applications/{Id}/guilds/{guildId}/commands", RequestMethod.POST, create, callback);
        }
        
        public void CreateGuildApplicationCommands(DiscordClient client, Guild guild, ApplicationCommandCreate create, Action<ApplicationCommand> callback = null)
        {
            CreateGuildApplicationCommands(client, guild.Id, create, callback);
        }
        
        public void EditGuildApplicationCommands(DiscordClient client, string guildId, ApplicationCommand update, Action<ApplicationCommand> callback = null)
        {
            client.REST.DoRequest($"/applications/{Id}/guilds/{guildId}/commands", RequestMethod.PATCH, update, callback);
        }
        
        public void EditGuildApplicationCommands(DiscordClient client, Guild guild, ApplicationCommand update, Action<ApplicationCommand> callback = null)
        {
            EditGuildApplicationCommands(client, guild.Id, update, callback);
        }
        
        public void DeleteGuildApplicationCommands(DiscordClient client, string guildId, string commandId, Action callback = null)
        {
            client.REST.DoRequest($"/applications/{Id}/guilds/{guildId}/commands/{commandId}", RequestMethod.DELETE, null, callback);
        }
        
        public void DeleteGuildApplicationCommands(DiscordClient client, Guild guild, ApplicationCommand delete, Action callback = null)
        {
            DeleteGuildApplicationCommands(client, guild.Id, delete.Id, callback);
        }

        public void EditOriginalInteractionResponse(DiscordClient client, string interactionToken, WebhookEditMessage message, Action<Message> callback = null)
        {
            client.REST.DoRequest($"/webhooks/{Id}/{interactionToken}/messages/@original", RequestMethod.PATCH, message, callback);
        }

        public void EditOriginalInteractionResponse(DiscordClient client, Interaction interaction, WebhookEditMessage message, Action<Message> callback = null) => EditOriginalInteractionResponse(client, interaction.Token, message, callback);
        
        public void DeleteOriginalInteractionResponse(DiscordClient client, string interactionToken, Action callback = null)
        {
            client.REST.DoRequest($"/webhooks/{Id}/{interactionToken}/messages/@original", RequestMethod.DELETE, null, callback);
        }

        public void DeleteOriginalInteractionResponse(DiscordClient client, Interaction interaction, Action callback = null) => DeleteOriginalInteractionResponse(client, interaction.Token, callback);
        
        public void CreateFollowUpMessage(DiscordClient client, string interactionToken, WebhookCreateMessage message, Action<Message> callback = null)
        {
            client.REST.DoRequest($"/webhooks/{Id}/{interactionToken}", RequestMethod.POST, message, callback);
        }

        public void CreateFollowUpMessage(DiscordClient client, Interaction interaction, WebhookCreateMessage message, Action<Message> callback = null) => CreateFollowUpMessage(client, interaction.Token, message, callback);
        
        public void EditFollowUpMessage(DiscordClient client, string interactionToken, string messageId, WebhookEditMessage edit, Action<Message> callback = null)
        {
            client.REST.DoRequest($"/webhooks/{Id}/{interactionToken}/messages/{messageId}", RequestMethod.PATCH, edit, callback);
        }

        public void EditFollowUpMessage(DiscordClient client, Interaction interaction, string messageId, WebhookEditMessage edit, Action<Message> callback = null) => EditFollowUpMessage(client, interaction.Token, messageId, edit, callback);
        public void EditFollowUpMessage(DiscordClient client, Interaction interaction, Message message, WebhookEditMessage edit, Action<Message> callback = null) => EditFollowUpMessage(client, interaction.Token, message.Id, edit, callback);
        
        public void DeleteFollowUpMessage(DiscordClient client, string interactionToken, string messageId, Action callback = null)
        {
            client.REST.DoRequest($"/webhooks/{Id}/{interactionToken}/messages/{messageId}", RequestMethod.DELETE, null, callback);
        }

        public void DeleteFollowUpMessage(DiscordClient client, Interaction interaction, string messageId, Action callback = null) => DeleteFollowUpMessage(client, interaction.Token, messageId, callback);
        public void DeleteFollowUpMessage(DiscordClient client, Interaction interaction, Message message, Action callback = null) => DeleteFollowUpMessage(client, interaction.Token, message.Id, callback);
    }
}