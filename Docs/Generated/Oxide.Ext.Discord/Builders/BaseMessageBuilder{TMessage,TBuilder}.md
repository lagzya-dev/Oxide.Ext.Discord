# BaseMessageBuilder&lt;TMessage,TBuilder&gt; class

Represents a builder for [`BaseMessageCreate`](../Entities/BaseMessageCreate.md)

```csharp
public abstract class BaseMessageBuilder<TMessage, TBuilder>
    where TMessage : BaseMessageCreate
    where TBuilder : BaseMessageBuilder
```

| parameter | description |
| --- | --- |
| TMessage |  |
| TBuilder |  |

## Public Members

| name | description |
| --- | --- |
| virtual [AddActionRow](#addactionrow-method)(…) | Adds a single [`ActionRowComponent`](../Entities/ActionRowComponent.md) |
| virtual [AddAllowedMentions](#addallowedmentions-method)(…) | Adds [`AllowedMentions`](../Entities/AllowedMentions.md) to the response |
| virtual [AddAttachment](#addattachment-method)(…) | Adds an attachment to the message |
| virtual [AddComponents](#addcomponents-method-1-of-2)(…) | Adds a collection MessageComponents/&gt; (2 methods) |
| virtual [AddContent](#addcontent-method)(…) | Adds message text |
| virtual [AddEmbed](#addembed-method-1-of-2)(…) | Adds a [`DiscordEmbed`](../Entities/DiscordEmbed.md) (2 methods) |
| virtual [AddEmbeds](#addembeds-method)(…) | Adds a collection of [`DiscordEmbed`](../Entities/DiscordEmbed.md) to the response |
| virtual [AsTts](#astts-method)(…) | Marks the message As Text-To-Speech |
| [Build](#build-method)() | Returns the built message |
| virtual [SuppressEmbeds](#suppressembeds-method)() | Suppresses embeds on this response |

## Protected Members

| name | description |
| --- | --- |
| [BaseMessageBuilder](#basemessagebuilder&amp;lt;tmessage,tbuilder&amp;gt;-constructor)(…) | Constructor |
| readonly [Builder](#builder-field) | This builder |
| readonly [Message](#message-field) | Message the builder is for |

## See Also

* class [BaseMessageCreate](../Entities/BaseMessageCreate.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [BaseMessageBuilder.cs](../../../../Oxide.Ext.Discord/Builders/BaseMessageBuilder.cs)
   
   
# AddContent method

Adds message text

```csharp
public virtual TBuilder AddContent(string content)
```

| parameter | description |
| --- | --- |
| content | Text to be added |

## Return Value

This

## See Also

* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AsTts method

Marks the message As Text-To-Speech

```csharp
public virtual TBuilder AsTts(bool enabled = true)
```

| parameter | description |
| --- | --- |
| enabled | Should TTS be enabled (Default true) |

## Return Value

this

## See Also

* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddEmbed method (1 of 2)

Adds a [`DiscordEmbed`](../Entities/DiscordEmbed.md)

```csharp
public virtual TBuilder AddEmbed(DiscordEmbed embed)
```

| parameter | description |
| --- | --- |
| embed | Embed to be added |

## Return Value

This

## See Also

* class [DiscordEmbed](../Entities/DiscordEmbed.md)
* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# AddEmbed method (2 of 2)

Adds [`DiscordEmbed`](../Entities/DiscordEmbed.md) created from a [`DiscordEmbedBuilder`](./DiscordEmbedBuilder.md)

```csharp
public virtual TBuilder AddEmbed(DiscordEmbedBuilder builder)
```

| parameter | description |
| --- | --- |
| builder | Build to add embeds from |

## Return Value

This

## See Also

* class [DiscordEmbedBuilder](./DiscordEmbedBuilder.md)
* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddEmbeds method

Adds a collection of [`DiscordEmbed`](../Entities/DiscordEmbed.md) to the response

```csharp
public virtual TBuilder AddEmbeds(ICollection<DiscordEmbed> embeds)
```

| parameter | description |
| --- | --- |
| embeds | Embeds to be added |

## Return Value

This

## See Also

* class [DiscordEmbed](../Entities/DiscordEmbed.md)
* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddAllowedMentions method

Adds [`AllowedMentions`](../Entities/AllowedMentions.md) to the response

```csharp
public virtual TBuilder AddAllowedMentions(AllowedMentions mentions)
```

| parameter | description |
| --- | --- |
| mentions | Mentions to be added |

## Return Value

This

## See Also

* class [AllowedMentions](../Entities/AllowedMentions.md)
* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# SuppressEmbeds method

Suppresses embeds on this response

```csharp
public virtual TBuilder SuppressEmbeds()
```

## Return Value

This

## See Also

* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddActionRow method

Adds a single [`ActionRowComponent`](../Entities/ActionRowComponent.md)

```csharp
public virtual TBuilder AddActionRow(ActionRowComponent component)
```

| parameter | description |
| --- | --- |
| component | Component to be added |

## Return Value

This

## See Also

* class [ActionRowComponent](../Entities/ActionRowComponent.md)
* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddComponents method (1 of 2)

Adds a collection MessageComponents/&gt;

```csharp
public virtual TBuilder AddComponents(ICollection<ActionRowComponent> components)
```

| parameter | description |
| --- | --- |
| components | Components to be added |

## Return Value

This

## See Also

* class [ActionRowComponent](../Entities/ActionRowComponent.md)
* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# AddComponents method (2 of 2)

Adds MessageComponents from [`MessageComponentBuilder`](./MessageComponentBuilder.md)

```csharp
public virtual TBuilder AddComponents(MessageComponentBuilder builder)
```

| parameter | description |
| --- | --- |
| builder | Build to add components from |

## Return Value

This

## See Also

* class [MessageComponentBuilder](./MessageComponentBuilder.md)
* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddAttachment method

Adds an attachment to the message

```csharp
public virtual TBuilder AddAttachment(string filename, byte[] data, string contentType, 
    string description = null, string title = null)
```

| parameter | description |
| --- | --- |
| filename | Name of the file |
| data | byte[] of the attachment |
| contentType | Attachment content type |
| description | Description for the attachment |
| title | Title of the attachment |

## See Also

* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Build method

Returns the built message

```csharp
public TMessage Build()
```

## See Also

* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# BaseMessageBuilder&lt;TMessage,TBuilder&gt; constructor

Constructor

```csharp
protected BaseMessageBuilder(TMessage message)
```

| parameter | description |
| --- | --- |
| message | Message being created |

## See Also

* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Message field

Message the builder is for

```csharp
protected readonly TMessage Message;
```

## See Also

* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Builder field

This builder

```csharp
protected readonly TBuilder Builder;
```

## See Also

* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
