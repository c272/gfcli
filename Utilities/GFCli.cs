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
        public static string ItemSelectedPrefix = "> ";
        public static ConsoleColor ItemSelectedColour = ConsoleColor.Green;

        /// <summary>
        /// Clears the last console line.
        /// </summary>
        public static void ClearLastLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - (Console.WindowWidth >= Console.BufferWidth ? 1 : 0));
        }
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
