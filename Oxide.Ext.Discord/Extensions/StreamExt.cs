using System.IO;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Extensions
{
    public static class StreamExt
    {
        public static async Task CopyToPooledAsync(this Stream stream, Stream to)
        {
            byte[] buffer = DiscordArrayPool<byte>.Shared.Rent(8196);
            
            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                if (bytesRead == 0)
                {
                    break;
                }
                
                await to.WriteAsync(buffer, 0, bytesRead).ConfigureAwait(false);
            }
        }
    }
}