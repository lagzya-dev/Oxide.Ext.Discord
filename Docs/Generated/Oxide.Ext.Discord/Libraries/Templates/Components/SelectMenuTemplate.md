# SelectMenuTemplate class

Represents a template for select menus

```csharp
public class SelectMenuTemplate : BaseComponentTemplate
```

## Public Members

| name | description |
| --- | --- |
| [SelectMenuTemplate](#SelectMenuTemplate)(…) | Constructor |
| [ChannelTypes](#ChannelTypes) { get; set; } | ChannelSelect[`ChannelType`](../../../Entities/Channels/ChannelType.md) to show Max 25 options |
| [CustomId](#CustomId) { get; set; } | Command for the Select Menu |
| [Enabled](#Enabled) { get; set; } | If the Button is enabled |
| [MaxValues](#MaxValues) { get; set; } | the maximum number of items that must be chosen Default 1, Min 0, Max 25 |
| [MinValues](#MinValues) { get; set; } | the minimum number of items that must be chosen Default 1, Min 0, Max 25 |
| [Options](#Options) { get; set; } | The choices in the select Max 25 options |
| [Placeholder](#Placeholder) { get; set; } | Custom placeholder text if nothing is selected Max 150 characters |
| override [ToComponent](#ToComponent)(…) |  |

## See Also

* class [BaseComponentTemplate](./BaseComponentTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [SelectMenuTemplate.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Libraries/Templates/Components/SelectMenuTemplate.cs)
   
   
# ToComponent method

```csharp
public override BaseComponent ToComponent(PlaceholderData data)
```

## See Also

* class [BaseComponent](../../../Entities/Interactions/MessageComponents/BaseComponent.md)
* class [PlaceholderData](../../Placeholders/PlaceholderData.md)
* class [SelectMenuTemplate](./SelectMenuTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# SelectMenuTemplate constructor

Constructor

```csharp
public SelectMenuTemplate(MessageComponentType type)
```

| parameter | description |
| --- | --- |
| type |  |

## See Also

* enum [MessageComponentType](../../../Entities/Interactions/MessageComponents/MessageComponentType.md)
* class [SelectMenuTemplate](./SelectMenuTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# CustomId property

Command for the Select Menu

```csharp
public string CustomId { get; set; }
```

## See Also

* class [SelectMenuTemplate](./SelectMenuTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Placeholder property

Custom placeholder text if nothing is selected Max 150 characters

```csharp
public string Placeholder { get; set; }
```

## See Also

* class [SelectMenuTemplate](./SelectMenuTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Options property

The choices in the select Max 25 options

```csharp
public List<SelectMenuOptionTemplate> Options { get; set; }
```

## See Also

* class [SelectMenuOptionTemplate](./SelectMenuOptionTemplate.md)
* class [SelectMenuTemplate](./SelectMenuTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ChannelTypes property

ChannelSelect[`ChannelType`](../../../Entities/Channels/ChannelType.md) to show Max 25 options

```csharp
public List<ChannelType> ChannelTypes { get; set; }
```

## See Also

* enum [ChannelType](../../../Entities/Channels/ChannelType.md)
* class [SelectMenuTemplate](./SelectMenuTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# MinValues property

the minimum number of items that must be chosen Default 1, Min 0, Max 25

```csharp
public int MinValues { get; set; }
```

## See Also

* class [SelectMenuTemplate](./SelectMenuTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# MaxValues property

the maximum number of items that must be chosen Default 1, Min 0, Max 25

```csharp
public int MaxValues { get; set; }
```

## See Also

* class [SelectMenuTemplate](./SelectMenuTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Enabled property

If the Button is enabled

```csharp
public bool Enabled { get; set; }
```

## See Also

* class [SelectMenuTemplate](./SelectMenuTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
