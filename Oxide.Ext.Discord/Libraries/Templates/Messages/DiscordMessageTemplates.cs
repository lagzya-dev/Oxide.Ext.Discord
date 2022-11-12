using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Interfaces.Entities.Templates;
using Oxide.Ext.Discord.Libraries.Langs;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Promise;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages
{
    /// <summary>
    /// Modal Templates Library
    /// </summary>
    public class DiscordMessageTemplates : BaseMessageTemplateLibrary<DiscordMessageTemplate, IDiscordMessageTemplate>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        internal DiscordMessageTemplates(ILogger logger) : base(TemplateType.Message, logger) { }
        
        public T GetGlobalEntity<T>(Plugin plugin, string templateName, PlaceholderData data = null, T entity = null) where T : class, IDiscordMessageTemplate, new() => GetGlobalTemplate(plugin, templateName).ToMessage(data, entity);
        public IDiscordPromise<List<T>> GetGlobalBulkEntityAsync<T>(Plugin plugin, BulkTemplateRequest request, List<T> entities = null) where T : class, IDiscordMessageTemplate, new() => GetGlobalBulkEntityInternalAsync(plugin, request, entities, DiscordPromise<List<T>>.Create());
        
        public T GetPlayerEntity<T>(Plugin plugin, string templateName, string playerId, PlaceholderData data = null, T entity = null) where T : class, IDiscordMessageTemplate, new() => GetPlayerTemplate(plugin, templateName, playerId).ToMessage(data, entity);
        public IDiscordPromise<List<T>> GetBulkEntityForPlayerAsync<T>(Plugin plugin, BulkTemplateRequest request, string playerId, List<T> entities = null) where T : class, IDiscordMessageTemplate, new() 
            => GetLocalizedBulkEntityAsync(plugin, request, entities, DiscordExtension.DiscordLang.GetPlayerLanguage(playerId));
        
        public T GetLocalizedEntity<T>(Plugin plugin, string templateName, string language = DiscordLang.DefaultOxideLanguage, PlaceholderData data = null, T entity = null) where T : class, IDiscordMessageTemplate, new() 
            => GetLocalizedTemplate(plugin, templateName, language).ToMessage(data, entity);
        public IDiscordPromise<List<T>> GetLocalizedBulkEntityAsync<T>(Plugin plugin, BulkTemplateRequest request, List<T> entities = null, string language = DiscordLang.DefaultOxideLanguage) where T : class, IDiscordMessageTemplate, new() 
            => GetLocalizedBulkEntityInternalAsync(plugin, request, entities, language, DiscordPromise<List<T>>.Create());

        public T GetLocalizedEntity<T>(Plugin plugin, string templateName, DiscordInteraction interaction, PlaceholderData data = null, T entity = null) where T : class, IDiscordMessageTemplate, new() => GetLocalizedTemplate(plugin, templateName, interaction).ToMessage(data, entity);
        public IDiscordPromise<List<T>> GetLocalizedBulkEntityAsync<T>(Plugin plugin, DiscordInteraction interaction, BulkTemplateRequest request, List<T> entities) where T : class, IDiscordMessageTemplate, new() 
            => GetLocalizedBulkEntityInternalAsync(plugin, interaction, request, entities, DiscordPromise<List<T>>.Create());
    }
}