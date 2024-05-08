# DiscordPool class

Discord Pool Library

```csharp
public class DiscordPool : BaseDiscordLibrary<DiscordPool>, IDebugLoggable
```

## Public Members

| name | description |
| --- | --- |
| [GetOrCreate](#getorcreate-method)(…) | Returns an existing [`DiscordPluginPool`](../Types/DiscordPluginPool.md) for the given plugin or returns a new one |
| [LogDebug](#logdebug-method)(…) |  |

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
* [DiscordPool.cs](../../../../Oxide.Ext.Discord/Libraries/DiscordPool.cs)
   
   
# GetOrCreate method

Returns an existing [`DiscordPluginPool`](../Types/DiscordPluginPool.md) for the given plugin or returns a new one

```csharp
public DiscordPluginPool GetOrCreate(Plugin plugin)
```

| parameter | description |
| --- | --- |
| plugin | The pool the plugin is for |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if the plugin is null |

## See Also

* class [DiscordPluginPool](../Types/DiscordPluginPool.md)
* class [DiscordPool](./DiscordPool.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnPluginLoaded method

```csharp
protected override void OnPluginLoaded(PluginSetup data, BotConnection connection)
```

## See Also

* class [PluginSetup](../Plugins/PluginSetup.md)
* class [BotConnection](../Connections/BotConnection.md)
* class [DiscordPool](./DiscordPool.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnPluginUnloaded method

```csharp
protected override void OnPluginUnloaded(Plugin plugin)
```

## See Also

* class [DiscordPool](./DiscordPool.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# LogDebug method

```csharp
public void LogDebug(DebugLogger logger)
```

## See Also

* class [DebugLogger](../Logging/DebugLogger.md)
* class [DiscordPool](./DiscordPool.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->