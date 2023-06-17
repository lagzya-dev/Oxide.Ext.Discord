# DiscordInteraction.CreateResponse method (1 of 7)

Create a response to an Interaction from the gateway. See [Create Interaction Response](https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response)

```csharp
public IPromise CreateResponse(DiscordClient client, BaseInteractionResponse response)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| response | Response to respond with |

## See Also

* interface [IPromise](../../../Interfaces/Promises/IPromise.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* class [BaseInteractionResponse](../Response/BaseInteractionResponse.md)
* class [DiscordInteraction](../DiscordInteraction.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](../DiscordInteraction.md.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordInteraction.CreateResponse method (2 of 7)

Create a Auto Complete response to an Interaction See [Create Interaction Response](https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response)

```csharp
public IPromise CreateResponse(DiscordClient client, InteractionAutoCompleteBuilder builder)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| builder | Auto Complete Builder for this response |

## See Also

* interface [IPromise](../../../Interfaces/Promises/IPromise.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* class [InteractionAutoCompleteBuilder](../../../Builders/Interactions/InteractionAutoCompleteBuilder.md)
* class [DiscordInteraction](../DiscordInteraction.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](../DiscordInteraction.md.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordInteraction.CreateResponse method (3 of 7)

Create a Auto Complete response to an Interaction See [Create Interaction Response](https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response)

```csharp
public IPromise CreateResponse(DiscordClient client, InteractionAutoCompleteMessage message)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| message | Message for this response |

## See Also

* interface [IPromise](../../../Interfaces/Promises/IPromise.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* class [InteractionAutoCompleteMessage](../Response/InteractionAutoCompleteMessage.md)
* class [DiscordInteraction](../DiscordInteraction.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](../DiscordInteraction.md.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordInteraction.CreateResponse method (4 of 7)

Create a Modal response to an Interaction See [Create Interaction Response](https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response)

```csharp
public IPromise CreateResponse(DiscordClient client, InteractionModalBuilder builder)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| builder | Modal Builder for this response |

## See Also

* interface [IPromise](../../../Interfaces/Promises/IPromise.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* class [InteractionModalBuilder](../../../Builders/Interactions/InteractionModalBuilder.md)
* class [DiscordInteraction](../DiscordInteraction.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](../DiscordInteraction.md.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordInteraction.CreateResponse method (5 of 7)

Create a Modal response to an Interaction See [Create Interaction Response](https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response)

```csharp
public IPromise CreateResponse(DiscordClient client, InteractionModalMessage message)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| message | Message for this response |

## See Also

* interface [IPromise](../../../Interfaces/Promises/IPromise.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* class [InteractionModalMessage](../Response/InteractionModalMessage.md)
* class [DiscordInteraction](../DiscordInteraction.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](../DiscordInteraction.md.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordInteraction.CreateResponse method (6 of 7)

Create a response to an Interaction from the gateway. See [Create Interaction Response](https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response)

```csharp
public IPromise CreateResponse(DiscordClient client, InteractionResponseType type, 
    InteractionCallbackData response = null)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| type | Type of the interaction response |
| response | Interaction Callback Message Data |

## See Also

* interface [IPromise](../../../Interfaces/Promises/IPromise.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* enum [InteractionResponseType](../InteractionResponseType.md)
* class [InteractionCallbackData](../Response/InteractionCallbackData.md)
* class [DiscordInteraction](../DiscordInteraction.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](../DiscordInteraction.md.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordInteraction.CreateResponse method (7 of 7)

Create a response to an Interaction from the gateway. See [Create Interaction Response](https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response)

```csharp
public IPromise CreateResponse(DiscordClient client, InteractionResponseType type, 
    InteractionResponseBuilder builder)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| type | Type of the interaction response |
| builder | Builder for this response |

## See Also

* interface [IPromise](../../../Interfaces/Promises/IPromise.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* enum [InteractionResponseType](../InteractionResponseType.md)
* class [InteractionResponseBuilder](../../../Builders/Interactions/InteractionResponseBuilder.md)
* class [DiscordInteraction](../DiscordInteraction.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](../DiscordInteraction.md.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
