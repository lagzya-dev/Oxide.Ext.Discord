using System;
using Oxide.Core.Libraries.Covalence;
using Oxide.Ext.Discord.Entities;

namespace Oxide.Ext.Discord.Libraries
{
    /// <summary>
    /// Default Discord Extension provided Placeholder Keys
    /// </summary>
    public static class DefaultKeys
    {
        /// <summary>
        /// <see cref="DiscordApplicationCommand"/> Placeholder Keys
        /// </summary>
        public static readonly AppCommandKeys AppCommand = new AppCommandKeys("command");
        
        /// <summary>
        /// <see cref="DiscordChannel"/> Placeholder Keys
        /// </summary>
        public static readonly ChannelKeys Channel = new ChannelKeys("channel");
        
        /// <summary>
        /// <see cref="DateTime"/> Placeholder Keys
        /// </summary>
        public static readonly DateTimeKeys DateTime = new DateTimeKeys("datetime");
        
        /// <summary>
        /// <see cref="DateTime"/> Placeholder Keys
        /// </summary>
        public static readonly DateTimeKeys DateTimeNow = new DateTimeKeys("datetime.now");
        
        /// <summary>
        /// <see cref="DiscordGuild"/> Placeholder Keys
        /// </summary>
        public static readonly GuildKeys Guild = new GuildKeys("guild");
        
        /// <summary>
        /// <see cref="DiscordInteraction"/> Placeholder Keys
        /// </summary>
        public static readonly InteractionKeys Interaction = new InteractionKeys("interaction");
        
        /// <summary>
        /// IP address placeholders
        /// </summary>
        public static readonly IpKeys Ip = new IpKeys("ip");
        
        /// <summary>
        /// <see cref="GuildMember"/> Placeholder Keys
        /// </summary>
        public static readonly MemberKeys Member = new MemberKeys("member");
        
        /// <summary>
        /// <see cref="DiscordMessage"/> Placeholder Keys
        /// </summary>
        public static readonly MessageKeys Message = new MessageKeys("message");
        
        /// <summary>
        /// <see cref="IPlayer"/> Placeholder Keys
        /// </summary>
        public static readonly PlayerKeys Player = new PlayerKeys("player");
        
        /// <summary>
        /// <see cref="IPlayer"/> Target Placeholder Keys
        /// </summary>
        public static readonly PlayerKeys PlayerTarget = new PlayerKeys("target.player");
        
        /// <summary>
        /// <see cref="Plugin"/> Placeholder Keys
        /// </summary>
        public static readonly PluginKeys Plugin = new PluginKeys("plugin");
        
        /// <summary>
        /// <see cref="ResponseError"/> Placeholder Keys
        /// </summary>
        public static readonly ResponseErrorKeys ResponseError = new ResponseErrorKeys("response.error");
        
        /// <summary>
        /// <see cref="DiscordRole"/> Placeholder Keys
        /// </summary>
        public static readonly RoleKeys Role = new RoleKeys("role");
        
        /// <summary>
        /// <see cref="IServer"/> Placeholder Keys
        /// </summary>
        public static readonly ServerKeys Server = new ServerKeys("server");
        
        /// <summary>
        /// <see cref="Snowflake"/> Placeholder Keys
        /// </summary>
        public static readonly SnowflakeKeys Snowflake = new SnowflakeKeys("snowflake");
        
        /// <summary>
        /// <see cref="TimeSpan"/> Placeholder Keys
        /// </summary>
        public static readonly TimespanKeys Timespan = new TimespanKeys("timespan");
        
        /// <summary>
        /// Timestamp Placeholder Keys
        /// </summary>
        public static readonly TimestampKeys Timestamp = new TimestampKeys("timestamp");
        
        /// <summary>
        /// Timestamp Now Placeholder Keys
        /// </summary>
        public static readonly TimestampKeys TimestampNow = new TimestampKeys("timestamp.now");
        
        /// <summary>
        /// <see cref="DiscordUser"/> Placeholder Keys
        /// </summary>
        public static readonly UserKeys User = new UserKeys("user");
        
        /// <summary>
        /// <see cref="DiscordUser"/> Bot User Placeholder Keys
        /// </summary>
        public static readonly UserKeys Bot = new UserKeys("bot");
        
        /// <summary>
        /// <see cref="DiscordUser"/> Target Placeholder Keys
        /// </summary>
        public static readonly UserKeys UserTarget = new UserKeys("target.user");
    }
}