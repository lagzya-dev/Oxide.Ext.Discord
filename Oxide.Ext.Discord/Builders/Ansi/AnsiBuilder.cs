using System.Text;
using Oxide.Ext.Discord.Cache;

namespace Oxide.Ext.Discord.Builders.Ansi
{
public class AnsiBuilder
    {
        private readonly StringBuilder _sb = new StringBuilder();

        public AnsiBuilder()
        {
            _sb.AppendLine("```ansi");
        }

        public void Append(string text, TextColor color = TextColor.Default, BackgroundColor background = BackgroundColor.Default, FontStyle style = FontStyle.Default)
        {
            _sb.Append("\u001b[");
            ProcessStyles(style);

            if (background != BackgroundColor.Default)
            {
                _sb.Append(EnumCache<BackgroundColor>.Instance.ToString(background));
                if (color != TextColor.Default)
                {
                    _sb.Append(';');
                }
            }

            if (color != TextColor.Default)
            {
                _sb.Append(EnumCache<TextColor>.Instance.ToString(color));
            }

            _sb.Append('m');
            _sb.Append(text);
            Reset();
        }

        public void AppendLine(string text, TextColor color = TextColor.Default, BackgroundColor background = BackgroundColor.Default, FontStyle style = FontStyle.Default)
        {
            Append(text, color, background, style);
            _sb.AppendLine();
        }

        public void AppendLine()
        {
            _sb.AppendLine();
        }
        
        private void ProcessStyles(FontStyle style)
        {
            if (style == FontStyle.Default)
            {
                _sb.Append("0;");
                return;
            }
            
            if (HasFlag(style, FontStyle.Bold))
            {
                _sb.Append("1;");
            }
            
            if (HasFlag(style, FontStyle.Underline))
            {
                _sb.Append("4;");
            }
        }

        private void Reset()
        {
            _sb.Append("\u001b[0;0m");
        }

        private bool HasFlag(FontStyle style, FontStyle flag)
        {
            return (style & flag) == flag;
        }

        public string Build()
        {
            _sb.AppendLine();
            _sb.Append("```");
            return _sb.ToString();
        }
    }
}