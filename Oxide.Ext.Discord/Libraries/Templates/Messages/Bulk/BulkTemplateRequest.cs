using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk
{
    public class BulkTemplateRequest : BasePoolable
    {
        public List<BulkTemplateItem> Items = new List<BulkTemplateItem>();

        public static BulkTemplateRequest Create(Plugin plugin) => DiscordPool.Instance.GetOrCreate(plugin).Get<BulkTemplateRequest>();

        public void AddItem(string templateName, PlaceholderData data = null)
        {
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException();
            Items.Add(new BulkTemplateItem(templateName, data));
        }
        
        protected override void EnterPool()
        {
            Items.Clear();
        }
    }
}