# DiscordAppCommand class

Application Command Oxide Library handler Routes Application Commands to their respective hook method handlers instead of having to manually handle it.

```csharp
public class DiscordAppCommand : BaseDiscordLibrary<DiscordAppCommand>, IDebugLoggable
```

## Public Members

| name | description |
| --- | --- |
| [AddApplicationCommand](#AddApplicationCommand-method)(…) | Registers a new Application Command for the given plugin |
| [AddAutoCompleteCommand](#AddAutoCompleteCommand-method)(…) | Registers a new Application Command for the given plugin |
| [AddMessageComponentCommand](#AddMessageComponentCommand-method)(…) | Adds a MessageComponent Command type. This matches CustomId with starts with |
| [AddModalSubmitCommand](#AddModalSubmitCommand-method)(…) | Adds a MessageComponent Command type. This matches CustomId with starts with |
| [LogDebug](#LogDebug-method)(…) |  |
| [RemoveApplicationCommand](#RemoveApplicationCommand-method)(…) | Removes an application command |

## Protected Members

| name | description |
| --- | --- |
| override [OnPluginLoaded](#OnPluginLoaded-method)(…) |  |
| override [OnPluginUnloaded](#OnPluginUnloaded-method)(…) |  |

## See Also

* class [BaseDiscordLibrary&lt;TLibrary&gt;](../BaseDiscordLibrary%7BTLibrary%7D.md)
* interface [IDebugLoggable](../../Interfaces/Logging/IDebugLoggable.md)
* namespace [Oxide.Ext.Discord.Libraries.AppCommands](./AppCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordAppCommand.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Libraries/AppCommands/DiscordAppCommand.cs)
   
   
# AddApplicationCommand method

Registers a new Application Command for the given plugin

```csharp
public void AddApplicationCommand(Plugin plugin, Snowflake applicationId, 
    Action<DiscordInteraction, InteractionDataParsed> callback, string command, 
    string group = null, string subCommand = null)
```

| parameter | description |
| --- | --- |
| plugin | Plugin the Application Command is for |
| applicationId | ID of the [`DiscordApplication`](../../Entities/Applications/DiscordApplication.md) for the command |
| callback | Callback for the command |
| command | Command name |
| group | Sub Command Group for the command |
| subCommand | Sub Command for the command |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if inputs are null |

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* class [DiscordInteraction](../../Entities/Interactions/DiscordInteraction.md)
* class [InteractionDataParsed](../../Entities/Interactions/InteractionDataParsed.md)
* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.AppCommands](./AppCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddAutoCompleteCommand method

Registers a new Application Command for the given plugin

```csharp
public void AddAutoCompleteCommand(Plugin plugin, Snowflake applicationId, 
    Action<DiscordInteraction, InteractionDataOption> callback, string command, string argument, 
    string group = null, string subCommand = null)
```

| parameter | description |
| --- | --- |
| plugin | Plugin the Application Command is for |
| applicationId | ID of [`DiscordApplication`](../../Entities/Applications/DiscordApplication.md) For the command |
| callback | Callback for the command |
| command | Command name |
| argument | Command Argument name for the Auto Complete |
| group | Sub Command Group for the command |
| subCommand | Sub Command for the command |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if inputs are null |

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* class [DiscordInteraction](../../Entities/Interactions/DiscordInteraction.md)
* class [InteractionDataOption](../../Entities/Interactions/InteractionDataOption.md)
* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.AppCommands](./AppCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddMessageComponentCommand method

Adds a MessageComponent Command type. This matches CustomId with starts with

```csharp
public void AddMessageComponentCommand(Plugin plugin, Snowflake applicationId, string customId, 
    Action<DiscordInteraction> callback)
```

| parameter | description |
| --- | --- |
| plugin | Plugin the command is for |
| applicationId | ID of [`DiscordApplication`](../../Entities/Applications/DiscordApplication.md) for the command |
| customId | Command to match with Starts with |
| callback | Callback for the command |

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* class [DiscordInteraction](../../Entities/Interactions/DiscordInteraction.md)
* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.AppCommands](./AppCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddModalSubmitCommand method

Adds a MessageComponent Command type. This matches CustomId with starts with

```csharp
public void AddModalSubmitCommand(Plugin plugin, Snowflake applicationId, string customId, 
    Action<DiscordInteraction> callback)
```

| parameter | description |
| --- | --- |
| plugin | Plugin the command is for |
| applicationId | ID of [`DiscordApplication`](../../Entities/Applications/DiscordApplication.md) for the command |
| customId | Command to match with Starts with |
| callback | Callback for the command |

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* class [DiscordInteraction](../../Entities/Interactions/DiscordInteraction.md)
* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.AppCommands](./AppCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RemoveApplicationCommand method

Removes an application command

```csharp
public void RemoveApplicationCommand(Plugin plugin, DiscordApplication app, InteractionType type, 
    string command, string group, string subCommand)
```

| parameter | description |
| --- | --- |
| plugin | Plugin to remove the command for |
| app | [`DiscordApplication`](../../Entities/Applications/DiscordApplication.md) for the command |
| type | Type of the command |
| command | Command name |
| group | Sub Command Group for the command |
| subCommand | Sub Command for the command |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if command is null or empty |

## See Also

* class [DiscordApplication](../../Entities/Applications/DiscordApplication.md)
* enum [InteractionType](../../Entities/Interactions/InteractionType.md)
* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.AppCommands](./AppCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# OnPluginLoaded method

```csharp
protected override void OnPluginLoaded(PluginSetup data, BotConnection connection)
```

## See Also

* class [PluginSetup](../../Plugins/Setup/PluginSetup.md)
* class [BotConnection](../../Connections/BotConnection.md)
* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.AppCommands](./AppCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# OnPluginUnloaded method

```csharp
protected override void OnPluginUnloaded(Plugin plugin)
```

## See Also

* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.AppCommands](./AppCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# LogDebug method

```csharp
public void LogDebug(DebugLogger logger)
```

## See Also

* class [DebugLogger](../../Logging/DebugLogger.md)
* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.AppCommands](./AppCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
