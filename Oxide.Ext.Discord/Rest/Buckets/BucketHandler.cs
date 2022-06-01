using System;
using System.Threading;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Rest.Buckets
{
    /// <summary>
    /// Handles the thread for the bucket
    /// </summary>
    public class BucketHandler
    {
        private readonly Bucket _bucket;
        private Thread _thread;
        private readonly ThreadStart _threadStart;
        private readonly object _lock = new object();
        private readonly ILogger _logger;
        
        /// <summary>
        /// Creates a new bucket handler for a given bucket
        /// </summary>
        /// <param name="bucket">Bucket the handler is for</param>
        /// <param name="logger">Logger</param>
        public BucketHandler(Bucket bucket, ILogger logger)
        {
            _bucket = bucket;
            _logger = logger;
            _threadStart = RunThread;
        }

        internal void OnRequestQueued()
        {
            lock (_lock)
            {
                if (_thread == null || !_thread.IsAlive)
                {
                    _thread = new Thread(_threadStart);
                    _thread.IsBackground = true;
                    _thread.Start();
                }
            }
        }

        private void RunThread()
        {
            try
            {
                while (_bucket.RequestCount() > 0)
                {
                    FireRequest();
                }
            }
            catch (ThreadAbortException)
            {
                _logger.Debug("Bucket thread has been aborted for: {0}", _bucket.BucketId);
            }
            catch (Exception ex)
            {
                _logger.Exception("An exception occured for bucket {0}", _bucket.BucketId, ex);
            }
            
            lock (_lock)
            {
                _bucket.OnBucketCompleted();
                _thread = null;
            }
        }
        
        private void FireRequest()
        {
            RequestHandler request = _bucket.GetRequest(0);
            request.Run();
        }

        internal void Shutdown()
        {
            _thread?.Abort();
        }
    }
}