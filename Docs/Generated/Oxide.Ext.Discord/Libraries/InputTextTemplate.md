# InputTextTemplate class

Input Text Message Component Template

```csharp
public class InputTextTemplate : BaseComponentTemplate
```

## Public Members

| name | description |
| --- | --- |
| [InputTextTemplate](#inputtexttemplate-constructor)() | Constructor |
| [InputTextTemplate](#inputtexttemplate-constructor)(…) | Constructor |
| [CustomId](#customid-property) { get; set; } | Custom ID of the input text |
| [Label](#label-property) { get; set; } | Text that appears on top of the input text field, max 80 characters |
| [MaxLength](#maxlength-property) { get; set; } | The maximum length of the text input |
| [MinLength](#minlength-property) { get; set; } | The minimum length of the text input |
| [Placeholder](#placeholder-property) { get; set; } | The placeholder for the text input field |
| [Required](#required-property) { get; set; } | Is the Input Text Required to be filled out |
| [Style](#style-property) { get; set; } | The style of the input text |
| [Value](#value-property) { get; set; } | The pre-filled value for text input |
| override [ToComponent](#tocomponent-method)(…) | Converts the template to a [`InputTextComponent`](../Entities/InputTextComponent.md) |

## See Also

* class [BaseComponentTemplate](./BaseComponentTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [InputTextTemplate.cs](../../../../Oxide.Ext.Discord/Libraries/InputTextTemplate.cs)
   
   
# ToComponent method

Converts the template to a [`InputTextComponent`](../Entities/InputTextComponent.md)

```csharp
public override BaseComponent ToComponent(PlaceholderData data)
```

| parameter | description |
| --- | --- |
| data |  |

## See Also

* class [BaseComponent](../Entities/BaseComponent.md)
* class [PlaceholderData](./PlaceholderData.md)
* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# InputTextTemplate constructor (1 of 2)

Constructor

```csharp
public InputTextTemplate()
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# InputTextTemplate constructor (2 of 2)

Constructor

```csharp
public InputTextTemplate(string label, string customId, string value = "", 
    InputTextStyles style = InputTextStyles.Short, bool required = false, string placeholder = "", 
    int minLength = 0, int maxLength = 4000)
```

| parameter | description |
| --- | --- |
| label |  |
| customId |  |
| value |  |
| style |  |
| required |  |
| placeholder |  |
| minLength |  |
| maxLength |  |

## See Also

* enum [InputTextStyles](../Entities/InputTextStyles.md)
* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# CustomId property

Custom ID of the input text

```csharp
public string CustomId { get; set; }
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Style property

The style of the input text

```csharp
public InputTextStyles Style { get; set; }
```

## See Also

* enum [InputTextStyles](../Entities/InputTextStyles.md)
* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Label property

Text that appears on top of the input text field, max 80 characters

```csharp
public string Label { get; set; }
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# MinLength property

The minimum length of the text input

```csharp
public int MinLength { get; set; }
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# MaxLength property

The maximum length of the text input

```csharp
public int MaxLength { get; set; }
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Placeholder property

The placeholder for the text input field

```csharp
public string Placeholder { get; set; }
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Value property

The pre-filled value for text input

```csharp
public string Value { get; set; }
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Required property

Is the Input Text Required to be filled out

```csharp
public bool Required { get; set; }
```

## See Also

* class [InputTextTemplate](./InputTextTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
