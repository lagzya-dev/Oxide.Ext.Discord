using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Oxide.Core;
using Oxide.Ext.Discord.Pooling;
namespace Oxide.Ext.Discord.Logging
{
    /// <summary>
    /// Represents a File Logger for Discord
    /// </summary>
    internal class DiscordFileLogger
    {
        private readonly List<FileMessage> _messageQueue = new List<FileMessage>();
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
            FileMessage file = DiscordPool.Get<FileMessage>();
            file.Init(level, message, ex);
            lock (_syncRoot)
            {
                _messageQueue.Add(file);
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
            lock (_syncRoot)
            {
                if (_messageQueue.Count == 0)
                {
                    return;
                }
            }

            StringBuilder sb = DiscordPool.GetStringBuilder();

            lock (_syncRoot)
            {
                object[] args = ArrayPool.Get(3);
                for (int index = 0; index < _messageQueue.Count; index++)
                {
                    FileMessage message = _messageQueue[index];
                    args[0] = message.Date.ToString(CultureInfo.CurrentCulture);
                    args[1] = message.LogLevel.ToString();
                    args[2] = message.GetMessage();
                    sb.AppendFormat("{0} [{1}] {2}", args);
                    sb.AppendLine();
                    DiscordPool.Free(ref message);
                }
                
                _messageQueue.Clear();
                
                ArrayPool.Free(args);
            }

            WriteToFile(sb.ToString());
            DiscordPool.FreeStringBuilder(ref sb);
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

        private class FileMessage : BasePoolable
        {
            public DiscordLogLevel LogLevel;
            public string Message;
            public DateTime Date;
            public Exception Exception;

            public void Init(DiscordLogLevel level, string message, Exception ex)
            {
                LogLevel = level;
                Message = message;
                Exception = ex;
                Date = DateTime.Now;
            }
            
            protected override void EnterPool()
            {
                LogLevel = default(DiscordLogLevel);
                Message = null;
                Date = default(DateTime);
                Exception = null;
            }

            public string GetMessage()
            {
                return Exception == null ? Message : $"{Message}\n{Exception}";
            }
        }
    }
}