# InviteCreatedEvent class

Represents [Invite Create](https://discord.com/developers/docs/topics/gateway#invite-create)

```csharp
public class InviteCreatedEvent
```

## Public Members

| name | description |
| --- | --- |
| [InviteCreatedEvent](#invitecreatedevent-constructor)() | The default constructor. |
| [ChannelId](#channelid-property) { get; set; } | The channel the invite is for |
| [Code](#code-property) { get; set; } | The unique invite code |
| [CreatedAt](#createdat-property) { get; set; } | The time at which the invite was created |
| [GuildId](#guildid-property) { get; set; } | The guild of the invite |
| [Inviter](#inviter-property) { get; set; } | The user that created the invite |
| [MaxAge](#maxage-property) { get; set; } | How long the invite is valid for (in seconds) |
| [MaxUses](#maxuses-property) { get; set; } | The maximum number of times the invite can be use |
| [TargetUser](#targetuser-property) { get; set; } | The target user for this invite |
| [TargetUserType](#targetusertype-property) { get; set; } | The type of user target for this invite |
| [Temporary](#temporary-property) { get; set; } | Whether or not the invite is temporary (invited users will be kicked on disconnect unless they're assigned a role) |
| [Uses](#uses-property) { get; set; } | How many times the invite has been used (always will be 0) |

## See Also

* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [InviteCreatedEvent.cs](../../../../Oxide.Ext.Discord/Entities/InviteCreatedEvent.cs)
   
   
# InviteCreatedEvent constructor

The default constructor.

```csharp
public InviteCreatedEvent()
```

## See Also

* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ChannelId property

The channel the invite is for

```csharp
public Snowflake ChannelId { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Code property

The unique invite code

```csharp
public string Code { get; set; }
```

## See Also

* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# CreatedAt property

The time at which the invite was created

```csharp
public DateTime CreatedAt { get; set; }
```

## See Also

* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GuildId property

The guild of the invite

```csharp
public Snowflake? GuildId { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Inviter property

The user that created the invite

```csharp
public DiscordUser Inviter { get; set; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# MaxAge property

How long the invite is valid for (in seconds)

```csharp
public int MaxAge { get; set; }
```

## See Also

* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# MaxUses property

The maximum number of times the invite can be use

```csharp
public int MaxUses { get; set; }
```

## See Also

* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# TargetUser property

The target user for this invite

```csharp
public DiscordUser TargetUser { get; set; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# TargetUserType property

The type of user target for this invite

```csharp
public TargetUserType TargetUserType { get; set; }
```

## See Also

* enum [TargetUserType](./TargetUserType.md)
* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Temporary property

Whether or not the invite is temporary (invited users will be kicked on disconnect unless they're assigned a role)

```csharp
public bool? Temporary { get; set; }
```

## See Also

* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Uses property

How many times the invite has been used (always will be 0)

```csharp
public int? Uses { get; set; }
```

## See Also

* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
