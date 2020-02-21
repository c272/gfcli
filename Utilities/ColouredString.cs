using System;
using System.Collections.Generic;
using System.Linq;
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
        
        /// <summary>
        /// Constructor for copying a coloured string.
        /// </summary>
        public ColouredString(ColouredString c)
        {
            //Some LINQ to shallow copy the list.
            Parts = c.Parts.Where(x => true).ToList();
        }

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

        /// <summary>
        /// Returns a substring of this coloured string.
        /// </summary>
        public ColouredString Substring(int index)
        {
            int curIndex = 0;
            for (int i = 0; i < Parts.Count; i++)
            {
                //Increment the index.
                if (Parts[i].Type == StringPartType.NewLine)
                {
                    curIndex++;
                }
                else
                {
                    curIndex += Parts[i].Text.Length;
                }

                //Has it gone over?
                if (curIndex >= index)
                {
                    //Yes. If this part is a newline, just return this and all parts after.
                    if (Parts[i].Type == StringPartType.NewLine)
                    {
                        return new ColouredString(Parts.Skip(i).ToList());
                    }

                    //Not a newline, get the text out to use.
                    int charsFromEnd = curIndex - index;
                    List<StringPart> partsToRet = new List<StringPart>();
                    partsToRet.Add(new StringPart()
                    {
                        Text = Parts[i].Text.Substring(Parts[i].Text.Length - charsFromEnd),
                        Colour = Parts[i].Colour,
                        Type = StringPartType.String
                    });

                    //Add the rest of the parts.
                    partsToRet.AddRange(Parts.Skip(i + 1));
                    return new ColouredString(partsToRet);
                }
            }

            //Index out of range.
            throw new IndexOutOfRangeException("Index was outside the bounds of the string.");
        }

        /// <summary>
        /// Sets all string components of this text to have one colour.
        /// </summary>
        /// <param name="itemSelectedColour"></param>
        public void SetColour(ConsoleColor c)
        {
            for (int i=0; i<Parts.Count; i++)
            {
                if (Parts[i].Type != StringPartType.String) { return; }
                Parts[i] = new StringPart()
                {
                    Text = Parts[i].Text,
                    Colour = c,
                    Type = Parts[i].Type
                };
            }
        }

        ////////////////////////////
        // OVERRIDES AND C# SUGAR //
        ////////////////////////////

        //Implicit conversion from string to ColouredString, and vice versa.
        public static implicit operator ColouredString(string s) => new ColouredString(s);
        public static explicit operator string(ColouredString s) => s.ToString();

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
    public struct StringPart
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
    }
}
