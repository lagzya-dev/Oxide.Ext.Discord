# BaseChannelMessageBuilder&lt;TMessage,TBuilder&gt; class

Represents a builder for [`MessageCreate`](../Entities/MessageCreate.md)

```csharp
public abstract class BaseChannelMessageBuilder<TMessage, TBuilder> : 
    BaseMessageBuilder<TMessage, TBuilder>
    where TMessage : MessageCreate
    where TBuilder : BaseChannelMessageBuilder
```

| parameter | description |
| --- | --- |
| TMessage | Type of the message |
| TBuilder | Type of the builder |

## Public Members

| name | description |
| --- | --- |
| [AddMessageReference](#addmessagereference-method)(…) | Adds a [`MessageReference`](../Entities/MessageReference.md) to the message |
| [AddReply](#addreply-method-1-of-2)(…) | Adds a [`AddMessageReference`](#addmessagereference-method) to the message (2 methods) |
| [AddSticker](#addsticker-method-1-of-2)(…) | Adds a sticker to the message (2 methods) |
| [AddStickers](#addstickers-method-1-of-2)(…) | Adds stickers to the message (2 methods) |
| [SuppressNotifications](#suppressnotifications-method)(…) | Adds a sticker to the message |

## Protected Members

| name | description |
| --- | --- |
| [BaseChannelMessageBuilder](#basechannelmessagebuilder&amp;lt;tmessage,tbuilder&amp;gt;-constructor)(…) | Constructor |

## See Also

* class [BaseMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseMessageBuilder%7BTMessage,TBuilder%7D.md)
* class [MessageCreate](../Entities/MessageCreate.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [BaseChannelMessageBuilder.cs](../../../../Oxide.Ext.Discord/Builders/BaseChannelMessageBuilder.cs)
   
   
# AddSticker method (1 of 2)

Adds a sticker to the message

```csharp
public TBuilder AddSticker(DiscordSticker sticker)
```

| parameter | description |
| --- | --- |
| sticker | Sticker to be added |

## Return Value

This

## See Also

* class [DiscordSticker](../Entities/DiscordSticker.md)
* class [BaseChannelMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseChannelMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# AddSticker method (2 of 2)

Adds a sticker to the message

```csharp
public TBuilder AddSticker(Snowflake stickerId)
```

| parameter | description |
| --- | --- |
| stickerId | Sticker ID to be added |

## Return Value

This

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* class [BaseChannelMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseChannelMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddStickers method (1 of 2)

Adds stickers to the message

```csharp
public TBuilder AddStickers(ICollection<DiscordSticker> stickerIds)
```

| parameter | description |
| --- | --- |
| stickerIds | Sticker ID's to be added |

## Return Value

This

## See Also

* class [DiscordSticker](../Entities/DiscordSticker.md)
* class [BaseChannelMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseChannelMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# AddStickers method (2 of 2)

Adds stickers to the message

```csharp
public TBuilder AddStickers(ICollection<Snowflake> stickerIds)
```

| parameter | description |
| --- | --- |
| stickerIds | Sticker ID's to be added |

## Return Value

This

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* class [BaseChannelMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseChannelMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddMessageReference method

Adds a [`MessageReference`](../Entities/MessageReference.md) to the message

```csharp
public TBuilder AddMessageReference(MessageReference reference)
```

| parameter | description |
| --- | --- |
| reference | Message Reference to be added |

## Return Value

This

## See Also

* class [MessageReference](../Entities/MessageReference.md)
* class [BaseChannelMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseChannelMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddReply method (1 of 2)

Adds a [`AddMessageReference`](#addmessagereference-method) to the message

```csharp
public TBuilder AddReply(DiscordMessage message, bool failIfNotExists = true)
```

| parameter | description |
| --- | --- |
| message | Message to reply to |
| failIfNotExists | Should the API call error if the message does not exist (Default true) |

## Return Value

This

## See Also

* class [DiscordMessage](../Entities/DiscordMessage.md)
* class [BaseChannelMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseChannelMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# AddReply method (2 of 2)

Adds a [`AddMessageReference`](#addmessagereference-method) to the message

```csharp
public TBuilder AddReply(Snowflake messageId, Snowflake? guildId = default, 
    bool failIfNotExists = true)
```

| parameter | description |
| --- | --- |
| messageId | ID of the message to reply to |
| guildId | Guild ID of the message if one exists |
| failIfNotExists | Should the API call error if the message does not exist (Default true) |

## Return Value

This

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* class [BaseChannelMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseChannelMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# SuppressNotifications method

Adds a sticker to the message

```csharp
public TBuilder SuppressNotifications(DiscordSticker sticker)
```

| parameter | description |
| --- | --- |
| sticker | Sticker to be added |

## Return Value

This

## See Also

* class [DiscordSticker](../Entities/DiscordSticker.md)
* class [BaseChannelMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseChannelMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# BaseChannelMessageBuilder&lt;TMessage,TBuilder&gt; constructor

Constructor

```csharp
protected BaseChannelMessageBuilder(TMessage message)
```

| parameter | description |
| --- | --- |
| message | Message being created |

## See Also

* class [BaseChannelMessageBuilder&lt;TMessage,TBuilder&gt;](./BaseChannelMessageBuilder%7BTMessage,TBuilder%7D.md)
* namespace [Oxide.Ext.Discord.Builders](./BuildersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
