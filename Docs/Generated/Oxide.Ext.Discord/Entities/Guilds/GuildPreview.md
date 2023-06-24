# GuildPreview class

Represents [Guild Preview Structure](https://discord.com/developers/docs/resources/guild#guild-preview-object)

```csharp
public class GuildPreview
```

## Public Members

| name | description |
| --- | --- |
| [GuildPreview](#GuildPreview)() | The default constructor. |
| [ApproximateMemberCount](#ApproximateMemberCount) { get; set; } | Approximate number of members in this guild |
| [ApproximatePresenceCount](#ApproximatePresenceCount) { get; set; } | Approximate number of non-offline members in this guild |
| [Description](#Description) { get; set; } | The description of a guild |
| [DiscoverySplash](#DiscoverySplash) { get; set; } | Discovery splash hash Only present for guilds with the "DISCOVERABLE" feature |
| [Emojis](#Emojis) { get; set; } | Custom guild emojis |
| [Features](#Features) { get; set; } | Enabled guild features See [`GuildFeatures`](./GuildFeatures.md) |
| [Icon](#Icon) { get; set; } | Base64 128x128 image for the guild icon |
| [Id](#Id) { get; set; } | Guild id |
| [Name](#Name) { get; set; } | Name of the guild (2-100 characters) |
| [Splash](#Splash) { get; set; } | Splash hash |
| [Stickers](#Stickers) { get; set; } | Custom guild stickers |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [GuildPreview.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Guilds/GuildPreview.cs)
   
   
# GuildPreview constructor

The default constructor.

```csharp
public GuildPreview()
```

## See Also

* class [GuildPreview](./GuildPreview.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Id property

Guild id

```csharp
public Snowflake Id { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [GuildPreview](./GuildPreview.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Name property

Name of the guild (2-100 characters)

```csharp
public string Name { get; set; }
```

## See Also

* class [GuildPreview](./GuildPreview.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Icon property

Base64 128x128 image for the guild icon

```csharp
public string Icon { get; set; }
```

## See Also

* class [GuildPreview](./GuildPreview.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Splash property

Splash hash

```csharp
public string Splash { get; set; }
```

## See Also

* class [GuildPreview](./GuildPreview.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# DiscoverySplash property

Discovery splash hash Only present for guilds with the "DISCOVERABLE" feature

```csharp
public string DiscoverySplash { get; set; }
```

## See Also

* class [GuildPreview](./GuildPreview.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Emojis property

Custom guild emojis

```csharp
public List<DiscordEmoji> Emojis { get; set; }
```

## See Also

* class [DiscordEmoji](../Emojis/DiscordEmoji.md)
* class [GuildPreview](./GuildPreview.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Features property

Enabled guild features See [`GuildFeatures`](./GuildFeatures.md)

```csharp
public List<GuildFeatures> Features { get; set; }
```

## See Also

* enum [GuildFeatures](./GuildFeatures.md)
* class [GuildPreview](./GuildPreview.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ApproximateMemberCount property

Approximate number of members in this guild

```csharp
public int? ApproximateMemberCount { get; set; }
```

## See Also

* class [GuildPreview](./GuildPreview.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ApproximatePresenceCount property

Approximate number of non-offline members in this guild

```csharp
public int? ApproximatePresenceCount { get; set; }
```

## See Also

* class [GuildPreview](./GuildPreview.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Description property

The description of a guild

```csharp
public string Description { get; set; }
```

## See Also

* class [GuildPreview](./GuildPreview.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Stickers property

Custom guild stickers

```csharp
public List<DiscordSticker> Stickers { get; set; }
```

## See Also

* class [DiscordSticker](../Stickers/DiscordSticker.md)
* class [GuildPreview](./GuildPreview.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
