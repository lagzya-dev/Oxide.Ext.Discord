using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Users
{
    public class NickId
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("nick")]
        public string Nick { get; set; }
     
        public static implicit operator KeyValuePair<string, string>(NickId nick) => new KeyValuePair<string, string>(nick.Id, nick.Nick);
    }
}
