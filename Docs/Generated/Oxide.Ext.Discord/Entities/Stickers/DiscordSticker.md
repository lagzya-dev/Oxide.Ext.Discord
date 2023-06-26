# DiscordSticker class

Represents a [Discord Sticker Structure](https://discord.com/developers/docs/resources/sticker#sticker-object)

```csharp
public class DiscordSticker : ISnowflakeEntity
```

## Public Members

| name | description |
| --- | --- |
| [DiscordSticker](#discordsticker-constructor)() | The default constructor. |
| [Available](#available-property) { get; set; } | Whether or not the sticker is available |
| [Description](#description-property) { get; set; } | Description of the sticker |
| [FormatType](#formattype-property) { get; set; } | Type of sticker format [`StickerFormatType`](./StickerFormatType.md) |
| [GuildId](#guildid-property) { get; set; } | Id of the guild that owns this sticker |
| [Id](#id-property) { get; set; } | ID of the sticker |
| [Name](#name-property) { get; set; } | Name of the sticker |
| [PackId](#packid-property) { get; set; } | ID of the pack the sticker is from |
| [SortValue](#sortvalue-property) { get; set; } | A sticker's sort order within a pack |
| [StickerUrl](#stickerurl-property) { get; } | Returns the Url for the sticker |
| [Tags](#tags-property) { get; set; } | For guild stickers, a unicode emoji representing the sticker's expression. For nitro stickers, a comma-separated list of related expressions. autocomplete/suggestion tags for the sticker (max 200 characters) |
| [Type](#type-property) { get; set; } | Type of sticker. |
| [User](#user-property) { get; set; } | The user that uploaded the sticker |
| [DeleteGuildSticker](#deleteguildsticker-method)(…) | Delete the given sticker. Requires the MANAGE_EMOJIS_AND_STICKERS permission. See [Delete Guild Sticker](https://discord.com/developers/docs/resources/sticker#delete-guild-sticker) |
| [ModifyGuildSticker](#modifyguildsticker-method)(…) | Modify the given sticker. Requires the MANAGE_EMOJIS_AND_STICKERS permission. Returns the updated sticker object on success. See [Modify Guild Sticker](https://discord.com/developers/docs/resources/sticker#modify-guild-sticker) |
| static [Get](#get-method)(…) | Returns a sticker object for the given sticker ID. See [Get Sticker](https://discord.com/developers/docs/resources/sticker#get-sticker) |

## See Also

* interface [ISnowflakeEntity](../../Interfaces/ISnowflakeEntity.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordSticker.cs](../../../../Oxide.Ext.Discord/Entities/Stickers/DiscordSticker.cs)
   
   
# Get method

Returns a sticker object for the given sticker ID. See [Get Sticker](https://discord.com/developers/docs/resources/sticker#get-sticker)

```csharp
public static IPromise<DiscordSticker> Get(DiscordClient client, Snowflake stickerId)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| stickerId | ID of the sticker |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* struct [Snowflake](../Snowflake.md)
* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ModifyGuildSticker method

Modify the given sticker. Requires the MANAGE_EMOJIS_AND_STICKERS permission. Returns the updated sticker object on success. See [Modify Guild Sticker](https://discord.com/developers/docs/resources/sticker#modify-guild-sticker)

```csharp
public IPromise<DiscordSticker> ModifyGuildSticker(DiscordClient client)
```

| parameter | description |
| --- | --- |
| client | Client to use |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# DeleteGuildSticker method

Delete the given sticker. Requires the MANAGE_EMOJIS_AND_STICKERS permission. See [Delete Guild Sticker](https://discord.com/developers/docs/resources/sticker#delete-guild-sticker)

```csharp
public IPromise DeleteGuildSticker(DiscordClient client)
```

| parameter | description |
| --- | --- |
| client | Client to use |

## See Also

* interface [IPromise](../../Interfaces/Promises/IPromise.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# DiscordSticker constructor

The default constructor.

```csharp
public DiscordSticker()
```

## See Also

* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Id property

ID of the sticker

```csharp
public Snowflake Id { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# PackId property

ID of the pack the sticker is from

```csharp
public Snowflake? PackId { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Name property

Name of the sticker

```csharp
public string Name { get; set; }
```

## See Also

* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Description property

Description of the sticker

```csharp
public string Description { get; set; }
```

## See Also

* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Tags property

For guild stickers, a unicode emoji representing the sticker's expression. For nitro stickers, a comma-separated list of related expressions. autocomplete/suggestion tags for the sticker (max 200 characters)

```csharp
public string Tags { get; set; }
```

## See Also

* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Type property

Type of sticker.

```csharp
public StickerType Type { get; set; }
```

## See Also

* enum [StickerType](./StickerType.md)
* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# FormatType property

Type of sticker format [`StickerFormatType`](./StickerFormatType.md)

```csharp
public StickerFormatType FormatType { get; set; }
```

## See Also

* enum [StickerFormatType](./StickerFormatType.md)
* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Available property

Whether or not the sticker is available

```csharp
public bool? Available { get; set; }
```

## See Also

* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GuildId property

Id of the guild that owns this sticker

```csharp
public Snowflake? GuildId { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# User property

The user that uploaded the sticker

```csharp
public DiscordUser User { get; set; }
```

## See Also

* class [DiscordUser](../Users/DiscordUser.md)
* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# SortValue property

A sticker's sort order within a pack

```csharp
public int? SortValue { get; set; }
```

## See Also

* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# StickerUrl property

Returns the Url for the sticker

```csharp
public string StickerUrl { get; }
```

## See Also

* class [DiscordSticker](./DiscordSticker.md)
* namespace [Oxide.Ext.Discord.Entities.Stickers](./StickersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
