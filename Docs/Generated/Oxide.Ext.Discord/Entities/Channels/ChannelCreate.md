# ChannelCreate class

Represents a [Guild Channel Create Structure](https://discord.com/developers/docs/resources/guild#create-guild-channel-json-params)

```csharp
public class ChannelCreate
```

## Public Members

| name | description |
| --- | --- |
| [ChannelCreate](#ChannelCreate)() | The default constructor. |
| [AvailableTags](#AvailableTags) { get; set; } | Set of tags that can be used in a `GUILD_FORUM` channel |
| [Bitrate](#Bitrate) { get; set; } | The bitrate (in bits) of the voice channel 8000 to 96000 (128000 for VIP servers) |
| [DefaultAutoArchiveDuration](#DefaultAutoArchiveDuration) { get; set; } | The default duration that the clients use (not the API) for newly created threads in the channel, in minutes, to automatically archive the thread after recent activity |
| [DefaultForumLayout](#DefaultForumLayout) { get; set; } | The default [`ForumLayoutTypes`](./ForumLayoutTypes.md) used to display posts in GUILD_FORUM channels. Defaults to NotSet, which indicates a layout view has not been set by a channel admin |
| [DefaultReactionEmoji](#DefaultReactionEmoji) { get; set; } | Emoji to show in the add reaction button on a thread in a `GUILD_FORUM` channel |
| [DefaultSortOrder](#DefaultSortOrder) { get; set; } | The default [`SortOrderType`](./SortOrderType.md) used to order posts in `GUILD_FORUM` channels |
| [Name](#Name) { get; set; } | The name of the channel (1-100 characters) |
| [Nsfw](#Nsfw) { get; set; } | Whether the channel is nsfw |
| [ParentId](#ParentId) { get; set; } | ID of the parent category for a channel (each parent category can contain up to 50 channels) |
| [PermissionOverwrites](#PermissionOverwrites) { get; set; } | Explicit permission overwrites for members and roles [`Overwrite`](./Overwrite.md) |
| [Position](#Position) { get; set; } | Sorting position of the channel |
| [RateLimitPerUser](#RateLimitPerUser) { get; set; } | Amount of seconds a user has to wait before sending another message (0-21600); bots, as well as users with the permission manage_messages or manage_channel, are unaffected |
| [Topic](#Topic) { get; set; } | The channel topic (0-1024 characters) |
| [Type](#Type) { get; set; } | the type of channel [`ChannelType`](./ChannelType.md) |
| [UserLimit](#UserLimit) { get; set; } | The user limit of the voice channel 0 refers to no limit, 1 to 99 refers to a user limit |
| [Validate](#Validate)() |  |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [ChannelCreate.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Channels/ChannelCreate.cs)
   
   
# Validate method

```csharp
public void Validate()
```

## See Also

* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ChannelCreate constructor

The default constructor.

```csharp
public ChannelCreate()
```

## See Also

* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Name property

The name of the channel (1-100 characters)

```csharp
public string Name { get; set; }
```

## See Also

* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Type property

the type of channel [`ChannelType`](./ChannelType.md)

```csharp
public ChannelType Type { get; set; }
```

## See Also

* enum [ChannelType](./ChannelType.md)
* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Topic property

The channel topic (0-1024 characters)

```csharp
public string Topic { get; set; }
```

## See Also

* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Bitrate property

The bitrate (in bits) of the voice channel 8000 to 96000 (128000 for VIP servers)

```csharp
public int? Bitrate { get; set; }
```

## See Also

* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# UserLimit property

The user limit of the voice channel 0 refers to no limit, 1 to 99 refers to a user limit

```csharp
public int? UserLimit { get; set; }
```

## See Also

* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RateLimitPerUser property

Amount of seconds a user has to wait before sending another message (0-21600); bots, as well as users with the permission manage_messages or manage_channel, are unaffected

```csharp
public int? RateLimitPerUser { get; set; }
```

## See Also

* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Position property

Sorting position of the channel

```csharp
public int? Position { get; set; }
```

## See Also

* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# PermissionOverwrites property

Explicit permission overwrites for members and roles [`Overwrite`](./Overwrite.md)

```csharp
public List<Overwrite> PermissionOverwrites { get; set; }
```

## See Also

* class [Overwrite](./Overwrite.md)
* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ParentId property

ID of the parent category for a channel (each parent category can contain up to 50 channels)

```csharp
public Snowflake? ParentId { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Nsfw property

Whether the channel is nsfw

```csharp
public bool? Nsfw { get; set; }
```

## See Also

* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# DefaultAutoArchiveDuration property

The default duration that the clients use (not the API) for newly created threads in the channel, in minutes, to automatically archive the thread after recent activity

```csharp
public int DefaultAutoArchiveDuration { get; set; }
```

## See Also

* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# DefaultReactionEmoji property

Emoji to show in the add reaction button on a thread in a `GUILD_FORUM` channel

```csharp
public DefaultReaction DefaultReactionEmoji { get; set; }
```

## See Also

* class [DefaultReaction](../Emojis/DefaultReaction.md)
* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AvailableTags property

Set of tags that can be used in a `GUILD_FORUM` channel

```csharp
public List<ForumTag> AvailableTags { get; set; }
```

## See Also

* class [ForumTag](./ForumTag.md)
* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# DefaultSortOrder property

The default [`SortOrderType`](./SortOrderType.md) used to order posts in `GUILD_FORUM` channels

```csharp
public SortOrderType? DefaultSortOrder { get; set; }
```

## See Also

* enum [SortOrderType](./SortOrderType.md)
* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# DefaultForumLayout property

The default [`ForumLayoutTypes`](./ForumLayoutTypes.md) used to display posts in GUILD_FORUM channels. Defaults to NotSet, which indicates a layout view has not been set by a channel admin

```csharp
public ForumLayoutTypes? DefaultForumLayout { get; set; }
```

## See Also

* enum [ForumLayoutTypes](./ForumLayoutTypes.md)
* class [ChannelCreate](./ChannelCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
