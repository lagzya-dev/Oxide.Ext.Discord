# MessageComponentBuilder class

Builder for Message Components

```csharp
public class MessageComponentBuilder
```

## Public Members

| name | description |
| --- | --- |
| [MessageComponentBuilder](#messagecomponentbuilder-constructor)() | Creates a new MessageComponentBuilder |
| [AddActionButton](#addactionbutton-method)(…) | Adds an action button to the current action row |
| [AddDummyButton](#adddummybutton-method)(…) | Adds a dummy button that doesn't do anything |
| [AddInputText](#addinputtext-method)(…) | Adds a select menu to a new action row |
| [AddLinkButton](#addlinkbutton-method)(…) | Adds a link button to the current action row |
| [AddSelectMenu](#addselectmenu-method)(…) | Adds a select menu to a new action row |
| [Build](#build-method)() | Returns the built action rows |

## See Also

* namespace [Oxide.Ext.Discord.Builders.MessageComponents](./MessageComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [MessageComponentBuilder.cs](../../../../Oxide.Ext.Discord/Builders/MessageComponents/MessageComponentBuilder.cs)
   
   
# AddActionButton method

Adds an action button to the current action row

```csharp
public MessageComponentBuilder AddActionButton(ButtonStyle style, string label, string customId, 
    bool disabled = false, bool addToNewRow = false, DiscordEmoji emoji = null)
```

| parameter | description |
| --- | --- |
| style | Button Style [Button Styles](https://discord.com/developers/docs/interactions/message-components#button-object-button-styles) |
| label | The text of the button |
| customId | The unique id of the button. Used to identify which button was clicked |
| disabled | If this button is disabled |
| addToNewRow | Should the button be added to a new row |
| emoji | Emoji to display with the button |

## Return Value

[`MessageComponentBuilder`](./MessageComponentBuilder.md)

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Throw if the button style is link or if the button goes outside the max number of action rows |

## See Also

* enum [ButtonStyle](../../Entities/Interactions/MessageComponents/ButtonStyle.md)
* class [DiscordEmoji](../../Entities/Emojis/DiscordEmoji.md)
* class [MessageComponentBuilder](./MessageComponentBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.MessageComponents](./MessageComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddDummyButton method

Adds a dummy button that doesn't do anything

```csharp
public MessageComponentBuilder AddDummyButton(string label, bool disabled = true)
```

| parameter | description |
| --- | --- |
| label | The text of the button |
| disabled | If this button is disabled |

## Return Value

[`MessageComponentBuilder`](./MessageComponentBuilder.md)

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Throw if the button goes outside the max number of action rows |

## See Also

* class [MessageComponentBuilder](./MessageComponentBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.MessageComponents](./MessageComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddLinkButton method

Adds a link button to the current action row

```csharp
public MessageComponentBuilder AddLinkButton(string label, string url, bool disabled = false, 
    bool addToNewRow = false, DiscordEmoji emoji = null)
```

| parameter | description |
| --- | --- |
| label | Text on the button |
| url | URL for the button |
| disabled | if the button should be disabled |
| addToNewRow | Show the button be added to a new row |
| emoji | Emoji to display on the button |

## Return Value

[`MessageComponentBuilder`](./MessageComponentBuilder.md)

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if the button goes outside the max number of action rows |

## See Also

* class [DiscordEmoji](../../Entities/Emojis/DiscordEmoji.md)
* class [MessageComponentBuilder](./MessageComponentBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.MessageComponents](./MessageComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddSelectMenu method

Adds a select menu to a new action row

```csharp
public SelectMenuComponentBuilder AddSelectMenu(MessageComponentType type, string customId, 
    string placeholder, int minValues = 1, int maxValues = 1, bool disabled = false)
```

| parameter | description |
| --- | --- |
| type | Select Menu Message Component Type |
| customId | Unique ID for the select menu |
| placeholder | Text to display if no value is selected yet |
| minValues | The min number of options you must select |
| maxValues | The max number of options you can select |
| disabled | If the select menu should be disabled |

## Return Value

[`SelectMenuComponentBuilder`](./SelectMenuComponentBuilder.md)

## See Also

* class [SelectMenuComponentBuilder](./SelectMenuComponentBuilder.md)
* enum [MessageComponentType](../../Entities/Interactions/MessageComponents/MessageComponentType.md)
* class [MessageComponentBuilder](./MessageComponentBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.MessageComponents](./MessageComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# AddInputText method

Adds a select menu to a new action row

```csharp
public MessageComponentBuilder AddInputText(string customId, string label, InputTextStyles style, 
    string value = null, bool? required = null, string placeholder = null, int? minLength = null, 
    int? maxLength = null)
```

| parameter | description |
| --- | --- |
| customId | Unique ID for the select menu |
| label | Label for the input text |
| style | Style of the Input Text |
| value | Default value for the Input Text |
| required | Is the Input Text Required to be filled out |
| placeholder | Text to display if no value is selected yet |
| minLength | The min number of options you must select |
| maxLength | The max number of options you can select |

## Return Value

[`MessageComponentBuilder`](./MessageComponentBuilder.md)

## See Also

* enum [InputTextStyles](../../Entities/Interactions/MessageComponents/InputTextStyles.md)
* class [MessageComponentBuilder](./MessageComponentBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.MessageComponents](./MessageComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Build method

Returns the built action rows

```csharp
public List<ActionRowComponent> Build()
```

## See Also

* class [ActionRowComponent](../../Entities/Interactions/MessageComponents/ActionRowComponent.md)
* class [MessageComponentBuilder](./MessageComponentBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.MessageComponents](./MessageComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# MessageComponentBuilder constructor

Creates a new MessageComponentBuilder

```csharp
public MessageComponentBuilder()
```

## See Also

* class [MessageComponentBuilder](./MessageComponentBuilder.md)
* namespace [Oxide.Ext.Discord.Builders.MessageComponents](./MessageComponentsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
