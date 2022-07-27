using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Json.Serialization
{
    /// <summary>
    /// This is a pooled JSON reader that can read as string, deserialize object, or populate a given object async
    /// </summary>
    public class DiscordJsonReader : BasePoolable
    {
        internal readonly MemoryStream Stream;
        private readonly StreamReader _reader;

        /// <summary>
        /// Constructor
        /// </summary>
        public DiscordJsonReader()
        {
            Stream = new MemoryStream();
            _reader = new StreamReader(Stream, DiscordEncoding.Encoding);
        }

        /// <summary>
        /// Copy from the given stream to our internal stream
        /// </summary>
        /// <param name="stream">Stream to copy</param>
        /// <returns></returns>
        public Task CopyAsync(Stream stream)
        {
            Stream.SetLength(0);
            return stream.CopyToPooledAsync(Stream);
        }
        
        /// <summary>
        /// Returns the Stream data as a string
        /// </summary>
        /// <returns>String of the stream data</returns>
        public Task<string> ReadAsStringAsync()
        {
            Stream.Position = 0;
            return _reader.ReadToEndAsync();
        }
        
        /// <summary>
        /// Deserializes the stream data to {T}
        /// </summary>
        /// <param name="client">Client to use with the Deserialization</param>
        /// <typeparam name="T">Type to deserialize to</typeparam>
        /// <returns>{T}</returns>
        public Task<T> DeserializeAsync<T>(BotClient client)
        {
            Stream.Position = 0;
            using (JsonTextReader reader = new JsonTextReader(_reader))
            {
                reader.CloseInput = false;
                return Task.FromResult(client.JsonSerializer.Deserialize<T>(reader));
            }
        }

        /// <summary>
        /// Populates the given object from the Stream
        /// </summary>
        /// <param name="client">Client to use with the Population</param>
        /// <param name="obj">Object to populate</param>
        /// <returns></returns>
        public Task PopulateAsync(BotClient client, object obj)
        {
            Stream.Position = 0;
            using (JsonTextReader reader = new JsonTextReader(_reader))
            {
                reader.CloseInput = false;
                client.JsonSerializer.Populate(reader, obj);
                return Task.CompletedTask;
            }
        }
        
        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            Stream.SetLength(0);
            DiscordPool.Free(this);
        }
    }
}