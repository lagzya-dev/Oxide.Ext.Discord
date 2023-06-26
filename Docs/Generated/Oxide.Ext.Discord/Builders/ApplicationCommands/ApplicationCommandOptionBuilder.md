# ApplicationCommandOptionBuilder class

Represents a Subcommand Option Builder for SubCommands

```csharp
public class ApplicationCommandOptionBuilder
```

## Public Members

| name | description |
| --- | --- |
| readonly [CommandName](#commandname-field) | The Name of the command |
| readonly [GroupName](#groupname-field) | The Name of the group |
| readonly [OptionName](#optionname-field) | The Name of the Option |
| readonly [SubCommandName](#subcommandname-field) | The Name of the Sub Command |
| [AddChoice](#addchoice-method-1-of-3))(…) | Adds a choice to this option of type string (3 methods) |
| [AddDescriptionLocalization](#adddescriptionlocalization-method)(…) | Adds Application Command Option Description Localization |
| [AddDescriptionLocalizations](#adddescriptionlocalizations-method)(…) | Adds command description localizations for a given plugin and lang key |
| [AddNameLocalization](#addnamelocalization-method)(…) | Adds Application Command Option Name Localization |
| [AddNameLocalizations](#addnamelocalizations-method)(…) | Adds command name localizations for a given plugin and lang key |
| [AutoComplete](#autocomplete-method)(…) | Enable auto complete for the option |
| [ChannelTypes](#channeltypes-method)(…) | Set's the channel types for the option |
| [MaxLength](#maxlength-method)(…) | Max Length for String Option Max Of 6000 |
| [MaxValue](#maxvalue-method-1-of-2))(…) | Max Value for Integer Option (2 methods) |
| [MinLength](#minlength-method)(…) | Min Length for String Option Max Of 6000 |
| [MinValue](#minvalue-method-1-of-2))(…) | Min Value for Integer Option (2 methods) |
| [Required](#required-method)(…) | Set the required state for the option |

## See Also

* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [ApplicationCommandOptionBuilder.cs](../../../../Oxide.Ext.Discord/Builders/ApplicationCommands/ApplicationCommandOptionBuilder.cs)
   
   
# AddNameLocalizations method

Adds command name localizations for a given plugin and lang key

```csharp
[Obsolete("AddNameLocalizations(Plugin plugin, string langKey) has been deprecated and will be removed in the future. Please use AddNameLocalization(string name, string lang) instead.")]
public ApplicationCommandOptionBuilder AddNameLocalizations(Plugin plugin, string langKey)
```

| parameter | description |
| --- | --- |
| plugin | Plugin containing the localizations |
| langKey | Lang Key containing the localized text |

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddNameLocalization method

Adds Application Command Option Name Localization

```csharp
public ApplicationCommandOptionBuilder AddNameLocalization(string name, ServerLocale serverLocale)
```

| parameter | description |
| --- | --- |
| name | Localized name value |
| serverLocale | Oxide lang the value is in |

## Return Value

This

## See Also

* struct [ServerLocale](../../Libraries/Locale/ServerLocale.md)
* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddDescriptionLocalizations method

Adds command description localizations for a given plugin and lang key

```csharp
[Obsolete("AddDescriptionLocalizations(Plugin plugin, string langKey) has been deprecated and will be removed in the future. Please use AddDescriptionLocalization(string name, string lang) instead.")]
public ApplicationCommandOptionBuilder AddDescriptionLocalizations(Plugin plugin, string langKey)
```

| parameter | description |
| --- | --- |
| plugin | Plugin containing the localizations |
| langKey | Lang Key containing the localized text |

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddDescriptionLocalization method

Adds Application Command Option Description Localization

```csharp
public ApplicationCommandOptionBuilder AddDescriptionLocalization(string description, 
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
* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Required method

Set the required state for the option

```csharp
public ApplicationCommandOptionBuilder Required(bool required = true)
```

| parameter | description |
| --- | --- |
| required | If the option is required (Default: true) |

## Return Value

This

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AutoComplete method

Enable auto complete for the option

```csharp
public ApplicationCommandOptionBuilder AutoComplete(bool autoComplete = true)
```

| parameter | description |
| --- | --- |
| autoComplete | If the option support auto complete (Default: true) |

## Return Value

This

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# MinValue method (1 of 2)

Min Value for Number Option

```csharp
public ApplicationCommandOptionBuilder MinValue(double minValue)
```

| parameter | description |
| --- | --- |
| minValue | Min Value |

## Return Value

This

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# MinValue method (2 of 2)

Min Value for Integer Option

```csharp
public ApplicationCommandOptionBuilder MinValue(int minValue)
```

| parameter | description |
| --- | --- |
| minValue | Min Value |

## Return Value

This

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# MaxValue method (1 of 2)

Max Value for Number Option

```csharp
public ApplicationCommandOptionBuilder MaxValue(double maxValue)
```

| parameter | description |
| --- | --- |
| maxValue | Max Value |

## Return Value

This

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# MaxValue method (2 of 2)

Max Value for Integer Option

```csharp
public ApplicationCommandOptionBuilder MaxValue(int maxValue)
```

| parameter | description |
| --- | --- |
| maxValue | Max Value |

## Return Value

This

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# MinLength method

Min Length for String Option Max Of 6000

```csharp
public ApplicationCommandOptionBuilder MinLength(int minLength)
```

| parameter | description |
| --- | --- |
| minLength | Min Length for the string |

## Return Value

This

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# MaxLength method

Max Length for String Option Max Of 6000

```csharp
public ApplicationCommandOptionBuilder MaxLength(int maxLength)
```

| parameter | description |
| --- | --- |
| maxLength | Max Length |

## Return Value

This

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ChannelTypes method

Set's the channel types for the option

```csharp
public ApplicationCommandOptionBuilder ChannelTypes(List<ChannelType> types)
```

| parameter | description |
| --- | --- |
| types | Types of channels the option allows |

## Return Value

This

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if [`CommandOptionType`](../../Entities/Interactions/ApplicationCommands/CommandOptionType.md) is not Channel |

## See Also

* enum [ChannelType](../../Entities/Channels/ChannelType.md)
* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddChoice method (1 of 3)

Adds a choice to this option of type double

```csharp
public ApplicationCommandOptionBuilder AddChoice(string name, double value, 
    Hash<string, string> nameLocalizations = null)
```

| parameter | description |
| --- | --- |
| name | Name of the choice |
| value | Value of the choice |
| nameLocalizations | Localizations for the name |

## Return Value

This

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if option type is not double |

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# AddChoice method (2 of 3)

Adds a choice to this option of type int

```csharp
public ApplicationCommandOptionBuilder AddChoice(string name, int value, 
    Hash<string, string> nameLocalizations = null)
```

| parameter | description |
| --- | --- |
| name | Name of the choice |
| value | Value of the choice |
| nameLocalizations | Localizations for the name |

## Return Value

This

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if option type is not int |

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# AddChoice method (3 of 3)

Adds a choice to this option of type string

```csharp
public ApplicationCommandOptionBuilder AddChoice(string name, string value, 
    Hash<string, string> nameLocalizations = null)
```

| parameter | description |
| --- | --- |
| name | Name of the choice |
| value | Value of the choice |
| nameLocalizations | Localizations for the name |

## Return Value

This

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if option type is not string |

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# CommandName field

The Name of the command

```csharp
public readonly string CommandName;
```

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GroupName field

The Name of the group

```csharp
public readonly string GroupName;
```

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# SubCommandName field

The Name of the Sub Command

```csharp
public readonly string SubCommandName;
```

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# OptionName field

The Name of the Option

```csharp
public readonly string OptionName;
```

## See Also

* class [ApplicationCommandOptionBuilder](./ApplicationCommandOptionBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
