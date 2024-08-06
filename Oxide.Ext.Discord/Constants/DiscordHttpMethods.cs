using System;
using System.Net.Http;
using Oxide.Core.Libraries;

namespace Oxide.Ext.Discord.Constants;

internal static class DiscordHttpMethods
{
    private static readonly HttpMethod Patch = new("PATCH");

    public static HttpMethod GetMethod(RequestMethod method)
    {
        switch (method)
        {
            case RequestMethod.DELETE:
                return HttpMethod.Delete;
            case RequestMethod.GET:
                return HttpMethod.Get;
            case RequestMethod.PATCH:
                return Patch;
            case RequestMethod.POST:
                return HttpMethod.Post;
            case RequestMethod.PUT:
                return HttpMethod.Put;
            default:
                throw new ArgumentOutOfRangeException(nameof(method), method, null);
        }
    }
}