# CommandCreate class

Represents [Application Command Create](https://discord.com/developers/docs/interactions/application-commands#create-global-application-command-json-params) Represents [Application Command Create](https://discord.com/developers/docs/interactions/application-commands#create-guild-application-command-json-params)

```csharp
public class CommandCreate
```

## Public Members

| name | description |
| --- | --- |
| [CommandCreate](#commandcreate-constructor)() | Constructor |
| [CommandCreate](#commandcreate-constructor)(…) | Constructor |
| [Contexts](#contexts-property) { get; set; } | Interaction context(s) where the command can be used |
| [DefaultMemberPermissions](#defaultmemberpermissions-property) { get; set; } | Set of permissions represented as a bit set |
| [Description](#description-property) { get; set; } | Description of the command (1-100 characters) |
| [DescriptionLocalizations](#descriptionlocalizations-property) { get; set; } | Localization dictionary for the description field. Values follow the same restrictions as description |
| [DmPermission](#dmpermission-property) { get; set; } | Indicates whether the command is available in DMs with the app, only for globally-scoped commands. By default, commands are visible. |
| [IntegrationTypes](#integrationtypes-property) { get; set; } | Installation context(s) where the command is available |
| [Name](#name-property) { get; set; } | 1-32 lowercase character name matching ^[-_\p{L}\p{N}\p{sc=Deva}\p{sc=Thai}]{1,32}$ |
| [NameLocalizations](#namelocalizations-property) { get; set; } | Localization dictionary for the name field. Values follow the same restrictions as name |
| [Nsfw](#nsfw-property) { get; set; } | Indicates whether the command is age-restricted |
| [Options](#options-property) { get; set; } | The parameters for the command See [`CommandOption`](./CommandOption.md) |
| [Type](#type-property) { get; set; } | The [`ApplicationCommandType`](./ApplicationCommandType.md) of the command |
| [Validate](#validate-method)() |  |

## See Also

* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [CommandCreate.cs](../../../../Oxide.Ext.Discord/Entities/CommandCreate.cs)
   
   
# Validate method

```csharp
public void Validate()
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# CommandCreate constructor (1 of 2)

Constructor

```csharp
public CommandCreate()
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# CommandCreate constructor (2 of 2)

Constructor

```csharp
public CommandCreate(string name, string description, 
    ApplicationCommandType type = ApplicationCommandType.ChatInput, 
    List<CommandOption> options = null)
```

## See Also

* enum [ApplicationCommandType](./ApplicationCommandType.md)
* class [CommandOption](./CommandOption.md)
* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Name property

1-32 lowercase character name matching ^[-_\p{L}\p{N}\p{sc=Deva}\p{sc=Thai}]{1,32}$

```csharp
public string Name { get; set; }
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# NameLocalizations property

Localization dictionary for the name field. Values follow the same restrictions as name

```csharp
public Hash<string, string> NameLocalizations { get; set; }
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Description property

Description of the command (1-100 characters)

```csharp
public string Description { get; set; }
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DescriptionLocalizations property

Localization dictionary for the description field. Values follow the same restrictions as description

```csharp
public Hash<string, string> DescriptionLocalizations { get; set; }
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Options property

The parameters for the command See [`CommandOption`](./CommandOption.md)

```csharp
public List<CommandOption> Options { get; set; }
```

## See Also

* class [CommandOption](./CommandOption.md)
* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DefaultMemberPermissions property

Set of permissions represented as a bit set

```csharp
public PermissionFlags DefaultMemberPermissions { get; set; }
```

## See Also

* enum [PermissionFlags](./PermissionFlags.md)
* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DmPermission property

Indicates whether the command is available in DMs with the app, only for globally-scoped commands. By default, commands are visible.

```csharp
[Obsolete("Deprecated (use Contexts instead)")]
public bool? DmPermission { get; set; }
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IntegrationTypes property

Installation context(s) where the command is available

```csharp
public List<ApplicationIntegrationType> IntegrationTypes { get; set; }
```

## See Also

* enum [ApplicationIntegrationType](./ApplicationIntegrationType.md)
* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Contexts property

Interaction context(s) where the command can be used

```csharp
public List<InteractionContextTypes> Contexts { get; set; }
```

## See Also

* enum [InteractionContextTypes](./InteractionContextTypes.md)
* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Type property

The [`ApplicationCommandType`](./ApplicationCommandType.md) of the command

```csharp
public ApplicationCommandType Type { get; set; }
```

## See Also

* enum [ApplicationCommandType](./ApplicationCommandType.md)
* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Nsfw property

Indicates whether the command is age-restricted

```csharp
public bool? Nsfw { get; set; }
```

## See Also

* class [CommandCreate](./CommandCreate.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
