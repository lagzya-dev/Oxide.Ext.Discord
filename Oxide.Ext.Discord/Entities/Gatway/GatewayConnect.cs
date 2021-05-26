
namespace Oxide.Ext.Discord.Entities.Gatway
{
    /// <summary>
    /// Represents the websocket connection data
    /// </summary>
    public static class GatewayConnect
    {
        /// <summary>
        /// Which websocket version to use
        /// </summary>
        public static int Version => 9;

        /// <summary>
        /// How the data sent / received will be encoded
        /// </summary>
        public static string Encoding => "json";

        /// <summary>
        /// Compression the websocket should use
        /// </summary>
        public static string Compress => string.Empty;

        /// <summary>
        /// Generated connection string for the websocket
        /// </summary>
        /// <returns></returns>
        public static readonly string ConnectionArgs = $"v={Version}&encoding={Encoding}";
    }
}
