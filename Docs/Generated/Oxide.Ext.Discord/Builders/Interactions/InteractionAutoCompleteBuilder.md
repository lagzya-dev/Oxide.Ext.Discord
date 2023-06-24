# InteractionAutoCompleteBuilder class

Builder for Auto Complete Interaction

```csharp
public class InteractionAutoCompleteBuilder
```

## Public Members

| name | description |
| --- | --- |
| [InteractionAutoCompleteBuilder](#InteractionAutoCompleteBuilder)(…) | Constructor |
| [AddAllOnlineFirstPlayers](#AddAllOnlineFirstPlayers)(…) | Adds Online Players to the list first If there is still space add Offline Players |
| [AddAllPlayers](#AddAllPlayers)(…) | Adds Any Player to the list |
| [AddByPlayerId](#AddByPlayerId)(…) | Adds a player by player Id to the list |
| [AddChoice](#AddChoice)(…) | Adds a [`CommandOptionChoice`](../../Entities/Interactions/ApplicationCommands/CommandOptionChoice.md) to the response (3 methods) |
| [AddChoices](#AddChoices)(…) | Adds a collection of [`CommandOptionChoice`](../../Entities/Interactions/ApplicationCommands/CommandOptionChoice.md) to the response |
| [AddGroups](#AddGroups)(…) | Adds Oxide Groups to the AutoComplete |
| [AddGroupsWithoutPermission](#AddGroupsWithoutPermission)(…) | Adds The List of Groups that have this permission |
| [AddGroupsWithoutPlayer](#AddGroupsWithoutPlayer)(…) | Adds The List of Groups that playerId has |
| [AddGroupsWithPermission](#AddGroupsWithPermission)(…) | Adds The List of Groups that have this permission |
| [AddGroupsWithPlayer](#AddGroupsWithPlayer)(…) | Adds The List of Groups that playerId has |
| [AddLoadablePlugins](#AddLoadablePlugins)(…) | Adds a list of plugins that can be loaded |
| [AddLoadedPlugins](#AddLoadedPlugins)(…) | Adds a list of plugins that are currently loaded |
| [AddOfflinePlayers](#AddOfflinePlayers)(…) | Adds Online Players to the list |
| [AddOnlinePlayers](#AddOnlinePlayers)(…) | Adds Online Players to the list |
| [AddPermissions](#AddPermissions)(…) | Adds Oxide Permissions to the AutoComplete |
| [AddPermissionsInGroup](#AddPermissionsInGroup)(…) |  |
| [AddPermissionsNotInGroup](#AddPermissionsNotInGroup)(…) |  |
| [AddPermissionsPlayerIn](#AddPermissionsPlayerIn)(…) | Adds The List of Permissions that playerId has |
| [AddPermissionsPlayerNotIn](#AddPermissionsPlayerNotIn)(…) | Adds The List of Permissions that playerId does not have |
| [Build](#Build)() | Returns the built message |
| [CanAddChoice](#CanAddChoice)() | Returns if the Auto Complete can add any more choices |

## See Also

* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [InteractionAutoCompleteBuilder.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Builders/Interactions/InteractionAutoCompleteBuilder.cs)
   
   
# AddChoice method (1 of 3)

Adds a [`CommandOptionChoice`](../../Entities/Interactions/ApplicationCommands/CommandOptionChoice.md) to the response

```csharp
public InteractionAutoCompleteBuilder AddChoice(CommandOptionChoice choice)
```

| parameter | description |
| --- | --- |
| choice | Choice to be added |

## Return Value

This

## See Also

* class [CommandOptionChoice](../../Entities/Interactions/ApplicationCommands/CommandOptionChoice.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---
   
   
# AddChoices method

Adds a collection of [`CommandOptionChoice`](../../Entities/Interactions/ApplicationCommands/CommandOptionChoice.md) to the response

```csharp
public InteractionAutoCompleteBuilder AddChoices(ICollection<CommandOptionChoice> choices)
```

| parameter | description |
| --- | --- |
| choices | Choices to be added |

## Return Value

This

## See Also

* class [CommandOptionChoice](../../Entities/Interactions/ApplicationCommands/CommandOptionChoice.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# CanAddChoice method

Returns if the Auto Complete can add any more choices

```csharp
public bool CanAddChoice()
```

## See Also

* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Build method

Returns the built message

```csharp
public InteractionAutoCompleteMessage Build()
```

## Return Value

[`InteractionAutoCompleteMessage`](../../Entities/Interactions/Response/InteractionAutoCompleteMessage.md)

## See Also

* class [InteractionAutoCompleteMessage](../../Entities/Interactions/Response/InteractionAutoCompleteMessage.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddGroups method

Adds Oxide Groups to the AutoComplete

```csharp
public void AddGroups(string filter = null, 
    StringComparison comparison = StringComparison.OrdinalIgnoreCase, 
    AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
```

| parameter | description |
| --- | --- |
| filter | String to filter by |
| comparison | StringComparison to use |
| search | [`AutoCompleteSearchMode`](./AutoComplete/AutoCompleteSearchMode.md) Filter search mode |

## See Also

* enum [AutoCompleteSearchMode](./AutoComplete/AutoCompleteSearchMode.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddPermissions method

Adds Oxide Permissions to the AutoComplete

```csharp
public void AddPermissions(string filter = null, 
    StringComparison comparison = StringComparison.OrdinalIgnoreCase, 
    AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
```

| parameter | description |
| --- | --- |
| filter | String to filter by |
| comparison | StringComparison to use |
| search | [`AutoCompleteSearchMode`](./AutoComplete/AutoCompleteSearchMode.md) Filter search mode |

## See Also

* enum [AutoCompleteSearchMode](./AutoComplete/AutoCompleteSearchMode.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddGroupsWithPermission method

Adds The List of Groups that have this permission

```csharp
public void AddGroupsWithPermission(string permission, string filter = null, 
    StringComparison comparison = StringComparison.OrdinalIgnoreCase, 
    AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
```

| parameter | description |
| --- | --- |
| permission | Permission to get groups for |
| filter | String to filter by |
| comparison | StringComparison to use |
| search | [`AutoCompleteSearchMode`](./AutoComplete/AutoCompleteSearchMode.md) Filter search mode |

## See Also

* enum [AutoCompleteSearchMode](./AutoComplete/AutoCompleteSearchMode.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddGroupsWithoutPermission method

Adds The List of Groups that have this permission

```csharp
public void AddGroupsWithoutPermission(string permission, string filter = null, 
    StringComparison comparison = StringComparison.OrdinalIgnoreCase, 
    AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
```

| parameter | description |
| --- | --- |
| permission | Permission to get groups for |
| filter | String to filter by |
| comparison | StringComparison to use |
| search | [`AutoCompleteSearchMode`](./AutoComplete/AutoCompleteSearchMode.md) Filter search mode |

## See Also

* enum [AutoCompleteSearchMode](./AutoComplete/AutoCompleteSearchMode.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddPermissionsInGroup method

```csharp
public void AddPermissionsInGroup(string group, string filter = null, 
    StringComparison comparison = StringComparison.OrdinalIgnoreCase, 
    AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
```

## See Also

* enum [AutoCompleteSearchMode](./AutoComplete/AutoCompleteSearchMode.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddPermissionsNotInGroup method

```csharp
public void AddPermissionsNotInGroup(string group, string filter = null, 
    StringComparison comparison = StringComparison.OrdinalIgnoreCase, 
    AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
```

## See Also

* enum [AutoCompleteSearchMode](./AutoComplete/AutoCompleteSearchMode.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddGroupsWithPlayer method

Adds The List of Groups that playerId has

```csharp
public void AddGroupsWithPlayer(string playerId, string filter = null, 
    StringComparison comparison = StringComparison.OrdinalIgnoreCase, 
    AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
```

| parameter | description |
| --- | --- |
| playerId | Player ID to get groups for |
| filter | String to filter by |
| comparison | StringComparison to use |
| search | [`AutoCompleteSearchMode`](./AutoComplete/AutoCompleteSearchMode.md) Filter search mode |

## See Also

* enum [AutoCompleteSearchMode](./AutoComplete/AutoCompleteSearchMode.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddGroupsWithoutPlayer method

Adds The List of Groups that playerId has

```csharp
public void AddGroupsWithoutPlayer(string playerId, string filter = null, 
    StringComparison comparison = StringComparison.OrdinalIgnoreCase, 
    AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
```

| parameter | description |
| --- | --- |
| playerId | Player ID to get groups for |
| filter | String to filter by |
| comparison | StringComparison to use |
| search | [`AutoCompleteSearchMode`](./AutoComplete/AutoCompleteSearchMode.md) Filter search mode |

## See Also

* enum [AutoCompleteSearchMode](./AutoComplete/AutoCompleteSearchMode.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddPermissionsPlayerIn method

Adds The List of Permissions that playerId has

```csharp
public void AddPermissionsPlayerIn(string playerId, string filter = null, 
    StringComparison comparison = StringComparison.OrdinalIgnoreCase, 
    AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
```

| parameter | description |
| --- | --- |
| playerId | Player ID to get permissions for |
| filter | String to filter by |
| comparison | StringComparison to use |
| search | [`AutoCompleteSearchMode`](./AutoComplete/AutoCompleteSearchMode.md) Filter search mode |

## See Also

* enum [AutoCompleteSearchMode](./AutoComplete/AutoCompleteSearchMode.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddPermissionsPlayerNotIn method

Adds The List of Permissions that playerId does not have

```csharp
public void AddPermissionsPlayerNotIn(string playerId, string filter = null, 
    StringComparison comparison = StringComparison.OrdinalIgnoreCase, 
    AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
```

| parameter | description |
| --- | --- |
| playerId | Player ID to get permissions for |
| filter | String to filter by |
| comparison | StringComparison to use |
| search | [`AutoCompleteSearchMode`](./AutoComplete/AutoCompleteSearchMode.md) Filter search mode |

## See Also

* enum [AutoCompleteSearchMode](./AutoComplete/AutoCompleteSearchMode.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddOnlinePlayers method

Adds Online Players to the list

```csharp
public void AddOnlinePlayers(string filter = null, PlayerNameFormatter formatter = null)
```

| parameter | description |
| --- | --- |
| filter | String to filter by |
| formatter | Formatter for the player name |

## See Also

* class [PlayerNameFormatter](./AutoComplete/PlayerNameFormatter.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddOfflinePlayers method

Adds Online Players to the list

```csharp
public void AddOfflinePlayers(string filter = null, PlayerNameFormatter formatter = null)
```

| parameter | description |
| --- | --- |
| filter | String to filter by |
| formatter | Formatter for the player name |

## See Also

* class [PlayerNameFormatter](./AutoComplete/PlayerNameFormatter.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddAllOnlineFirstPlayers method

Adds Online Players to the list first If there is still space add Offline Players

```csharp
public void AddAllOnlineFirstPlayers(string filter = null, PlayerNameFormatter formatter = null)
```

| parameter | description |
| --- | --- |
| filter | String to filter by |
| formatter | Formatter for the player name |

## See Also

* class [PlayerNameFormatter](./AutoComplete/PlayerNameFormatter.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddAllPlayers method

Adds Any Player to the list

```csharp
public void AddAllPlayers(string filter = null, PlayerNameFormatter formatter = null)
```

| parameter | description |
| --- | --- |
| filter | String to filter by |
| formatter | Formatter for the player name |

## See Also

* class [PlayerNameFormatter](./AutoComplete/PlayerNameFormatter.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddByPlayerId method

Adds a player by player Id to the list

```csharp
public void AddByPlayerId(string playerId, PlayerNameFormatter formatter = null)
```

| parameter | description |
| --- | --- |
| playerId | Player ID to add |
| formatter | Formatter for the player name |

## See Also

* class [PlayerNameFormatter](./AutoComplete/PlayerNameFormatter.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddLoadablePlugins method

Adds a list of plugins that can be loaded

```csharp
public void AddLoadablePlugins(string filter = null, 
    StringComparison comparison = StringComparison.OrdinalIgnoreCase, 
    AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
```

| parameter | description |
| --- | --- |
| filter | String to filter by |
| comparison | StringComparison to use |
| search | [`AutoCompleteSearchMode`](./AutoComplete/AutoCompleteSearchMode.md) Filter search mode |

## See Also

* enum [AutoCompleteSearchMode](./AutoComplete/AutoCompleteSearchMode.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddLoadedPlugins method

Adds a list of plugins that are currently loaded

```csharp
public void AddLoadedPlugins(string filter = null, 
    StringComparison comparison = StringComparison.OrdinalIgnoreCase, 
    AutoCompleteSearchMode search = AutoCompleteSearchMode.StartsWith)
```

| parameter | description |
| --- | --- |
| filter | String to filter by |
| comparison | StringComparison to use |
| search | [`AutoCompleteSearchMode`](./AutoComplete/AutoCompleteSearchMode.md) Filter search mode |

## See Also

* enum [AutoCompleteSearchMode](./AutoComplete/AutoCompleteSearchMode.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# InteractionAutoCompleteBuilder constructor

Constructor

```csharp
public InteractionAutoCompleteBuilder(DiscordInteraction interaction, 
    InteractionAutoCompleteMessage message = null)
```

| parameter | description |
| --- | --- |
| interaction | Interaction this build is for |
| message | Starting [`InteractionAutoCompleteMessage`](../../Entities/Interactions/Response/InteractionAutoCompleteMessage.md) |

## See Also

* class [DiscordInteraction](../../Entities/Interactions/DiscordInteraction.md)
* class [InteractionAutoCompleteMessage](../../Entities/Interactions/Response/InteractionAutoCompleteMessage.md)
* class [InteractionAutoCompleteBuilder](./InteractionAutoCompleteBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
