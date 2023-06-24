# InteractionData class

Represents [ApplicationCommandInteractionData](https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-data-structure)

```csharp
public class InteractionData
```

## Public Members

| name | description |
| --- | --- |
| [InteractionData](#InteractionData)() | The default constructor. |
| [Components](#Components) { get; set; } | The values submitted by the user (Modal Submit) |
| [ComponentType](#ComponentType) { get; set; } | For components, the type of the component |
| [CustomId](#CustomId) { get; set; } | For components, the custom_id of the component |
| [GuildId](#GuildId) { get; set; } | The id of the guild the command is registered to |
| [Id](#Id) { get; set; } | ID of the invoked command |
| [Name](#Name) { get; set; } | The name of the invoked command |
| [Options](#Options) { get; set; } | The params + values from the user |
| [Resolved](#Resolved) { get; set; } | Converted [`DiscordUser`](../Users/DiscordUser.md)s, [`DiscordRole`](../Permissions/DiscordRole.md)s, [`DiscordChannel`](../Channels/DiscordChannel.md)s, [`GuildMember`](../Guilds/GuildMember.md)s, [`DiscordMessage`](../Messages/DiscordMessage.md)s [`MessageAttachment`](../Messages/MessageAttachment.md)s |
| [TargetId](#TargetId) { get; set; } | Id the of user or message targeted by a user or message command |
| [Type](#Type) { get; set; } | The type of the invoked command |
| [Values](#Values) { get; set; } | For components, the values for the select menu component |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [InteractionData.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Interactions/InteractionData.cs)
   
   
# InteractionData constructor

The default constructor.

```csharp
public InteractionData()
```

## See Also

* class [InteractionData](./InteractionData.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Id property

ID of the invoked command

```csharp
public Snowflake Id { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [InteractionData](./InteractionData.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Name property

The name of the invoked command

```csharp
public string Name { get; set; }
```

## See Also

* class [InteractionData](./InteractionData.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Type property

The type of the invoked command

```csharp
public ApplicationCommandType? Type { get; set; }
```

## See Also

* enum [ApplicationCommandType](./ApplicationCommands/ApplicationCommandType.md)
* class [InteractionData](./InteractionData.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Resolved property

Converted [`DiscordUser`](../Users/DiscordUser.md)s, [`DiscordRole`](../Permissions/DiscordRole.md)s, [`DiscordChannel`](../Channels/DiscordChannel.md)s, [`GuildMember`](../Guilds/GuildMember.md)s, [`DiscordMessage`](../Messages/DiscordMessage.md)s [`MessageAttachment`](../Messages/MessageAttachment.md)s

```csharp
public InteractionDataResolved Resolved { get; set; }
```

## See Also

* class [InteractionDataResolved](./InteractionDataResolved.md)
* class [InteractionData](./InteractionData.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Options property

The params + values from the user

```csharp
public List<InteractionDataOption> Options { get; set; }
```

## See Also

* class [InteractionDataOption](./InteractionDataOption.md)
* class [InteractionData](./InteractionData.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GuildId property

The id of the guild the command is registered to

```csharp
public Snowflake? GuildId { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [InteractionData](./InteractionData.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# CustomId property

For components, the custom_id of the component

```csharp
public string CustomId { get; set; }
```

## See Also

* class [InteractionData](./InteractionData.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ComponentType property

For components, the type of the component

```csharp
public MessageComponentType? ComponentType { get; set; }
```

## See Also

* enum [MessageComponentType](./MessageComponents/MessageComponentType.md)
* class [InteractionData](./InteractionData.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Values property

For components, the values for the select menu component

```csharp
public List<SelectMenuOption> Values { get; set; }
```

## See Also

* class [SelectMenuOption](./MessageComponents/SelectMenus/SelectMenuOption.md)
* class [InteractionData](./InteractionData.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# TargetId property

Id the of user or message targeted by a user or message command

```csharp
public Snowflake? TargetId { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [InteractionData](./InteractionData.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Components property

The values submitted by the user (Modal Submit)

```csharp
public List<ActionRowComponent> Components { get; set; }
```

## See Also

* class [ActionRowComponent](./MessageComponents/ActionRowComponent.md)
* class [InteractionData](./InteractionData.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
