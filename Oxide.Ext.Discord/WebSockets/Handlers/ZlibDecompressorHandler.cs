using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.WebSockets.Handlers
{
    internal class ZlibDecompressorHandler
    {
        //private readonly MemoryStream _input;
        //private readonly DeflateStream _zlib;
        //private readonly MemoryStream _output;
        //private readonly StreamReader _outputStream;
        private readonly Encoding _encoding;
        private readonly ILogger _logger;

        private static readonly byte[] ZlibSuffix = {0x00, 0x00, 0xFF, 0xFF};
        private const byte ZlibPrefix = 0x78;

        public ZlibDecompressorHandler(Encoding encoding, ILogger logger)
        {
            //_input = new MemoryStream();
            //_zlib = new DeflateStream(_input, CompressionMode.Decompress);
            //_output = new MemoryStream();
            //_outputStream = new StreamReader(_output, encoding);
            _encoding = encoding;
            _logger = logger;
        }

        public async Task<string> DecompressMessage(ArraySegment<byte> bytes, CancellationToken token)
        {
            try
            {
                byte[] array = bytes.Array ?? throw new ArgumentNullException(nameof(bytes));
                if (bytes.Count < 4)
                {
                    _logger.Warning("Tried to decompress a message with less than 4 bytes. Count: {0}", bytes.Count);
                    return string.Empty;
                }

                using (MemoryStream input = new MemoryStream())
                {
                    if (array[0] == ZlibPrefix)
                    {
                        await input.WriteAsync(array, bytes.Offset + 2, bytes.Count - 2, token);
                    }
                    else
                    {
                        await input.WriteAsync(array, bytes.Offset, bytes.Count, token);
                    }

                    await input.FlushAsync(token);
                    input.Position = 0;

                    using (DeflateStream zlib = new DeflateStream(input, CompressionMode.Decompress, true))
                    {
                        using (MemoryStream output = new MemoryStream())
                        {
                            await zlib.CopyToAsync(output);
                            output.Position = 0;

                            using (StreamReader reader = new StreamReader(output, _encoding))
                            {
                                string message = await reader.ReadToEndAsync();

                                _logger.Debug($"Processed Message: String Length: {message.Length} Output Length: {output.Length}");

                                return message;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception("An error occured decompression zlib stream", ex);
                return string.Empty;
            }
        }

        private bool IsValidZlibStream(ArraySegment<byte> bytes)
        {
            byte[] array = bytes.Array ?? throw new InvalidOperationException();
            for (int i = 0; i < 4; i++)
            {
                if (array[array.Length - 1 - i] != ZlibSuffix[ZlibSuffix.Length - 1 - i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}