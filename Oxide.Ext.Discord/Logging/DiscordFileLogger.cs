using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using Oxide.Core;
using Oxide.Ext.Discord.Cache;

namespace Oxide.Ext.Discord.Logging
{
    /// <summary>
    /// Represents a File Logger for Discord
    /// </summary>
    internal class DiscordFileLogger
    {
        private readonly ConcurrentQueue<string> _messages = new ConcurrentQueue<string>();
        private readonly string _logFileName;
        private readonly string _fileLogFormat;
        private readonly AutoResetEvent _reset;

        internal DiscordFileLogger(string pluginName, string fileLogFormat, AutoResetEvent reset)
        {
            _fileLogFormat = fileLogFormat;
            _reset = reset;
            string logPath = Path.Combine(Interface.Oxide.LogDirectory, pluginName);
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            
            _logFileName = Path.Combine(logPath, $"{pluginName}-{DateTime.Now:yyyy-MM-dd_h-mm-ss-tt}.txt");
        }

        internal void AddMessage(DiscordLogLevel level, string message, Exception ex)
        {
            _messages.Enqueue(string.Format(_fileLogFormat, DateTime.Now, EnumCache<DiscordLogLevel>.Instance.ToString(level), message));
            if (ex != null)
            {
                _messages.Enqueue(ex.ToString());
            }
            _reset.Set();
        }
        
        internal void WriteLog()
        {
            if (_messages.IsEmpty)
            {
                return;
            }
            
            using (StreamWriter fileWriter = File.AppendText(_logFileName))
            {
                while (_messages.TryDequeue(out string message))
                {
                    fileWriter.WriteLine(message);
                }
            }
        }

        internal void OnShutdown()
        {
            WriteLog();
            
        }
    }
}