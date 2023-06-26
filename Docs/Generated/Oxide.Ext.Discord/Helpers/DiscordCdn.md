# DiscordCdn class

Represents Discord [CDN Endpoints](https://discord.com/developers/docs/reference#image-formatting-cdn-endpoints)

```csharp
public static class DiscordCdn
```

## Public Members

| name | description |
| --- | --- |
| const [CdnUrl](#CdnUrl-field) | Base CDN Url |
| static [GetAchievementIconUrl](#GetAchievementIconUrl-method)(…) | Returns the Url of the Achievement Icon |
| static [GetApplicationAssetUrl](#GetApplicationAssetUrl-method)(…) | Returns the applications asset icon url |
| static [GetApplicationCoverUrl](#GetApplicationCoverUrl-method)(…) | Returns the url to the application cover |
| static [GetApplicationIconUrl](#GetApplicationIconUrl-method)(…) | Returns the url to the application icon |
| static [GetChannelIcon](#GetChannelIcon-method)(…) | Returns the icon for a given channel |
| static [GetCustomEmojiUrl](#GetCustomEmojiUrl-method)(…) | Returns the Url to the custom emoji |
| static [GetExtension](#GetExtension-method)(…) | Returns the extension to use for the image |
| static [GetGuildBannerUrl](#GetGuildBannerUrl-method)(…) | Returns the Url of the Guild Banner |
| static [GetGuildDiscoverySplashUrl](#GetGuildDiscoverySplashUrl-method)(…) | Return the Url of the Guild Discovery Splash |
| static [GetGuildIconUrl](#GetGuildIconUrl-method)(…) | Returns the Url to the Guild Icon |
| static [GetGuildMemberAvatar](#GetGuildMemberAvatar-method)(…) | Returns the Url of the Guild Member avatar |
| static [GetGuildMemberBanner](#GetGuildMemberBanner-method)(…) | Returns the guild member banner for the given guild / user ID |
| static [GetGuildScheduledEventCover](#GetGuildScheduledEventCover-method)(…) | Returns the guild schedule event cover icon with the given ID |
| static [GetGuildSplashUrl](#GetGuildSplashUrl-method)(…) | Returns the Url of the Guild Splash |
| static [GetRoleIcon](#GetRoleIcon-method)(…) | Returns the sticker url with the given ID |
| static [GetSticker](#GetSticker-method)(…) | Returns the sticker url with the given ID |
| static [GetStickerPackBanner](#GetStickerPackBanner-method)(…) | Returns the banner for a given sticker pack |
| static [GetStorePageAssetUrl](#GetStorePageAssetUrl-method)(…) | Returns the Store Page Asset Url |
| static [GetTeamIconUrl](#GetTeamIconUrl-method)(…) | Returns the Url of the Team Icon |
| static [GetUserAvatarUrl](#GetUserAvatarUrl-method)(…) | Returns the Url of the users avatar |
| static [GetUserBanner](#GetUserBanner-method)(…) | Returns the Url of the User Banner |
| static [GetUserDefaultAvatarUrl](#GetUserDefaultAvatarUrl-method)(…) | Returns the Url of the users default avatar |

## See Also

* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [DiscordCdn.cs](../../../../Oxide.Ext.Discord/Helpers/DiscordCdn.cs)
   
   
# GetCustomEmojiUrl method

Returns the Url to the custom emoji

```csharp
public static string GetCustomEmojiUrl(Snowflake emojiId, DiscordImageFormat format)
```

| parameter | description |
| --- | --- |
| emojiId | ID of the emoji |
| format | The format the emoji is in |

## Return Value

Url of the emoji

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Thrown if format is Jpg or WebP |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetGuildIconUrl method

Returns the Url to the Guild Icon

```csharp
public static string GetGuildIconUrl(Snowflake guildId, string guildIcon, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| guildId | Guild ID for the icon |
| guildIcon | Guild Icon from guild |
| format | Format the icon is in |

## Return Value

Url of the guild icon

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetGuildSplashUrl method

Returns the Url of the Guild Splash

```csharp
public static string GetGuildSplashUrl(Snowflake guildId, string guildSplash, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| guildId | Guild ID for the icon |
| guildSplash | Guild Splash from guild |
| format | Format the icon is in |

## Return Value

Url of the guild splash

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Thrown if format is Gif |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetGuildDiscoverySplashUrl method

Return the Url of the Guild Discovery Splash

```csharp
public static string GetGuildDiscoverySplashUrl(Snowflake guildId, string guildDiscoverySplash, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| guildId | Guild ID for the icon |
| guildDiscoverySplash | Guild Discovery Splash from guild |
| format | Format the icon is in |

## Return Value

Url of the guild discovery splash

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Thrown if format is Gif |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetGuildBannerUrl method

Returns the Url of the Guild Banner

```csharp
public static string GetGuildBannerUrl(Snowflake guildId, string guildBanner, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| guildId | Guild ID for the icon |
| guildBanner | Guild Banner from guild |
| format | Format the icon is in |

## Return Value

Url of the guild banner

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Thrown if format is Gif |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetUserBanner method

Returns the Url of the User Banner

```csharp
public static string GetUserBanner(Snowflake userId, string userBanner, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| userId | User ID for the Banner |
| userBanner | User Banner from user |
| format | Format the icon is in |

## Return Value

Url of the User banner

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Thrown if format is Gif |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetUserDefaultAvatarUrl method

Returns the Url of the users default avatar

```csharp
public static string GetUserDefaultAvatarUrl(string userDiscriminator)
```

| parameter | description |
| --- | --- |
| userDiscriminator | Discord User Discriminator |

## Return Value

Url of the default avatar url

## See Also

* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetUserAvatarUrl method

Returns the Url of the users avatar

```csharp
public static string GetUserAvatarUrl(Snowflake userId, string userAvatar, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| userId | Discord User ID |
| userAvatar | User avatar from user |
| format | Format the avatar is in |

## Return Value

Url of the users avatar

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetGuildMemberAvatar method

Returns the Url of the Guild Member avatar

```csharp
public static string GetGuildMemberAvatar(Snowflake guildId, Snowflake userId, string memberAvatar, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| guildId | Guild ID of the Guild Member |
| userId | Discord User ID |
| memberAvatar | Guild Member avatar |
| format | Format the avatar is in |

## Return Value

Url of the Guild Member avatar

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetApplicationIconUrl method

Returns the url to the application icon

```csharp
public static string GetApplicationIconUrl(Snowflake applicationId, string icon, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| applicationId | Application ID |
| icon | Icon field from application |
| format | Format the icon is in |

## Return Value

Url of the application icon

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Throw if format is Gif |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetApplicationCoverUrl method

Returns the url to the application cover

```csharp
public static string GetApplicationCoverUrl(Snowflake applicationId, string coverImage, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| applicationId | Application ID |
| coverImage | Icon field from application |
| format | Format the icon is in |

## Return Value

Url of the application icon

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Throw if format is Gif |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetApplicationAssetUrl method

Returns the applications asset icon url

```csharp
public static string GetApplicationAssetUrl(Snowflake applicationId, string assetId, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| applicationId | Application ID of the icon |
| assetId | Asset ID for the application |
| format | Format the icon is in |

## Return Value

Url of the application asset icon

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Throw if format is Gif |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetAchievementIconUrl method

Returns the Url of the Achievement Icon

```csharp
public static string GetAchievementIconUrl(Snowflake applicationId, Snowflake achievementId, 
    string iconHash)
```

| parameter | description |
| --- | --- |
| applicationId | Application ID of the icon |
| achievementId | Achievement ID |
| iconHash | Achievement Icon Hash |

## Return Value

Url of the achievement icon

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Throw if format is Gif |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetStorePageAssetUrl method

Returns the Store Page Asset Url

```csharp
public static string GetStorePageAssetUrl(Snowflake applicationId, ulong assetId, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| applicationId | Application ID of the icon |
| assetId | Asset ID |
| format | Format the icon is in |

## Return Value

Url of the achievement icon

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Throw if format is Gif |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetTeamIconUrl method

Returns the Url of the Team Icon

```csharp
public static string GetTeamIconUrl(Snowflake teamId, string teamIcon, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| teamId | Team ID of the Icon |
| teamIcon | Icon field from Team |
| format | Format the icon is in |

## Return Value

Url of the achievement icon

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Throw if format is Gif |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetStickerPackBanner method

Returns the banner for a given sticker pack

```csharp
public static string GetStickerPackBanner(Snowflake applicationId, Snowflake bannerAssetId, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| applicationId | Application ID for the stickers |
| bannerAssetId | Banner Asset ID for the stickers |
| format | Image Formatting for the banner |

## Return Value

Url to the sticker pack banner

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Thrown if image type is not PNG,JPEG, or WebP |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetSticker method

Returns the sticker url with the given ID

```csharp
public static string GetSticker(DiscordSticker sticker)
```

| parameter | description |
| --- | --- |
| sticker | Sticker to get the url for |

## Return Value

Return url for the sticker

## See Also

* class [DiscordSticker](../Entities/Stickers/DiscordSticker.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetRoleIcon method

Returns the sticker url with the given ID

```csharp
public static string GetRoleIcon(Snowflake roleId, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| roleId | ID of the role |
| format | Format for the icon to be returned in |

## Return Value

Return url for the role icon

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Thrown if image type is not PNG or Lottie |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetGuildScheduledEventCover method

Returns the guild schedule event cover icon with the given ID

```csharp
public static string GetGuildScheduledEventCover(Snowflake scheduledEventId, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| scheduledEventId | Scheduled Event ID |
| format | Format for the icon to be returned in |

## Return Value

Return url for the guild schedule event cover icon

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Thrown if image type is not PNG or Lottie |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetGuildMemberBanner method

Returns the guild member banner for the given guild / user ID

```csharp
public static string GetGuildMemberBanner(Snowflake guildId, Snowflake userId, 
    DiscordImageFormat format = DiscordImageFormat.Auto)
```

| parameter | description |
| --- | --- |
| guildId | Guild ID of the user |
| userId | User ID of the user |
| format | Format for the icon to be returned in |

## Return Value

Return url for the guild member banner

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentException | Thrown if image type is not PNG or Lottie |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetChannelIcon method

Returns the icon for a given channel

```csharp
public static string GetChannelIcon(Snowflake channelId, string icon)
```

| parameter | description |
| --- | --- |
| channelId | Channel ID for the Icon |
| icon | Icon hash for the channel |

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetExtension method

Returns the extension to use for the image

```csharp
public static string GetExtension(DiscordImageFormat format, string image)
```

| parameter | description |
| --- | --- |
| format | Image format that is requested |
| image | Image data from the field |

## Return Value

Image extension for the image format and image data

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentOutOfRangeException | Thrown if Image Format is out of range |

## See Also

* enum [DiscordImageFormat](../Entities/Images/DiscordImageFormat.md)
* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# CdnUrl field

Base CDN Url

```csharp
public const string CdnUrl;
```

## See Also

* class [DiscordCdn](./DiscordCdn.md)
* namespace [Oxide.Ext.Discord.Helpers](./HelpersNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
