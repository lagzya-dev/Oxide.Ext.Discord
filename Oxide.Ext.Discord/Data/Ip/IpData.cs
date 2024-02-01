using System;
using Oxide.Ext.Discord.Services.IpApi;
using ProtoBuf;

namespace Oxide.Ext.Discord.Data.Ip
{
    [ProtoContract]
    internal class IpData
    {
        [ProtoMember(1)]
        public string CountryCode { get; set; }
        
        [ProtoMember(2)]
        public string CountryName { get; set; }
        
        [ProtoMember(3)]
        public DateTime CreatedDate { get; set; }

        public bool IsExpired => DateTime.UtcNow > CreatedDate + TimeSpan.FromDays(30);
        
        public IpData() { }

        public IpData(IpResult result)
        {
            CountryName = result.Country;
            CountryCode = result.CountryCode;
            CreatedDate = DateTime.UtcNow;
        }
    }
}