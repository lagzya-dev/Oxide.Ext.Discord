# InteractionDataOption class

Represents [Application Command Interaction Data Option](https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-interaction-data-option-structure)

```csharp
public class InteractionDataOption
```

## Public Members

| name | description |
| --- | --- |
| [InteractionDataOption](#InteractionDataOption-constructor)() | The default constructor. |
| [Focused](#Focused-property) { get; set; } | True if this option is the currently focused option for autocomplete |
| [Name](#Name-property) { get; set; } | The name of the parameter |
| [Options](#Options-property) { get; set; } | Present if this option is a group or subcommand See [`InteractionDataOption`](./InteractionDataOption.md) |
| [Type](#Type-property) { get; set; } | Value of ApplicationCommandOptionType |
| [GetBool](#GetBool-method)() | Returns the value as a bool |
| [GetInt](#GetInt-method)() | Returns the value as an int |
| [GetNumber](#GetNumber-method)() | Returns the value as a double |
| [GetSnowflake](#GetSnowflake-method)() | Returns the value as a Snowflake |
| [GetString](#GetString-method)() | Returns the value as a string |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [InteractionDataOption.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Interactions/InteractionDataOption.cs)
   
   
# GetString method

Returns the value as a string

```csharp
public string GetString()
```

## See Also

* class [InteractionDataOption](./InteractionDataOption.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetInt method

Returns the value as an int

```csharp
public int GetInt()
```

## See Also

* class [InteractionDataOption](./InteractionDataOption.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetBool method

Returns the value as a bool

```csharp
public bool GetBool()
```

## See Also

* class [InteractionDataOption](./InteractionDataOption.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetNumber method

Returns the value as a double

```csharp
public double GetNumber()
```

## See Also

* class [InteractionDataOption](./InteractionDataOption.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetSnowflake method

Returns the value as a Snowflake

```csharp
public Snowflake GetSnowflake()
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [InteractionDataOption](./InteractionDataOption.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# InteractionDataOption constructor

The default constructor.

```csharp
public InteractionDataOption()
```

## See Also

* class [InteractionDataOption](./InteractionDataOption.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Name property

The name of the parameter

```csharp
public string Name { get; set; }
```

## See Also

* class [InteractionDataOption](./InteractionDataOption.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Type property

Value of ApplicationCommandOptionType

```csharp
public CommandOptionType Type { get; set; }
```

## See Also

* enum [CommandOptionType](./ApplicationCommands/CommandOptionType.md)
* class [InteractionDataOption](./InteractionDataOption.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Options property

Present if this option is a group or subcommand See [`InteractionDataOption`](./InteractionDataOption.md)

```csharp
public List<InteractionDataOption> Options { get; set; }
```

## See Also

* class [InteractionDataOption](./InteractionDataOption.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Focused property

True if this option is the currently focused option for autocomplete

```csharp
public bool? Focused { get; set; }
```

## See Also

* class [InteractionDataOption](./InteractionDataOption.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
