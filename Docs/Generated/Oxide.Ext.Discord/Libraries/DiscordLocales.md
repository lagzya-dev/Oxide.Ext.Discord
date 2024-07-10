# DiscordLocales class

Converts discord locale codes into oxide locale codes

```csharp
public class DiscordLocales : BaseDiscordLibrary<DiscordLocales>
```

## Public Members

| name | description |
| --- | --- |
| [ServerLanguage](#serverlanguage-property) { get; } | Returns the Oxide Server language |
| [AddDiscordLocale](#adddiscordlocale-method)(…) | Adds a one way [`DiscordLocale`](./DiscordLocale.md) -&gt; [`ServerLocale`](./ServerLocale.md) mapping |
| [AddOxideLocale](#addoxidelocale-method)(…) | Adds a one way [`ServerLocale`](./ServerLocale.md) -&gt; [`DiscordLocale`](./DiscordLocale.md) mapping |
| [Contains](#contains-method-1-of-2)(…) | Returns if the [`ServerLocale`](./ServerLocale.md) mapping exists (2 methods) |
| [GetDiscordInteractionLangMessage](#getdiscordinteractionlangmessage-method-1-of-2)(…) | Retrieves the lang message for a Discord Interaction (2 methods) |
| [GetDiscordLocale](#getdiscordlocale-method)(…) | Returns the discord locale for a given oxide locale |
| [GetDiscordLocalizations](#getdiscordlocalizations-method)(…) | Returns all the discord localizations for a specific lang key in a plugin |
| [GetPlayerLanguage](#getplayerlanguage-method-1-of-2)(…) | Returns the oxide locale for the given IPlayer (2 methods) |
| [GetServerLanguage](#getserverlanguage-method)(…) | Returns the oxide locale for a given discord locale |
| const [DefaultServerLanguage](#defaultserverlanguage-field) | Default Oxide Lang (English) |

## Protected Members

| name | description |
| --- | --- |
| override [OnPluginUnloaded](#onpluginunloaded-method)(…) |  |

## See Also

* class [BaseDiscordLibrary&lt;TLibrary&gt;](./BaseDiscordLibrary%7BTLibrary%7D.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [DiscordLocales.cs](../../../../Oxide.Ext.Discord/Libraries/DiscordLocales.cs)
   
   
# AddOxideLocale method

Adds a one way [`ServerLocale`](./ServerLocale.md) -&gt; [`DiscordLocale`](./DiscordLocale.md) mapping

```csharp
public void AddOxideLocale(ServerLocale serverLang, DiscordLocale discordLang)
```

| parameter | description |
| --- | --- |
| serverLang |  |
| discordLang |  |

## See Also

* struct [ServerLocale](./ServerLocale.md)
* struct [DiscordLocale](./DiscordLocale.md)
* class [DiscordLocales](./DiscordLocales.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AddDiscordLocale method

Adds a one way [`DiscordLocale`](./DiscordLocale.md) -&gt; [`ServerLocale`](./ServerLocale.md) mapping

```csharp
public void AddDiscordLocale(DiscordLocale discordLang, ServerLocale serverLang)
```

| parameter | description |
| --- | --- |
| discordLang |  |
| serverLang |  |

## See Also

* struct [DiscordLocale](./DiscordLocale.md)
* struct [ServerLocale](./ServerLocale.md)
* class [DiscordLocales](./DiscordLocales.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Contains method (1 of 2)

Returns if the [`DiscordLocale`](./DiscordLocale.md) mapping exists

```csharp
public bool Contains(DiscordLocale locale)
```

| parameter | description |
| --- | --- |
| locale |  |

## See Also

* struct [DiscordLocale](./DiscordLocale.md)
* class [DiscordLocales](./DiscordLocales.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Contains method (2 of 2)

Returns if the [`ServerLocale`](./ServerLocale.md) mapping exists

```csharp
public bool Contains(ServerLocale locale)
```

| parameter | description |
| --- | --- |
| locale |  |

## See Also

* struct [ServerLocale](./ServerLocale.md)
* class [DiscordLocales](./DiscordLocales.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetServerLanguage method

Returns the oxide locale for a given discord locale

```csharp
public ServerLocale GetServerLanguage(DiscordLocale discordLocale)
```

| parameter | description |
| --- | --- |
| discordLocale | Discord locale to get oxide locale for |

## Return Value

Oxide locale if it exists; null otherwise

## See Also

* struct [ServerLocale](./ServerLocale.md)
* struct [DiscordLocale](./DiscordLocale.md)
* class [DiscordLocales](./DiscordLocales.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetDiscordLocale method

Returns the discord locale for a given oxide locale

```csharp
public DiscordLocale GetDiscordLocale(ServerLocale serverLocale)
```

| parameter | description |
| --- | --- |
| serverLocale | oxide locale to get discord locale for |

## Return Value

Discord locale if it exists; null otherwise

## See Also

* struct [DiscordLocale](./DiscordLocale.md)
* struct [ServerLocale](./ServerLocale.md)
* class [DiscordLocales](./DiscordLocales.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetPlayerLanguage method (1 of 2)

Returns the oxide locale for the given IPlayer

```csharp
public ServerLocale GetPlayerLanguage(IPlayer player)
```

| parameter | description |
| --- | --- |
| player | IPlayer to get the locale for |

## Return Value

Locale for the given IPlayer

## See Also

* struct [ServerLocale](./ServerLocale.md)
* class [DiscordLocales](./DiscordLocales.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# GetPlayerLanguage method (2 of 2)

Returns the oxide locale for the given playerId

```csharp
public ServerLocale GetPlayerLanguage(string playerId)
```

| parameter | description |
| --- | --- |
| playerId | PlayerId to get the locale for |

## Return Value

Locale for the given playerId

## See Also

* struct [ServerLocale](./ServerLocale.md)
* class [DiscordLocales](./DiscordLocales.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetDiscordLocalizations method

Returns all the discord localizations for a specific lang key in a plugin

```csharp
public Hash<string, string> GetDiscordLocalizations(Plugin plugin, string langKey)
```

| parameter | description |
| --- | --- |
| plugin |  |
| langKey |  |

## See Also

* class [DiscordLocales](./DiscordLocales.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetDiscordInteractionLangMessage method (1 of 2)

Retrieves the lang message for a Discord Interaction

```csharp
public string GetDiscordInteractionLangMessage(Plugin plugin, DiscordInteraction interaction, 
    string langKey)
```

| parameter | description |
| --- | --- |
| plugin | Plugin the lang is from |
| interaction | The interaction to be localized |
| langKey | The lang key to lookup |

## Return Value

Localized message if found; Empty string otherwise

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if any of the input arguments are null |

## See Also

* class [DiscordInteraction](../Entities/DiscordInteraction.md)
* class [DiscordLocales](./DiscordLocales.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# GetDiscordInteractionLangMessage method (2 of 2)

Retrieves the lang message for a Discord Interaction

```csharp
public string GetDiscordInteractionLangMessage(Plugin plugin, DiscordInteraction interaction, 
    string langKey, params object[] args)
```

| parameter | description |
| --- | --- |
| plugin | Plugin the lang is from |
| interaction | The interaction to be localized |
| langKey | The lang key to lookup |
| args | Localization formatting args |

## Return Value

Localized message if found; Empty string otherwise

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if any of the input arguments are null |

## See Also

* class [DiscordInteraction](../Entities/DiscordInteraction.md)
* class [DiscordLocales](./DiscordLocales.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# OnPluginUnloaded method

```csharp
protected override void OnPluginUnloaded(Plugin plugin)
```

## See Also

* class [DiscordLocales](./DiscordLocales.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ServerLanguage property

Returns the Oxide Server language

```csharp
public ServerLocale ServerLanguage { get; }
```

## See Also

* struct [ServerLocale](./ServerLocale.md)
* class [DiscordLocales](./DiscordLocales.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DefaultServerLanguage field

Default Oxide Lang (English)

```csharp
public const string DefaultServerLanguage;
```

## See Also

* class [DiscordLocales](./DiscordLocales.md)
* namespace [Oxide.Ext.Discord.Libraries](./LibrariesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
