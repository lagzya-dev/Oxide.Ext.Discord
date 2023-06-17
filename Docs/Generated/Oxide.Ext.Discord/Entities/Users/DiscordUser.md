# DiscordUser class

Represents [User Structure](https://discord.com/developers/docs/resources/user#user-object)

```csharp
public class DiscordUser : IDebugLoggable, IDiscordCacheable<DiscordUser>, ISnowflakeEntity
```

## Public Members

| name | description |
| --- | --- |
| [DiscordUser](DiscordUser/DiscordUser.md)() | The default constructor. |
| [AccentColor](DiscordUser/AccentColor.md) { get; set; } | The user's banner color encoded as an integer representation of hexadecimal color code |
| [Avatar](DiscordUser/Avatar.md) { get; set; } | The user's avatar hash |
| [Banner](DiscordUser/Banner.md) { get; set; } | The user's banner, or null if unset |
| [Bot](DiscordUser/Bot.md) { get; set; } | Whether the user belongs to an OAuth2 application |
| [Discriminator](DiscordUser/Discriminator.md) { get; set; } | The user's 4-digit discord-tag |
| [DisplayName](DiscordUser/DisplayName.md) { get; } | The display name for the user |
| [Email](DiscordUser/Email.md) { get; set; } | The user's email |
| [Flags](DiscordUser/Flags.md) { get; set; } | The flags on a user's account [`UserFlags`](./UserFlags.md) |
| [FullUserName](DiscordUser/FullUserName.md) { get; } | Returns the username#discriminator for the user |
| [GetAvatarUrl](DiscordUser/GetAvatarUrl.md) { get; } | Avatar Url for the user |
| [GetBannerUrl](DiscordUser/GetBannerUrl.md) { get; } | Banner Url for the user |
| [GetDefaultAvatarUrl](DiscordUser/GetDefaultAvatarUrl.md) { get; } | Default Avatar Url for the User |
| [GlobalName](DiscordUser/GlobalName.md) { get; set; } | The user's global name |
| [HasUpdatedUsername](DiscordUser/HasUpdatedUsername.md) { get; } | Returns true if the user has upgraded their username to the new username format |
| [Id](DiscordUser/Id.md) { get; set; } | The user's id |
| [IsBot](DiscordUser/IsBot.md) { get; } | Returns if the DiscordUser is a bot |
| [IsSystem](DiscordUser/IsSystem.md) { get; } | Returns if the DiscordUser is a system user |
| [Locale](DiscordUser/Locale.md) { get; set; } | The user's chosen language option |
| [Mention](DiscordUser/Mention.md) { get; } | Returns a string to mention this users nickname in a message |
| [MfaEnabled](DiscordUser/MfaEnabled.md) { get; set; } | Whether the user has two factor enabled on their account |
| [Player](DiscordUser/Player.md) { get; } | Returns the IPlayer for the discord user if linked; null otherwise |
| [PremiumType](DiscordUser/PremiumType.md) { get; set; } | The type of Nitro subscription on a user's account [`UserPremiumType`](./UserPremiumType.md) |
| [PublicFlags](DiscordUser/PublicFlags.md) { get; set; } | The public flags on a user's account [`UserFlags`](./UserFlags.md) |
| [System](DiscordUser/System.md) { get; set; } | Whether the user is an Official Discord System user (part of the urgent message system) |
| [Username](DiscordUser/Username.md) { get; set; } | The user's username, not unique across the platform |
| [Verified](DiscordUser/Verified.md) { get; set; } | Whether the email on this account has been verified |
| [CreateDirectMessageChannel](DiscordUser/CreateDirectMessageChannel.md)(…) | Create a Direct Message to the current User See [Create DM](https://discord.com/developers/docs/resources/user#create-dm) |
| [CreateGroupDm](DiscordUser/CreateGroupDm.md)(…) | Create a Group Direct Message |
| [GetCurrentUserGuilds](DiscordUser/GetCurrentUserGuilds.md)(…) | Returns the guilds for the currently logged in user See [Get Current User Guilds](https://discord.com/developers/docs/resources/user#get-current-user-guilds) |
| [GetUserConnections](DiscordUser/GetUserConnections.md)(…) | Returns a list of connection objects. Requires the connections OAuth2 scope. |
| [GroupDmAddRecipient](DiscordUser/GroupDmAddRecipient.md)(…) | Adds a recipient to a Group DM using their access token See [Group DM Add Recipient](https://discord.com/developers/docs/resources/channel#group-dm-add-recipient) (2 methods) |
| [GroupDmRemoveRecipient](DiscordUser/GroupDmRemoveRecipient.md)(…) | Removes a recipient from a Group DM (2 methods) |
| [LeaveGuild](DiscordUser/LeaveGuild.md)(…) | Leave the guild that the currently logged in user is in See [Leave Guild](https://discord.com/developers/docs/resources/user#leave-guild) |
| [LogDebug](DiscordUser/LogDebug.md)(…) |  |
| [ModifyCurrentUser](DiscordUser/ModifyCurrentUser.md)(…) | Modify the currently logged in user See [Modify Current User](https://discord.com/developers/docs/resources/user#modify-current-user) |
| [SendDirectMessage](DiscordUser/SendDirectMessage.md)(…) | Send a message to a user in a direct message channel (3 methods) |
| [SendGlobalTemplateDirectMessage](DiscordUser/SendGlobalTemplateDirectMessage.md)(…) | Reply to a message using a global message template |
| [SendTemplateDirectMessage](DiscordUser/SendTemplateDirectMessage.md)(…) | Send a message in a DM to the user using a localized message template |
| [Update](DiscordUser/Update.md)(…) | Updates the user data with the passed in user |
| static [CreateDirectMessageChannel](DiscordUser/CreateDirectMessageChannel.md)(…) | Create a Direct Message to the current User See [Create DM](https://discord.com/developers/docs/resources/user#create-dm) |
| static [GetCurrentUser](DiscordUser/GetCurrentUser.md)(…) | Returns the currently logged in user account See [Get Current User](https://discord.com/developers/docs/resources/user#get-current-user) |
| static [GetUser](DiscordUser/GetUser.md)(…) | Returns the user for the given user Id See [Get User](https://discord.com/developers/docs/resources/user#get-user) |

## See Also

* interface [IDebugLoggable](../../Interfaces/Logging/IDebugLoggable.md)
* interface [IDiscordCacheable&lt;T&gt;](../../Interfaces/IDiscordCacheable-1.md)
* interface [ISnowflakeEntity](../../Interfaces/ISnowflakeEntity.md)
* namespace [Oxide.Ext.Discord.Entities.Users](./UsersNamespace.md.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordUser.cs](https://github.com/dassjosh/Oxide.Ext.Discord/Entities/Users/DiscordUser.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
