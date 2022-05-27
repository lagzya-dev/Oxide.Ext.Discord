using System;
using System.Net;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Entities.Api
{
    public class RateLimitResponse : BasePoolable
    {
        public string BucketId;
        public bool HitGlobalRateLimit;
        public DateTimeOffset ResetAt;
        public int BucketLimit;
        public int BucketRemaining;

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

        protected override void EnterPool()
        {
            BucketId = null;
            HitGlobalRateLimit = false;
            ResetAt = default(DateTimeOffset);
            BucketLimit = 0;
            BucketRemaining = 0;
        }

        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}