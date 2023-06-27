# BotConnection class

Bot Connection Settings

```csharp
public class BotConnection
```

## Public Members

| name | description |
| --- | --- |
| [BotConnection](#botconnection-constructor)() | Constructor |
| [BotConnection](#botconnection-constructor)(…) | Constructor |
| [ApiToken](#apitoken-property) { get; set; } | API token for the bot |
| [ApplicationId](#applicationid-property) { get; } | Application ID of the Bot Token |
| [HiddenToken](#hiddentoken-property) { get; } | Hidden Bot Token. Used when needing to display the token. |
| [Intents](#intents-property) { get; set; } | Intents that your bot needs to work See [`GatewayIntents`](../Entities/Gateway/GatewayIntents.md) |
| [LogLevel](#loglevel-property) { get; set; } | Discord Extension Logging Level. See [`DiscordLogLevel`](../Logging/DiscordLogLevel.md) |
| [HasAnyIntent](#hasanyintent-method)(…) | Returns if the settings has any intent specified |
| [HasIntents](#hasintents-method)(…) | Returns if the settings has the given intents |

## See Also

* namespace [Oxide.Ext.Discord.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [BotConnection.cs](../../../../Oxide.Ext.Discord/Connections/BotConnection.cs)
   
   
# HasIntents method

Returns if the settings has the given intents

```csharp
public bool HasIntents(GatewayIntents intents)
```

| parameter | description |
| --- | --- |
| intents | Intents to be compared against |

## Return Value

True if settings has the given intents; False otherwise

## See Also

* enum [GatewayIntents](../Entities/Gateway/GatewayIntents.md)
* class [BotConnection](./BotConnection.md)
* namespace [Oxide.Ext.Discord.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# HasAnyIntent method

Returns if the settings has any intent specified

```csharp
public bool HasAnyIntent(GatewayIntents intents)
```

| parameter | description |
| --- | --- |
| intents | Intents to compare against |

## Return Value

True if settings has at least one of the given intents

## See Also

* enum [GatewayIntents](../Entities/Gateway/GatewayIntents.md)
* class [BotConnection](./BotConnection.md)
* namespace [Oxide.Ext.Discord.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# BotConnection constructor (1 of 2)

Constructor

```csharp
public BotConnection()
```

## See Also

* class [BotConnection](./BotConnection.md)
* namespace [Oxide.Ext.Discord.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# BotConnection constructor (2 of 2)

Constructor

```csharp
public BotConnection(string apiToken, GatewayIntents intents = GatewayIntents.None, 
    DiscordLogLevel logLevel = DiscordLogLevel.Info)
```

## See Also

* enum [GatewayIntents](../Entities/Gateway/GatewayIntents.md)
* enum [DiscordLogLevel](../Logging/DiscordLogLevel.md)
* class [BotConnection](./BotConnection.md)
* namespace [Oxide.Ext.Discord.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ApiToken property

API token for the bot

```csharp
public string ApiToken { get; set; }
```

## See Also

* class [BotConnection](./BotConnection.md)
* namespace [Oxide.Ext.Discord.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Intents property

Intents that your bot needs to work See [`GatewayIntents`](../Entities/Gateway/GatewayIntents.md)

```csharp
public GatewayIntents Intents { get; set; }
```

## See Also

* enum [GatewayIntents](../Entities/Gateway/GatewayIntents.md)
* class [BotConnection](./BotConnection.md)
* namespace [Oxide.Ext.Discord.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# LogLevel property

Discord Extension Logging Level. See [`DiscordLogLevel`](../Logging/DiscordLogLevel.md)

```csharp
public DiscordLogLevel LogLevel { get; set; }
```

## See Also

* enum [DiscordLogLevel](../Logging/DiscordLogLevel.md)
* class [BotConnection](./BotConnection.md)
* namespace [Oxide.Ext.Discord.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# HiddenToken property

Hidden Bot Token. Used when needing to display the token.

```csharp
public string HiddenToken { get; }
```

## See Also

* class [BotConnection](./BotConnection.md)
* namespace [Oxide.Ext.Discord.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ApplicationId property

Application ID of the Bot Token

```csharp
public Snowflake ApplicationId { get; }
```

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* class [BotConnection](./BotConnection.md)
* namespace [Oxide.Ext.Discord.Connections](./ConnectionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
