# DiscordApplication class

Represents [Application Structure](https://discord.com/developers/docs/resources/application#application-object)

```csharp
public class DiscordApplication : IDebugLoggable
```

## Public Members

| name | description |
| --- | --- |
| [DiscordApplication](#DiscordApplication-constructor)() | The default constructor. |
| [BotPublic](#BotPublic-property) { get; set; } | When false only app owner can join the app's bot to guilds |
| [BotRequireCodeGrant](#BotRequireCodeGrant-property) { get; set; } | When true the app's bot will only join upon completion of the full oauth2 code grant flow |
| [CoverImage](#CoverImage-property) { get; set; } | If this application is a game sold on Discord, this field will be the hash of the image on store embeds |
| [CustomInstallUrl](#CustomInstallUrl-property) { get; set; } | The application's default custom authorization link, if enabled |
| [Description](#Description-property) { get; set; } | The description of the app |
| [Flags](#Flags-property) { get; set; } | The application's public flags |
| [GetApplicationCoverUrl](#GetApplicationCoverUrl-property) { get; } | Returns the URL for the application cover |
| [GetApplicationIconUrl](#GetApplicationIconUrl-property) { get; } | Returns the URL for the applications Icon |
| [GuildId](#GuildId-property) { get; set; } | If this application is a game sold on Discord, this field will be the guild to which it has been linked |
| [Icon](#Icon-property) { get; set; } | The icon hash of the app |
| [Id](#Id-property) { get; set; } | The id of the app |
| [InstallParams](#InstallParams-property) { get; set; } | Settings for the application's default in-app authorization link, if enabled |
| [Name](#Name-property) { get; set; } | The name of the app |
| [Owner](#Owner-property) { get; set; } | Partial user object containing info on the owner of the application |
| [PrimarySkuId](#PrimarySkuId-property) { get; set; } | If this application is a game sold on Discord, this field will be the id of the "Game SKU" that is created, if exists |
| [PrivacyPolicyUrl](#PrivacyPolicyUrl-property) { get; set; } | The url of the app's privacy policy |
| [RoleConnectionsVerificationUrl](#RoleConnectionsVerificationUrl-property) { get; set; } | The application's role connection verification entry point, which when configured will render the app as a verification method in the guild role verification configuration |
| [RpcOrigins](#RpcOrigins-property) { get; set; } | An array of rpc origin urls, if rpc is enabled |
| [Slug](#Slug-property) { get; set; } | If this application is a game sold on Discord, this field will be the URL slug that links to the store page |
| [Tags](#Tags-property) { get; set; } | Up to 5 tags describing the content and functionality of the application |
| [Team](#Team-property) { get; set; } | If the application belongs to a team, this will be a list of the members of that team |
| [TermsOfServiceUrl](#TermsOfServiceUrl-property) { get; set; } | The url of the app's terms of service |
| [Verify](#Verify-property) { get; set; } | The hex encoded key for verification in interactions and the GameSDK's GetTicket |
| [BulkOverwriteGlobalCommands](#BulkOverwriteGlobalCommands-method)(…) | Takes a list of application commands, overwriting existing commands that are registered globally for this application. Updates will be available in all guilds after 1 hour. See [Bulk Overwrite Global Application Commands](https://discord.com/developers/docs/interactions/application-commands#bulk-overwrite-global-application-commands) |
| [CreateGlobalCommand](#CreateGlobalCommand-method)(…) | Create a new global command. New global commands will be available in all guilds after 1 hour. Note: Creating a command with the same name as an existing command for your application will overwrite the old command. See [Create Global Application Command](https://discord.com/developers/docs/interactions/application-commands#create-global-application-command) |
| [CreateGuildCommand](#CreateGuildCommand-method)(…) | Create a new guild command. New guild commands will be available in the guild immediately. See [Create Guild Application Command](https://discord.com/developers/docs/interactions/application-commands#create-guild-application-command) |
| [EditRoleConnectionMetadata](#EditRoleConnectionMetadata-method)(…) | Updates and returns a list of application role connection metadata objects for the given application. See [Update Application Role Connection Metadata Records](https://discord.com/developers/docs/resources/application-role-connection-metadata#update-application-role-connection-metadata-records) |
| [GetAllCommands](#GetAllCommands-method)(…) | Returns all commands registered to this application |
| [GetGlobalCommand](#GetGlobalCommand-method)(…) | Fetch global command by ID See [Get Global Application Command](https://discord.com/developers/docs/interactions/application-commands#get-global-application-command) |
| [GetGlobalCommands](#GetGlobalCommands-method)(…) | Fetch all of the global commands for your application. Returns a list of ApplicationCommand. See [Get Global Application Commands](https://discord.com/developers/docs/interactions/application-commands#get-global-application-commands) |
| [GetGuildCommand](#GetGuildCommand-method)(…) | Get guild command by Id See [Get Guild Application Command](https://discord.com/developers/docs/interactions/application-commands#get-guild-application-command) |
| [GetGuildCommandPermissions](#GetGuildCommandPermissions-method)(…) | Fetches command permissions for all commands for your application in a guild. Returns an array of GuildApplicationCommandPermissions objects. See [Get Guild Application Command Permissions](https://discord.com/developers/docs/interactions/application-commands#get-guild-application-command-permissions) |
| [GetGuildCommands](#GetGuildCommands-method)(…) | Fetch all of the guild commands for your application for a specific guild. See [Get Guild Application Commands](https://discord.com/developers/docs/interactions/application-commands#get-guild-application-commands) |
| [GetRoleConnectionMetadata](#GetRoleConnectionMetadata-method)(…) | Returns a list of application role connection metadata objects for the given application. See [Get Application Role Connection Metadata Records](https://discord.com/developers/docs/resources/application-role-connection-metadata#get-application-role-connection-metadata-records) |
| [HasAnyApplicationFlags](#HasAnyApplicationFlags-method)(…) | Returns if the given application has any of the passed in application flags If [`Flags`](./DiscordApplication/Flags.md) is null false is returned |
| [HasApplicationFlag](#HasApplicationFlag-method)(…) | Returns if the given application has the passed in application flag If [`Flags`](./DiscordApplication/Flags.md) is null false is returned |
| [LogDebug](#LogDebug-method)(…) |  |

## See Also

* interface [IDebugLoggable](../../Interfaces/Logging/IDebugLoggable.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordApplication.cs](../../../../Oxide.Ext.Discord/Entities/Applications/DiscordApplication.cs)
   
   
# HasApplicationFlag method

Returns if the given application has the passed in application flag If [`Flags`](./DiscordApplication/Flags) is null false is returned

```csharp
public bool HasApplicationFlag(ApplicationFlags flag)
```

| parameter | description |
| --- | --- |
| flag | Flag to compare against |

## Return Value

True of application has flag; False Otherwise

## See Also

* enum [ApplicationFlags](./ApplicationFlags.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# HasAnyApplicationFlags method

Returns if the given application has any of the passed in application flags If [`Flags`](./DiscordApplication/Flags) is null false is returned

```csharp
public bool HasAnyApplicationFlags(ApplicationFlags flag)
```

| parameter | description |
| --- | --- |
| flag | Flag to compare against |

## Return Value

True of application has flag; False Otherwise

## See Also

* enum [ApplicationFlags](./ApplicationFlags.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetGlobalCommands method

Fetch all of the global commands for your application. Returns a list of ApplicationCommand. See [Get Global Application Commands](https://discord.com/developers/docs/interactions/application-commands#get-global-application-commands)

```csharp
public IPromise<List<DiscordApplicationCommand>> GetGlobalCommands(DiscordClient client, 
    bool withLocalizations = false)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| withLocalizations | Include Command Localizations |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordApplicationCommand](../Interactions/ApplicationCommands/DiscordApplicationCommand.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetGlobalCommand method

Fetch global command by ID See [Get Global Application Command](https://discord.com/developers/docs/interactions/application-commands#get-global-application-command)

```csharp
public IPromise<DiscordApplicationCommand> GetGlobalCommand(DiscordClient client, 
    Snowflake commandId)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| commandId | ID of command to get |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordApplicationCommand](../Interactions/ApplicationCommands/DiscordApplicationCommand.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* struct [Snowflake](../Snowflake.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# CreateGlobalCommand method

Create a new global command. New global commands will be available in all guilds after 1 hour. Note: Creating a command with the same name as an existing command for your application will overwrite the old command. See [Create Global Application Command](https://discord.com/developers/docs/interactions/application-commands#create-global-application-command)

```csharp
public IPromise<DiscordApplicationCommand> CreateGlobalCommand(DiscordClient client, 
    CommandCreate create)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| create | Command to create |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordApplicationCommand](../Interactions/ApplicationCommands/DiscordApplicationCommand.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* class [CommandCreate](../Interactions/ApplicationCommands/CommandCreate.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# BulkOverwriteGlobalCommands method

Takes a list of application commands, overwriting existing commands that are registered globally for this application. Updates will be available in all guilds after 1 hour. See [Bulk Overwrite Global Application Commands](https://discord.com/developers/docs/interactions/application-commands#bulk-overwrite-global-application-commands)

```csharp
public IPromise<List<DiscordApplicationCommand>> BulkOverwriteGlobalCommands(DiscordClient client, 
    List<CommandBulkOverwrite> commands)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| commands | List of commands to overwrite |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordApplicationCommand](../Interactions/ApplicationCommands/DiscordApplicationCommand.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* class [CommandBulkOverwrite](../Interactions/ApplicationCommands/CommandBulkOverwrite.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetGuildCommands method

Fetch all of the guild commands for your application for a specific guild. See [Get Guild Application Commands](https://discord.com/developers/docs/interactions/application-commands#get-guild-application-commands)

```csharp
public IPromise<List<DiscordApplicationCommand>> GetGuildCommands(DiscordClient client, 
    Snowflake guildId, bool withLocalizations = false)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| guildId | ID of the guild to get commands for |
| withLocalizations | Include Command Localizations |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordApplicationCommand](../Interactions/ApplicationCommands/DiscordApplicationCommand.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* struct [Snowflake](../Snowflake.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetGuildCommand method

Get guild command by Id See [Get Guild Application Command](https://discord.com/developers/docs/interactions/application-commands#get-guild-application-command)

```csharp
public IPromise<DiscordApplicationCommand> GetGuildCommand(DiscordClient client, Snowflake guildId, 
    Snowflake commandId)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| guildId | ID of the guild to get commands for |
| commandId | ID of the command to get |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordApplicationCommand](../Interactions/ApplicationCommands/DiscordApplicationCommand.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* struct [Snowflake](../Snowflake.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# CreateGuildCommand method

Create a new guild command. New guild commands will be available in the guild immediately. See [Create Guild Application Command](https://discord.com/developers/docs/interactions/application-commands#create-guild-application-command)

```csharp
public IPromise<DiscordApplicationCommand> CreateGuildCommand(DiscordClient client, 
    Snowflake guildId, CommandCreate create)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| guildId | Guild ID to create the command in |
| create | Command to create |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordApplicationCommand](../Interactions/ApplicationCommands/DiscordApplicationCommand.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* struct [Snowflake](../Snowflake.md)
* class [CommandCreate](../Interactions/ApplicationCommands/CommandCreate.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetGuildCommandPermissions method

Fetches command permissions for all commands for your application in a guild. Returns an array of GuildApplicationCommandPermissions objects. See [Get Guild Application Command Permissions](https://discord.com/developers/docs/interactions/application-commands#get-guild-application-command-permissions)

```csharp
public IPromise<List<GuildCommandPermissions>> GetGuildCommandPermissions(DiscordClient client, 
    Snowflake guildId)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| guildId | Guild ID to get the permissions from |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [GuildCommandPermissions](../Interactions/ApplicationCommands/GuildCommandPermissions.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* struct [Snowflake](../Snowflake.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetAllCommands method

Returns all commands registered to this application

```csharp
public IPromise<List<DiscordApplicationCommand>> GetAllCommands(DiscordClient client, 
    bool withLocalizations = false)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| withLocalizations | Should the response include localizations |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordApplicationCommand](../Interactions/ApplicationCommands/DiscordApplicationCommand.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetRoleConnectionMetadata method

Returns a list of application role connection metadata objects for the given application. See [Get Application Role Connection Metadata Records](https://discord.com/developers/docs/resources/application-role-connection-metadata#get-application-role-connection-metadata-records)

```csharp
public IPromise<List<ApplicationRoleConnectionMetadata>> GetRoleConnectionMetadata(
    DiscordClient client)
```

| parameter | description |
| --- | --- |
| client | Client to use |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [ApplicationRoleConnectionMetadata](./RoleConnection/ApplicationRoleConnectionMetadata.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# EditRoleConnectionMetadata method

Updates and returns a list of application role connection metadata objects for the given application. See [Update Application Role Connection Metadata Records](https://discord.com/developers/docs/resources/application-role-connection-metadata#update-application-role-connection-metadata-records)

```csharp
public IPromise<List<ApplicationRoleConnectionMetadata>> EditRoleConnectionMetadata(
    DiscordClient client, List<ApplicationRoleConnectionMetadata> records)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| records | The records to update on the application |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [ApplicationRoleConnectionMetadata](./RoleConnection/ApplicationRoleConnectionMetadata.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# LogDebug method

```csharp
public void LogDebug(DebugLogger logger)
```

## See Also

* class [DebugLogger](../../Logging/DebugLogger.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# DiscordApplication constructor

The default constructor.

```csharp
public DiscordApplication()
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Id property

The id of the app

```csharp
public Snowflake Id { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Name property

The name of the app

```csharp
public string Name { get; set; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Icon property

The icon hash of the app

```csharp
public string Icon { get; set; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Description property

The description of the app

```csharp
public string Description { get; set; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RpcOrigins property

An array of rpc origin urls, if rpc is enabled

```csharp
public List<string> RpcOrigins { get; set; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# BotPublic property

When false only app owner can join the app's bot to guilds

```csharp
public bool BotPublic { get; set; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# BotRequireCodeGrant property

When true the app's bot will only join upon completion of the full oauth2 code grant flow

```csharp
public bool BotRequireCodeGrant { get; set; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# TermsOfServiceUrl property

The url of the app's terms of service

```csharp
public string TermsOfServiceUrl { get; set; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# PrivacyPolicyUrl property

The url of the app's privacy policy

```csharp
public string PrivacyPolicyUrl { get; set; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Owner property

Partial user object containing info on the owner of the application

```csharp
public DiscordUser Owner { get; set; }
```

## See Also

* class [DiscordUser](../Users/DiscordUser.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Verify property

The hex encoded key for verification in interactions and the GameSDK's GetTicket

```csharp
public string Verify { get; set; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Team property

If the application belongs to a team, this will be a list of the members of that team

```csharp
public DiscordTeam Team { get; set; }
```

## See Also

* class [DiscordTeam](../Teams/DiscordTeam.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GuildId property

If this application is a game sold on Discord, this field will be the guild to which it has been linked

```csharp
public Snowflake? GuildId { get; set; }
```

## See Also

* struct [Snowflake](../Snowflake.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# PrimarySkuId property

If this application is a game sold on Discord, this field will be the id of the "Game SKU" that is created, if exists

```csharp
public string PrimarySkuId { get; set; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Slug property

If this application is a game sold on Discord, this field will be the URL slug that links to the store page

```csharp
public string Slug { get; set; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# CoverImage property

If this application is a game sold on Discord, this field will be the hash of the image on store embeds

```csharp
public string CoverImage { get; set; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Flags property

The application's public flags

```csharp
public ApplicationFlags? Flags { get; set; }
```

## See Also

* enum [ApplicationFlags](./ApplicationFlags.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Tags property

Up to 5 tags describing the content and functionality of the application

```csharp
public List<string> Tags { get; set; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# InstallParams property

Settings for the application's default in-app authorization link, if enabled

```csharp
public InstallParams InstallParams { get; set; }
```

## See Also

* class [InstallParams](./InstallParams.md)
* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# CustomInstallUrl property

The application's default custom authorization link, if enabled

```csharp
public string CustomInstallUrl { get; set; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# RoleConnectionsVerificationUrl property

The application's role connection verification entry point, which when configured will render the app as a verification method in the guild role verification configuration

```csharp
public string RoleConnectionsVerificationUrl { get; set; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetApplicationIconUrl property

Returns the URL for the applications Icon

```csharp
public string GetApplicationIconUrl { get; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GetApplicationCoverUrl property

Returns the URL for the application cover

```csharp
public string GetApplicationCoverUrl { get; }
```

## See Also

* class [DiscordApplication](./DiscordApplication.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
