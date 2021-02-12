using System.Collections.Generic;
using System.Text;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// String Extension methods
    /// </summary>
    public static class StringExt
    {
        /// <summary>
        /// Parses the specified command into uMod command format
        /// Sourced from RustCore.cs of OxideMod (https://github.com/theumod/uMod.Rust/blob/oxide/src/RustCore.cs)
        /// </summary>
        /// <param name="argStr"></param>
        /// <param name="command"></param>
        /// <param name="args"></param>
        public static void ParseCommand(this string argStr, out string command, out string[] args)
        {
            List<string> argList = new List<string>();
            StringBuilder sb = new StringBuilder();
            bool inLongArg = false;

            foreach (char c in argStr)
            {
                if (c == '"')
                {
                    if (inLongArg)
                    {
                        string arg = sb.ToString().Trim();
                        if (!string.IsNullOrEmpty(arg))
                            argList.Add(arg);
                        sb = new StringBuilder();
                        inLongArg = false;
                    }
                    else
                        inLongArg = true;
                }
                else if (char.IsWhiteSpace(c) && !inLongArg)
                {
                    string arg = sb.ToString().Trim();
                    if (!string.IsNullOrEmpty(arg))
                        argList.Add(arg);
                    sb = new StringBuilder();
                }
                else
                    sb.Append(c);
            }

            if (sb.Length > 0)
            {
                string arg = sb.ToString().Trim();
                if (!string.IsNullOrEmpty(arg))
                    argList.Add(arg);
            }

            if (argList.Count == 0)
            {
                command = null;
                args = null;
                return;
            }

            command = argList[0].ToLower();
            argList.RemoveAt(0);
            args = argList.ToArray();
        }
    }
}