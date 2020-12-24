using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Guilds.Integrations;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Entities.Webhooks;
using Oxide.Ext.Discord.REST;

namespace Oxide.Ext.Discord.Entities.AuditLogs
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class AuditLog
    {
        [JsonProperty("webhooks")]
        public List<Webhook> Webhooks { get; set; }

        [JsonProperty("users")]
        public List<DiscordUser> Users { get; set; }

        [JsonProperty("audit_log_entries")]
        public List<AuditLogEntry> AuditLogEntries { get; set; }
        
        [JsonProperty("integrations")]
        public List<Integration> Integrations { get; set; }

        public static void GetGuildAuditLog(DiscordClient client, Guild guild, Action<AuditLog> callback = null) => GetGuildAuditLog(client, guild.Id, callback);

        public static void GetGuildAuditLog(DiscordClient client, string guildId, Action<AuditLog> callback = null)
        {
            client.REST.DoRequest($"/guilds/{guildId}/audit-logs", RequestMethod.GET, null, callback);
        }
    }
}
