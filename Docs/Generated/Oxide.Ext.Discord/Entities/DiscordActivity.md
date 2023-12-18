# DiscordActivity class

Represents [Activity Structure](https://discord.com/developers/docs/topics/gateway-events#activity-object)

```csharp
public class DiscordActivity
```

## Public Members

| name | description |
| --- | --- |
| [DiscordActivity](#discordactivity-constructor)() | The default constructor. |
| [ApplicationId](#applicationid-property) { get; set; } | Application id for the game |
| [Assets](#assets-property) { get; set; } | Images for the presence and their hover texts See [`ActivityAssets`](./ActivityAssets.md) |
| [Buttons](#buttons-property) { get; set; } | The custom buttons shown in the Rich Presence (max 2) See [`ActivityButton`](./ActivityButton.md) |
| [CreatedAt](#createdat-property) { get; set; } | Timestamp of when the activity was added to the user's session |
| [Details](#details-property) { get; set; } | What the player is currently doing |
| [Emoji](#emoji-property) { get; set; } | The emoji used for a custom status See [`DiscordEmoji`](./DiscordEmoji.md) |
| [Flags](#flags-property) { get; set; } | Describes what the payload includes See [`ActivityFlags`](./ActivityFlags.md) |
| [GetLargeImageUrl](#getlargeimageurl-property) { get; } | Returns the large image url for the presence asset |
| [GetSmallImageUrl](#getsmallimageurl-property) { get; } | Returns the small image url for the presence asset |
| [Instance](#instance-property) { get; set; } | Whether or not the activity is an instanced game session |
| [Name](#name-property) { get; set; } | The activity's name |
| [Party](#party-property) { get; set; } | Information for the current party of the player See [`ActivityParty`](./ActivityParty.md) |
| [Secrets](#secrets-property) { get; set; } | Secrets for Rich Presence joining and spectating See [`ActivitySecrets`](./ActivitySecrets.md) |
| [State](#state-property) { get; set; } | The user's current party status |
| [Timestamps](#timestamps-property) { get; set; } | Unix timestamps for start and/or end of the game See [`ActivityTimestamps`](./ActivityTimestamps.md) |
| [Type](#type-property) { get; set; } | Activity type See [`ActivityType`](./ActivityType.md) |
| [Url](#url-property) { get; set; } | Stream url, is validated when type is 1 |

## See Also

* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [DiscordActivity.cs](../../../../Oxide.Ext.Discord/Entities/DiscordActivity.cs)
   
   
# DiscordActivity constructor

The default constructor.

```csharp
public DiscordActivity()
```

## See Also

* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Name property

The activity's name

```csharp
public string Name { get; set; }
```

## See Also

* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Type property

Activity type See [`ActivityType`](./ActivityType.md)

```csharp
public ActivityType Type { get; set; }
```

## See Also

* enum [ActivityType](./ActivityType.md)
* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Url property

Stream url, is validated when type is 1

```csharp
public string Url { get; set; }
```

## See Also

* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# CreatedAt property

Timestamp of when the activity was added to the user's session

```csharp
public DateTimeOffset CreatedAt { get; set; }
```

## See Also

* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Timestamps property

Unix timestamps for start and/or end of the game See [`ActivityTimestamps`](./ActivityTimestamps.md)

```csharp
public List<ActivityTimestamps> Timestamps { get; set; }
```

## See Also

* class [ActivityTimestamps](./ActivityTimestamps.md)
* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ApplicationId property

Application id for the game

```csharp
public Snowflake? ApplicationId { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Details property

What the player is currently doing

```csharp
public string Details { get; set; }
```

## See Also

* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# State property

The user's current party status

```csharp
public string State { get; set; }
```

## See Also

* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Emoji property

The emoji used for a custom status See [`DiscordEmoji`](./DiscordEmoji.md)

```csharp
public DiscordEmoji Emoji { get; set; }
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Party property

Information for the current party of the player See [`ActivityParty`](./ActivityParty.md)

```csharp
public ActivityParty Party { get; set; }
```

## See Also

* class [ActivityParty](./ActivityParty.md)
* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Assets property

Images for the presence and their hover texts See [`ActivityAssets`](./ActivityAssets.md)

```csharp
public ActivityAssets Assets { get; set; }
```

## See Also

* class [ActivityAssets](./ActivityAssets.md)
* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Secrets property

Secrets for Rich Presence joining and spectating See [`ActivitySecrets`](./ActivitySecrets.md)

```csharp
public ActivitySecrets Secrets { get; set; }
```

## See Also

* class [ActivitySecrets](./ActivitySecrets.md)
* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Instance property

Whether or not the activity is an instanced game session

```csharp
public bool? Instance { get; set; }
```

## See Also

* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Flags property

Describes what the payload includes See [`ActivityFlags`](./ActivityFlags.md)

```csharp
public ActivityFlags? Flags { get; set; }
```

## See Also

* enum [ActivityFlags](./ActivityFlags.md)
* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Buttons property

The custom buttons shown in the Rich Presence (max 2) See [`ActivityButton`](./ActivityButton.md)

```csharp
public List<ActivityButton> Buttons { get; set; }
```

## See Also

* class [ActivityButton](./ActivityButton.md)
* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetLargeImageUrl property

Returns the large image url for the presence asset

```csharp
public string GetLargeImageUrl { get; }
```

## See Also

* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetSmallImageUrl property

Returns the small image url for the presence asset

```csharp
public string GetSmallImageUrl { get; }
```

## See Also

* class [DiscordActivity](./DiscordActivity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
