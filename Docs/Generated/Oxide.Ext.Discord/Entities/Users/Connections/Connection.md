# Connection class

Represents a Discord Users [Connection](https://discord.com/developers/docs/resources/user#connection-object)

```csharp
public class Connection
```

## Public Members

| name | description |
| --- | --- |
| [Connection](#Connection-constructor)() | The default constructor. |
| [FriendSync](#FriendSync-property) { get; set; } | Whether friend sync is enabled for this connection |
| [Id](#Id-property) { get; set; } | ID of the connection account |
| [Integrations](#Integrations-property) { get; set; } | An array of partial server integrations [`Integration`](../../Integrations/Integration.md) |
| [Name](#Name-property) { get; set; } | The username of the connection account |
| [Revoked](#Revoked-property) { get; set; } | Whether the connection is revoked |
| [ShowActivity](#ShowActivity-property) { get; set; } | Whether activities related to this connection will be shown in presence updates |
| [TwoWayLink](#TwoWayLink-property) { get; set; } | Whether this connection has a corresponding third party OAuth2 token |
| [Type](#Type-property) { get; set; } | The service of the connection (twitch, youtube) [`ConnectionType`](./ConnectionType.md) |
| [Verified](#Verified-property) { get; set; } | Whether the connection is verified |
| [Visibility](#Visibility-property) { get; set; } | Visibility of this connection [`ConnectionVisibilityType`](./ConnectionVisibilityType.md) |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Users.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [Connection.cs](../../../../Oxide.Ext.Discord/Entities/Users/Connections/Connection.cs)
   
   
# Connection constructor

The default constructor.

```csharp
public Connection()
```

## See Also

* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities.Users.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Id property

ID of the connection account

```csharp
public Snowflake Id { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities.Users.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Name property

The username of the connection account

```csharp
public string Name { get; set; }
```

## See Also

* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities.Users.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Type property

The service of the connection (twitch, youtube) [`ConnectionType`](./ConnectionType.md)

```csharp
public ConnectionType Type { get; set; }
```

## See Also

* enum [ConnectionType](./ConnectionType.md)
* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities.Users.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Revoked property

Whether the connection is revoked

```csharp
public bool? Revoked { get; set; }
```

## See Also

* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities.Users.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Integrations property

An array of partial server integrations [`Integration`](../../Integrations/Integration.md)

```csharp
public Hash<Snowflake, Integration> Integrations { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [Integration](../../Integrations/Integration.md)
* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities.Users.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Verified property

Whether the connection is verified

```csharp
public bool Verified { get; set; }
```

## See Also

* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities.Users.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# FriendSync property

Whether friend sync is enabled for this connection

```csharp
public bool FriendSync { get; set; }
```

## See Also

* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities.Users.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# TwoWayLink property

Whether this connection has a corresponding third party OAuth2 token

```csharp
public bool TwoWayLink { get; set; }
```

## See Also

* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities.Users.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ShowActivity property

Whether activities related to this connection will be shown in presence updates

```csharp
public bool ShowActivity { get; set; }
```

## See Also

* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities.Users.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Visibility property

Visibility of this connection [`ConnectionVisibilityType`](./ConnectionVisibilityType.md)

```csharp
public ConnectionVisibilityType Visibility { get; set; }
```

## See Also

* enum [ConnectionVisibilityType](./ConnectionVisibilityType.md)
* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities.Users.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
