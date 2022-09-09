using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Builders;
using Oxide.Ext.Discord.Exceptions.Entities.Guild;
using Oxide.Ext.Discord.Interfaces;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#get-guild-prune-count">Guild Prune Get</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildPruneGet : IDiscordQueryString, IDiscordValidation
    {
        /// <summary>
        /// Number of days to count prune for (1 - 30)
        /// </summary>
        [JsonProperty("days")]
        public int Days { get; set; } = 7;
        
        /// <summary>
        /// List of roles to include
        /// </summary>
        [JsonProperty("include_roles")]
        public List<Snowflake> IncludeRoles { get; set; }
        
        /// <inheritdoc/>
        public virtual string ToQueryString()
        {
            Validate();
            QueryStringBuilder builder = QueryStringBuilder.Create();
            builder.Add("days", Days.ToString());
            if (IncludeRoles != null)
            {
                builder.AddList("include_roles", IncludeRoles, ",");
            }

            return builder.ToStringAndFree();
        }

        ///<inheritdoc/>
        public void Validate()
        {
            InvalidGuildPruneException.ThrowIfInvalidDays(Days);
        }
    }
}