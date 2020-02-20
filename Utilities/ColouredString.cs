using System;
using System.Collections.Generic;
using System.Text;

namespace gfcli
{
    /// <summary>
    /// Represents a string with multiple colours on the CLI.
    /// </summary>
    public class ColouredString
    {
        //The parts of this coloured string.
        public List<StringPart> Parts;
        public IEnumerable<ColouredString> Lines
        {
            get
            {
                //Make a list of coloured strings.
                List<StringPart> lineParts = new List<StringPart>();
                for (int i = 0; i < Parts.Count; i++)
                {
                    //Newline? go to next one.
                    if (Parts[i].Type == StringPartType.NewLine)
                    {
                        yield return new ColouredString(lineParts);
                        lineParts = new List<StringPart>();
                        continue;
                    }

                    lineParts.Add(Parts[i]);
                }
            }
        }

        /// <summary>
        /// Returns the total length of the coloured string.
        /// </summary>
        public int Length
        {
            get
            {
                int totalLen = 0;
                foreach (var part in Parts)
                {
                    if (part.Type != StringPartType.String) { continue; }
                    totalLen += part.Text.Length;
                }

                return totalLen;
            }
        }

        /// <summary>
        /// Easy constructor for text with a colour.
        /// </summary>
        public ColouredString(string s, ConsoleColor? c = null)
        {
            if (c == null) { c = Constants.Default; }
            Parts.Add(new StringPart()
            {
                Text = s,
                Colour = (ConsoleColor)c,
                Type = StringPartType.String
            });
        }

        /// <summary>
        /// Constructor for substrings.
        /// </summary>
        public ColouredString(List<StringPart> parts) { Parts = parts; }

        /// <summary>
        /// Constructor for a blank coloured string.
        /// </summary>
        public ColouredString() { }

        /// <summary>
        /// Adds a new line to this coloured string.
        /// </summary>
        public ColouredString NewLine()
        {
            Parts.Add(new StringPart() { Type = StringPartType.NewLine });
            return this;
        }

        /// <summary>
        /// Adds a piece of coloured text to this coloured string.
        /// </summary>
        public ColouredString Add(string s, ConsoleColor? c=null)
        {
            if (c == null) { c = Constants.Default; }
            Parts.Add(new StringPart()
            {
                Text = s,
                Colour = (ConsoleColor)c,
                Type = StringPartType.String
            });
            return this;
        }

        /// <summary>
        /// Adds a piece of coloured text to the string.
        /// </summary>
        public ColouredString Add(StringPart s)
        {
            Parts.Add(s);
            return this;
        }

        /// <summary>
        /// Adds a line to the current string.
        /// </summary>
        public ColouredString AddLine(ColouredString s)
        {
            Parts.Add(new StringPart() { Type = StringPartType.NewLine });
            Parts.AddRange(s.Parts);
            Parts.Add(new StringPart() { Type = StringPartType.NewLine });
            return this;
        }

        /// <summary>
        /// Appends a separate coloured string to this coloured string.
        /// </summary>
        public ColouredString Append(ColouredString s)
        {
            Parts.AddRange(s.Parts);
            return this;
        }
    }

    /// <summary>
    /// Represents a single part of a string.
    /// </summary>
    public class StringPart
    {
        public StringPartType Type;
        public ConsoleColor Colour;
        public string Text;
    }

    public enum StringPartType
    {
        NewLine,
        String,
        Padding
    }
}
