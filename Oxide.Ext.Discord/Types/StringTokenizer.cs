using System.Collections;
using System.Collections.Generic;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Types
{
    internal class StringTokenizer : BasePoolable, IEnumerator<string>
    {
        private string _string;
        private char _token;
        private int _maxLength;
        private int _stringIndex;
        public int Index { get; private set; } = -1;

        public string Current { get; private set; }

        object IEnumerator.Current => Current;

        public static StringTokenizer Create(string str, char token) => Create(str, token, str.Length);

        public static StringTokenizer Create(string str, char token, int maxLength)
        {
            StringTokenizer tokenizer = DiscordPool.Internal.Get<StringTokenizer>();
            tokenizer.Init(str, token, maxLength);
            return tokenizer;
        }

        private void Init(string str, char token, int maxLength)
        {
            _string = str;
            _token = token;
            _maxLength = maxLength;
        }

        public bool MoveNext()
        {
            if (_stringIndex >= _maxLength)
            {
                return false;
            }
            
            int index = _string.IndexOf(_token, _stringIndex);
            if (index == -1 || index >= _maxLength)
            {
                index = _maxLength;
            }
            
            int length = index - _stringIndex;
            if (length == 0)
            {
                _stringIndex = index + 1;
                return MoveNext();
            }

            Current = _string.Substring(_stringIndex, length);
            _stringIndex = index + 1;
            Index++;
            return true;
        }

        public void Reset()
        {
            _stringIndex = 0;
            Current = null;
            Index = -1;
        }

        protected override void EnterPool()
        {
            _stringIndex = 0;
            Current = null;
            _token = default(char);
            _maxLength = 0;
            _stringIndex = 0;
            Index = -1;
        }
    }
}