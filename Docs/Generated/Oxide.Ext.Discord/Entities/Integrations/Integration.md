# Integration class

Represents [Integration Structure](https://discord.com/developers/docs/resources/guild#integration-object)

```csharp
public class Integration : IntegrationUpdate, ISnowflakeEntity
```

## Public Members

| name | description |
| --- | --- |
| [Integration](#integration-constructor)() | The default constructor. |
| [Account](#account-property) { get; set; } | Integration account information |
| [Application](#application-property) { get; set; } | The bot/OAuth2 application for discord integrations |
| [Enabled](#enabled-property) { get; set; } | Is this integration enabled |
| [Id](#id-property) { get; set; } | Integration ID |
| [Name](#name-property) { get; set; } | Integration Name |
| [Revoked](#revoked-property) { get; set; } | Has this integration been revoked |
| [RoleId](#roleid-property) { get; set; } | ID that this integration uses for "subscribers" |
| [SubscriberCount](#subscribercount-property) { get; set; } | How many subscribers this integration has |
| [SyncedAt](#syncedat-property) { get; set; } | When this integration was last synced |
| [Syncing](#syncing-property) { get; set; } | Is this integration syncing |
| [Type](#type-property) { get; set; } | Integration type See [`IntegrationType`](./IntegrationType.md) |
| [User](#user-property) { get; set; } | User for this integration |

## See Also

* class [IntegrationUpdate](./IntegrationUpdate.md)
* interface [ISnowflakeEntity](../../Interfaces/ISnowflakeEntity.md)
* namespace [Oxide.Ext.Discord.Entities.Integrations](./IntegrationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [Integration.cs](../../../../Oxide.Ext.Discord/Entities/Integrations/Integration.cs)
   
   
# Integration constructor

The default constructor.

```csharp
public Integration()
```

## See Also

* class [Integration](./Integration.md)
* namespace [Oxide.Ext.Discord.Entities.Integrations](./IntegrationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Id property

Integration ID

```csharp
public Snowflake Id { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [Integration](./Integration.md)
* namespace [Oxide.Ext.Discord.Entities.Integrations](./IntegrationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Name property

Integration Name

```csharp
public string Name { get; set; }
```

## See Also

* class [Integration](./Integration.md)
* namespace [Oxide.Ext.Discord.Entities.Integrations](./IntegrationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Type property

Integration type See [`IntegrationType`](./IntegrationType.md)

```csharp
public IntegrationType Type { get; set; }
```

## See Also

* enum [IntegrationType](./IntegrationType.md)
* class [Integration](./Integration.md)
* namespace [Oxide.Ext.Discord.Entities.Integrations](./IntegrationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Enabled property

Is this integration enabled

```csharp
public bool? Enabled { get; set; }
```

## See Also

* class [Integration](./Integration.md)
* namespace [Oxide.Ext.Discord.Entities.Integrations](./IntegrationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Syncing property

Is this integration syncing

```csharp
public bool? Syncing { get; set; }
```

## See Also

* class [Integration](./Integration.md)
* namespace [Oxide.Ext.Discord.Entities.Integrations](./IntegrationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RoleId property

ID that this integration uses for "subscribers"

```csharp
public Snowflake? RoleId { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [Integration](./Integration.md)
* namespace [Oxide.Ext.Discord.Entities.Integrations](./IntegrationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# User property

User for this integration

```csharp
public DiscordUser User { get; set; }
```

## See Also

* class [DiscordUser](../Users/DiscordUser.md)
* class [Integration](./Integration.md)
* namespace [Oxide.Ext.Discord.Entities.Integrations](./IntegrationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Account property

Integration account information

```csharp
public IntegrationAccount Account { get; set; }
```

## See Also

* class [IntegrationAccount](./IntegrationAccount.md)
* class [Integration](./Integration.md)
* namespace [Oxide.Ext.Discord.Entities.Integrations](./IntegrationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# SyncedAt property

When this integration was last synced

```csharp
public DateTime? SyncedAt { get; set; }
```

## See Also

* class [Integration](./Integration.md)
* namespace [Oxide.Ext.Discord.Entities.Integrations](./IntegrationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# SubscriberCount property

How many subscribers this integration has

```csharp
public int? SubscriberCount { get; set; }
```

## See Also

* class [Integration](./Integration.md)
* namespace [Oxide.Ext.Discord.Entities.Integrations](./IntegrationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Revoked property

Has this integration been revoked

```csharp
public bool? Revoked { get; set; }
```

## See Also

* class [Integration](./Integration.md)
* namespace [Oxide.Ext.Discord.Entities.Integrations](./IntegrationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Application property

The bot/OAuth2 application for discord integrations

```csharp
public IntegrationApplication Application { get; set; }
```

## See Also

* class [IntegrationApplication](./IntegrationApplication.md)
* class [Integration](./Integration.md)
* namespace [Oxide.Ext.Discord.Entities.Integrations](./IntegrationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
