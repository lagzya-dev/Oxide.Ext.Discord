using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Interfaces.Logging;
using Oxide.Ext.Discord.Libraries.Pooling;

namespace Oxide.Ext.Discord.Logging
{
    public class DebugLogger
    {
        private readonly StringBuilder _logger = DiscordPool.Internal.GetStringBuilder();
        private const char IndentCharacter = '\t';

        private int _indent;

        public void IncrementIndent() => _indent++;
        public void DecrementIndent() => _indent = MathExt.Max(_indent - 1, 0);

        public void AppendIndent() => _logger.Append(IndentCharacter, _indent);

        public void AppendFieldPrefix(string name)
        {
            AppendIndent();
            _logger.Append(name);
            _logger.Append(": ");
        }
        
        public void AppendField(string name, string value)
        {
            AppendFieldPrefix(name);
            _logger.AppendLine(value);
        }

        public void AppendField(string name, int value) => AppendField(name, StringCache<int>.Instance.ToString(value));
        public void AppendField(string name, double value) => AppendField(name, StringCache<double>.Instance.ToString(value));
        public void AppendField(string name, float value) => AppendField(name, StringCache<float>.Instance.ToString(value));
        public void AppendField(string name, ulong value) => AppendField(name, StringCache<ulong>.Instance.ToString(value));
        public void AppendField(string name, long value) => AppendField(name, StringCache<long>.Instance.ToString(value));
        public void AppendField(string name, Snowflake value) => AppendField(name, value.ToString());
        public void AppendField(string name, Snowflake? value)
        {
            if (value.HasValue)
            {
                AppendField(name, value.Value);
                return;
            }
            
            AppendField(name, "Invalid ID");
        }

        public void AppendFieldEnum<T>(string name, T value)  where T : struct, IComparable, IFormattable, IConvertible => AppendField(name, EnumCache<T>.Instance.ToString(value));
        
        public void AppendFieldOutOf(string name, int amount, int total)
        {
            AppendFieldPrefix(name);
            _logger.Append(StringCache<int>.Instance.ToString(amount));
            _logger.Append('/');
            _logger.AppendLine(StringCache<int>.Instance.ToString(total));
        }

        public void AppendMethod(string name, MethodInfo info)
        {
            AppendFieldPrefix(name);
            _logger.Append(info.DeclaringType?.Name ?? "Unknown Type");
            _logger.Append('.');
            _logger.AppendLine(info.Name);
        }

        public void AppendField(string name, string value1, string value2)
        {
            AppendFieldPrefix(name);
            _logger.Append(value1);
            _logger.Append(' ');
            _logger.AppendLine(value2);
        }

        public void AppendField(string name, TimeSpan time)
        {
            AppendFieldPrefix(name);
            bool hasTime = AppendTimeSlice(time.TotalDays, time.Hours, " Days", false);
            hasTime = AppendTimeSlice(time.TotalHours, time.Hours, " Hours", hasTime);
            hasTime = AppendTimeSlice(time.TotalMinutes, time.Minutes, " Hours", hasTime);
            hasTime = AppendTimeSlice(time.TotalSeconds, time.Seconds, " Seconds", hasTime);
            if (hasTime)
            {
                return;
            }
            
            hasTime = AppendTimeSlice(time.TotalMilliseconds, time.Milliseconds, " Milliseconds", false);
            if (hasTime)
            {
                return;
            }
            
            _logger.Append("0 Seconds");
        }

        private bool AppendTimeSlice(double total, int time, string suffix, bool hasTime)
        {
            if (total < 1)
            {
                return false;
            }
            
            if (hasTime)
            {
                _logger.Append(' ');
            }

            _logger.Append(StringCache<int>.Instance.ToString(time));
            _logger.Append(suffix);
            return true;
        }
        
        public void AppendField(string name, bool value)
        {
            AppendFieldPrefix(name);
            _logger.AppendLine(value ? "Yes" : "No");
        }
        
        public void AppendNullField(string name)
        {
            AppendFieldPrefix(name);
            _logger.AppendLine("IS NULL");
        }

        public void AppendLine()
        {
            _logger.AppendLine();
        }
        
        public void AppendLine(char character, int amount)
        {
            AppendIndent();
            _logger.Append(character, amount);
            _logger.AppendLine();
        }
        
        public void AppendLine(string value)
        {
            AppendIndent();
            _logger.AppendLine(value);
        }

        public void AppendChannelPath(string name, DiscordGuild guild, DiscordChannel channel, DiscordChannel parent = null)
        {
            AppendFieldPrefix(name);
            _logger.Append(guild?.Name ?? "Unknown Guild");
            _logger.Append(" (");
            _logger.Append(guild?.Id.ToString());
            _logger.Append(")/");
            if (parent != null)
            {
                _logger.Append(parent.Name ?? "Unknown Channel");
                _logger.Append(" (");
                _logger.Append(parent.Id.ToString());
                _logger.Append(")/");
            }
            _logger.Append(channel?.Name ?? "Unknown Channel");
            _logger.Append(" (");
            _logger.Append(channel?.Id.ToString());
            _logger.Append(")");
        }

        public void AppendObject(string name, IDebugLoggable obj)
        {
            if (obj == null)
            {
                AppendNullField(name);
                return;
            }
            
            StartObject(name);
            obj.LogDebug(this);
            EndObject();
        }
        
        public void AppendList(string name, IEnumerable<string> items)
        {
            List<string> list = items.ToPooledList(DiscordPool.Internal);
            AppendList(name, list);
            DiscordPool.Internal.FreeList(list);
        }

        public void AppendList(string name, List<string> items)
        {
            if (items.Count == 0)
            {
                AppendField(name, "[]");
                return;
            }
            
            StartArray(name);
            for (int index = 0; index < items.Count; index++)
            {
                AppendLine(items[index]);
            }
            EndArray();
        }

        public void AppendList<T>(string name, IEnumerable<T> items) where T : IDebugLoggable
        {
            List<T> list = items.ToPooledList(DiscordPool.Internal);
            AppendList(name, list);
            DiscordPool.Internal.FreeList(list);
        }
        
        public void AppendList<T>(string name, List<T> items) where T : IDebugLoggable
        {
            if (items.Count == 0)
            {
                AppendField(name, "[]");
                return;
            }
            
            StartArray(name);
            for (int index = 0; index < items.Count; index++)
            {
                AppendObject(string.Empty, items[index]);
            }
            EndArray();
        }

        public void StartArray(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                AppendFieldPrefix(name);
                _logger.AppendLine();
            }
            AppendIndent();
            _logger.AppendLine("[");
            IncrementIndent();
        }

        public void EndArray()
        {
            DecrementIndent();
            AppendIndent();
            _logger.Append("]");
            _logger.AppendLine();
        }
        
        public void StartObject(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                AppendFieldPrefix(name);
                _logger.AppendLine();
            }

            AppendIndent();
            _logger.AppendLine("{");
            IncrementIndent();
        }

        public void EndObject()
        {
            DecrementIndent();
            AppendIndent();
            _logger.Append("}");
            _logger.AppendLine();
        }

        public override string ToString() => DiscordPool.Internal.FreeStringBuilderToString(_logger);
    }
}