using System.Collections.Generic;
using System.Text;
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
            StringBuilder sb = new StringBuilder();
            byte[] boundaryBytes = Encoding.UTF8.GetBytes(boundary);

            List<byte> data = new List<byte>();

            foreach (IMultipartSection section in multipartSections)
            {
                AddMultipartSection(sb, section, data, boundaryBytes);
            }

            data.AddRange(NewLine);
            data.AddRange(Separator);
            data.AddRange(boundaryBytes);
            data.AddRange(Separator);
            data.AddRange(NewLine);
            
            return data.ToArray();
        }

        private static void AddMultipartSection(StringBuilder sb, IMultipartSection section, List<byte> data, byte[] boundary)
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
            
            data.AddRange(NewLine);
            data.AddRange(Separator);
            data.AddRange(boundary);
            data.AddRange(NewLine);
            data.AddRange(Encoding.UTF8.GetBytes(sb.ToString()));
            data.AddRange(NewLine);
            data.AddRange(section.Data);
        }
    }
}