# DiscordAutoCompleteChoiceTemplates class

Auto Complete Choice Templates Library

```csharp
public class DiscordAutoCompleteChoiceTemplates : 
    BaseTemplateLibrary<DiscordAutoCompleteChoiceTemplate>
```

## Public Members

| name | description |
| --- | --- |
| [DiscordAutoCompleteChoiceTemplates](#discordautocompletechoicetemplates-constructor)(…) | Constructor |
| [ApplyGlobal](#applyglobal-method)(…) | Applies a Global Template to a [`CommandOptionChoice`](../../../Entities/Interactions/ApplicationCommands/CommandOptionChoice.md) with optional placeholders |
| [ApplyLocalized](#applylocalized-method-1-of-2)(…) | Applies a Localized Template to a [`CommandOptionChoice`](../../../Entities/Interactions/ApplicationCommands/CommandOptionChoice.md) with optional placeholders (2 methods) |
| [GetGlobalTemplate](#getglobaltemplate-method)(…) | Returns a global Auto Complete Template for the given plugin and template name |
| [RegisterGlobalTemplate](#registerglobaltemplate-method)(…) | Registers a global template for Auto Complete Choices |
| [RegisterLocalizedTemplate](#registerlocalizedtemplate-method)(…) | Registers a global template for Auto Complete Choices |

## Protected Members

| name | description |
| --- | --- |
| override [OnPluginUnloaded](#onpluginunloaded-method)(…) |  |

## See Also

* class [BaseTemplateLibrary&lt;TTemplate&gt;](../BaseTemplateLibrary%7BTTemplate%7D.md)
* class [DiscordAutoCompleteChoiceTemplate](./DiscordAutoCompleteChoiceTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.AutoComplete](./AutoCompleteNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [DiscordAutoCompleteChoiceTemplates.cs](../../../../Oxide.Ext.Discord/Libraries/Templates/AutoComplete/DiscordAutoCompleteChoiceTemplates.cs)
   
   
# RegisterGlobalTemplate method

Registers a global template for Auto Complete Choices

```csharp
public IPromise RegisterGlobalTemplate(Plugin plugin, string templateName, 
    DiscordAutoCompleteChoiceTemplate template, TemplateVersion version, TemplateVersion minVersion)
```

| parameter | description |
| --- | --- |
| plugin | Plugin the template is for |
| templateName | Name of the template |
| template | The template to register |
| version | Current version of the template |
| minVersion | Minimum supported version of the template |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Throw if plugin or templateName is null |

## See Also

* interface [IPromise](../../../Interfaces/Promises/IPromise.md)
* class [DiscordAutoCompleteChoiceTemplate](./DiscordAutoCompleteChoiceTemplate.md)
* struct [TemplateVersion](../TemplateVersion.md)
* class [DiscordAutoCompleteChoiceTemplates](./DiscordAutoCompleteChoiceTemplates.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.AutoComplete](./AutoCompleteNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# RegisterLocalizedTemplate method

Registers a global template for Auto Complete Choices

```csharp
public IPromise RegisterLocalizedTemplate(Plugin plugin, string templateName, 
    DiscordAutoCompleteChoiceTemplate template, TemplateVersion version, 
    TemplateVersion minVersion, string language = "en")
```

| parameter | description |
| --- | --- |
| plugin | Plugin the template is for |
| templateName | Name of the template |
| template | The template to register |
| version | Current version of the template |
| minVersion | Minimum supported version of the template |
| language | Server Language for the localized template |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Throw if plugin, templateName, or language is null/empty |

## See Also

* interface [IPromise](../../../Interfaces/Promises/IPromise.md)
* class [DiscordAutoCompleteChoiceTemplate](./DiscordAutoCompleteChoiceTemplate.md)
* struct [TemplateVersion](../TemplateVersion.md)
* class [DiscordAutoCompleteChoiceTemplates](./DiscordAutoCompleteChoiceTemplates.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.AutoComplete](./AutoCompleteNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# GetGlobalTemplate method

Returns a global Auto Complete Template for the given plugin and template name

```csharp
public DiscordAutoCompleteChoiceTemplate GetGlobalTemplate(Plugin plugin, string templateName)
```

| parameter | description |
| --- | --- |
| plugin | Plugin for the template |
| templateName | Name of the template |

## See Also

* class [DiscordAutoCompleteChoiceTemplate](./DiscordAutoCompleteChoiceTemplate.md)
* class [DiscordAutoCompleteChoiceTemplates](./DiscordAutoCompleteChoiceTemplates.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.AutoComplete](./AutoCompleteNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ApplyGlobal method

Applies a Global Template to a [`CommandOptionChoice`](../../../Entities/Interactions/ApplicationCommands/CommandOptionChoice.md) with optional placeholders

```csharp
public CommandOptionChoice ApplyGlobal(Plugin plugin, string templateName, 
    CommandOptionChoice choice = null, PlaceholderData placeholders = null)
```

| parameter | description |
| --- | --- |
| plugin | Plugin for the template |
| templateName | Name of the template |
| choice | Choice to be applied to |
| placeholders | Placeholders to apply |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if plugin or templateName is null/empty |

## See Also

* class [CommandOptionChoice](../../../Entities/Interactions/ApplicationCommands/CommandOptionChoice.md)
* class [PlaceholderData](../../Placeholders/PlaceholderData.md)
* class [DiscordAutoCompleteChoiceTemplates](./DiscordAutoCompleteChoiceTemplates.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.AutoComplete](./AutoCompleteNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ApplyLocalized method (1 of 2)

Applies a Localized Template to a [`CommandOptionChoice`](../../../Entities/Interactions/ApplicationCommands/CommandOptionChoice.md) with optional placeholders

```csharp
public CommandOptionChoice ApplyLocalized(Plugin plugin, string templateName, 
    CommandOptionChoice choice = null, PlaceholderData placeholders = null, string language = "en")
```

| parameter | description |
| --- | --- |
| plugin | Plugin for the template |
| templateName | Name of the template |
| choice | Choice to be applied to |
| placeholders | Placeholders to apply |
| language | Server Language to apply |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if plugin or templateName is null/empty |

## See Also

* class [CommandOptionChoice](../../../Entities/Interactions/ApplicationCommands/CommandOptionChoice.md)
* class [PlaceholderData](../../Placeholders/PlaceholderData.md)
* class [DiscordAutoCompleteChoiceTemplates](./DiscordAutoCompleteChoiceTemplates.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.AutoComplete](./AutoCompleteNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# ApplyLocalized method (2 of 2)

Applies a Localized Template to a [`CommandOptionChoice`](../../../Entities/Interactions/ApplicationCommands/CommandOptionChoice.md) with optional placeholders

```csharp
public CommandOptionChoice ApplyLocalized(Plugin plugin, string templateName, 
    DiscordInteraction interaction, CommandOptionChoice choice = null, 
    PlaceholderData placeholders = null)
```

| parameter | description |
| --- | --- |
| plugin | Plugin for the template |
| templateName | Name of the template |
| interaction | Interaction for the localization |
| choice | Choice to be applied to |
| placeholders | Placeholders to apply |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if plugin or templateName is null/empty |

## See Also

* class [CommandOptionChoice](../../../Entities/Interactions/ApplicationCommands/CommandOptionChoice.md)
* class [DiscordInteraction](../../../Entities/Interactions/DiscordInteraction.md)
* class [PlaceholderData](../../Placeholders/PlaceholderData.md)
* class [DiscordAutoCompleteChoiceTemplates](./DiscordAutoCompleteChoiceTemplates.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.AutoComplete](./AutoCompleteNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# OnPluginUnloaded method

```csharp
protected override void OnPluginUnloaded(Plugin plugin)
```

## See Also

* class [DiscordAutoCompleteChoiceTemplates](./DiscordAutoCompleteChoiceTemplates.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.AutoComplete](./AutoCompleteNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# DiscordAutoCompleteChoiceTemplates constructor

Constructor

```csharp
public DiscordAutoCompleteChoiceTemplates(ILogger logger)
```

| parameter | description |
| --- | --- |
| logger |  |

## See Also

* interface [ILogger](../../../Logging/ILogger.md)
* class [DiscordAutoCompleteChoiceTemplates](./DiscordAutoCompleteChoiceTemplates.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.AutoComplete](./AutoCompleteNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
