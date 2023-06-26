# Snowflake structure

Represents an ID in discord.

```csharp
public struct Snowflake : IComparable<Snowflake>, IComparable<ulong>, IEquatable<Snowflake>, 
    IEquatable<ulong>
```

## Public Members

| name | description |
| --- | --- |
| [Snowflake](#Snowflake-constructor)(…) | Create a new snowflake from a ulong (3 constructors) |
| readonly [Id](#Id-field) | Snowflake Value |
| [CompareTo](#CompareTo-method)(…) | Returns the ID field of num compared to this snowflakes ID field (2 methods) |
| override [Equals](#Equals-method)(…) | Returns if the obj is snowflake or ulong with matching ID. |
| [Equals](#Equals-method)(…) | Returns if the two snowflakes are the same ID. (2 methods) |
| [GetCreationDate](#GetCreationDate-method)() | Returns when the ID was created |
| override [GetHashCode](#GetHashCode-method)() | Returns the HashCode of the ID |
| [IsValid](#IsValid-method)() | Returns if the ID value is not 0 |
| override [ToString](#ToString-method)() | Returns ID as a string |
| static readonly [DiscordEpoch](#DiscordEpoch-field) | DateTimeOffset since discord Epoch |
| static [TryParse](#TryParse-method)(…) | Try to parse the a string into a snowflake value |
| [operator ==](#op_Equality-operator) | Returns true if left and right are equal |
| [explicit operator](#op_Explicit-operator) | Converts a ulong to a snowflake (2 operators) |
| [operator &gt;](#op_GreaterThan-operator) | Returns true if left snowflake's ID is greater than right's ID |
| [operator &gt;=](#op_GreaterThanOrEqual-operator) | Returns true if left snowflake's ID is greater or equal to right's ID |
| [implicit operator](#op_Implicit-operator) | Converts snowflake to a ulong (2 operators) |
| [operator !=](#op_Inequality-operator) | Returns true if left and right are not equal |
| [operator &lt;](#op_LessThan-operator) | Returns true if left snowflake's ID is less than right's ID |
| [operator &lt;=](#op_LessThanOrEqual-operator) | Returns true if left snowflake's ID is less than right's ID or equal |

## See Also

* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [Snowflake.cs](../../../../Oxide.Ext.Discord/Entities/Snowflake.cs)
   
   
# GetCreationDate method

Returns when the ID was created

```csharp
public DateTimeOffset GetCreationDate()
```

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IsValid method

Returns if the ID value is not 0

```csharp
public bool IsValid()
```

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# TryParse method

Try to parse the a string into a snowflake value

```csharp
public static bool TryParse(string value, out Snowflake snowflake)
```

| parameter | description |
| --- | --- |
| value | String to parse |
| snowflake | Snowflake to return |

## Return Value

True if parse succeeded; false otherwise

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Equals method (1 of 3)

Returns if the obj is snowflake or ulong with matching ID.

```csharp
public override bool Equals(object obj)
```

| parameter | description |
| --- | --- |
| obj | Object to check |

## Return Value

True if equal; False otherwise

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# GetHashCode method

Returns the HashCode of the ID

```csharp
public override int GetHashCode()
```

## Return Value

ID fields hashcode

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ToString method

Returns ID as a string

```csharp
public override string ToString()
```

## Return Value

ID as a string

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# CompareTo method (1 of 2)

Returns the ID field of num compared to this snowflakes ID field

```csharp
public int CompareTo(Snowflake num)
```

| parameter | description |
| --- | --- |
| num | Value to compare ID to |

## Return Value

A value indication if the num is less than, equal to, or greater than our ID

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# Snowflake Equality operator

Returns true if left and right are equal

```csharp
public static bool operator ==(Snowflake left, Snowflake right)
```

| parameter | description |
| --- | --- |
| left | Snowflake to compare |
| right | Snowflake to compare |

## Return Value

True if the snowflake ID's are equal; false otherwise

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Snowflake Inequality operator

Returns true if left and right are not equal

```csharp
public static bool operator !=(Snowflake left, Snowflake right)
```

| parameter | description |
| --- | --- |
| left | Snowflake to compare |
| right | Snowflake to compare |

## Return Value

True if the snowflake ID's are not equal; false otherwise

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Snowflake LessThan operator

Returns true if left snowflake's ID is less than right's ID

```csharp
public static bool operator <(Snowflake left, Snowflake right)
```

| parameter | description |
| --- | --- |
| left | Snowflake to be less than |
| right | Snowflake to be greater than |

## Return Value

True if left is less than right

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Snowflake GreaterThan operator

Returns true if left snowflake's ID is greater than right's ID

```csharp
public static bool operator >(Snowflake left, Snowflake right)
```

| parameter | description |
| --- | --- |
| left | Snowflake to be greater than |
| right | Snowflake to be less than |

## Return Value

True if left is greater than right

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Snowflake LessThanOrEqual operator

Returns true if left snowflake's ID is less than right's ID or equal

```csharp
public static bool operator <=(Snowflake left, Snowflake right)
```

| parameter | description |
| --- | --- |
| left | Snowflake to be less than or equal |
| right | Snowflake to be greater than or equal |

## Return Value

True if left is less than or equal to right

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Snowflake GreaterThanOrEqual operator

Returns true if left snowflake's ID is greater or equal to right's ID

```csharp
public static bool operator >=(Snowflake left, Snowflake right)
```

| parameter | description |
| --- | --- |
| left | Snowflake to be greater than or equal |
| right | Snowflake to be less than or equal |

## Return Value

True if left is greater or equal to right

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Snowflake Implicit operator (1 of 2)

Converts snowflake to a ulong

```csharp
public static implicit operator ulong(Snowflake snowflake)
```

| parameter | description |
| --- | --- |
| snowflake | Snowflake to be converted to ulong |

## Return Value

Snowflake ID as ulong

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# Snowflake Explicit operator (1 of 2)

Converts a string to a snowflake

```csharp
public static explicit operator Snowflake(string id)
```

| parameter | description |
| --- | --- |
| id | Id to be converted to snowflake |

## Return Value

ID converted to a snowflake

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# Snowflake constructor (1 of 3)

Create a new snowflake from a string

```csharp
public Snowflake(string id)
```

| parameter | description |
| --- | --- |
| id |  |

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# Id field

Snowflake Value

```csharp
public readonly ulong Id;
```

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DiscordEpoch field

DateTimeOffset since discord Epoch

```csharp
public static readonly DateTimeOffset DiscordEpoch;
```

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
