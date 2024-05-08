# WebhookEditMessage class

Represents [Webhook Edit Message Structure](https://discord.com/developers/docs/resources/webhook#edit-webhook-message-jsonform-params)

```csharp
public class WebhookEditMessage : IDiscordMessageTemplate, IFileAttachments
```

## Public Members

| name | description |
| --- | --- |
| [WebhookEditMessage](#webhookeditmessage-constructor)() | The default constructor. |
| [AllowedMentions](#allowedmentions-property) { get; set; } | Allowed mentions for the message |
| [Attachments](#attachments-property) { get; set; } | Attachments for the message |
| [Components](#components-property) { get; set; } | Components to include with the message |
| [Content](#content-property) { get; set; } | The message contents (up to 2000 characters) |
| [Embeds](#embeds-property) { get; set; } | Embedded rich content (Up to 10 embeds) |
| [FileAttachments](#fileattachments-property) { get; set; } | Attachments for a discord message |
| [AddAttachment](#addattachment-method)(…) | Adds an attachment to the message |
| [AddEmbed](#addembed-method)(…) | Adds a new embed to the list of embed to send |
| [Validate](#validate-method)() |  |

## See Also

* interface [IDiscordMessageTemplate](../Interfaces/IDiscordMessageTemplate.md)
* interface [IFileAttachments](../Interfaces/IFileAttachments.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [WebhookEditMessage.cs](../../../../Oxide.Ext.Discord/Entities/WebhookEditMessage.cs)
   
   
# AddEmbed method

Adds a new embed to the list of embed to send

```csharp
public WebhookEditMessage AddEmbed(DiscordEmbed embed)
```

| parameter | description |
| --- | --- |
| embed | Embed to add |

## Return Value

This

## Exceptions

| exception | condition |
| --- | --- |
| IndexOutOfRangeException | Thrown if more than 10 embeds are added in a send as that is the discord limit |

## See Also

* class [DiscordEmbed](./DiscordEmbed.md)
* class [WebhookEditMessage](./WebhookEditMessage.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
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

* class [WebhookEditMessage](./WebhookEditMessage.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Validate method

```csharp
public void Validate()
```

## See Also

* class [WebhookEditMessage](./WebhookEditMessage.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# WebhookEditMessage constructor

The default constructor.

```csharp
public WebhookEditMessage()
```

## See Also

* class [WebhookEditMessage](./WebhookEditMessage.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Content property

The message contents (up to 2000 characters)

```csharp
public string Content { get; set; }
```

## See Also

* class [WebhookEditMessage](./WebhookEditMessage.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Embeds property

Embedded rich content (Up to 10 embeds)

```csharp
public List<DiscordEmbed> Embeds { get; set; }
```

## See Also

* class [DiscordEmbed](./DiscordEmbed.md)
* class [WebhookEditMessage](./WebhookEditMessage.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AllowedMentions property

Allowed mentions for the message

```csharp
public AllowedMentions AllowedMentions { get; set; }
```

## See Also

* class [AllowedMentions](./AllowedMentions.md)
* class [WebhookEditMessage](./WebhookEditMessage.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Components property

Components to include with the message

```csharp
public List<ActionRowComponent> Components { get; set; }
```

## See Also

* class [ActionRowComponent](./ActionRowComponent.md)
* class [WebhookEditMessage](./WebhookEditMessage.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Attachments property

Attachments for the message

```csharp
public List<MessageAttachment> Attachments { get; set; }
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* class [WebhookEditMessage](./WebhookEditMessage.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# FileAttachments property

Attachments for a discord message

```csharp
public List<MessageFileAttachment> FileAttachments { get; set; }
```

## See Also

* class [MessageFileAttachment](./MessageFileAttachment.md)
* class [WebhookEditMessage](./WebhookEditMessage.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
