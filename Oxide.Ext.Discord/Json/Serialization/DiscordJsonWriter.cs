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

        /// <summary>
        /// Returns a pooled <see cref="DiscordJsonWriter"/>
        /// </summary>
        /// <returns></returns>
        public static DiscordJsonWriter Get()
        {
            return DiscordPool.Get<DiscordJsonWriter>();
        }

        /// <summary>
        /// Serializes the payload to the output stream
        /// </summary>
        /// <param name="serializer">Serializer to use</param>
        /// <param name="payload">Payload to serialize</param>
        /// <param name="output">Output stream to write to</param>
        public static async Task WriteAndCopyAsync(JsonSerializer serializer, object payload, Stream output)
        {
            DiscordJsonWriter writer = Get();
            await writer.WriteAsync(serializer, payload).ConfigureAwait(false);
            await writer.Stream.CopyToPooledAsync(output).ConfigureAwait(false);
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
        
        ///<inheritdoc/>
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