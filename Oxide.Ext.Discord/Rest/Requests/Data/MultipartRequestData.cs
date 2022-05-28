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
    public class MultipartRequestData : RequestData
    {
        private const string DefaultContentType = "multipart/form-data;boundary=\"{0}\"";

        /// <summary>
        /// Initializes the Request Data
        /// </summary>
        public void InitMultipart(DiscordClient client, IFileAttachments attachments)
        {
            Client = client;
            Data = attachments;

            string boundary = Guid.NewGuid().ToString().Replace("-", "");
            ContentType = string.Format(DefaultContentType, boundary);
                
            List<IMultipartSection> multipartSections = DiscordPool.GetList<IMultipartSection>();
            multipartSections.Add(MultipartFormSection.CreateFormSection("payload_json", JsonConvert.SerializeObject(Data, Client.Bot.ClientSerializerSettings), "application/json"));
            for (int index = 0; index < attachments.FileAttachments.Count; index++)
            {
                MessageFileAttachment fileAttachment = attachments.FileAttachments[index];
                multipartSections.Add(MultipartFileSection.CreateFileSection($"files[{(index + 1).ToString()}]", fileAttachment.FileName, fileAttachment.Data, fileAttachment.ContentType));
            }
            Contents = MultipartHandler.GetMultipartFormData(boundary, multipartSections);
            for (int index = 0; index < multipartSections.Count; index++)
            {
                IMultipartSection section = multipartSections[index];
                section.Dispose();
            }
            DiscordPool.FreeList(ref multipartSections);
        }
        
        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}