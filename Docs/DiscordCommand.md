# Discord Command

DiscordCommand is a library that allows plugin to register Discord Command callback similar to Oxide Chat Commands.

## Getting Started

To gain access to the Discord Link API you add the following line of code in your plugin.
```c#
private readonly DiscordCommand _dcCommands = Interface.Oxide.GetLibrary<DiscordCommand>();
```

## Registering Commands

### Attributes

### Methods