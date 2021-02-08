using System.Net;
using System;

namespace Oxide.Ext.Discord.REST
{
    public class RestError
    {
        public int HttpStatusCode { get; set; }
        public RequestMethod RequestMethod { get; } 
        public WebException Exception { get; }
        public Uri Url { get; }
        public object Data { get; }
        public DiscordApiError DiscordError { get; set; }
        public string Message { get; set; }
        
        public RestError(WebException exception, Uri url, RequestMethod requestMethod, object data)
        {
            Exception = exception;
            Url = url;
            RequestMethod = requestMethod;
            Data = data;
        }
    }
}