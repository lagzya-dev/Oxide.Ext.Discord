using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Extensions;

namespace Oxide.Ext.Discord.Net
{
    public class DiscordStreamContent : HttpContent
    {
        private readonly Stream _content;

        public DiscordStreamContent(Stream content)
        {
            _content = content ?? throw new ArgumentNullException(nameof(content));
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            _content.Position = 0;
            return _content.CopyToPooledAsync(stream);
        }

        protected override bool TryComputeLength(out long length)
        {
            if (_content.CanSeek)
            {
                length = _content.Length;
                return true;
            }
            length = 0;
            return false;
        }

        protected override Task<Stream> CreateContentReadStreamAsync()
        {
            return Task.FromResult(_content);
        }
    }
}