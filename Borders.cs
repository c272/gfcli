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

        /// <summary>
        /// Builds the horizontal line for either the top or bottom of the border.
        /// </summary>
        public string BuildHorizontal(int insideWidth, bool top)
        {
            string final = "";

            //Add the top left/bottom left corner.
            if (top) { final += TopLeft; } else { final += BottomLeft; }

            //Add width (needs +1 because 1 character has already been added).
            final = final.PadRight(insideWidth + 1, Horizontal);

            //Add top right/bottom right.
            if (top) { final += TopRight; } else { final += BottomRight; }

            return final;
        }
    }
}
