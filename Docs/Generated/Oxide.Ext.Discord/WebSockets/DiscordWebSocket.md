# DiscordWebSocket class

Represents a websocket that connects to discord

```csharp
public class DiscordWebSocket : IDebugLoggable
```

## Public Members

| name | description |
| --- | --- |
| [DiscordWebSocket](#DiscordWebSocket)(…) | Socket used by the BotClient |
| [SocketHasConnected](#SocketHasConnected) { get; } | If the bot has successfully connected to the websocket at least once |
| [ShouldReconnect](#ShouldReconnect) | If we should attempt to reconnect to discord on disconnect |
| [ShouldResume](#ShouldResume) | If we should attempt to resume our previous session after connecting |
| [Connect](#Connect)() | Connect to the websocket |
| [Disconnect](#Disconnect)(…) | Disconnects the websocket from discord |
| [IsConnected](#IsConnected)() | Returns if the websocket is in the open state |
| [IsConnecting](#IsConnecting)() | Returns if the websocket is in the connecting state |
| [IsDisconnected](#IsDisconnected)() | Returns if the websocket is null or is currently closing / closed |
| [IsDisconnecting](#IsDisconnecting)() | Returns if the websocket is null or is currently closing / closed |
| [IsPendingReconnect](#IsPendingReconnect)() | Returns if the socket is waiting to reconnect |
| [LogDebug](#LogDebug)(…) |  |
| [ReconnectIfRequested](#ReconnectIfRequested)() | Reconnected to the websocket is a reconnect is requested and we are not shutting down |
| [Send](#Send)(…) | Send a command to discord over the websocket |
| [Shutdown](#Shutdown)() | Shutdowns the websocket completely. Used when bot is being shutdown |

## See Also

* interface [IDebugLoggable](../Interfaces/Logging/IDebugLoggable.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [DiscordWebSocket.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/WebSockets/DiscordWebSocket.cs)
   
   
# Connect method

Connect to the websocket

```csharp
public void Connect()
```

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if the socket still exists. Must call disconnect before trying to connect |

## See Also

* class [DiscordWebSocket](./DiscordWebSocket.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Disconnect method

Disconnects the websocket from discord

```csharp
public void Disconnect(bool reconnect, bool resume, bool requested = false)
```

| parameter | description |
| --- | --- |
| reconnect | Should we attempt to reconnect to discord after disconnecting |
| resume | Should we attempt to resume our previous session |
| requested | If discord requested that we reconnect to discord |

## See Also

* class [DiscordWebSocket](./DiscordWebSocket.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Shutdown method

Shutdowns the websocket completely. Used when bot is being shutdown

```csharp
public void Shutdown()
```

## See Also

* class [DiscordWebSocket](./DiscordWebSocket.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Send method

Send a command to discord over the websocket

```csharp
public void Send(DiscordClient client, GatewayCommandCode opCode, object data)
```

| parameter | description |
| --- | --- |
| client | Client sending the command |
| opCode | Command code to send |
| data | Data to send |

## See Also

* class [DiscordClient](../Clients/DiscordClient.md)
* enum [GatewayCommandCode](../Entities/Gateway/Commands/GatewayCommandCode.md)
* class [DiscordWebSocket](./DiscordWebSocket.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ReconnectIfRequested method

Reconnected to the websocket is a reconnect is requested and we are not shutting down

```csharp
public void ReconnectIfRequested()
```

## See Also

* class [DiscordWebSocket](./DiscordWebSocket.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IsConnecting method

Returns if the websocket is in the connecting state

```csharp
public bool IsConnecting()
```

## Return Value

Returns if the websocket is in connecting state

## See Also

* class [DiscordWebSocket](./DiscordWebSocket.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IsConnected method

Returns if the websocket is in the open state

```csharp
public bool IsConnected()
```

## Return Value

Returns if the websocket is in open state

## See Also

* class [DiscordWebSocket](./DiscordWebSocket.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IsPendingReconnect method

Returns if the socket is waiting to reconnect

```csharp
public bool IsPendingReconnect()
```

## Return Value

Returns if the socket is waiting to reconnect

## See Also

* class [DiscordWebSocket](./DiscordWebSocket.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IsDisconnecting method

Returns if the websocket is null or is currently closing / closed

```csharp
public bool IsDisconnecting()
```

## Return Value

Returns if the websocket is null or is currently closing / closed

## See Also

* class [DiscordWebSocket](./DiscordWebSocket.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IsDisconnected method

Returns if the websocket is null or is currently closing / closed

```csharp
public bool IsDisconnected()
```

## Return Value

Returns if the websocket is null or is currently closing / closed

## See Also

* class [DiscordWebSocket](./DiscordWebSocket.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# LogDebug method

```csharp
public void LogDebug(DebugLogger logger)
```

## See Also

* class [DebugLogger](../Logging/DebugLogger.md)
* class [DiscordWebSocket](./DiscordWebSocket.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DiscordWebSocket constructor

Socket used by the BotClient

```csharp
public DiscordWebSocket(BotClient client, ILogger logger)
```

| parameter | description |
| --- | --- |
| client | Client using the socket |
| logger | Logger for the bot client |

## See Also

* class [BotClient](../Clients/BotClient.md)
* interface [ILogger](../Logging/ILogger.md)
* class [DiscordWebSocket](./DiscordWebSocket.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# SocketHasConnected property

If the bot has successfully connected to the websocket at least once

```csharp
public bool SocketHasConnected { get; }
```

## See Also

* class [DiscordWebSocket](./DiscordWebSocket.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ShouldReconnect field

If we should attempt to reconnect to discord on disconnect

```csharp
public bool ShouldReconnect;
```

## See Also

* class [DiscordWebSocket](./DiscordWebSocket.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ShouldResume field

If we should attempt to resume our previous session after connecting

```csharp
public bool ShouldResume;
```

## See Also

* class [DiscordWebSocket](./DiscordWebSocket.md)
* namespace [Oxide.Ext.Discord.WebSockets](./WebSocketsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
