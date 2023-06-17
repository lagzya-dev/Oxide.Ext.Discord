# PermissionFlags enumeration

Represents [Permission Flags](https://discord.com/developers/docs/topics/permissions#permissions-bitwise-permission-flags) for user or role

```csharp
[Flags]
public enum PermissionFlags : ulong
```

## Values

| name | value | description |
| --- | --- | --- |
| None | `0x0000000000000000` | Represents No Permissions |
| All | `0xFFFFFFFFFFFFFFFF` | Represents all possible Permissions Flags |
| CreateInstantInvite | `0x0000000000000001` | Allows creation of instant invites Channel Type (Text, Voice, Stage) |
| KickMembers | `0x0000000000000002` | Allows kicking members |
| BanMembers | `0x0000000000000004` | Allows banning members |
| Administrator | `0x0000000000000008` | Allows all permissions and bypasses channel permission overwrites |
| ManageChannels | `0x0000000000000010` | Allows management and editing of channels Channel Type (Text, Voice, Stage) |
| ManageGuild | `0x0000000000000020` | Allows management and editing of the guild |
| AddReactions | `0x0000000000000040` | Allows for the addition of reactions to messages Channel Type (Text) |
| ViewAuditLog | `0x0000000000000080` | Allows for viewing of audit logs |
| PrioritySpeaker | `0x0000000000000100` | Allows for using priority speaker in a voice channel Channel Type (Voice) |
| Stream | `0x0000000000000200` | Allows the user to go live Channel Type (Voice) |
| ViewChannel | `0x0000000000000400` | Allows guild members to view a channel, which includes reading messages in text channels and joining voice channels Channel Type (Text, Voice, Stage) |
| SendMessages | `0x0000000000000800` | Allows for sending messages in a channel Channel Type (Text) |
| SendTtsMessages | `0x0000000000001000` | Allows for sending of /tts messages Channel Type (Text) |
| ManageMessages | `0x0000000000002000` | Allows for deletion of other users messages Channel Type (Text) |
| EmbedLinks | `0x0000000000004000` | Links sent by users with this permission will be auto-embedded Channel Type (Text) |
| AttachFiles | `0x0000000000008000` | Allows for uploading images and files Channel Type (Text) |
| ReadMessageHistory | `0x0000000000010000` | Allows for reading of message history Channel Type (Text) |
| MentionEveryone | `0x0000000000020000` | Allows for using the @everyone tag to notify all users in a channel, and the @here tag to notify all online users in a channel Channel Type (Text, Stage) |
| UseExternalEmojis | `0x0000000000040000` | Allows the usage of custom emojis from other servers Channel Type (Text) |
| ViewGuildInsights | `0x0000000000080000` | Allows for viewing guild insights |
| Connect | `0x0000000000100000` | Allows for joining of a voice channel Channel Type (Voice, Stage) |
| Speak | `0x0000000000200000` | Allows for speaking in a voice channel Channel Type (Voice) |
| MuteMembers | `0x0000000000400000` | Allows for muting members in a voice channel Channel Type (Voice, Stage) |
| DeafanMembers | `0x0000000000800000` | Allows for deafening of members in a voice channel Channel Type (Voice, Stage) |
| MoveMembers | `0x0000000001000000` | Allows for moving of members between voice channels Channel Type (Voice, Stage) |
| UseVad | `0x0000000002000000` | Allows for using voice-activity-detection in a voice channel Channel Type (Voice) |
| ChangeNickname | `0x0000000004000000` | Allows for modification of own nickname |
| ManageNicknames | `0x0000000008000000` | Allows for modification of other users nicknames |
| ManageRoles | `0x0000000010000000` | Allows management and editing of roles Channel Type (Text, Voice, Stage) |
| ManageWebhooks | `0x0000000020000000` | Allows management and editing of webhooks Channel Type (Text) |
| ManageGuildExpressions | `0x0000000040000000` | Allows management and editing of emojis, stickers, and soundboard sounds |
| ManageEmojisAndStickers | `0x0000000040000000` | Allows management and editing of emojis |
| UseSlashCommands | `0x0000000080000000` | Allows members to use application commands, including slash commands and context menu commands. |
| RequestToSpeak | `0x0000000100000000` | Allows for requesting to speak in stage channels. Channel Type (Stage) (This permission is under active development and may be changed or removed.) |
| ManageEvents | `0x0000000200000000` | Allows for creating, editing, and deleting scheduled events Channel Type (Voice, Stage) |
| ManageThreads | `0x0000000400000000` | Allows for deleting and archiving threads, and viewing all private threads Channel Type (Text) |
| UsePublicThreads | `0x0000000800000000` | Allows for creating and participating in threads Channel Type (Text) |
| CreatePublicThreads | `0x0000000800000000` | Allows for creating threads Channel Type (Text) |
| UsePrivateThreads | `0x0000001000000000` | Allows for creating and participating in private threads Channel Type (Text) |
| CreatePrivateThreads | `0x0000001000000000` | Allows for creating private threads Channel Type (Text) |
| UseExternalStickers | `0x0000002000000000` | Allows the usage of custom stickers from other servers Channel Type (Text) |
| SendMessagesInThreads | `0x0000004000000000` | Allows for sending messages in threads Channel Type (Text) |
| StartEmbeddedActivities | `0x0000008000000000` | Allows for launching activities (applications with the `EMBEDDED` flag) in a voice channel Channel Type (Voice) |
| UseEmbeddedActivities | `0x0000008000000000` | Allows for using Activities (applications with the `EMBEDDED` flag) in a voice channel Channel Type (Voice) |
| ModerateMembers | `0x0000010000000000` | Allows for timing out users to prevent them from sending or reacting to messages in chat and threads, and from speaking in voice and stage channels |
| ViewCreatorMonetizationAnalytics | `0x0000020000000000` | Allows for viewing role subscription insights |
| UseSoundboard | `0x0000040000000000` | Allows for using soundboard in a voice channel |
| UseExternalSounds | `0x0000200000000000` | Allows the usage of custom soundboard sounds from other servers |
| SendVoiceMessages | `0x0000400000000000` | Allows sending voice messages |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Permissions](./PermissionsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [PermissionFlags.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Permissions/PermissionFlags.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
