using System.Net;
using System;

namespace Oxide.Ext.Discord.REST
{
    public class RestError
    {
        public int HttpStatusCode { get; set; }
        public RequestMethod RequestMethod { get; } 
        public WebException Exception { get; }
        public Uri Uri { get; }
        public object Data { get; }
        public DiscordApiError DiscordApiError { get; set; }
        public string Message { get; set; }
        
        public RestError(WebException exception, Uri uri, RequestMethod requestMethod, object data)
        {
            Exception = exception;
            Uri = uri;
            RequestMethod = requestMethod;
            Data = data;
        }
    }
}