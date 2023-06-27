# UserPlaceholders class

[`DiscordUser`](../../../Entities/Users/DiscordUser.md) placeholders

```csharp
public static class UserPlaceholders
```

## Public Members

| name | description |
| --- | --- |
| static [AvatarUrl](#avatarurl-method)(…) | [`GetAvatarUrl`](../../../Entities/Users/DiscordUser.md#getavatarurl-property) placeholder |
| static [BannerUrl](#bannerurl-method)(…) | [`GetBannerUrl`](../../../Entities/Users/DiscordUser.md#getbannerurl-property) placeholder |
| static [Discriminator](#discriminator-method)(…) | [`Discriminator`](../../../Entities/Users/DiscordUser.md#discriminator-property) placeholder |
| static [FullName](#fullname-method)(…) | [`FullUserName`](../../../Entities/Users/DiscordUser.md#fullusername-property) placeholder |
| static [Id](#id-method)(…) | [`Id`](../../../Entities/Users/DiscordUser.md#id-property) placeholder |
| static [IsLinked](#islinked-method)(…) | [`IsLinked`](../../../Extensions/DiscordUserExt.md#islinked-method) placeholder |
| static [Mention](#mention-method)(…) | [`Mention`](../../../Entities/Users/DiscordUser.md#mention-property) placeholder |
| static [RegisterPlaceholders](#registerplaceholders-method)(…) | Registers placeholders for the given plugin. |
| static [UserName](#username-method)(…) | [`Username`](../../../Entities/Users/DiscordUser.md#username-property) placeholder |

## See Also

* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [UserPlaceholders.cs](../../../../Oxide.Ext.Discord/Libraries/Placeholders/Default/UserPlaceholders.cs)
   
   
# Id method

[`Id`](../../../Entities/Users/DiscordUser.md#id-property) placeholder

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

[`Username`](../../../Entities/Users/DiscordUser.md#username-property) placeholder

```csharp
public static string UserName(DiscordUser user)
```

## See Also

* class [DiscordUser](../../../Entities/Users/DiscordUser.md)
* class [UserPlaceholders](./UserPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Discriminator method

[`Discriminator`](../../../Entities/Users/DiscordUser.md#discriminator-property) placeholder

```csharp
public static string Discriminator(DiscordUser user)
```

## See Also

* class [DiscordUser](../../../Entities/Users/DiscordUser.md)
* class [UserPlaceholders](./UserPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# FullName method

[`FullUserName`](../../../Entities/Users/DiscordUser.md#fullusername-property) placeholder

```csharp
public static string FullName(DiscordUser user)
```

## See Also

* class [DiscordUser](../../../Entities/Users/DiscordUser.md)
* class [UserPlaceholders](./UserPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# AvatarUrl method

[`GetAvatarUrl`](../../../Entities/Users/DiscordUser.md#getavatarurl-property) placeholder

```csharp
public static string AvatarUrl(DiscordUser user)
```

## See Also

* class [DiscordUser](../../../Entities/Users/DiscordUser.md)
* class [UserPlaceholders](./UserPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# BannerUrl method

[`GetBannerUrl`](../../../Entities/Users/DiscordUser.md#getbannerurl-property) placeholder

```csharp
public static string BannerUrl(DiscordUser user)
```

## See Also

* class [DiscordUser](../../../Entities/Users/DiscordUser.md)
* class [UserPlaceholders](./UserPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Mention method

[`Mention`](../../../Entities/Users/DiscordUser.md#mention-property) placeholder

```csharp
public static string Mention(DiscordUser user)
```

## See Also

* class [DiscordUser](../../../Entities/Users/DiscordUser.md)
* class [UserPlaceholders](./UserPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# IsLinked method

[`IsLinked`](../../../Extensions/DiscordUserExt.md#islinked-method) placeholder

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
