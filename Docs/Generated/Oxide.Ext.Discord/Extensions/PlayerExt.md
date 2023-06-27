# PlayerExt class

IPlayer Extensions for sending Discord Message to an IPlayer

```csharp
public static class PlayerExt
```

## Public Members

| name | description |
| --- | --- |
| static [GetDiscordUser](#getdiscorduser-method)(…) | Returns a minimal Discord User for the given player |
| static [GetDiscordUserId](#getdiscorduserid-method)(…) | Returns the Discord ID of the IPlayer if linked |
| static [GetGuildMember](#getguildmember-method)(…) | Returns a minimal Guild Member for the given player |
| static [IsDummyPlayer](#isdummyplayer-method)(…) | Returns if the IPlayer is a DiscordDummyPlayer |
| static [IsLinked](#islinked-method)(…) | Returns true if the player is linked |
| static [SendDiscordGlobalTemplateMessage](#senddiscordglobaltemplatemessage-method)(…) | Send a message in a DM to the linked user using a global message template |
| static [SendDiscordMessage](#senddiscordmessage-method-1-of-4)(…) | Send a Discord Message to an IPlayer if they're registered (4 methods) |
| static [SendDiscordTemplateMessage](#senddiscordtemplatemessage-method)(…) | Send a message in a DM to the linked user using a localized message template |

## See Also

* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [PlayerExt.cs](../../../../Oxide.Ext.Discord/Extensions/PlayerExt.cs)
   
   
# SendDiscordMessage method (1 of 4)

Send a Discord Message to an IPlayer if they're registered

```csharp
public static IPromise<DiscordMessage> SendDiscordMessage(this IPlayer player, 
    DiscordClient client, DiscordEmbed embed)
```

| parameter | description |
| --- | --- |
| player | Player to send the discord message to |
| client | Client to use for sending the message |
| embed | Embed to send |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordMessage](../Entities/Messages/DiscordMessage.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [DiscordEmbed](../Entities/Messages/Embeds/DiscordEmbed.md)
* class [PlayerExt](./PlayerExt.md)
* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# SendDiscordMessage method (2 of 4)

Send a Discord Message to an IPlayer if they're registered

```csharp
public static IPromise<DiscordMessage> SendDiscordMessage(this IPlayer player, 
    DiscordClient client, List<DiscordEmbed> embeds)
```

| parameter | description |
| --- | --- |
| player | Player to send the discord message to |
| client | Client to use for sending the message |
| embeds | Embeds to send |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordMessage](../Entities/Messages/DiscordMessage.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [DiscordEmbed](../Entities/Messages/Embeds/DiscordEmbed.md)
* class [PlayerExt](./PlayerExt.md)
* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# SendDiscordMessage method (3 of 4)

Send a Discord Message to an IPlayer if they're registered

```csharp
public static IPromise<DiscordMessage> SendDiscordMessage(this IPlayer player, 
    DiscordClient client, MessageCreate message)
```

| parameter | description |
| --- | --- |
| player | Player to send the discord message to |
| client | Client to use for sending the message |
| message | Message to send |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordMessage](../Entities/Messages/DiscordMessage.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [MessageCreate](../Entities/Messages/MessageCreate.md)
* class [PlayerExt](./PlayerExt.md)
* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# SendDiscordMessage method (4 of 4)

Send a Discord Message to an IPlayer if they're registered

```csharp
public static IPromise<DiscordMessage> SendDiscordMessage(this IPlayer player, 
    DiscordClient client, string message)
```

| parameter | description |
| --- | --- |
| player | Player to send the discord message to |
| client | Client to use for sending the message |
| message | Message to send |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordMessage](../Entities/Messages/DiscordMessage.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [PlayerExt](./PlayerExt.md)
* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# SendDiscordGlobalTemplateMessage method

Send a message in a DM to the linked user using a global message template

```csharp
public static IPromise<DiscordMessage> SendDiscordGlobalTemplateMessage(this IPlayer player, 
    DiscordClient client, string templateName, MessageCreate message = null, 
    PlaceholderData placeholders = null)
```

| parameter | description |
| --- | --- |
| player | Player to send the message to |
| client | Client to use |
| templateName | Template Name |
| message | Message to use (optional) |
| placeholders | Placeholders to apply (optional) |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordMessage](../Entities/Messages/DiscordMessage.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [MessageCreate](../Entities/Messages/MessageCreate.md)
* class [PlaceholderData](../Libraries/Placeholders/PlaceholderData.md)
* class [PlayerExt](./PlayerExt.md)
* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# SendDiscordTemplateMessage method

Send a message in a DM to the linked user using a localized message template

```csharp
public static IPromise<DiscordMessage> SendDiscordTemplateMessage(this IPlayer player, 
    DiscordClient client, string templateName, MessageCreate message = null, 
    PlaceholderData placeholders = null)
```

| parameter | description |
| --- | --- |
| player | Player to send the message to |
| client | Client to use |
| templateName | Template Name |
| message | Message to use (optional) |
| placeholders | Placeholders to apply (optional) |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordMessage](../Entities/Messages/DiscordMessage.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [MessageCreate](../Entities/Messages/MessageCreate.md)
* class [PlaceholderData](../Libraries/Placeholders/PlaceholderData.md)
* class [PlayerExt](./PlayerExt.md)
* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IsLinked method

Returns true if the player is linked

```csharp
public static bool IsLinked(this IPlayer player)
```

| parameter | description |
| --- | --- |
| player | Player to check if they're linked |

## Return Value

True if linked; False otherwise

## See Also

* class [PlayerExt](./PlayerExt.md)
* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetDiscordUserId method

Returns the Discord ID of the IPlayer if linked

```csharp
public static Snowflake? GetDiscordUserId(this IPlayer player)
```

| parameter | description |
| --- | --- |
| player | Player to get Discord ID for |

## Return Value

Discord ID if linked; null otherwise

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* class [PlayerExt](./PlayerExt.md)
* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetDiscordUser method

Returns a minimal Discord User for the given player

```csharp
public static DiscordUser GetDiscordUser(this IPlayer player)
```

| parameter | description |
| --- | --- |
| player | Player to get Discord User for |

## Return Value

Discord User if linked; null otherwise

## See Also

* class [DiscordUser](../Entities/Users/DiscordUser.md)
* class [PlayerExt](./PlayerExt.md)
* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetGuildMember method

Returns a minimal Guild Member for the given player

```csharp
public static GuildMember GetGuildMember(this IPlayer player, DiscordGuild guild)
```

| parameter | description |
| --- | --- |
| player | Player to get Discord User for |
| guild | Guild the member is in |

## Return Value

GuildMember if linked and in guild; null otherwise

## See Also

* class [GuildMember](../Entities/Guilds/GuildMember.md)
* class [DiscordGuild](../Entities/Guilds/DiscordGuild.md)
* class [PlayerExt](./PlayerExt.md)
* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IsDummyPlayer method

Returns if the IPlayer is a DiscordDummyPlayer

```csharp
public static bool IsDummyPlayer(this IPlayer player)
```

| parameter | description |
| --- | --- |
| player |  |

## See Also

* class [PlayerExt](./PlayerExt.md)
* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
