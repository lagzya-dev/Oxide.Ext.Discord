# PlaceholderData class

Placeholder Data for placeholders

```csharp
public class PlaceholderData : BasePoolable
```

## Public Members

| name | description |
| --- | --- |
| [Add&lt;T&gt;](#Add)(…) | Adds the data with the given name |
| [AddChannel](#AddChannel)(…) | Adds a [`DiscordChannel`](../../Entities/Channels/DiscordChannel.md) by [`DiscordClient`](../../Clients/DiscordClient.md), ChannelId, and Optional GuildId (2 methods) |
| [AddCommand](#AddCommand)(…) | Add a [`DiscordApplicationCommand`](../../Entities/Interactions/ApplicationCommands/DiscordApplicationCommand.md) |
| [AddGuild](#AddGuild)(…) | Add a [`DiscordGuild`](../../Entities/Guilds/DiscordGuild.md) by [`DiscordClient`](../../Clients/DiscordClient.md) and GuildId (2 methods) |
| [AddGuildMember](#AddGuildMember)(…) | Add a [`GuildMember`](../../Entities/Guilds/GuildMember.md) by [`DiscordClient`](../../Clients/DiscordClient.md), GuildId, and UserId (2 methods) |
| [AddInteraction](#AddInteraction)(…) | Adds a [`DiscordInteraction`](../../Entities/Interactions/DiscordInteraction.md) |
| [AddMessage](#AddMessage)(…) | Add a [`DiscordMessage`](../../Entities/Messages/DiscordMessage.md) |
| [AddPlayer](#AddPlayer)(…) | Adds a IPlayer |
| [AddPlugin](#AddPlugin)(…) | Adds a Plugin |
| [AddRequestError](#AddRequestError)(…) | Add a [`ResponseError`](../../Entities/Api/ResponseError.md) |
| [AddRole](#AddRole)(…) | Adds a [`DiscordRole`](../../Entities/Permissions/DiscordRole.md) by [`DiscordClient`](../../Clients/DiscordClient.md), GuildId, and RoleId (2 methods) |
| [AddSnowflake](#AddSnowflake)(…) | Adds a [`Snowflake`](../../Entities/Snowflake.md) |
| [AddTarget](#AddTarget)(…) | Adds a target IPlayer |
| [AddTimestamp](#AddTimestamp)(…) | Adds a Unix Timestamp |
| [AddUser](#AddUser)(…) | Adds a [`DiscordUser`](../../Entities/Users/DiscordUser.md) |
| [Clone](#Clone)() | Clones the current placeholder data into a new [`PlaceholderData`](./PlaceholderData.md) |
| [Get&lt;T&gt;](#Get)() | Returns the object with the given type of {T} The key name used is |
| [Get&lt;T&gt;](#Get)(…) | Returns the object with the given type of T If the object is not found the default(T) is returned |
| [GetKeys](#GetKeys)() | Returns comma seperated string of all the registered key Useful for debugging placeholders |
| [ManualPool](#ManualPool)() | Disable automatic pooling and handle manually by plugin |

## Protected Members

| name | description |
| --- | --- |
| override [EnterPool](#EnterPool)() |  |

## See Also

* class [BasePoolable](../../Pooling/BasePoolable.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [PlaceholderData.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Libraries/Placeholders/PlaceholderData.cs)
   
   
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
   
   
# AddTimestamp method

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
