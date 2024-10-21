# DiscordEmoji class

Represents [Emoji Structure](https://discord.com/developers/docs/resources/emoji#emoji-object)

```csharp
public class DiscordEmoji : EmojiUpdate, ISnowflakeEntity
```

## Public Members

| name | description |
| --- | --- |
| [DiscordEmoji](#discordemoji-constructor)() | The default constructor. |
| static [FromCharacter](#fromcharacter-method)(…) | Returns an emoji object for the given emoji character |
| static [FromCustom](#fromcustom-method)(…) | Returns an Emoji object from a custom emoji ID and Animated flag |
| [Animated](#animated-property) { get; set; } | Whether this emoji is animated |
| [Available](#available-property) { get; set; } | Whether this emoji can be used, may be false due to loss of Server Boosts |
| [EmojiId](#emojiid-property) { get; set; } | Emoji id |
| [Id](#id-property) { get; } | The ID for the emoji if it is custom; Otherwise default snowflake |
| [Managed](#managed-property) { get; set; } | Whether this emoji is managed |
| [Mention](#mention-property) { get; } | Show the emoji in a message |
| [RequireColons](#requirecolons-property) { get; set; } | Whether this emoji must be wrapped in colons |
| [Url](#url-property) { get; } | Url to the emoji image |
| [User](#user-property) { get; set; } | User that created this emoji |
| [ToDataString](#todatastring-method)() | Returns the data string to be used in the API request |

## See Also

* class [EmojiUpdate](./EmojiUpdate.md)
* interface [ISnowflakeEntity](../Interfaces/ISnowflakeEntity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [DiscordEmoji.cs](../../../../Oxide.Ext.Discord/Entities/DiscordEmoji.cs)
   
   
# FromCharacter method

Returns an emoji object for the given emoji character

```csharp
public static DiscordEmoji FromCharacter(string emoji)
```

| parameter | description |
| --- | --- |
| emoji |  |

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# FromCustom method

Returns an Emoji object from a custom emoji ID and Animated flag

```csharp
public static DiscordEmoji FromCustom(Snowflake id, bool animated = false)
```

| parameter | description |
| --- | --- |
| id | ID of the emoji |
| animated | If the emoji is animated |

## See Also

* struct [Snowflake](./Snowflake.md)
* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ToDataString method

Returns the data string to be used in the API request

```csharp
public string ToDataString()
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DiscordEmoji constructor

The default constructor.

```csharp
public DiscordEmoji()
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Id property

The ID for the emoji if it is custom; Otherwise default snowflake

```csharp
public Snowflake Id { get; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# EmojiId property

Emoji id

```csharp
public Snowflake? EmojiId { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# User property

User that created this emoji

```csharp
public DiscordUser User { get; set; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# RequireColons property

Whether this emoji must be wrapped in colons

```csharp
public bool? RequireColons { get; set; }
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Managed property

Whether this emoji is managed

```csharp
public bool? Managed { get; set; }
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Animated property

Whether this emoji is animated

```csharp
public bool? Animated { get; set; }
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Available property

Whether this emoji can be used, may be false due to loss of Server Boosts

```csharp
public bool? Available { get; set; }
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Url property

Url to the emoji image

```csharp
public string Url { get; }
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Mention property

Show the emoji in a message

```csharp
public string Mention { get; }
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
