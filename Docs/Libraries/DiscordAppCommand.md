# Discord App Command

[Discord App Command](../Generated/Oxide.Ext.Discord/Libraries/AppCommands/DiscordAppCommand.md)
is a library that allows registering callbacks to handle application command interactions.
This library can handle Slash Commands, Auto Complete, Message Components, and Modal Submits.

## Registering Callbacks

You can register callback is two ways. You can add the appropriate attribute to the method to use as the callback,
or you can call the registration methods on [Discord App Command](../Generated/Oxide.Ext.Discord/Libraries/AppCommands/DiscordAppCommand.md).

### Attributes

It is recommended to use attributes if you know the commands / custom ids beforehand.

#### Discord Application Command Attribute

The [Discord Application Command Attribute](../Generated/Oxide.Ext.Discord/Attributes/ApplicationCommands/DiscordApplicationCommandAttribute.md) 
allows you to register a application command callback. 
The method below will be called when a interaction comes in that matches `/command group sub` from discord.  
*Note: Group and Sub are optional parameters and should be null if not used in the command* 

```csharp
[DiscordApplicationCommandAttribute("command", "sub", "group")]
private void HandleCallback(DiscordInteraction interaction, InteractionDataParsed parsed) { }
```

**See:**  
[Discord Interaction](../Generated/Oxide.Ext.Discord/Entities/Interactions/DiscordInteraction.md)  
[Interaction Data Parsed](../Generated/Oxide.Ext.Discord/Entities/Interactions/InteractionDataParsed.md)

#### Discord Auto Complete Attribute

The [Discord Auto Complete Attribute](../Generated/Oxide.Ext.Discord/Attributes/ApplicationCommands/DiscordAutoCompleteCommandAttribute.md)
allows you to register a auto complete callback.
The method below will be called when a interaction comes in that matches `/command group sub arg` from discord. 
The focused argument will contain the data from the auto complete
*Note: Group and Sub are optional parameters and should be null if not used in the command*

```csharp
[DiscordAutoCompleteCommandAttribute("command", "arg", "sub", "group")]
private void HandleCallback(DiscordInteraction interaction, InteractionDataOption focused) { }
```

**See:**  
[Discord Interaction](../Generated/Oxide.Ext.Discord/Entities/Interactions/DiscordInteraction.md)  
[Interaction Data Option](../Generated/Oxide.Ext.Discord/Entities/Interactions/InteractionDataOption.md)

#### Discord Message Component Attribute

The [Discord Message Component Attribute](../Generated/Oxide.Ext.Discord/Attributes/ApplicationCommands/DiscordMessageComponentCommandAttribute.md)
allows you to register a message component callback.
The method below will be called when a message component with the specified custom id is called.

```csharp
[DiscordMessageComponentCommandAttribute("customid")]
private void HandleCallback(DiscordInteraction interaction) { }
```

**See:**  
[Discord Interaction](../Generated/Oxide.Ext.Discord/Entities/Interactions/DiscordInteraction.md)

#### Discord Modal Submit Attribute

The [Discord Modal Submit Attribute](../Generated/Oxide.Ext.Discord/Attributes/ApplicationCommands/DiscordModalSubmitAttribute.md)
allows you to register a modal submit callback.
The method below will be called when a modal submit with the specified custom id is called.

```csharp
[DiscordModalSubmitAttribute("customid")]
private void HandleCallback(DiscordInteraction interaction) { }
```

**See:**  
[Discord Interaction](../Generated/Oxide.Ext.Discord/Entities/Interactions/DiscordInteraction.md)

### Methods

You can register callbacks using the available methods on [Discord App Command](../Generated/Oxide.Ext.Discord/Libraries/AppCommands/DiscordAppCommand.md)
by using the provided methods. It is recommended to use this if you don't know the commands or custom ids before hand.
