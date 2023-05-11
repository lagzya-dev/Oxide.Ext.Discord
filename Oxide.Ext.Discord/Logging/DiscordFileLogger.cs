using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

        private static readonly Thread WriterThread;
        private static readonly List<DiscordFileLogger> Loggers = new List<DiscordFileLogger>();
        private static readonly AutoResetEvent Reset = new AutoResetEvent(false);
        private static bool _exit;

        static DiscordFileLogger()
        {
            WriterThread = new Thread(WriteLogThread)
            {
                IsBackground = true,
                Name = nameof(DiscordFileLogger)
            };
            WriterThread.Start();
        }
        
        internal DiscordFileLogger(string pluginName, string fileLogFormat)
        {
            _fileLogFormat = fileLogFormat;
            string logPath = Path.Combine(Interface.Oxide.LogDirectory, pluginName);
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            
            _logFileName = Path.Combine(logPath, $"{pluginName}-{DateTime.Now:yyyy-MM-dd_h-mm-ss-tt}.txt");
            Loggers.Add(this);
        }

        internal void AddMessage(DiscordLogLevel level, string message, Exception ex)
        {
            _messages.Enqueue(string.Format(_fileLogFormat, DateTime.Now, EnumCache<DiscordLogLevel>.Instance.ToString(level), message));
            if (ex != null)
            {
                _messages.Enqueue(ex.ToString());
            }
        }

        private static void WriteLogThread()
        {
            try
            {
                while (!_exit)
                {
                    Reset.WaitOne();
                    for (int index = 0; index < Loggers.Count; index++)
                    {
                        DiscordFileLogger logger = Loggers[index];
                        logger.WriteLog();
                    }
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                DiscordExtension.GlobalLogger.Exception("An exception occured writing log file.", ex);
                WriteLogThread();
            }
        }

        private void WriteLog()
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

        internal static void OnServerShutdown()
        {
            _exit = true;
            Reset.Set();
            WriterThread.Join();
        }

        internal void OnShutdown()
        {
            WriteLog();
            Loggers.Remove(this);
        }
    }
}