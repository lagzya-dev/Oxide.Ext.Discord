# MessageAttachment class

Represents a message [Attachment Structure](https://discord.com/developers/docs/resources/channel#attachment-object)

```csharp
public class MessageAttachment : ISnowflakeEntity
```

## Public Members

| name | description |
| --- | --- |
| [MessageAttachment](#messageattachment-constructor)() | The default constructor. |
| [ContentType](#contenttype-property) { get; set; } | The attachment's [media type](https://en.wikipedia.org/wiki/Media_type) |
| [Description](#description-property) { get; set; } | Description for the file |
| [DurationSecs](#durationsecs-property) { get; set; } | The duration of the audio file (currently for voice messages) |
| [Ephemeral](#ephemeral-property) { get; set; } | Whether this attachment is ephemeral |
| [Filename](#filename-property) { get; set; } | Name of file attached |
| [Flags](#flags-property) { get; set; } | Attachment Flags |
| [Height](#height-property) { get; set; } | Height of file (if image) |
| [Id](#id-property) { get; set; } | Attachment ID |
| [ProxyUrl](#proxyurl-property) { get; set; } | A proxied url of file |
| [Size](#size-property) { get; set; } | Size of file in bytes |
| [Title](#title-property) { get; set; } | Title of the file |
| [Url](#url-property) { get; set; } | Source url of file |
| [Waveform](#waveform-property) { get; set; } | base64 encoded bytearray representing a sampled waveform (currently for voice messages) |
| [Width](#width-property) { get; set; } | Width of file (if image) |

## See Also

* interface [ISnowflakeEntity](../Interfaces/ISnowflakeEntity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [MessageAttachment.cs](../../../../Oxide.Ext.Discord/Entities/MessageAttachment.cs)
   
   
# MessageAttachment constructor

The default constructor.

```csharp
public MessageAttachment()
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Id property

Attachment ID

```csharp
public Snowflake Id { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [MessageAttachment](./MessageAttachment.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Filename property

Name of file attached

```csharp
public string Filename { get; set; }
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Title property

Title of the file

```csharp
public string Title { get; set; }
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Description property

Description for the file

```csharp
public string Description { get; set; }
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ContentType property

The attachment's [media type](https://en.wikipedia.org/wiki/Media_type)

```csharp
public string ContentType { get; set; }
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Size property

Size of file in bytes

```csharp
public int? Size { get; set; }
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Url property

Source url of file

```csharp
public string Url { get; set; }
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ProxyUrl property

A proxied url of file

```csharp
public string ProxyUrl { get; set; }
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Height property

Height of file (if image)

```csharp
public int? Height { get; set; }
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Width property

Width of file (if image)

```csharp
public int? Width { get; set; }
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Ephemeral property

Whether this attachment is ephemeral

```csharp
public bool? Ephemeral { get; set; }
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DurationSecs property

The duration of the audio file (currently for voice messages)

```csharp
public float? DurationSecs { get; set; }
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Waveform property

base64 encoded bytearray representing a sampled waveform (currently for voice messages)

```csharp
public string Waveform { get; set; }
```

## See Also

* class [MessageAttachment](./MessageAttachment.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Flags property

Attachment Flags

```csharp
public AttachmentFlags? Flags { get; set; }
```

## See Also

* enum [AttachmentFlags](./AttachmentFlags.md)
* class [MessageAttachment](./MessageAttachment.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
