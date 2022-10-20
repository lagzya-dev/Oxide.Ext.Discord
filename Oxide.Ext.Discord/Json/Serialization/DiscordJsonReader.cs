using System;
using System.IO;
using Newtonsoft.Json;
using Oxide.Core;
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
        /// Returns a pooled <see cref="DiscordJsonReader"/>
        /// </summary>
        /// <returns></returns>
        public static DiscordJsonReader Create()
        {
            return DiscordPool.Get<DiscordJsonReader>();
        }
        
        /// <summary>
        /// Returns a pooled <see cref="DiscordJsonReader"/> with stream loaded into it
        /// </summary>
        /// <param name="stream">Stream to load</param>
        /// <returns></returns>
        public static DiscordJsonReader CreateFromStream(Stream stream)
        {
            DiscordJsonReader reader = Create();
            reader.CopyFrom(stream);
            return reader;
        }
        
        /// <summary>
        /// Deserialize from stream to type {T}
        /// </summary>
        /// <param name="serializer">Serializer to use</param>
        /// <param name="stream">Stream to read from</param>
        /// <typeparam name="T">Type to return</typeparam>
        /// <returns></returns>
        public static T DeserializeFrom<T>(JsonSerializer serializer, Stream stream)
        {
            DiscordJsonReader reader = new DiscordJsonReader();
            reader.CopyFrom(stream);
            T result = reader.Deserialize<T>(serializer);
            reader.Dispose();
            return result;
        }

        /// <summary>
        /// Copy from the given stream to our internal stream
        /// </summary>
        /// <param name="stream">Stream to copy</param>
        public void CopyFrom(Stream stream)
        {
            ClearStream();
            //stream.Position = 0;
            //stream.CopyTo(Stream);
            stream.CopyToPooled(Stream, true);
        }
        
        /// <summary>
        /// Returns the Stream data as a string
        /// </summary>
        /// <returns>String of the stream data</returns>
        public string ReadAsString()
        {
            ResetStream();
            return _reader.ReadToEnd();
        }

        /// <summary>
        /// Deserializes the stream data to {T}
        /// </summary>
        /// <param name="serializer"><see cref="JsonSerializer"/> to use with the Deserialization</param>
        /// <typeparam name="T">Type to deserialize to</typeparam>
        /// <returns>{T}</returns>
        public T Deserialize<T>(JsonSerializer serializer)
        {
            ResetStream();
            try
            {
                using (JsonTextReader reader = new JsonTextReader(_reader))
                {
                    reader.CloseInput = false;
                    return serializer.Deserialize<T>(reader);
                }
            }
            catch (Exception ex)
            {
                Interface.Oxide.LogException($"Failed to Deserialize. Pos: {Stream.Position} Length: {Stream.Length}", ex);
                ResetStream();
                Interface.Oxide.LogDebug($"A:{ReadAsString()}");
                Interface.Oxide.LogDebug($"B:{DiscordEncoding.Encoding.GetString(Stream.ToArray(), 0, (int)Stream.Length)}");
                throw;
            }
        }

        private void ResetStream()
        {
            Stream.Position = 0;
            _reader.DiscardBufferedData();
        }

        private void ClearStream()
        {
            _reader.DiscardBufferedData();
            Stream.SetLength(0);
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            ClearStream();
        }
    }
}