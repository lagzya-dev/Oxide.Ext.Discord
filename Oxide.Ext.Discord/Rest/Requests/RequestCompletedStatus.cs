namespace Oxide.Ext.Discord.Rest.Requests
{
    /// <summary>
    /// Represents the completed status for the request
    /// </summary>
    public enum RequestCompletedStatus
    {
        /// <summary>
        /// The request completed successfully
        /// </summary>
        Success,
        
        /// <summary>
        /// The request encountered a fatal error
        /// </summary>
        ErrorFatal,
        
        /// <summary>
        /// The error attempt multiple times to complete the request and was unsuccessful
        /// </summary>
        ErrorRetry,
        
        /// <summary>
        /// The request was cancelled. The <see cref="DiscordClient"/> was disconnected while making the request
        /// </summary>
        Cancelled
    }
}