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
        public List<StringPart> Parts = new List<StringPart>();
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

                //Return whatever's left.
                if (lineParts.Count > 0) { yield return new ColouredString(lineParts); }
            }
        }

        /// <summary>
        /// Returns a substring of this coloured string.
        /// </summary>
        public ColouredString Substring(int index)
        {
            List<StringPart> toRet = new List<StringPart>();
            for (int i=0; i<Parts.Count; i++)
            {

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

        ////////////////////
        /// CONSTRUCTORS ///
        ////////////////////

        /// <summary>
        /// Easy constructor for text with a colour.
        /// </summary>
        public ColouredString(string s, ConsoleColor? c = null)
        {
            if (c == null) { c = GFCli.DefaultConsoleColour; }

            //Add all lines.
            var lines = s.Replace("\r", "").Split('\n');
            foreach (var line in lines)
            {
                Parts.Add(new StringPart()
                {
                    Text = line,
                    Colour = (ConsoleColor)c,
                    Type = StringPartType.String
                });
                Parts.Add(StringPart.NewLine);
            }
            
            //Remove trailing newline.
            Parts.RemoveAt(Parts.Count - 1);
        }

        /// <summary>
        /// Constructor for substrings.
        /// </summary>
        public ColouredString(List<StringPart> parts) { Parts = parts; }

        /// <summary>
        /// Constructor for a blank coloured string.
        /// </summary>
        public ColouredString() { }

        ///////////////
        /// METHODS ///
        ///////////////

        /// <summary>
        /// Adds a new line to this coloured string.
        /// </summary>
        public ColouredString NewLine(int count=1)
        {
            for (int i = 0; i < count; i++)
            {
                Parts.Add(StringPart.NewLine);
            }
            return this;
        }

        /// <summary>
        /// Adds a piece of coloured text to this coloured string.
        /// </summary>
        public ColouredString Append(string s, ConsoleColor? c=null)
        {
            if (c == null) { c = GFCli.DefaultConsoleColour; }

            //Add all lines.
            var lines = s.Replace("\r", "").Split('\n');
            foreach (var line in lines)
            {
                Parts.Add(new StringPart()
                {
                    Text = line,
                    Colour = (ConsoleColor)c,
                    Type = StringPartType.String
                });
                Parts.Add(StringPart.NewLine);
            }

            //Remove trailing newline.
            Parts.RemoveAt(Parts.Count - 1);

            return this;
        }

        /// <summary>
        /// Adds a piece of coloured text to the string.
        /// </summary>
        public ColouredString Append(StringPart s)
        {
            Parts.Add(s);
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

        /// <summary>
        /// Adds a line to the current string.
        /// </summary>
        public ColouredString AddLine(ColouredString s)
        {
            Parts.Add(StringPart.NewLine);
            Parts.AddRange(s.Parts);
            return this;
        }

        /// <summary>
        /// Trims newlines from the start and end of the string.
        /// </summary>
        public ColouredString TrimNewLines(bool startOnly=false)
        {
            //Any parts to trim?
            if (Parts.Count == 0) { return this; }

            //Begin trim from start.
            while (Parts.Count != 0 && Parts[0].Type == StringPartType.NewLine)
            {
                Parts.RemoveAt(0);
            }

            //Begin trim from end.
            if (!startOnly)
            {
                while (Parts.Count != 0 && Parts[Parts.Count-1].Type == StringPartType.NewLine)
                {
                    Parts.RemoveAt(Parts.Count-1);
                }
            }

            return this;
        }

        ////////////////////////////
        // OVERRIDES AND C# SUGAR //
        ////////////////////////////

        //Implicit conversion from string to ColouredString.
        public static implicit operator ColouredString(string s) => new ColouredString(s);

        //Stringifies the coloured string to a basic string.
        public override string ToString()
        {
            string final = "";
            foreach (var part in Parts)
            {
                if (part.Type == StringPartType.NewLine) { final += "\r\n"; continue; }
                final += part.Text;
            }

            return final;
        }
    }

    /// <summary>
    /// Represents a single part of a string.
    /// </summary>
    public class StringPart
    {
        //New line string part.
        public static StringPart NewLine = new StringPart() { Type = StringPartType.NewLine };

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
