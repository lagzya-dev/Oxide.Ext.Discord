# ThreadChannelUpdate class

Represents a [Thread Channel Update Structure](https://discord.com/developers/docs/resources/channel#modify-channel-json-params-thread)

```csharp
public class ThreadChannelUpdate
```

## Public Members

| name | description |
| --- | --- |
| [ThreadChannelUpdate](#ThreadChannelUpdate)() | The default constructor. |
| [AppliedTags](#AppliedTags) { get; set; } | The IDs of the set of tags that have been applied to a thread in a GUILD_FORUM channel |
| [Archived](#Archived) { get; set; } | Whether the channel is archived |
| [AutoArchiveDuration](#AutoArchiveDuration) { get; set; } | Duration in minutes to automatically archive the thread after recent activity Can be set to: 60, 1440, 4320, 10080 |
| [Flags](#Flags) { get; set; } | Channel flags combined as a bitfield; PINNED can only be set for threads in forum channels |
| [Invitable](#Invitable) { get; set; } | Whether non-moderators can add other non-moderators to a thread Only available on private threads |
| [Locked](#Locked) { get; set; } | Whether the thread is locked When a thread is locked, only users with MANAGE_THREADS can unarchive it |
| [Name](#Name) { get; set; } | The name of the channel (1-100 characters) |
| [RateLimitPerUser](#RateLimitPerUser) { get; set; } | Amount of seconds a user has to wait before sending another message (0-21600) Bots and users with the permission manage_messages, manage_thread, or manage_channel, are unaffected |
| [Validate](#Validate)() |  |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [ThreadChannelUpdate.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Channels/Threads/ThreadChannelUpdate.cs)
   
   
# Validate method

```csharp
public void Validate()
```

## See Also

* class [ThreadChannelUpdate](./ThreadChannelUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ThreadChannelUpdate constructor

The default constructor.

```csharp
public ThreadChannelUpdate()
```

## See Also

* class [ThreadChannelUpdate](./ThreadChannelUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Name property

The name of the channel (1-100 characters)

```csharp
public string Name { get; set; }
```

## See Also

* class [ThreadChannelUpdate](./ThreadChannelUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Archived property

Whether the channel is archived

```csharp
public bool Archived { get; set; }
```

## See Also

* class [ThreadChannelUpdate](./ThreadChannelUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# AutoArchiveDuration property

Duration in minutes to automatically archive the thread after recent activity Can be set to: 60, 1440, 4320, 10080

```csharp
public int AutoArchiveDuration { get; set; }
```

## See Also

* class [ThreadChannelUpdate](./ThreadChannelUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Locked property

Whether the thread is locked When a thread is locked, only users with MANAGE_THREADS can unarchive it

```csharp
public bool Locked { get; set; }
```

## See Also

* class [ThreadChannelUpdate](./ThreadChannelUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Invitable property

Whether non-moderators can add other non-moderators to a thread Only available on private threads

```csharp
public bool Invitable { get; set; }
```

## See Also

* class [ThreadChannelUpdate](./ThreadChannelUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# RateLimitPerUser property

Amount of seconds a user has to wait before sending another message (0-21600) Bots and users with the permission manage_messages, manage_thread, or manage_channel, are unaffected

```csharp
public int? RateLimitPerUser { get; set; }
```

## See Also

* class [ThreadChannelUpdate](./ThreadChannelUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Flags property

Channel flags combined as a bitfield; PINNED can only be set for threads in forum channels

```csharp
public ChannelFlags? Flags { get; set; }
```

## See Also

* enum [ChannelFlags](../ChannelFlags.md)
* class [ThreadChannelUpdate](./ThreadChannelUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# AppliedTags property

The IDs of the set of tags that have been applied to a thread in a GUILD_FORUM channel

```csharp
public List<Snowflake> AppliedTags { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [ThreadChannelUpdate](./ThreadChannelUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
