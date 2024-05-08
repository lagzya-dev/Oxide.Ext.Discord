# ResponseError class

Error object that is returned to the caller when a request fails

```csharp
public class ResponseError : BaseDiscordException
```

## Public Members

| name | description |
| --- | --- |
| [ContentType](#contenttype-property) { get; } | HTTP Content Type for the request |
| [DiscordError](#discorderror-property) { get; } | If discord returned an error this will contain that error message |
| [Exception](#exception-property) { get; } | The exception from the request |
| [HttpStatusCode](#httpstatuscode-property) { get; } | HTTP Status code |
| [RateLimitCode](#ratelimitcode-property) { get; } | If error was a rate limit the code from the rate limit |
| [RateLimitMessage](#ratelimitmessage-property) { get; } | If error was a rate limit the message from the rate limit |
| [ResponseMessage](#responsemessage-property) { get; } | Full string response if we received one |
| [StringContents](#stringcontents-property) { get; } | The string contents of the request |
| readonly [ErrorDate](#errordate-field) | DateTimeOffset when the error occured |
| readonly [RequestData](#requestdata-field) | What data was passed to the request |
| readonly [RequestId](#requestid-field) | ID of the request |
| readonly [RequestMethod](#requestmethod-field) | The request method that was called |
| readonly [Url](#url-field) | The URI that was called |
| [SuppressErrorMessage](#suppresserrormessage-method)() | Suppresses the error message from being logged |

## See Also

* class [BaseDiscordException](../Exceptions/BaseDiscordException.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [ResponseError.cs](../../../../Oxide.Ext.Discord/Entities/ResponseError.cs)
   
   
# SuppressErrorMessage method

Suppresses the error message from being logged

```csharp
public void SuppressErrorMessage()
```

## See Also

* class [ResponseError](./ResponseError.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Exception property

The exception from the request

```csharp
public Exception Exception { get; }
```

## See Also

* class [ResponseError](./ResponseError.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ContentType property

HTTP Content Type for the request

```csharp
public string ContentType { get; }
```

## See Also

* class [ResponseError](./ResponseError.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# StringContents property

The string contents of the request

```csharp
public string StringContents { get; }
```

## See Also

* class [ResponseError](./ResponseError.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# HttpStatusCode property

HTTP Status code

```csharp
public DiscordHttpStatusCode HttpStatusCode { get; }
```

## See Also

* enum [DiscordHttpStatusCode](./DiscordHttpStatusCode.md)
* class [ResponseError](./ResponseError.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DiscordError property

If discord returned an error this will contain that error message

```csharp
public ResponseErrorMessage DiscordError { get; }
```

## See Also

* class [ResponseErrorMessage](./ResponseErrorMessage.md)
* class [ResponseError](./ResponseError.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ResponseMessage property

Full string response if we received one

```csharp
public string ResponseMessage { get; }
```

## See Also

* class [ResponseError](./ResponseError.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# RateLimitMessage property

If error was a rate limit the message from the rate limit

```csharp
public string RateLimitMessage { get; }
```

## See Also

* class [ResponseError](./ResponseError.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# RateLimitCode property

If error was a rate limit the code from the rate limit

```csharp
public int? RateLimitCode { get; }
```

## See Also

* class [ResponseError](./ResponseError.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# RequestId field

ID of the request

```csharp
public readonly Snowflake RequestId;
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [ResponseError](./ResponseError.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# RequestMethod field

The request method that was called

```csharp
public readonly RequestMethod RequestMethod;
```

## See Also

* class [ResponseError](./ResponseError.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Url field

The URI that was called

```csharp
public readonly string Url;
```

## See Also

* class [ResponseError](./ResponseError.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# RequestData field

What data was passed to the request

```csharp
public readonly object RequestData;
```

## See Also

* class [ResponseError](./ResponseError.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ErrorDate field

DateTimeOffset when the error occured

```csharp
public readonly DateTimeOffset ErrorDate;
```

## See Also

* class [ResponseError](./ResponseError.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
