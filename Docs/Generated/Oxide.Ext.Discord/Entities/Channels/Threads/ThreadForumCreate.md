# ThreadForumCreate class

Represents a [Start Thread in Forum Channel](https://discord.com/developers/docs/resources/channel#start-thread-in-forum-channel-jsonform-params) Structure

```csharp
public class ThreadForumCreate : IFileAttachments
```

## Public Members

| name | description |
| --- | --- |
| [ThreadForumCreate](#threadforumcreate-constructor)() | The default constructor. |
| [AppliedTags](#appliedtags-property) { get; set; } | The IDs of the set of tags that have been applied to a thread in a GUILD_FORUM or GUILD_MEDIA channel |
| [Attachments](#attachments-property) { get; set; } | Attachments for the message |
| [AutoArchiveDuration](#autoarchiveduration-property) { get; set; } | Duration in minutes to automatically archive the thread after recent activity, can be set to: 60, 1440, 4320, 10080 |
| [FileAttachments](#fileattachments-property) { get; set; } | Attachments for a discord message |
| [Message](#message-property) { get; set; } | Contents of the first message in the forum thread |
| [Name](#name-property) { get; set; } | 1-100 character thread name |
| [RateLimitPerUser](#ratelimitperuser-property) { get; set; } | Amount of seconds a user has to wait before sending another message (0-21600) |
| [AddAttachment](#addattachment-method)(…) | Adds an attachment to the message |
| [Validate](#validate-method)() | Validates the Thread Forum Create |

## See Also

* interface [IFileAttachments](../../../Interfaces/IFileAttachments.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [ThreadForumCreate.cs](../../../../Oxide.Ext.Discord/Entities/Channels/Threads/ThreadForumCreate.cs)
   
   
# AddAttachment method

Adds an attachment to the message

```csharp
public void AddAttachment(string filename, byte[] data, string contentType, 
    string description = null)
```

| parameter | description |
| --- | --- |
| filename | Name of the file |
| data | byte[] of the attachment |
| contentType | Attachment content type |
| description | Description for the attachment |

## See Also

* class [ThreadForumCreate](./ThreadForumCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Validate method

Validates the Thread Forum Create

```csharp
public void Validate()
```

## See Also

* class [ThreadForumCreate](./ThreadForumCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ThreadForumCreate constructor

The default constructor.

```csharp
public ThreadForumCreate()
```

## See Also

* class [ThreadForumCreate](./ThreadForumCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Name property

1-100 character thread name

```csharp
public string Name { get; set; }
```

## See Also

* class [ThreadForumCreate](./ThreadForumCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# AutoArchiveDuration property

Duration in minutes to automatically archive the thread after recent activity, can be set to: 60, 1440, 4320, 10080

```csharp
public int? AutoArchiveDuration { get; set; }
```

## See Also

* class [ThreadForumCreate](./ThreadForumCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# RateLimitPerUser property

Amount of seconds a user has to wait before sending another message (0-21600)

```csharp
public int? RateLimitPerUser { get; set; }
```

## See Also

* class [ThreadForumCreate](./ThreadForumCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Message property

Contents of the first message in the forum thread

```csharp
public MessageCreate Message { get; set; }
```

## See Also

* class [MessageCreate](../../Messages/MessageCreate.md)
* class [ThreadForumCreate](./ThreadForumCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# AppliedTags property

The IDs of the set of tags that have been applied to a thread in a GUILD_FORUM or GUILD_MEDIA channel

```csharp
public List<Snowflake> AppliedTags { get; set; }
```

## See Also

* struct [Snowflake](../../Snowflake.md)
* class [ThreadForumCreate](./ThreadForumCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# FileAttachments property

Attachments for a discord message

```csharp
public List<MessageFileAttachment> FileAttachments { get; set; }
```

## See Also

* class [MessageFileAttachment](../../Messages/MessageFileAttachment.md)
* class [ThreadForumCreate](./ThreadForumCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Attachments property

Attachments for the message

```csharp
public List<MessageAttachment> Attachments { get; set; }
```

## See Also

* class [MessageAttachment](../../Messages/MessageAttachment.md)
* class [ThreadForumCreate](./ThreadForumCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Channels.Threads](./ThreadsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
