# DiscordMessage.Create method (1 of 5)

Post a message to a guild text or DM channel. If operating on a guild channel, this endpoint requires the SEND_MESSAGES permission to be present on the current user. If the tts field is set to true, the SEND_TTS_MESSAGES permission is required for the message to be spoken. See [Create Message](https://discord.com/developers/docs/resources/channel#create-message)

```csharp
public static IPromise<DiscordMessage> Create(DiscordClient client, Snowflake channelId, 
    DiscordEmbed embed)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| channelId | Channel ID to send the message to |
| embed | Embed to be send in the message |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../../Interfaces/Promises/IPromise-1.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* struct [Snowflake](../../Snowflake.md)
* class [DiscordEmbed](../Embeds/DiscordEmbed.md)
* class [DiscordMessage](../DiscordMessage.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](../DiscordMessage.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordMessage.Create method (2 of 5)

Post a message to a guild text or DM channel. If operating on a guild channel, this endpoint requires the SEND_MESSAGES permission to be present on the current user. If the tts field is set to true, the SEND_TTS_MESSAGES permission is required for the message to be spoken. See [Create Message](https://discord.com/developers/docs/resources/channel#create-message)

```csharp
public static IPromise<DiscordMessage> Create(DiscordClient client, Snowflake channelId, 
    DiscordMessageBuilder builder)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| channelId | Channel ID to send the message to |
| builder | Builder for the message |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../../Interfaces/Promises/IPromise-1.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* struct [Snowflake](../../Snowflake.md)
* class [DiscordMessageBuilder](../../../Builders/Messages/DiscordMessageBuilder.md)
* class [DiscordMessage](../DiscordMessage.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](../DiscordMessage.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordMessage.Create method (3 of 5)

Post a message to a guild text or DM channel. If operating on a guild channel, this endpoint requires the SEND_MESSAGES permission to be present on the current user. If the tts field is set to true, the SEND_TTS_MESSAGES permission is required for the message to be spoken. See [Create Message](https://discord.com/developers/docs/resources/channel#create-message)

```csharp
public static IPromise<DiscordMessage> Create(DiscordClient client, Snowflake channelId, 
    List<DiscordEmbed> embeds)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| channelId | Channel ID to send the message to |
| embeds | Embeds to be send in the message |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../../Interfaces/Promises/IPromise-1.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* struct [Snowflake](../../Snowflake.md)
* class [DiscordEmbed](../Embeds/DiscordEmbed.md)
* class [DiscordMessage](../DiscordMessage.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](../DiscordMessage.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordMessage.Create method (4 of 5)

Post a message to a guild text or DM channel. If operating on a guild channel, this endpoint requires the SEND_MESSAGES permission to be present on the current user. If the tts field is set to true, the SEND_TTS_MESSAGES permission is required for the message to be spoken. See [Create Message](https://discord.com/developers/docs/resources/channel#create-message)

```csharp
public static IPromise<DiscordMessage> Create(DiscordClient client, Snowflake channelId, 
    MessageCreate message)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| channelId | Channel ID to send the message to |
| message | Message to be created |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../../Interfaces/Promises/IPromise-1.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* struct [Snowflake](../../Snowflake.md)
* class [MessageCreate](../MessageCreate.md)
* class [DiscordMessage](../DiscordMessage.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](../DiscordMessage.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordMessage.Create method (5 of 5)

Post a message to a guild text or DM channel. If operating on a guild channel, this endpoint requires the SEND_MESSAGES permission to be present on the current user. If the tts field is set to true, the SEND_TTS_MESSAGES permission is required for the message to be spoken. See [Create Message](https://discord.com/developers/docs/resources/channel#create-message)

```csharp
public static IPromise<DiscordMessage> Create(DiscordClient client, Snowflake channelId, 
    string message)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| channelId | Channel ID to send the message to |
| message | Content of the message |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../../Interfaces/Promises/IPromise-1.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* struct [Snowflake](../../Snowflake.md)
* class [DiscordMessage](../DiscordMessage.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](../DiscordMessage.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
