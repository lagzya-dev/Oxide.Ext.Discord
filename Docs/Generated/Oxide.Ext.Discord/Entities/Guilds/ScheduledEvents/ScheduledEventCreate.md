# ScheduledEventCreate class

Represents [Guild Scheduled Event Create](https://discord.com/developers/docs/resources/guild-scheduled-event#create-guild-scheduled-event) within discord

```csharp
public class ScheduledEventCreate
```

## Public Members

| name | description |
| --- | --- |
| [ScheduledEventCreate](#ScheduledEventCreate-constructor)() | The default constructor. |
| [ChannelId](#ChannelId-property) { get; set; } | The channel ID in which the scheduled event will be hosted, or null if [`scheduled entity type`](./ScheduledEventEntityType.md) is External |
| [Description](#Description-property) { get; set; } | The description of the scheduled event (1-1000 characters) |
| [EntityMetadata](#EntityMetadata-property) { get; set; } | Additional metadata for the guild scheduled event |
| [EntityType](#EntityType-property) { get; set; } | The type of the scheduled event |
| [Image](#Image-property) { get; set; } | The cover image of the scheduled event |
| [Name](#Name-property) { get; set; } | The name of the scheduled event (1-100 characters) |
| [PrivacyLevel](#PrivacyLevel-property) { get; set; } | The privacy level of the scheduled event |
| [ScheduledEndTime](#ScheduledEndTime-property) { get; set; } | The time the scheduled event will end, required if [`EntityType`](./GuildScheduledEvent/EntityType.md) is EXTERNAL |
| [ScheduledStartTime](#ScheduledStartTime-property) { get; set; } | The time the scheduled event will start |
| [Validate](#Validate-method)() |  |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents](./ScheduledEventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [ScheduledEventCreate.cs](../../../../Oxide.Ext.Discord/Entities/Guilds/ScheduledEvents/ScheduledEventCreate.cs)
   
   
# Validate method

```csharp
public void Validate()
```

## See Also

* class [ScheduledEventCreate](./ScheduledEventCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents](./ScheduledEventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ScheduledEventCreate constructor

The default constructor.

```csharp
public ScheduledEventCreate()
```

## See Also

* class [ScheduledEventCreate](./ScheduledEventCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents](./ScheduledEventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ChannelId property

The channel ID in which the scheduled event will be hosted, or null if [`scheduled entity type`](./ScheduledEventEntityType.md) is External

```csharp
public Snowflake? ChannelId { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [ScheduledEventCreate](./ScheduledEventCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents](./ScheduledEventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# EntityMetadata property

Additional metadata for the guild scheduled event

```csharp
public ScheduledEventEntityMetadata EntityMetadata { get; set; }
```

## See Also

* class [ScheduledEventEntityMetadata](./ScheduledEventEntityMetadata.md)
* class [ScheduledEventCreate](./ScheduledEventCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents](./ScheduledEventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Name property

The name of the scheduled event (1-100 characters)

```csharp
public string Name { get; set; }
```

## See Also

* class [ScheduledEventCreate](./ScheduledEventCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents](./ScheduledEventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# PrivacyLevel property

The privacy level of the scheduled event

```csharp
public ScheduledEventPrivacyLevel PrivacyLevel { get; set; }
```

## See Also

* enum [ScheduledEventPrivacyLevel](./ScheduledEventPrivacyLevel.md)
* class [ScheduledEventCreate](./ScheduledEventCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents](./ScheduledEventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ScheduledStartTime property

The time the scheduled event will start

```csharp
public DateTime ScheduledStartTime { get; set; }
```

## See Also

* class [ScheduledEventCreate](./ScheduledEventCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents](./ScheduledEventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ScheduledEndTime property

The time the scheduled event will end, required if [`EntityType`](./GuildScheduledEvent/EntityType) is EXTERNAL

```csharp
public DateTime? ScheduledEndTime { get; set; }
```

## See Also

* class [ScheduledEventCreate](./ScheduledEventCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents](./ScheduledEventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Description property

The description of the scheduled event (1-1000 characters)

```csharp
public string Description { get; set; }
```

## See Also

* class [ScheduledEventCreate](./ScheduledEventCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents](./ScheduledEventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# EntityType property

The type of the scheduled event

```csharp
public ScheduledEventEntityType EntityType { get; set; }
```

## See Also

* enum [ScheduledEventEntityType](./ScheduledEventEntityType.md)
* class [ScheduledEventCreate](./ScheduledEventCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents](./ScheduledEventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Image property

The cover image of the scheduled event

```csharp
public DiscordImageData Image { get; set; }
```

## See Also

* struct [DiscordImageData](../../Images/DiscordImageData.md)
* class [ScheduledEventCreate](./ScheduledEventCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents](./ScheduledEventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
