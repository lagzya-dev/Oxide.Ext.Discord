using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Multipart;

namespace Oxide.Ext.Discord.Rest.Requests.Data
{
    /// <summary>
    /// Represents a multipart request data type
    /// </summary>
    public class MultipartRequestData : BaseRequestData
    {
        private const string DefaultContentType = "multipart/form-data;boundary=\"{0}\"";

        /// <summary>
        /// Initializes the Request Data
        /// </summary>
        public void Init(DiscordClient client, IFileAttachments attachments)
        {
            Data = attachments;

            string boundary = Guid.NewGuid().ToString().Replace("-", "");
            ContentType = string.Format(DefaultContentType, boundary);
                
            List<IMultipartSection> multipartSections = DiscordPool.GetList<IMultipartSection>();

            StringContents = JsonConvert.SerializeObject(Data, client.Bot.ClientSerializerSettings);
            multipartSections.Add(MultipartFormSection.CreateFormSection("payload_json", StringContents, "application/json"));
            for (int index = 0; index < attachments.FileAttachments.Count; index++)
            {
                MessageFileAttachment fileAttachment = attachments.FileAttachments[index];
                multipartSections.Add(MultipartFileSection.CreateFileSection($"files[{(index + 1).ToString()}]", fileAttachment.FileName, fileAttachment.Data, fileAttachment.ContentType));
            }
            
            MultipartWriter writer = DiscordPool.Get<MultipartWriter>();
            for (int index = 0; index < multipartSections.Count; index++)
            {
                IMultipartSection section = multipartSections[index];
                writer.Write(section);
                section.Dispose();
            }
            writer.WriteEnding();

            Contents = writer.ToArray();
            DiscordPool.Free(ref writer);
            DiscordPool.FreeList(ref multipartSections);
        }
        
        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}