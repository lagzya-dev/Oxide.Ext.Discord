using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Oxide.Core;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Extensions;

namespace Oxide.Ext.Discord.Logging
{
    /// <summary>
    /// Represents a File Logger for Discord
    /// </summary>
    internal class DiscordFileLogger
    {
        private readonly MemoryStream _stream = new MemoryStream();
        private StreamWriter _writer;
        private readonly object _sync = new object();
        private readonly string _logFileName;
        private readonly string _fileLogFormat;
        
        private static readonly Thread WriterThread;
        private static readonly List<DiscordFileLogger> Loggers = new List<DiscordFileLogger>();

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
            _writer = new StreamWriter(_stream);
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
            lock (_sync)
            {
                _writer.Write(_fileLogFormat, DateTime.Now, EnumCache<DiscordLogLevel>.Instance.ToString(level), message);
                
                if (ex != null)
                {
                    _writer.WriteLine();
                    _writer.Write(ex.ToString());
                }
                _writer.WriteLine();
            }
        }

        private static void WriteLogThread()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        for (int index = 0; index < Loggers.Count; index++)
                        {
                            DiscordFileLogger logger = Loggers[index];
                            logger.WriteLog();
                        }
                    }
                    finally
                    {
                        Thread.Sleep(1000);
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
            lock (_sync)
            {
                _writer.Flush();
                if (_stream.Position == 0)
                {
                    return;
                }

                using (StreamWriter fileWriter = File.AppendText(_logFileName))
                {
                    _stream.CopyToPooled(fileWriter.BaseStream);
                }
                
                _stream.SetLength(0);
            }
        }

        internal static void OnServerShutdown()
        {
            WriterThread.Abort();
        }

        internal void OnShutdown()
        {
            WriteLog();
            Loggers.Remove(this);
            lock (_sync)
            {
                _writer.Dispose();
                _writer = null;
            }
        }
    }
}