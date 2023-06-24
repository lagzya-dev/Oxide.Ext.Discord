# GuildCreate class

Represents [Create Guild Structure](https://discord.com/developers/docs/resources/guild#create-guild)

```csharp
public class GuildCreate
```

## Public Members

| name | description |
| --- | --- |
| [GuildCreate](#GuildCreate-constructor)() | The default constructor. |
| [AfkChannelId](#AfkChannelId-property) { get; set; } | ID of afk channel |
| [AfkTimeout](#AfkTimeout-property) { get; set; } | Afk timeout in seconds Can be set to: null, 60, 300, 900, 1800, 3600 |
| [Channels](#Channels-property) { get; set; } | Channels in the guild |
| [DefaultMessageNotifications](#DefaultMessageNotifications-property) { get; set; } | Default message notification level |
| [ExplicitContentFilter](#ExplicitContentFilter-property) { get; set; } | Explicit content filter level |
| [Icon](#Icon-property) { get; set; } | Base64 128x128 image for the guild icon |
| [Name](#Name-property) { get; set; } | Name of the guild (2-100 characters) |
| [Region](#Region-property) { get; set; } | Voice region id |
| [Roles](#Roles-property) { get; set; } | Roles in the guild |
| [SystemChannelFlags](#SystemChannelFlags-property) { get; set; } | System channel flags |
| [SystemChannelId](#SystemChannelId-property) { get; set; } | The id of the channel where guild notices such as welcome messages and boost events are posted |
| [VerificationLevel](#VerificationLevel-property) { get; set; } | Verification level |
| [Validate](#Validate-method)() |  |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [GuildCreate.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Guilds/GuildCreate.cs)
   
   
# Validate method

```csharp
public void Validate()
```

## See Also

* class [GuildCreate](./GuildCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GuildCreate constructor

The default constructor.

```csharp
public GuildCreate()
```

## See Also

* class [GuildCreate](./GuildCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Name property

Name of the guild (2-100 characters)

```csharp
public string Name { get; set; }
```

## See Also

* class [GuildCreate](./GuildCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Region property

Voice region id

```csharp
[Obsolete("Deprecated in Discord API")]
public string Region { get; set; }
```

## See Also

* class [GuildCreate](./GuildCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Icon property

Base64 128x128 image for the guild icon

```csharp
public DiscordImageData? Icon { get; set; }
```

## See Also

* struct [DiscordImageData](../Images/DiscordImageData.md)
* class [GuildCreate](./GuildCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# VerificationLevel property

Verification level

```csharp
public GuildVerificationLevel? VerificationLevel { get; set; }
```

## See Also

* enum [GuildVerificationLevel](./GuildVerificationLevel.md)
* class [GuildCreate](./GuildCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# DefaultMessageNotifications property

Default message notification level

```csharp
public DefaultNotificationLevel? DefaultMessageNotifications { get; set; }
```

## See Also

* enum [DefaultNotificationLevel](./DefaultNotificationLevel.md)
* class [GuildCreate](./GuildCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ExplicitContentFilter property

Explicit content filter level

```csharp
public ExplicitContentFilterLevel? ExplicitContentFilter { get; set; }
```

## See Also

* enum [ExplicitContentFilterLevel](./ExplicitContentFilterLevel.md)
* class [GuildCreate](./GuildCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Roles property

Roles in the guild

```csharp
public List<DiscordRole> Roles { get; set; }
```

## See Also

* class [DiscordRole](../Permissions/DiscordRole.md)
* class [GuildCreate](./GuildCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Channels property

Channels in the guild

```csharp
public List<DiscordChannel> Channels { get; set; }
```

## See Also

* class [DiscordChannel](../Channels/DiscordChannel.md)
* class [GuildCreate](./GuildCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AfkChannelId property

ID of afk channel

```csharp
public Snowflake? AfkChannelId { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [GuildCreate](./GuildCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AfkTimeout property

Afk timeout in seconds Can be set to: null, 60, 300, 900, 1800, 3600

```csharp
public int? AfkTimeout { get; set; }
```

## See Also

* class [GuildCreate](./GuildCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# SystemChannelId property

The id of the channel where guild notices such as welcome messages and boost events are posted

```csharp
public Snowflake? SystemChannelId { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [GuildCreate](./GuildCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# SystemChannelFlags property

System channel flags

```csharp
public SystemChannelFlags? SystemChannelFlags { get; set; }
```

## See Also

* enum [SystemChannelFlags](./SystemChannelFlags.md)
* class [GuildCreate](./GuildCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
