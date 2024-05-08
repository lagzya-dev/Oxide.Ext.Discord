# AutoModAction class

Represents [Auto Mod Action](https://discord.com/developers/docs/resources/auto-moderation#auto-moderation-action-object)

```csharp
public class AutoModAction
```

## Public Members

| name | description |
| --- | --- |
| [AutoModAction](#automodaction-constructor)() | The default constructor. |
| [Metadata](#metadata-property) { get; set; } | Additional metadata needed during execution for this specific action type |
| [Type](#type-property) { get; set; } | Type of [`AutoModActionType`](./AutoModActionType.md) |

## See Also

* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [AutoModAction.cs](../../../../Oxide.Ext.Discord/Entities/AutoModAction.cs)
   
   
# AutoModAction constructor

The default constructor.

```csharp
public AutoModAction()
```

## See Also

* class [AutoModAction](./AutoModAction.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Type property

Type of [`AutoModActionType`](./AutoModActionType.md)

```csharp
public AutoModActionType Type { get; set; }
```

## See Also

* enum [AutoModActionType](./AutoModActionType.md)
* class [AutoModAction](./AutoModAction.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Metadata property

Additional metadata needed during execution for this specific action type

```csharp
public AutoModActionMetadata Metadata { get; set; }
```

## See Also

* class [AutoModActionMetadata](./AutoModActionMetadata.md)
* class [AutoModAction](./AutoModAction.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->