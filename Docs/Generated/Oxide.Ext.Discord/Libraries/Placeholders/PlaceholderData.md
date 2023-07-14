# PlaceholderData class

Placeholder Data for placeholders

```csharp
public class PlaceholderData : BasePoolable
```

## Public Members

| name | description |
| --- | --- |
| [Add&lt;T&gt;](#add&amp;lt;t&amp;gt;-method)(…) | Adds the data with the given name |
| [AddChannel](#addchannel-method-1-of-2)(…) | Adds a [`DiscordChannel`](../../Entities/Channels/DiscordChannel.md) by [`DiscordClient`](../../Clients/DiscordClient.md), ChannelId, and Optional GuildId (2 methods) |
| [AddCommand](#addcommand-method)(…) | Add a [`DiscordApplicationCommand`](../../Entities/Interactions/ApplicationCommands/DiscordApplicationCommand.md) |
| [AddGuild](#addguild-method-1-of-2)(…) | Add a [`DiscordGuild`](../../Entities/Guilds/DiscordGuild.md) by [`DiscordClient`](../../Clients/DiscordClient.md) and GuildId (2 methods) |
| [AddGuildMember](#addguildmember-method-1-of-2)(…) | Add a [`GuildMember`](../../Entities/Guilds/GuildMember.md) by [`DiscordClient`](../../Clients/DiscordClient.md), GuildId, and UserId (2 methods) |
| [AddInteraction](#addinteraction-method)(…) | Adds a [`DiscordInteraction`](../../Entities/Interactions/DiscordInteraction.md) |
| [AddMessage](#addmessage-method)(…) | Add a [`DiscordMessage`](../../Entities/Messages/DiscordMessage.md) |
| [AddPlayer](#addplayer-method)(…) | Adds a IPlayer |
| [AddPlugin](#addplugin-method)(…) | Adds a Plugin |
| [AddRequestError](#addrequesterror-method)(…) | Add a [`ResponseError`](../../Entities/Api/ResponseError.md) |
| [AddRole](#addrole-method-1-of-2)(…) | Adds a [`DiscordRole`](../../Entities/Permissions/DiscordRole.md) by [`DiscordClient`](../../Clients/DiscordClient.md), GuildId, and RoleId (2 methods) |
| [AddSnowflake](#addsnowflake-method)(…) | Adds a [`Snowflake`](../../Entities/Snowflake.md) |
| [AddTarget](#addtarget-method)(…) | Adds a target IPlayer |
| [AddTimestamp](#addtimestamp-method-1-of-2)(…) | Adds a Unix Timestamp (2 methods) |
| [AddUser](#adduser-method)(…) | Adds a [`DiscordUser`](../../Entities/Users/DiscordUser.md) |
| [Clone](#clone-method)() | Clones the current placeholder data into a new [`PlaceholderData`](./PlaceholderData.md) |
| [Get&lt;T&gt;](#get&amp;lt;t&amp;gt;-method)() | Returns the object with the given type of {T} The key name used is |
| [Get&lt;T&gt;](#get&amp;lt;t&amp;gt;-method)(…) | Returns the object with the given type of T If the object is not found the default(T) is returned |
| [GetKeys](#getkeys-method)() | Returns comma seperated string of all the registered key Useful for debugging placeholders |
| [ManualPool](#manualpool-method)() | Disable automatic pooling and handle manually by plugin |
| [Remove](#remove-method)(…) | Removes placeholder data key with the given name |
| [RemoveChannel](#removechannel-method)() | Removes channel placeholder data |
| [RemoveGuild](#removeguild-method)() | Removes guild placeholder data |
| [RemoveGuildMember](#removeguildmember-method)() | Removes guild member data |
| [RemoveMessage](#removemessage-method)() | Removes message placeholder data |
| [RemovePlayer](#removeplayer-method)() | Removes player placeholder data |
| [RemoveRole](#removerole-method)() | Removes role placeholder data |
| [RemoveUser](#removeuser-method)() | Removes user placeholder data |

## Protected Members

| name | description |
| --- | --- |
| override [EnterPool](#enterpool-method)() |  |

## See Also

* class [BasePoolable](../../Pooling/BasePoolable.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [PlaceholderData.cs](../../../../Oxide.Ext.Discord/Libraries/Placeholders/PlaceholderData.cs)
   
   
# AddCommand method

Add a [`DiscordApplicationCommand`](../../Entities/Interactions/ApplicationCommands/DiscordApplicationCommand.md)

```csharp
public PlaceholderData AddCommand(DiscordApplicationCommand command)
```

| parameter | description |
| --- | --- |
| command | Application Command to add |

## Return Value

This

## See Also

* class [DiscordApplicationCommand](../../Entities/Interactions/ApplicationCommands/DiscordApplicationCommand.md)
* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddGuild method (1 of 2)

Add a [`DiscordGuild`](../../Entities/Guilds/DiscordGuild.md)

```csharp
public PlaceholderData AddGuild(DiscordGuild guild)
```

| parameter | description |
| --- | --- |
| guild | Guild to add |

## Return Value

This

## See Also

* class [DiscordGuild](../../Entities/Guilds/DiscordGuild.md)
* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# AddGuild method (2 of 2)

Add a [`DiscordGuild`](../../Entities/Guilds/DiscordGuild.md) by [`DiscordClient`](../../Clients/DiscordClient.md) and GuildId

```csharp
public PlaceholderData AddGuild(DiscordClient client, Snowflake? guildId)
```

| parameter | description |
| --- | --- |
| client | Discord Client for the guild |
| guildId | Guild ID of the guild |

## Return Value

This

## See Also

* class [DiscordClient](../../Clients/DiscordClient.md)
* struct [Snowflake](../../Entities/Snowflake.md)
* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RemoveGuild method

Removes guild placeholder data

```csharp
public PlaceholderData RemoveGuild()
```

## Return Value

This

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddMessage method

Add a [`DiscordMessage`](../../Entities/Messages/DiscordMessage.md)

```csharp
public PlaceholderData AddMessage(DiscordMessage message)
```

| parameter | description |
| --- | --- |
| message | Message to add |

## Return Value

This

## See Also

* class [DiscordMessage](../../Entities/Messages/DiscordMessage.md)
* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RemoveMessage method

Removes message placeholder data

```csharp
public PlaceholderData RemoveMessage()
```

## Return Value

This

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddGuildMember method (1 of 2)

Add a [`GuildMember`](../../Entities/Guilds/GuildMember.md)

```csharp
public PlaceholderData AddGuildMember(GuildMember member)
```

| parameter | description |
| --- | --- |
| member | Member to add |

## See Also

* class [GuildMember](../../Entities/Guilds/GuildMember.md)
* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# AddGuildMember method (2 of 2)

Add a [`GuildMember`](../../Entities/Guilds/GuildMember.md) by [`DiscordClient`](../../Clients/DiscordClient.md), GuildId, and UserId

```csharp
public PlaceholderData AddGuildMember(DiscordClient client, Snowflake guildId, Snowflake memberId)
```

| parameter | description |
| --- | --- |
| client | DiscordClient for the guild |
| guildId | Guild ID for the guild |
| memberId | Member UserId in the guild |

## Return Value

This

## See Also

* class [DiscordClient](../../Clients/DiscordClient.md)
* struct [Snowflake](../../Entities/Snowflake.md)
* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RemoveGuildMember method

Removes guild member data

```csharp
public PlaceholderData RemoveGuildMember()
```

## Return Value

This

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddUser method

Adds a [`DiscordUser`](../../Entities/Users/DiscordUser.md)

```csharp
public PlaceholderData AddUser(DiscordUser user)
```

| parameter | description |
| --- | --- |
| user | User to add |

## Return Value

This

## See Also

* class [DiscordUser](../../Entities/Users/DiscordUser.md)
* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RemoveUser method

Removes user placeholder data

```csharp
public PlaceholderData RemoveUser()
```

## Return Value

This

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddRole method (1 of 2)

Adds a [`DiscordRole`](../../Entities/Permissions/DiscordRole.md)

```csharp
public PlaceholderData AddRole(DiscordRole role)
```

| parameter | description |
| --- | --- |
| role | Role to add |

## Return Value

This

## See Also

* class [DiscordRole](../../Entities/Permissions/DiscordRole.md)
* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# AddRole method (2 of 2)

Adds a [`DiscordRole`](../../Entities/Permissions/DiscordRole.md) by [`DiscordClient`](../../Clients/DiscordClient.md), GuildId, and RoleId

```csharp
public PlaceholderData AddRole(DiscordClient client, Snowflake guildId, Snowflake roleId)
```

| parameter | description |
| --- | --- |
| client | Client for the guild |
| guildId | Guild ID of the guild |
| roleId | Role ID of the role |

## Return Value

This

## See Also

* class [DiscordClient](../../Clients/DiscordClient.md)
* struct [Snowflake](../../Entities/Snowflake.md)
* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RemoveRole method

Removes role placeholder data

```csharp
public PlaceholderData RemoveRole()
```

## Return Value

This

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddChannel method (1 of 2)

Adds a [`DiscordChannel`](../../Entities/Channels/DiscordChannel.md)

```csharp
public PlaceholderData AddChannel(DiscordChannel channel)
```

| parameter | description |
| --- | --- |
| channel | Channel to add |

## Return Value

This

## See Also

* class [DiscordChannel](../../Entities/Channels/DiscordChannel.md)
* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# AddChannel method (2 of 2)

Adds a [`DiscordChannel`](../../Entities/Channels/DiscordChannel.md) by [`DiscordClient`](../../Clients/DiscordClient.md), ChannelId, and Optional GuildId

```csharp
public PlaceholderData AddChannel(DiscordClient client, Snowflake channelId, 
    Snowflake? guildId = default)
```

| parameter | description |
| --- | --- |
| client | Client for the channel |
| channelId | Channel ID of the channel |
| guildId | Guild ID of the channel if channel is in a guild |

## Return Value

This

## See Also

* class [DiscordClient](../../Clients/DiscordClient.md)
* struct [Snowflake](../../Entities/Snowflake.md)
* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RemoveChannel method

Removes channel placeholder data

```csharp
public PlaceholderData RemoveChannel()
```

## Return Value

This

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddInteraction method

Adds a [`DiscordInteraction`](../../Entities/Interactions/DiscordInteraction.md)

```csharp
public PlaceholderData AddInteraction(DiscordInteraction interaction)
```

| parameter | description |
| --- | --- |
| interaction | Interaction to add |

## Return Value

This

## See Also

* class [DiscordInteraction](../../Entities/Interactions/DiscordInteraction.md)
* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddPlayer method

Adds a IPlayer

```csharp
public PlaceholderData AddPlayer(IPlayer player)
```

| parameter | description |
| --- | --- |
| player | player to add |

## Return Value

This

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RemovePlayer method

Removes player placeholder data

```csharp
public PlaceholderData RemovePlayer()
```

## Return Value

This

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddTarget method

Adds a target IPlayer

```csharp
public PlaceholderData AddTarget(IPlayer player)
```

| parameter | description |
| --- | --- |
| player | player to add |

## Return Value

This

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddPlugin method

Adds a Plugin

```csharp
public PlaceholderData AddPlugin(Plugin plugin)
```

| parameter | description |
| --- | --- |
| plugin | Plugin to add |

## Return Value

This

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddTimestamp method (1 of 2)

Adds a Unix Timestamp

```csharp
public PlaceholderData AddTimestamp(DateTimeOffset timestamp)
```

| parameter | description |
| --- | --- |
| timestamp | Unix timestamp |

## Return Value

This

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# AddTimestamp method (2 of 2)

Adds a Unix Timestamp

```csharp
public PlaceholderData AddTimestamp(long timestamp)
```

| parameter | description |
| --- | --- |
| timestamp | Unix timestamp |

## Return Value

This

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddSnowflake method

Adds a [`Snowflake`](../../Entities/Snowflake.md)

```csharp
public PlaceholderData AddSnowflake(Snowflake id)
```

| parameter | description |
| --- | --- |
| id | [`Snowflake`](../../Entities/Snowflake.md) ID |

## Return Value

This

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddRequestError method

Add a [`ResponseError`](../../Entities/Api/ResponseError.md)

```csharp
public PlaceholderData AddRequestError(ResponseError error)
```

| parameter | description |
| --- | --- |
| error | RequestError to add |

## Return Value

This

## See Also

* class [ResponseError](../../Entities/Api/ResponseError.md)
* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Add&lt;T&gt; method

Adds the data with the given name

```csharp
public PlaceholderData Add<T>(string name, T obj)
```

| parameter | description |
| --- | --- |
| name | Name of the data key |
| obj | Object to add |

## Return Value

This

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Remove method

Removes placeholder data key with the given name

```csharp
public PlaceholderData Remove(string name)
```

## Return Value

This

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Get&lt;T&gt; method (1 of 2)

Returns the object with the given type of {T} The key name used is

```csharp
nameof(T)
```

```csharp
public T Get<T>()
```

| parameter | description |
| --- | --- |
| T | Type to return |

## Return Value

{T}

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# Get&lt;T&gt; method (2 of 2)

Returns the object with the given type of T If the object is not found the default(T) is returned

```csharp
public T Get<T>(string name)
```

| parameter | description |
| --- | --- |
| T | Type to return |
| name | Name of the data key |

## Return Value

{T}

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetKeys method

Returns comma seperated string of all the registered key Useful for debugging placeholders

```csharp
public string GetKeys()
```

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ManualPool method

Disable automatic pooling and handle manually by plugin

```csharp
public void ManualPool()
```

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Clone method

Clones the current placeholder data into a new [`PlaceholderData`](./PlaceholderData.md)

```csharp
public PlaceholderData Clone()
```

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# EnterPool method

```csharp
protected override void EnterPool()
```

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
