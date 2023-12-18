# DiscordUser class

Represents [User Structure](https://discord.com/developers/docs/resources/user#user-object)

```csharp
public class DiscordUser : IDebugLoggable, IDiscordCacheable<DiscordUser>, ISnowflakeEntity
```

## Public Members

| name | description |
| --- | --- |
| [DiscordUser](#discorduser-constructor)() | The default constructor. |
| [AccentColor](#accentcolor-property) { get; set; } | The user's banner color encoded as an integer representation of hexadecimal color code |
| [Avatar](#avatar-property) { get; set; } | The user's avatar hash |
| [AvatarDecoration](#avatardecoration-property) { get; set; } | The user's avatar decoration hash |
| [Banner](#banner-property) { get; set; } | The user's banner, or null if unset |
| [Bot](#bot-property) { get; set; } | Whether the user belongs to an OAuth2 application |
| [Discriminator](#discriminator-property) { get; set; } | The user's 4-digit discord-tag |
| [DisplayName](#displayname-property) { get; } | The display name for the user |
| [Email](#email-property) { get; set; } | The user's email |
| [Flags](#flags-property) { get; set; } | The flags on a user's account [`UserFlags`](./UserFlags.md) |
| [FullUserName](#fullusername-property) { get; } | Returns the username#discriminator for the user |
| [GetAvatarDecorationUrl](#getavatardecorationurl-property) { get; } | Avatar Decoration Url for the user |
| [GetAvatarUrl](#getavatarurl-property) { get; } | Avatar Url for the user |
| [GetBannerUrl](#getbannerurl-property) { get; } | Banner Url for the user |
| [GetDefaultAvatarUrl](#getdefaultavatarurl-property) { get; } | Default Avatar Url for the User |
| [GlobalName](#globalname-property) { get; set; } | The user's global name |
| [HasUpdatedUsername](#hasupdatedusername-property) { get; } | Returns true if the user has upgraded their username to the new username format |
| [Id](#id-property) { get; set; } | The user's id |
| [IsBot](#isbot-property) { get; } | Returns if the DiscordUser is a bot |
| [IsSystem](#issystem-property) { get; } | Returns if the DiscordUser is a system user |
| [Locale](#locale-property) { get; set; } | The user's chosen language option |
| [Mention](#mention-property) { get; } | Returns a string to mention this users nickname in a message |
| [MfaEnabled](#mfaenabled-property) { get; set; } | Whether the user has two factor enabled on their account |
| [Player](#player-property) { get; } | Returns the IPlayer for the discord user if linked; null otherwise |
| [PremiumType](#premiumtype-property) { get; set; } | The type of Nitro subscription on a user's account [`UserPremiumType`](./UserPremiumType.md) |
| [PublicFlags](#publicflags-property) { get; set; } | The public flags on a user's account [`UserFlags`](./UserFlags.md) |
| [System](#system-property) { get; set; } | Whether the user is an Official Discord System user (part of the urgent message system) |
| [Username](#username-property) { get; set; } | The user's username, not unique across the platform |
| [Verified](#verified-property) { get; set; } | Whether the email on this account has been verified |
| [CreateDirectMessageChannel](#createdirectmessagechannel-method)(…) | Create a Direct Message to the current User See [Create DM](https://discord.com/developers/docs/resources/user#create-dm) |
| [CreateGroupDm](#creategroupdm-method)(…) | Create a Group Direct Message |
| [GetCurrentUserGuilds](#getcurrentuserguilds-method)(…) | Returns the guilds for the currently logged in user See [Get Current User Guilds](https://discord.com/developers/docs/resources/user#get-current-user-guilds) |
| [GetUserConnections](#getuserconnections-method)(…) | Returns a list of connection objects. Requires the connections OAuth2 scope. |
| [GroupDmAddRecipient](#groupdmaddrecipient-method-1-of-2)(…) | Adds a recipient to a Group DM using their access token See [Group DM Add Recipient](https://discord.com/developers/docs/resources/channel#group-dm-add-recipient) (2 methods) |
| [GroupDmRemoveRecipient](#groupdmremoverecipient-method-1-of-2)(…) | Removes a recipient from a Group DM (2 methods) |
| [LeaveGuild](#leaveguild-method)(…) | Leave the guild that the currently logged in user is in See [Leave Guild](https://discord.com/developers/docs/resources/user#leave-guild) |
| [LogDebug](#logdebug-method)(…) |  |
| [ModifyCurrentUser](#modifycurrentuser-method)(…) | Modify the currently logged in user See [Modify Current User](https://discord.com/developers/docs/resources/user#modify-current-user) |
| [SendDirectMessage](#senddirectmessage-method-1-of-3)(…) | Send a message to a user in a direct message channel (3 methods) |
| [SendGlobalTemplateDirectMessage](#sendglobaltemplatedirectmessage-method)(…) | Reply to a message using a global message template |
| [SendTemplateDirectMessage](#sendtemplatedirectmessage-method)(…) | Send a message in a DM to the user using a localized message template |
| [Update](#update-method)(…) | Updates the user data with the passed in user |
| static [CreateDirectMessageChannel](#createdirectmessagechannel-method)(…) | Create a Direct Message to the current User See [Create DM](https://discord.com/developers/docs/resources/user#create-dm) |
| static [GetCurrentUser](#getcurrentuser-method)(…) | Returns the currently logged in user account See [Get Current User](https://discord.com/developers/docs/resources/user#get-current-user) |
| static [GetUser](#getuser-method)(…) | Returns the user for the given user Id See [Get User](https://discord.com/developers/docs/resources/user#get-user) |

## See Also

* interface [IDebugLoggable](../Interfaces/IDebugLoggable.md)
* interface [IDiscordCacheable&lt;T&gt;](../Interfaces/IDiscordCacheable%7BT%7D.md)
* interface [ISnowflakeEntity](../Interfaces/ISnowflakeEntity.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [DiscordUser.cs](../../../../Oxide.Ext.Discord/Entities/DiscordUser.cs)
   
   
# GetCurrentUser method

Returns the currently logged in user account See [Get Current User](https://discord.com/developers/docs/resources/user#get-current-user)

```csharp
public static IPromise<DiscordUser> GetCurrentUser(DiscordClient client)
```

| parameter | description |
| --- | --- |
| client | Client to use |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetUser method

Returns the user for the given user Id See [Get User](https://discord.com/developers/docs/resources/user#get-user)

```csharp
public static IPromise<DiscordUser> GetUser(DiscordClient client, Snowflake userId)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| userId | User ID to lookup |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* struct [Snowflake](./Snowflake.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# SendDirectMessage method (1 of 3)

Send a message to a user in a direct message channel

```csharp
public IPromise<DiscordMessage> SendDirectMessage(DiscordClient client, List<DiscordEmbed> embeds)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| embeds | Embeds to be send in the message |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [DiscordMessage](./DiscordMessage.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [DiscordEmbed](./DiscordEmbed.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# SendDirectMessage method (2 of 3)

Send a message to a user in a direct message channel

```csharp
public IPromise<DiscordMessage> SendDirectMessage(DiscordClient client, MessageCreate message)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| message | Message to be created |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [DiscordMessage](./DiscordMessage.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [MessageCreate](./MessageCreate.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# SendDirectMessage method (3 of 3)

Send a message to a user in a direct message channel

```csharp
public IPromise<DiscordMessage> SendDirectMessage(DiscordClient client, string message)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| message | Content of the message |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [DiscordMessage](./DiscordMessage.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# SendTemplateDirectMessage method

Send a message in a DM to the user using a localized message template

```csharp
public IPromise<DiscordMessage> SendTemplateDirectMessage(DiscordClient client, 
    string templateName, string language = "en", MessageCreate message = null, 
    PlaceholderData placeholders = null)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| templateName | Template Name |
| language | Oxide language to use |
| message | Message to use (optional) |
| placeholders | Placeholders to apply (optional) |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [DiscordMessage](./DiscordMessage.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [MessageCreate](./MessageCreate.md)
* class [PlaceholderData](../Libraries/PlaceholderData.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# SendGlobalTemplateDirectMessage method

Reply to a message using a global message template

```csharp
public IPromise<DiscordMessage> SendGlobalTemplateDirectMessage(DiscordClient client, 
    string templateName, MessageCreate message = null, PlaceholderData placeholders = null)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| templateName | Template Name |
| message | Message to use (optional) |
| placeholders | Placeholders to apply (optional) |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [DiscordMessage](./DiscordMessage.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [MessageCreate](./MessageCreate.md)
* class [PlaceholderData](../Libraries/PlaceholderData.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ModifyCurrentUser method

Modify the currently logged in user See [Modify Current User](https://discord.com/developers/docs/resources/user#modify-current-user)

```csharp
public IPromise<DiscordUser> ModifyCurrentUser(DiscordClient client, UserModifyCurrent current)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| current | The updated current user information |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [UserModifyCurrent](./UserModifyCurrent.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetCurrentUserGuilds method

Returns the guilds for the currently logged in user See [Get Current User Guilds](https://discord.com/developers/docs/resources/user#get-current-user-guilds)

```csharp
public IPromise<List<DiscordGuild>> GetCurrentUserGuilds(DiscordClient client, 
    UserGuildsRequest request = null)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| request | Request parameters for filtering guilds |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [DiscordGuild](./DiscordGuild.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [UserGuildsRequest](./UserGuildsRequest.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# LeaveGuild method

Leave the guild that the currently logged in user is in See [Leave Guild](https://discord.com/developers/docs/resources/user#leave-guild)

```csharp
public IPromise LeaveGuild(DiscordClient client, Snowflake guildId)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| guildId | Guild ID to leave |

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* struct [Snowflake](./Snowflake.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# CreateDirectMessageChannel method (1 of 2)

Create a Direct Message to the current User See [Create DM](https://discord.com/developers/docs/resources/user#create-dm)

```csharp
public IPromise<DiscordChannel> CreateDirectMessageChannel(DiscordClient client)
```

| parameter | description |
| --- | --- |
| client | Client to use |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [DiscordChannel](./DiscordChannel.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# CreateDirectMessageChannel method (2 of 2)

Create a Direct Message to the current User See [Create DM](https://discord.com/developers/docs/resources/user#create-dm)

```csharp
public static IPromise<DiscordChannel> CreateDirectMessageChannel(DiscordClient client, 
    Snowflake userId)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| userId | User ID to send the message to |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [DiscordChannel](./DiscordChannel.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* struct [Snowflake](./Snowflake.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# CreateGroupDm method

Create a Group Direct Message

```csharp
public IPromise<DiscordChannel> CreateGroupDm(DiscordClient client, string[] accessTokens, 
    Hash<Snowflake, string> nicks)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| accessTokens | access tokens of users that have granted your app the gdm.join scope |
| nicks | a list of user ids to their respective nicknames |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [DiscordChannel](./DiscordChannel.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* struct [Snowflake](./Snowflake.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetUserConnections method

Returns a list of connection objects. Requires the connections OAuth2 scope.

```csharp
public IPromise<List<Connection>> GetUserConnections(DiscordClient client)
```

| parameter | description |
| --- | --- |
| client | Client to use |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Connection](./Connection.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GroupDmAddRecipient method (1 of 2)

Adds a recipient to a Group DM using their access token See [Group DM Add Recipient](https://discord.com/developers/docs/resources/channel#group-dm-add-recipient)

```csharp
public IPromise GroupDmAddRecipient(DiscordClient client, DiscordChannel channel, 
    string accessToken)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| channel | Channel to add recipient to |
| accessToken | Users access token |

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [DiscordChannel](./DiscordChannel.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# GroupDmAddRecipient method (2 of 2)

Adds a recipient to a Group DM using their access token See [Group DM Add Recipient](https://discord.com/developers/docs/resources/channel#group-dm-add-recipient)

```csharp
public IPromise GroupDmAddRecipient(DiscordClient client, Snowflake channelId, string accessToken, 
    string nick)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| channelId | Channel ID to add user to |
| accessToken | Users access token |
| nick | User nickname |

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* struct [Snowflake](./Snowflake.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GroupDmRemoveRecipient method (1 of 2)

Removes a recipient from a Group DM

```csharp
public IPromise GroupDmRemoveRecipient(DiscordClient client, DiscordChannel channel)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| channel | Channel to remove recipient from |

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* class [DiscordChannel](./DiscordChannel.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# GroupDmRemoveRecipient method (2 of 2)

Removes a recipient from a Group DM

```csharp
public IPromise GroupDmRemoveRecipient(DiscordClient client, Snowflake channelId)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| channelId | Channel ID to remove recipient from |

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [DiscordClient](../Clients/DiscordClient.md)
* struct [Snowflake](./Snowflake.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Update method

Updates the user data with the passed in user

```csharp
public void Update(DiscordUser update)
```

| parameter | description |
| --- | --- |
| update |  |

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# LogDebug method

```csharp
public void LogDebug(DebugLogger logger)
```

## See Also

* class [DebugLogger](../Logging/DebugLogger.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DiscordUser constructor

The default constructor.

```csharp
public DiscordUser()
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Id property

The user's id

```csharp
public Snowflake Id { get; set; }
```

## See Also

* struct [Snowflake](./Snowflake.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Username property

The user's username, not unique across the platform

```csharp
public string Username { get; set; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GlobalName property

The user's global name

```csharp
public string GlobalName { get; set; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Discriminator property

The user's 4-digit discord-tag

```csharp
[Obsolete("This field will be removed by discord in a future API version")]
public string Discriminator { get; set; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Avatar property

The user's avatar hash

```csharp
public string Avatar { get; set; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Bot property

Whether the user belongs to an OAuth2 application

```csharp
public bool? Bot { get; set; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# System property

Whether the user is an Official Discord System user (part of the urgent message system)

```csharp
public bool? System { get; set; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# MfaEnabled property

Whether the user has two factor enabled on their account

```csharp
public bool? MfaEnabled { get; set; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Banner property

The user's banner, or null if unset

```csharp
public string Banner { get; set; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AccentColor property

The user's banner color encoded as an integer representation of hexadecimal color code

```csharp
public DiscordColor? AccentColor { get; set; }
```

## See Also

* struct [DiscordColor](./DiscordColor.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Locale property

The user's chosen language option

```csharp
public string Locale { get; set; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Verified property

Whether the email on this account has been verified

```csharp
public bool? Verified { get; set; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Email property

The user's email

```csharp
public string Email { get; set; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Flags property

The flags on a user's account [`UserFlags`](./UserFlags.md)

```csharp
public UserFlags? Flags { get; set; }
```

## See Also

* enum [UserFlags](./UserFlags.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# PremiumType property

The type of Nitro subscription on a user's account [`UserPremiumType`](./UserPremiumType.md)

```csharp
public UserPremiumType? PremiumType { get; set; }
```

## See Also

* enum [UserPremiumType](./UserPremiumType.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# PublicFlags property

The public flags on a user's account [`UserFlags`](./UserFlags.md)

```csharp
public UserFlags? PublicFlags { get; set; }
```

## See Also

* enum [UserFlags](./UserFlags.md)
* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# AvatarDecoration property

The user's avatar decoration hash

```csharp
public string AvatarDecoration { get; set; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Mention property

Returns a string to mention this users nickname in a message

```csharp
public string Mention { get; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetDefaultAvatarUrl property

Default Avatar Url for the User

```csharp
public string GetDefaultAvatarUrl { get; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetAvatarUrl property

Avatar Url for the user

```csharp
public string GetAvatarUrl { get; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetAvatarDecorationUrl property

Avatar Decoration Url for the user

```csharp
public string GetAvatarDecorationUrl { get; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetBannerUrl property

Banner Url for the user

```csharp
public string GetBannerUrl { get; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# FullUserName property

Returns the username#discriminator for the user

```csharp
public string FullUserName { get; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# DisplayName property

The display name for the user

```csharp
public string DisplayName { get; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IsBot property

Returns if the DiscordUser is a bot

```csharp
public bool IsBot { get; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# IsSystem property

Returns if the DiscordUser is a system user

```csharp
public bool IsSystem { get; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# HasUpdatedUsername property

Returns true if the user has upgraded their username to the new username format

```csharp
public bool HasUpdatedUsername { get; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Player property

Returns the IPlayer for the discord user if linked; null otherwise

```csharp
public IPlayer Player { get; }
```

## See Also

* class [DiscordUser](./DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities](./EntitiesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
