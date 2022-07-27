using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Json.Serialization
{
    /// <summary>
    /// This is a pooled JSON writer that can write JSON to a stream
    /// </summary>
    public class DiscordJsonWriter : BasePoolable
    {
        /// <summary>
        /// Stream that is written to
        /// </summary>
        public readonly MemoryStream Stream;
        
        private readonly JsonTextWriter _writer;
        private StreamReader _reader;

        /// <summary>
        /// Constructor
        /// </summary>
        public DiscordJsonWriter()
        {
            Stream = new MemoryStream();
            StreamWriter sWriter = new StreamWriter(Stream, DiscordEncoding.Encoding, 1024, true);
            _writer = new JsonTextWriter(sWriter);
            _writer.Formatting = Formatting.None;
        }

        /// <summary>
        /// Serializes the payload to the Stream
        /// </summary>
        /// <param name="client">Client to serialize with</param>
        /// <param name="payload">Payload to be serialized</param>
        /// <returns></returns>
        public Task WriteAsync(BotClient client, object payload)
        {
            //DiscordExtension.GlobalLogger.Debug($"{nameof(JsonWriterPoolable)}.{nameof(WriteAsync)} Before: {{0}} Position: {{1}} Type: {{2}}", Stream.Length, Stream.Position, payload.GetType());
            client.JsonSerializer.Serialize(_writer, payload);
            _writer.Flush();
            //DiscordExtension.GlobalLogger.Debug($"{nameof(JsonWriterPoolable)}.{nameof(WriteAsync)} After: {{0}} Position: {{1}} Type: {{2}}", Stream.Length, Stream.Position, payload.GetType());
            return Task.CompletedTask;
        }

        internal Task<string> ReadAsStringAsync()
        {
            //DiscordExtension.GlobalLogger.Debug($"{nameof(JsonWriterPoolable)}.{nameof(ReadAsStringAsync)} Read: {{0}} Position: {{1}}", Stream.Length, Stream.Position);
            if (_reader == null)
            {
                _reader = new StreamReader(Stream, DiscordEncoding.Encoding, false, 1024, true);
            }
            
            Stream.Position = 0;
            return _reader.ReadToEndAsync();
        }
        
        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            Stream.SetLength(0);
            DiscordPool.Free(this);
        }
    }
}