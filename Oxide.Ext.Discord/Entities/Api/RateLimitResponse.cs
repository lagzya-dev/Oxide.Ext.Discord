using System;
using System.Net;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Entities.Api
{
    /// <summary>
    /// Represents a rate limit response from an API request
    /// </summary>
    public class RateLimitResponse : BasePoolable
    {
        /// <summary>
        /// The Bucket ID of the rate limit
        /// </summary>
        public string BucketId;
        
        /// <summary>
        /// If we hit the global rate limit with this request
        /// </summary>
        public bool HitGlobalRateLimit;
        
        /// <summary>
        /// The date time when this bucket will reset
        /// </summary>
        public DateTimeOffset ResetAt;
        
        /// <summary>
        /// The number of request for this bucket
        /// </summary>
        public int BucketLimit;
        
        /// <summary>
        /// The number of remaining requests for this bucket
        /// </summary>
        public int BucketRemaining;
        
        /// <summary>
        /// Initialize the RateLimitResponse
        /// </summary>
        /// <param name="headers">Headers for the rate limit</param>
        /// <param name="logger">Logger</param>
        public void Init(WebHeaderCollection headers, ILogger logger)
        {
            // logger.Debug($"Headers: {RateLimitHeaders.IsGlobal}: {headers.GetBool(RateLimitHeaders.IsGlobal)} " +
            //              $"{RateLimitHeaders.RetryAfter}: {headers.GetInt(RateLimitHeaders.RetryAfter)} " +
            //              $"{RateLimitHeaders.BucketId}: {headers.Get(RateLimitHeaders.BucketId)} " +
            //              $"{RateLimitHeaders.BucketLimit}: {headers.GetInt(RateLimitHeaders.BucketLimit)} " +
            //              $"{RateLimitHeaders.BucketRemaining}: {headers.GetInt(RateLimitHeaders.BucketRemaining)} " +
            //              $"{RateLimitHeaders.BucketReset}: {headers.GetDouble(RateLimitHeaders.BucketReset)} {headers.GetDouble(RateLimitHeaders.BucketReset).ToDateTimeOffsetFromSeconds()}");
            HitGlobalRateLimit = headers.GetBool(RateLimitHeaders.IsGlobal);
            if (HitGlobalRateLimit)
            {
                HitGlobalRateLimit = true;
                ResetAt = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(headers.GetInt(RateLimitHeaders.RetryAfter));
                return;
            }

            BucketId = headers.Get(RateLimitHeaders.BucketId);
            if (!string.IsNullOrEmpty(BucketId))
            {
                BucketLimit = headers.GetInt(RateLimitHeaders.BucketLimit);
                BucketRemaining =  headers.GetInt(RateLimitHeaders.BucketRemaining);
                ResetAt = headers.GetDouble(RateLimitHeaders.BucketReset).ToDateTimeOffsetFromSeconds();
            }
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            BucketId = null;
            HitGlobalRateLimit = false;
            ResetAt = default(DateTimeOffset);
            BucketLimit = 0;
            BucketRemaining = 0;
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}