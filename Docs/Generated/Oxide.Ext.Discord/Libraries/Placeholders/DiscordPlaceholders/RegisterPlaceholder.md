# DiscordPlaceholders.RegisterPlaceholder method (1 of 7)

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

* class [DiscordPlaceholders](../DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](../DiscordPlaceholders.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordPlaceholders.RegisterPlaceholder&lt;TResult&gt; method (2 of 7)

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

* class [DiscordPlaceholders](../DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](../DiscordPlaceholders.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordPlaceholders.RegisterPlaceholder&lt;TData&gt; method (3 of 7)

Registers a placeholder

```csharp
public void RegisterPlaceholder<TData>(Plugin plugin, string placeholder, string dataKey)
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

* class [DiscordPlaceholders](../DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](../DiscordPlaceholders.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordPlaceholders.RegisterPlaceholder&lt;TData,TResult&gt; method (4 of 7)

Registers a placeholder of type {T}

```csharp
public void RegisterPlaceholder<TData, TResult>(Plugin plugin, string placeholder, 
    Func<PlaceholderState, TData, TResult> callback)
```

| parameter | description |
| --- | --- |
| TData | Type of the data key |
| plugin | Plugin this placeholder is for |
| placeholder | Placeholder string |
| callback | Callback Method for the placeholder |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException |  |

## See Also

* class [PlaceholderState](../PlaceholderState.md)
* class [DiscordPlaceholders](../DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](../DiscordPlaceholders.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordPlaceholders.RegisterPlaceholder&lt;TData,TResult&gt; method (5 of 7)

Registers a placeholder of type {T}

```csharp
public void RegisterPlaceholder<TData, TResult>(Plugin plugin, string placeholder, 
    Func<TData, TResult> callback)
```

| parameter | description |
| --- | --- |
| TData | Type of the data key |
| plugin | Plugin this placeholder is for |
| placeholder | Placeholder string |
| callback | Callback Method for the placeholder |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException |  |

## See Also

* class [DiscordPlaceholders](../DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](../DiscordPlaceholders.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordPlaceholders.RegisterPlaceholder&lt;TData,TResult&gt; method (6 of 7)

Registers a placeholder of type {T}

```csharp
public void RegisterPlaceholder<TData, TResult>(Plugin plugin, string placeholder, string dataKey, 
    Func<PlaceholderState, TData, TResult> callback)
```

| parameter | description |
| --- | --- |
| TData | Type of the data key |
| plugin | Plugin this placeholder is for |
| placeholder | Placeholder string |
| dataKey | The name of the data key in PlaceholderData |
| callback | Callback Method for the placeholder |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException |  |

## See Also

* class [PlaceholderState](../PlaceholderState.md)
* class [DiscordPlaceholders](../DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](../DiscordPlaceholders.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordPlaceholders.RegisterPlaceholder&lt;TData,TResult&gt; method (7 of 7)

Registers a placeholder of type {T}

```csharp
public void RegisterPlaceholder<TData, TResult>(Plugin plugin, string placeholder, string dataKey, 
    Func<TData, TResult> callback)
```

| parameter | description |
| --- | --- |
| TData | Type of the data key |
| plugin | Plugin this placeholder is for |
| placeholder | Placeholder string |
| dataKey | The name of the data key in PlaceholderData |
| callback | Callback Method for the placeholder |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException |  |

## See Also

* class [DiscordPlaceholders](../DiscordPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders](../DiscordPlaceholders.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
