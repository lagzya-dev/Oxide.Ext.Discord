## About

**Oxide.Ext.Discord** is an Oxide extension which acts as a bridge between Oxide and the Discord API.

Should you encounter a problem or bug with the extension, please feel free to create an issue here. Try to include as much detail as possible, including steps to reproduce the issue. A code example is highly appreciated.

## Upgrading to version 3.0.0
Before upgrading to version 3.0.0 make sure all the discord extension plugins you plan to use support the new version. 
Plugins that were made for version 1.0.0 or 2.0.0 are **not** compatible with version 3.0.0.

## Installation

To install the extension to your Oxide server, you must follow a few simple steps:
1) Open the server files, and navigate to the "Managed" folder (eg. "RustDedicated_Data/Managed")
2) Download the latest release.
3) Copy the "Oxide.Ext.Discord.dll" file into your "Managed" folder.
4) Restart your server!

## Developer

If you would like to create plugins for the extension please [Click Here](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Docs/README.md) to learn more  
If you would like to view the Discord Extension Documentation [Click Here](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Docs/Generated/Oxide.Ext.Discord.md)  
**If the links don't work please try the readme on the github page**

## Getting your API Key

An API key is used to authenticate requests made to and from Discord.

**Note: DO NOT SHARE YOUR API KEY!**   
Sharing your key may result in punishments from Discord (including a platform-wide ban) if the token is used to abuse the API.

Obtaining an API Key:
1) Visit the official Discord Developers page here: [Discord Developer Portal](https://discord.com/developers/applications)
2) Click "New App".  
   ![](https://i.postimg.cc/ZKwQdZZP/1-New-Application.png)
3) Name your app and click create!  
   ![](https://i.postimg.cc/Vk5V9TLx/2-Create-App-Name.png)
4) You will now be redirected to your created app. Click on "Bot" on the left hand side and then "Add Bot".  
   ![](https://i.postimg.cc/htw32rXf/3-Add-Bot.png)
5) A) Under the newly created bot section, Enter the username for your bot and upload an icon.  
   B) Then enable the Presence and Server Members Intent and Message Content Intent.  
   C) To get your Discord API Token click on the "Copy" button.  
   D) This is the token that is used by discord extension plugins.     
   ![](https://i.postimg.cc/WzPFSttB/5-discord-intents.png)
6) A.Next we're going to setup the permissions that bot has in your Discord Server.  
   B.Click on OAuth2 on the left hand side.  
   C.Then click on URL Generator.   
   D. Under Scope select "Bot".  
   E. Under permissions select which permissions that bot should have.  
   F. Once you have all of this selected click on the copy button.    
   ![](https://i.postimg.cc/ZnXStyHc/image.png)
7) Now it's time to add your new bot to your guild!
   Paste the link from the previous step into the url section of your browser.
   Select which Discord Server you want to invite the bot into and continue and then authorize.  
   ![](https://i.postimg.cc/JnPXqRxm/image.png)
8) Your bot will now be in your discord server.

## Configuration

The discord extension configuration can be found at oxide/discord.config.json. 
The config allows you to modify the command prefixes for commands that use the discord extension command library.

```json
{
   "Commands": {
      "Command Prefixes": [
         "/",
         "!"
      ]
   },
   "Rest": {
      "API Error Request Retries": 3,
      "API Rate Limit Request Retries": 6
   },
   "Logging": {
      "Server Console Log Level": "Info",
      "File Log Level": "Verbose",
      "Hide Discord Error Codes": [],
      "File DateTime Format": "HH:mm:ss.ff"
   },
   "Users": {
      "Direct Message Blocked Duration (Hours)": 24.0
   },
   "Search": {
      "Enable Player Name Trie Search (High Performance / High Memory Usage)": true
   },
   "Validation": {
      "Enable Request Validation": true
   },
   "Bot": {
      "Automatically Apply Gateway Intents": true
   }
}
```

## Application Command Permission Configuration
[How To Configure Application Command Permissions](https://discord.com/blog/slash-commands-permissions-discord-apps-bots)  
[Application Command Permissions FAQ](https://support.discord.com/hc/en-us/articles/4644915651095-Command-Permissions)  

## Commands
Console commands for the Discord Extension:  
`de.version` - displays the current Discord Extension version  
`de.websocket.reset` - resets all websockets to fix bugged connections  
`de.websocket.reconnect` - reconnect all websockets  
`de.rest.reset` - reset all rest API clients  
`de.search.highperformance.enable 1/0` - enable or disable high performance player name searches  
`de.placeholders.list` - list all available placeholders registered in the Discord Extension  
`de.pool.clearentities` - resets pool entities  
`de.pool.remove` - removes all pools currently registered  
`de.log.console ` - set's the console log level (Verbose, Debug, Info, Warning, Error, Exception, Off)  
`de.log.file` - set's the file log level (Verbose, Debug, Info, Warning, Error, Exception, Off)  
`de.validation.enable 1/0` - enable / disable api request validation  
`de.debug` - prints debug information for the Discord Extension  
`de.help` - displays the command help text  

## Discord Application Commands
Commands the Discord Extension registers on the bot:  
`/de commands delete` - deletes an application command from the bot. This should be used when a plugin registers a command but is no longed used.  

## Contributing

Want to contribute? Create a fork of the repo and create a pull request for any changes you wish to make! 
