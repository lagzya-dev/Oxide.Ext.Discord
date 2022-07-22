using System;
using System.IO;
using Oxide.Core;
using Oxide.Ext.Discord.Logging;
using ProtoBuf;

namespace Oxide.Ext.Discord.Data
{
    internal static class DataHandler
    {
        internal static ExtData Data { get; private set; }
        private const int NumBackups = 1;
        private static readonly string DataPath = Path.Combine(Interface.Oxide.DataDirectory, "discord.users.data");
        private static bool DataUpdated { get; set; }

        public static void Load()
        {
            for (int i = 0; i <= NumBackups; i++)
            {
                try
                {
                    string path = GetPathForIndex(i);
                    if (File.Exists(path))
                    {
                        using (FileStream file = File.OpenRead(path))
                        {
                            Data = Serializer.Deserialize<ExtData>(file);
                            if (Data != null)
                            {
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    DiscordExtension.GlobalLogger.Exception("An error occured loading the {{0}} Data File", DataPath, ex);
                }
            }

            if (Data == null)
            {
                Data = new ExtData();
                Save(true);
            }
        }

        public static void Save(bool force)
        {
            if (!DataUpdated && !force)
            {
                return;
            }

            try
            {
                string path = GetPathForIndex(NumBackups);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                for (int i = NumBackups - 1; i >= 0; i--)
                {
                    path = GetPathForIndex(i);
                    if (File.Exists(path))
                    {
                        File.Move(path, GetPathForIndex(i + 1));
                    }
                }

                path = GetPathForIndex(0);
                FileMode saveMode = File.Exists(path) ? FileMode.Truncate : FileMode.Create;

                using (FileStream file = File.Open(path, saveMode))
                {
                    Serializer.Serialize(file, Data);
                }
                    
                DataUpdated = false;
            }
            catch (Exception ex)
            {
                DiscordExtension.GlobalLogger.Exception("An error occured saving the Data File", ex);
            }
        }

        private static string GetPathForIndex(int index)
        {
            if (index == 0)
            {
                return DataPath;
            }

            return $"{DataPath}.{index.ToString()}";
        }

        internal static void OnDataChanged()
        {
            DataUpdated = true;
        }
    }
}