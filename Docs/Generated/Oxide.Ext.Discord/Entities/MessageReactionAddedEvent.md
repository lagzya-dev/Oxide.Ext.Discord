# MessageReactionAddedEvent class

Represents [Message Reaction Add](https://discord.com/developers/docs/topics/gateway#message-reaction-add)

```csharp
public class MessageReactionAddedEvent
```

## Public Members

| name | description |
| --- | --- |
| [MessageReactionAddedEvent](#messagereactionaddedevent-constructor)() | The default constructor. |
| [Burst](#burst-property) { get; set; } | True if this is a super-reaction |
| [BurstColors](#burstcolors-property) { get; set; } | Colors used for super-reaction animation |
| [ChannelId](#channelid-property) { get; set; } | The id of the channel |
| [Emoji](#emoji-property) { get; set; } | The emoji used to react |
| [GuildId](#guildid-property) { get; set; } | The id of the guild |
| [Member](#member-property) { get; set; } | The member who reacted if this happened in a guild |
| [MessageAuthorId](#messageauthorid-property) { get; set; } | ID of the user who authored the message which was reacted to |
| [MessageId](#messageid-property) { get; set; } | The id of the message |
| [Type](#type-property) { get; set; } | The type of the reaction |
| [UserId](#userid-property) { get; set; } | The id of the user |

## See Also

* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [MessageReactionAddedEvent.cs](../../../../Oxide.Ext.Discord/Entities/MessageReactionAddedEvent.cs)
   
   
# MessageReactionAddedEvent constructor

The default constructor.

```csharp
public MessageReactionAddedEvent()
```

## See Also

* class [MessageReactionAddedEvent](./MessageReactionAddedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# UserId property

The id of the user

```csharp
public Snowflake UserId { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [MessageReactionAddedEvent](./MessageReactionAddedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ChannelId property

The id of the channel

```csharp
public Snowflake ChannelId { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [MessageReactionAddedEvent](./MessageReactionAddedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# MessageId property

The id of the message

```csharp
public Snowflake MessageId { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [MessageReactionAddedEvent](./MessageReactionAddedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GuildId property

The id of the guild

```csharp
public Snowflake? GuildId { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [MessageReactionAddedEvent](./MessageReactionAddedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Member property

The member who reacted if this happened in a guild

```csharp
public GuildMember Member { get; set; }
```

## See Also

* class [GuildMember](./GuildMember.md)
* class [MessageReactionAddedEvent](./MessageReactionAddedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Emoji property

The emoji used to react

```csharp
public DiscordEmoji Emoji { get; set; }
```

## See Also

* class [DiscordEmoji](./DiscordEmoji.md)
* class [MessageReactionAddedEvent](./MessageReactionAddedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# MessageAuthorId property

ID of the user who authored the message which was reacted to

```csharp
public Snowflake? MessageAuthorId { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [MessageReactionAddedEvent](./MessageReactionAddedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Burst property

True if this is a super-reaction

```csharp
public bool Burst { get; set; }
```

## See Also

* class [MessageReactionAddedEvent](./MessageReactionAddedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# BurstColors property

Colors used for super-reaction animation

```csharp
public List<DiscordColor> BurstColors { get; set; }
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* class [MessageReactionAddedEvent](./MessageReactionAddedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Type property

The type of the reaction

```csharp
public ReactionType Type { get; set; }
```

## See Also

* enum [ReactionType](./ReactionType.md)
* class [MessageReactionAddedEvent](./MessageReactionAddedEvent.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
