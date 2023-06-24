# AutoModRuleModify class

Represents [Auto Mod Rule Modify](https://discord.com/developers/docs/resources/auto-moderation#modify-auto-moderation-rule-json-params)

```csharp
public class AutoModRuleModify
```

## Public Members

| name | description |
| --- | --- |
| [AutoModRuleModify](#AutoModRuleModify-constructor)(…) | Constructor |
| [Actions](#Actions-property) { get; set; } | Actions which will execute when the rule is triggered |
| [Enabled](#Enabled-property) { get; set; } | Whether the rule is enabled |
| [EventType](#EventType-property) { get; set; } | Rule [`AutoModEventType`](./AutoModEventType.md) |
| [ExemptChannels](#ExemptChannels-property) { get; set; } | Channel ids that should not be affected by the rule (Maximum of 50) |
| [ExemptRoles](#ExemptRoles-property) { get; set; } | Role ids that should not be affected by the rule (Maximum of 20) |
| [Name](#Name-property) { get; set; } | Rule name |
| [TriggerMetadata](#TriggerMetadata-property) { get; set; } | Rule [`AutoModTriggerMetadata`](./AutoModTriggerMetadata.md) |
| [TriggerType](#TriggerType-property) { get; } | Rule [`AutoModTriggerType`](./AutoModTriggerType.md) |
| [Validate](#Validate-method)() |  |

## See Also

* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [AutoModRuleModify.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/AutoMod/AutoModRuleModify.cs)
   
   
# Validate method

```csharp
public void Validate()
```

## See Also

* class [AutoModRuleModify](./AutoModRuleModify.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AutoModRuleModify constructor

Constructor

```csharp
public AutoModRuleModify(AutoModTriggerType triggerType)
```

| parameter | description |
| --- | --- |
| triggerType | Trigger type being modified |

## See Also

* enum [AutoModTriggerType](./AutoModTriggerType.md)
* class [AutoModRuleModify](./AutoModRuleModify.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Name property

Rule name

```csharp
public string Name { get; set; }
```

## See Also

* class [AutoModRuleModify](./AutoModRuleModify.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# EventType property

Rule [`AutoModEventType`](./AutoModEventType.md)

```csharp
public AutoModEventType EventType { get; set; }
```

## See Also

* enum [AutoModEventType](./AutoModEventType.md)
* class [AutoModRuleModify](./AutoModRuleModify.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# TriggerMetadata property

Rule [`AutoModTriggerMetadata`](./AutoModTriggerMetadata.md)

```csharp
public AutoModTriggerMetadata TriggerMetadata { get; set; }
```

## See Also

* class [AutoModTriggerMetadata](./AutoModTriggerMetadata.md)
* class [AutoModRuleModify](./AutoModRuleModify.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Actions property

Actions which will execute when the rule is triggered

```csharp
public List<AutoModAction> Actions { get; set; }
```

## See Also

* class [AutoModAction](./AutoModAction.md)
* class [AutoModRuleModify](./AutoModRuleModify.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Enabled property

Whether the rule is enabled

```csharp
public bool Enabled { get; set; }
```

## See Also

* class [AutoModRuleModify](./AutoModRuleModify.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ExemptRoles property

Role ids that should not be affected by the rule (Maximum of 20)

```csharp
public List<Snowflake> ExemptRoles { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [AutoModRuleModify](./AutoModRuleModify.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ExemptChannels property

Channel ids that should not be affected by the rule (Maximum of 50)

```csharp
public List<Snowflake> ExemptChannels { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [AutoModRuleModify](./AutoModRuleModify.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# TriggerType property

Rule [`AutoModTriggerType`](./AutoModTriggerType.md)

```csharp
public AutoModTriggerType TriggerType { get; }
```

## See Also

* enum [AutoModTriggerType](./AutoModTriggerType.md)
* class [AutoModRuleModify](./AutoModRuleModify.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
