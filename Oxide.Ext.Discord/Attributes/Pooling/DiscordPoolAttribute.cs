using System;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Attributes.Pooling
{
    /// <summary>
    /// Attribute for setting <see cref="DiscordPluginPool"/> on a plugin
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DiscordPoolAttribute : BaseDiscordAttribute
    {
        
    }
}