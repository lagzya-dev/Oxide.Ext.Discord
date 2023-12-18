using Oxide.Ext.Discord.Builders;
using Oxide.Ext.Discord.Exceptions;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Libraries;

namespace Oxide.Ext.Discord.Entities
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/guild#list-guild-members-query-string-params">List Guild Members</a> Stucture
    /// </summary>
    public class GuildListMembers : IDiscordQueryString, IDiscordValidation
    {
        /// <summary>
        /// Max number of members to return (1-1000)
        /// Default is 1
        /// </summary>
        public int? Limit { get; set; }
        
        /// <summary>
        /// The highest user id in the previous page
        /// </summary>
        public Snowflake? After { get; set; }
        
        ///<inheritdoc/>
        public string ToQueryString()
        {
            Validate();
            QueryStringBuilder builder = QueryStringBuilder.Create(DiscordPool.Internal);
            if (Limit.HasValue)
            {
                builder.Add("limit", Limit.Value.ToString());
            }

            if (After.HasValue)
            {
                builder.Add("after", After.Value.ToString());
            }

            return builder.ToStringAndFree();
        }

        /// <inheritdoc/>
        public void Validate()
        {
            InvalidGuildListMembersException.ThrowIfInvalidLimit(Limit);
        }
    }
}