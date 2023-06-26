# Discord Placeholders

[Discord Placeholders](../Generated/Oxide.Ext.Discord/Libraries/Placeholders/DiscordPlaceholders.md)
is a library that can be used for placeholders in strings and [Discord Templates](DiscordTemplates.md).
There are many built in placeholders that can be used along with the ability for plugins to register custom placeholders.
Placeholders are highly customizable and is recommend to be used in messages along with [Discord Templates](DiscordTemplates.md).

## Placeholder Data
When a placeholder is being processed data is added to [PlaceholderData](../Generated/Oxide.Ext.Discord/Libraries/Placeholders/PlaceholderData.md) 
to be used by the placeholders that need certain types for their data. You can get a [PlaceholderData](../Generated/Oxide.Ext.Discord/Libraries/Placeholders/PlaceholderData.md)
instance by with the code below.  

*Note: PlaceholderData is pooled and once processing placeholders is complete it will be returned to the pool. If you want to use the data multiple times call
[ManualPool()](../Generated/Oxide.Ext.Discord/Libraries/Placeholders/PlaceholderData.md#manualpool-method) method  

*Note: If you need to use [PlaceholderData](../Generated/Oxide.Ext.Discord/Libraries/Placeholders/PlaceholderData.md) 
with different values you can use the [Clone()](../Generated/Oxide.Ext.Discord/Libraries/Placeholders/PlaceholderData.md#clone-method) method*  

```csharp
private readonly DiscordPlaceholders _placeholders = GetLibrary<DiscordPlaceholders>();

private PlaceholderData GetData()
{
    _placeholders.CreateData(this);
}
```

## Formatting

Depending on the return type of the placeholder there are different formatting built in options available. 
You can apply formatting to a placeholder by adding a `:` and adding the formatting to the end of the placeholder. 
Ex `{player.health:0.00}` will apply a format of `0.00` to the health. 
Formatting is supported on return type of IFormattable, string[], and object[].
For string[] and object[] the format will define the separator for the type when joined.

## Registering Custom Placeholders

*Note: All custom placeholders must be prefixed with the lowercase name of the plugin registering them follow by a period.*
For example if the plugin is DiscordCore the prefix would be `discordcore.`

### Static Placeholder

A static placeholder is a placeholder who's value will never change.

```csharp
private readonly DiscordPlaceholders _placeholders = GetLibrary<DiscordPlaceholders>();

private void RegisterPlaceholders()
{
    _placeholders.RegisterPlaceholder(this, "myplugin.name", "staticvalue");
}
```

**See:**
[RegisterPlaceholder](../Generated/Oxide.Ext.Discord/Libraries/Placeholders/DiscordPlaceholders.md#registerplaceholder-method-1-of-7)

### Data Key Placeholder
A Data Key Placeholder is a placeholder that should use a registered [PlaceholderData](../Generated/Oxide.Ext.Discord/Libraries/Placeholders/PlaceholderData.md) 
key as the placeholder value.

```csharp
private readonly DiscordPlaceholders _placeholders = GetLibrary<DiscordPlaceholders>();

private void RegisterPlaceholders()
{
    _placeholders.RegisterPlaceholder<string>(this, "myplugin.name", "datakey");
}
```

**See:**
[RegisterPlaceholder](../Generated/Oxide.Ext.Discord/Libraries/Placeholders/DiscordPlaceholders.md#registerplaceholder-method-3-of-7)

### Callback Placeholder

A Callback placeholder is a placeholder that will will callback an empty method and that remove will return a <TResult> for the placeholder.

```csharp
private readonly DiscordPlaceholders _placeholders = GetLibrary<DiscordPlaceholders>();

private void RegisterPlaceholders()
{
    _placeholders.RegisterPlaceholder<int>(this, "myplugin.name", GetConfigValue);
}

private int GetConfigValue() => _myConfig.ConfigValue;
```

### Callback<T> Placeholder

A Callback<T> placeholder is a placeholder that will call a method with the provided T value.
If the placeholder needs access to [PlaceholderState](../Generated/Oxide.Ext.Discord/Libraries/Placeholders/PlaceholderState.md)
That is an option argument that can be added

```csharp
private readonly DiscordPlaceholders _placeholders = GetLibrary<DiscordPlaceholders>();

private void RegisterPlaceholders()
{
    _placeholders.RegisterPlaceholder<ItemDefinition, int>(this, "myplugin.itemid", ItemId);
}

private string ItemId(ItemDefinition def) => def.itemid;
```

```csharp
private readonly DiscordPlaceholders _placeholders = GetLibrary<DiscordPlaceholders>();

private void RegisterPlaceholders()
{
    _placeholders.RegisterPlaceholder<ItemDefinition, int>(this, "myplugin.itemid", ItemId);
}

private string DisplayItem(PlaceholderState state, ItemDefinition def) => $"{def.displayName.english} {state.Data.Get<int>("itemamount")}x";
```

**See:**
[RegisterPlaceholder](../Generated/Oxide.Ext.Discord/Libraries/Placeholders/DiscordPlaceholders.md#registerplaceholder-method-5-of-7)

## Processing Placeholders

To process placeholders you will call the [Process Placeholders](../Generated/Oxide.Ext.Discord/Libraries/Placeholders/DiscordPlaceholders.md#processplaceholders-method) Method

```csharp
private readonly DiscordPlaceholders _placeholders = GetLibrary<DiscordPlaceholders>();

private string CallPlaceholders(IPlayer player)
{
    PlaceholderData data = _placeholders.CreateData(this).AddPlayer(player);
    return _placeholders.ProcessPlaceholders("{player.name} ({player.id})", data);
}
```

## Built In Placeholders
There are a lot of built in placeholders available from the Discord Extension. You can view all the defaults here: [Default Namespace](../Generated/Oxide.Ext.Discord/Libraries/Placeholders/Default/DefaultNamespace.md).
It is also possible to register placeholders default placeholders using a different datakey. For example you can see that on [Player Placeholders](../Generated/Oxide.Ext.Discord/Libraries/Placeholders/Default/PlayerPlaceholders.md)