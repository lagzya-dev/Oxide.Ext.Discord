using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.REST;

namespace Oxide.Ext.Discord.Entities.Invites
{
    /// <summary>
    /// Represents an <a href="https://discord.com/developers/docs/resources/invite#invite-object">Invite Structure</a> that when used, adds a user to a guild or group DM channel.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Invite
    {
        /// <summary>
        /// The invite code (unique ID)
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// The guild this invite is for
        /// See <see cref="Guild"/>
        /// </summary>
        [JsonProperty("guild")]
        public Guild Guild { get; set; }
        
        /// <summary>
        /// The channel this invite is for
        /// See <see cref="Channel"/>
        /// </summary>
        [JsonProperty("channel")]
        public Channel Channel { get; set; }
        
        /// <summary>
        /// The user who created the invite
        /// See <see cref="DiscordUser"/>
        /// </summary>
        [JsonProperty("inviter")]
        public DiscordUser Inviter { get; set; }
        
        /// <summary>
        /// The target user for this invite
        /// See <see cref="DiscordUser"/>
        /// </summary>
        [JsonProperty("target_user")]
        public DiscordUser TargetUser { get; set; }
        
        /// <summary>
        /// The type of user target for this invite
        /// See <see cref="TargetUserType"/>
        /// </summary>
        [JsonProperty("target_user_type")]
        public TargetUserType? UserTargetType { get; set; }
        
        /// <summary>
        /// Approximate count of online members (only present when target_user is set)
        /// </summary>
        [JsonProperty("approximate_presence_count")]
        public int? ApproximatePresenceCount { get; set; }
        
        /// <summary>
        /// Approximate count of total members
        /// </summary>
        [JsonProperty("approximate_member_count")]
        public int? ApproximateMemberCount { get; set; }

        /// <summary>
        /// Get Invite URL Parameters
        /// See <a href="https://discord.com/developers/docs/resources/invite#get-invite">Get Invite</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="inviteCode">Invite code</param>
        /// <param name="callback">Callback with the invite</param>
        public static void GetInvite(DiscordClient client, string inviteCode, Action<Invite> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/invites/{inviteCode}", RequestMethod.GET, null, callback, onError);
        }

        /// <summary>
        /// Delete an invite.
        /// Requires the MANAGE_CHANNELS permission on the channel this invite belongs to, or MANAGE_GUILD to remove any invite across the guild.
        /// Returns an invite object on success.
        /// See <a href="https://discord.com/developers/docs/resources/invite#delete-invite">Delete Invite</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the deleted invite</param>
        public void DeleteInvite(DiscordClient client, Action<Invite> callback = null, Action<RestError> onError = null)
        {
            client.Bot.Rest.DoRequest($"/invites/{Code}", RequestMethod.DELETE, null, callback, onError);
        }
    }
}
