# Discord Link

[Discord Link](../Generated/Oxide.Ext.Discord/Libraries/Linking/DiscordLink.md) is a library provided by the Discord Extension.
This library allows plugin to access a standardized API to get information about a players who have linked their steam and discord accounts.
In order for Discord Link to work a plugin must be registered with Discord Link as the link plugin.
When the Discord Link plugin is unloaded then DiscordLink will remove that plugin as the link plugin.

## Getting Started

To gain access to the Discord Link API you add the following line of code in your plugin.
```c#
private readonly DiscordLink _link = GetLibrary<DiscordLink>();
```

## Registering Link Plugin

To register your plugin you first need to inherit the `IDiscordLinkPlugin` interface on your plugin.
The interface can be seen below and you must implement those methods on your plugin.

```c#
public class MyDiscordPlugin : CovalencePlugin, IDiscordLinkPlugin 
{

}

public interface IDiscordLinkPlugin
{
    IDictionary<string, Snowflake> GetSteamToDiscordIds();
}
```

Once your plugin has inherited from this interface you need to register your plugin in the following way

```c#
private readonly DiscordLink _link = Interface.Oxide.GetLibrary<DiscordLink>();

private void OnInit()
{
    if (//Linking Is Enabled)
    {
        _link.AddLinkPlugin(this);
    }
}

//Call _link.OnLinked when your plugin completes a link
private void HandleLink(IPlayer player, DiscordUser user)
{
    _link.OnLinked(this, player, user);
}

//Call _link.OnUnlinked when your plugin completes an unlink
private void HandleUnlink(IPlayer player, DiscordUser user)
{
    _link.OnUnlinked(this, player, user);
}
```

The plugin will automatically be removed as the link plugin when unloaded

## Player IDs
Player IDs are a struct used by Discord Link to represent Player ID's. Player ID's contain a string ID and some helper properties

```csharp
public struct PlayerId : IEquatable<PlayerId>
{
    // ID of the player
    public readonly string Id;
    
    // Returns true if the ID is valid; false otherwise
    public bool IsValid { get; }
       
    // Returns the IPlayer for the Player ID
    public IPlayer Player { get; }
}
```

## Extension Methods

Using Discord Link also provides a bunch of extension methods that can be used.
These methods are available on both IPLayer and DiscordUser objects.

### IPlayer Methods

There are extension methods that can be called from an IPlayer object in the following way.

```c#
void MyMethod(IPlayer player) 
{
    player.SendDiscordMessage(_client, "Hello Discord User! How do you do?");
}
```

#### Available IPlayer Extension Methods

```c#
// Sends a DM message to a discord user in a from the bot connected to the client 
void SendDiscordMessage(DiscordClient client, string message)
void SendDiscordMessage(DiscordClient client, Embed embed)
void SendDiscordMessage(this IPlayer player, DiscordClient client, MessageCreate message

// Returns true if the player is linked
bool IsLinked()

// Returns the Discord ID for the player.
// Value will be null if not linked
Snowflake? GetDiscordUserId()

// Returns a Discord User for the player
// The value will be null if not linked
// This is a minimal player with just the ID field set
DiscordUser GetDiscordUser()

// Returns the Guild Member of the linked player for the given guild.
// If the player is not linked or found in the guild null will be returned
GuildMember GetGuildMember(Guild guild)
```

### DiscordUser Methods

There are extension methods that can be called from an DiscordUser object in the following way.

```c#
void MyMethod(DiscordUser user) 
{
    user.SendChatMessage("Hello");
}
```

#### Available DiscordUser Extension Methods

```c#
//DiscordUser has a field called Player which returns the IPlayer for the linked Discord User.
Player

// Sends a chat message to the player on the server if they're connected
void SendChatMessage(string message)
void SendChatMessage(string message, string prefix, params object[] args)

// Returns true if the DiscordUser is linked
bool IsLinked()

// Returns true if the DiscordUser is linked and that linked player has permission
bool HasPermission(string permission)
```