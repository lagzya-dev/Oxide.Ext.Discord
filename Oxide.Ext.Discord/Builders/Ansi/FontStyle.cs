using System;

namespace Oxide.Ext.Discord.Builders.Ansi
{
    [Flags]
    public enum FontStyle : byte
    {
        Default = 0,
        Bold = 1 << 1,
        Underline = 1 << 2
    }
}