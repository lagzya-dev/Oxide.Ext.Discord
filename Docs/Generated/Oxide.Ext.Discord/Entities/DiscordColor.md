# DiscordColor structure

Represents a Discord Color

```csharp
public struct DiscordColor
```

## Public Members

| name | description |
| --- | --- |
| [DiscordColor](#DiscordColor-constructor)(…) | DiscordColor Constructor (7 constructors) |
| static readonly [Blue](#Blue-field) | Blue Role Color |
| static readonly [Blurple](#Blurple-field) | Discord Blurple Color |
| static readonly [BlurpleOld](#BlurpleOld-field) | Discord Old Blurple Color |
| static readonly [Danger](#Danger-field) | Discord Danger Color |
| static readonly [DarkBlue](#DarkBlue-field) | Dark Blue Role Color |
| static readonly [DarkerGrey](#DarkerGrey-field) | Darker Gray Role Color |
| static readonly [DarkGreen](#DarkGreen-field) | Dark Green Role Color |
| static readonly [DarkGrey](#DarkGrey-field) | Dark Gray Role Color |
| static readonly [DarkMagenta](#DarkMagenta-field) | Dark Magenta Role Color |
| static readonly [DarkOrange](#DarkOrange-field) | Dark Orange Role Color |
| static readonly [DarkPurple](#DarkPurple-field) | Dark Purple Role Color |
| static readonly [DarkRed](#DarkRed-field) | Dark Red Role Color |
| static readonly [DarkTeal](#DarkTeal-field) | Dark Teal Role Color |
| static readonly [Default](#Default-field) | Default Role Color |
| static readonly [Fuchsia](#Fuchsia-field) | Discord Fuchsia Color |
| static readonly [Gold](#Gold-field) | Gold Role Color |
| static readonly [Green](#Green-field) | Green Role Color |
| static readonly [LighterGrey](#LighterGrey-field) | Lighter Gray Role Color |
| static readonly [LightGrey](#LightGrey-field) | Light Gray Role Color |
| static readonly [LightOrange](#LightOrange-field) | Light Orange Role Color |
| static readonly [Magenta](#Magenta-field) | Magenta Role Color |
| static readonly [Orange](#Orange-field) | Orange Role Color |
| static readonly [Purple](#Purple-field) | Purple Role Color |
| static readonly [Red](#Red-field) | Red Role Color |
| static readonly [Success](#Success-field) | Discord Success Color |
| static readonly [Teal](#Teal-field) | Teal Role Color |
| static readonly [Warning](#Warning-field) | Discord Warning Color |
| readonly [Color](#Color-field) | uint value of the hex color code |
| override [ToString](#ToString-method)() | Returns the color as a string |

## See Also

* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [DiscordColor.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/DiscordColor.cs)
   
   
# ToString method

Returns the color as a string

```csharp
public override string ToString()
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DiscordColor constructor (1 of 7)

DiscordColor Constructor

```csharp
public DiscordColor(string color)
```

| parameter | description |
| --- | --- |
| color | string hex color code |

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Throw if color is greater than #FFFFFF |

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# Color field

uint value of the hex color code

```csharp
public readonly uint Color;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Default field

Default Role Color

```csharp
public static readonly DiscordColor Default;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Teal field

Teal Role Color

```csharp
public static readonly DiscordColor Teal;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DarkTeal field

Dark Teal Role Color

```csharp
public static readonly DiscordColor DarkTeal;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Green field

Green Role Color

```csharp
public static readonly DiscordColor Green;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DarkGreen field

Dark Green Role Color

```csharp
public static readonly DiscordColor DarkGreen;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Blue field

Blue Role Color

```csharp
public static readonly DiscordColor Blue;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DarkBlue field

Dark Blue Role Color

```csharp
public static readonly DiscordColor DarkBlue;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Purple field

Purple Role Color

```csharp
public static readonly DiscordColor Purple;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DarkPurple field

Dark Purple Role Color

```csharp
public static readonly DiscordColor DarkPurple;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Magenta field

Magenta Role Color

```csharp
public static readonly DiscordColor Magenta;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DarkMagenta field

Dark Magenta Role Color

```csharp
public static readonly DiscordColor DarkMagenta;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Gold field

Gold Role Color

```csharp
public static readonly DiscordColor Gold;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# LightOrange field

Light Orange Role Color

```csharp
public static readonly DiscordColor LightOrange;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Orange field

Orange Role Color

```csharp
public static readonly DiscordColor Orange;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DarkOrange field

Dark Orange Role Color

```csharp
public static readonly DiscordColor DarkOrange;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Red field

Red Role Color

```csharp
public static readonly DiscordColor Red;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DarkRed field

Dark Red Role Color

```csharp
public static readonly DiscordColor DarkRed;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# LightGrey field

Light Gray Role Color

```csharp
public static readonly DiscordColor LightGrey;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# LighterGrey field

Lighter Gray Role Color

```csharp
public static readonly DiscordColor LighterGrey;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DarkGrey field

Dark Gray Role Color

```csharp
public static readonly DiscordColor DarkGrey;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DarkerGrey field

Darker Gray Role Color

```csharp
public static readonly DiscordColor DarkerGrey;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Success field

Discord Success Color

```csharp
public static readonly DiscordColor Success;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Warning field

Discord Warning Color

```csharp
public static readonly DiscordColor Warning;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Danger field

Discord Danger Color

```csharp
public static readonly DiscordColor Danger;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# BlurpleOld field

Discord Old Blurple Color

```csharp
public static readonly DiscordColor BlurpleOld;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Blurple field

Discord Blurple Color

```csharp
public static readonly DiscordColor Blurple;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Fuchsia field

Discord Fuchsia Color

```csharp
public static readonly DiscordColor Fuchsia;
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
