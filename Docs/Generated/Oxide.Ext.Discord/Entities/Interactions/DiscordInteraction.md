# DiscordInteraction class

Represents [Interaction Structure](https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-structure)

```csharp
public class DiscordInteraction
```

## Public Members

| name | description |
| --- | --- |
| [DiscordInteraction](DiscordInteraction/DiscordInteraction.md)() | The default constructor. |
| [ApplicationId](DiscordInteraction/ApplicationId.md) { get; set; } | ID of the application this interaction is for |
| [AppPermissions](DiscordInteraction/AppPermissions.md) { get; set; } | Bitwise set of permissions the app or bot has within the channel the interaction was sent from |
| [Channel](DiscordInteraction/Channel.md) { get; set; } | Channel that the interaction was sent from |
| [ChannelId](DiscordInteraction/ChannelId.md) { get; set; } | Channel that the interaction was sent from |
| [Data](DiscordInteraction/Data.md) { get; set; } | Interaction data payload See [`InteractionData`](./InteractionData.md) |
| [Focused](DiscordInteraction/Focused.md) { get; } | Returns the Focused option for Auto Complete |
| [GuildId](DiscordInteraction/GuildId.md) { get; set; } | Guild that the interaction was sent from |
| [GuildLocale](DiscordInteraction/GuildLocale.md) { get; set; } | The guild's preferred locale, if invoked in a guild [Discord Locale Values](https://discord.com/developers/docs/dispatch/field-values#predefined-field-values-accepted-locales) |
| [Id](DiscordInteraction/Id.md) { get; set; } | Id of the interaction |
| [Locale](DiscordInteraction/Locale.md) { get; set; } | The selected language of the invoking user [Discord Locale Values](https://discord.com/developers/docs/dispatch/field-values#predefined-field-values-accepted-locales) |
| [Member](DiscordInteraction/Member.md) { get; set; } | Guild member data for the invoking user, including permissions |
| [Message](DiscordInteraction/Message.md) { get; set; } | For components, the message they were attached to |
| [Parsed](DiscordInteraction/Parsed.md) { get; } | Returns the interaction parsed args to make it easier to process that interaction. |
| [Token](DiscordInteraction/Token.md) { get; set; } | Continuation token for responding to the interaction Interaction tokens are valid for 15 minutes and can be used to send followup messages but you must send an initial response within 3 seconds of receiving the event. If the 3 second deadline is exceeded, the token will be invalidated. |
| [Type](DiscordInteraction/Type.md) { get; set; } | The type of interaction See [`InteractionType`](./InteractionType.md) |
| [User](DiscordInteraction/User.md) { get; } | User object. If in DM then DM user else GuildMember.User |
| [Version](DiscordInteraction/Version.md) { get; set; } | Read-only property, always 1 |
| readonly [CreatedDate](DiscordInteraction/CreatedDate.md) | The UTC DateTime this interaction was created |
| [CreateFollowUpMessage](DiscordInteraction/CreateFollowUpMessage.md)(…) | Create a followup message for an Interaction See [Create Followup Message](https://discord.com/developers/docs/interactions/receiving-and-responding#create-followup-message) (2 methods) |
| [CreateModalResponse](DiscordInteraction/CreateModalResponse.md)(…) | Creates a interaction modal response from a modal template |
| [CreateResponse](DiscordInteraction/CreateResponse.md)(…) | Create a response to an Interaction from the gateway. See [Create Interaction Response](https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response) (7 methods) |
| [CreateTemplateResponse](DiscordInteraction/CreateTemplateResponse.md)(…) | Creates a interaction message response from a message template |
| [DefferResponse](DiscordInteraction/DefferResponse.md)(…) | Creates a response indicating that: for application commands there will be an update in the future for message component commands that you have acknowledged the command and there may be an update in the future See [Create Interaction Response](https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response) |
| [DeleteFollowUpMessage](DiscordInteraction/DeleteFollowUpMessage.md)(…) | Deletes a followup message for an Interaction See [Delete Followup Message](https://discord.com/developers/docs/interactions/receiving-and-responding#delete-followup-message) |
| [DeleteOriginalResponse](DiscordInteraction/DeleteOriginalResponse.md)(…) | Deletes the initial Interaction response See [Delete Original Interaction Response](https://discord.com/developers/docs/interactions/receiving-and-responding#delete-original-interaction-response) |
| [EditFollowUpMessage](DiscordInteraction/EditFollowUpMessage.md)(…) | Edits a followup message for an Interaction See [Edit Followup Message](https://discord.com/developers/docs/interactions/receiving-and-responding#edit-followup-message) |
| [EditOriginalResponse](DiscordInteraction/EditOriginalResponse.md)(…) | Edits the initial Interaction response See [Edit Original Interaction Response](https://discord.com/developers/docs/interactions/receiving-and-responding#edit-original-interaction-response) |
| [EditTemplateOriginalResponse](DiscordInteraction/EditTemplateOriginalResponse.md)(…) | Edit a interaction response with a message template |
| [GetAutoCompleteBuilder](DiscordInteraction/GetAutoCompleteBuilder.md)() | Returns a [`InteractionAutoCompleteBuilder`](../../Builders/Interactions/InteractionAutoCompleteBuilder.md) for this interaction |
| [GetFollowupBuilder](DiscordInteraction/GetFollowupBuilder.md)() | Returns a [`InteractionFollowupBuilder`](../../Builders/Interactions/InteractionFollowupBuilder.md) for this interaction |
| [GetLangMessage](DiscordInteraction/GetLangMessage.md)(…) | Returns a localized string for this interaction (2 methods) |
| [GetModalBuilder](DiscordInteraction/GetModalBuilder.md)() | Returns a [`InteractionAutoCompleteBuilder`](../../Builders/Interactions/InteractionAutoCompleteBuilder.md) for this interaction |
| [GetResponseBuilder](DiscordInteraction/GetResponseBuilder.md)() | Returns a [`InteractionResponseBuilder`](../../Builders/Interactions/InteractionResponseBuilder.md) for this interaction |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Interactions](./InteractionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordInteraction.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Interactions/DiscordInteraction.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
