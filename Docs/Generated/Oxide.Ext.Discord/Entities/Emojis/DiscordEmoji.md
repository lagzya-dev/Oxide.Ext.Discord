# DiscordEmoji class

Represents [Emoji Structure](https://discord.com/developers/docs/resources/emoji#emoji-object)

```csharp
public class DiscordEmoji : EmojiUpdate, ISnowflakeEntity
```

## Public Members

| name | description |
| --- | --- |
| [DiscordEmoji](#DiscordEmoji-constructor)() | The default constructor. |
| static [FromCharacter](#FromCharacter-method)(…) | Returns an emoji object for the given emoji character |
| [Animated](#Animated-property) { get; set; } | Whether this emoji is animated |
| [Available](#Available-property) { get; set; } | Whether this emoji can be used, may be false due to loss of Server Boosts |
| [EmojiId](#EmojiId-property) { get; set; } | Emoji id |
| [Id](#Id-property) { get; } | The ID for the emoji if it is custom; Otherwise invalid snowflake |
| [Managed](#Managed-property) { get; set; } | Whether this emoji is managed |
| [Mention](#Mention-property) { get; } | Show the emoji in a message |
| [RequireColons](#RequireColons-property) { get; set; } | Whether this emoji must be wrapped in colons |
| [Url](#Url-property) { get; } | Url to the emoji image |
| [User](#User-property) { get; set; } | User that created this emoji |
| [ToDataString](#ToDataString-method)() | Returns the data string to be used in the API request |

## See Also

* class [EmojiUpdate](./EmojiUpdate.md)
* interface [ISnowflakeEntity](../../Interfaces/ISnowflakeEntity.md)
* namespace [Oxide.Ext.Discord.Entities.Emojis](./EmojisNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordEmoji.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Emojis/DiscordEmoji.cs)
   
   
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
* namespace [Oxide.Ext.Discord.Entities.Emojis](./EmojisNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ToDataString method

Returns the data string to be used in the API request

```csharp
public string ToDataString()
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities.Emojis](./EmojisNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# DiscordEmoji constructor

The default constructor.

```csharp
public DiscordEmoji()
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities.Emojis](./EmojisNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Id property

The ID for the emoji if it is custom; Otherwise invalid snowflake

```csharp
public Snowflake Id { get; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities.Emojis](./EmojisNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# EmojiId property

Emoji id

```csharp
public Snowflake? EmojiId { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities.Emojis](./EmojisNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# User property

User that created this emoji

```csharp
public DiscordUser User { get; set; }
```

## See Also

* class [DiscordUser](../Users/DiscordUser.md)
* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities.Emojis](./EmojisNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RequireColons property

Whether this emoji must be wrapped in colons

```csharp
public bool? RequireColons { get; set; }
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities.Emojis](./EmojisNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Managed property

Whether this emoji is managed

```csharp
public bool? Managed { get; set; }
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities.Emojis](./EmojisNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Animated property

Whether this emoji is animated

```csharp
public bool? Animated { get; set; }
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities.Emojis](./EmojisNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Available property

Whether this emoji can be used, may be false due to loss of Server Boosts

```csharp
public bool? Available { get; set; }
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities.Emojis](./EmojisNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Url property

Url to the emoji image

```csharp
public string Url { get; }
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities.Emojis](./EmojisNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Mention property

Show the emoji in a message

```csharp
public string Mention { get; }
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* namespace [Oxide.Ext.Discord.Entities.Emojis](./EmojisNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
