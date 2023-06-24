# BaseMessageTemplateLibrary&lt;TTemplate&gt; class

Library for Discord Message templates

```csharp
public abstract class BaseMessageTemplateLibrary<TTemplate> : BaseTemplateLibrary<TTemplate>
    where TTemplate : class, new()
```

## Public Members

| name | description |
| --- | --- |
| [GetGlobalTemplate](#GetGlobalTemplate)(…) | Returns a global message template for the plugin with the given name |
| [GetLocalizedTemplate](#GetLocalizedTemplate)(…) | Returns a message template for a given language (2 methods) |
| [GetPlayerTemplate](#GetPlayerTemplate)(…) | Returns a message template for a given IPlayer player (2 methods) |
| [RegisterGlobalTemplateAsync](#RegisterGlobalTemplateAsync)(…) | Registers a global message template Global Message templates cannot be localized |
| [RegisterLocalizedTemplateAsync](#RegisterLocalizedTemplateAsync)(…) | Registers a message template with the given name and language |

## Protected Members

| name | description |
| --- | --- |
| override [OnPluginUnloaded](#OnPluginUnloaded)(…) |  |

## See Also

* class [BaseTemplateLibrary&lt;TTemplate&gt;](./BaseTemplateLibrary%7BTTemplate%7D.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates](./TemplatesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [BaseMessageTemplateLibrary.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Libraries/Templates/BaseMessageTemplateLibrary.cs)
   
   
# RegisterGlobalTemplateAsync method

Registers a global message template Global Message templates cannot be localized

```csharp
public IPromise RegisterGlobalTemplateAsync(Plugin plugin, string templateName, TTemplate template, 
    TemplateVersion version, TemplateVersion minVersion)
```

| parameter | description |
| --- | --- |
| plugin | Plugin the template is for |
| templateName | Unique name of the template |
| template | Template to register |
| version | Version of the template |
| minVersion | Min supported version for this template |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException |  |

## See Also

* interface [IPromise](../../Interfaces/Promises/IPromise.md)
* struct [TemplateVersion](./TemplateVersion.md)
* class [BaseMessageTemplateLibrary&lt;TTemplate&gt;](./BaseMessageTemplateLibrary%7BTTemplate%7D.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates](./TemplatesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RegisterLocalizedTemplateAsync method

Registers a message template with the given name and language

```csharp
public IPromise RegisterLocalizedTemplateAsync(Plugin plugin, string templateName, 
    TTemplate template, TemplateVersion version, TemplateVersion minVersion, string language = "en")
```

| parameter | description |
| --- | --- |
| plugin | Plugin the [`DiscordMessageTemplate`](./Messages/DiscordMessageTemplate.md) is for |
| templateName | Name of the [`DiscordMessageTemplate`](./Messages/DiscordMessageTemplate.md) |
| template | Template to be registered |
| version | Version of the template |
| minVersion | The minimum supported template version. If an existing template exists and it's version is &gt;= the minimum supported version then we will use that template. If an existing template exists and it's version is &lt; the min supported version. The existing version is backed up and a new template is created |
| language | Language for the template |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException |  |

## See Also

* interface [IPromise](../../Interfaces/Promises/IPromise.md)
* struct [TemplateVersion](./TemplateVersion.md)
* class [BaseMessageTemplateLibrary&lt;TTemplate&gt;](./BaseMessageTemplateLibrary%7BTTemplate%7D.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates](./TemplatesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetGlobalTemplate method

Returns a global message template for the plugin with the given name

```csharp
public TTemplate GetGlobalTemplate(Plugin plugin, string templateName)
```

| parameter | description |
| --- | --- |
| plugin | Plugin the template is for |
| templateName | Name of the template |

## Return Value

[`DiscordMessageTemplate`](./Messages/DiscordMessageTemplate.md)

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException |  |

## See Also

* class [BaseMessageTemplateLibrary&lt;TTemplate&gt;](./BaseMessageTemplateLibrary%7BTTemplate%7D.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates](./TemplatesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetPlayerTemplate method (1 of 2)

Returns a message template for a given IPlayer player

```csharp
public TTemplate GetPlayerTemplate(Plugin plugin, string templateName, IPlayer player)
```

| parameter | description |
| --- | --- |
| plugin | Plugin the template is for |
| templateName | Name of the template |
| player | IPlayer for the template |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if Plugin is null or name / language is null or empty |

## See Also

* class [BaseMessageTemplateLibrary&lt;TTemplate&gt;](./BaseMessageTemplateLibrary%7BTTemplate%7D.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates](./TemplatesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---
   
   
# GetLocalizedTemplate method (1 of 2)

Returns a message template for a given language

```csharp
public TTemplate GetLocalizedTemplate(Plugin plugin, string templateName, 
    DiscordInteraction interaction)
```

| parameter | description |
| --- | --- |
| plugin | Plugin the template is for |
| templateName | Name of the template |
| interaction | Interaction to get the template for |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if Plugin is null or name / language is null or empty |

## See Also

* class [DiscordInteraction](../../Entities/Interactions/DiscordInteraction.md)
* class [BaseMessageTemplateLibrary&lt;TTemplate&gt;](./BaseMessageTemplateLibrary%7BTTemplate%7D.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates](./TemplatesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---
   
   
# OnPluginUnloaded method

```csharp
protected override void OnPluginUnloaded(Plugin plugin)
```

## See Also

* class [BaseMessageTemplateLibrary&lt;TTemplate&gt;](./BaseMessageTemplateLibrary%7BTTemplate%7D.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates](./TemplatesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
