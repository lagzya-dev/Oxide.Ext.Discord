using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects
{
    using System;
    using Oxide.Ext.Discord.REST;

    public class Invite
    {
        public string code { get; set; }

        public Guild guild { get; set; }

        public Channel channel { get; set; }
        
        [JsonProperty("inviter")]
        public User Inviter { get; set; }
        
        [JsonProperty("target_user")]
        public User TargetUser { get; set; }
        
        [JsonProperty("target_user_type")]
        public UserTargetType? UserTargetType { get; set; }
        
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
            client.REST.DoRequest($"/invites/{code}", RequestMethod.DELETE, null, callback);
        }
    }
}
