using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Services.IpApi;
using Oxide.Plugins;
using ProtoBuf;

namespace Oxide.Ext.Discord.Data.Ip
{
    [ProtoContract]
    internal class DiscordIpData : BaseDataFile<DiscordIpData>
    {
        [ProtoMember(1)] 
        private readonly Hash<string, IpData> Ips = new Hash<string, IpData>();

        public bool HasData(string ip) => Ips.ContainsKey(ip);
        
        public void AddData(string ip, IpResult result)
        {
            Ips[ip] = new IpData(result);
            OnDataChanged();
        }

        public string GetCountryName(string ip) => Ips.TryGetValue(ip, out IpData data) ? data.CountryName : "Unknown";
        public string GetCountryCode(string ip) => Ips.TryGetValue(ip, out IpData data) ? data.CountryCode : string.Empty;
        
        internal override void OnDataLoaded()
        {
            int count = Ips.Count;
            Ips.RemoveAll(ip => ip.IsExpired);
            if (count != Ips.Count)
            {
                OnDataChanged();
            }
        }
    }
}