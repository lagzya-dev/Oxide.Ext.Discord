using System;
using System.Collections.Generic;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Libraries.Linking;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Plugins;
using Oxide.Ext.Discord.Promise;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// IPlayer Extensions for sending Discord Message to an IPlayer
    /// </summary>
    public static class PlayerExt
    {
        /// <summary>
        /// Send a Discord Message to an IPlayer if they're registered
        /// </summary>
        /// <param name="player">Player to send the discord message to</param>
        /// <param name="client">Client to use for sending the message</param>
        /// <param name="message">Message to send</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback with error information</param>
        public static IDiscordPromise<DiscordMessage> SendDiscordMessage(this IPlayer player, DiscordClient client, string message)
        {
            MessageCreate create = new MessageCreate
            {
                Content = message
            };
            
            return player.SendDiscordMessage(client, create);
        }

        /// <summary>
        /// Send a Discord Message to an IPlayer if they're registered
        /// </summary>
        /// <param name="player">Player to send the discord message to</param>
        /// <param name="client">Client to use for sending the message</param>
        /// <param name="embed">Embed to send</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback with error information</param>
        public static IDiscordPromise<DiscordMessage> SendDiscordMessage(this IPlayer player, DiscordClient client, DiscordEmbed embed)
        {
            MessageCreate create = new MessageCreate
            {
                Embeds = new List<DiscordEmbed> {embed}
            };
            
            return player.SendDiscordMessage(client, create);
        }
        
        /// <summary>
        /// Send a Discord Message to an IPlayer if they're registered
        /// </summary>
        /// <param name="player">Player to send the discord message to</param>
        /// <param name="client">Client to use for sending the message</param>
        /// <param name="embeds">Embeds to send</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback with error information</param>
        public static IDiscordPromise<DiscordMessage> SendDiscordMessage(this IPlayer player, DiscordClient client, List<DiscordEmbed> embeds)
        {
            MessageCreate create = new MessageCreate
            {
                Embeds = embeds
            };
            
            return player.SendDiscordMessage(client, create);
        }
        
        /// <summary>
        /// Send a Discord Message to an IPlayer if they're registered
        /// </summary>
        /// <param name="player">Player to send the discord message to</param>
        /// <param name="client">Client to use for sending the message</param>
        /// <param name="message">Message to send</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback with error information</param>
        public static IDiscordPromise<DiscordMessage> SendDiscordMessage(this IPlayer player, DiscordClient client, MessageCreate message)
        {
            return SendMessage(client, player.GetDiscordUserId(), message);
        }

        /// <summary>
        /// Send a message in a DM to the linked user using a global message template
        /// </summary>
        /// <param name="player">Player to send the message to</param>
        /// <param name="client">Client to use</param>
        /// <param name="plugin">Plugin for the template</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="message">Message to use (optional)</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        /// <param name="callback">Callback when the message is created</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static IDiscordPromise<DiscordMessage> SendDiscordGlobalTemplateMessage(this IPlayer player, DiscordClient client, Plugin plugin, string templateName, MessageCreate message = null, PlaceholderData placeholders = null)
        {
            MessageCreate template = DiscordExtension.DiscordMessageTemplates.GetPlayerTemplate(plugin, templateName, player).ToMessage(placeholders, message);
            return SendMessage(client, player.GetDiscordUserId(), template);
        }

        /// <summary>
        /// Send a message in a DM to the linked user using a localized message template
        /// </summary>
        /// <param name="player">Player to send the message to</param>
        /// <param name="client">Client to use</param>
        /// <param name="plugin">Plugin for the template</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="message">Message to use (optional)</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        /// <param name="callback">Callback when the message is created</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static IDiscordPromise<DiscordMessage> SendDiscordTemplateMessage(this IPlayer player, DiscordClient client, Plugin plugin, string templateName, MessageCreate message = null, PlaceholderData placeholders = null)
        {
            MessageCreate template = DiscordExtension.DiscordMessageTemplates.GetPlayerTemplate(plugin, templateName, player).ToMessage(placeholders, message);
            return SendMessage(client, player.GetDiscordUserId(), template);
        }
        
        private static IDiscordPromise<DiscordMessage> SendMessage(DiscordClient client, Snowflake? id, MessageCreate message)
        {
            if (!client.IsConnected())
            {
                return DiscordPromise<DiscordMessage>.FromResult(null);
            }
            
            if (!id.HasValue)
            {
                return DiscordPromise<DiscordMessage>.FromResult(null);;
            }
            
            DiscordChannel channel = client.Bot.DirectMessagesByUserId[id.Value];
            if (channel != null)
            {
                return channel.CreateMessage(client, message);
            }

            DiscordUser user = EntityCache<DiscordUser>.Instance.GetOrCreate(id.Value);
            return user.SendDirectMessage(client, message);
        }

        /// <summary>
        /// Returns true if the player is linked
        /// </summary>
        /// <param name="player">Player to check if they're linked</param>
        /// <returns>True if linked; False otherwise</returns>
        public static bool IsLinked(this IPlayer player)
        {
            return DiscordLink.Instance.IsLinked(player.Id);
        }

        /// <summary>
        /// Returns the Discord ID of the IPlayer if linked
        /// </summary>
        /// <param name="player">Player to get Discord ID for</param>
        /// <returns>Discord ID if linked; null otherwise</returns>
        public static Snowflake? GetDiscordUserId(this IPlayer player)
        {
            return DiscordLink.Instance.GetDiscordId(player);
        }
        
        /// <summary>
        /// Returns a minimal Discord User for the given player
        /// </summary>
        /// <param name="player">Player to get Discord User for</param>
        /// <returns>Discord User if linked; null otherwise</returns>
        public static DiscordUser GetDiscordUser(this IPlayer player)
        {
            return DiscordLink.Instance.GetDiscordUser(player);
        }

        /// <summary>
        /// Returns a minimal Guild Member for the given player
        /// </summary>
        /// <param name="player">Player to get Discord User for</param>
        /// <param name="guild">Guild the member is in</param>
        /// <returns>GuildMember if linked and in guild; null otherwise</returns>
        public static GuildMember GetGuildMember(this IPlayer player, DiscordGuild guild)
        {
            return DiscordLink.Instance.GetLinkedMember(player, guild);
        }

        /// <summary>
        /// Returns if the IPlayer is a <see cref="DiscordDummyPlayer"/>
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool IsDummyPlayer(this IPlayer player) => player is DiscordDummyPlayer;
    }
}