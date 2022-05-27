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
                //_logger.Debug($"BucketHandler.OnRequestQueued Thread: {(_thread == null ? "NULL" : "NOT NULL")} Is Alive: {_thread?.IsAlive ?? false} Name: {_bucket.BucketId}");
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
            //_logger.Debug($"Running Thread For Bucket: {_bucket.BucketId}");
            try
            {
                //_logger.Debug($"{_bucket.BucketId} Count: {_bucket.RequestCount()}");
                while (_bucket.RequestCount() > 0)
                {
                    //_logger.Debug($"{_bucket.BucketId} Fire Request. Total: {_bucket.RequestCount()}");
                    FireRequest();
                    //_logger.Debug($"{_bucket.BucketId} Completed Request");
                }
                //_logger.Debug($"Sleeping For: {(_bucket.ResetAt - DateTimeOffset.UtcNow).TotalSeconds} Seconds: {_bucket.BucketId}");
                //ThreadExt.SleepUntil(_bucket.ResetAt);
            }
            // catch (ThreadInterruptedException)
            // {
            //     _logger.Debug($"BucketHandler.OnRequestQueued RunThread Thread Interrupted: {_bucket.BucketId}");
            //     RunThread();
            // }
            catch (ThreadAbortException)
            {
                _logger.Debug("Bucket thread has been aborted for: {0}", _bucket.BucketId);
            }
            catch (Exception ex)
            {
                _logger.Exception("An exception occured for bucket {0}", _bucket.BucketId, ex);
            }
            
            //_logger.Debug($"BucketHandler.OnRequestQueued RunThread Completed Name: {_bucket.BucketId} {_thread?.ManagedThreadId}");
            
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