# InteractionDataArgs class

Args supplied for the interaction

```csharp
public class InteractionDataArgs
```

## Public Members

| name | description |
| --- | --- |
| [GetBool](#GetBool)(…) | Returns the bool value supplied to command option matching the name. If the arg was optional and wasn't supplied default supplied value will be used. |
| [GetChannel](#GetChannel)(…) | Returns the [`DiscordChannel`](../Channels/DiscordChannel.md) that was resolved from the command. |
| [GetFloat](#GetFloat)(…) | Returns the float value supplied to command option matching the name. If the arg was optional and wasn't supplied default supplied value will be used. |
| [GetInt](#GetInt)(…) | Returns the int value supplied to command option matching the name. If the arg was optional and wasn't supplied default supplied value will be used. |
| [GetNumber](#GetNumber)(…) | Returns the double value supplied to command option matching the name. If the arg was optional and wasn't supplied default supplied value will be used. |
| [GetRole](#GetRole)(…) | Returns the [`DiscordRole`](../Permissions/DiscordRole.md) that was resolved from the command. |
| [GetString](#GetString)(…) | Returns the string value supplied to command option matching the name. If the arg was optional and wasn't supplied default supplied value will be used. |
| [GetUser](#GetUser)(…) | Returns the [`DiscordUser`](../Users/DiscordUser.md) that was resolved from the command. |
| [HasArg](#HasArg)(…) | Returns if a given arg exists |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [InteractionDataArgs.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Interactions/InteractionDataArgs.cs)
   
   
# HasArg method

Returns if a given arg exists

```csharp
public bool HasArg(string name)
```

| parameter | description |
| --- | --- |
| name |  |

## See Also

* class [InteractionDataArgs](./InteractionDataArgs.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetString method

Returns the string value supplied to command option matching the name. If the arg was optional and wasn't supplied default supplied value will be used.

```csharp
public string GetString(string name, string @default = "")
```

| parameter | description |
| --- | --- |
| name | Name of the command option |
| default | Default value to return if not supplied |

## Return Value

String for the matching command option name

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if the option type is not a string |

## See Also

* class [InteractionDataArgs](./InteractionDataArgs.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetInt method

Returns the int value supplied to command option matching the name. If the arg was optional and wasn't supplied default supplied value will be used.

```csharp
public int GetInt(string name, int @default = 0)
```

| parameter | description |
| --- | --- |
| name | Name of the command option |
| default | Default value to return if not supplied |

## Return Value

Int for the matching command option name

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if the option type is not an int |

## See Also

* class [InteractionDataArgs](./InteractionDataArgs.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetBool method

Returns the bool value supplied to command option matching the name. If the arg was optional and wasn't supplied default supplied value will be used.

```csharp
public bool GetBool(string name, bool @default = false)
```

| parameter | description |
| --- | --- |
| name | Name of the command option |
| default | Default value to return if not supplied |

## Return Value

Bool for the matching command option name

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if the option type is not a bool |

## See Also

* class [InteractionDataArgs](./InteractionDataArgs.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetUser method

Returns the [`DiscordUser`](../Users/DiscordUser.md) that was resolved from the command.

```csharp
public DiscordUser GetUser(string name)
```

| parameter | description |
| --- | --- |
| name | Name of the command option |

## Return Value

[`DiscordUser`](../Users/DiscordUser.md) resolved for the matching command option name

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if the option type is not a [`DiscordUser`](../Users/DiscordUser.md) or mentionable |

## See Also

* class [DiscordUser](../Users/DiscordUser.md)
* class [InteractionDataArgs](./InteractionDataArgs.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetChannel method

Returns the [`DiscordChannel`](../Channels/DiscordChannel.md) that was resolved from the command.

```csharp
public DiscordChannel GetChannel(string name)
```

| parameter | description |
| --- | --- |
| name | Name of the command option |

## Return Value

[`DiscordChannel`](../Channels/DiscordChannel.md) resolved for the matching command option name

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if the option type is not a [`DiscordChannel`](../Channels/DiscordChannel.md) |

## See Also

* class [DiscordChannel](../Channels/DiscordChannel.md)
* class [InteractionDataArgs](./InteractionDataArgs.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetRole method

Returns the [`DiscordRole`](../Permissions/DiscordRole.md) that was resolved from the command.

```csharp
public DiscordRole GetRole(string name)
```

| parameter | description |
| --- | --- |
| name | Name of the command option |

## Return Value

[`DiscordRole`](../Permissions/DiscordRole.md) resolved for the matching command option name

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if the option type is not a [`DiscordRole`](../Permissions/DiscordRole.md) or mentionable |

## See Also

* class [DiscordRole](../Permissions/DiscordRole.md)
* class [InteractionDataArgs](./InteractionDataArgs.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetNumber method

Returns the double value supplied to command option matching the name. If the arg was optional and wasn't supplied default supplied value will be used.

```csharp
public double GetNumber(string name, double @default = 0m)
```

| parameter | description |
| --- | --- |
| name | Name of the command option |
| default | Default value to return if not supplied |

## Return Value

double for the matching command option name

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if the option type is not a double |

## See Also

* class [InteractionDataArgs](./InteractionDataArgs.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetFloat method

Returns the float value supplied to command option matching the name. If the arg was optional and wasn't supplied default supplied value will be used.

```csharp
public float GetFloat(string name, float @default = 0)
```

| parameter | description |
| --- | --- |
| name | Name of the command option |
| default | Default value to return if not supplied |

## Return Value

double for the matching command option name

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if the option type is not a double |

## See Also

* class [InteractionDataArgs](./InteractionDataArgs.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
