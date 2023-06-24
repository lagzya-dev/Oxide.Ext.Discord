# StageInstance class

Represents a channel [Stage Instance](https://discord.com/developers/docs/resources/stage-instance#stage-instance-object-stage-instance-structure) within Discord.

```csharp
public class StageInstance : ISnowflakeEntity
```

## Public Members

| name | description |
| --- | --- |
| [StageInstance](#StageInstance-constructor)() | The default constructor. |
| [ChannelId](#ChannelId-property) { get; set; } | The ID of the associated Stage channel |
| [DiscoverableDisabled](#DiscoverableDisabled-property) { get; set; } | Whether or not Stage discovery is disabled (deprecated) |
| [GuildId](#GuildId-property) { get; set; } | The guild ID of the associated Stage channel |
| [GuildScheduledEventId](#GuildScheduledEventId-property) { get; set; } | The id of the scheduled event for this Stage instance |
| [Id](#Id-property) { get; set; } | The ID of this Stage instance |
| [PrivacyLevel](#PrivacyLevel-property) { get; set; } | The privacy level of the Stage instance |
| [Topic](#Topic-property) { get; set; } | The topic of the Stage instance (1-120 characters) |
| [Delete](#Delete-method)(…) | Deletes the Stage instance. Requires the user to be a moderator of the Stage channel. See [Delete Stage Instance](https://discord.com/developers/docs/resources/stage-instance#delete-stage-instance) |
| [Edit](#Edit-method)(…) | Modifies fields of an existing Stage instance. Requires the user to be a moderator of the Stage channel. See [Update Stage Instance](https://discord.com/developers/docs/resources/stage-instance#modify-stage-instance) |
| static [Create](#Create-method)(…) | Creates a new Stage instance associated to a Stage channel. Requires the user to be a moderator of the Stage channel. See [Create Stage Instance](https://discord.com/developers/docs/resources/stage-instance#create-stage-instance) |
| static [Get](#Get-method)(…) | Gets the stage instance associated with the Stage channel, if it exists. See [Get Stage Instance](https://discord.com/developers/docs/resources/stage-instance#get-stage-instance) |

## See Also

* interface [ISnowflakeEntity](../../../Interfaces/ISnowflakeEntity.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Stages](./StagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [StageInstance.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Channels/Stages/StageInstance.cs)
   
   
# Create method

Creates a new Stage instance associated to a Stage channel. Requires the user to be a moderator of the Stage channel. See [Create Stage Instance](https://discord.com/developers/docs/resources/stage-instance#create-stage-instance)

```csharp
public static IPromise<StageInstance> Create(DiscordClient client, StageInstanceCreate create)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| create | Create Stage Instance Object |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* class [StageInstanceCreate](./StageInstanceCreate.md)
* class [StageInstance](./StageInstance.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Stages](./StagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Get method

Gets the stage instance associated with the Stage channel, if it exists. See [Get Stage Instance](https://discord.com/developers/docs/resources/stage-instance#get-stage-instance)

```csharp
public static IPromise<StageInstance> Get(DiscordClient client, Snowflake channelId)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| channelId | Channel ID to get the stage instance for |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* struct [Snowflake](../../Snowflake.md)
* class [StageInstance](./StageInstance.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Stages](./StagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Edit method

Modifies fields of an existing Stage instance. Requires the user to be a moderator of the Stage channel. See [Update Stage Instance](https://discord.com/developers/docs/resources/stage-instance#modify-stage-instance)

```csharp
public IPromise<StageInstance> Edit(DiscordClient client, StageInstanceUpdate update)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| update | Update for the stage instance |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* class [StageInstanceUpdate](./StageInstanceUpdate.md)
* class [StageInstance](./StageInstance.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Stages](./StagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Delete method

Deletes the Stage instance. Requires the user to be a moderator of the Stage channel. See [Delete Stage Instance](https://discord.com/developers/docs/resources/stage-instance#delete-stage-instance)

```csharp
public IPromise Delete(DiscordClient client)
```

| parameter | description |
| --- | --- |
| client | Client to use |

## See Also

* interface [IPromise](../../../Interfaces/Promises/IPromise.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* class [StageInstance](./StageInstance.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Stages](./StagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# StageInstance constructor

The default constructor.

```csharp
public StageInstance()
```

## See Also

* class [StageInstance](./StageInstance.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Stages](./StagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Id property

The ID of this Stage instance

```csharp
public Snowflake Id { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [StageInstance](./StageInstance.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Stages](./StagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# GuildId property

The guild ID of the associated Stage channel

```csharp
public Snowflake GuildId { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [StageInstance](./StageInstance.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Stages](./StagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ChannelId property

The ID of the associated Stage channel

```csharp
public Snowflake ChannelId { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [StageInstance](./StageInstance.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Stages](./StagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Topic property

The topic of the Stage instance (1-120 characters)

```csharp
public string Topic { get; set; }
```

## See Also

* class [StageInstance](./StageInstance.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Stages](./StagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# PrivacyLevel property

The privacy level of the Stage instance

```csharp
public PrivacyLevel PrivacyLevel { get; set; }
```

## See Also

* enum [PrivacyLevel](./PrivacyLevel.md)
* class [StageInstance](./StageInstance.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Stages](./StagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# DiscoverableDisabled property

Whether or not Stage discovery is disabled (deprecated)

```csharp
[Obsolete("Deprecated by discord")]
public bool DiscoverableDisabled { get; set; }
```

## See Also

* class [StageInstance](./StageInstance.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Stages](./StagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# GuildScheduledEventId property

The id of the scheduled event for this Stage instance

```csharp
public Snowflake? GuildScheduledEventId { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [StageInstance](./StageInstance.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Stages](./StagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
