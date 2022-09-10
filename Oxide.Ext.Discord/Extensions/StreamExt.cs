using System.IO;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// Stream Extension Methods
    /// </summary>
    internal static class StreamExt
    {
        /// <summary>
        /// Copies one stream to another using a pooled byte[] buffer
        /// </summary>
        /// <param name="from">Stream to copy from</param>
        /// <param name="to">Stream to copy to</param>
        public static async Task CopyToPooledAsync(this Stream from, Stream to)
        {
            byte[] buffer = DiscordArrayPool<byte>.Shared.Rent(8196);

            while (true)
            {
                int bytesRead = await from.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                if (bytesRead == 0)
                {
                    break;
                }
                
                await to.WriteAsync(buffer, 0, bytesRead).ConfigureAwait(false);
            }
            
            DiscordArrayPool<byte>.Shared.Return(buffer);
        }

        /// <summary>
        /// Copies one stream to another using a pooled byte[] buffer
        /// </summary>
        /// <param name="from">Stream to copy from</param>
        /// <param name="to">Stream to copy to</param>
        public static void CopyToPooled(this Stream from, Stream to)
        {
            byte[] buffer = DiscordArrayPool<byte>.Shared.Rent(8196);
            
            while (true)
            {
                int bytesRead = from.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    break;
                }

                to.Write(buffer, 0, bytesRead);
            }
            
            DiscordArrayPool<byte>.Shared.Return(buffer);
        }
    }
}