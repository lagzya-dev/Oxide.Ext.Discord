using System.Text.RegularExpressions;
using Oxide.Ext.Discord.Types;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Cache
{
    public sealed partial class EmojiCache : Singleton<EmojiCache>
    {
        private readonly Hash<string, string> _emojiToText = new Hash<string, string>();
        private readonly Hash<string, string> _textToEmoji = new Hash<string, string>();
        private readonly Regex _emojiRegex = new Regex(@"(\u00a9|\u00ae|[\u2000-\u3300]|\ud83c[\ud000-\udfff]|\ud83d[\ud000-\udfff]|\ud83e[\ud000-\udfff])", RegexOptions.Compiled);
        private readonly Regex _textRegex = new Regex(@":[\d\w_]*", RegexOptions.Compiled);

        private EmojiCache() { }
        
        public string EmojiToText(string emoji) => _emojiToText[emoji];

        public string TextToEmoji(string text) => _textToEmoji[text];

        public string ReplaceEmojiWithText(string text, string nonMatchReplacement = "") => _emojiRegex.Replace(text, match => _emojiToText[match.Value] ?? nonMatchReplacement);

        public string ReplaceTextWithEmoji(string text, string nonMatchReplacement = "") => _textRegex.Replace(text, match => _textToEmoji[match.Value] ?? nonMatchReplacement);
    }
}