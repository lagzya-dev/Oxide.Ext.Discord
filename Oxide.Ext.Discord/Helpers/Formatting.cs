namespace Oxide.Ext.Discord.Helpers
{
    public class Formatting
    {
        public static string PingUser(string userId) => $"<@{userId}>";
        public static string PingUserNickname(string userId) => $"<@!{userId}>";
        public static string PingChannel(string channelId) => $"<#{channelId}>";
        public static string PingRole(string roleId) => $"<@&{roleId}>";
        public static string CustomEmoji(string name, string id) => $"<:{name}:{id}>";
        public static string CustomAnimatedEmoji(string name, string id) => $"<a:{name}:{id}>";
        
        public static string Italics(string message) => $"*{message}*";
        public static string Bold(string message) => $"**{message}**";
        public static string ItalicsBold(string message) => $"***{message}***";
        public static string Underline(string message) => $"__{message}__";
        public static string UnderlineItalics(string message) => $"__*{message}*__";
        public static string UnderlineBold(string message) => $"__**{message}**__";
        public static string UnderlineBoldItalics(string message) => $"__***{message}***__";
        public static string Strikethrough(string message) => $"~~{message}~~";
        public static string CodeBlockOneLine(string message) => $"`{message}`";
        public static string CodeBlockMultiLine(string message) => $"```{message}```";
        public static string CodeBlockLanguage(string message, string language) => $"```{language}\n{message}```";
        public static string BlockQuoteSingleLine(string message) => $"> {message}";
        public static string BlockQuoteMultiLine(string message) => $">>> {message}";
    }
}