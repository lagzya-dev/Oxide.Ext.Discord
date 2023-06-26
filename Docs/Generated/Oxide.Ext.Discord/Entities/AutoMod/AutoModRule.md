# AutoModRule class

Represents [Auto Mod Rule](https://discord.com/developers/docs/resources/auto-moderation#auto-moderation-rule-object)

```csharp
public class AutoModRule
```

## Public Members

| name | description |
| --- | --- |
| [AutoModRule](#automodrule-constructor)() | The default constructor. |
| [Actions](#actions-property) { get; set; } | Actions which will execute when the rule is triggered |
| [CreatorId](#creatorid-property) { get; set; } | User which first created this rule |
| [Enabled](#enabled-property) { get; set; } | Whether the rule is enabled |
| [EventType](#eventtype-property) { get; set; } | Rule [`AutoModEventType`](./AutoModEventType.md) |
| [ExemptChannels](#exemptchannels-property) { get; set; } | Channel ids that should not be affected by the rule (Maximum of 50) |
| [ExemptRoles](#exemptroles-property) { get; set; } | Role ids that should not be affected by the rule (Maximum of 20) |
| [GuildId](#guildid-property) { get; set; } | ID of the Guild which this rule belongs to |
| [Id](#id-property) { get; set; } | Id of this rule |
| [Name](#name-property) { get; set; } | Rule name |
| [TriggerMetadata](#triggermetadata-property) { get; set; } | Rule [`AutoModTriggerMetadata`](./AutoModTriggerMetadata.md) |
| [TriggerType](#triggertype-property) { get; set; } | Rule [`AutoModTriggerType`](./AutoModTriggerType.md) |
| [Delete](#delete-method)(…) | Delete a rule Requires ManageGuild permissions. See [Delete Auto Moderation Rule](https://discord.com/developers/docs/resources/auto-moderation#delete-auto-moderation-rule) |
| [Edit](#edit-method)(…) | Modify an existing rule Requires ManageGuild permissions. See [Modify Auto Moderation Rule](https://discord.com/developers/docs/resources/auto-moderation#modify-auto-moderation-rule) |
| static [Create](#create-method)(…) | Create a new rule Requires ManageGuild permissions. See [Create Auto Moderation Rule](https://discord.com/developers/docs/resources/auto-moderation#create-auto-moderation-rule) |
| static [Get](#get-method)(…) | Get a single rule Requires ManageGuild permissions. See [Get Auto Moderation Rule](https://discord.com/developers/docs/resources/auto-moderation#get-auto-moderation-rule) |
| static [GetAll](#getall-method)(…) | Modify an existing rule Requires ManageGuild permissions. See [List Auto Moderation Rules for Guild](https://discord.com/developers/docs/resources/auto-moderation#list-auto-moderation-rules-for-guild) |

## See Also

* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [AutoModRule.cs](../../../../Oxide.Ext.Discord/Entities/AutoMod/AutoModRule.cs)
   
   
# GetAll method

Modify an existing rule Requires ManageGuild permissions. See [List Auto Moderation Rules for Guild](https://discord.com/developers/docs/resources/auto-moderation#list-auto-moderation-rules-for-guild)

```csharp
public static IPromise<List<AutoModRule>> GetAll(DiscordClient client, Snowflake guildId)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| guildId | Guild ID to list the rules for |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* struct [Snowflake](../Snowflake.md)
* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Get method

Get a single rule Requires ManageGuild permissions. See [Get Auto Moderation Rule](https://discord.com/developers/docs/resources/auto-moderation#get-auto-moderation-rule)

```csharp
public static IPromise<AutoModRule> Get(DiscordClient client, Snowflake guildId, Snowflake ruleId)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| guildId | Guild ID of the rule |
| ruleId | Rule ID to get the rule for |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* struct [Snowflake](../Snowflake.md)
* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Create method

Create a new rule Requires ManageGuild permissions. See [Create Auto Moderation Rule](https://discord.com/developers/docs/resources/auto-moderation#create-auto-moderation-rule)

```csharp
public static IPromise<AutoModRule> Create(DiscordClient client, Snowflake guildId, 
    AutoModRuleCreate create)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| guildId | Guild ID of the rule |
| create | Rule to be created |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* struct [Snowflake](../Snowflake.md)
* class [AutoModRuleCreate](./AutoModRuleCreate.md)
* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Edit method

Modify an existing rule Requires ManageGuild permissions. See [Modify Auto Moderation Rule](https://discord.com/developers/docs/resources/auto-moderation#modify-auto-moderation-rule)

```csharp
public IPromise<AutoModRule> Edit(DiscordClient client, AutoModRuleModify modify)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| modify | [`AutoModRuleModify`](./AutoModRuleModify.md) |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* class [AutoModRuleModify](./AutoModRuleModify.md)
* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Delete method

Delete a rule Requires ManageGuild permissions. See [Delete Auto Moderation Rule](https://discord.com/developers/docs/resources/auto-moderation#delete-auto-moderation-rule)

```csharp
public IPromise Delete(DiscordClient client)
```

| parameter | description |
| --- | --- |
| client | Client to use |

## See Also

* interface [IPromise](../../Interfaces/Promises/IPromise.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AutoModRule constructor

The default constructor.

```csharp
public AutoModRule()
```

## See Also

* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Id property

Id of this rule

```csharp
public Snowflake Id { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GuildId property

ID of the Guild which this rule belongs to

```csharp
public Snowflake GuildId { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Name property

Rule name

```csharp
public string Name { get; set; }
```

## See Also

* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# CreatorId property

User which first created this rule

```csharp
public Snowflake CreatorId { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# EventType property

Rule [`AutoModEventType`](./AutoModEventType.md)

```csharp
public AutoModEventType EventType { get; set; }
```

## See Also

* enum [AutoModEventType](./AutoModEventType.md)
* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# TriggerType property

Rule [`AutoModTriggerType`](./AutoModTriggerType.md)

```csharp
public AutoModTriggerType TriggerType { get; set; }
```

## See Also

* enum [AutoModTriggerType](./AutoModTriggerType.md)
* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# TriggerMetadata property

Rule [`AutoModTriggerMetadata`](./AutoModTriggerMetadata.md)

```csharp
public AutoModTriggerMetadata TriggerMetadata { get; set; }
```

## See Also

* class [AutoModTriggerMetadata](./AutoModTriggerMetadata.md)
* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Actions property

Actions which will execute when the rule is triggered

```csharp
public List<AutoModAction> Actions { get; set; }
```

## See Also

* class [AutoModAction](./AutoModAction.md)
* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Enabled property

Whether the rule is enabled

```csharp
public bool Enabled { get; set; }
```

## See Also

* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ExemptRoles property

Role ids that should not be affected by the rule (Maximum of 20)

```csharp
public List<Snowflake> ExemptRoles { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ExemptChannels property

Channel ids that should not be affected by the rule (Maximum of 50)

```csharp
public List<Snowflake> ExemptChannels { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [AutoModRule](./AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](./AutoModNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
