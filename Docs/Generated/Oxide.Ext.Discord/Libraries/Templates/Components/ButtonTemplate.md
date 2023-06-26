# ButtonTemplate class

Template for Button Components

```csharp
public class ButtonTemplate : BaseComponentTemplate
```

## Public Members

| name | description |
| --- | --- |
| [ButtonTemplate](#buttontemplate-constructor)() | Default Constructor |
| [ButtonTemplate](#buttontemplate-constructor-1-of-2))(…) | Constructor without emoji (2 constructors) |
| [Command](#command-property) { get; set; } | Command for the button. If Link then this will set the Url field; Else the CustomId field |
| [Emoji](#emoji-property) { get; set; } | Emoji for the button |
| [Enabled](#enabled-property) { get; set; } | If the Button is enabled |
| [Inline](#inline-property) { get; set; } | Should the button be on the same or new row |
| [Label](#label-property) { get; set; } | Display label for the button |
| [Style](#style-property) { get; set; } | [`ButtonStyle`](../../../Entities/Interactions/MessageComponents/ButtonStyle.md) for the button |
| override [ToComponent](#tocomponent-method)(…) | Converts the template to a [`ButtonComponent`](../../../Entities/Interactions/MessageComponents/ButtonComponent.md) |

## See Also

* class [BaseComponentTemplate](./BaseComponentTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [ButtonTemplate.cs](../../../../Oxide.Ext.Discord/Libraries/Templates/Components/ButtonTemplate.cs)
   
   
# ToComponent method

Converts the template to a [`ButtonComponent`](../../../Entities/Interactions/MessageComponents/ButtonComponent.md)

```csharp
public override BaseComponent ToComponent(PlaceholderData data)
```

## See Also

* class [BaseComponent](../../../Entities/Interactions/MessageComponents/BaseComponent.md)
* class [PlaceholderData](../../Placeholders/PlaceholderData.md)
* class [ButtonTemplate](./ButtonTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# ButtonTemplate constructor (1 of 3)

Default Constructor

```csharp
public ButtonTemplate()
```

## See Also

* class [ButtonTemplate](./ButtonTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# ButtonTemplate constructor (2 of 3)

Constructor without emoji

```csharp
public ButtonTemplate(string label, ButtonStyle style, string command, bool enabled = true, 
    bool inline = true)
```

| parameter | description |
| --- | --- |
| label | Button Label |
| style | [`ButtonStyle`](../../../Entities/Interactions/MessageComponents/ButtonStyle.md) |
| command | Button Command |
| enabled | Is button enabled? |
| inline | Should the button be on the same row or a new row? |

## See Also

* enum [ButtonStyle](../../../Entities/Interactions/MessageComponents/ButtonStyle.md)
* class [ButtonTemplate](./ButtonTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# ButtonTemplate constructor (3 of 3)

Constructor with emoji

```csharp
public ButtonTemplate(string label, ButtonStyle style, string command, string emoji, 
    bool enabled = true, bool inline = true)
```

| parameter | description |
| --- | --- |
| label | Button Label |
| style | [`ButtonStyle`](../../../Entities/Interactions/MessageComponents/ButtonStyle.md) |
| command | Button Command |
| emoji | Emoji for the button |
| enabled | Is button enabled? |
| inline | Should the button be on the same row or a new row? |

## See Also

* enum [ButtonStyle](../../../Entities/Interactions/MessageComponents/ButtonStyle.md)
* class [ButtonTemplate](./ButtonTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Label property

Display label for the button

```csharp
public string Label { get; set; }
```

## See Also

* class [ButtonTemplate](./ButtonTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Emoji property

Emoji for the button

```csharp
public EmojiTemplate Emoji { get; set; }
```

## See Also

* class [EmojiTemplate](../Emojis/EmojiTemplate.md)
* class [ButtonTemplate](./ButtonTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Style property

[`ButtonStyle`](../../../Entities/Interactions/MessageComponents/ButtonStyle.md) for the button

```csharp
public ButtonStyle Style { get; set; }
```

## See Also

* enum [ButtonStyle](../../../Entities/Interactions/MessageComponents/ButtonStyle.md)
* class [ButtonTemplate](./ButtonTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Command property

Command for the button. If Link then this will set the Url field; Else the CustomId field

```csharp
public string Command { get; set; }
```

## See Also

* class [ButtonTemplate](./ButtonTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Inline property

Should the button be on the same or new row

```csharp
public bool Inline { get; set; }
```

## See Also

* class [ButtonTemplate](./ButtonTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
   
   
# Enabled property

If the Button is enabled

```csharp
public bool Enabled { get; set; }
```

## See Also

* class [ButtonTemplate](./ButtonTemplate.md)
* namespace [Oxide.Ext.Discord.Libraries.Templates.Components](./ComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
