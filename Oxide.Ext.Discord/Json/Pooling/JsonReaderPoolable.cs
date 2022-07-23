using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Json.Pooling
{
    public class JsonReaderPoolable : BasePoolable
    {
        private readonly MemoryStream Stream;
        private readonly StreamReader Reader;

        public JsonReaderPoolable()
        {
            Stream = new MemoryStream();
            Reader = new StreamReader(Stream, DiscordEncoding.Encoding);
        }

        public Task CopyAsync(Stream stream)
        {
            Stream.SetLength(0);
            return stream.CopyToPooledAsync(Stream);
        }
        
        public Task<string> ReadAsStringAsync()
        {
            Stream.Position = 0;
            return Reader.ReadToEndAsync();
        }
        
        public Task<T> Deserialize<T>(BotClient client)
        {
            Stream.Position = 0;
            using (JsonTextReader reader = new JsonTextReader(Reader))
            {
                reader.CloseInput = false;
                return Task.FromResult(client.ClientSerializer.Deserialize<T>(reader));
            }
        }
        
        protected override void DisposeInternal()
        {
            Stream.SetLength(0);
            DiscordPool.Free(this);
        }
    }
}