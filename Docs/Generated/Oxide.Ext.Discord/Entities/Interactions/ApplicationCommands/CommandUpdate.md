# CommandUpdate class

Represents [Application Command Update](https://discord.com/developers/docs/interactions/application-commands#edit-global-application-command-json-params)

```csharp
public class CommandUpdate
```

## Public Members

| name | description |
| --- | --- |
| [CommandUpdate](#CommandUpdate-constructor)() | The default constructor. |
| [DefaultMemberPermissions](#DefaultMemberPermissions-property) { get; set; } | Set of permissions represented as a bit set |
| [DefaultPermissions](#DefaultPermissions-property) { get; set; } | Whether the command is enabled by default when the app is added to a guild |
| [Description](#Description-property) { get; set; } | Description of the command (1-100 characters) |
| [DescriptionLocalizations](#DescriptionLocalizations-property) { get; set; } | Localization dictionary for the description field. Values follow the same restrictions as description |
| [DmPermission](#DmPermission-property) { get; set; } | Indicates whether the command is available in DMs with the app, only for globally-scoped commands. By default, commands are visible. |
| [Name](#Name-property) { get; set; } | 1-32 lowercase character name matching ^[\w-]{1,32}$ |
| [NameLocalizations](#NameLocalizations-property) { get; set; } | Localization dictionary for the name field. Values follow the same restrictions as name |
| [Nsfw](#Nsfw-property) { get; set; } | Indicates whether the command is age-restricted |
| [Options](#Options-property) { get; set; } | The parameters for the command See [`CommandOption`](./CommandOption.md) |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [CommandUpdate.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Interactions/ApplicationCommands/CommandUpdate.cs)
   
   
# CommandUpdate constructor

The default constructor.

```csharp
public CommandUpdate()
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Name property

1-32 lowercase character name matching ^[\w-]{1,32}$

```csharp
public string Name { get; set; }
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# NameLocalizations property

Localization dictionary for the name field. Values follow the same restrictions as name

```csharp
public Hash<string, string> NameLocalizations { get; set; }
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Description property

Description of the command (1-100 characters)

```csharp
public string Description { get; set; }
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# DescriptionLocalizations property

Localization dictionary for the description field. Values follow the same restrictions as description

```csharp
public Hash<string, string> DescriptionLocalizations { get; set; }
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Options property

The parameters for the command See [`CommandOption`](./CommandOption.md)

```csharp
public List<CommandOption> Options { get; set; }
```

## See Also

* class [CommandOption](./CommandOption.md)
* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# DefaultMemberPermissions property

Set of permissions represented as a bit set

```csharp
public PermissionFlags DefaultMemberPermissions { get; set; }
```

## See Also

* enum [PermissionFlags](../../Permissions/PermissionFlags.md)
* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# DmPermission property

Indicates whether the command is available in DMs with the app, only for globally-scoped commands. By default, commands are visible.

```csharp
public bool? DmPermission { get; set; }
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# DefaultPermissions property

Whether the command is enabled by default when the app is added to a guild

```csharp
public bool? DefaultPermissions { get; set; }
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Nsfw property

Indicates whether the command is age-restricted

```csharp
public bool? Nsfw { get; set; }
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
