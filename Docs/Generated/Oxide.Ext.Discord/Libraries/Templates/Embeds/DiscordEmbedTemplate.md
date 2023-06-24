# DiscordEmbedTemplate class

Discord Template for embed

```csharp
public class DiscordEmbedTemplate : IBulkTemplate<DiscordEmbed>
```

## Public Members

| name | description |
| --- | --- |
| [DiscordEmbedTemplate](#DiscordEmbedTemplate)() | Constructor |
| [DiscordEmbedTemplate](#DiscordEmbedTemplate)(…) | Constructor |
| [Color](#Color) { get; set; } | The Hex Color for the embed |
| [Description](#Description) { get; set; } | The description of the embed |
| [Enabled](#Enabled) { get; set; } | If this embed is enabled |
| [Fields](#Fields) { get; set; } | Fields for the embed |
| [Footer](#Footer) { get; set; } | Footer for the embed |
| [ImageUrl](#ImageUrl) { get; set; } | Image URL to show in the embed |
| [ThumbnailUrl](#ThumbnailUrl) { get; set; } | Thumbnail url to show in the embed |
| [TimeStamp](#TimeStamp) { get; set; } | Show timestamp in the embed |
| [Title](#Title) { get; set; } | The Tile for the embed |
| [Url](#Url) { get; set; } | This Title Url for the embed |
| [VideoUrl](#VideoUrl) { get; set; } | Video url to show in the embed |
| [ToEntity](#ToEntity)(…) | Converts the template to a [`DiscordEmbed`](../../../Entities/Messages/Embeds/DiscordEmbed.md) |
| [ToEntityBulk](#ToEntityBulk)(…) |  |

## See Also

* interface [IBulkTemplate&lt;T&gt;](../../../Interfaces/Templates/IBulkTemplate%7BT%7D.md)
* class [DiscordEmbed](../../../Entities/Messages/Embeds/DiscordEmbed.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Embeds](./EmbedsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [DiscordEmbedTemplate.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Libraries/Templates/Embeds/DiscordEmbedTemplate.cs)
   
   
# ToEntity method

Converts the template to a [`DiscordEmbed`](../../../Entities/Messages/Embeds/DiscordEmbed.md)

```csharp
public DiscordEmbed ToEntity(PlaceholderData data = null, DiscordEmbed embed = null)
```

| parameter | description |
| --- | --- |
| data | Data to use |
| embed | Initial embed to use |

## See Also

* class [DiscordEmbed](../../../Entities/Messages/Embeds/DiscordEmbed.md)
* class [PlaceholderData](../../Placeholders/PlaceholderData.md)
* class [DiscordEmbedTemplate](./DiscordEmbedTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Embeds](./EmbedsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ToEntityBulk method

```csharp
public IPromise<List<DiscordEmbed>> ToEntityBulk(List<PlaceholderData> data = null)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordEmbed](../../../Entities/Messages/Embeds/DiscordEmbed.md)
* class [PlaceholderData](../../Placeholders/PlaceholderData.md)
* class [DiscordEmbedTemplate](./DiscordEmbedTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Embeds](./EmbedsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# DiscordEmbedTemplate constructor (1 of 2)

Constructor

```csharp
public DiscordEmbedTemplate()
```

## See Also

* class [DiscordEmbedTemplate](./DiscordEmbedTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Embeds](./EmbedsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---
   
   
# Enabled property

If this embed is enabled

```csharp
public bool Enabled { get; set; }
```

## See Also

* class [DiscordEmbedTemplate](./DiscordEmbedTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Embeds](./EmbedsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Title property

The Tile for the embed

```csharp
public string Title { get; set; }
```

## See Also

* class [DiscordEmbedTemplate](./DiscordEmbedTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Embeds](./EmbedsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Url property

This Title Url for the embed

```csharp
public string Url { get; set; }
```

## See Also

* class [DiscordEmbedTemplate](./DiscordEmbedTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Embeds](./EmbedsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Description property

The description of the embed

```csharp
public string Description { get; set; }
```

## See Also

* class [DiscordEmbedTemplate](./DiscordEmbedTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Embeds](./EmbedsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Color property

The Hex Color for the embed

```csharp
public string Color { get; set; }
```

## See Also

* class [DiscordEmbedTemplate](./DiscordEmbedTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Embeds](./EmbedsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ImageUrl property

Image URL to show in the embed

```csharp
public string ImageUrl { get; set; }
```

## See Also

* class [DiscordEmbedTemplate](./DiscordEmbedTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Embeds](./EmbedsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ThumbnailUrl property

Thumbnail url to show in the embed

```csharp
public string ThumbnailUrl { get; set; }
```

## See Also

* class [DiscordEmbedTemplate](./DiscordEmbedTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Embeds](./EmbedsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# VideoUrl property

Video url to show in the embed

```csharp
public string VideoUrl { get; set; }
```

## See Also

* class [DiscordEmbedTemplate](./DiscordEmbedTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Embeds](./EmbedsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# TimeStamp property

Show timestamp in the embed

```csharp
public bool TimeStamp { get; set; }
```

## See Also

* class [DiscordEmbedTemplate](./DiscordEmbedTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Embeds](./EmbedsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Fields property

Fields for the embed

```csharp
public List<DiscordEmbedFieldTemplate> Fields { get; set; }
```

## See Also

* class [DiscordEmbedFieldTemplate](./DiscordEmbedFieldTemplate.md)
* class [DiscordEmbedTemplate](./DiscordEmbedTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Embeds](./EmbedsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Footer property

Footer for the embed

```csharp
public EmbedFooterTemplate Footer { get; set; }
```

## See Also

* class [EmbedFooterTemplate](./EmbedFooterTemplate.md)
* class [DiscordEmbedTemplate](./DiscordEmbedTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Embeds](./EmbedsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
