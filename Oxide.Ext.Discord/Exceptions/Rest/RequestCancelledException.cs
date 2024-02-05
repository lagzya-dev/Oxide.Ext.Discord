namespace Oxide.Ext.Discord.Exceptions
{
    public class RequestCancelledException : BaseDiscordException
    {
        public RequestCancelledException() : base("The request was cancelled")
        {
            
        }
    }
}