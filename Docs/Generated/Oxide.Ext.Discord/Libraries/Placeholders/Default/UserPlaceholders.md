# UserPlaceholders class

[`DiscordUser`](../../../Entities/Users/DiscordUser.md) placeholders

```csharp
public static class UserPlaceholders
```

## Public Members

| name | description |
| --- | --- |
| static [AvatarUrl](#AvatarUrl-method)(…) | [`GetAvatarUrl`](../../../Entities/Users/DiscordUser/GetAvatarUrl.md) placeholder |
| static [BannerUrl](#BannerUrl-method)(…) | [`GetBannerUrl`](../../../Entities/Users/DiscordUser/GetBannerUrl.md) placeholder |
| static [Discriminator](#Discriminator-method)(…) | [`Discriminator`](../../../Entities/Users/DiscordUser/Discriminator.md) placeholder |
| static [FullName](#FullName-method)(…) | [`FullUserName`](../../../Entities/Users/DiscordUser/FullUserName.md) placeholder |
| static [Id](#Id-method)(…) | [`Id`](../../../Entities/Users/DiscordUser/Id.md) placeholder |
| static [IsLinked](#IsLinked-method)(…) | [`IsLinked`](../../../Extensions/DiscordUserExt/IsLinked.md) placeholder |
| static [Mention](#Mention-method)(…) | [`Mention`](../../../Entities/Users/DiscordUser/Mention.md) placeholder |
| static [RegisterPlaceholders](#RegisterPlaceholders-method)(…) | Registers placeholders for the given plugin. |
| static [UserName](#UserName-method)(…) | [`Username`](../../../Entities/Users/DiscordUser/Username.md) placeholder |

## See Also

* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [UserPlaceholders.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Libraries/Placeholders/Default/UserPlaceholders.cs)
   
   
# Id method

[`Id`](../../../Entities/Users/DiscordUser/Id) placeholder

```csharp
public static Snowflake Id(DiscordUser user)
```

## See Also

* struct [Snowflake](../../../Entities/Snowflake.md)
* class [DiscordUser](../../../Entities/Users/DiscordUser.md)
* class [UserPlaceholders](./UserPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# UserName method

[`Username`](../../../Entities/Users/DiscordUser/Username) placeholder

```csharp
public static string UserName(DiscordUser user)
```

## See Also

* class [DiscordUser](../../../Entities/Users/DiscordUser.md)
* class [UserPlaceholders](./UserPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Discriminator method

[`Discriminator`](../../../Entities/Users/DiscordUser/Discriminator) placeholder

```csharp
public static string Discriminator(DiscordUser user)
```

## See Also

* class [DiscordUser](../../../Entities/Users/DiscordUser.md)
* class [UserPlaceholders](./UserPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# FullName method

[`FullUserName`](../../../Entities/Users/DiscordUser/FullUserName) placeholder

```csharp
public static string FullName(DiscordUser user)
```

## See Also

* class [DiscordUser](../../../Entities/Users/DiscordUser.md)
* class [UserPlaceholders](./UserPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# AvatarUrl method

[`GetAvatarUrl`](../../../Entities/Users/DiscordUser/GetAvatarUrl) placeholder

```csharp
public static string AvatarUrl(DiscordUser user)
```

## See Also

* class [DiscordUser](../../../Entities/Users/DiscordUser.md)
* class [UserPlaceholders](./UserPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# BannerUrl method

[`GetBannerUrl`](../../../Entities/Users/DiscordUser/GetBannerUrl) placeholder

```csharp
public static string BannerUrl(DiscordUser user)
```

## See Also

* class [DiscordUser](../../../Entities/Users/DiscordUser.md)
* class [UserPlaceholders](./UserPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Mention method

[`Mention`](../../../Entities/Users/DiscordUser/Mention) placeholder

```csharp
public static string Mention(DiscordUser user)
```

## See Also

* class [DiscordUser](../../../Entities/Users/DiscordUser.md)
* class [UserPlaceholders](./UserPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# IsLinked method

[`IsLinked`](../../../Extensions/DiscordUserExt/IsLinked) placeholder

```csharp
public static bool IsLinked(DiscordUser user)
```

## See Also

* class [DiscordUser](../../../Entities/Users/DiscordUser.md)
* class [UserPlaceholders](./UserPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# RegisterPlaceholders method

Registers placeholders for the given plugin.

```csharp
public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
```

| parameter | description |
| --- | --- |
| plugin | Plugin to register placeholders for |
| placeholderPrefix | Prefix to use for the placeholders |
| dataKey | Data key in [`PlaceholderData`](../PlaceholderData.md) |

## See Also

* class [UserPlaceholders](./UserPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
