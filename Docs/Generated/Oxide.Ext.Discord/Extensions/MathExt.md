# MathExt class

Extensions for math operations

```csharp
public static class MathExt
```

## Public Members

| name | description |
| --- | --- |
| static [Clamp&lt;T&gt;](#clamp&amp;lt;t&amp;gt;-method)(…) | Returns the value of {T} clamped between min and max value |
| static [Max&lt;T&gt;](#max&amp;lt;t&amp;gt;-method)(…) | Returns the Max value between left and right |
| static [Min&lt;T&gt;](#min&amp;lt;t&amp;gt;-method)(…) | Returns the min value between left and right |

## See Also

* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [MathExt.cs](../../../../Oxide.Ext.Discord/Extensions/MathExt.cs)
   
   
# Clamp&lt;T&gt; method

Returns the value of {T} clamped between min and max value

```csharp
public static T Clamp<T>(this T val, T min, T max)
    where T : IComparable<T>
```

| parameter | description |
| --- | --- |
| T | Type to be clamped |
| val | Value to be clamped |
| min | Min value |
| max | Max Value |

## Return Value

Value of {T} clamped between min and max

## See Also

* class [MathExt](./MathExt.md)
* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Min&lt;T&gt; method

Returns the min value between left and right

```csharp
public static T Min<T>(T left, T right)
    where T : IComparable<T>
```

| parameter | description |
| --- | --- |
| T | Type of IComparable{T} |
| left | Left argument |
| right | Right argument |

## Return Value

Left if less than or equal to right else right

## See Also

* class [MathExt](./MathExt.md)
* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Max&lt;T&gt; method

Returns the Max value between left and right

```csharp
public static T Max<T>(T left, T right)
    where T : IComparable<T>
```

| parameter | description |
| --- | --- |
| T | Type of IComparable{T} |
| left | Left argument |
| right | Right argument |

## Return Value

Left if greater than or equal to right else right

## See Also

* class [MathExt](./MathExt.md)
* namespace [Oxide.Ext.Discord.Extensions](./ExtensionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->