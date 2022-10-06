using System.Text;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Rest.Buckets
{
    /// <summary>
    /// Represents a type that splits string by a specific char
    /// </summary>
    public class BucketIdGenerator : BasePoolable
    {
        private RequestMethod _method;
        private string _string;
        private int _length;
        private int _lastIndex;
        private bool _isCompleted;
        private string _previous;
        private string _current;
        private int _index;
        
        private const char SplitChar = '/';
        private const char QueryStringChar = '?';
        private const string IdReplacement = "id";
        private const string ReactionsRoute =  "reactions";

        /// <summary>
        /// Constructor for string splitter
        /// </summary>
        /// <param name="method">HTTP request method</param>
        /// <param name="route">String to be split</param>
        private void Init(RequestMethod method, string route)
        {
            _method = method;
            _string = route;
            _length = _string.LastIndexOf(QueryStringChar);
            if (_length == -1)
            {
                _length = _string.Length;
            }
        }
        
        /// <summary>
        /// Returns the Rate Limit Bucket for the given route
        /// https://discord.com/developers/docs/topics/rate-limits#rate-limits
        /// </summary>
        /// <param name="method">Request method for the request</param>
        /// <param name="route">API Route</param>
        /// <returns>Bucket ID for route</returns>
        public static string GetBucketId(RequestMethod method, string route)
        {
            BucketIdGenerator gen = DiscordPool.Get<BucketIdGenerator>();
            gen.Init(method, route);
            string bucketId = gen.GetBucketId();
            gen.Dispose();
            return bucketId;
        }

        /// <summary>
        /// Creates the bucket ID
        /// </summary>
        /// <returns></returns>
        public string GetBucketId()
        {
            MoveNext();
            
            StringBuilder bucket = DiscordPool.GetStringBuilder();
            bucket.Append(EnumCache<RequestMethod>.Instance.ToString(_method));
            bucket.Append(':');
            bucket.Append(_current);

            while (!_isCompleted)
            {
                MoveNext();
                bucket.Append("/");
                bucket.Append(_current);
                _index++;
            }

            return DiscordPool.FreeStringBuilderToString(bucket);
        }

        /// <summary>
        /// Returns the next string in the split
        /// </summary>
        /// <returns></returns>
        public void MoveNext()
        {
            while (!_isCompleted)
            {
                int nextIndex = _string.IndexOf(SplitChar, _lastIndex);
                if (nextIndex == -1 || nextIndex >= _length)
                {
                    nextIndex = _length;
                }

                int length = nextIndex - _lastIndex;
                //If the length is > 0 update the string else we move to the next string
                if (length > 0)
                {
                    UpdateCurrent(length);
                    NextIndex(nextIndex);
                    break;
                }
                
                NextIndex(nextIndex);
            }
        }

        /// <summary>
        /// Updates the _lastIndex processed.
        /// If the _lastIndex >= _length then we are done processing the Bucket ID
        /// If the current route is now reactions we stop processing the bucket ID
        /// </summary>
        /// <param name="lastIndex"></param>
        private void NextIndex(int lastIndex)
        {
            _lastIndex = lastIndex + 1;
            if (_lastIndex >= _length)
            {
                _isCompleted = true;
            }
            
            //All Reactions belong to the same bucket
            if (_current == ReactionsRoute)
            {
                _isCompleted = true;
            }
        }

        //Updates the current route segment
        private void UpdateCurrent(int length)
        {
            _previous = _current;
            _current = _string.Substring(_lastIndex, length);
            
            //If previous is not a major ID we don't want to include the ID in the bucket ID so use "id" string instead
            if (!IsMajorId() && char.IsNumber(_current[0]) && ulong.TryParse(_current, out ulong _))
            {
                _current = IdReplacement;
            }
        }
        
        //Returns if the previous route segment was guild, channel, or webhook.
        //The Snowflake ID's for these routes should be included in the bucket ID
        private bool IsMajorId()
        {
            //We should only use Major ID if the previous segment name is the first segment and the ID is the second.
            if (_index == 1)
            {
                switch (_previous)
                {
                    case "guilds":
                    case "channels":
                    case "webhooks":
                        return true;
                }
            }
            
            return false;
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            _method = default(RequestMethod);
            _string = null;
            _length = 0;
            _lastIndex = 0;
            _isCompleted = false;
            _previous = null;
            _current = null;
            _index = 0;
        }
    }
}