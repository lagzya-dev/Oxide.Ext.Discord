using System;
using System.IO;
using Oxide.Core;
using Oxide.Ext.Discord.Data.Ip;
using Oxide.Ext.Discord.Exceptions.Data;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Types;
using Oxide.Plugins;
using ProtoBuf;

namespace Oxide.Ext.Discord.Data
{
    internal sealed class DataHandler : Singleton<DataHandler>
    {
        internal static readonly string RootPath = Path.Combine(Interface.Oxide.DataDirectory, "DiscordExtension");
        private readonly Hash<Type, DataFileInfo> _fileInfo = new Hash<Type, DataFileInfo>
        {
            [typeof(DiscordUserData)] = new DataFileInfo("discord.users.data", 2),
            [typeof(DiscordIpData)] = new DataFileInfo("discord.ip.data", 1)
        };

        private DataHandler() { }

        public void Initialize()
        {
            Load<DiscordUserData>();
            Load<DiscordIpData>();
        }

        public void OnServerSave() => SaveAll(false);

        public void Shutdown() => SaveAll(true);

        private void SaveAll(bool force)
        {
            Save(DiscordUserData.Instance, force);
            Save(DiscordIpData.Instance, force);
        }
        
        public void Load<TData>() where TData : BaseDataFile<TData>, new()
        {
            Type type = typeof(TData);
            DataFileInfo info = _fileInfo[type];
            DataInfoNotFoundException.ThrowIfDataInfoNotFound(type, info);
            
            int index = 0;
            while (true)
            {
                try
                {
                    string path = info.GetPathForIndex(index);
                    if (!Directory.Exists(RootPath))
                    {
                        Directory.CreateDirectory(RootPath);
                    }
                    
                    if (!File.Exists(path))
                    {
                        break;
                    }
                    
                    using (FileStream file = File.OpenRead(path))
                    {
                        TData data = Serializer.Deserialize<TData>(file);
                        if (data != null)
                        {
                            BaseDataFile<TData>.Instance = data;
                            data.OnDataLoaded();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    DiscordExtension.GlobalLogger.Exception("An error occured loading the {0} Data File of type {1}", info.FilePath, type.FullName, ex);
                }
                finally
                {
                    index++;
                }
            }

            BaseDataFile<TData>.Instance = new TData();
        }
        
        public void Save<TData>(TData data, bool force) where TData : BaseDataFile<TData>, new()
        {
            if (!data.DataUpdated && !force)
            {
                return;
            }
            
            Type type = typeof(TData);
            DataFileInfo info = _fileInfo[type];
            DataInfoNotFoundException.ThrowIfDataInfoNotFound(type, info);

            try
            {
                int numBackups = info.NumBackups;
                string path = info.GetPathForIndex(numBackups);
                if (!Directory.Exists(RootPath))
                {
                    Directory.CreateDirectory(RootPath);
                }
                
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                for (int i = numBackups - 1; i >= 0; i--)
                {
                    path = info.GetPathForIndex(i);
                    if (File.Exists(path))
                    {
                        File.Move(path, info.GetPathForIndex(i + 1));
                    }
                }

                path = info.GetPathForIndex(0);
                FileMode saveMode = File.Exists(path) ? FileMode.Truncate : FileMode.Create;

                using (FileStream file = File.Open(path, saveMode))
                {
                    Serializer.Serialize(file, this);
                }
                    
                data.OnDataSaved();
            }
            catch (Exception ex)
            {
                DiscordExtension.GlobalLogger.Exception("An error occured saving the Data File", ex);
            }
        }
    }
}