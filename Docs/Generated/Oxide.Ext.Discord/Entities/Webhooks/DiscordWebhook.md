# DiscordWebhook class

Represents [Webhook Structure](https://discord.com/developers/docs/resources/webhook#webhook-object)

```csharp
public class DiscordWebhook
```

## Public Members

| name | description |
| --- | --- |
| [DiscordWebhook](DiscordWebhook/DiscordWebhook.md)() | The default constructor. |
| [ApplicationId](DiscordWebhook/ApplicationId.md) { get; set; } | The bot/OAuth2 application that created this webhook |
| [Avatar](DiscordWebhook/Avatar.md) { get; set; } | the default user avatar hash of the webhook |
| [ChannelId](DiscordWebhook/ChannelId.md) { get; set; } | The channel id this webhook is for, if any |
| [GuildId](DiscordWebhook/GuildId.md) { get; set; } | The guild id this webhook is for, if any |
| [Id](DiscordWebhook/Id.md) { get; set; } | The id of the webhook |
| [Name](DiscordWebhook/Name.md) { get; set; } | The default name of the webhook |
| [SourceChannel](DiscordWebhook/SourceChannel.md) { get; set; } | The channel that this webhook is following (returned for Channel Follower Webhooks) |
| [SourceGuild](DiscordWebhook/SourceGuild.md) { get; set; } | The guild of the channel that this webhook is following (returned for Channel Follower Webhooks) |
| [Token](DiscordWebhook/Token.md) { get; set; } | The secure token of the webhook returned for Incoming Webhooks |
| [Type](DiscordWebhook/Type.md) { get; set; } | The type of the webhook See [`WebhookType`](./WebhookType.md) |
| [User](DiscordWebhook/User.md) { get; set; } | The user this webhook was created by not returned when getting a webhook with its token |
| [DeleteWebhook](DiscordWebhook/DeleteWebhook.md)(…) | Delete a webhook permanently. Requires the MANAGE_WEBHOOKS permission. See [Delete Webhook](https://discord.com/developers/docs/resources/webhook#delete-webhook) |
| [DeleteWebhookMessage](DiscordWebhook/DeleteWebhookMessage.md)(…) | Deletes a message that was created by the webhook. |
| [DeleteWebhookWithToken](DiscordWebhook/DeleteWebhookWithToken.md)(…) | Delete a webhook permanently. Does not require authentication. See [Delete Webhook with Token](https://discord.com/developers/docs/resources/webhook#delete-webhook-with-token) |
| [EditWebhook](DiscordWebhook/EditWebhook.md)(…) | Modify a webhook. Requires the MANAGE_WEBHOOKS permission. See [Modify Webhook](https://discord.com/developers/docs/resources/webhook#modify-webhook) |
| [EditWebhookMessage](DiscordWebhook/EditWebhookMessage.md)(…) | Edits a previously-sent webhook message from the same token. See [Edit Webhook Message](https://discord.com/developers/docs/resources/webhook#edit-webhook-message) |
| [EditWebhookMessageGlobalTemplate](DiscordWebhook/EditWebhookMessageGlobalTemplate.md)(…) | Edit a message from a webhook using a global message template |
| [EditWebhookMessageTemplate](DiscordWebhook/EditWebhookMessageTemplate.md)(…) | Edit a message from a webhook using a localized message template |
| [ExecuteWebhook](DiscordWebhook/ExecuteWebhook.md)(…) | Executes a webhook See [Execute Webhook](https://discord.com/developers/docs/resources/webhook#execute-webhook) (2 methods) |
| [ExecuteWebhookGlobalTemplate](DiscordWebhook/ExecuteWebhookGlobalTemplate.md)(…) | Send a message to a webhook using a global message template |
| [ExecuteWebhookTemplate](DiscordWebhook/ExecuteWebhookTemplate.md)(…) | Send a message to a webhook using a localized message template |
| [ExecuteWebhookWithMessage](DiscordWebhook/ExecuteWebhookWithMessage.md)(…) | Executes a webhook See [Execute Webhook](https://discord.com/developers/docs/resources/webhook#execute-webhook) (2 methods) |
| [GetWebhookMessage](DiscordWebhook/GetWebhookMessage.md)(…) | Gets a previously-sent webhook message from the same token. See [Edit Webhook Message](https://discord.com/developers/docs/resources/webhook#get-webhook-message) |
| [ModifyWebhookWithToken](DiscordWebhook/ModifyWebhookWithToken.md)(…) | Modify a webhook. Requires the MANAGE_WEBHOOKS permission. See [Modify Webhook with Token](https://discord.com/developers/docs/resources/webhook#modify-webhook-with-token) |
| static [CreateWebhook](DiscordWebhook/CreateWebhook.md)(…) | Create a new webhook. Requires the MANAGE_WEBHOOKS permission. See [Create Webhook](https://discord.com/developers/docs/resources/webhook#create-webhook) |
| static [GetChannelWebhooks](DiscordWebhook/GetChannelWebhooks.md)(…) | Returns a list of channel webhook. See [Get Channel Webhooks](https://discord.com/developers/docs/resources/webhook#get-channel-webhooks) |
| static [GetGuildWebhooks](DiscordWebhook/GetGuildWebhooks.md)(…) | Returns a list of guild webhooks See [Get Guild Webhooks](https://discord.com/developers/docs/resources/webhook#get-guild-webhooks) |
| static [GetWebhook](DiscordWebhook/GetWebhook.md)(…) | Returns the webhook with the given webhook ID See [Get Webhook](https://discord.com/developers/docs/resources/webhook#get-webhook) |
| static [GetWebhookWithToken](DiscordWebhook/GetWebhookWithToken.md)(…) | Returns the webhook with the given ID &amp; Token This call does not required authentication No user is returned in webhook object See [Get Webhook with Token](https://discord.com/developers/docs/resources/webhook#get-webhook-with-token) |
| static [GetWebhookWithUrl](DiscordWebhook/GetWebhookWithUrl.md)(…) | Returns the webhook with the given ID &amp; Token This call does not required authentication No user is returned in webhook object See [Get Webhook with Token](https://discord.com/developers/docs/resources/webhook#get-webhook-with-token) |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Webhooks](./WebhooksNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordWebhook.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Webhooks/DiscordWebhook.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
