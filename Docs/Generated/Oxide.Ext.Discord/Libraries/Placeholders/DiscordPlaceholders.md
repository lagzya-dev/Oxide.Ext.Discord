# DiscordPlaceholders class

Discord Placeholders Library

```csharp
public class DiscordPlaceholders : BaseDiscordLibrary<DiscordPlaceholders>
```

## Public Members

| name | description |
| --- | --- |
| [CreateData](#createdata-method)(…) | Creates Pooled [`PlaceholderData`](./PlaceholderData.md) |
| [ProcessPlaceholders](#processplaceholders-method)(…) | Process placeholders for the given Text |
| [RegisterPlaceholder](#registerplaceholder-method)(…) | Registers a placeholder static value placeholder |
| [RegisterPlaceholder&lt;TResult&gt;](#registerplaceholder&amp;lt;tresult&amp;gt;-method)(…) | Registers a placeholder |
| [RegisterPlaceholder&lt;TData&gt;](#registerplaceholder&amp;lt;tdata&amp;gt;-method)(…) | Registers a placeholder that uses the dataKey value |
| [RegisterPlaceholder&lt;TData,TResult&gt;](#registerplaceholder&amp;lt;tdata,tresult&amp;gt;-method-1-of-4))(…) | Registers a placeholder of type {T} (4 methods) |

## Protected Members

| name | description |
| --- | --- |
| override [OnPluginUnloaded](#onpluginunloaded-method)(…) |  |

## See Also

* class [BaseDiscordLibrary&lt;TLibrary&gt;](../BaseDiscordLibrary%7BTLibrary%7D.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordPlaceholders.cs](../../../../Oxide.Ext.Discord/Libraries/Placeholders/DiscordPlaceholders.cs)
   
   
# ProcessPlaceholders method

Process placeholders for the given Text

```csharp
public string ProcessPlaceholders(string text, PlaceholderData data)
```

| parameter | description |
| --- | --- |
| text | Text to process placeholders for |
| data | Placeholder Data for the placeholders |

## Return Value

string with placeholders replaced. If no placeholders are found the original string is returned

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* class [DiscordPlaceholders](./DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# CreateData method

Creates Pooled [`PlaceholderData`](./PlaceholderData.md)

```csharp
public PlaceholderData CreateData(Plugin plugin)
```

## Return Value

[`PlaceholderData`](./PlaceholderData.md)

## See Also

* class [PlaceholderData](./PlaceholderData.md)
* class [DiscordPlaceholders](./DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RegisterPlaceholder method (1 of 7)

Registers a placeholder static value placeholder

```csharp
public void RegisterPlaceholder(Plugin plugin, string placeholder, string value)
```

| parameter | description |
| --- | --- |
| plugin | Plugin this placeholder is for |
| placeholder | Placeholder string |
| value | Static string value |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException |  |

## See Also

* class [DiscordPlaceholders](./DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# RegisterPlaceholder&lt;TResult&gt; method (2 of 7)

Registers a placeholder

```csharp
public void RegisterPlaceholder<TResult>(Plugin plugin, string placeholder, Func<TResult> callback)
```

| parameter | description |
| --- | --- |
| plugin | Plugin this placeholder is for |
| placeholder | Placeholder string |
| callback | Callback Method for the placeholder |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException |  |

## See Also

* class [DiscordPlaceholders](./DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# RegisterPlaceholder&lt;TData&gt; method (3 of 7)

Registers a placeholder that uses the dataKey value

```csharp
public void RegisterPlaceholder<TData>(Plugin plugin, string placeholder, string dataKey)
```

| parameter | description |
| --- | --- |
| TData | Type that is registered in the PlaceholderData |
| plugin | Plugin this placeholder is for |
| placeholder | Placeholder string |
| dataKey |  |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if placeholder or plugin is null |

## See Also

* class [DiscordPlaceholders](./DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# RegisterPlaceholder&lt;TData,TResult&gt; method (4 of 7)

Registers a placeholder of type {T}

```csharp
public void RegisterPlaceholder<TData, TResult>(Plugin plugin, string placeholder, 
    Func<PlaceholderState, TData, TResult> callback)
```

| parameter | description |
| --- | --- |
| TData | Type of the data key |
| TResult | The return type of the placeholder callback |
| plugin | Plugin this placeholder is for |
| placeholder | Placeholder string |
| callback | Callback Method for the placeholder |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException |  |

## See Also

* class [PlaceholderState](./PlaceholderState.md)
* class [DiscordPlaceholders](./DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# RegisterPlaceholder&lt;TData,TResult&gt; method (5 of 7)

Registers a placeholder of type {T}

```csharp
public void RegisterPlaceholder<TData, TResult>(Plugin plugin, string placeholder, 
    Func<TData, TResult> callback)
```

| parameter | description |
| --- | --- |
| TData | Type of the data key |
| TResult | The return type of the placeholder callback |
| plugin | Plugin this placeholder is for |
| placeholder | Placeholder string |
| callback | Callback Method for the placeholder |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException |  |

## See Also

* class [DiscordPlaceholders](./DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# RegisterPlaceholder&lt;TData,TResult&gt; method (6 of 7)

Registers a placeholder of type {T}

```csharp
public void RegisterPlaceholder<TData, TResult>(Plugin plugin, string placeholder, string dataKey, 
    Func<PlaceholderState, TData, TResult> callback)
```

| parameter | description |
| --- | --- |
| TData | Type of the data key |
| TResult | The return type of the placeholder callback |
| plugin | Plugin this placeholder is for |
| placeholder | Placeholder string |
| dataKey | The name of the data key in PlaceholderData |
| callback | Callback Method for the placeholder |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException |  |

## See Also

* class [PlaceholderState](./PlaceholderState.md)
* class [DiscordPlaceholders](./DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---

# RegisterPlaceholder&lt;TData,TResult&gt; method (7 of 7)

Registers a placeholder of type {T}

```csharp
public void RegisterPlaceholder<TData, TResult>(Plugin plugin, string placeholder, string dataKey, 
    Func<TData, TResult> callback)
```

| parameter | description |
| --- | --- |
| TData | Type of the data key |
| TResult | The return type of the placeholder callback |
| plugin | Plugin this placeholder is for |
| placeholder | Placeholder string |
| dataKey | The name of the data key in PlaceholderData |
| callback | Callback Method for the placeholder |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException |  |

## See Also

* class [DiscordPlaceholders](./DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# OnPluginUnloaded method

```csharp
protected override void OnPluginUnloaded(Plugin plugin)
```

## See Also

* class [DiscordPlaceholders](./DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](./PlaceholdersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
