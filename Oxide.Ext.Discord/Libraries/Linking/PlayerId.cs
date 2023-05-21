using System;
using Oxide.Core.Libraries.Covalence;
using Oxide.Ext.Discord.Cache;

namespace Oxide.Ext.Discord.Libraries.Linking
{
    public struct PlayerId : IEquatable<PlayerId>
    {
        public readonly string Id;
        public bool IsValid => !string.IsNullOrEmpty(Id);
        public IPlayer Player => ServerPlayerCache.Instance.GetPlayer(Id);
        
        public PlayerId(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
        
        public PlayerId(IPlayer player)
        {
            Id = player?.Id ?? throw new ArgumentNullException(nameof(player));
        }

        public bool Equals(PlayerId other) => Id == other.Id;

        public override bool Equals(object obj) => obj is PlayerId other && Equals(other);

        public override int GetHashCode() => Id != null ? Id.GetHashCode() : 0;
    }
}