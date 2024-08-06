using System;
using System.Collections;
using System.Collections.Generic;
using Oxide.Ext.Discord.Libraries;

namespace Oxide.Ext.Discord.Types
{
    internal class ReverseStringTokenizer : BasePoolable, IEnumerator<ReadOnlyMemory<char>>
    {
        private string _string;
        private char _token;
        private int _stringIndex;

        public ReadOnlyMemory<char> Current { get; private set; }

        object IEnumerator.Current => Current;

        public static ReverseStringTokenizer Create(string str, char token)
        {
            ReverseStringTokenizer tokenizer = DiscordPool.Internal.Get<ReverseStringTokenizer>();
            tokenizer.Init(str, token);
            return tokenizer;
        }

        private void Init(string str, char token)
        {
            _string = str;
            _token = token;
            _stringIndex = str.Length - 1;
        }

        public bool MoveNext()
        {
            if (_stringIndex <= 0)
            {
                return false;
            }
            
            int index = _string.LastIndexOf(_token, _stringIndex) + 1;
            if (index < 0)
            {
                index = 0;
            }
            
            int length = _stringIndex - index + 1;
            if (length == 0)
            {
                _stringIndex = index - 2;
                return MoveNext();
            }

            Current = _string.AsMemory().Slice(index, length);
            _stringIndex = index - 2;
            return true;
        }

        public void Reset()
        {
            _stringIndex = 0;
            Current = null;
        }

        protected override void EnterPool()
        {
            _string = null;
            Current = null;
            _token = default(char);
            _stringIndex = 0;
        }
    }
}