# AutoModActionExecutionEvent class

Represents [Auto Moderation Action Execution Event](https://discord.com/developers/docs/topics/gateway#auto-moderation-action-execution-auto-moderation-action-execution-event-fields)

```csharp
public class AutoModActionExecutionEvent
```

## Public Members

| name | description |
| --- | --- |
| [AutoModActionExecutionEvent](#AutoModActionExecutionEvent-constructor)() | The default constructor. |
| [Action](#Action-property) { get; set; } | The action which was executed |
| [AlertSystemMessageId](#AlertSystemMessageId-property) { get; set; } | The id of any system auto moderation messages posted as a result of this action |
| [Content](#Content-property) { get; set; } | The user generated text content |
| [GuildId](#GuildId-property) { get; set; } | Id of the guild in which action was executed |
| [MatchedContent](#MatchedContent-property) { get; set; } | The substring in content that triggered the rule |
| [MatchedKeyword](#MatchedKeyword-property) { get; set; } | The word or phrase configured in the rule that triggered the rule |
| [MessageId](#MessageId-property) { get; set; } | Id of any user message which content belongs to |
| [RuleId](#RuleId-property) { get; set; } | Id of the rule which action belongs to |
| [RuleTriggerType](#RuleTriggerType-property) { get; set; } | The [`AutoModTriggerType`](../../AutoMod/AutoModTriggerType.md) of rule which was triggered |
| [UserId](#UserId-property) { get; set; } | Id of the user which generated the content which triggered the rule |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [AutoModActionExecutionEvent.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Gateway/Events/AutoModActionExecutionEvent.cs)
   
   
# AutoModActionExecutionEvent constructor

The default constructor.

```csharp
public AutoModActionExecutionEvent()
```

## See Also

* class [AutoModActionExecutionEvent](./AutoModActionExecutionEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# GuildId property

Id of the guild in which action was executed

```csharp
public Snowflake GuildId { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [AutoModActionExecutionEvent](./AutoModActionExecutionEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Action property

The action which was executed

```csharp
public AutoModAction Action { get; set; }
```

## See Also

* class [AutoModAction](../../AutoMod/AutoModAction.md)
* class [AutoModActionExecutionEvent](./AutoModActionExecutionEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# RuleId property

Id of the rule which action belongs to

```csharp
public Snowflake RuleId { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [AutoModActionExecutionEvent](./AutoModActionExecutionEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# RuleTriggerType property

The [`AutoModTriggerType`](../../AutoMod/AutoModTriggerType.md) of rule which was triggered

```csharp
public AutoModTriggerType RuleTriggerType { get; set; }
```

## See Also

* enum [AutoModTriggerType](../../AutoMod/AutoModTriggerType.md)
* class [AutoModActionExecutionEvent](./AutoModActionExecutionEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# UserId property

Id of the user which generated the content which triggered the rule

```csharp
public Snowflake? UserId { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [AutoModActionExecutionEvent](./AutoModActionExecutionEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# MessageId property

Id of any user message which content belongs to

```csharp
public Snowflake? MessageId { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [AutoModActionExecutionEvent](./AutoModActionExecutionEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# AlertSystemMessageId property

The id of any system auto moderation messages posted as a result of this action

```csharp
public Snowflake? AlertSystemMessageId { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [AutoModActionExecutionEvent](./AutoModActionExecutionEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Content property

The user generated text content

```csharp
public string Content { get; set; }
```

## See Also

* class [AutoModActionExecutionEvent](./AutoModActionExecutionEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# MatchedKeyword property

The word or phrase configured in the rule that triggered the rule

```csharp
public string MatchedKeyword { get; set; }
```

## See Also

* class [AutoModActionExecutionEvent](./AutoModActionExecutionEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# MatchedContent property

The substring in content that triggered the rule

```csharp
public string MatchedContent { get; set; }
```

## See Also

* class [AutoModActionExecutionEvent](./AutoModActionExecutionEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Gateway.Events](./EventsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
