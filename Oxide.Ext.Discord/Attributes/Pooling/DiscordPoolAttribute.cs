using System;
using Oxide.Ext.Discord.Types;

namespace Oxide.Ext.Discord.Attributes
{
    /// <summary>
    /// Attribute for setting <see cref="DiscordPluginPool"/> on a plugin
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DiscordPoolAttribute : BaseDiscordAttribute
    {
        
    }
}