# CommandLocalization class

Localization for Application Commands

```csharp
public class CommandLocalization
```

## Public Members

| name | description |
| --- | --- |
| [CommandLocalization](#commandlocalization-constructor-1-of-2))(…) | Constructor (2 constructors) |
| [Arguments](#arguments-property) { get; set; } | Localized Argument Options |
| [Description](#description-property) { get; set; } | Localization for [`Description`](../../../Entities/Interactions/ApplicationCommands/CommandCreate/Description.md) or [`Description`](../../../Entities/Interactions/ApplicationCommands/CommandOption/Description.md) |
| [Name](#name-property) { get; set; } | Localization for [`Name`](../../../Entities/Interactions/ApplicationCommands/CommandCreate/Name.md) or [`Name`](../../../Entities/Interactions/ApplicationCommands/CommandOption/Name.md) |
| [Options](#options-property) { get; set; } | Localized Options for the Command |
| [ApplyCommandLocalization](#applycommandlocalization-method)(…) | Apply Command Localizations to the [`CommandCreate`](../../../Entities/Interactions/ApplicationCommands/CommandCreate.md) |

## See Also

* namespace [Oxide.Ext.Discord.Libraries.Templates.Commands](./CommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [CommandLocalization.cs](../../../../Oxide.Ext.Discord/Libraries/Templates/Commands/CommandLocalization.cs)
   
   
# ApplyCommandLocalization method

Apply Command Localizations to the [`CommandCreate`](../../../Entities/Interactions/ApplicationCommands/CommandCreate.md)

```csharp
public void ApplyCommandLocalization(CommandCreate create, DiscordLocale locale)
```

| parameter | description |
| --- | --- |
| create |  |
| locale |  |

## See Also

* class [CommandCreate](../../../Entities/Interactions/ApplicationCommands/CommandCreate.md)
* struct [DiscordLocale](../../Locale/DiscordLocale.md)
* class [CommandLocalization](./CommandLocalization.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Commands](./CommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# CommandLocalization constructor (1 of 2)

Constructor

```csharp
public CommandLocalization(CommandCreate create, DiscordLocale locale)
```

| parameter | description |
| --- | --- |
| create | Command to be created |
| locale | Oxide lang of the localization |

## See Also

* class [CommandCreate](../../../Entities/Interactions/ApplicationCommands/CommandCreate.md)
* struct [DiscordLocale](../../Locale/DiscordLocale.md)
* class [CommandLocalization](./CommandLocalization.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Commands](./CommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# CommandLocalization constructor (2 of 2)

Constructor

```csharp
public CommandLocalization(CommandOption opt, DiscordLocale locale)
```

| parameter | description |
| --- | --- |
| opt |  |
| locale |  |

## See Also

* class [CommandOption](../../../Entities/Interactions/ApplicationCommands/CommandOption.md)
* struct [DiscordLocale](../../Locale/DiscordLocale.md)
* class [CommandLocalization](./CommandLocalization.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Commands](./CommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Name property

Localization for [`Name`](../../../Entities/Interactions/ApplicationCommands/CommandCreate/Name) or [`Name`](../../../Entities/Interactions/ApplicationCommands/CommandOption/Name)

```csharp
public string Name { get; set; }
```

## See Also

* class [CommandLocalization](./CommandLocalization.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Commands](./CommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Description property

Localization for [`Description`](../../../Entities/Interactions/ApplicationCommands/CommandCreate/Description) or [`Description`](../../../Entities/Interactions/ApplicationCommands/CommandOption/Description)

```csharp
public string Description { get; set; }
```

## See Also

* class [CommandLocalization](./CommandLocalization.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Commands](./CommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Options property

Localized Options for the Command

```csharp
public Hash<string, CommandLocalization> Options { get; set; }
```

## See Also

* class [CommandLocalization](./CommandLocalization.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Commands](./CommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Arguments property

Localized Argument Options

```csharp
public Hash<string, ArgumentLocalization> Arguments { get; set; }
```

## See Also

* class [ArgumentLocalization](./ArgumentLocalization.md)
* class [CommandLocalization](./CommandLocalization.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Commands](./CommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
