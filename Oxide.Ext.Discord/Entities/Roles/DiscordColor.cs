using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Roles
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
        /// uint value of the hex color code
        /// </summary>
        public uint Color { get; }

        /// <summary>
        /// DiscordColor Constructor
        /// </summary>
        /// <param name="color">uint value of hex color code</param>
        public DiscordColor(uint color)
        {
            if (color > 0xFFFFFF)
            {
                throw new Exception($"Color '{color}' is outside the valid color range");
            }
            
            Color = color;
        }

        /// <summary>
        /// DiscordColor Constructor
        /// </summary>
        /// <param name="color">string hex color code</param>
        /// <exception cref="Exception">Throw if color is greater than #FFFFFF</exception>
        public DiscordColor(string color)
        {
            uint parsedColor = uint.Parse(color.TrimStart('#'), NumberStyles.AllowHexSpecifier);
            if (parsedColor > 0xFFFFFF)
            {
                throw new Exception($"Color '{color}' is outside the valid color range");
            }

            Color = parsedColor;
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
            if (red < 0 || red > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(red), "Value must be between 0 - 255");
            }
            
            if (green < 0 || green > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(green), "Value must be between 0 - 255");
            }
            
            if (blue < 0 || blue > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(blue), "Value must be between 0 - 255");
            }

            Color = (uint)(red * 65536 + green * 256 + blue);
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
            if (red < 0f || red > 1f)
            {
                throw new ArgumentOutOfRangeException(nameof(red), "Value must be between 0 - 1");
            }
            
            if (green < 0f || green > 1f)
            {
                throw new ArgumentOutOfRangeException(nameof(green), "Value must be between 0 - 1");
            }
            
            if (blue < 0f || blue > 1f)
            {
                throw new ArgumentOutOfRangeException(nameof(blue), "Value must be between 0 - 1");
            }
            
            Color = (uint)(red * 255 * 65536 + green * 255 * 256 + blue * 255);
        }
    }

    /// <summary>
    /// Handles the JSON Serialization / Deserialization for DiscordColor
    /// </summary>
    public class DiscordColorConverter : JsonConverter
    {
        /// <summary>
        /// Writes to JSON
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DiscordColor color = (DiscordColor) value;
            writer.WriteValue(color.Color);
        }

        /// <summary>
        /// Reads from JSON
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new DiscordColor(uint.Parse(reader.Value.ToString()));
        }

        /// <summary>
        /// Check if can convert
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DiscordColor);
        }
    }
}