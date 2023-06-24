# DebugLogger class

Debug Logger used for logging debug information

```csharp
public class DebugLogger
```

## Public Members

| name | description |
| --- | --- |
| [DebugLogger](#DebugLogger-constructor)() | The default constructor. |
| [AppendChannelPath](#AppendChannelPath-method)(…) | Appends a channel path to the logger. This path will include the guild name / Parent Channel Name (Optional) / Channel Name |
| [AppendField](#AppendField-method)(…) | Appends a field into the logger (11 methods) |
| [AppendFieldEnum&lt;T&gt;](#AppendFieldEnum-method)(…) | Appends a field with the given name and enum value |
| [AppendFieldOutOf](#AppendFieldOutOf-method)(…) | Appends a field with the given name and int amount over int total value |
| [AppendFieldPrefix](#AppendFieldPrefix-method)(…) | Appends the field name into the logger |
| [AppendIndent](#AppendIndent-method)() | Appends the current indent into the logger |
| [AppendLine](#AppendLine-method)() | Appends a line to the logger |
| [AppendLine](#AppendLine-method)(…) | Appends a line to the logger with the given character repeated amount time (2 methods) |
| [AppendList](#AppendList-method)(…) | Appends an IEnumerable where T is string items to add to the logger (2 methods) |
| [AppendList&lt;T&gt;](#AppendList-method)(…) | Appends an IEnumerable where T is [`IDebugLoggable`](../Interfaces/Logging/IDebugLoggable.md) items to add to the logger (2 methods) |
| [AppendMethod](#AppendMethod-method)(…) | Appends a field with the given name and method info |
| [AppendNullField](#AppendNullField-method)(…) | Appends a field with the given name and Null value |
| [AppendObject](#AppendObject-method)(…) | Appends a [`IDebugLoggable`](../Interfaces/Logging/IDebugLoggable.md) object to the logger with the given name |
| [DecrementIndent](#DecrementIndent-method)() | Decrements the Indent |
| [EndArray](#EndArray-method)() | Ends an array on the logger |
| [EndObject](#EndObject-method)() | Ends an object on the logger |
| [IncrementIndent](#IncrementIndent-method)() | Increments the Indent |
| [StartArray](#StartArray-method)(…) | Starts an array on the logger with the given name |
| [StartObject](#StartObject-method)(…) | Starts an object on the logger with the given name |
| override [ToString](#ToString-method)() | Returns the logged data as a string |

## See Also

* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [DebugLogger.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Logging/DebugLogger.cs)
   
   
# IncrementIndent method

Increments the Indent

```csharp
public void IncrementIndent()
```

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DecrementIndent method

Decrements the Indent

```csharp
public void DecrementIndent()
```

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AppendIndent method

Appends the current indent into the logger

```csharp
public void AppendIndent()
```

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AppendFieldPrefix method

Appends the field name into the logger

```csharp
public void AppendFieldPrefix(string name)
```

| parameter | description |
| --- | --- |
| name | Name of the field |

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AppendField method (1 of 11)

Appends a field with the given name and bool value

```csharp
public void AppendField(string name, bool value)
```

| parameter | description |
| --- | --- |
| name | Name of the field |
| value | Value of the field |

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# AppendFieldEnum&lt;T&gt; method

Appends a field with the given name and enum value

```csharp
public void AppendFieldEnum<T>(string name, T value)
    where T : struct, IComparable, IFormattable, IConvertible
```

| parameter | description |
| --- | --- |
| name | Name of the field |
| value | Value of the field |

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AppendFieldOutOf method

Appends a field with the given name and int amount over int total value

```csharp
public void AppendFieldOutOf(string name, int amount, int total)
```

| parameter | description |
| --- | --- |
| name | Name of the field |
| amount | Amount / Top value |
| total | Total / Bottom Value |

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AppendMethod method

Appends a field with the given name and method info

```csharp
public void AppendMethod(string name, MethodInfo info)
```

| parameter | description |
| --- | --- |
| name | Name of the field |
| info | Method info to append |

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AppendNullField method

Appends a field with the given name and Null value

```csharp
public void AppendNullField(string name)
```

| parameter | description |
| --- | --- |
| name | Name of the field |

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AppendLine method (1 of 3)

Appends a line to the logger

```csharp
public void AppendLine()
```

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# AppendChannelPath method

Appends a channel path to the logger. This path will include the guild name / Parent Channel Name (Optional) / Channel Name

```csharp
public void AppendChannelPath(string name, DiscordGuild guild, DiscordChannel channel, 
    DiscordChannel parent = null)
```

| parameter | description |
| --- | --- |
| name | Name of the field |
| guild | Guild for the name |
| channel | Channel for the channel name |
| parent | Parent for the Parent Channel Name |

## See Also

* class [DiscordGuild](../Entities/Guilds/DiscordGuild.md)
* class [DiscordChannel](../Entities/Channels/DiscordChannel.md)
* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AppendObject method

Appends a [`IDebugLoggable`](../Interfaces/Logging/IDebugLoggable.md) object to the logger with the given name

```csharp
public void AppendObject(string name, IDebugLoggable obj)
```

| parameter | description |
| --- | --- |
| name | Name of the object |
| obj | Object to be logged |

## See Also

* interface [IDebugLoggable](../Interfaces/Logging/IDebugLoggable.md)
* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AppendList method (1 of 4)

Appends an IEnumerable where T is string items to add to the logger

```csharp
public void AppendList(string name, IEnumerable<string> items)
```

| parameter | description |
| --- | --- |
| name | Name of the list |
| items | String items to add |

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# StartArray method

Starts an array on the logger with the given name

```csharp
public void StartArray(string name)
```

| parameter | description |
| --- | --- |
| name | Name of the array |

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# EndArray method

Ends an array on the logger

```csharp
public void EndArray()
```

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# StartObject method

Starts an object on the logger with the given name

```csharp
public void StartObject(string name)
```

| parameter | description |
| --- | --- |
| name |  |

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# EndObject method

Ends an object on the logger

```csharp
public void EndObject()
```

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ToString method

Returns the logged data as a string

```csharp
public override string ToString()
```

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DebugLogger constructor

The default constructor.

```csharp
public DebugLogger()
```

## See Also

* class [DebugLogger](./DebugLogger.md)
* namespace [Oxide.Ext.Discord.Logging](./LoggingNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
