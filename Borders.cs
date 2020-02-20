using System;
using System.Collections.Generic;
using System.Text;

namespace gfcli
{
    /// <summary>
    /// Encapsulation class for panel borders.
    /// </summary>
    public class Border
    {
        /// <summary>
        /// The "single line" style of border.
        /// </summary>
        public static Border Single
        {
            get
            {
                return new Border("┌┐└┘─│");
            }
        }

        /// <summary>
        /// The "double line" style of border.
        /// </summary>
        public static Border Double
        {
            get
            {
                return new Border("╒╕╘╛═│");
            }
        }

        public char TopLeft { get; }
        public char TopRight { get; }
        public char BottomLeft { get; }
        public char BottomRight { get; }
        public char Horizontal { get; }
        public char Vertical { get; }

        //Creates a character with the given characters (in order of property).
        public Border(string chars)
        {
            TopLeft = chars[0];
            TopRight = chars[1];
            BottomLeft = chars[2];
            BottomRight = chars[3];
            Horizontal = chars[4];
            Vertical = chars[5];
        }
    }
}
