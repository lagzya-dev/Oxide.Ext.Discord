using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk
{
    /// <summary>
    /// Represents a Bulk Template Request
    /// </summary>
    public class BulkTemplateRequest : BasePoolable
    {
        /// <summary>
        /// Items for the bulk template request
        /// </summary>
        public readonly List<BulkTemplateItem> Items = new List<BulkTemplateItem>();

        /// <summary>
        /// Returns a pooled request for the given plugin
        /// </summary>
        /// <param name="plugin"></param>
        /// <returns></returns>
        public static BulkTemplateRequest Create(Plugin plugin) => DiscordPool.Instance.GetOrCreate(plugin).Get<BulkTemplateRequest>();

        /// <summary>
        /// Adds an item to the request
        /// </summary>
        /// <param name="templateName">Name of the template</param>
        /// <param name="data"><see cref="PlaceholderData"/> for the template</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddItem(string templateName, PlaceholderData data = null)
        {
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException();
            Items.Add(new BulkTemplateItem(templateName, data));
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            Items.Clear();
        }
    }
}