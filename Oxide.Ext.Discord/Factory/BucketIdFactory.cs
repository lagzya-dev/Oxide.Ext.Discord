using System.Text;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Buckets;
using Oxide.Ext.Discord.Singleton;

namespace Oxide.Ext.Discord.Factory
{
    internal class BucketIdFactory : Singleton<BucketIdFactory>
    {
        private const char SplitChar = '/';
        private const char QueryStringChar = '?';
        private const string IdReplacement = "id";
        private const string ReactionsRoute =  "reactions";

        /// <summary>
        /// Returns the Rate Limit Bucket for the given route
        /// https://discord.com/developers/docs/topics/rate-limits#rate-limits
        /// </summary>
        /// <param name="method">Request method for the request</param>
        /// <param name="route">API Route</param>
        /// <returns>Bucket ID for route</returns>
        internal BucketId GenerateId(RequestMethod method, string route)
        {
            BucketGeneratorState state = DiscordPool.Internal.Get<BucketGeneratorState>();
            state.Init(route);
            string id = GenerateId(method, state);
            state.Dispose();
            return new BucketId(id);
        }
        
        /// <summary>
        /// Creates the bucket ID
        /// </summary>
        /// <returns></returns>
        private static string GenerateId(RequestMethod method, BucketGeneratorState state)
        {
            StringBuilder bucket = DiscordPool.Internal.GetStringBuilder();
            bucket.Append(EnumCache<RequestMethod>.Instance.ToString(method));
            bucket.Append(':');
            bucket.Append(state.Current);

            while (!state.IsCompleted)
            {
                state.MoveNext();
                bucket.Append("/");
                bucket.Append(state.Current);
            }

            return DiscordPool.Internal.FreeStringBuilderToString(bucket);
        }

        private class BucketGeneratorState : BasePoolable
        {
            public string Current;
            public bool IsCompleted;
            
            private string _string;
            private int _length;
            private int _lastIndex;
            private string _previous;
            private int _index;

            /// <summary>
            /// Constructor for string splitter
            /// </summary>
            /// <param name="route">String to be split</param>
            public void Init(string route)
            {
                _string = route;
                _length = _string.LastIndexOf(QueryStringChar);
                if (_length == -1)
                {
                    _length = _string.Length;
                }
                
                MoveNext();
            }

            /// <summary>
            /// Returns the next string in the split
            /// </summary>
            /// <returns></returns>
            public void MoveNext()
            {
                while (!IsCompleted)
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

                _index++;
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
                    IsCompleted = true;
                }

                //All Reactions belong to the same bucket
                if (Current == ReactionsRoute)
                {
                    IsCompleted = true;
                }
            }

            //Updates the current route segment
            private void UpdateCurrent(int length)
            {
                _previous = Current;
                
                //If previous is not a major ID we don't want to include the ID in the bucket ID so use "id" string instead
                if (!IsMajorId() && char.IsNumber(_string[_lastIndex]) && ulong.TryParse(Current, out ulong _))
                {
                    Current = IdReplacement;
                }
                else
                {
                    Current = _string.Substring(_lastIndex, length);
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
                _string = null;
                _length = 0;
                _lastIndex = 0;
                IsCompleted = false;
                _previous = null;
                Current = null;
                _index = 0;
            }
        }
    }
}