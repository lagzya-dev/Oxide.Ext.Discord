using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Roles
{
    [JsonConverter(typeof(DiscordColorConverter))]
    public struct DiscordColor
    {
        public static readonly DiscordColor Default = new DiscordColor(0);
        public static readonly DiscordColor Teal = new DiscordColor(0x1ABC9C);
        public static readonly DiscordColor DarkTeal = new DiscordColor(0x11806A);
        public static readonly DiscordColor Green = new DiscordColor(0x2ECC71);
        public static readonly DiscordColor DarkGreen = new DiscordColor(0x1F8B4C);
        public static readonly DiscordColor Blue = new DiscordColor(0x3498DB);
        public static readonly DiscordColor DarkBlue = new DiscordColor(0x206694);
        public static readonly DiscordColor Purple = new DiscordColor(0x9B59B6);
        public static readonly DiscordColor DarkPurple = new DiscordColor(0x71368A);
        public static readonly DiscordColor Magenta = new DiscordColor(0xE91E63);
        public static readonly DiscordColor DarkMagenta = new DiscordColor(0xAD1457);
        public static readonly DiscordColor Gold = new DiscordColor(0xF1C40F);
        public static readonly DiscordColor LightOrange = new DiscordColor(0xC27C0E);
        public static readonly DiscordColor Orange = new DiscordColor(0xE67E22);
        public static readonly DiscordColor DarkOrange = new DiscordColor(0xA84300);
        public static readonly DiscordColor Red = new DiscordColor(0xE74C3C);
        public static readonly DiscordColor DarkRed = new DiscordColor(0x992D22);
        public static readonly DiscordColor LightGrey = new DiscordColor(0x979C9F);
        public static readonly DiscordColor LighterGrey = new DiscordColor(0x95A5A6);
        public static readonly DiscordColor DarkGrey = new DiscordColor(0x607D8B);
        public static readonly DiscordColor DarkerGrey = new DiscordColor(0x546E7A);
        
        public uint Color { get; }

        public DiscordColor(uint color)
        {
            Color = color;
        }

        public DiscordColor(string color)
        {
            uint parsedColor = uint.Parse(color.TrimStart('#'), NumberStyles.AllowHexSpecifier);
            if (parsedColor > 0xFFFFFF)
            {
                throw new Exception($"Color '{color}' is outside the valid color range");
            }

            Color = parsedColor;
        }

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

    public class DiscordColorConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DiscordColor color = (DiscordColor) value;
            writer.WriteValue(color.Color);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new DiscordColor(uint.Parse(reader.Value.ToString()));
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DiscordColor);
        }
    }
}