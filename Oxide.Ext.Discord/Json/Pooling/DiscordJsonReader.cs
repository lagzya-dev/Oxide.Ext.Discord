using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Json.Pooling
{
    public class DiscordJsonReader : BasePoolable
    {
        internal readonly MemoryStream Stream;
        private readonly StreamReader Reader;

        public DiscordJsonReader()
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
        
        public Task<T> DeserializeAsync<T>(BotClient client)
        {
            Stream.Position = 0;
            using (JsonTextReader reader = new JsonTextReader(Reader))
            {
                reader.CloseInput = false;
                return Task.FromResult(client.ClientSerializer.Deserialize<T>(reader));
            }
        }

        public Task PopulateAsync(BotClient client, object obj)
        {
            Stream.Position = 0;
            using (JsonTextReader reader = new JsonTextReader(Reader))
            {
                reader.CloseInput = false;
                client.ClientSerializer.Populate(reader, obj);
                return Task.CompletedTask;
            }
        }
        
        protected override void DisposeInternal()
        {
            Stream.SetLength(0);
            DiscordPool.Free(this);
        }
    }
}