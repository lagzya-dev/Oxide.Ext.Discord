# DiscordApplication class

Represents [Application Structure](https://discord.com/developers/docs/resources/application#application-object)

```csharp
public class DiscordApplication : IDebugLoggable
```

## Public Members

| name | description |
| --- | --- |
| [DiscordApplication](DiscordApplication/DiscordApplication.md)() | The default constructor. |
| [BotPublic](DiscordApplication/BotPublic.md) { get; set; } | When false only app owner can join the app's bot to guilds |
| [BotRequireCodeGrant](DiscordApplication/BotRequireCodeGrant.md) { get; set; } | When true the app's bot will only join upon completion of the full oauth2 code grant flow |
| [CoverImage](DiscordApplication/CoverImage.md) { get; set; } | If this application is a game sold on Discord, this field will be the hash of the image on store embeds |
| [CustomInstallUrl](DiscordApplication/CustomInstallUrl.md) { get; set; } | The application's default custom authorization link, if enabled |
| [Description](DiscordApplication/Description.md) { get; set; } | The description of the app |
| [Flags](DiscordApplication/Flags.md) { get; set; } | The application's public flags |
| [GetApplicationCoverUrl](DiscordApplication/GetApplicationCoverUrl.md) { get; } | Returns the URL for the application cover |
| [GetApplicationIconUrl](DiscordApplication/GetApplicationIconUrl.md) { get; } | Returns the URL for the applications Icon |
| [GuildId](DiscordApplication/GuildId.md) { get; set; } | If this application is a game sold on Discord, this field will be the guild to which it has been linked |
| [Icon](DiscordApplication/Icon.md) { get; set; } | The icon hash of the app |
| [Id](DiscordApplication/Id.md) { get; set; } | The id of the app |
| [InstallParams](DiscordApplication/InstallParams.md) { get; set; } | Settings for the application's default in-app authorization link, if enabled |
| [Name](DiscordApplication/Name.md) { get; set; } | The name of the app |
| [Owner](DiscordApplication/Owner.md) { get; set; } | Partial user object containing info on the owner of the application |
| [PrimarySkuId](DiscordApplication/PrimarySkuId.md) { get; set; } | If this application is a game sold on Discord, this field will be the id of the "Game SKU" that is created, if exists |
| [PrivacyPolicyUrl](DiscordApplication/PrivacyPolicyUrl.md) { get; set; } | The url of the app's privacy policy |
| [RoleConnectionsVerificationUrl](DiscordApplication/RoleConnectionsVerificationUrl.md) { get; set; } | The application's role connection verification entry point, which when configured will render the app as a verification method in the guild role verification configuration |
| [RpcOrigins](DiscordApplication/RpcOrigins.md) { get; set; } | An array of rpc origin urls, if rpc is enabled |
| [Slug](DiscordApplication/Slug.md) { get; set; } | If this application is a game sold on Discord, this field will be the URL slug that links to the store page |
| [Tags](DiscordApplication/Tags.md) { get; set; } | Up to 5 tags describing the content and functionality of the application |
| [Team](DiscordApplication/Team.md) { get; set; } | If the application belongs to a team, this will be a list of the members of that team |
| [TermsOfServiceUrl](DiscordApplication/TermsOfServiceUrl.md) { get; set; } | The url of the app's terms of service |
| [Verify](DiscordApplication/Verify.md) { get; set; } | The hex encoded key for verification in interactions and the GameSDK's GetTicket |
| [BulkOverwriteGlobalCommands](DiscordApplication/BulkOverwriteGlobalCommands.md)(…) | Takes a list of application commands, overwriting existing commands that are registered globally for this application. Updates will be available in all guilds after 1 hour. See [Bulk Overwrite Global Application Commands](https://discord.com/developers/docs/interactions/application-commands#bulk-overwrite-global-application-commands) |
| [CreateGlobalCommand](DiscordApplication/CreateGlobalCommand.md)(…) | Create a new global command. New global commands will be available in all guilds after 1 hour. Note: Creating a command with the same name as an existing command for your application will overwrite the old command. See [Create Global Application Command](https://discord.com/developers/docs/interactions/application-commands#create-global-application-command) |
| [CreateGuildCommand](DiscordApplication/CreateGuildCommand.md)(…) | Create a new guild command. New guild commands will be available in the guild immediately. See [Create Guild Application Command](https://discord.com/developers/docs/interactions/application-commands#create-guild-application-command) |
| [EditRoleConnectionMetadata](DiscordApplication/EditRoleConnectionMetadata.md)(…) | Updates and returns a list of application role connection metadata objects for the given application. See [Update Application Role Connection Metadata Records](https://discord.com/developers/docs/resources/application-role-connection-metadata#update-application-role-connection-metadata-records) |
| [GetAllCommands](DiscordApplication/GetAllCommands.md)(…) | Returns all commands registered to this application |
| [GetGlobalCommand](DiscordApplication/GetGlobalCommand.md)(…) | Fetch global command by ID See [Get Global Application Command](https://discord.com/developers/docs/interactions/application-commands#get-global-application-command) |
| [GetGlobalCommands](DiscordApplication/GetGlobalCommands.md)(…) | Fetch all of the global commands for your application. Returns a list of ApplicationCommand. See [Get Global Application Commands](https://discord.com/developers/docs/interactions/application-commands#get-global-application-commands) |
| [GetGuildCommand](DiscordApplication/GetGuildCommand.md)(…) | Get guild command by Id See [Get Guild Application Command](https://discord.com/developers/docs/interactions/application-commands#get-guild-application-command) |
| [GetGuildCommandPermissions](DiscordApplication/GetGuildCommandPermissions.md)(…) | Fetches command permissions for all commands for your application in a guild. Returns an array of GuildApplicationCommandPermissions objects. See [Get Guild Application Command Permissions](https://discord.com/developers/docs/interactions/application-commands#get-guild-application-command-permissions) |
| [GetGuildCommands](DiscordApplication/GetGuildCommands.md)(…) | Fetch all of the guild commands for your application for a specific guild. See [Get Guild Application Commands](https://discord.com/developers/docs/interactions/application-commands#get-guild-application-commands) |
| [GetRoleConnectionMetadata](DiscordApplication/GetRoleConnectionMetadata.md)(…) | Returns a list of application role connection metadata objects for the given application. See [Get Application Role Connection Metadata Records](https://discord.com/developers/docs/resources/application-role-connection-metadata#get-application-role-connection-metadata-records) |
| [HasAnyApplicationFlags](DiscordApplication/HasAnyApplicationFlags.md)(…) | Returns if the given application has any of the passed in application flags If [`Flags`](./DiscordApplication/Flags.md) is null false is returned |
| [HasApplicationFlag](DiscordApplication/HasApplicationFlag.md)(…) | Returns if the given application has the passed in application flag If [`Flags`](./DiscordApplication/Flags.md) is null false is returned |
| [LogDebug](DiscordApplication/LogDebug.md)(…) |  |

## See Also

* interface [IDebugLoggable](../../Interfaces/Logging/IDebugLoggable.md)
* namespace [Oxide.Ext.Discord.Entities.Applications](./ApplicationsNamespace.md.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordApplication.cs](https://github.com/dassjosh/Oxide.Ext.Discord/Entities/Applications/DiscordApplication.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
