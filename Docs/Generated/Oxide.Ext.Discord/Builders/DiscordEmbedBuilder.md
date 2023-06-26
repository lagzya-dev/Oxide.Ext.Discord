# DiscordEmbedBuilder class

Builds a new DiscordEmbed

```csharp
public class DiscordEmbedBuilder
```

## Public Members

| name | description |
| --- | --- |
| [DiscordEmbedBuilder](#DiscordEmbedBuilder-constructor)() | Constructor for the builder creating a new embed |
| [DiscordEmbedBuilder](#DiscordEmbedBuilder-constructor)(…) | Constructor for the builder using an existing embed |
| [AddAuthor](#AddAuthor-method)(…) | Adds an author to the embed message. The author will appear above the title |
| [AddBlankField](#AddBlankField-method)(…) | Adds a blank field. If inline it will add a blank column. If not inline will add a blank row |
| [AddColor](#AddColor-method)(…) | Adds a Discord Color to the embed (7 methods) |
| [AddDescription](#AddDescription-method)(…) | Adds a description to the embed message |
| [AddField](#AddField-method)(…) | Adds a new field with the name as the title and value as the value. If inline will add a new column. If row will add in a new row. |
| [AddFooter](#AddFooter-method)(…) | Adds a footer to the embed message |
| [AddImage](#AddImage-method)(…) | Adds an image to the embed. The url should point to the url of the image. If using attachment image you can make the url: "attachment://{image name}.{image extension} |
| [AddNowTimestamp](#AddNowTimestamp-method)() | Adds a timestamp to an embed with the current time |
| [AddProvider](#AddProvider-method)(…) | Adds a provider to the embed |
| [AddThumbnail](#AddThumbnail-method)(…) | Adds a thumbnail in the top right corner of the embed If using attachment image you can make the url: "attachment://{image name}.{image extension} |
| [AddTimestamp](#AddTimestamp-method)(…) | Adds a timestamp to an embed with the given time |
| [AddTitle](#AddTitle-method)(…) | Adds a title to the embed message |
| [AddUrl](#AddUrl-method)(…) | Adds a url to the embed message |
| [AddVideo](#AddVideo-method)(…) | Adds a video to the embed |
| [Build](#Build-method)() | Returns the built embed |
| [BuildList](#BuildList-method)() | Returns the built embed in a list |

## See Also

* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [DiscordEmbedBuilder.cs](../../../../Oxide.Ext.Discord/Builders/DiscordEmbedBuilder.cs)
   
   
# AddTitle method

Adds a title to the embed message

```csharp
public DiscordEmbedBuilder AddTitle(string title)
```

| parameter | description |
| --- | --- |
| title | Title to add |

## Return Value

This

## See Also

* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddDescription method

Adds a description to the embed message

```csharp
public DiscordEmbedBuilder AddDescription(string description)
```

| parameter | description |
| --- | --- |
| description | description to add |

## Return Value

This

## See Also

* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddUrl method

Adds a url to the embed message

```csharp
public DiscordEmbedBuilder AddUrl(string url)
```

| parameter | description |
| --- | --- |
| url |  |

## Return Value

This

## See Also

* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddAuthor method

Adds an author to the embed message. The author will appear above the title

```csharp
public DiscordEmbedBuilder AddAuthor(string name, string url = null, string iconUrl = null, 
    string proxyIconUrl = null)
```

| parameter | description |
| --- | --- |
| name | Name of the author |
| url | Url to go to when the authors name is clicked on |
| iconUrl | Icon Url to use for the author |
| proxyIconUrl | Backup icon url. Can be left null if you only have one icon url |

## Return Value

This

## See Also

* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddFooter method

Adds a footer to the embed message

```csharp
public DiscordEmbedBuilder AddFooter(string text, string iconUrl = null, string proxyIconUrl = null)
```

| parameter | description |
| --- | --- |
| text | Text to be added to the footer |
| iconUrl | Icon url to add in the footer. Appears to the left of the text |
| proxyIconUrl | Backup icon url. Can be left null if you only have one icon url |

## Return Value

This

## See Also

* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddColor method (1 of 7)

Adds a Discord Color to the embed

```csharp
public DiscordEmbedBuilder AddColor(DiscordColor color)
```

| parameter | description |
| --- | --- |
| color |  |

## Return Value

This

## See Also

* struct [DiscordColor](../Entities/DiscordColor.md)
* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# AddNowTimestamp method

Adds a timestamp to an embed with the current time

```csharp
public DiscordEmbedBuilder AddNowTimestamp()
```

## Return Value

This

## See Also

* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddTimestamp method

Adds a timestamp to an embed with the given time

```csharp
public DiscordEmbedBuilder AddTimestamp(DateTime timestamp)
```

| parameter | description |
| --- | --- |
| timestamp | Timestamp to set for the embed |

## Return Value

This

## See Also

* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddBlankField method

Adds a blank field. If inline it will add a blank column. If not inline will add a blank row

```csharp
public DiscordEmbedBuilder AddBlankField(bool inline)
```

| parameter | description |
| --- | --- |
| inline | If the field is inline |

## Return Value

This

## See Also

* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddField method

Adds a new field with the name as the title and value as the value. If inline will add a new column. If row will add in a new row.

```csharp
public DiscordEmbedBuilder AddField(string name, string value, bool inline)
```

| parameter | description |
| --- | --- |
| name | Name of the field |
| value | Value of the field |
| inline | If the field should be inlined |

## Return Value

This

## See Also

* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddImage method

Adds an image to the embed. The url should point to the url of the image. If using attachment image you can make the url: "attachment://{image name}.{image extension}

```csharp
public DiscordEmbedBuilder AddImage(string url, int? width = null, int? height = null, 
    string proxyUrl = null)
```

| parameter | description |
| --- | --- |
| url | Url for the image |
| width | width of the image |
| height | height of the image |
| proxyUrl | Backup url for the image |

## Return Value

This

## See Also

* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddThumbnail method

Adds a thumbnail in the top right corner of the embed If using attachment image you can make the url: "attachment://{image name}.{image extension}

```csharp
public DiscordEmbedBuilder AddThumbnail(string url, int? width = null, int? height = null, 
    string proxyUrl = null)
```

| parameter | description |
| --- | --- |
| url | Url for the image |
| width | width of the image |
| height | height of the image |
| proxyUrl | Backup url for the image |

## Return Value

This

## See Also

* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddVideo method

Adds a video to the embed

```csharp
public DiscordEmbedBuilder AddVideo(string url, int? width = null, int? height = null, 
    string proxyUrl = null)
```

| parameter | description |
| --- | --- |
| url | Url for the video |
| width | Width of the video |
| height | Height of the video |
| proxyUrl | Proxy Url for the video |

## Return Value

This

## See Also

* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddProvider method

Adds a provider to the embed

```csharp
public DiscordEmbedBuilder AddProvider(string name, string url)
```

| parameter | description |
| --- | --- |
| name | Name for the provider |
| url | Url for the provider |

## Return Value

This

## See Also

* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Build method

Returns the built embed

```csharp
public DiscordEmbed Build()
```

## Return Value

[`DiscordEmbed`](../Entities/Messages/Embeds/DiscordEmbed.md)

## See Also

* class [DiscordEmbed](../Entities/Messages/Embeds/DiscordEmbed.md)
* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# BuildList method

Returns the built embed in a list

```csharp
public List<DiscordEmbed> BuildList()
```

## Return Value

List of [`DiscordEmbed`](../Entities/Messages/Embeds/DiscordEmbed.md)

## See Also

* class [DiscordEmbed](../Entities/Messages/Embeds/DiscordEmbed.md)
* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DiscordEmbedBuilder constructor (1 of 2)

Constructor for the builder creating a new embed

```csharp
public DiscordEmbedBuilder()
```

## See Also

* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
