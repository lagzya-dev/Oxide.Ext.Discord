using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Rest.Multipart
{
    /// <summary>
    /// Represents a MultiPartFormSection
    /// </summary>
    internal class MultipartFormSection : BasePoolable, IMultipartSection
    {
        /// <summary>
        /// Name of the file being sent
        /// </summary>
        public string FileName => null;
        
        /// <summary>
        /// Content Type for the file being sent
        /// </summary>
        public string ContentType { get; private set; }
        
        /// <summary>
        /// Data for the file being sent
        /// </summary>
        public string Data { get; private set;  }
        
        /// <summary>
        /// Section name for the multipart section
        /// </summary>
        public string SectionName { get; private set;  }

        public static MultipartFormSection CreateFormSection(string sectionName, string data, string contentType)
        {
            MultipartFormSection section = DiscordPool.Get<MultipartFormSection>();
            section.Init(sectionName, data, contentType);
            return section;
        }
        
        /// <summary>
        /// Constructor for byte form data
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        private void Init(string sectionName, string data, string contentType)
        {
            ContentType = contentType;
            Data = data;
            SectionName = sectionName;
        }
        
        public void WriteData(MultipartWriter writer)
        {
            writer.Write(Data);
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