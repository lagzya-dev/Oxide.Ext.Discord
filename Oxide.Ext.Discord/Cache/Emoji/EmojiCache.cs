using System.Text.RegularExpressions;
using Oxide.Ext.Discord.Types;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Cache;

/// <summary>
/// Cached Emoji Data
/// </summary>
public sealed partial class EmojiCache : Singleton<EmojiCache>
{
    private readonly Hash<string, string> _emojiToText = new();
    private readonly Hash<string, string> _textToEmoji = new();
    private readonly Regex _emojiRegex = new(@"(\u00a9|\u00ae|[\u2000-\u3300]|\ud83c[\ud000-\udfff]|\ud83d[\ud000-\udfff]|\ud83e[\ud000-\udfff])", RegexOptions.Compiled);
    private readonly Regex _textRegex = new(@":[\d\w_]*", RegexOptions.Compiled);

    private EmojiCache() { }
        
    /// <summary>
    /// Convert an emoji character to the emoji string text
    /// </summary>
    /// <param name="emoji">Emoji to convert</param>
    /// <returns></returns>
    public string EmojiToText(string emoji) => _emojiToText[emoji];

    /// <summary>
    /// Convert emoji string text to an emoji character
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string TextToEmoji(string text) => _textToEmoji[text];

    /// <summary>
    /// Replaces emoji character with emoji string characters
    /// </summary>
    /// <param name="text"></param>
    /// <param name="nonMatchReplacement"></param>
    /// <returns></returns>
    public string ReplaceEmojiWithText(string text, string nonMatchReplacement = "") => _emojiRegex.Replace(text, match => _emojiToText[match.Value] ?? nonMatchReplacement);

    /// <summary>
    /// Replaces emoji string text with emoji characters
    /// </summary>
    /// <param name="text"></param>
    /// <param name="nonMatchReplacement"></param>
    /// <returns></returns>
    public string ReplaceTextWithEmoji(string text, string nonMatchReplacement = "") => _textRegex.Replace(text, match => _textToEmoji[match.Value] ?? nonMatchReplacement);
}