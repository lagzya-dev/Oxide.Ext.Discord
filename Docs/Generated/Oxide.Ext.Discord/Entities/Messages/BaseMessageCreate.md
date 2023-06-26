# BaseMessageCreate class

Represents a base message in discord

```csharp
public abstract class BaseMessageCreate : IDiscordMessageTemplate, IFileAttachments
```

## Public Members

| name | description |
| --- | --- |
| [AllowedMention](#AllowedMention-property) { get; set; } | Allowed mentions for a message Allows for more granular control over mentions without various hacks to the message content. |
| [Attachments](#Attachments-property) { get; set; } | Attachments for the message |
| [Components](#Components-property) { get; set; } | Used to create message components on a message |
| [Content](#Content-property) { get; set; } | Contents of the message |
| [Embeds](#Embeds-property) { get; set; } | Embeds for the message Embeds are deduplicated by URL. If a message contains multiple embeds with the same URL, only the first is shown. |
| [FileAttachments](#FileAttachments-property) { get; set; } | Attachments for a discord message |
| [Flags](#Flags-property) { get; set; } | Attachments for the message |
| [StickerIds](#StickerIds-property) { get; set; } | IDs of up to 3 stickers in the server to send in the message |
| [Tts](#Tts-property) { get; set; } | Whether this was a TTS message |
| [AddAttachment](#AddAttachment-method)(…) | Adds an attachment to the message |
| [Validate](#Validate-method)() |  |

## Protected Members

| name | description |
| --- | --- |
| [BaseMessageCreate](#BaseMessageCreate-constructor)() | The default constructor. |
| virtual [ValidateFlags](#ValidateFlags-method)() | Validates that the message flags are correct for the message type |
| virtual [ValidateRequiredFields](#ValidateRequiredFields-method)() | Validates required fields for the message |

## See Also

* interface [IDiscordMessageTemplate](../../Interfaces/Entities/Messages/IDiscordMessageTemplate.md)
* interface [IFileAttachments](../../Interfaces/IFileAttachments.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [BaseMessageCreate.cs](../../../../Oxide.Ext.Discord/Entities/Messages/BaseMessageCreate.cs)
   
   
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

* class [BaseMessageCreate](./BaseMessageCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Validate method

```csharp
public void Validate()
```

## See Also

* class [BaseMessageCreate](./BaseMessageCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ValidateRequiredFields method

Validates required fields for the message

```csharp
protected virtual void ValidateRequiredFields()
```

## See Also

* class [BaseMessageCreate](./BaseMessageCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ValidateFlags method

Validates that the message flags are correct for the message type

```csharp
protected virtual void ValidateFlags()
```

## Exceptions

| exception | condition |
| --- | --- |
| [InvalidMessageException](../../Exceptions/Entities/Messages/InvalidMessageException.md) |  |

## See Also

* class [BaseMessageCreate](./BaseMessageCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# BaseMessageCreate constructor

The default constructor.

```csharp
protected BaseMessageCreate()
```

## See Also

* class [BaseMessageCreate](./BaseMessageCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Content property

Contents of the message

```csharp
public string Content { get; set; }
```

## See Also

* class [BaseMessageCreate](./BaseMessageCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Tts property

Whether this was a TTS message

```csharp
public bool? Tts { get; set; }
```

## See Also

* class [BaseMessageCreate](./BaseMessageCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Embeds property

Embeds for the message Embeds are deduplicated by URL. If a message contains multiple embeds with the same URL, only the first is shown.

```csharp
public List<DiscordEmbed> Embeds { get; set; }
```

## See Also

* class [DiscordEmbed](./Embeds/DiscordEmbed.md)
* class [BaseMessageCreate](./BaseMessageCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AllowedMention property

Allowed mentions for a message Allows for more granular control over mentions without various hacks to the message content.

```csharp
public AllowedMention AllowedMention { get; set; }
```

## See Also

* class [AllowedMention](./AllowedMentions/AllowedMention.md)
* class [BaseMessageCreate](./BaseMessageCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Components property

Used to create message components on a message

```csharp
public List<ActionRowComponent> Components { get; set; }
```

## See Also

* class [ActionRowComponent](../Interactions/MessageComponents/ActionRowComponent.md)
* class [BaseMessageCreate](./BaseMessageCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# StickerIds property

IDs of up to 3 stickers in the server to send in the message

```csharp
public List<Snowflake> StickerIds { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [BaseMessageCreate](./BaseMessageCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Flags property

Attachments for the message

```csharp
public MessageFlags? Flags { get; set; }
```

## See Also

* enum [MessageFlags](./MessageFlags.md)
* class [BaseMessageCreate](./BaseMessageCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# FileAttachments property

Attachments for a discord message

```csharp
public List<MessageFileAttachment> FileAttachments { get; set; }
```

## See Also

* class [MessageFileAttachment](./MessageFileAttachment.md)
* class [BaseMessageCreate](./BaseMessageCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Attachments property

Attachments for the message

```csharp
public List<MessageAttachment> Attachments { get; set; }
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* class [BaseMessageCreate](./BaseMessageCreate.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](./MessagesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
