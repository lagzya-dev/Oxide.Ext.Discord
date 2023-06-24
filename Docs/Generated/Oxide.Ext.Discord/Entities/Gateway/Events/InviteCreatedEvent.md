# InviteCreatedEvent class

Represents [Invite Create](https://discord.com/developers/docs/topics/gateway#invite-create)

```csharp
public class InviteCreatedEvent
```

## Public Members

| name | description |
| --- | --- |
| [InviteCreatedEvent](#InviteCreatedEvent-constructor)() | The default constructor. |
| [ChannelId](#ChannelId-property) { get; set; } | The channel the invite is for |
| [Code](#Code-property) { get; set; } | The unique invite code |
| [CreatedAt](#CreatedAt-property) { get; set; } | The time at which the invite was created |
| [GuildId](#GuildId-property) { get; set; } | The guild of the invite |
| [Inviter](#Inviter-property) { get; set; } | The user that created the invite |
| [MaxAge](#MaxAge-property) { get; set; } | How long the invite is valid for (in seconds) |
| [MaxUses](#MaxUses-property) { get; set; } | The maximum number of times the invite can be use |
| [TargetUser](#TargetUser-property) { get; set; } | The target user for this invite |
| [TargetUserType](#TargetUserType-property) { get; set; } | The type of user target for this invite |
| [Temporary](#Temporary-property) { get; set; } | Whether or not the invite is temporary (invited users will be kicked on disconnect unless they're assigned a role) |
| [Uses](#Uses-property) { get; set; } | How many times the invite has been used (always will be 0) |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [InviteCreatedEvent.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Gateway/Events/InviteCreatedEvent.cs)
   
   
# InviteCreatedEvent constructor

The default constructor.

```csharp
public InviteCreatedEvent()
```

## See Also

* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ChannelId property

The channel the invite is for

```csharp
public Snowflake ChannelId { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Code property

The unique invite code

```csharp
public string Code { get; set; }
```

## See Also

* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# CreatedAt property

The time at which the invite was created

```csharp
public DateTime CreatedAt { get; set; }
```

## See Also

* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# GuildId property

The guild of the invite

```csharp
public Snowflake? GuildId { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Inviter property

The user that created the invite

```csharp
public DiscordUser Inviter { get; set; }
```

## See Also

* class [DiscordUser](../../Users/DiscordUser.md)
* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# MaxAge property

How long the invite is valid for (in seconds)

```csharp
public int MaxAge { get; set; }
```

## See Also

* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# MaxUses property

The maximum number of times the invite can be use

```csharp
public int MaxUses { get; set; }
```

## See Also

* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# TargetUser property

The target user for this invite

```csharp
public DiscordUser TargetUser { get; set; }
```

## See Also

* class [DiscordUser](../../Users/DiscordUser.md)
* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# TargetUserType property

The type of user target for this invite

```csharp
public TargetUserType TargetUserType { get; set; }
```

## See Also

* enum [TargetUserType](../../Invites/TargetUserType.md)
* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Temporary property

Whether or not the invite is temporary (invited users will be kicked on disconnect unless they're assigned a role)

```csharp
public bool? Temporary { get; set; }
```

## See Also

* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Uses property

How many times the invite has been used (always will be 0)

```csharp
public int? Uses { get; set; }
```

## See Also

* class [InviteCreatedEvent](./InviteCreatedEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
