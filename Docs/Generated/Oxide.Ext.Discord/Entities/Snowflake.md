# Snowflake structure

Represents an ID in discord.

```csharp
public struct Snowflake : IComparable<Snowflake>, IComparable<ulong>, IEquatable<Snowflake>, 
    IEquatable<ulong>
```

## Public Members

| name | description |
| --- | --- |
| [Snowflake](#snowflake-constructor-1-of-3))(…) | Create a new snowflake from a ulong (3 constructors) |
| readonly [Id](#id-field) | Snowflake Value |
| [CompareTo](#compareto-method-1-of-2))(…) | Returns the ID field of num compared to this snowflakes ID field (2 methods) |
| override [Equals](#equals-method)(…) | Returns if the obj is snowflake or ulong with matching ID. |
| [Equals](#equals-method-1-of-2))(…) | Returns if the two snowflakes are the same ID. (2 methods) |
| [GetCreationDate](#getcreationdate-method)() | Returns when the ID was created |
| override [GetHashCode](#gethashcode-method)() | Returns the HashCode of the ID |
| [IsValid](#isvalid-method)() | Returns if the ID value is not 0 |
| override [ToString](#tostring-method)() | Returns ID as a string |
| static readonly [DiscordEpoch](#discordepoch-field) | DateTimeOffset since discord Epoch |
| static [TryParse](#tryparse-method)(…) | Try to parse the a string into a snowflake value |
| [operator ==](#snowflake-equality-operator) | Returns true if left and right are equal |
| [explicit operator](#snowflake-explicit-operator-1-of-2)) | Converts a ulong to a snowflake (2 operators) |
| [operator &gt;](#snowflake-greaterthan-operator) | Returns true if left snowflake's ID is greater than right's ID |
| [operator &gt;=](#snowflake-greaterthanorequal-operator) | Returns true if left snowflake's ID is greater or equal to right's ID |
| [implicit operator](#snowflake-implicit-operator-1-of-2)) | Converts snowflake to a ulong (2 operators) |
| [operator !=](#snowflake-inequality-operator) | Returns true if left and right are not equal |
| [operator &lt;](#snowflake-lessthan-operator) | Returns true if left snowflake's ID is less than right's ID |
| [operator &lt;=](#snowflake-lessthanorequal-operator) | Returns true if left snowflake's ID is less than right's ID or equal |

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

# Equals method (2 of 3)

Returns if the two snowflakes are the same ID.

```csharp
public bool Equals(Snowflake other)
```

| parameter | description |
| --- | --- |
| other | Other snowflake to compare |

## Return Value

True if the snowflake IDs match; false otherwise.

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Equals method (3 of 3)

Returns if other equals our ID

```csharp
public bool Equals(ulong other)
```

| parameter | description |
| --- | --- |
| other | Other to compare against |

## Return Value

True if ID equals; False otherwise.

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
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

# CompareTo method (2 of 2)

Returns the ID field of num compared to this snowflakes ID field

```csharp
public int CompareTo(ulong other)
```

| parameter | description |
| --- | --- |
| other | Value to compare ID to |

## Return Value

A value indication if the num is less than, equal to, or greater than our ID

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
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

# Snowflake Implicit operator (2 of 2)

Converts snowflake to a string

```csharp
public static implicit operator string(Snowflake snowflake)
```

| parameter | description |
| --- | --- |
| snowflake | Snowflake to be converted to string |

## Return Value

Snowflake ID as string

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
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

# Snowflake Explicit operator (2 of 2)

Converts a ulong to a snowflake

```csharp
public static explicit operator Snowflake(ulong id)
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

# Snowflake constructor (2 of 3)

Create a new snowflake from a ulong

```csharp
public Snowflake(ulong id)
```

| parameter | description |
| --- | --- |
| id |  |

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Snowflake constructor (3 of 3)

Create a snowflake from a DateTimeOffset and increment

```csharp
public Snowflake(DateTimeOffset offset, ulong increment = 0)
```

| parameter | description |
| --- | --- |
| offset |  |
| increment | Increment value of the snowflake |

## See Also

* struct [Snowflake](./Snowflake.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
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
