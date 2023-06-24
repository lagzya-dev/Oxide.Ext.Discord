# DiscordPluginPool class

Built in pooling for discord entities

```csharp
public class DiscordPluginPool
```

## Public Members

| name | description |
| --- | --- |
| [DiscordPluginPool](#DiscordPluginPool-constructor)(…) | Constructor |
| [FreeHash&lt;TKey,TValue&gt;](#FreeHash-method)(…) | Frees a pooled Hash |
| [FreeHashSet&lt;T&gt;](#FreeHashSet-method)(…) | Free's a pooled HashSet |
| [FreeList&lt;T&gt;](#FreeList-method)(…) | Free's a pooled List |
| [FreeMemoryStream](#FreeMemoryStream-method)(…) | Frees a MemoryStream back to the pool |
| [FreePlaceholderData](#FreePlaceholderData-method)(…) | Frees a [`PlaceholderData`](../Libraries/Placeholders/PlaceholderData.md) back to the pool |
| [FreeStringBuilder](#FreeStringBuilder-method)(…) | Frees a StringBuilder back to the pool |
| [FreeStringBuilderToString](#FreeStringBuilderToString-method)(…) | Frees a StringBuilder back to the pool returning the built String |
| [Get&lt;T&gt;](#Get-method)() | Returns a pooled object of {T} type Must inherit from [`BasePoolable`](./BasePoolable.md) and have an empty default constructor |
| [GetHash&lt;TKey,TValue&gt;](#GetHash-method)() | Returns a pooled Hash |
| [GetHashSet&lt;T&gt;](#GetHashSet-method)() | Returns a pooled HashSet |
| [GetList&lt;T&gt;](#GetList-method)() | Returns a pooled List |
| [GetMemoryStream](#GetMemoryStream-method)() | Returns a pooled MemoryStream |
| [GetPlaceholderData](#GetPlaceholderData-method)() | Returns a pooled [`PlaceholderData`](../Libraries/Placeholders/PlaceholderData.md) |
| [GetStringBuilder](#GetStringBuilder-method)() | Returns a pooled StringBuilder |
| [GetStringBuilder](#GetStringBuilder-method)(…) | Returns a pooled StringBuilder |
| [SetSettings](#SetSettings-method)(…) | Sets the settings for the pools |

## See Also

* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [DiscordPluginPool.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Pooling/DiscordPluginPool.cs)
   
   
# SetSettings method

Sets the settings for the pools

```csharp
public void SetSettings(PoolSettings settings)
```

| parameter | description |
| --- | --- |
| settings |  |

## See Also

* class [PoolSettings](./PoolSettings.md)
* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Get&lt;T&gt; method

Returns a pooled object of {T} type Must inherit from [`BasePoolable`](./BasePoolable.md) and have an empty default constructor

```csharp
public T Get<T>()
    where T : BasePoolable, new()
```

| parameter | description |
| --- | --- |
| T | Type to be returned |

## Return Value

Pooled object of type T

## See Also

* class [BasePoolable](./BasePoolable.md)
* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetList&lt;T&gt; method

Returns a pooled List

```csharp
public List<T> GetList<T>()
```

| parameter | description |
| --- | --- |
| T | Type for the list |

## Return Value

Pooled List

## See Also

* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# FreeList&lt;T&gt; method

Free's a pooled List

```csharp
public void FreeList<T>(List<T> list)
```

| parameter | description |
| --- | --- |
| T | Type of the list |
| list | List to be freed |

## See Also

* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetHash&lt;TKey,TValue&gt; method

Returns a pooled Hash

```csharp
public Hash<TKey, TValue> GetHash<TKey, TValue>()
```

| parameter | description |
| --- | --- |
| TKey | Type for the key |
| TValue | Type for the value |

## Return Value

Pooled Hash

## See Also

* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# FreeHash&lt;TKey,TValue&gt; method

Frees a pooled Hash

```csharp
public void FreeHash<TKey, TValue>(Hash<TKey, TValue> hash)
```

| parameter | description |
| --- | --- |
| TKey | Type for key |
| TValue | Type for value |
| hash | Hash to be freed |

## See Also

* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetHashSet&lt;T&gt; method

Returns a pooled HashSet

```csharp
public HashSet<T> GetHashSet<T>()
```

| parameter | description |
| --- | --- |
| T | Type for the HashSet |

## Return Value

Pooled List

## See Also

* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# FreeHashSet&lt;T&gt; method

Free's a pooled HashSet

```csharp
public void FreeHashSet<T>(HashSet<T> list)
```

| parameter | description |
| --- | --- |
| T | Type of the HashSet |
| list | HashSet to be freed |

## See Also

* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetStringBuilder method (1 of 2)

Returns a pooled StringBuilder

```csharp
public StringBuilder GetStringBuilder()
```

## Return Value

Pooled StringBuilder

## See Also

* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# FreeStringBuilder method

Frees a StringBuilder back to the pool

```csharp
public void FreeStringBuilder(StringBuilder sb)
```

| parameter | description |
| --- | --- |
| sb | StringBuilder being freed |

## See Also

* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# FreeStringBuilderToString method

Frees a StringBuilder back to the pool returning the built String

```csharp
public string FreeStringBuilderToString(StringBuilder sb)
```

| parameter | description |
| --- | --- |
| sb | StringBuilder being freed |

## See Also

* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetMemoryStream method

Returns a pooled MemoryStream

```csharp
public MemoryStream GetMemoryStream()
```

## Return Value

Pooled MemoryStream

## See Also

* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# FreeMemoryStream method

Frees a MemoryStream back to the pool

```csharp
public void FreeMemoryStream(MemoryStream stream)
```

| parameter | description |
| --- | --- |
| stream | MemoryStream being freed |

## See Also

* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetPlaceholderData method

Returns a pooled [`PlaceholderData`](../Libraries/Placeholders/PlaceholderData.md)

```csharp
public PlaceholderData GetPlaceholderData()
```

## Return Value

Pooled [`PlaceholderData`](../Libraries/Placeholders/PlaceholderData.md)

## See Also

* class [PlaceholderData](../Libraries/Placeholders/PlaceholderData.md)
* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# FreePlaceholderData method

Frees a [`PlaceholderData`](../Libraries/Placeholders/PlaceholderData.md) back to the pool

```csharp
public void FreePlaceholderData(PlaceholderData data)
```

| parameter | description |
| --- | --- |
| data | [`PlaceholderData`](../Libraries/Placeholders/PlaceholderData.md) being freed |

## See Also

* class [PlaceholderData](../Libraries/Placeholders/PlaceholderData.md)
* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DiscordPluginPool constructor

Constructor

```csharp
public DiscordPluginPool(Plugin plugin)
```

| parameter | description |
| --- | --- |
| plugin | Plugin the pool is for |

## See Also

* class [DiscordPluginPool](./DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](./PoolingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
