using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.REST;

namespace Oxide.Ext.Discord.DiscordObjects.SlashCommands
{
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
        public User Owner { get; set; }
        
        [JsonProperty("summary")]
        public string Summary { get; set; }
        
        [JsonProperty("verify_key")]
        public string Verify { get; set; }
        
        [JsonProperty("team")]
        public Team.Team Team { get; set; }
        
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
            GetGuildApplicationCommands(client, guild.id, callback);
        }
        
        public void CreateGuildApplicationCommands(DiscordClient client, string guildId, ApplicationCommandCreate create, Action<ApplicationCommand> callback = null)
        {
            client.REST.DoRequest($"/applications/{Id}/guilds/{guildId}/commands", RequestMethod.POST, create, callback);
        }
        
        public void CreateGuildApplicationCommands(DiscordClient client, Guild guild, ApplicationCommandCreate create, Action<ApplicationCommand> callback = null)
        {
            CreateGuildApplicationCommands(client, guild.id, create, callback);
        }
        
        public void EditGuildApplicationCommands(DiscordClient client, string guildId, ApplicationCommand update, Action<ApplicationCommand> callback = null)
        {
            client.REST.DoRequest($"/applications/{Id}/guilds/{guildId}/commands", RequestMethod.PATCH, update, callback);
        }
        
        public void EditGuildApplicationCommands(DiscordClient client, Guild guild, ApplicationCommand update, Action<ApplicationCommand> callback = null)
        {
            EditGuildApplicationCommands(client, guild.id, update, callback);
        }
        
        public void DeleteGuildApplicationCommands(DiscordClient client, string guildId, string commandId, Action callback = null)
        {
            client.REST.DoRequest($"/applications/{Id}/guilds/{guildId}/commands/{commandId}", RequestMethod.DELETE, null, callback);
        }
        
        public void DeleteGuildApplicationCommands(DiscordClient client, Guild guild, ApplicationCommand delete, Action callback = null)
        {
            DeleteGuildApplicationCommands(client, guild.id, delete.Id, callback);
        }
    }
}