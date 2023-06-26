# DiscordRole class

Represents [Role Structure](https://discord.com/developers/docs/topics/permissions#role-object)

```csharp
public class DiscordRole : ISnowflakeEntity
```

## Public Members

| name | description |
| --- | --- |
| [DiscordRole](#discordrole-constructor)() | The default constructor. |
| [Color](#color-property) { get; set; } | Role Color |
| [Hoist](#hoist-property) { get; set; } | If this role is pinned in the user listing |
| [Icon](#icon-property) { get; set; } | The role's icon image (if the guild has the ROLE_ICONS feature) |
| [Id](#id-property) { get; set; } | Role id |
| [Managed](#managed-property) { get; set; } | Whether this role is managed by an integration |
| [Mention](#mention-property) { get; } | Returns a string to mention this role in a message |
| [Mentionable](#mentionable-property) { get; set; } | Whether this role is mentionable |
| [Name](#name-property) { get; set; } | Role name |
| [Permissions](#permissions-property) { get; set; } | Role Permissions |
| [Position](#position-property) { get; set; } | Position of this role |
| [RoleIcon](#roleicon-property) { get; } | Return the Role Icon URL for a Discord Role. Empty string is not set. |
| [Tags](#tags-property) { get; set; } | The tags this role has |
| [UnicodeEmoji](#unicodeemoji-property) { get; set; } | The role's unicode emoji as a standard emoji (if the guild has the ROLE_ICONS feature) |
| [HasPermission](#haspermission-method)(…) | Returns if the role has the specified permission |
| [IsBoosterRole](#isboosterrole-method)() | Returns if this role is the booster |
| [Validate](#validate-method)() |  |

## See Also

* interface [ISnowflakeEntity](../../Interfaces/ISnowflakeEntity.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordRole.cs](../../../../Oxide.Ext.Discord/Entities/Permissions/DiscordRole.cs)
   
   
# HasPermission method

Returns if the role has the specified permission

```csharp
public bool HasPermission(PermissionFlags perm)
```

| parameter | description |
| --- | --- |
| perm | Permission to check for |

## Return Value

Return true if role has permission; false otherwise

## See Also

* enum [PermissionFlags](./PermissionFlags.md)
* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# IsBoosterRole method

Returns if this role is the booster

```csharp
public bool IsBoosterRole()
```

## Return Value

True if booster role. False otherwise;

## See Also

* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Validate method

```csharp
public void Validate()
```

## See Also

* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# DiscordRole constructor

The default constructor.

```csharp
public DiscordRole()
```

## See Also

* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Id property

Role id

```csharp
public Snowflake Id { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Name property

Role name

```csharp
public string Name { get; set; }
```

## See Also

* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Color property

Role Color

```csharp
public DiscordColor Color { get; set; }
```

## See Also

* struct [DiscordColor](../DiscordColor.md)
* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Hoist property

If this role is pinned in the user listing

```csharp
public bool? Hoist { get; set; }
```

## See Also

* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Icon property

The role's icon image (if the guild has the ROLE_ICONS feature)

```csharp
public string Icon { get; set; }
```

## See Also

* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# UnicodeEmoji property

The role's unicode emoji as a standard emoji (if the guild has the ROLE_ICONS feature)

```csharp
public string UnicodeEmoji { get; set; }
```

## See Also

* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Position property

Position of this role

```csharp
public int Position { get; set; }
```

## See Also

* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Permissions property

Role Permissions

```csharp
public PermissionFlags Permissions { get; set; }
```

## See Also

* enum [PermissionFlags](./PermissionFlags.md)
* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Managed property

Whether this role is managed by an integration

```csharp
public bool Managed { get; set; }
```

## See Also

* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Mentionable property

Whether this role is mentionable

```csharp
public bool Mentionable { get; set; }
```

## See Also

* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Tags property

The tags this role has

```csharp
public RoleTags Tags { get; set; }
```

## See Also

* class [RoleTags](./RoleTags.md)
* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Mention property

Returns a string to mention this role in a message

```csharp
public string Mention { get; }
```

## See Also

* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RoleIcon property

Return the Role Icon URL for a Discord Role. Empty string is not set.

```csharp
public string RoleIcon { get; }
```

## See Also

* class [DiscordRole](./DiscordRole.md)
* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
