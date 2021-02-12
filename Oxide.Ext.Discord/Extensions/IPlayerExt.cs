using Oxide.Core.Libraries.Covalence;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// IPlayer Extensions for sending Discord Message to an IPlayer
    /// </summary>
    public static class IPlayerExt
    {
        /// <summary>
        /// Send a Discord Message to an IPlayer if they're registered
        /// </summary>
        /// <param name="player">Player to send the discord message to</param>
        /// <param name="client">Client to use for sending the message</param>
        /// <param name="message">Message to send</param>
        public static void SendDiscordMessage(this IPlayer player, DiscordClient client, string message)
        {
            MessageCreate create = new MessageCreate
            {
                Content = message
            };
            
            player.SendDiscordMessage(client, create);
        }

        /// <summary>
        /// Send a Discord Message to an IPlayer if they're registered
        /// </summary>
        /// <param name="player">Player to send the discord message to</param>
        /// <param name="client">Client to use for sending the message</param>
        /// <param name="embed">Embed to send</param>
        public static void SendDiscordMessage(this IPlayer player, DiscordClient client, Embed embed)
        {
            MessageCreate create = new MessageCreate
            {
                Embed = embed
            };
            
            player.SendDiscordMessage(client, create);
        }
        
        /// <summary>
        /// Send a Discord Message to an IPlayer if they're registered
        /// </summary>
        /// <param name="player">Player to send the discord message to</param>
        /// <param name="client">Client to use for sending the message</param>
        /// <param name="message">Message to send</param>
        public static void SendDiscordMessage(this IPlayer player, DiscordClient client, MessageCreate message)
        {
            SendMessage(client, GetUser(player), message);
        }
        
        private static void SendMessage(DiscordClient client, DiscordUser user, MessageCreate message)
        {
            if (user == null)
            {
                return;
            }

            Channel channel = client.Bot.DirectMessagesByUserId[user.Id];
            if (channel != null)
            {
                channel.CreateMessage(client, message);
                return;
            }
            
            user.CreateDirectMessage(client, newChannel => newChannel.CreateMessage(client, message));
        }

        /// <summary>
        /// Returns a DiscordUser with the ID populated if linked
        /// </summary>
        /// <param name="player">Player to get user for</param>
        /// <returns>Discord User if linked; null otherwise</returns>
        public static DiscordUser GetUser(this IPlayer player)
        {
            Snowflake? id = DiscordExtension.DiscordLink.GetDiscordId(player);
            if (id.HasValue)
            {
                return new DiscordUser
                {
                    Id = id.Value
                };
            }

            return null;
        }
        
        /// <summary>
        /// Returns the Discord ID of the IPlayer if linked
        /// </summary>
        /// <param name="player">Player to get Discord ID for</param>
        /// <returns>Discord ID if linked; null otherwise</returns>
        public static Snowflake? GetUserId(this IPlayer player)
        {
            return DiscordExtension.DiscordLink.GetDiscordId(player);
        }
    }
}