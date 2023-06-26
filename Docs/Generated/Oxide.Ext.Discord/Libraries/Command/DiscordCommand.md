# DiscordCommand class

Represents a library for discord commands

```csharp
[Obsolete("DiscordCommand is deprecated and will be removed in a future update. Please upgrade to DiscordAppCommand")]
public class DiscordCommand : BaseDiscordLibrary<DiscordCommand>, IDebugLoggable
```

## Public Members

| name | description |
| --- | --- |
| readonly [CommandPrefixes](#commandprefixes-field) | Available command prefixes used by the extension |
| [AddDirectMessageCommand](#adddirectmessagecommand-method)(…) | Adds a discord direct message command Sourced From Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L134) |
| [AddDirectMessageLocalizedCommand](#adddirectmessagelocalizedcommand-method)(…) | Adds a localized discord direct message command Sourced from Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L123) |
| [AddGuildCommand](#addguildcommand-method)(…) | Adds a discord guild command Sourced From Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L134) |
| [AddGuildLocalizedCommand](#addguildlocalizedcommand-method)(…) | Adds a localized discord guild command Sourced from Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L123) |
| [HasCommands](#hascommands-method)() | Returns if there are any guild discord commands are registered |
| [HasDirectMessageCommands](#hasdirectmessagecommands-method)() | Returns if there are any guild discord commands are registered |
| [HasGuildCommands](#hasguildcommands-method)() | Returns if there are any guild discord commands are registered |
| [LogDebug](#logdebug-method)(…) |  |
| [RemoveDiscordCommand](#removediscordcommand-method)(…) | Removes a previously registered discord command Sourced From Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L286) |

## Protected Members

| name | description |
| --- | --- |
| override [OnPluginLoaded](#onpluginloaded-method)(…) |  |
| override [OnPluginUnloaded](#onpluginunloaded-method)(…) | Called when a plugin has been unloaded |

## See Also

* class [BaseDiscordLibrary&lt;TLibrary&gt;](../BaseDiscordLibrary%7BTLibrary%7D.md)
* interface [IDebugLoggable](../../Interfaces/Logging/IDebugLoggable.md)
* namespace [Oxide.Ext.Discord.Libraries.Command](./CommandNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordCommand.cs](../../../../Oxide.Ext.Discord/Libraries/Command/DiscordCommand.cs)
   
   
# HasCommands method

Returns if there are any guild discord commands are registered

```csharp
public bool HasCommands()
```

## See Also

* class [DiscordCommand](./DiscordCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.Command](./CommandNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# HasDirectMessageCommands method

Returns if there are any guild discord commands are registered

```csharp
public bool HasDirectMessageCommands()
```

## See Also

* class [DiscordCommand](./DiscordCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.Command](./CommandNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# HasGuildCommands method

Returns if there are any guild discord commands are registered

```csharp
public bool HasGuildCommands()
```

## See Also

* class [DiscordCommand](./DiscordCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.Command](./CommandNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddDirectMessageLocalizedCommand method

Adds a localized discord direct message command Sourced from Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L123)

```csharp
public void AddDirectMessageLocalizedCommand(string langKey, Plugin plugin, string callback)
```

| parameter | description |
| --- | --- |
| langKey | Lang Key on the plugin that contains the command |
| plugin | Plugin to add the localized command for |
| callback | Method name of the callback |

## See Also

* class [DiscordCommand](./DiscordCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.Command](./CommandNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddDirectMessageCommand method

Adds a discord direct message command Sourced From Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L134)

```csharp
public void AddDirectMessageCommand(string command, Plugin plugin, string callback)
```

| parameter | description |
| --- | --- |
| command | Command to add |
| plugin | Plugin to add the command for |
| callback | Method name of the callback |

## See Also

* class [DiscordCommand](./DiscordCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.Command](./CommandNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddGuildLocalizedCommand method

Adds a localized discord guild command Sourced from Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L123)

```csharp
public void AddGuildLocalizedCommand(string langKey, Plugin plugin, 
    List<Snowflake> allowedChannels, string callback)
```

| parameter | description |
| --- | --- |
| langKey | Lang Key on the plugin that contains the command |
| plugin | Plugin to add the localized command for |
| allowedChannels | Channel or Category Id's this command is allowed in |
| callback | Method name of the callback |

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* class [DiscordCommand](./DiscordCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.Command](./CommandNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddGuildCommand method

Adds a discord guild command Sourced From Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L134)

```csharp
public void AddGuildCommand(string command, Plugin plugin, List<Snowflake> allowedChannels, 
    string callback)
```

| parameter | description |
| --- | --- |
| command | Name of the command |
| plugin | Plugin to add the localized command for |
| allowedChannels | Channel or Category Id's this command is allowed in |
| callback | Method name of the callback |

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* class [DiscordCommand](./DiscordCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.Command](./CommandNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RemoveDiscordCommand method

Removes a previously registered discord command Sourced From Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L286)

```csharp
public void RemoveDiscordCommand(string command, Plugin plugin)
```

| parameter | description |
| --- | --- |
| command | Command to remove |
| plugin | Plugin the command is for |

## See Also

* class [DiscordCommand](./DiscordCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.Command](./CommandNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# OnPluginLoaded method

```csharp
protected override void OnPluginLoaded(PluginSetup data, BotConnection connection)
```

## See Also

* class [PluginSetup](../../Plugins/Setup/PluginSetup.md)
* class [BotConnection](../../Connections/BotConnection.md)
* class [DiscordCommand](./DiscordCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.Command](./CommandNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# OnPluginUnloaded method

Called when a plugin has been unloaded

```csharp
protected override void OnPluginUnloaded(Plugin sender)
```

| parameter | description |
| --- | --- |
| sender |  |

## See Also

* class [DiscordCommand](./DiscordCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.Command](./CommandNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# LogDebug method

```csharp
public void LogDebug(DebugLogger logger)
```

## See Also

* class [DebugLogger](../../Logging/DebugLogger.md)
* class [DiscordCommand](./DiscordCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.Command](./CommandNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# CommandPrefixes field

Available command prefixes used by the extension

```csharp
public readonly char[] CommandPrefixes;
```

## See Also

* class [DiscordCommand](./DiscordCommand.md)
* namespace [Oxide.Ext.Discord.Libraries.Command](./CommandNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
