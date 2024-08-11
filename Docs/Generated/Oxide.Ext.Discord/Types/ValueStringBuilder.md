# ValueStringBuilder structure

```csharp
[Obsolete("Types with embedded references are not supported in this version of your compiler.")]
public struct ValueStringBuilder
```

## Public Members

| name | description |
| --- | --- |
| [ValueStringBuilder](#valuestringbuilder-constructor-1-of-2)(…) |  (2 constructors) |
| [Capacity](#capacity-property) { get; } |  |
| [Item](#valuestringbuilder-indexer) { get; } |  |
| [Length](#length-property) { get; set; } |  |
| [RawChars](#rawchars-property) { get; } | Returns the underlying storage of the builder. |
| [Append](#append-method-1-of-12)(…) |  (12 methods) |
| [AppendLine](#appendline-method)() |  |
| [AppendSpan](#appendspan-method)(…) |  |
| [AsSpan](#asspan-method)() |  |
| [AsSpan](#asspan-method-1-of-3)(…) | Returns a span around the contents of the builder. (3 methods) |
| [Dispose](#dispose-method)() |  |
| [EnsureCapacity](#ensurecapacity-method)(…) |  |
| [GetPinnableReference](#getpinnablereference-method)() | Get a pinnable reference to the builder. Does not ensure there is a null char after [`Length`](#length-property) This overload is pattern matched in the C# 7.3+ compiler so you can omit the explicit method call, and write eg "fixed (char* c = builder)" |
| [GetPinnableReference](#getpinnablereference-method)(…) | Get a pinnable reference to the builder. |
| [Insert](#insert-method-1-of-2)(…) |  (2 methods) |
| override [ToString](#tostring-method)() |  |
| [TryCopyTo](#trycopyto-method)(…) |  |

## See Also

* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [ValueStringBuilder.cs](../../../../Oxide.Ext.Discord/Types/ValueStringBuilder.cs)
   
   
# EnsureCapacity method

```csharp
public void EnsureCapacity(int capacity)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetPinnableReference method (1 of 2)

Get a pinnable reference to the builder. Does not ensure there is a null char after [`Length`](#length-property) This overload is pattern matched in the C# 7.3+ compiler so you can omit the explicit method call, and write eg "fixed (char* c = builder)"

```csharp
public char GetPinnableReference()
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# GetPinnableReference method (2 of 2)

Get a pinnable reference to the builder.

```csharp
public char GetPinnableReference(bool terminate)
```

| parameter | description |
| --- | --- |
| terminate | Ensures that the builder has a null char after [`Length`](#length-property) |

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ToString method

```csharp
public override string ToString()
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AsSpan method (1 of 4)

```csharp
public ReadOnlySpan<char> AsSpan()
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# AsSpan method (2 of 4)

Returns a span around the contents of the builder.

```csharp
public ReadOnlySpan<char> AsSpan(bool terminate)
```

| parameter | description |
| --- | --- |
| terminate | Ensures that the builder has a null char after [`Length`](#length-property) |

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# AsSpan method (3 of 4)

```csharp
public ReadOnlySpan<char> AsSpan(int start)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# AsSpan method (4 of 4)

```csharp
public ReadOnlySpan<char> AsSpan(int start, int length)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# TryCopyTo method

```csharp
public bool TryCopyTo(Span<char> destination, out int charsWritten)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Insert method (1 of 2)

```csharp
public void Insert(int index, string? s)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Insert method (2 of 2)

```csharp
public void Insert(int index, char value, int count)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Append method (1 of 12)

```csharp
public void Append(char c)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Append method (2 of 12)

```csharp
public void Append(ReadOnlySpan<char> value)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Append method (3 of 12)

```csharp
public void Append(string s)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Append method (4 of 12)

```csharp
public void Append(char c, int count)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Append method (5 of 12)

```csharp
public void Append(byte value, string format = null, IFormatProvider provider = null)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Append method (6 of 12)

```csharp
public void Append(int value, string format = null, IFormatProvider provider = null)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Append method (7 of 12)

```csharp
public void Append(long value, string format = null, IFormatProvider provider = null)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Append method (8 of 12)

```csharp
public void Append(sbyte value, string format = null, IFormatProvider provider = null)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Append method (9 of 12)

```csharp
public void Append(short value, string format = null, IFormatProvider provider = null)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Append method (10 of 12)

```csharp
public void Append(uint value, string format = null, IFormatProvider provider = null)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Append method (11 of 12)

```csharp
public void Append(ulong value, string format = null, IFormatProvider provider = null)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Append method (12 of 12)

```csharp
public void Append(ushort value, string format = null, IFormatProvider provider = null)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AppendSpan method

```csharp
public Span<char> AppendSpan(int length)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AppendLine method

```csharp
public void AppendLine()
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Dispose method

```csharp
public void Dispose()
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ValueStringBuilder constructor (1 of 2)

```csharp
public ValueStringBuilder(int initialCapacity)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# ValueStringBuilder constructor (2 of 2)

```csharp
public ValueStringBuilder(Span<char> initialBuffer)
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Length property

```csharp
public int Length { get; set; }
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Capacity property

```csharp
public int Capacity { get; }
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ValueStringBuilder indexer

```csharp
public char this[int index] { get; }
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# RawChars property

Returns the underlying storage of the builder.

```csharp
public Span<char> RawChars { get; }
```

## See Also

* struct [ValueStringBuilder](./ValueStringBuilder.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
