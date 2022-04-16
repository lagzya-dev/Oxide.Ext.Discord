using System.Collections.Generic;
using System.IO;
using System.Text;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Rest.Multipart
{
    /// <summary>
    /// Handles converting Multipart requests to their respective byte[]
    /// </summary>
    public static class MultipartHandler
    {
        private static readonly byte[] NewLine = Encoding.UTF8.GetBytes("\r\n");
        private static readonly byte[] Separator = Encoding.UTF8.GetBytes("--");
        
        internal static byte[] GetMultipartFormData(string boundary, List<IMultipartSection> multipartSections)
        {
            StringBuilder sb = DiscordPool.GetStringBuilder();
            byte[] boundaryBytes = Encoding.UTF8.GetBytes(boundary);

            MemoryStream stream = DiscordPool.GetMemoryStream();

            foreach (IMultipartSection section in multipartSections)
            {
                AddMultipartSection(sb, section, stream, boundaryBytes);
            }

            stream.Write(NewLine, 0, NewLine.Length);
            stream.Write(Separator, 0, Separator.Length);
            stream.Write(boundaryBytes, 0, boundaryBytes.Length);
            stream.Write(Separator, 0, Separator.Length);
            stream.Write(NewLine, 0, NewLine.Length);

            DiscordPool.FreeStringBuilder(ref sb);
            byte[] bytes = stream.ToArray();
            DiscordPool.FreeMemoryStream(ref stream);
            return bytes;
        }

        private static void AddMultipartSection(StringBuilder sb, IMultipartSection section, MemoryStream stream, byte[] boundary)
        {
            sb.Length = 0;
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append(section.SectionName);
            sb.Append("\"");
            if (section.FileName != null)
            {
                sb.Append("; filename=\"");
                sb.Append(section.FileName);
                sb.Append("\"");
            }

            if (!string.IsNullOrEmpty(section.ContentType))
            {
                sb.AppendLine();
                sb.Append("Content-Type: ");
                sb.Append(section.ContentType);
            }

            sb.AppendLine();
            
            byte[] encoded = Encoding.UTF8.GetBytes(sb.ToString());
            stream.Write(NewLine, 0, NewLine.Length);
            stream.Write(Separator, 0, Separator.Length);
            stream.Write(NewLine, 0, NewLine.Length);
            stream.Write(encoded, 0, encoded.Length);
            stream.Write(NewLine, 0, NewLine.Length);
            stream.Write(section.Data, 0, section.Data.Length);
        }
    }
}