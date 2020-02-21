using System;
using System.Collections.Generic;
using System.Text;

namespace gfcli
{
    /// <summary>
    /// Constants in the CLI.
    /// </summary>
    public class GFCli
    {
        public static ConsoleColor DefaultConsoleColour = Console.ForegroundColor;
        public static char ItemSelectedChar = '>';
        public static ConsoleColor ItemSelectedColour = ConsoleColor.Green;
    }

    /// <summary>
    /// Spacing mode for margins and padding.
    /// </summary>
    public enum SpacingMode
    {
        Vertical,
        Horizontal,
        Top,
        Left,
        Right,
        Bottom,
        All,
    }
}
