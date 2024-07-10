# InteractionDataParsed class

Parses Interaction Data to make it easier to process for application commands

```csharp
public class InteractionDataParsed
```

## Public Members

| name | description |
| --- | --- |
| [InteractionDataParsed](#interactiondataparsed-constructor)(…) | Constructor for the data parser. |
| [Args](#args-property) { get; } | Interaction Data Supplied Args if ChatInput Command Type if an application command |
| [CommandGroup](#commandgroup-property) { get; } | Command group for the interaction if ChatInput Command Type if an application command Null if command group is not used for the command. Defaults to empty string if command does not have a command group |
| [InGuild](#inguild-property) { get; } | Returns true if this command was used in a guild; false otherwise. |
| [SubCommand](#subcommand-property) { get; } | Sub Command for the interaction if ChatInput Command Typ if an application command Null if sub command group is not used for the command. Defaults to empty string if command does not have sub command |
| readonly [Command](#command-field) | Parsed command for the interaction if an application command |
| readonly [Data](#data-field) | [`InteractionData`](./InteractionData.md) for the Interaction |
| readonly [GuildOxideLocale](#guildoxidelocale-field) | Discord Guild's locale converted to oxide lang locale |
| readonly [Interaction](#interaction-field) | Interaction this data is for |
| readonly [SelectMenuValues](#selectmenuvalues-field) | If a [`BaseSelectMenuComponent`](./BaseSelectMenuComponent.md) triggered this interaction. The values selected from the select menu. |
| readonly [TriggeredComponentId](#triggeredcomponentid-field) | The CustomId of the [`BaseInteractableComponent`](./BaseInteractableComponent.md) that triggered the interaction if a component triggered this interaction |
| readonly [Type](#type-field) | [`ApplicationCommandType`](./ApplicationCommandType.md) The type of interaction that was triggered |
| readonly [UserOxideLocale](#useroxidelocale-field) | Discord User's locale converted to oxide lang locale |

## See Also

* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [InteractionDataParsed.cs](../../../../Oxide.Ext.Discord/Entities/InteractionDataParsed.cs)
   
   
# InteractionDataParsed constructor

Constructor for the data parser.

```csharp
public InteractionDataParsed(DiscordInteraction interaction)
```

| parameter | description |
| --- | --- |
| interaction | Interaction to be parsed |

## See Also

* class [DiscordInteraction](./DiscordInteraction.md)
* class [InteractionDataParsed](./InteractionDataParsed.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# CommandGroup property

Command group for the interaction if ChatInput Command Type if an application command Null if command group is not used for the command. Defaults to empty string if command does not have a command group

```csharp
public string CommandGroup { get; }
```

## See Also

* class [InteractionDataParsed](./InteractionDataParsed.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# SubCommand property

Sub Command for the interaction if ChatInput Command Typ if an application command Null if sub command group is not used for the command. Defaults to empty string if command does not have sub command

```csharp
public string SubCommand { get; }
```

## See Also

* class [InteractionDataParsed](./InteractionDataParsed.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Args property

Interaction Data Supplied Args if ChatInput Command Type if an application command

```csharp
public InteractionDataArgs Args { get; }
```

## See Also

* class [InteractionDataArgs](./InteractionDataArgs.md)
* class [InteractionDataParsed](./InteractionDataParsed.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# InGuild property

Returns true if this command was used in a guild; false otherwise.

```csharp
public bool InGuild { get; }
```

## See Also

* class [InteractionDataParsed](./InteractionDataParsed.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Interaction field

Interaction this data is for

```csharp
public readonly DiscordInteraction Interaction;
```

## See Also

* class [DiscordInteraction](./DiscordInteraction.md)
* class [InteractionDataParsed](./InteractionDataParsed.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Data field

[`InteractionData`](./InteractionData.md) for the Interaction

```csharp
public readonly InteractionData Data;
```

## See Also

* class [InteractionData](./InteractionData.md)
* class [InteractionDataParsed](./InteractionDataParsed.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Type field

[`ApplicationCommandType`](./ApplicationCommandType.md) The type of interaction that was triggered

```csharp
public readonly ApplicationCommandType? Type;
```

## See Also

* enum [ApplicationCommandType](./ApplicationCommandType.md)
* class [InteractionDataParsed](./InteractionDataParsed.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Command field

Parsed command for the interaction if an application command

```csharp
public readonly string Command;
```

## See Also

* class [InteractionDataParsed](./InteractionDataParsed.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# TriggeredComponentId field

The CustomId of the [`BaseInteractableComponent`](./BaseInteractableComponent.md) that triggered the interaction if a component triggered this interaction

```csharp
public readonly string TriggeredComponentId;
```

## See Also

* class [InteractionDataParsed](./InteractionDataParsed.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# SelectMenuValues field

If a [`BaseSelectMenuComponent`](./BaseSelectMenuComponent.md) triggered this interaction. The values selected from the select menu.

```csharp
public readonly List<string> SelectMenuValues;
```

## See Also

* class [InteractionDataParsed](./InteractionDataParsed.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# UserOxideLocale field

Discord User's locale converted to oxide lang locale

```csharp
public readonly ServerLocale UserOxideLocale;
```

## See Also

* struct [ServerLocale](../Libraries/ServerLocale.md)
* class [InteractionDataParsed](./InteractionDataParsed.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GuildOxideLocale field

Discord Guild's locale converted to oxide lang locale

```csharp
public readonly ServerLocale GuildOxideLocale;
```

## See Also

* struct [ServerLocale](../Libraries/ServerLocale.md)
* class [InteractionDataParsed](./InteractionDataParsed.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
