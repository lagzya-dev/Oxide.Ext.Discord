using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Extensions;
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

        public static DiscordJsonWriter Get()
        {
            return DiscordPool.Get<DiscordJsonWriter>();
        }

        public static async Task WriteAndCopyAsync(JsonSerializer serializer, object payload, Stream to)
        {
            DiscordJsonWriter writer = Get();
            await writer.WriteAsync(serializer, payload).ConfigureAwait(false);
            await writer.Stream.CopyToPooledAsync(to).ConfigureAwait(false);
            writer.Dispose();
        }

        /// <summary>
        /// Serializes the payload to the Stream
        /// </summary>
        /// <param name="serializer"><see cref="JsonSerializer"/> to serialize with</param>
        /// <param name="payload">Payload to be serialized</param>
        /// <returns></returns>
        public Task WriteAsync(JsonSerializer serializer, object payload)
        {
            ClearStream();
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
            ClearStream();
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

            ResetStream();
            return _reader.ReadToEndAsync();
        }

        private void ResetStream()
        {
            _writer.Flush();
            _reader?.DiscardBufferedData();
            Stream.Position = 0;
        }
        
        private void ClearStream()
        {
            _writer.Flush();
            _reader?.DiscardBufferedData();
            Stream.SetLength(0);
        }
        
        protected override void EnterPool()
        {
            ClearStream();
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}