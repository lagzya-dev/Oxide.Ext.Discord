using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Json.Pooling
{
    public class DiscordJsonWriter : BasePoolable
    {
        public readonly MemoryStream Stream;
        
        private readonly JsonTextWriter Writer;
        private readonly StreamReader Reader;

        public DiscordJsonWriter()
        {
            Stream = new MemoryStream();
            StreamWriter sWriter = new StreamWriter(Stream, DiscordEncoding.Encoding, 1024, true);
            Writer = new JsonTextWriter(sWriter);
            Reader = new StreamReader(Stream, DiscordEncoding.Encoding, false, 1024, true);
        }

        public Task WriteAsync(BotClient client, object payload)
        {
            //DiscordExtension.GlobalLogger.Debug($"{nameof(JsonWriterPoolable)}.{nameof(WriteAsync)} Before: {{0}} Position: {{1}} Type: {{2}}", Stream.Length, Stream.Position, payload.GetType());
            client.ClientSerializer.Serialize(Writer, payload);
            Writer.Flush();
            //DiscordExtension.GlobalLogger.Debug($"{nameof(JsonWriterPoolable)}.{nameof(WriteAsync)} After: {{0}} Position: {{1}} Type: {{2}}", Stream.Length, Stream.Position, payload.GetType());
            return Task.CompletedTask;
        }

        public Task<string> ReadAsStringAsync()
        {
            //DiscordExtension.GlobalLogger.Debug($"{nameof(JsonWriterPoolable)}.{nameof(ReadAsStringAsync)} Read: {{0}} Position: {{1}}", Stream.Length, Stream.Position);
            Stream.Position = 0;
            return Reader.ReadToEndAsync();
        }
        
        protected override void DisposeInternal()
        {
            Stream.SetLength(0);
            DiscordPool.Free(this);
        }
    }
}