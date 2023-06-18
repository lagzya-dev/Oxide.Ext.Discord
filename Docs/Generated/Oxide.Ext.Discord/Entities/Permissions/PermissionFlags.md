# PermissionFlags enumeration

Represents [Permission Flags](https://discord.com/developers/docs/topics/permissions#permissions-bitwise-permission-flags) for user or role

```csharp
[Flags]
public enum PermissionFlags : ulong
```

## Values

| name | value | description |
| --- | --- | --- |
| None | `0` | Represents No Permissions |
| All | `0xFFFFFFFFFFFFFFFF` | Represents all possible Permissions Flags |
| CreateInstantInvite | `1 << 0` | Allows creation of instant invites Channel Type (Text, Voice, Stage) |
| KickMembers | `1 << 1` | Allows kicking members |
| BanMembers | `1 << 2` | Allows banning members |
| Administrator | `1 << 3` | Allows all permissions and bypasses channel permission overwrites |
| ManageChannels | `1 << 4` | Allows management and editing of channels Channel Type (Text, Voice, Stage) |
| ManageGuild | `1 << 5` | Allows management and editing of the guild |
| AddReactions | `1 << 6` | Allows for the addition of reactions to messages Channel Type (Text) |
| ViewAuditLog | `1 << 7` | Allows for viewing of audit logs |
| PrioritySpeaker | `1 << 8` | Allows for using priority speaker in a voice channel Channel Type (Voice) |
| Stream | `1 << 9` | Allows the user to go live Channel Type (Voice) |
| ViewChannel | `1 << 10` | Allows guild members to view a channel, which includes reading messages in text channels and joining voice channels Channel Type (Text, Voice, Stage) |
| SendMessages | `1 << 11` | Allows for sending messages in a channel Channel Type (Text) |
| SendTtsMessages | `1 << 12` | Allows for sending of /tts messages Channel Type (Text) |
| ManageMessages | `1 << 13` | Allows for deletion of other users messages Channel Type (Text) |
| EmbedLinks | `1 << 14` | Links sent by users with this permission will be auto-embedded Channel Type (Text) |
| AttachFiles | `1 << 15` | Allows for uploading images and files Channel Type (Text) |
| ReadMessageHistory | `1 << 16` | Allows for reading of message history Channel Type (Text) |
| MentionEveryone | `1 << 17` | Allows for using the @everyone tag to notify all users in a channel, and the @here tag to notify all online users in a channel Channel Type (Text, Stage) |
| UseExternalEmojis | `1 << 18` | Allows the usage of custom emojis from other servers Channel Type (Text) |
| ViewGuildInsights | `1 << 19` | Allows for viewing guild insights |
| Connect | `1 << 20` | Allows for joining of a voice channel Channel Type (Voice, Stage) |
| Speak | `1 << 21` | Allows for speaking in a voice channel Channel Type (Voice) |
| MuteMembers | `1 << 22` | Allows for muting members in a voice channel Channel Type (Voice, Stage) |
| DeafanMembers | `1 << 23` | Allows for deafening of members in a voice channel Channel Type (Voice, Stage) |
| MoveMembers | `1 << 24` | Allows for moving of members between voice channels Channel Type (Voice, Stage) |
| UseVad | `1 << 25` | Allows for using voice-activity-detection in a voice channel Channel Type (Voice) |
| ChangeNickname | `1 << 26` | Allows for modification of own nickname |
| ManageNicknames | `1 << 27` | Allows for modification of other users nicknames |
| ManageRoles | `1 << 28` | Allows management and editing of roles Channel Type (Text, Voice, Stage) |
| ManageWebhooks | `1 << 29` | Allows management and editing of webhooks Channel Type (Text) |
| ManageGuildExpressions | `1 << 30` | Allows management and editing of emojis, stickers, and soundboard sounds |
| ManageEmojisAndStickers | `1 << 30` | Allows management and editing of emojis |
| UseSlashCommands | `1 << 31` | Allows members to use application commands, including slash commands and context menu commands. |
| RequestToSpeak | `1 << 32` | Allows for requesting to speak in stage channels. Channel Type (Stage) (This permission is under active development and may be changed or removed.) |
| ManageEvents | `1 << 33` | Allows for creating, editing, and deleting scheduled events Channel Type (Voice, Stage) |
| ManageThreads | `1 << 34` | Allows for deleting and archiving threads, and viewing all private threads Channel Type (Text) |
| UsePublicThreads | `1 << 35` | Allows for creating and participating in threads Channel Type (Text) |
| CreatePublicThreads | `1 << 35` | Allows for creating threads Channel Type (Text) |
| UsePrivateThreads | `1 << 36` | Allows for creating and participating in private threads Channel Type (Text) |
| CreatePrivateThreads | `1 << 36` | Allows for creating private threads Channel Type (Text) |
| UseExternalStickers | `1 << 37` | Allows the usage of custom stickers from other servers Channel Type (Text) |
| SendMessagesInThreads | `1 << 38` | Allows for sending messages in threads Channel Type (Text) |
| StartEmbeddedActivities | `1 << 39` | Allows for launching activities (applications with the `EMBEDDED` flag) in a voice channel Channel Type (Voice) |
| UseEmbeddedActivities | `1 << 39` | Allows for using Activities (applications with the `EMBEDDED` flag) in a voice channel Channel Type (Voice) |
| ModerateMembers | `1 << 40` | Allows for timing out users to prevent them from sending or reacting to messages in chat and threads, and from speaking in voice and stage channels |
| ViewCreatorMonetizationAnalytics | `1 << 41` | Allows for viewing role subscription insights |
| UseSoundboard | `1 << 42` | Allows for using soundboard in a voice channel |
| UseExternalSounds | `1 << 45` | Allows the usage of custom soundboard sounds from other servers |
| SendVoiceMessages | `1 << 46` | Allows sending voice messages |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [PermissionFlags.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Permissions/PermissionFlags.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
