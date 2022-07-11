using System;
using System.Collections.Generic;
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
        private readonly string _logPath;
        private string _logFileName;
        private DateTime _logNameDate;

        internal DiscordFileLogger()
        {
            _writerThread = new Thread(WriteLogThread)
            {
                IsBackground = true
            };
            _writerThread.Start();
            
            _logPath = Path.Combine(Interface.Oxide.LogDirectory, "DiscordExtension");
            if (!Directory.Exists(_logPath))
            {
                Directory.CreateDirectory(_logPath);
            }
            
            SetLogFileName();
        }
        
        private void SetLogFileName()
        {
            if (_logNameDate != DateTime.Now.Date)
            {
                _logFileName = Path.Combine(_logPath, $"DiscordExtension-{DateTime.Now:yyyy-MM-dd}.txt");
                _logNameDate = DateTime.Now.Date;
            }
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
                    _sb.Append(ex.ToString());
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
                _sb.Length = 0;
            }

            WriteToFile(log);
        }
        
        private void WriteToFile(string text)
        {
            SetLogFileName();
            using (StreamWriter streamWriter = new StreamWriter(_logFileName, true))
            {
                streamWriter.WriteLine(text);
            }
        }

        internal void OnServerShutdown()
        {
            _writerThread.Abort();
            WriteLog();
        }
    }
}