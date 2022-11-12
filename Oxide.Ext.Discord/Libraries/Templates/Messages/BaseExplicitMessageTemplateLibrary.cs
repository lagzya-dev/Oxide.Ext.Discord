using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Libraries.Langs;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Promise;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages
{
    public abstract class BaseExplicitMessageTemplateLibrary<TTemplate, TEntity> : BaseMessageTemplateLibrary<TTemplate, TEntity>
        where TTemplate : BaseMessageTemplate<TEntity>, new()
        where TEntity : class, new()
    {
        protected BaseExplicitMessageTemplateLibrary(TemplateType type, ILogger logger) : base(type, logger) { }
        
        public TEntity GetGlobalEntity(Plugin plugin, string templateName, PlaceholderData data = null, TEntity entity = null) => GetGlobalTemplate(plugin, templateName).ToEntity(data, entity);
        public IDiscordPromise<List<TEntity>> GetGlobalBulkEntityAsync(Plugin plugin, BulkTemplateRequest request, List<TEntity> entities = null) => GetGlobalBulkEntityInternalAsync(plugin, request, entities, DiscordPromise<List<TEntity>>.Create());
        
        public TEntity GetPlayerEntity(Plugin plugin, string templateName, string playerId, PlaceholderData data = null, TEntity entity = null) => GetPlayerTemplate(plugin, templateName, playerId).ToEntity(data, entity);
        public IDiscordPromise<List<TEntity>> GetBulkEntityForPlayerAsync(Plugin plugin, BulkTemplateRequest request, string playerId, List<TEntity> entities = null) 
            => GetLocalizedBulkEntityAsync(plugin, request, entities, DiscordExtension.DiscordLang.GetPlayerLanguage(playerId));
        
        public TEntity GetLocalizedEntity(Plugin plugin, string templateName, string language = DiscordLang.DefaultOxideLanguage, PlaceholderData data = null, TEntity entity = null) 
            => GetLocalizedTemplate(plugin, templateName, language).ToEntity(data, entity);
        public IDiscordPromise<List<TEntity>> GetLocalizedBulkEntityAsync(Plugin plugin, BulkTemplateRequest request, List<TEntity> entities = null, string language = DiscordLang.DefaultOxideLanguage) 
            => GetLocalizedBulkEntityInternalAsync(plugin, request, entities, language, DiscordPromise<List<TEntity>>.Create());
        
        public TEntity GetLocalizedEntity(Plugin plugin, string templateName, DiscordInteraction interaction, PlaceholderData data = null, TEntity entity = null) => GetLocalizedTemplate(plugin, templateName, interaction).ToEntity(data, entity);
        public IDiscordPromise<List<TEntity>> GetLocalizedBulkEntityAsync(Plugin plugin, DiscordInteraction interaction, BulkTemplateRequest request, List<TEntity> entities) => GetLocalizedBulkEntityInternalAsync(plugin, interaction, request, entities, DiscordPromise<List<TEntity>>.Create());
    }
}