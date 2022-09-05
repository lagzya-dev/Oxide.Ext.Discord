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
            StreamWriter sWriter = new StreamWriter(Stream, DiscordEncoding.Encoding, 2048, true);
            _writer = new JsonTextWriter(sWriter);
            _writer.Formatting = Formatting.None;
        }

        /// <summary>
        /// Serializes the payload to the Stream
        /// </summary>
        /// <param name="serializer"><see cref="JsonSerializer"/> to serialize with</param>
        /// <param name="payload">Payload to be serialized</param>
        /// <returns></returns>
        public Task WriteAsync(JsonSerializer serializer, object payload)
        {
            Stream.SetLength(0);
            serializer.Serialize(_writer, payload);
            _writer.Flush();
            return Task.CompletedTask;
        }
        
        /// <summary>
        /// Writes the payload to the Stream
        /// </summary>
        /// <param name="serializer"><see cref="JsonSerializer"/> to serialize with</param>
        /// <param name="payload">Payload to be serialized</param>
        public void Write(JsonSerializer serializer, object payload)
        {
            Stream.SetLength(0);
            serializer.Serialize(_writer, payload);
            _writer.Flush();
        }

        internal Task<string> ReadAsStringAsync()
        {
            //DiscordExtension.GlobalLogger.Debug($"{nameof(JsonWriterPoolable)}.{nameof(ReadAsStringAsync)} Read: {{0}} Position: {{1}}", Stream.Length, Stream.Position);
            if (_reader == null)
            {
                _reader = new StreamReader(Stream, DiscordEncoding.Encoding, false, 2048, true);
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