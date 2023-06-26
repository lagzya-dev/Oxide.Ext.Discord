# ApplicationSubCommandBuilder class

Application Sub Command Builder

```csharp
public class ApplicationSubCommandBuilder
```

## Public Members

| name | description |
| --- | --- |
| readonly [CommandName](#commandname-field) | The Name of the command |
| readonly [GroupName](#groupname-field) | The Name of the group |
| readonly [SubCommandName](#subcommandname-field) | The Name of the Sub Command |
| [AddDescriptionLocalization](#adddescriptionlocalization-method)(…) | Adds Application Sub Command Description Localizations |
| [AddDescriptionLocalizations](#adddescriptionlocalizations-method)(…) | Adds command description localizations for a given plugin and lang key |
| [AddNameLocalization](#addnamelocalization-method)(…) | Adds Application Sub Command Name Localization |
| [AddNameLocalizations](#addnamelocalizations-method)(…) | Adds command name localizations for a given plugin and lang key |
| [AddOption](#addoption-method)(…) | Adds a new option |

## See Also

* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [ApplicationSubCommandBuilder.cs](../../../../Oxide.Ext.Discord/Builders/ApplicationCommands/ApplicationSubCommandBuilder.cs)
   
   
# AddNameLocalizations method

Adds command name localizations for a given plugin and lang key

```csharp
[Obsolete("AddNameLocalizations(Plugin plugin, string langKey) has been deprecated and will be removed in the future. Please use AddNameLocalization(string name, string lang) instead.")]
public ApplicationSubCommandBuilder AddNameLocalizations(Plugin plugin, string langKey)
```

| parameter | description |
| --- | --- |
| plugin | Plugin containing the localizations |
| langKey | Lang Key containing the localized text |

## See Also

* class [ApplicationSubCommandBuilder](./ApplicationSubCommandBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddNameLocalization method

Adds Application Sub Command Name Localization

```csharp
public ApplicationSubCommandBuilder AddNameLocalization(string name, ServerLocale serverLocale)
```

| parameter | description |
| --- | --- |
| name | Localized name value |
| serverLocale | Oxide lang the value is in |

## Return Value

This

## See Also

* struct [ServerLocale](../../Libraries/Locale/ServerLocale.md)
* class [ApplicationSubCommandBuilder](./ApplicationSubCommandBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddDescriptionLocalizations method

Adds command description localizations for a given plugin and lang key

```csharp
[Obsolete("AddDescriptionLocalizations(Plugin plugin, string langKey) has been deprecated and will be removed in the future. Please use AddDescriptionLocalization(string name, string lang) instead.")]
public ApplicationSubCommandBuilder AddDescriptionLocalizations(Plugin plugin, string langKey)
```

| parameter | description |
| --- | --- |
| plugin | Plugin containing the localizations |
| langKey | Lang Key containing the localized text |

## See Also

* class [ApplicationSubCommandBuilder](./ApplicationSubCommandBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddDescriptionLocalization method

Adds Application Sub Command Description Localizations

```csharp
public ApplicationSubCommandBuilder AddDescriptionLocalization(string description, 
    ServerLocale serverLocale)
```

| parameter | description |
| --- | --- |
| description | Localized description value |
| serverLocale | Oxide lang the value is in |

## Return Value

This

## See Also

* struct [ServerLocale](../../Libraries/Locale/ServerLocale.md)
* class [ApplicationSubCommandBuilder](./ApplicationSubCommandBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddOption method

Adds a new option

```csharp
public ApplicationSubCommandBuilder AddOption(CommandOptionType type, string name, 
    string description, Action<ApplicationCommandOptionBuilder> builder = null)
```

| parameter | description |
| --- | --- |
| type | Option data type (Cannot be SubCommand or SubCommandGroup) |
| name | Name of the option |
| description | Description of the option |
| builder | Callback with the [`ApplicationCommandOptionBuilder`](./ApplicationCommandOptionBuilder.md) |

## Return Value

This

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if type is SubCommand or SubCommandGroup |

## See Also

* enum [CommandOptionType](../../Entities/Interactions/ApplicationCommands/CommandOptionType.md)
* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* class [ApplicationSubCommandBuilder](./ApplicationSubCommandBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# CommandName field

The Name of the command

```csharp
public readonly string CommandName;
```

## See Also

* class [ApplicationSubCommandBuilder](./ApplicationSubCommandBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GroupName field

The Name of the group

```csharp
public readonly string GroupName;
```

## See Also

* class [ApplicationSubCommandBuilder](./ApplicationSubCommandBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# SubCommandName field

The Name of the Sub Command

```csharp
public readonly string SubCommandName;
```

## See Also

* class [ApplicationSubCommandBuilder](./ApplicationSubCommandBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
