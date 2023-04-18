using System.Collections.Generic;
using Oxide.Ext.Discord.Entities.Applications.RoleConnection;

namespace Oxide.Ext.Discord.Exceptions.Entities.Applications
{
    public class DiscordApplicationException : BaseDiscordException
    {
        private DiscordApplicationException(string message) : base(message) { }

        internal static void ThrowIfInvalidApplicationRoleConnectionMetadataLength(List<ApplicationRoleConnectionMetadata> records)
        {
            const int maxLength = 5;
            
            if (records == null)
            {
                throw new DiscordApplicationException($"{nameof(records)} cannot be null");
            }

            if (records.Count > 5)
            {
                throw new DiscordApplicationException($"{nameof(records)} cannot have more than 5 records");
            }
        }
    }
}