# DiscordInvite class

Represents an [Invite Structure](https://discord.com/developers/docs/resources/invite#invite-object) that when used, adds a user to a guild or group DM channel.

```csharp
public class DiscordInvite
```

## Public Members

| name | description |
| --- | --- |
| [DiscordInvite](#discordinvite-constructor)() | The default constructor. |
| [ApproximateMemberCount](#approximatemembercount-property) { get; set; } | Approximate count of total members |
| [ApproximatePresenceCount](#approximatepresencecount-property) { get; set; } | Approximate count of online members (only present when target_user is set) |
| [Channel](#channel-property) { get; set; } | The channel this invite is for See [`Channel`](./#channel-property) |
| [Code](#code-property) { get; set; } | The invite code (unique ID) |
| [ExpiresAt](#expiresat-property) { get; set; } | When the invite code expires |
| [Guild](#guild-property) { get; set; } | The guild this invite is for See [`Guild`](./#guild-property) |
| [GuildScheduledEvent](#guildscheduledevent-property) { get; set; } | Guild scheduled event data, only included if guild_scheduled_event_id contains a valid guild scheduled event id |
| [Inviter](#inviter-property) { get; set; } | The user who created the invite See [`DiscordUser`](../Users/DiscordUser.md) |
| [StageInstance](#stageinstance-property) { get; set; } | Stage instance data if there is a public Stage instance in the Stage channel this invite is for |
| [TargetUser](#targetuser-property) { get; set; } | The target user for this invite See [`DiscordUser`](../Users/DiscordUser.md) |
| [UserTargetType](#usertargettype-property) { get; set; } | The type of user target for this invite See [`TargetUserType`](./TargetUserType.md) |
| [Delete](#delete-method)(…) | Delete an invite. Requires the MANAGE_CHANNELS permission on the channel this invite belongs to, or MANAGE_GUILD to remove any invite across the guild. Returns an invite object on success. See [Delete Invite](https://discord.com/developers/docs/resources/invite#delete-invite) |
| static [Get](#get-method)(…) | Returns an invite object for the given code. See [Get Invite](https://discord.com/developers/docs/resources/invite#get-invite) |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Invites](./InvitesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordInvite.cs](../../../../Oxide.Ext.Discord/Entities/Invites/DiscordInvite.cs)
   
   
# Get method

Returns an invite object for the given code. See [Get Invite](https://discord.com/developers/docs/resources/invite#get-invite)

```csharp
public static IPromise<DiscordInvite> Get(DiscordClient client, string inviteCode, 
    InviteLookup lookup = null)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| inviteCode | Invite code |
| lookup | Lookup query string parameters for the request |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* class [InviteLookup](./InviteLookup.md)
* class [DiscordInvite](./DiscordInvite.md)
* namespace [Oxide.Ext.Discord.Entities.Invites](./InvitesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Delete method

Delete an invite. Requires the MANAGE_CHANNELS permission on the channel this invite belongs to, or MANAGE_GUILD to remove any invite across the guild. Returns an invite object on success. See [Delete Invite](https://discord.com/developers/docs/resources/invite#delete-invite)

```csharp
public IPromise<DiscordInvite> Delete(DiscordClient client)
```

| parameter | description |
| --- | --- |
| client | Client to use |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [DiscordClient](../../Clients/DiscordClient.md)
* class [DiscordInvite](./DiscordInvite.md)
* namespace [Oxide.Ext.Discord.Entities.Invites](./InvitesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# DiscordInvite constructor

The default constructor.

```csharp
public DiscordInvite()
```

## See Also

* class [DiscordInvite](./DiscordInvite.md)
* namespace [Oxide.Ext.Discord.Entities.Invites](./InvitesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Code property

The invite code (unique ID)

```csharp
public string Code { get; set; }
```

## See Also

* class [DiscordInvite](./DiscordInvite.md)
* namespace [Oxide.Ext.Discord.Entities.Invites](./InvitesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Guild property

The guild this invite is for See `Guild`

```csharp
public DiscordGuild Guild { get; set; }
```

## See Also

* class [DiscordGuild](../Guilds/DiscordGuild.md)
* class [DiscordInvite](./DiscordInvite.md)
* namespace [Oxide.Ext.Discord.Entities.Invites](./InvitesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Channel property

The channel this invite is for See `Channel`

```csharp
public DiscordChannel Channel { get; set; }
```

## See Also

* class [DiscordChannel](../Channels/DiscordChannel.md)
* class [DiscordInvite](./DiscordInvite.md)
* namespace [Oxide.Ext.Discord.Entities.Invites](./InvitesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Inviter property

The user who created the invite See [`DiscordUser`](../Users/DiscordUser.md)

```csharp
public DiscordUser Inviter { get; set; }
```

## See Also

* class [DiscordUser](../Users/DiscordUser.md)
* class [DiscordInvite](./DiscordInvite.md)
* namespace [Oxide.Ext.Discord.Entities.Invites](./InvitesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# TargetUser property

The target user for this invite See [`DiscordUser`](../Users/DiscordUser.md)

```csharp
public DiscordUser TargetUser { get; set; }
```

## See Also

* class [DiscordUser](../Users/DiscordUser.md)
* class [DiscordInvite](./DiscordInvite.md)
* namespace [Oxide.Ext.Discord.Entities.Invites](./InvitesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# UserTargetType property

The type of user target for this invite See [`TargetUserType`](./TargetUserType.md)

```csharp
public TargetUserType? UserTargetType { get; set; }
```

## See Also

* enum [TargetUserType](./TargetUserType.md)
* class [DiscordInvite](./DiscordInvite.md)
* namespace [Oxide.Ext.Discord.Entities.Invites](./InvitesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ApproximatePresenceCount property

Approximate count of online members (only present when target_user is set)

```csharp
public int? ApproximatePresenceCount { get; set; }
```

## See Also

* class [DiscordInvite](./DiscordInvite.md)
* namespace [Oxide.Ext.Discord.Entities.Invites](./InvitesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ApproximateMemberCount property

Approximate count of total members

```csharp
public int? ApproximateMemberCount { get; set; }
```

## See Also

* class [DiscordInvite](./DiscordInvite.md)
* namespace [Oxide.Ext.Discord.Entities.Invites](./InvitesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ExpiresAt property

When the invite code expires

```csharp
public DateTime? ExpiresAt { get; set; }
```

## See Also

* class [DiscordInvite](./DiscordInvite.md)
* namespace [Oxide.Ext.Discord.Entities.Invites](./InvitesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# StageInstance property

Stage instance data if there is a public Stage instance in the Stage channel this invite is for

```csharp
[Obsolete("This field is considered deprecated by discord and may be removed in a future update.")]
public InviteStageInstance StageInstance { get; set; }
```

## See Also

* class [InviteStageInstance](./InviteStageInstance.md)
* class [DiscordInvite](./DiscordInvite.md)
* namespace [Oxide.Ext.Discord.Entities.Invites](./InvitesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# GuildScheduledEvent property

Guild scheduled event data, only included if guild_scheduled_event_id contains a valid guild scheduled event id

```csharp
public InviteStageInstance GuildScheduledEvent { get; set; }
```

## See Also

* class [InviteStageInstance](./InviteStageInstance.md)
* class [DiscordInvite](./DiscordInvite.md)
* namespace [Oxide.Ext.Discord.Entities.Invites](./InvitesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
