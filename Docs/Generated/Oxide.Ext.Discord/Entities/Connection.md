# Connection class

Represents a Discord Users [Connection](https://discord.com/developers/docs/resources/user#connection-object)

```csharp
public class Connection
```

## Public Members

| name | description |
| --- | --- |
| [Connection](#connection-constructor)() | The default constructor. |
| [FriendSync](#friendsync-property) { get; set; } | Whether friend sync is enabled for this connection |
| [Id](#id-property) { get; set; } | ID of the connection account |
| [Integrations](#integrations-property) { get; set; } | An array of partial server integrations [`Integration`](./Integration.md) |
| [Name](#name-property) { get; set; } | The username of the connection account |
| [Revoked](#revoked-property) { get; set; } | Whether the connection is revoked |
| [ShowActivity](#showactivity-property) { get; set; } | Whether activities related to this connection will be shown in presence updates |
| [TwoWayLink](#twowaylink-property) { get; set; } | Whether this connection has a corresponding third party OAuth2 token |
| [Type](#type-property) { get; set; } | The service of the connection (twitch, youtube) [`ConnectionType`](./ConnectionType.md) |
| [Verified](#verified-property) { get; set; } | Whether the connection is verified |
| [Visibility](#visibility-property) { get; set; } | Visibility of this connection [`ConnectionVisibilityType`](./ConnectionVisibilityType.md) |

## See Also

* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [Connection.cs](../../../../Oxide.Ext.Discord/Entities/Connection.cs)
   
   
# Connection constructor

The default constructor.

```csharp
public Connection()
```

## See Also

* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Id property

ID of the connection account

```csharp
public Snowflake Id { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Name property

The username of the connection account

```csharp
public string Name { get; set; }
```

## See Also

* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Type property

The service of the connection (twitch, youtube) [`ConnectionType`](./ConnectionType.md)

```csharp
public ConnectionType Type { get; set; }
```

## See Also

* enum [ConnectionType](./ConnectionType.md)
* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Revoked property

Whether the connection is revoked

```csharp
public bool? Revoked { get; set; }
```

## See Also

* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Integrations property

An array of partial server integrations [`Integration`](./Integration.md)

```csharp
public Hash<Snowflake, Integration> Integrations { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [Integration](./Integration.md)
* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Verified property

Whether the connection is verified

```csharp
public bool Verified { get; set; }
```

## See Also

* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# FriendSync property

Whether friend sync is enabled for this connection

```csharp
public bool FriendSync { get; set; }
```

## See Also

* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# TwoWayLink property

Whether this connection has a corresponding third party OAuth2 token

```csharp
public bool TwoWayLink { get; set; }
```

## See Also

* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ShowActivity property

Whether activities related to this connection will be shown in presence updates

```csharp
public bool ShowActivity { get; set; }
```

## See Also

* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Visibility property

Visibility of this connection [`ConnectionVisibilityType`](./ConnectionVisibilityType.md)

```csharp
public ConnectionVisibilityType Visibility { get; set; }
```

## See Also

* enum [ConnectionVisibilityType](./ConnectionVisibilityType.md)
* class [Connection](./Connection.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
