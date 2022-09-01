using Oxide.Ext.Discord.Builders;
using Oxide.Ext.Discord.Exceptions.Entities.Guild;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Pooling;
namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#search-guild-members-query-string-params">Search Guild Members</a> Structure
    /// </summary>
    public class GuildSearchMembers : IDiscordQueryString, IDiscordValidation
    {
        /// <summary>
        /// Query string to match username(s) and nickname(s) against.
        /// </summary>
        public string Query { get; set; }
        
        /// <summary>
        /// Max number of members to return (1-1000)
        /// Default is 1
        /// </summary>
        public int? Limit { get; set; }
        
        ///<inheritdoc/>
        public string ToQueryString()
        {
            Validate();
            QueryStringBuilder builder = QueryStringBuilder.Create();
            builder.Add("query", Query);
            
            if (Limit.HasValue)
            {
                builder.Add("limit", Limit.Value.ToString());
            }

            return builder.ToStringAndFree();
        }

        /// <inheritdoc/>
        public void Validate()
        {
            InvalidGuildSearchMembersException.ThrowIfInvalidQuery(Query);
            InvalidGuildSearchMembersException.ThrowIfInvalidLimit(Limit);
        }
    }
}