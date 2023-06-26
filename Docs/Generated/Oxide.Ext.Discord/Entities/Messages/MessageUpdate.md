# MessageUpdate class

Represents a [Message Update Structure](https://discord.com/developers/docs/resources/channel#edit-message-jsonform-params) sent in a channel within Discord..

```csharp
public class MessageUpdate : IDiscordMessageTemplate, IFileAttachments
```

## Public Members

| name | description |
| --- | --- |
| [MessageUpdate](#messageupdate-constructor)() | Constructor |
| [MessageUpdate](#messageupdate-constructor)(…) | Constructor for message to be edited Only sets the Attachments field |
| [AllowedMentions](#allowedmentions-property) { get; set; } | Allowed mentions for the message |
| [Attachments](#attachments-property) { get; set; } | Attachments for the message |
| [Components](#components-property) { get; set; } | Components to include with the message |
| [Content](#content-property) { get; set; } | Contents of the message up to 2000 characters |
| [Embeds](#embeds-property) { get; set; } | Up to 10 rich embeds (up to 6000 characters) |
| [FileAttachments](#fileattachments-property) { get; set; } | Attachments for a discord message |
| [Flags](#flags-property) { get; set; } | Edit the flags of a message (only SUPPRESS_EMBEDS can currently be set/unset) |
| [AddAttachment](#addattachment-method)(…) | Adds an attachment to the message |
| [Validate](#validate-method)() |  |

## See Also

* interface [IDiscordMessageTemplate](../../Interfaces/Entities/Messages/IDiscordMessageTemplate.md)
* interface [IFileAttachments](../../Interfaces/IFileAttachments.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [MessageUpdate.cs](../../../../Oxide.Ext.Discord/Entities/Messages/MessageUpdate.cs)
   
   
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

* class [MessageUpdate](./MessageUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Validate method

```csharp
public void Validate()
```

## See Also

* class [MessageUpdate](./MessageUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# MessageUpdate constructor (1 of 2)

Constructor

```csharp
public MessageUpdate()
```

## See Also

* class [MessageUpdate](./MessageUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# MessageUpdate constructor (2 of 2)

Constructor for message to be edited Only sets the Attachments field

```csharp
public MessageUpdate(DiscordMessage message)
```

| parameter | description |
| --- | --- |
| message |  |

## See Also

* class [DiscordMessage](./DiscordMessage.md)
* class [MessageUpdate](./MessageUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Content property

Contents of the message up to 2000 characters

```csharp
public string Content { get; set; }
```

## See Also

* class [MessageUpdate](./MessageUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Embeds property

Up to 10 rich embeds (up to 6000 characters)

```csharp
public List<DiscordEmbed> Embeds { get; set; }
```

## See Also

* class [DiscordEmbed](./Embeds/DiscordEmbed.md)
* class [MessageUpdate](./MessageUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Flags property

Edit the flags of a message (only SUPPRESS_EMBEDS can currently be set/unset)

```csharp
public MessageFlags? Flags { get; set; }
```

## See Also

* enum [MessageFlags](./MessageFlags.md)
* class [MessageUpdate](./MessageUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AllowedMentions property

Allowed mentions for the message

```csharp
public AllowedMention AllowedMentions { get; set; }
```

## See Also

* class [AllowedMention](./AllowedMentions/AllowedMention.md)
* class [MessageUpdate](./MessageUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Components property

Components to include with the message

```csharp
public List<ActionRowComponent> Components { get; set; }
```

## See Also

* class [ActionRowComponent](../Interactions/MessageComponents/ActionRowComponent.md)
* class [MessageUpdate](./MessageUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Attachments property

Attachments for the message

```csharp
public List<MessageAttachment> Attachments { get; set; }
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* class [MessageUpdate](./MessageUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# FileAttachments property

Attachments for a discord message

```csharp
public List<MessageFileAttachment> FileAttachments { get; set; }
```

## See Also

* class [MessageFileAttachment](./MessageFileAttachment.md)
* class [MessageUpdate](./MessageUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
