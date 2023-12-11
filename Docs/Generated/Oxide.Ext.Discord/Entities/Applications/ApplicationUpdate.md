# ApplicationUpdate class

Represents [Edit Application Structure](https://discord.com/developers/docs/resources/application#edit-current-application-json-params)

```csharp
public class ApplicationUpdate
```

## Public Members

| name | description |
| --- | --- |
| [ApplicationUpdate](#applicationupdate-constructor)() | The default constructor. |
| [CoverImage](#coverimage-property) { get; set; } | Default rich presence invite cover image for the app |
| [CustomInstallUrl](#custominstallurl-property) { get; set; } | Default custom authorization URL for the app, if enabled |
| [Description](#description-property) { get; set; } | Description of the app |
| [Flags](#flags-property) { get; set; } | App's public flags |
| [Icon](#icon-property) { get; set; } | Icon for the app |
| [InstallParams](#installparams-property) { get; set; } | Settings for the application's default in-app authorization link, if enabled |
| [InteractionsEndpointUrl](#interactionsendpointurl-property) { get; set; } | Interactions endpoint URL for the app |
| [RoleConnectionsVerificationUrl](#roleconnectionsverificationurl-property) { get; set; } | Role connection verification URL for the app |
| [Tags](#tags-property) { get; set; } | List of tags describing the content and functionality of the app (max of 20 characters per tag). Max of 5 tags. |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [ApplicationUpdate.cs](../../../../Oxide.Ext.Discord/Entities/Applications/ApplicationUpdate.cs)
   
   
# ApplicationUpdate constructor

The default constructor.

```csharp
public ApplicationUpdate()
```

## See Also

* class [ApplicationUpdate](./ApplicationUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# CustomInstallUrl property

Default custom authorization URL for the app, if enabled

```csharp
public string CustomInstallUrl { get; set; }
```

## See Also

* class [ApplicationUpdate](./ApplicationUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Description property

Description of the app

```csharp
public string Description { get; set; }
```

## See Also

* class [ApplicationUpdate](./ApplicationUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RoleConnectionsVerificationUrl property

Role connection verification URL for the app

```csharp
public string RoleConnectionsVerificationUrl { get; set; }
```

## See Also

* class [ApplicationUpdate](./ApplicationUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# InstallParams property

Settings for the application's default in-app authorization link, if enabled

```csharp
public InstallParams InstallParams { get; set; }
```

## See Also

* class [InstallParams](./InstallParams.md)
* class [ApplicationUpdate](./ApplicationUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Flags property

App's public flags

```csharp
public ApplicationFlags? Flags { get; set; }
```

## See Also

* enum [ApplicationFlags](./ApplicationFlags.md)
* class [ApplicationUpdate](./ApplicationUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Icon property

Icon for the app

```csharp
public DiscordImageData? Icon { get; set; }
```

## See Also

* struct [DiscordImageData](../Images/DiscordImageData.md)
* class [ApplicationUpdate](./ApplicationUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# CoverImage property

Default rich presence invite cover image for the app

```csharp
public DiscordImageData? CoverImage { get; set; }
```

## See Also

* struct [DiscordImageData](../Images/DiscordImageData.md)
* class [ApplicationUpdate](./ApplicationUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# InteractionsEndpointUrl property

Interactions endpoint URL for the app

```csharp
public string InteractionsEndpointUrl { get; set; }
```

## See Also

* class [ApplicationUpdate](./ApplicationUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Tags property

List of tags describing the content and functionality of the app (max of 20 characters per tag). Max of 5 tags.

```csharp
public List<string> Tags { get; set; }
```

## See Also

* class [ApplicationUpdate](./ApplicationUpdate.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
