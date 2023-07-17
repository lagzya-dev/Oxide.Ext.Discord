# TimestampPlaceholders class

Timestamp placeholders

```csharp
public static class TimestampPlaceholders
```

## Public Members

| name | description |
| --- | --- |
| static [LongDate](#longdate-method)(…) | [`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder |
| static [LongDateTime](#longdatetime-method)(…) | [`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder |
| static [Longtime](#longtime-method)(…) | [`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder |
| static [RegisterPlaceholders](#registerplaceholders-method)(…) | Registers placeholders for the given plugin. |
| static [RelativeTime](#relativetime-method)(…) | [`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder |
| static [ShortDate](#shortdate-method)(…) | [`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder |
| static [ShortDateTime](#shortdatetime-method)(…) | [`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder |
| static [ShortTime](#shorttime-method)(…) | [`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder |
| static [Timestamp](#timestamp-method)(…) | [`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder |

## See Also

* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [TimestampPlaceholders.cs](../../../../Oxide.Ext.Discord/Libraries/Placeholders/Default/TimestampPlaceholders.cs)
   
   
# Timestamp method

[`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder

```csharp
public static string Timestamp(long timestamp)
```

## See Also

* class [TimestampPlaceholders](./TimestampPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ShortTime method

[`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder

```csharp
public static string ShortTime(long timestamp)
```

## See Also

* class [TimestampPlaceholders](./TimestampPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Longtime method

[`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder

```csharp
public static string Longtime(long timestamp)
```

## See Also

* class [TimestampPlaceholders](./TimestampPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ShortDate method

[`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder

```csharp
public static string ShortDate(long timestamp)
```

## See Also

* class [TimestampPlaceholders](./TimestampPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# LongDate method

[`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder

```csharp
public static string LongDate(long timestamp)
```

## See Also

* class [TimestampPlaceholders](./TimestampPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ShortDateTime method

[`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder

```csharp
public static string ShortDateTime(long timestamp)
```

## See Also

* class [TimestampPlaceholders](./TimestampPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# LongDateTime method

[`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder

```csharp
public static string LongDateTime(long timestamp)
```

## See Also

* class [TimestampPlaceholders](./TimestampPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# RelativeTime method

[`UnixTimestamp`](../../../Helpers/DiscordFormatting.md#unixtimestamp-method) placeholder

```csharp
public static string RelativeTime(long timestamp)
```

## See Also

* class [TimestampPlaceholders](./TimestampPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# RegisterPlaceholders method

Registers placeholders for the given plugin.

```csharp
public static void RegisterPlaceholders(Plugin plugin, TimestampKeys keys, 
    PlaceholderDataKey dataKey)
```

| parameter | description |
| --- | --- |
| plugin | Plugin to register placeholders for |
| keys | Prefix to use for the placeholders |
| dataKey | Data key in [`PlaceholderData`](../PlaceholderData.md) |

## See Also

* class [TimestampKeys](../Keys/TimestampKeys.md)
* struct [PlaceholderDataKey](../PlaceholderDataKey.md)
* class [TimestampPlaceholders](./TimestampPlaceholders.md)
* namespace [Oxide.Ext.Discord.Libraries.Placeholders.Default](./DefaultNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
