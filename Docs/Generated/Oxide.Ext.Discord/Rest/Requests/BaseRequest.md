# BaseRequest class

Represents a base request class for REST API calls

```csharp
public abstract class BaseRequest : BasePoolable, IDebugLoggable
```

## Public Members

| name | description |
| --- | --- |
| [Data](#Data-field) | Data to be sent with the request |
| [Id](#Id-field) | ID of the request. Generated from the DateTimeOffset when the request was created |
| [Method](#Method-field) | HTTP request method |
| [Route](#Route-field) | Route on the API |
| [Status](#Status-field) | Current status of the request |
| [LogDebug](#LogDebug-method)(…) |  |

## Protected Members

| name | description |
| --- | --- |
| [BaseRequest](#BaseRequest-constructor)() | The default constructor. |
| [Logger](#Logger-field) | Logger for the request |
| override [EnterPool](#EnterPool-method)() |  |
| [Init](#Init-method)(…) | Initializes the request |
| override [LeavePool](#LeavePool-method)() |  |
| abstract [OnRequestError](#OnRequestError-method)(…) | Callback for API calls that error |
| abstract [OnRequestSuccess](#OnRequestSuccess-method)(…) | Callback for successful API Calls |

## See Also

* class [BasePoolable](../../Pooling/BasePoolable.md)
* interface [IDebugLoggable](../../Interfaces/Logging/IDebugLoggable.md)
* namespace [Oxide.Ext.Discord.Rest.Requests](./RequestsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [BaseRequest.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Rest/Requests/BaseRequest.cs)
   
   
# Init method

Initializes the request

```csharp
protected void Init(DiscordClient client, HttpClient httpClient, RequestMethod method, 
    string route, object data)
```

## See Also

* class [DiscordClient](../../Clients/DiscordClient.md)
* class [BaseRequest](./BaseRequest.md)
* namespace [Oxide.Ext.Discord.Rest.Requests](./RequestsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# OnRequestSuccess method

Callback for successful API Calls

```csharp
protected abstract void OnRequestSuccess(RequestResponse response)
```

| parameter | description |
| --- | --- |
| response | Response for the API Call |

## See Also

* class [RequestResponse](../../Entities/Api/RequestResponse.md)
* class [BaseRequest](./BaseRequest.md)
* namespace [Oxide.Ext.Discord.Rest.Requests](./RequestsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# OnRequestError method

Callback for API calls that error

```csharp
protected abstract void OnRequestError(RequestResponse response)
```

| parameter | description |
| --- | --- |
| response | Response for the error |

## See Also

* class [RequestResponse](../../Entities/Api/RequestResponse.md)
* class [BaseRequest](./BaseRequest.md)
* namespace [Oxide.Ext.Discord.Rest.Requests](./RequestsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# EnterPool method

```csharp
protected override void EnterPool()
```

## See Also

* class [BaseRequest](./BaseRequest.md)
* namespace [Oxide.Ext.Discord.Rest.Requests](./RequestsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# LeavePool method

```csharp
protected override void LeavePool()
```

## See Also

* class [BaseRequest](./BaseRequest.md)
* namespace [Oxide.Ext.Discord.Rest.Requests](./RequestsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# LogDebug method

```csharp
public void LogDebug(DebugLogger logger)
```

## See Also

* class [DebugLogger](../../Logging/DebugLogger.md)
* class [BaseRequest](./BaseRequest.md)
* namespace [Oxide.Ext.Discord.Rest.Requests](./RequestsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# BaseRequest constructor

The default constructor.

```csharp
protected BaseRequest()
```

## See Also

* class [BaseRequest](./BaseRequest.md)
* namespace [Oxide.Ext.Discord.Rest.Requests](./RequestsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Id field

ID of the request. Generated from the DateTimeOffset when the request was created

```csharp
public Snowflake Id;
```

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* class [BaseRequest](./BaseRequest.md)
* namespace [Oxide.Ext.Discord.Rest.Requests](./RequestsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Method field

HTTP request method

```csharp
public RequestMethod Method;
```

## See Also

* class [BaseRequest](./BaseRequest.md)
* namespace [Oxide.Ext.Discord.Rest.Requests](./RequestsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Status field

Current status of the request

```csharp
public RequestStatus Status;
```

## See Also

* enum [RequestStatus](./RequestStatus.md)
* class [BaseRequest](./BaseRequest.md)
* namespace [Oxide.Ext.Discord.Rest.Requests](./RequestsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Route field

Route on the API

```csharp
public string Route;
```

## See Also

* class [BaseRequest](./BaseRequest.md)
* namespace [Oxide.Ext.Discord.Rest.Requests](./RequestsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Data field

Data to be sent with the request

```csharp
public object Data;
```

## See Also

* class [BaseRequest](./BaseRequest.md)
* namespace [Oxide.Ext.Discord.Rest.Requests](./RequestsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Logger field

Logger for the request

```csharp
protected ILogger Logger;
```

## See Also

* interface [ILogger](../../Logging/ILogger.md)
* class [BaseRequest](./BaseRequest.md)
* namespace [Oxide.Ext.Discord.Rest.Requests](./RequestsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
