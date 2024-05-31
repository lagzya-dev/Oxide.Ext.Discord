# CommandUpdate class

Represents [Application Command Update](https://discord.com/developers/docs/interactions/application-commands#edit-global-application-command-json-params)

```csharp
public class CommandUpdate
```

## Public Members

| name | description |
| --- | --- |
| [CommandUpdate](#commandupdate-constructor)() | The default constructor. |
| [Contexts](#contexts-property) { get; set; } | Interaction context(s) where the command can be used |
| [DefaultMemberPermissions](#defaultmemberpermissions-property) { get; set; } | Set of permissions represented as a bit set |
| [DefaultPermissions](#defaultpermissions-property) { get; set; } | Whether the command is enabled by default when the app is added to a guild |
| [Description](#description-property) { get; set; } | Description of the command (1-100 characters) |
| [DescriptionLocalizations](#descriptionlocalizations-property) { get; set; } | Localization dictionary for the description field. Values follow the same restrictions as description |
| [DmPermission](#dmpermission-property) { get; set; } | Indicates whether the command is available in DMs with the app, only for globally-scoped commands. By default, commands are visible. |
| [IntegrationTypes](#integrationtypes-property) { get; set; } | Installation context(s) where the command is available |
| [Name](#name-property) { get; set; } | 1-32 lowercase character name matching ^[\w-]{1,32}$ |
| [NameLocalizations](#namelocalizations-property) { get; set; } | Localization dictionary for the name field. Values follow the same restrictions as name |
| [Nsfw](#nsfw-property) { get; set; } | Indicates whether the command is age-restricted |
| [Options](#options-property) { get; set; } | The parameters for the command See [`CommandOption`](./CommandOption.md) |

## See Also

* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [CommandUpdate.cs](../../../../Oxide.Ext.Discord/Entities/CommandUpdate.cs)
   
   
# CommandUpdate constructor

The default constructor.

```csharp
public CommandUpdate()
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Name property

1-32 lowercase character name matching ^[\w-]{1,32}$

```csharp
public string Name { get; set; }
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# NameLocalizations property

Localization dictionary for the name field. Values follow the same restrictions as name

```csharp
public Hash<string, string> NameLocalizations { get; set; }
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Description property

Description of the command (1-100 characters)

```csharp
public string Description { get; set; }
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DescriptionLocalizations property

Localization dictionary for the description field. Values follow the same restrictions as description

```csharp
public Hash<string, string> DescriptionLocalizations { get; set; }
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Options property

The parameters for the command See [`CommandOption`](./CommandOption.md)

```csharp
public List<CommandOption> Options { get; set; }
```

## See Also

* class [CommandOption](./CommandOption.md)
* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DefaultMemberPermissions property

Set of permissions represented as a bit set

```csharp
public PermissionFlags DefaultMemberPermissions { get; set; }
```

## See Also

* enum [PermissionFlags](./PermissionFlags.md)
* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DmPermission property

Indicates whether the command is available in DMs with the app, only for globally-scoped commands. By default, commands are visible.

```csharp
[Obsolete("Deprecated (use Contexts instead)")]
public bool? DmPermission { get; set; }
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DefaultPermissions property

Whether the command is enabled by default when the app is added to a guild

```csharp
public bool? DefaultPermissions { get; set; }
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IntegrationTypes property

Installation context(s) where the command is available

```csharp
public List<ApplicationIntegrationType> IntegrationTypes { get; set; }
```

## See Also

* enum [ApplicationIntegrationType](./ApplicationIntegrationType.md)
* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Contexts property

Interaction context(s) where the command can be used

```csharp
public List<InteractionContextTypes> Contexts { get; set; }
```

## See Also

* enum [InteractionContextTypes](./InteractionContextTypes.md)
* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Nsfw property

Indicates whether the command is age-restricted

```csharp
public bool? Nsfw { get; set; }
```

## See Also

* class [CommandUpdate](./CommandUpdate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
