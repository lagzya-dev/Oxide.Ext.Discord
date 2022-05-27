using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Rest.Multipart
{
    /// <summary>
    /// Represents a MultiPartFileSection
    /// </summary>
    internal class MultipartFileSection : BasePoolable, IMultipartSection
    {
        /// <summary>
        /// Name of the file being sent
        /// </summary>
        public string FileName { get; private set; }
        
        /// <summary>
        /// Content Type for the file being sent
        /// </summary>
        public string ContentType { get; private set; }
        
        /// <summary>
        /// Data for the file being sent
        /// </summary>
        public byte[] Data { get; private set; }
        
        /// <summary>
        /// Section name for the multipart section
        /// </summary>
        public string SectionName { get; private set; }

        /// <summary>
        /// Constructor for a multipart file
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        private void Init(string sectionName, string fileName, byte[] data, string contentType)
        {
            FileName = fileName;
            ContentType = contentType;
            Data = data;
            SectionName = sectionName;
        }

        public static MultipartFileSection CreateFileSection(string sectionName, string fileName, byte[] data, string contentType)
        {
            MultipartFileSection section = DiscordPool.Get<MultipartFileSection>();
            section.Init(sectionName, fileName, data, contentType);
            return section;
        }
        
        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
        
        protected override void EnterPool()
        {
            base.EnterPool();
            ContentType = null;
            Data = null;
            SectionName = null;
        }
    }
}