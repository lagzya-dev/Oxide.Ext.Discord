using System.IO;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// Stream Extension Methods
    /// </summary>
    public static class StreamExt
    {
        /// <summary>
        /// Copies one stream to another using a pooled byte[] buffer
        /// </summary>
        /// <param name="from">Stream to copy from</param>
        /// <param name="to">Stream to copy to</param>
        public static async Task CopyToPooledAsync(this Stream from, Stream to)
        {
            from.Position = 0;
            byte[] buffer = DiscordArrayPool<byte>.Shared.Rent(8196);

            int bytesRead;
            while ((bytesRead = await from.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) != 0)
            {
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
            from.Position = 0;
            byte[] buffer = DiscordArrayPool<byte>.Shared.Rent(8196);
            
            int bytesRead;
            while ((bytesRead = from.Read(buffer, 0, buffer.Length)) != 0)
            {
                to.Write(buffer, 0, bytesRead);
            }
            
            DiscordArrayPool<byte>.Shared.Return(buffer);
        }
    }
}