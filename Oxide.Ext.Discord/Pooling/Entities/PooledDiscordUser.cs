using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Pooling.Entities
{
    internal class PooledDiscordUser : BasePoolable, IDiscordUser
    {
        public Snowflake Id { get; set; }
        public string Username { get; set; }
        public string Discriminator { get; set; }
        public string Avatar { get; set; }
        public bool? Bot { get; set; }
        public bool? System { get; set; }
        public bool? MfaEnabled { get; set; }
        public string Banner { get; set; }
        public DiscordColor? AccentColor { get; set; }
        public string Locale { get; set; }
        public bool? Verified { get; set; }
        public string Email { get; set; }
        public UserFlags? Flags { get; set; }
        public UserPremiumType? PremiumType { get; set; }
        public UserFlags? PublicFlags { get; set; }

        protected override void EnterPool()
        {
            Id = default(Snowflake);
            Username = null;
            Discriminator = null;
            Avatar = null;
            Bot = null;
            System = null;
            MfaEnabled = null;
            Banner = null;
            AccentColor = null;
            Locale = null;
            Verified = null;
            Email = null;
            Flags = null;
            PremiumType = null;
            PublicFlags = null;
        }
    }
}