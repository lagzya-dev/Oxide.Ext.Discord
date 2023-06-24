# BasePool&lt;TPooled,TPool&gt; class

Represents a BasePool in Discord

```csharp
public abstract class BasePool<TPooled, TPool> : IPool<TPooled>
    where TPooled : class
    where TPool : BasePool, new()
```

| parameter | description |
| --- | --- |
| TPooled | Type being pooled |
| TPool | Type of the pool |

## Public Members

| name | description |
| --- | --- |
| [ClearPoolEntities](#ClearPoolEntities)() | Clears the pool of all pooled objects and resets state to when the pool was first created |
| [Free](#Free)(…) | Frees an item back to the pool |
| [Get](#Get)() | Returns an element from the pool if it exists else it creates a new one |
| [OnPluginUnloaded](#OnPluginUnloaded)(…) |  |
| [RemoveAllPools](#RemoveAllPools)() | Wipes all the pools for this type |
| static [ForPlugin](#ForPlugin)(…) | Returns a pool for the given plugin pool |

## Protected Members

| name | description |
| --- | --- |
| [BasePool](#BasePool)() | The default constructor. |
| [PluginPool](#PluginPool) | Plugin Pool for this pool |
| abstract [CreateNew](#CreateNew)() | Creates new type of T |
| abstract [GetPoolSize](#GetPoolSize)(…) | Returns the pool size from the pool settings for the pool |
| virtual [OnFreeItem](#OnFreeItem)(…) | Returns if an item can be freed to the pool |
| virtual [OnGetItem](#OnGetItem)(…) | Called when an item is retrieved from the pool |

## See Also

* interface [IPool&lt;T&gt;](./IPool%7BT%7D.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [BasePool.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Pooling/BasePool.cs)
   
   
# GetPoolSize method

Returns the pool size from the pool settings for the pool

```csharp
protected abstract PoolSize GetPoolSize(PoolSettings settings)
```

| parameter | description |
| --- | --- |
| settings |  |

## See Also

* struct [PoolSize](./PoolSize.md)
* class [PoolSettings](./PoolSettings.md)
* class [BasePool&lt;TPooled,TPool&gt;](./BasePool%7BTPooled,TPool%7D.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ForPlugin method

Returns a pool for the given plugin pool

```csharp
public static TPool ForPlugin(DiscordPluginPool pluginPool)
```

| parameter | description |
| --- | --- |
| pluginPool | [`DiscordPluginPool`](./DiscordPluginPool.md) to get the pool from |

## See Also

* class [DiscordPluginPool](./DiscordPluginPool.md)
* class [BasePool&lt;TPooled,TPool&gt;](./BasePool%7BTPooled,TPool%7D.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Get method

Returns an element from the pool if it exists else it creates a new one

```csharp
public TPooled Get()
```

## See Also

* class [BasePool&lt;TPooled,TPool&gt;](./BasePool%7BTPooled,TPool%7D.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# CreateNew method

Creates new type of T

```csharp
protected abstract TPooled CreateNew()
```

## Return Value

Newly created type of T

## See Also

* class [BasePool&lt;TPooled,TPool&gt;](./BasePool%7BTPooled,TPool%7D.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Free method

Frees an item back to the pool

```csharp
public void Free(TPooled item)
```

| parameter | description |
| --- | --- |
| item | Item being freed |

## See Also

* class [BasePool&lt;TPooled,TPool&gt;](./BasePool%7BTPooled,TPool%7D.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnPluginUnloaded method

```csharp
public void OnPluginUnloaded(DiscordPluginPool pluginPool)
```

## See Also

* class [DiscordPluginPool](./DiscordPluginPool.md)
* class [BasePool&lt;TPooled,TPool&gt;](./BasePool%7BTPooled,TPool%7D.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ClearPoolEntities method

Clears the pool of all pooled objects and resets state to when the pool was first created

```csharp
public void ClearPoolEntities()
```

## See Also

* class [BasePool&lt;TPooled,TPool&gt;](./BasePool%7BTPooled,TPool%7D.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# RemoveAllPools method

Wipes all the pools for this type

```csharp
public void RemoveAllPools()
```

## See Also

* class [BasePool&lt;TPooled,TPool&gt;](./BasePool%7BTPooled,TPool%7D.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnGetItem method

Called when an item is retrieved from the pool

```csharp
protected virtual void OnGetItem(TPooled item)
```

| parameter | description |
| --- | --- |
| item | Item being retrieved |

## See Also

* class [BasePool&lt;TPooled,TPool&gt;](./BasePool%7BTPooled,TPool%7D.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnFreeItem method

Returns if an item can be freed to the pool

```csharp
protected virtual bool OnFreeItem(ref TPooled item)
```

| parameter | description |
| --- | --- |
| item | Item to be freed |

## Return Value

True if can be freed; false otherwise

## See Also

* class [BasePool&lt;TPooled,TPool&gt;](./BasePool%7BTPooled,TPool%7D.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# BasePool&lt;TPooled,TPool&gt; constructor

The default constructor.

```csharp
protected BasePool()
```

## See Also

* class [BasePool&lt;TPooled,TPool&gt;](./BasePool%7BTPooled,TPool%7D.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# PluginPool field

Plugin Pool for this pool

```csharp
protected DiscordPluginPool PluginPool;
```

## See Also

* class [DiscordPluginPool](./DiscordPluginPool.md)
* class [BasePool&lt;TPooled,TPool&gt;](./BasePool%7BTPooled,TPool%7D.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
