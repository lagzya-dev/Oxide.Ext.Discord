using System;
using System.Net.Http.Headers;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Buckets;

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
        public BucketId BucketId;
        
        /// <summary>
        /// If we hit the global rate limit with this request
        /// </summary>
        public bool IsGlobalRateLimit;
        
        /// <summary>
        /// The date time when this bucket will reset
        /// </summary>
        public DateTimeOffset ResetAt;
        
        /// <summary>
        /// The number of request for this bucket
        /// </summary>
        public int Limit;
        
        /// <summary>
        /// The number of remaining requests for this bucket
        /// </summary>
        public int Remaining;

        /// <summary>
        /// The scope the rate limit is for
        /// </summary>
        public string Scope;

        /// <summary>
        /// Initialize the RateLimitResponse
        /// </summary>
        /// <param name="headers">Headers for the rate limit</param>
        public void Init(HttpResponseHeaders headers)
        {
            IsGlobalRateLimit = headers.GetBool(RateLimitHeaders.IsGlobal);
            Scope = headers.Get(RateLimitHeaders.Scope);
            if (IsGlobalRateLimit)
            {
                IsGlobalRateLimit = true;
                ResetAt = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(GetBucketReset(headers));
                return;
            }

            BucketId = headers.GetBucketId(RateLimitHeaders.BucketId);
            if (!BucketId.IsValid)
            {
                return;
            }
            
            Limit = headers.GetInt(RateLimitHeaders.BucketLimit);
            Remaining =  headers.GetInt(RateLimitHeaders.BucketRemaining);
            ResetAt = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(GetBucketReset(headers));
        }

        private double GetBucketReset(HttpResponseHeaders headers)
        {
            return Math.Max(headers.GetDouble(RateLimitHeaders.BucketResetAfter), headers.GetDouble(RateLimitHeaders.RetryAfter));
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            BucketId = default(BucketId);
            IsGlobalRateLimit = false;
            ResetAt = default(DateTimeOffset);
            Limit = 0;
            Remaining = 0;
            Scope = null;
        }
    }
}