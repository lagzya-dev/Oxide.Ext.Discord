using Newtonsoft.Json;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Entities.Api
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class RateLimitContent : BasePoolable
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        
        [JsonProperty("code")]
        public int? Code { get; set; }

        protected override void EnterPool()
        {
            Message = null;
            Code = null;
        }
    }
}