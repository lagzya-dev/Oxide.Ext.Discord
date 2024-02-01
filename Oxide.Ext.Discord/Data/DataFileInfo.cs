using System.IO;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Data
{
    internal class DataFileInfo
    {
        public readonly string FilePath;
        public readonly int NumBackups;
        
        private readonly Hash<int, string> _backupPaths = new Hash<int, string>();

        public DataFileInfo(string fileName, int numBackups)
        {
            FilePath = Path.Combine(DataHandler.RootPath, fileName);
            NumBackups = numBackups;
        }

        public string GetPathForIndex(int index)
        {
            if (index == 0)
            {
                return FilePath;
            }

            string backup = _backupPaths[index];
            if (backup != null)
            {
                return backup;
            }

            backup = $"{FilePath}.{index.ToString()}";
            _backupPaths[index] = backup;

            return backup;
        }
    }
}