using System;
using System.Globalization;
using System.IO;
using System.Text;
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
        private readonly StringBuilder _sb = new StringBuilder();
        private readonly Thread _writerThread;
        private readonly object _syncRoot = new object();
        private readonly string _logFileName;

        internal DiscordFileLogger()
        {
            _writerThread = new Thread(WriteLogThread)
            {
                IsBackground = true,
                Name = nameof(DiscordFileLogger)
            };
            _writerThread.Start();
            
            string logPath = Path.Combine(Interface.Oxide.LogDirectory, "DiscordExtension");
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            
            _logFileName = Path.Combine(logPath, $"DiscordExtension-{DateTime.Now:yyyy-MM-dd h-mm-ss-tt}.txt");
        }

        internal void AddMessage(DiscordLogLevel level, string message, Exception ex)
        {
            lock (_syncRoot)
            {
                _sb.Append(DateTime.Now.ToString(CultureInfo.CurrentCulture));
                _sb.Append(" [");
                _sb.Append(EnumCache<DiscordLogLevel>.ToString(level));
                _sb.Append("] ");
                _sb.Append(message);
                if (ex != null)
                {
                    _sb.AppendLine();
                    _sb.Append(ex);
                }
                _sb.AppendLine();
            }
        }

        private void WriteLogThread()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        WriteLog();
                    }
                    finally
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (ThreadAbortException)
            {
                
            }
        }

        private void WriteLog()
        {
            string log;
            lock (_syncRoot)
            {
                if (_sb.Length == 0)
                {
                    return;
                }

                log = _sb.ToString();
                _sb.Clear();
            }

            using (StreamWriter streamWriter = new StreamWriter(_logFileName, true))
            {
                streamWriter.WriteLine(log);
            }
        }

        internal void OnServerShutdown()
        {
            _writerThread.Abort();
            WriteLog();
        }
    }
}