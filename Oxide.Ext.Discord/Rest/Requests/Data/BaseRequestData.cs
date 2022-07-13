using System.IO;
using System.Net;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Rest.Requests.Data
{
    /// <summary>
    /// Represents request data passed into an API request
    /// </summary>
    public abstract class BaseRequestData : BasePoolable
    {
        /// <summary>
        /// Data contents serialized to JSON
        /// </summary>
        public string StringContents;
        
        /// <summary>
        /// Data serialized to bytes 
        /// </summary>
        public byte[] Contents;
        
        /// <summary>
        /// The content type of the data
        /// </summary>
        public string ContentType;
        
        /// <summary>
        /// Data to be serialized into the request
        /// </summary>
        protected object Data;
        
        /// <summary>
        /// Creates request data for the given request
        /// </summary>
        /// <param name="request">Request to create data for</param>
        /// <returns><see cref="JsonRequestData"/></returns>
        public static BaseRequestData CreateRequestData(BaseRequest request)
        {
            if (request.Data is IFileAttachments attachments && attachments.FileAttachments != null && attachments.FileAttachments.Count != 0)
            {
                MultipartRequestData multipart = DiscordPool.Get<MultipartRequestData>();
                multipart.Init(request.Client, attachments);
                return multipart;
            }
            
            JsonRequestData data = DiscordPool.Get<JsonRequestData>();
            data.Init(request.Client, request.Data);
            return data;
        }
        
        /// <summary>
        /// Writes the request data to the web request
        /// </summary>
        /// <param name="request">Request to be written to</param>
        public void WriteRequestData(WebRequest request)
        {
            if (Contents == null || Contents.Length == 0)
            {
                return;
            }

            request.ContentLength = Contents.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(Contents, 0, Contents.Length);
            }
        }
        
        ///<inheritdoc/>
        protected override void EnterPool()
        {
            StringContents = null;
            Contents = null;
            ContentType = null;
            Data = null;
        }
    }
}