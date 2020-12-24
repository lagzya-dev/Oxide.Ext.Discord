namespace Oxide.Ext.Discord.Entities.Activities
{
    public enum ActivityType
    {
        Game = 0,       // Playing {name}
        Streaming = 1,  // Streaming {name}
        Listening = 2,  // Listening {name}
        Watching = 3,   // Watching {name}
        Custom = 4,     //{emoji} {name} EX: ":smiley: I am cool"
        Competing = 5   // Competing in {name}
    }
}
