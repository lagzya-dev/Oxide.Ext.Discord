# DiscordEntitlement class

Represents a [Entitlement Structure](https://discord.com/developers/docs/monetization/entitlements#entitlement-object-entitlement-structure)

```csharp
public class DiscordEntitlement
```

## Public Members

| name | description |
| --- | --- |
| [DiscordEntitlement](#discordentitlement-constructor)() | The default constructor. |
| [ApplicationId](#applicationid-property) { get; set; } | ID of the parent application |
| [Consumed](#consumed-property) { get; set; } | For consumable items, whether the entitlement has been consumed |
| [Deleted](#deleted-property) { get; set; } | Entitlement was deleted |
| [EndsAt](#endsat-property) { get; set; } | Date at which the entitlement is no longer valid. Not present when using test entitlements. |
| [GuildId](#guildid-property) { get; set; } | ID of the guild that is granted access to the entitlement's sku |
| [Id](#id-property) { get; set; } | ID of the entitlement |
| [SkuId](#skuid-property) { get; set; } | ID of the SKU |
| [StartsAt](#startsat-property) { get; set; } | Start date at which the entitlement is valid. Not present when using test entitlements. |
| [Type](#type-property) { get; set; } | Type of entitlement |
| [UserId](#userid-property) { get; set; } | ID of the user that is granted access to the entitlement's sku |
| [ConsumeEntitlement](#consumeentitlement-method)(…) | For One-Time Purchase consumable SKUs, marks a given entitlement for the user as consumed. See [Consume an Entitlement](https://discord.com/developers/docs/monetization/entitlements#consume-an-entitlement) |
| [CreateTestEntitlement](#createtestentitlement-method)(…) | Creates a test entitlement to a given SKU for a given guild or user. Discord will act as though that user or guild has entitlement to your premium offering. See [Create Test Entitlement](https://discord.com/developers/docs/monetization/entitlements#create-test-entitlement) |
| [DeleteTestEntitlement](#deletetestentitlement-method)(…) | Deletes a currently-active test entitlement. Discord will act as though that user or guild no longer has entitlement to your premium offering. See [Delete Test Entitlement](https://discord.com/developers/docs/monetization/entitlements#delete-test-entitlement) |
| static [GetEntitlements](#getentitlements-method)(…) | Returns all entitlements for a given app, active and expired. See [List Entitlements](https://discord.com/developers/docs/monetization/entitlements#list-entitlements) |

## See Also

* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [DiscordEntitlement.cs](../../../../Oxide.Ext.Discord/Entities/DiscordEntitlement.cs)
   
   
# GetEntitlements method

Returns all entitlements for a given app, active and expired. See [List Entitlements](https://discord.com/developers/docs/monetization/entitlements#list-entitlements)

```csharp
public static IPromise<List<DiscordEntitlement>> GetEntitlements(DiscordClient client, 
    Snowflake applicationId, GetEntitlements getEntitlements = null)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| applicationId | Application ID to get entitlement for |
| getEntitlements | Query string options for the request |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* struct [Snowflake](./Snowflake.md)
* class [GetEntitlements](./GetEntitlements.md)
* class [DiscordEntitlement](./DiscordEntitlement.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ConsumeEntitlement method

For One-Time Purchase consumable SKUs, marks a given entitlement for the user as consumed. See [Consume an Entitlement](https://discord.com/developers/docs/monetization/entitlements#consume-an-entitlement)

```csharp
public IPromise ConsumeEntitlement(DiscordClient client)
```

| parameter | description |
| --- | --- |
| client | Client to use |

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [DiscordEntitlement](./DiscordEntitlement.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# CreateTestEntitlement method

Creates a test entitlement to a given SKU for a given guild or user. Discord will act as though that user or guild has entitlement to your premium offering. See [Create Test Entitlement](https://discord.com/developers/docs/monetization/entitlements#create-test-entitlement)

```csharp
public IPromise<DiscordEntitlement> CreateTestEntitlement(DiscordClient client, 
    CreateTestEntitlement create)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| create | Create request |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [CreateTestEntitlement](./CreateTestEntitlement.md)
* class [DiscordEntitlement](./DiscordEntitlement.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DeleteTestEntitlement method

Deletes a currently-active test entitlement. Discord will act as though that user or guild no longer has entitlement to your premium offering. See [Delete Test Entitlement](https://discord.com/developers/docs/monetization/entitlements#delete-test-entitlement)

```csharp
public IPromise DeleteTestEntitlement(DiscordClient client)
```

| parameter | description |
| --- | --- |
| client | Client to use |

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [DiscordEntitlement](./DiscordEntitlement.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DiscordEntitlement constructor

The default constructor.

```csharp
public DiscordEntitlement()
```

## See Also

* class [DiscordEntitlement](./DiscordEntitlement.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Id property

ID of the entitlement

```csharp
public Snowflake Id { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [DiscordEntitlement](./DiscordEntitlement.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# SkuId property

ID of the SKU

```csharp
public Snowflake SkuId { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [DiscordEntitlement](./DiscordEntitlement.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ApplicationId property

ID of the parent application

```csharp
public Snowflake ApplicationId { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [DiscordEntitlement](./DiscordEntitlement.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# UserId property

ID of the user that is granted access to the entitlement's sku

```csharp
public Snowflake? UserId { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [DiscordEntitlement](./DiscordEntitlement.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Type property

Type of entitlement

```csharp
public EntitlementType Type { get; set; }
```

## See Also

* enum [EntitlementType](./EntitlementType.md)
* class [DiscordEntitlement](./DiscordEntitlement.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Deleted property

Entitlement was deleted

```csharp
public bool Deleted { get; set; }
```

## See Also

* class [DiscordEntitlement](./DiscordEntitlement.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# StartsAt property

Start date at which the entitlement is valid. Not present when using test entitlements.

```csharp
public DateTime StartsAt { get; set; }
```

## See Also

* class [DiscordEntitlement](./DiscordEntitlement.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# EndsAt property

Date at which the entitlement is no longer valid. Not present when using test entitlements.

```csharp
public DateTime EndsAt { get; set; }
```

## See Also

* class [DiscordEntitlement](./DiscordEntitlement.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GuildId property

ID of the guild that is granted access to the entitlement's sku

```csharp
public Snowflake? GuildId { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [DiscordEntitlement](./DiscordEntitlement.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Consumed property

For consumable items, whether the entitlement has been consumed

```csharp
public bool? Consumed { get; set; }
```

## See Also

* class [DiscordEntitlement](./DiscordEntitlement.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
