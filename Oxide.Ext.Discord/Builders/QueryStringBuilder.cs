using System.Collections.Generic;
using System.Text;

namespace Oxide.Ext.Discord.Builders
{
    public class QueryStringBuilder
    {
        private readonly StringBuilder _builder = new StringBuilder("?");

        public void Add(string key, string value)
        {
            _builder.Append(key);
            _builder.Append('=');
            _builder.Append(value);
            _builder.Append('&');
        }

        public void AddList<T>(string key, List<T> list, string separator)
        {
            _builder.Append(key);
            _builder.Append('=');
            for (int index = 0; index < list.Count; index++)
            {
                T entry = list[index];
                _builder.Append(entry.ToString());
                if (index + 1 != list.Count)
                {
                    _builder.Append(separator);
                }
            }

            _builder.Append('&');
        }

        public string ToString()
        {
            return _builder.ToString();
        }
    }
}