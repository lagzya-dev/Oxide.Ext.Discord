using System.Collections.Generic;
using Oxide.Ext.Discord.Singleton;

namespace Oxide.Ext.Discord.Cache
{
    /// <summary>
    /// Caches file names when sending attachments
    /// </summary>
    public sealed class FileAttachmentCache : Singleton<FileAttachmentCache>
    {
        private readonly List<string> _cache = new List<string>();

        private FileAttachmentCache() { }
        
        public string GetName(int index)
        {
            if (index >= _cache.Count)
            {
                for (int i = _cache.Count; i <= index; i++)
                {
                    _cache[i] = $"files[{(index + 1).ToString()}]";
                }
            }

            return _cache[index];
        }
    }
}