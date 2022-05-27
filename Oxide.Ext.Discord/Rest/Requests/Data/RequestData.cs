using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Rest.Requests.Data
{
    public class RequestData : BasePoolable
    {
        /// <summary>
        /// Data serialized to bytes 
        /// </summary>
        public byte[] Contents;
        
        public string ContentType;
        
        protected object Data;

        protected DiscordClient Client;

        private const string DefaultContentType = "application/json";

        private void Init(DiscordClient client, object data)
        {
            Client = client;
            Data = data;
            ContentType = DefaultContentType;
            if (Data != null)
            {
                Contents = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Data, Client.Bot.ClientSerializerSettings));
            }
        }

        public static RequestData CreateRequestData(BaseRequest request)
        {
            if (request.Data is IFileAttachments attachments && attachments.FileAttachments != null && attachments.FileAttachments.Count != 0)
            {
                MultipartRequestData multipart = DiscordPool.Get<MultipartRequestData>();
                multipart.Init(request.Client, attachments);
                return multipart;
            }
            
            RequestData data = DiscordPool.Get<RequestData>();
            data.Init(request.Client, request.Data);
            return data;
        }

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
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }

        protected override void EnterPool()
        {
            Contents = null;
            ContentType = null;
            Data = null;
            Client = null;
        }
    }
}