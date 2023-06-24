# CommandCreate class

Represents [Application Command Create](https://discord.com/developers/docs/interactions/application-commands#create-global-application-command-json-params) Represents [Application Command Create](https://discord.com/developers/docs/interactions/application-commands#create-guild-application-command-json-params)

```csharp
public class CommandCreate
```

## Public Members

| name | description |
| --- | --- |
| [CommandCreate](#CommandCreate)() | Constructor |
| [CommandCreate](#CommandCreate)(…) | Constructor |
| [DefaultMemberPermissions](#DefaultMemberPermissions) { get; set; } | Set of permissions represented as a bit set |
| [Description](#Description) { get; set; } | Description of the command (1-100 characters) |
| [DescriptionLocalizations](#DescriptionLocalizations) { get; set; } | Localization dictionary for the description field. Values follow the same restrictions as description |
| [DmPermission](#DmPermission) { get; set; } | Indicates whether the command is available in DMs with the app, only for globally-scoped commands. By default, commands are visible. |
| [Name](#Name) { get; set; } | 1-32 lowercase character name matching ^[-_\p{L}\p{N}\p{sc=Deva}\p{sc=Thai}]{1,32}$ |
| [NameLocalizations](#NameLocalizations) { get; set; } | Localization dictionary for the name field. Values follow the same restrictions as name |
| [Nsfw](#Nsfw) { get; set; } | Indicates whether the command is age-restricted |
| [Options](#Options) { get; set; } | The parameters for the command See [`CommandOption`](./CommandOption.md) |
| [Type](#Type) { get; set; } | The [`ApplicationCommandType`](./ApplicationCommandType.md) of the command |
| [Validate](#Validate)() |  |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [CommandCreate.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/CommandCreate.cs)
   
   
# Validate method

```csharp
public void Validate()
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# CommandCreate constructor (1 of 2)

Constructor

```csharp
public CommandCreate()
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---
   
   
# Name property

1-32 lowercase character name matching ^[-_\p{L}\p{N}\p{sc=Deva}\p{sc=Thai}]{1,32}$

```csharp
public string Name { get; set; }
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# NameLocalizations property

Localization dictionary for the name field. Values follow the same restrictions as name

```csharp
public Hash<string, string> NameLocalizations { get; set; }
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Description property

Description of the command (1-100 characters)

```csharp
public string Description { get; set; }
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# DescriptionLocalizations property

Localization dictionary for the description field. Values follow the same restrictions as description

```csharp
public Hash<string, string> DescriptionLocalizations { get; set; }
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Options property

The parameters for the command See [`CommandOption`](./CommandOption.md)

```csharp
public List<CommandOption> Options { get; set; }
```

## See Also

* class [CommandOption](./CommandOption.md)
* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# DefaultMemberPermissions property

Set of permissions represented as a bit set

```csharp
public PermissionFlags DefaultMemberPermissions { get; set; }
```

## See Also

* enum [PermissionFlags](../../Permissions/PermissionFlags.md)
* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# DmPermission property

Indicates whether the command is available in DMs with the app, only for globally-scoped commands. By default, commands are visible.

```csharp
public bool? DmPermission { get; set; }
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Nsfw property

Indicates whether the command is age-restricted

```csharp
public bool? Nsfw { get; set; }
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Type property

The [`ApplicationCommandType`](./ApplicationCommandType.md) of the command

```csharp
public ApplicationCommandType Type { get; set; }
```

## See Also

* enum [ApplicationCommandType](./ApplicationCommandType.md)
* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
