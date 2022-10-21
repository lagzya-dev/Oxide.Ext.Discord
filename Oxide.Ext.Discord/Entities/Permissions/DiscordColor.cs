using System;
using System.Globalization;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Exceptions.Entities.Permissions;
using Oxide.Ext.Discord.Json.Converters;

namespace Oxide.Ext.Discord.Entities.Permissions
{
    /// <summary>
    /// Represents a Discord Color
    /// </summary>
    [JsonConverter(typeof(DiscordColorConverter))]
    public struct DiscordColor
    {
        /// <summary>
        /// Default Role Color
        /// </summary>
        public static readonly DiscordColor Default = new DiscordColor(0);
        
        /// <summary>
        /// Teal Role Color
        /// </summary>
        public static readonly DiscordColor Teal = new DiscordColor(0x1ABC9C);
        
        /// <summary>
        /// Dark Teal Role Color
        /// </summary>
        public static readonly DiscordColor DarkTeal = new DiscordColor(0x11806A);
        
        /// <summary>
        /// Green Role Color
        /// </summary>
        public static readonly DiscordColor Green = new DiscordColor(0x2ECC71);
        
        /// <summary>
        /// Dark Green Role Color
        /// </summary>
        public static readonly DiscordColor DarkGreen = new DiscordColor(0x1F8B4C);
        
        /// <summary>
        /// Blue Role Color
        /// </summary>
        public static readonly DiscordColor Blue = new DiscordColor(0x3498DB);
        
        /// <summary>
        /// Dark Blue Role Color
        /// </summary>
        public static readonly DiscordColor DarkBlue = new DiscordColor(0x206694);
        
        /// <summary>
        /// Purple Role Color
        /// </summary>
        public static readonly DiscordColor Purple = new DiscordColor(0x9B59B6);
        
        /// <summary>
        /// Dark Purple Role Color
        /// </summary>
        public static readonly DiscordColor DarkPurple = new DiscordColor(0x71368A);
        
        /// <summary>
        /// Magenta Role Color
        /// </summary>
        public static readonly DiscordColor Magenta = new DiscordColor(0xE91E63);
        
        /// <summary>
        /// Dark Magenta Role Color
        /// </summary>
        public static readonly DiscordColor DarkMagenta = new DiscordColor(0xAD1457);
        
        /// <summary>
        /// Gold Role Color
        /// </summary>
        public static readonly DiscordColor Gold = new DiscordColor(0xF1C40F);
        
        /// <summary>
        /// Light Orange Role Color
        /// </summary>
        public static readonly DiscordColor LightOrange = new DiscordColor(0xC27C0E);
        
        /// <summary>
        /// Orange Role Color
        /// </summary>
        public static readonly DiscordColor Orange = new DiscordColor(0xE67E22);
        
        /// <summary>
        /// Dark Orange Role Color
        /// </summary>
        public static readonly DiscordColor DarkOrange = new DiscordColor(0xA84300);
        
        /// <summary>
        /// Red Role Color
        /// </summary>
        public static readonly DiscordColor Red = new DiscordColor(0xE74C3C);
        
        /// <summary>
        /// Dark Red Role Color
        /// </summary>
        public static readonly DiscordColor DarkRed = new DiscordColor(0x992D22);
        
        /// <summary>
        /// Light Gray Role Color
        /// </summary>
        public static readonly DiscordColor LightGrey = new DiscordColor(0x979C9F);
        
        /// <summary>
        /// Lighter Gray Role Color
        /// </summary>
        public static readonly DiscordColor LighterGrey = new DiscordColor(0x95A5A6);
        
        /// <summary>
        /// Dark Gray Role Color
        /// </summary>
        public static readonly DiscordColor DarkGrey = new DiscordColor(0x607D8B);
        
        /// <summary>
        /// Darker Gray Role Color
        /// </summary>
        public static readonly DiscordColor DarkerGrey = new DiscordColor(0x546E7A);
        
        /// <summary>
        /// Discord Success Color
        /// </summary>
        public static readonly DiscordColor Success = new DiscordColor(0x57F287);
        
        /// <summary>
        /// Discord Warning Color
        /// </summary>
        public static readonly DiscordColor Warning = new DiscordColor(0xFEE75C);
        
        /// <summary>
        /// Discord Danger Color
        /// </summary>
        public static readonly DiscordColor Danger = new DiscordColor(0xED4245);
        
        /// <summary>
        /// Discord Old Blurple Color
        /// </summary>
        public static readonly DiscordColor BlurpleOld = new DiscordColor(0x7289DA);
        
        /// <summary>
        /// Discord Blurple Color
        /// </summary>
        public static readonly DiscordColor Blurple = new DiscordColor(0x5865F2);
        
        /// <summary>
        /// Discord Fuchsia Color
        /// </summary>
        public static readonly DiscordColor Fuchsia = new DiscordColor(0xEB459E);

        /// <summary>
        /// uint value of the hex color code
        /// </summary>
        public readonly uint Color;

        /// <summary>
        /// DiscordColor Constructor
        /// </summary>
        /// <param name="color">uint value of hex color code</param>
        public DiscordColor(uint color)
        {
            InvalidDiscordColorException.ThrowIfInvalidColor(color);

            Color = color;
        }

        /// <summary>
        /// DiscordColor Constructor
        /// </summary>
        /// <param name="color">string hex color code</param>
        /// <exception cref="Exception">Throw if color is greater than #FFFFFF</exception>
        public DiscordColor(string color) : this(uint.Parse(color.TrimStart('#'), NumberStyles.AllowHexSpecifier))
        {

        }

        /// <summary>
        /// DiscordColor Constructor
        /// </summary>
        /// <param name="red">Red color (0-255)</param>
        /// <param name="green">Green color (0-255)</param>
        /// <param name="blue">Blue color (0-255)</param>
        public DiscordColor(byte red, byte green, byte blue)
        {
            Color = (uint)((red << 16) + (green << 8) + blue);
        }
        
        /// <summary>
        /// DiscordColor Constructor
        /// </summary>
        /// <param name="red">Red color (0-255)</param>
        /// <param name="green">Green color (0-255)</param>
        /// <param name="blue">Blue color (0-255)</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if any of the colors are &lt; 0 or &gt; 255</exception>
        public DiscordColor(int red, int green, int blue)
        {
            InvalidDiscordColorException.ThrowIfOutOfColorRange(nameof(red), red);
            InvalidDiscordColorException.ThrowIfOutOfColorRange(nameof(green), green);
            InvalidDiscordColorException.ThrowIfOutOfColorRange(nameof(blue), blue);

            Color = (uint)((red << 16) + (green << 8) + blue);
        }
        
        /// <summary>
        /// DiscordColor Constructor
        /// </summary>
        /// <param name="red">Red color (0-255)</param>
        /// <param name="green">Green color (0-255)</param>
        /// <param name="blue">Blue color (0-255)</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if any of the colors are &gt; 255</exception>
        public DiscordColor(uint red, uint green, uint blue)
        {
            InvalidDiscordColorException.ThrowIfOutOfColorRange(nameof(red), red);
            InvalidDiscordColorException.ThrowIfOutOfColorRange(nameof(green), green);
            InvalidDiscordColorException.ThrowIfOutOfColorRange(nameof(blue), blue);

            Color = (red << 16) + (green << 8) + blue;
        }

        /// <summary>
        /// DiscordColor Constructor
        /// </summary>
        /// <param name="red">Red color (0.0 - 1.0)</param>
        /// <param name="green">Green color (0.0 - 1.0)</param>
        /// <param name="blue">Blue color (0.0 - 1.0)</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if any of the colors are &lt; 0.0 or &gt; 1.0</exception>
        public DiscordColor(float red, float green, float blue)
        {
            InvalidDiscordColorException.ThrowIfOutOfColorRange(nameof(red), red);
            InvalidDiscordColorException.ThrowIfOutOfColorRange(nameof(green), green);
            InvalidDiscordColorException.ThrowIfOutOfColorRange(nameof(blue), blue);
            
            Color = ((uint)(red * 255) << 16) + ((uint)(green * 255)  << 8) + (uint)(blue * 255);
        }
        
        /// <summary>
        /// DiscordColor Constructor
        /// </summary>
        /// <param name="red">Red color (0.0 - 1.0)</param>
        /// <param name="green">Green color (0.0 - 1.0)</param>
        /// <param name="blue">Blue color (0.0 - 1.0)</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if any of the colors are &lt; 0.0 or &gt; 1.0</exception>
        public DiscordColor(double red, double green, double blue) : this((float)red, (float)green, (float)blue)
        {

        }
    }
}