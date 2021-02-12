using Oxide.Core.Libraries.Covalence;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// Adds extension methods to Discord User to allow sending server chat commands to the player
    /// </summary>
    public static class DiscordUserExt
    {
        /// <summary>
        /// Send chat message to the user if they're connected
        /// </summary>
        /// <param name="user">User to send the message to on the server</param>
        /// <param name="message">Message to send</param>
        public static void SendChatMessage(this DiscordUser user, string message)
        {
            IPlayer player = user.GetPlayer();
            if (player != null && player.IsConnected)
            {
                player.Message(message);
            }
        }

        /// <summary>
        /// Send chat message to the user if they're connected
        /// </summary>
        /// <param name="user">User to send the message to on the server</param>
        /// <param name="message">Message to send</param>
        /// <param name="prefix">Message Prefix</param>
        /// <param name="args">Message Args</param>
        public static void SendChatMessage(this DiscordUser user, string message, string prefix, params object[] args)
        {
            IPlayer player = user.GetPlayer();
            if (player != null && player.IsConnected)
            {
                player.Message(message, prefix, args);
            }
        }

        /// <summary>
        /// Get's the IPlayer for the given discord user
        /// </summary>
        /// <param name="user">User to get IPlayer for</param>
        /// <returns>IPlayer if discord user is linked; null otherwise</returns>
        public static IPlayer GetPlayer(this DiscordUser user)
        {
            return DiscordExtension.DiscordLink.GetPlayer(user.Id);
        }
    }
}