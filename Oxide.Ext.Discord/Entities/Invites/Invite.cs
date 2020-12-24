using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.REST;

namespace Oxide.Ext.Discord.Entities.Invites
{
    public class Invite
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("guild")]
        public Guild Guild { get; set; }
        
        [JsonProperty("channel")]
        public Channel Channel { get; set; }
        
        [JsonProperty("inviter")]
        public User Inviter { get; set; }
        
        [JsonProperty("target_user")]
        public User TargetUser { get; set; }
        
        [JsonProperty("target_user_type")]
        public TargetUserType? UserTargetType { get; set; }
        
        [JsonProperty("approximate_presence_count")]
        public int? ApproximatePresenceCount { get; set; }
        
        [JsonProperty("approximate_member_count")]
        public int? ApproximateMemberCount { get; set; }

        public static void GetInvite(DiscordClient client, string inviteCode, Action<Invite> callback = null)
        {
            client.REST.DoRequest($"/invites/{inviteCode}", RequestMethod.GET, null, callback);
        }

        public void DeleteInvite(DiscordClient client, Action<Invite> callback = null)
        {
            client.REST.DoRequest($"/invites/{Code}", RequestMethod.DELETE, null, callback);
        }
    }
}
