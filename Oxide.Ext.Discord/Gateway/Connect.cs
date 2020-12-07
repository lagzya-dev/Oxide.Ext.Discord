namespace Oxide.Ext.Discord.Gateway
{
    using Newtonsoft.Json;

    class Connect
    {
        [JsonProperty("v")]
        public static int Version { get; } = 8;

        [JsonProperty("encoding")]
        public static string Encoding { get; } = "json";

        [JsonProperty("compress")]
        public static string Compress { get; } = string.Empty;
        
        public static string Serialize() => $"v={Version}&encoding={Encoding}";
    }
}
