# InputTextTemplate class

Input Text Message Component Template

```csharp
public class InputTextTemplate : BaseComponentTemplate
```

## Public Members

| name | description |
| --- | --- |
| [InputTextTemplate](#InputTextTemplate-constructor)() | Constructor |
| [InputTextTemplate](#InputTextTemplate-constructor)(…) | Constructor |
| [CustomId](#CustomId-property) { get; set; } | Custom ID of the input text |
| [Label](#Label-property) { get; set; } | Text that appears on top of the input text field, max 80 characters |
| [MaxLength](#MaxLength-property) { get; set; } | The maximum length of the text input |
| [MinLength](#MinLength-property) { get; set; } | The minimum length of the text input |
| [Placeholder](#Placeholder-property) { get; set; } | The placeholder for the text input field |
| [Required](#Required-property) { get; set; } | Is the Input Text Required to be filled out |
| [Style](#Style-property) { get; set; } | The style of the input text |
| [Value](#Value-property) { get; set; } | The pre-filled value for text input |
| override [ToComponent](#ToComponent-method)(…) | Converts the template to a [`InputTextComponent`](../../../Entities/Interactions/MessageComponents/InputTextComponent.md) |

## See Also

* class [BaseComponentTemplate](./BaseComponentTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [InputTextTemplate.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Libraries/Templates/Components/InputTextTemplate.cs)
   
   
# ToComponent method

Converts the template to a [`InputTextComponent`](../../../Entities/Interactions/MessageComponents/InputTextComponent.md)

```csharp
public override BaseComponent ToComponent(PlaceholderData data)
```

| parameter | description |
| --- | --- |
| data |  |

## See Also

* class [BaseComponent](../../../Entities/Interactions/MessageComponents/BaseComponent.md)
* class [PlaceholderData](../../Placeholders/PlaceholderData.md)
* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# InputTextTemplate constructor (1 of 2)

Constructor

```csharp
public InputTextTemplate()
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---
   
   
# CustomId property

Custom ID of the input text

```csharp
public string CustomId { get; set; }
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Style property

The style of the input text

```csharp
public InputTextStyles Style { get; set; }
```

## See Also

* enum [InputTextStyles](../../../Entities/Interactions/MessageComponents/InputTextStyles.md)
* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Label property

Text that appears on top of the input text field, max 80 characters

```csharp
public string Label { get; set; }
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# MinLength property

The minimum length of the text input

```csharp
public int MinLength { get; set; }
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# MaxLength property

The maximum length of the text input

```csharp
public int MaxLength { get; set; }
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Placeholder property

The placeholder for the text input field

```csharp
public string Placeholder { get; set; }
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Value property

The pre-filled value for text input

```csharp
public string Value { get; set; }
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Required property

Is the Input Text Required to be filled out

```csharp
public bool Required { get; set; }
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
