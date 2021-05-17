using System.Text;

namespace Oxide.Ext.Discord.Entities.Users
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/user#get-current-user-guilds-query-string-params">Users Guild Request</a>
    /// </summary>
    public class UserGuildsRequest
    {
        /// <summary>
        /// Get guilds before this guild ID
        /// </summary>
        public Snowflake? Before { get; set; }
        
        /// <summary>
        /// Get guilds after this guild ID
        /// </summary>
        public Snowflake? After { get; set; }

        /// <summary>
        /// Max number of guilds to return (1-200)
        /// </summary>
        public int Limit { get; set; } = 200;

        public virtual string ToQueryString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("?limit=");
            sb.Append(Limit.ToString());

            if (Before.HasValue)
            {
                sb.Append("&before=");
                sb.Append(Before.ToString());
            }
            
            if (After.HasValue)
            {
                sb.Append("&after=");
                sb.Append(After.ToString());
            }

            return sb.ToString();
        }
    }
}