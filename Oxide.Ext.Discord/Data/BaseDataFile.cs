using System;
using System.IO;
using Oxide.Core;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;
using ProtoBuf;

namespace Oxide.Ext.Discord.Data
{
    internal abstract class BaseDataFile<T> where T : BaseDataFile<T>, new()
    {
        internal static T Instance;
        
        private readonly string _dataPath;
        private bool DataUpdated { get; set; }
        private readonly Hash<int, string> _backupPaths = new Hash<int, string>();

        protected BaseDataFile()
        {
            _dataPath = Path.Combine(Interface.Oxide.DataDirectory, GetFileName());
        }

        public abstract string GetFileName();
        public abstract int GetNumBackups();

        public static void Load()
        {
            T data = new T();
            int index = 0;
            while (true)
            {
                try
                {
                    string path = GetPathForIndex(data._dataPath, index, data._backupPaths);
                    if (!File.Exists(path))
                    {
                        break;
                    }
                    
                    using (FileStream file = File.OpenRead(path))
                    {
                        T deserialize = Serializer.Deserialize<T>(file);
                        if (deserialize != null)
                        {
                            Instance = deserialize;
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    DiscordExtension.GlobalLogger.Exception("An error occured loading the {{0}} Data File of type {{1}}", data._dataPath, typeof(T), ex);
                }
                finally
                {
                    index++;
                }
            }

            Instance = data;
        }
        
        public void Save(bool force)
        {
            if (!DataUpdated && !force)
            {
                return;
            }

            try
            {
                int numBackups = GetNumBackups();
                string path = GetPathForIndex(numBackups);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                for (int i = numBackups - 1; i >= 0; i--)
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
                    Serializer.Serialize(file, this);
                }
                    
                DataUpdated = false;
            }
            catch (Exception ex)
            {
                DiscordExtension.GlobalLogger.Exception("An error occured saving the Data File", ex);
            }
        }

        private string GetPathForIndex(int index)
        {
            return GetPathForIndex(_dataPath, index, _backupPaths);
        }
        
        private static string GetPathForIndex(string path, int index, Hash<int, string> paths)
        {
            if (index == 0)
            {
                return path;
            }

            string backup = paths[index];
            if (backup != null)
            {
                return backup;
            }

            backup = $"{path}.{index.ToString()}";
            paths[index] = backup;

            return backup;
        }

        internal void OnDataChanged()
        {
            DataUpdated = true;
        }
    }
}