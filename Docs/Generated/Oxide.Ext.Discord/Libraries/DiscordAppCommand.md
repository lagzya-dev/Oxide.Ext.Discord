# DiscordAppCommand class

Application Command Oxide Library handler Routes Application Commands to their respective hook method handlers instead of having to manually handle it.

```csharp
public class DiscordAppCommand : BaseDiscordLibrary<DiscordAppCommand>, IDebugLoggable
```

## Public Members

| name | description |
| --- | --- |
| [AddApplicationCommand](#addapplicationcommand-method)(…) | Registers a new Application Command for the given plugin |
| [AddAutoCompleteCommand](#addautocompletecommand-method)(…) | Registers a new Application Command for the given plugin |
| [AddMessageComponentCommand](#addmessagecomponentcommand-method)(…) | Adds a MessageComponent Command type. This matches CustomId with starts with |
| [AddModalSubmitCommand](#addmodalsubmitcommand-method)(…) | Adds a MessageComponent Command type. This matches CustomId with starts with |
| [LogDebug](#logdebug-method)(…) |  |
| [RemoveApplicationCommand](#removeapplicationcommand-method)(…) | Removes an application command |

## Protected Members

| name | description |
| --- | --- |
| override [OnPluginLoaded](#onpluginloaded-method)(…) |  |
| override [OnPluginUnloaded](#onpluginunloaded-method)(…) |  |

## See Also

* class [BaseDiscordLibrary&lt;TLibrary&gt;](./BaseDiscordLibrary%7BTLibrary%7D.md)
* interface [IDebugLoggable](../Interfaces/IDebugLoggable.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [DiscordAppCommand.cs](../../../../Oxide.Ext.Discord/Libraries/DiscordAppCommand.cs)
   
   
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
| applicationId | ID of the [`DiscordApplication`](../Entities/DiscordApplication.md) for the command |
| callback | Callback for the command |
| command | Command name |
| group | Sub Command Group for the command |
| subCommand | Sub Command for the command |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if inputs are null |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* class [DiscordInteraction](../Entities/DiscordInteraction.md)
* class [InteractionDataParsed](../Entities/InteractionDataParsed.md)
* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
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
| applicationId | ID of [`DiscordApplication`](../Entities/DiscordApplication.md) For the command |
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

* struct [Snowflake](../Entities/Snowflake.md)
* class [DiscordInteraction](../Entities/DiscordInteraction.md)
* class [InteractionDataOption](../Entities/InteractionDataOption.md)
* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddMessageComponentCommand method

Adds a MessageComponent Command type. This matches CustomId with starts with

```csharp
public void AddMessageComponentCommand(Plugin plugin, Snowflake applicationId, string customId, 
    Action<DiscordInteraction> callback)
```

| parameter | description |
| --- | --- |
| plugin | Plugin the command is for |
| applicationId | ID of [`DiscordApplication`](../Entities/DiscordApplication.md) for the command |
| customId | Command to match with Starts with |
| callback | Callback for the command |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* class [DiscordInteraction](../Entities/DiscordInteraction.md)
* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddModalSubmitCommand method

Adds a MessageComponent Command type. This matches CustomId with starts with

```csharp
public void AddModalSubmitCommand(Plugin plugin, Snowflake applicationId, string customId, 
    Action<DiscordInteraction> callback)
```

| parameter | description |
| --- | --- |
| plugin | Plugin the command is for |
| applicationId | ID of [`DiscordApplication`](../Entities/DiscordApplication.md) for the command |
| customId | Command to match with Starts with |
| callback | Callback for the command |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* class [DiscordInteraction](../Entities/DiscordInteraction.md)
* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# RemoveApplicationCommand method

Removes an application command

```csharp
public void RemoveApplicationCommand(Plugin plugin, DiscordApplication app, InteractionType type, 
    string command, string group, string subCommand)
```

| parameter | description |
| --- | --- |
| plugin | Plugin to remove the command for |
| app | [`DiscordApplication`](../Entities/DiscordApplication.md) for the command |
| type | Type of the command |
| command | Command name |
| group | Sub Command Group for the command |
| subCommand | Sub Command for the command |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if command is null or empty |

## See Also

* class [DiscordApplication](../Entities/DiscordApplication.md)
* enum [InteractionType](../Entities/InteractionType.md)
* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnPluginLoaded method

```csharp
protected override void OnPluginLoaded(PluginSetup data, BotConnection connection)
```

## See Also

* class [PluginSetup](../Plugins/PluginSetup.md)
* class [BotConnection](../Connections/BotConnection.md)
* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnPluginUnloaded method

```csharp
protected override void OnPluginUnloaded(Plugin plugin)
```

## See Also

* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# LogDebug method

```csharp
public void LogDebug(DebugLogger logger)
```

## See Also

* class [DebugLogger](../Logging/DebugLogger.md)
* class [DiscordAppCommand](./DiscordAppCommand.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
