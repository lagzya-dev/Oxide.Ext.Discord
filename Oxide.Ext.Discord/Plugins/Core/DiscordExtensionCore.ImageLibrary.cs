using Oxide.Core.Plugins;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Plugins.Core
{
    internal partial class DiscordExtensionCore
    {
        [PluginReference]
        public Plugin ImageLibrary;
        
        public string GetPlayerAvatarUrl(string playerId) => IsLoaded(ImageLibrary) ? ImageLibrary.Call<string>("GetImageURL", playerId, 0ul) : string.Empty;

        public string GetImageUrl(string imageName, ulong imageId) => IsLoaded(ImageLibrary) ? ImageLibrary.Call<string>("GetImageURL", imageName, imageId) : string.Empty;
    }
}