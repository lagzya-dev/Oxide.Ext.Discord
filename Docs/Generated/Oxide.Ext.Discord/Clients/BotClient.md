# BotClient class

Represents a bot connected to discord

```csharp
public class BotClient : BaseClient, IDebugLoggable
```

## Public Members

| name | description |
| --- | --- |
| [BotClient](#botclient-constructor)(…) | Connection settings to use for the bot |
| [Application](#application-property) { get; } | Application reference for this bot |
| [BotUser](#botuser-property) { get; } | Bot User |
| [IsFullyLoaded](#isfullyloaded-property) { get; } | Returns if the bot has fully loaded. All guilds are loaded and if GuildMembers is specified all guild members have been loaded |
| [IsReady](#isready-property) { get; } | Returns if ReadyData is set |
| readonly [DirectMessagesByChannelId](#directmessagesbychannelid-field) | All the direct messages that we have seen by channel ID |
| readonly [DirectMessagesByUserId](#directmessagesbyuserid-field) | All the direct messages that we have seen by User ID |
| readonly [Servers](#servers-field) | All the servers that this bot is in |
| override [AddClient](#addclient-method)(…) |  |
| [AddDirectChannel](#adddirectchannel-method)(…) | Adds a Direct Message Channel to the bot cache |
| [AddGuild](#addguild-method)(…) | Adds a guild to the list of servers a bot is in |
| [AddGuildOrUpdate](#addguildorupdate-method)(…) | Adds a guild if it does not exist or updates the guild with |
| [DisconnectWebsocket](#disconnectwebsocket-method)(…) | Close the websocket with discord |
| [GetChannel](#getchannel-method)(…) | Returns the channel for the given channel ID. If guild ID is null, it will search for a direct message channel If guild ID is not null, it will search for a guild channel |
| [GetGuild](#getguild-method)(…) | Returns a guild for the specific ID |
| [LogDebug](#logdebug-method)(…) |  |
| override [RemoveClient](#removeclient-method)(…) |  |
| [RemoveDirectMessageChannel](#removedirectmessagechannel-method)(…) | Removes a direct message channel if it exists |
| [SendWebSocketCommand](#sendwebsocketcommand-method)(…) | Sends a websocket command |

## See Also

* class [BaseClient](./BaseClient.md)
* interface [IDebugLoggable](../Interfaces/IDebugLoggable.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [BotClient.cs](../../../../Oxide.Ext.Discord/Clients/BotClient.cs)
   
   
# DisconnectWebsocket method

Close the websocket with discord

```csharp
public void DisconnectWebsocket(bool reconnect = false, bool resume = false)
```

| parameter | description |
| --- | --- |
| reconnect | Should we attempt to reconnect to discord after closing |
| resume | Should we attempt to resume the previous session |

## See Also

* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddClient method

```csharp
public override bool AddClient(DiscordClient client, PluginSetup setup)
```

## See Also

* class [DiscordClient](./DiscordClient.md)
* class [PluginSetup](../Plugins/PluginSetup.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# RemoveClient method

```csharp
public override bool RemoveClient(DiscordClient client)
```

## See Also

* class [DiscordClient](./DiscordClient.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# SendWebSocketCommand method

Sends a websocket command

```csharp
public void SendWebSocketCommand(DiscordClient client, GatewayCommandCode opCode, object data)
```

| parameter | description |
| --- | --- |
| client | Client sending the command |
| opCode | [`GatewayCommandCode`](../Entities/GatewayCommandCode.md) OP Code for the command |
| data | Command Payload |

## See Also

* class [DiscordClient](./DiscordClient.md)
* enum [GatewayCommandCode](../Entities/GatewayCommandCode.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetGuild method

Returns a guild for the specific ID

```csharp
public DiscordGuild GetGuild(Snowflake? guildId)
```

| parameter | description |
| --- | --- |
| guildId | ID of the guild |

## Return Value

Guild with the specified ID

## See Also

* class [DiscordGuild](../Entities/DiscordGuild.md)
* struct [Snowflake](../Entities/Snowflake.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetChannel method

Returns the channel for the given channel ID. If guild ID is null, it will search for a direct message channel If guild ID is not null, it will search for a guild channel

```csharp
public DiscordChannel GetChannel(Snowflake channelId, Snowflake? guildId)
```

| parameter | description |
| --- | --- |
| channelId |  |
| guildId |  |

## See Also

* class [DiscordChannel](../Entities/DiscordChannel.md)
* struct [Snowflake](../Entities/Snowflake.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddGuild method

Adds a guild to the list of servers a bot is in

```csharp
public void AddGuild(DiscordGuild guild)
```

| parameter | description |
| --- | --- |
| guild |  |

## See Also

* class [DiscordGuild](../Entities/DiscordGuild.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddGuildOrUpdate method

Adds a guild if it does not exist or updates the guild with

```csharp
public void AddGuildOrUpdate(DiscordGuild guild)
```

| parameter | description |
| --- | --- |
| guild |  |

## See Also

* class [DiscordGuild](../Entities/DiscordGuild.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddDirectChannel method

Adds a Direct Message Channel to the bot cache

```csharp
public void AddDirectChannel(DiscordChannel channel)
```

| parameter | description |
| --- | --- |
| channel | Channel to be added |

## See Also

* class [DiscordChannel](../Entities/DiscordChannel.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# RemoveDirectMessageChannel method

Removes a direct message channel if it exists

```csharp
public void RemoveDirectMessageChannel(Snowflake id)
```

| parameter | description |
| --- | --- |
| id | ID of the channel to remove |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# LogDebug method

```csharp
public void LogDebug(DebugLogger logger)
```

## See Also

* class [DebugLogger](../Logging/DebugLogger.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# BotClient constructor

Connection settings to use for the bot

```csharp
public BotClient(BotConnection connection)
```

| parameter | description |
| --- | --- |
| connection |  |

## See Also

* class [BotConnection](../Connections/BotConnection.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Application property

Application reference for this bot

```csharp
public DiscordApplication Application { get; }
```

## See Also

* class [DiscordApplication](../Entities/DiscordApplication.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# BotUser property

Bot User

```csharp
public DiscordUser BotUser { get; }
```

## See Also

* class [DiscordUser](../Entities/DiscordUser.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IsFullyLoaded property

Returns if the bot has fully loaded. All guilds are loaded and if GuildMembers is specified all guild members have been loaded

```csharp
public bool IsFullyLoaded { get; }
```

## See Also

* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IsReady property

Returns if ReadyData is set

```csharp
public bool IsReady { get; }
```

## See Also

* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Servers field

All the servers that this bot is in

```csharp
public readonly Hash<Snowflake, DiscordGuild> Servers;
```

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* class [DiscordGuild](../Entities/DiscordGuild.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DirectMessagesByChannelId field

All the direct messages that we have seen by channel ID

```csharp
public readonly Hash<Snowflake, DiscordChannel> DirectMessagesByChannelId;
```

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* class [DiscordChannel](../Entities/DiscordChannel.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DirectMessagesByUserId field

All the direct messages that we have seen by User ID

```csharp
public readonly Hash<Snowflake, DiscordChannel> DirectMessagesByUserId;
```

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* class [DiscordChannel](../Entities/DiscordChannel.md)
* class [BotClient](./BotClient.md)
* namespace [Oxide.Ext.Discord.Clients](./ClientsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
