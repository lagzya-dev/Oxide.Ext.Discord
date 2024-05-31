# Discord Extension Plugin Examples

Here you will find some examples of plugins using the discord extension. Some of the examples will leave out parts that do not related to the discord extension in order to keep the examples short.

## Table Of Contents
- [Discord Extension Plugin Examples](#discord-extension-plugin-examples)
    * [Basic Connecting Example](#basic-connecting-example)
    * [Early Connecting Example](#early-connecting-example)
    * [Early Connecting Example Option 2](#early-connecting-example-option-2)
    * [Advanced Connecting Example](#advanced-connecting-example)
    * [Registering Plugin for Discord Link](#registering-plugin-for-discord-link)
    * [Discord Command Example](#discord-command-example)
    * [Advanced command example](#advanced-command-example)
    * [Discord Channel Subscriptions](#discord-channel-subscriptions)
    
## Basic Connecting Example

```c#
using System.Linq;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Clients;
using Oxide.Ext.Discord.Connections;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Interfaces;

namespace Oxide.Plugins
{
    [Info("Connect Example", "MJSU", "1.0.0")]
    [Description("Example on how to connect to the discord extension")]
    internal class ConnectExample : CovalencePlugin, IDiscordPlugin
    {
        #region Class Fields
        public DiscordClient Client {get; set;}
        #endregion

        private void OnServerInitialized()
        {
            Client.Connect("YourBotToken", GatewayIntents.Guilds | GatewayIntents.GuildMembers);
        }

        [HookMethod(DiscordExtHooks.OnDiscordGatewayReady)]
        private void OnDiscordGatewayReady(GatewayReadyEvent ready)
        {
            Puts($"Bot connected to:{ready.Guilds.FirstOrDefault().Value.Name}");
        }
    }
}
```

## Early Connecting Example
This example is if you're wanting to connect to the discord before the OnServerInitialized hook is called
```c#
using System.Linq;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Clients;
using Oxide.Ext.Discord.Connections;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Interfaces;

namespace Oxide.Plugins
{
    [Info("Connect Example", "MJSU", "1.0.0")]
    [Description("Example on how to connect to the discord extension")]
    internal class ConnectExample : CovalencePlugin, IDiscordPlugin
    {
        #region Class Fields
        public DiscordClient Client {get; set;}
        #endregion

        private void OnDiscordClientCreated()
        {
            Client.Connect("YourBotToken", GatewayIntents.Guilds | GatewayIntents.GuildMembers);
        }

        [HookMethod(DiscordExtHooks.OnDiscordGatewayReady)]
        private void OnDiscordGatewayReady(GatewayReadyEvent ready)
        {
            Puts($"Bot connected to:{ready.Guilds.FirstOrDefault().Value.Name}");
        }
    }
}
```

## Registering Plugin for Discord Link
```c#
using System.Collections.Generic;
using System.Linq;
using Oxide.Core;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Clients;
using Oxide.Ext.Discord.Connections;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Libraries;

namespace Oxide.Plugins
{
    [Info("Discord Link Example", "MJSU", "1.0.0")]
    [Description("Example on how to use Discord Link")]
    internal class ConnectExample : CovalencePlugin, IDiscordPlugin, IDiscordLink
    {
        #region Class Fields
        public DiscordClient Client {get; set;}
        
        private readonly DiscordLink _link = Interface.Oxide.GetLibrary<DiscordLink>();
        #endregion

        private void OnServerInitialized()
        {
            //Register your plugin as a discord link plugin
            _link.AddLinkPlugin(this);
            
            Client.Connect("YourBotToken", GatewayIntents.Guilds | GatewayIntents.GuildMembers);
        }

        [HookMethod(DiscordExtHooks.OnDiscordGatewayReady)]
        private void OnDiscordGatewayReady(GatewayReadyEvent ready)
        {
            Puts($"Bot connected to:{ready.Guilds.FirstOrDefault().Value.Name}");
        }

        //Return your saved discord to server links.
        //Note this is only called once while registering plugin to link
        public IDictionary<PlayerId, Snowflake> GetPlayerIdToDiscordIds()
        {
            return new Hash<PlayerId, Snowflake>
            {
                [new PlayerId("512312512")] = new Snowflake(12321321)
            };
        }
        
        //Method in your plugin should call _link.OnLinked whenever a player and discord are linked
        public void OnLinked(IPlayer player, DiscordUser user) 
        {
            _link.OnLinked(this, player, user);
        }
        
        //Method in your plugin should call _link.OnUnlinked whenever a player and discord are unlinked
        public void OnUnlinked(IPlayer player, DiscordUser user) 
        {
            _link.OnUnlinked(this, player, user);
        }
    }
}
```

## Discord Command Example
**NOTE** Discord Command is considered deprecated and will be removed in a future update

```c#
using System.Linq;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Attributes;
using Oxide.Ext.Discord.Clients;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Libraries;

namespace Oxide.Plugins
{
    [Info("Command Example", "MJSU", "1.0.0")]
    [Description("Example on how to use discord commands")]
    internal class ConnectExample : CovalencePlugin, IDiscordPlugin
    {
        #region Class Fields
        public DiscordClient Client { get; set; }
        
        private readonly DiscordCommand _dcCommands = Interface.Oxide.GetLibrary<DiscordCommand>();
        #endregion

        private void OnServerInitialized()
        {
            Client.Connect("YourBotToken", GatewayIntents.Guilds | GatewayIntents.GuildMembers);
        }

        [HookMethod(DiscordExtHooks.OnDiscordGatewayReady)]
        private void OnDiscordGatewayReady(GatewayReadyEvent ready)
        {
            Puts($"Bot connected to:{ready.Guilds.FirstOrDefault().Value.Name}");
        }

        //By passing true instead of false it will use the lang key with the given string as the command
        [GuildCommand("myguildcommand", false)]
        private void MyGuildCommand(DiscordMessage message, string cmd, string[] args)
        {
            message.Reply(Client,"You used the guild command");
        }
        
        //By passing true instead of false it will use the lang key with the given string as the command
        [DirectMessageCommand("mydirectcommand", false)]
        private void MyDirectCommand(DiscordMessage message, string cmd, string[] args)
        {
            message.Reply(Client,"You used the direct message command command");
        }
    }
}
```

## Advanced command example
**NOTE** Discord Command is considered deprecated and will be removed in a future update
```c#
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Clients;
using Oxide.Ext.Discord.Connections;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Libraries;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Plugins
{
    [Info("Advanced Command Example", "MJSU", "1.0.0")]
    [Description("Example on how to do advanced command setup")]
    internal class ConnectExample : CovalencePlugin, IDiscordPlugin
    {
        #region Class Fields

        public DiscordClient Client { get; set; }

        private PluginConfig _pluginConfig;

        private readonly DiscordCommand _dcCommands = Interface.Oxide.GetLibrary<DiscordCommand>();

        private readonly BotConnection _discordSettings = new();

        #endregion

        private void OnServerInitialized()
        {
            _discordSettings.ApiToken = _pluginConfig.DiscordApiKey;
            _discordSettings.Intents = GatewayIntents.GuildMessages | GatewayIntents.DirectMessages | GatewayIntents.Guilds | GatewayIntents.GuildMembers;
            _discordSettings.LogLevel = _pluginConfig.ExtensionDebugging;

            RegisterDiscordCommand(nameof(MyDiscordCommand), "myDiscordCommand", _pluginConfig.AllowInDm, _pluginConfig.AllowInGuild, _pluginConfig.AllowedChannels);
            RegisterDiscordLangCommand(nameof(MyDiscordCommand), "lang key", _pluginConfig.AllowInDm, _pluginConfig.AllowInGuild, _pluginConfig.AllowedChannels);

            Client.Connect(_discordSettings);
        }

        [HookMethod(DiscordExtHooks.OnDiscordGatewayReady)]
        private void OnDiscordGatewayReady(GatewayReadyEvent ready)
        {
            Puts($"Bot connected to:{ready.Guilds.FirstOrDefault().Value.Name}");
        }

        private void MyDiscordCommand(DiscordMessage message, string cmd, string[] args)
        {
            message.Reply(Client, "You used my discord command");
        }

        //Registers a command by name
        public void RegisterDiscordCommand(string command, string name, bool direct, bool guild, List<Snowflake> allowedChannels)
        {
            if (direct)
            {
                _dcCommands.AddDirectMessageCommand(name, this, command);
            }

            if (guild)
            {
                _dcCommands.AddGuildCommand(name, this, allowedChannels, command);
            }
        }

        //Registers a command by lang key
        public void RegisterDiscordLangCommand(string command, string langKey, bool direct, bool guild, List<Snowflake> allowedChannels)
        {
            if (direct)
            {
                _dcCommands.AddDirectMessageLocalizedCommand(langKey, this, command);
            }

            if (guild)
            {
                _dcCommands.AddGuildLocalizedCommand(langKey, this, allowedChannels, command);
            }
        }

        private class PluginConfig
        {
            [DefaultValue("")]
            [JsonProperty(PropertyName = "Discord Bot Token")]
            public string DiscordApiKey { get; set; }

            [DefaultValue(true)]
            [JsonProperty(PropertyName = "Allow Discord Commands In Direct Messages")]
            public bool AllowInDm { get; set; }

            [DefaultValue(false)]
            [JsonProperty(PropertyName = "Allow Discord Commands In Guild")]
            public bool AllowInGuild { get; set; }

            [JsonProperty(PropertyName = "Allow Guild Commands Only In The Following Guild Channel Or Category (Channel ID Or Category ID)")]
            public List<Snowflake> AllowedChannels { get; set; }

            [JsonConverter(typeof(StringEnumConverter))]
            [DefaultValue(DiscordLogLevel.Info)]
            [JsonProperty(PropertyName = "Discord Extension Log Level (Verbose, Debug, Info, Warning, Error, Exception, Off)")]
            public DiscordLogLevel ExtensionDebugging { get; set; }
        }
    }
}
```

## Discord Channel Subscriptions
```c#
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Clients;
using Oxide.Ext.Discord.Connections;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Libraries;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Plugins
{
    [Info("Channels Subscriptions Example", "MJSU", "1.0.0")]
    [Description("Example on how to subscribe to a channel")]
    internal class ConnectExample : CovalencePlugin, IDiscordPlugin
    {
        #region Class Fields
        public DiscordClient Client { get; set; }

        private PluginConfig _pluginConfig;

        private readonly DiscordSubscriptions _subscriptions = Interface.Oxide.GetLibrary<DiscordSubscriptions>();

        private readonly BotConnection _discordSettings = new BotConnection();
        
        #endregion

        private void OnServerInitialized()
        {
            _discordSettings.ApiToken = _pluginConfig.DiscordApiKey;
            _discordSettings.Intents = GatewayIntents.GuildMessages | GatewayIntents.DirectMessages | GatewayIntents.Guilds | GatewayIntents.GuildMembers;
            _discordSettings.LogLevel = _pluginConfig.ExtensionDebugging;

            _subscriptions.AddChannelSubscription(Client, _pluginConfig.SubscribedChannelId, OnChannelMessage);
            Client.Connect(_discordSettings);
        }

        [HookMethod(DiscordExtHooks.OnDiscordGatewayReady)]
        private void OnDiscordGatewayReady(GatewayReadyEvent ready)
        {
            Puts($"Bot connected to:{ready.Guilds.FirstOrDefault().Value.Name}");
        }
        
        private void OnChannelMessage(DiscordMessage message)
        {
            message.Reply(Client, "I am subscribed to listen to messages in this channel");
        }

        private class PluginConfig
        {
            [DefaultValue("")]
            [JsonProperty(PropertyName = "Discord Bot Token")]
            public string DiscordApiKey { get; set; }
            
            [DefaultValue("")]
            [JsonProperty(PropertyName = "Subscribed Channel ID")]
            public Snowflake SubscribedChannelId { get; set; }

            [JsonConverter(typeof(StringEnumConverter))]
            [DefaultValue(DiscordLogLevel.Info)]
            [JsonProperty(PropertyName = "Discord Extension Log Level (Verbose, Debug, Info, Warning, Error, Exception, Off)")]
            public DiscordLogLevel ExtensionDebugging { get; set; }
        }
    }
}
```