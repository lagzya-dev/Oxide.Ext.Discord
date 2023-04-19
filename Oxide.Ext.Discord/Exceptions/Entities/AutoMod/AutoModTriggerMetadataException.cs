using System.Collections.Generic;
using Oxide.Ext.Discord.Entities.AutoMod;

namespace Oxide.Ext.Discord.Exceptions.Entities.AutoMod
{
    /// <summary>
    /// Exceptions for <see cref="AutoModTriggerMetadata"/>
    /// </summary>
    public class AutoModTriggerMetadataException : BaseDiscordException
    {
        private AutoModTriggerMetadataException(string message) : base(message) { }

        internal static void ThrowIfKeywordFilterInvalid(List<string> filter)
        {
            const int maxKeywordFilterLength = 1000;
            const int maxKeywordLength = 60;
            
            if (filter == null)
            {
                return;
            }

            if (filter.Count > maxKeywordFilterLength)
            {
                throw new AutoModTriggerMetadataException($"{nameof(AutoModTriggerMetadata)}.{nameof(AutoModTriggerMetadata.KeywordFilter)} cannot have more than {maxKeywordFilterLength} keywords");
            }

            for (int index = 0; index < filter.Count; index++)
            {
                string keyword = filter[index];
                if (keyword.Length > maxKeywordLength)
                {
                    throw new AutoModTriggerMetadataException($"{nameof(AutoModTriggerMetadata)}.{nameof(AutoModTriggerMetadata.KeywordFilter)} keyword {keyword} cannot have more than {maxKeywordLength} characters");
                }
            }
        }
        
        internal static void ThrowIfRegexPatternsInvalid(List<string> patterns)
        {
            const int maxPatternsLength = 10;
            const int maxRegexLength = 260;
            
            if (patterns == null)
            {
                return;
            }

            if (patterns.Count > maxPatternsLength)
            {
                throw new AutoModTriggerMetadataException($"{nameof(AutoModTriggerMetadata)}.{nameof(AutoModTriggerMetadata.RegexPatterns)} cannot have more than {maxPatternsLength} patterns");
            }

            for (int index = 0; index < patterns.Count; index++)
            {
                string regex = patterns[index];
                if (regex.Length > maxRegexLength)
                {
                    throw new AutoModTriggerMetadataException($"{nameof(AutoModTriggerMetadata)}.{nameof(AutoModTriggerMetadata.KeywordFilter)} regex {regex} cannot have more than {maxRegexLength} characters");
                }
            }
        }
        
        internal static void ThrowIfAllowListInvalid(List<string> allowList, AutoModTriggerType type)
        {
            if (allowList == null)
            {
                return;
            }

            switch (type)
            {
                case AutoModTriggerType.Keyword:
                    ThrowIfAllowListKeywordInvalid(allowList);
                    break;
                case AutoModTriggerType.KeywordPreset:
                    ThrowIfAllowListKeywordPresetInvalid(allowList);
                    break;
            }
        }

        private static void ThrowIfAllowListKeywordInvalid(List<string> allowList)
        {
            const int maxAllowListLength = 100;
            const int maxAllowLength = 60;
            
            if (allowList.Count > maxAllowListLength)
            {
                throw new AutoModTriggerMetadataException($"{nameof(AutoModTriggerMetadata)}.{nameof(AutoModTriggerMetadata.AllowList)} cannot have more than {maxAllowListLength} allowed strings");
            }

            for (int index = 0; index < allowList.Count; index++)
            {
                string str = allowList[index];
                if (str.Length > maxAllowLength)
                {
                    throw new AutoModTriggerMetadataException($"{nameof(AutoModTriggerMetadata)}.{nameof(AutoModTriggerMetadata.AllowList)} strings {str} cannot have more than {maxAllowLength} characters");
                }
            }
        }
        
        private static void ThrowIfAllowListKeywordPresetInvalid(List<string> allowList)
        {
            const int maxAllowListLength = 1000;
            const int maxAllowLength = 60;
            
            if (allowList.Count > maxAllowListLength)
            {
                throw new AutoModTriggerMetadataException($"{nameof(AutoModTriggerMetadata)}.{nameof(AutoModTriggerMetadata.AllowList)} cannot have more than {maxAllowListLength} allowed strings");
            }

            for (int index = 0; index < allowList.Count; index++)
            {
                string str = allowList[index];
                if (str.Length > maxAllowLength)
                {
                    throw new AutoModTriggerMetadataException($"{nameof(AutoModTriggerMetadata)}.{nameof(AutoModTriggerMetadata.AllowList)} strings {str} cannot have more than {maxAllowLength} characters");
                }
            }
        }

        internal static void ThrowIfInvalidMentionTotalLimit(int limit)
        {
            const int maxLimit = 50;
            
            if (limit > maxLimit)
            {
                throw new AutoModTriggerMetadataException($"{nameof(AutoModTriggerMetadata)}.{nameof(AutoModTriggerMetadata.AllowList)} cannot be more than {maxLimit}");
            }
        }
    }
}