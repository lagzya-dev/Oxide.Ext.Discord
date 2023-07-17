# DiscordLink class

Represents a library for discord linking

```csharp
public class DiscordLink : BaseDiscordLibrary<DiscordLink>, IDebugLoggable
```

## Public Members

| name | description |
| --- | --- |
| [IsEnabled](#isenabled-property) { get; } | Returns if there is a registered link plugin |
| [LinkedCount](#linkedcount-property) { get; } | Returns the number of linked players |
| readonly [DiscordIds](#discordids-field) | Readonly Collection of all Discord ID's |
| readonly [DiscordToPlayerIds](#discordtoplayerids-field) | Readonly Dictionary of Discord ID's to Player ID's |
| readonly [PlayerIds](#playerids-field) | Readonly Collection of all Player ID's |
| readonly [PlayerToDiscordIds](#playertodiscordids-field) | Readonly Dictionary of Player ID's to Discord ID's |
| [AddLinkPlugin](#addlinkplugin-method)(…) | Adds a link plugin to be the plugin used with the Discord Link library |
| [GetDiscordId](#getdiscordid-method-1-of-3)(…) | Returns the Discord ID for the given Player ID (3 methods) |
| [GetDiscordUser](#getdiscorduser-method-1-of-3)(…) | Returns a minimal Discord User (3 methods) |
| [GetLinkedMember](#getlinkedmember-method-1-of-3)(…) | Returns a linked guild member for the matching player id in the given guild (3 methods) |
| [GetPlayer](#getplayer-method)(…) | Returns the IPlayer for the given Discord ID |
| [GetPlayerId](#getplayerid-method-1-of-2)(…) | Returns the Player ID of the given Discord ID if there is a link (2 methods) |
| [IsLinked](#islinked-method-1-of-5)(…) | Returns if the specified ID is linked (5 methods) |
| [LogDebug](#logdebug-method)(…) |  |
| [OnLinked](#onlinked-method)(…) | Called by a link plugin when a link occured |
| [OnUnlinked](#onunlinked-method)(…) | Called by a link plugin when an unlink occured |
| [RemoveLinkPlugin](#removelinkplugin-method)(…) | Removes a link plugin from the Discord Link library |

## Protected Members

| name | description |
| --- | --- |
| override [OnPluginUnloaded](#onpluginunloaded-method)(…) |  |

## See Also

* class [BaseDiscordLibrary&lt;TLibrary&gt;](../BaseDiscordLibrary%7BTLibrary%7D.md)
* interface [IDebugLoggable](../../Interfaces/Logging/IDebugLoggable.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordLink.cs](../../../../Oxide.Ext.Discord/Libraries/Linking/DiscordLink.cs)
   
   
# AddLinkPlugin method

Adds a link plugin to be the plugin used with the Discord Link library

```csharp
public void AddLinkPlugin(IDiscordLinkPlugin plugin)
```

| parameter | description |
| --- | --- |
| plugin |  |

## See Also

* interface [IDiscordLinkPlugin](./IDiscordLinkPlugin.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RemoveLinkPlugin method

Removes a link plugin from the Discord Link library

```csharp
public void RemoveLinkPlugin(IDiscordLinkPlugin plugin)
```

| parameter | description |
| --- | --- |
| plugin |  |

## See Also

* interface [IDiscordLinkPlugin](./IDiscordLinkPlugin.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# OnPluginUnloaded method

```csharp
protected override void OnPluginUnloaded(Plugin plugin)
```

## See Also

* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# IsLinked method (1 of 5)

Returns if the specified ID is linked

```csharp
public bool IsLinked(DiscordUser user)
```

| parameter | description |
| --- | --- |
| user | Discord user to check |

## Return Value

True if the user is linked; false otherwise

## See Also

* class [DiscordUser](../../Entities/Users/DiscordUser.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# IsLinked method (2 of 5)

Returns if the specified ID is linked

```csharp
public bool IsLinked(IPlayer player)
```

| parameter | description |
| --- | --- |
| player | Player to check if linked |

## Return Value

True if the player is linked; false otherwise

## See Also

* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# IsLinked method (3 of 5)

Returns if the specified ID is linked

```csharp
public bool IsLinked(PlayerId playerId)
```

| parameter | description |
| --- | --- |
| playerId | Player ID of the player |

## Return Value

True if the ID is linked; false otherwise

## See Also

* struct [PlayerId](./PlayerId.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# IsLinked method (4 of 5)

Returns if the specified ID is linked

```csharp
public bool IsLinked(Snowflake discordId)
```

| parameter | description |
| --- | --- |
| discordId | Discord ID of the player |

## Return Value

True if the ID is linked; false otherwise

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# IsLinked method (5 of 5)

Returns if the specified ID is linked

```csharp
public bool IsLinked(string playerId)
```

| parameter | description |
| --- | --- |
| playerId | Player ID of the player |

## Return Value

True if the ID is linked; false otherwise

## See Also

* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetPlayerId method (1 of 2)

Returns the Player ID of the given Discord ID if there is a link

```csharp
public PlayerId GetPlayerId(DiscordUser user)
```

| parameter | description |
| --- | --- |
| user | [`DiscordUser`](../../Entities/Users/DiscordUser.md) to get player Id for |

## Return Value

Player ID of the given given discord ID if linked; null otherwise

## See Also

* struct [PlayerId](./PlayerId.md)
* class [DiscordUser](../../Entities/Users/DiscordUser.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# GetPlayerId method (2 of 2)

Returns the Player ID of the given Discord ID if there is a link

```csharp
public PlayerId GetPlayerId(Snowflake discordId)
```

| parameter | description |
| --- | --- |
| discordId | Discord ID to get player ID for |

## Return Value

Player ID of the given given discord ID if linked; null otherwise

## See Also

* struct [PlayerId](./PlayerId.md)
* struct [Snowflake](../../Entities/Snowflake.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetPlayer method

Returns the IPlayer for the given Discord ID

```csharp
public IPlayer GetPlayer(Snowflake discordId)
```

| parameter | description |
| --- | --- |
| discordId | Discord ID to get IPlayer for |

## Return Value

IPlayer for the given Discord ID; null otherwise

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetDiscordId method (1 of 3)

Returns the Discord ID for the given IPlayer

```csharp
public Snowflake GetDiscordId(IPlayer player)
```

| parameter | description |
| --- | --- |
| player | Player to get Discord ID for |

## Return Value

Discord ID for the given Player ID; null otherwise

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# GetDiscordId method (2 of 3)

Returns the Discord ID for the given Player ID

```csharp
public Snowflake GetDiscordId(PlayerId playerId)
```

| parameter | description |
| --- | --- |
| playerId | Player ID to get Discord ID for |

## Return Value

Discord ID for the given Player ID; null otherwise

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* struct [PlayerId](./PlayerId.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# GetDiscordId method (3 of 3)

Returns the Discord ID for the given Player ID

```csharp
public Snowflake GetDiscordId(string playerId)
```

| parameter | description |
| --- | --- |
| playerId | Player ID to get Discord ID for |

## Return Value

Discord ID for the given Player ID; null otherwise

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetDiscordUser method (1 of 3)

Returns a minimal Discord User

```csharp
public DiscordUser GetDiscordUser(IPlayer player)
```

| parameter | description |
| --- | --- |
| player | Player to get the Discord User for |

## Return Value

Discord ID for the given IPlayer; null otherwise

## See Also

* class [DiscordUser](../../Entities/Users/DiscordUser.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# GetDiscordUser method (2 of 3)

Returns a minimal Discord User

```csharp
public DiscordUser GetDiscordUser(PlayerId playerId)
```

| parameter | description |
| --- | --- |
| playerId | ID of the in game player |

## Return Value

Discord ID for the given Player ID; null otherwise

## See Also

* class [DiscordUser](../../Entities/Users/DiscordUser.md)
* struct [PlayerId](./PlayerId.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# GetDiscordUser method (3 of 3)

Returns a minimal Discord User

```csharp
public DiscordUser GetDiscordUser(string playerId)
```

| parameter | description |
| --- | --- |
| playerId | ID of the in game player |

## Return Value

Discord ID for the given Player ID; null otherwise

## See Also

* class [DiscordUser](../../Entities/Users/DiscordUser.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetLinkedMember method (1 of 3)

Returns a linked guild member for the matching IPlayer in the given guild

```csharp
public GuildMember GetLinkedMember(IPlayer player, DiscordGuild guild)
```

| parameter | description |
| --- | --- |
| player | Player to get the Discord User for |
| guild | Guild the member is in |

## Return Value

Discord ID for the given Player ID; null otherwise

## See Also

* class [GuildMember](../../Entities/Guilds/GuildMember.md)
* class [DiscordGuild](../../Entities/Guilds/DiscordGuild.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# GetLinkedMember method (2 of 3)

Returns a linked guild member for the matching player id in the given guild

```csharp
public GuildMember GetLinkedMember(PlayerId playerId, DiscordGuild guild)
```

| parameter | description |
| --- | --- |
| playerId | ID of the in game player |
| guild | Guild the member is in |

## Return Value

Discord ID for the given Player ID; null otherwise

## See Also

* class [GuildMember](../../Entities/Guilds/GuildMember.md)
* struct [PlayerId](./PlayerId.md)
* class [DiscordGuild](../../Entities/Guilds/DiscordGuild.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# GetLinkedMember method (3 of 3)

Returns a linked guild member for the matching player id in the given guild

```csharp
public GuildMember GetLinkedMember(string playerId, DiscordGuild guild)
```

| parameter | description |
| --- | --- |
| playerId | ID of the in game player |
| guild | Guild the member is in |

## Return Value

Discord ID for the given Player ID; null otherwise

## See Also

* class [GuildMember](../../Entities/Guilds/GuildMember.md)
* class [DiscordGuild](../../Entities/Guilds/DiscordGuild.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# OnLinked method

Called by a link plugin when a link occured

```csharp
public void OnLinked(Plugin plugin, IPlayer player, DiscordUser discord)
```

| parameter | description |
| --- | --- |
| plugin | Plugin that initiated the link |
| player | Player being linked |
| discord | DiscordUser being linked |

## See Also

* class [DiscordUser](../../Entities/Users/DiscordUser.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# OnUnlinked method

Called by a link plugin when an unlink occured

```csharp
public void OnUnlinked(Plugin plugin, IPlayer player, DiscordUser discord)
```

| parameter | description |
| --- | --- |
| plugin | Plugin that is unlinking |
| player | Player being unlinked |
| discord | DiscordUser being unlinked |

## See Also

* class [DiscordUser](../../Entities/Users/DiscordUser.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# LogDebug method

```csharp
public void LogDebug(DebugLogger logger)
```

## See Also

* class [DebugLogger](../../Logging/DebugLogger.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# IsEnabled property

Returns if there is a registered link plugin

```csharp
public bool IsEnabled { get; }
```

## See Also

* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# LinkedCount property

Returns the number of linked players

```csharp
public int LinkedCount { get; }
```

## See Also

* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# PlayerToDiscordIds field

Readonly Dictionary of Player ID's to Discord ID's

```csharp
public readonly IReadOnlyDictionary<PlayerId, Snowflake> PlayerToDiscordIds;
```

## See Also

* struct [PlayerId](./PlayerId.md)
* struct [Snowflake](../../Entities/Snowflake.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# DiscordToPlayerIds field

Readonly Dictionary of Discord ID's to Player ID's

```csharp
public readonly IReadOnlyDictionary<Snowflake, PlayerId> DiscordToPlayerIds;
```

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* struct [PlayerId](./PlayerId.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# PlayerIds field

Readonly Collection of all Player ID's

```csharp
public readonly ICollection<PlayerId> PlayerIds;
```

## See Also

* struct [PlayerId](./PlayerId.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# DiscordIds field

Readonly Collection of all Discord ID's

```csharp
public readonly ICollection<Snowflake> DiscordIds;
```

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* class [DiscordLink](./DiscordLink.md)
* namespace [Oxide.Ext.Discord.Libraries.Linking](./LinkingNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
