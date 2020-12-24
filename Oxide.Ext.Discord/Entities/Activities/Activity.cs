using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Emojis;
using Oxide.Ext.Discord.Helpers;

namespace Oxide.Ext.Discord.Entities.Activities
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Activity
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type")]
        public ActivityType Type { get; set; }
        
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }
        
        public DateTime CreatedAtDt => CreatedAt.ToDateTime();
        
        [JsonProperty("timestamps")]
        public List<ActivityTimestamps> Timestamps { get; set; }
        
        [JsonProperty("application_id")]
        public string ApplicationId { get; set; }
        
        [JsonProperty("details")]
        public string Details { get; set; }
        
        [JsonProperty("state")]
        public string State { get; set; }
        
        [JsonProperty("emoji")]
        public Emoji Emoji { get; set; }
        
        [JsonProperty("party")]
        public ActivityParty Party { get; set; }
        
        [JsonProperty("assets")]
        public ActivityAssets Assets { get; set; }
        
        [JsonProperty("secrets")]
        public ActivitySecrets Secrets { get; set; }
        
        [JsonProperty("instance")]
        public bool? Instance { get; set; }
        
        [JsonProperty("flags")]
        public ActivityFlags? Flags { get; set; }
    }
}