using System.IO;
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

        private readonly StreamWriter _streamWriter;
        private readonly JsonTextWriter _writer;
        private StreamReader _reader;

        /// <summary>
        /// Constructor
        /// </summary>
        public DiscordJsonWriter()
        {
            Stream = new MemoryStream();
            _streamWriter = new StreamWriter(Stream, DiscordEncoding.Instance.Encoding, 2048, true);
            _writer = new JsonTextWriter(_streamWriter);
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
        public static void WriteAndCopy(JsonSerializer serializer, object payload, Stream output)
        {
            DiscordJsonWriter writer = Get();
            writer.Write(serializer, payload);
            writer.Stream.CopyToPooled(output);
            writer.Dispose();
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
            _streamWriter.Flush();
        }

        internal string ReadAsString()
        {
            //DiscordExtension.GlobalLogger.Debug($"{nameof(JsonWriterPoolable)}.{nameof(ReadAsStringAsync)} Read: {{0}} Position: {{1}}", Stream.Length, Stream.Position);
            if (_reader == null)
            {
                _reader = new StreamReader(Stream, DiscordEncoding.Instance.Encoding, false, 2048, true);
            }

            ResetStream();
            return _reader.ReadToEnd();
        }

        private void ResetStream()
        {
            Flush();
            Stream.Position = 0;
        }
        
        private void ClearStream()
        {
            Flush();
            Stream.SetLength(0);
        }

        private void Flush()
        {
            _writer.Flush();
            _streamWriter.Flush();
            _reader?.DiscardBufferedData();
        }
        
        ///<inheritdoc/>
        protected override void EnterPool()
        {
            ClearStream();
        }
    }
}