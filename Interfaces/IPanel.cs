using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gfcli.Interfaces
{
    /// <summary>
    /// Represents a single paneled object on the CLI.
    /// </summary>
    public abstract class IPanel<T> : IPanelItem
    {
        /// <summary>
        /// Returns a typed instance of the inheriting class.
        /// </summary>
        protected abstract T Self();

        /// <summary>
        /// The character margin between the panel and the CLI borders.
        /// </summary>
        public int MarginTop { get; set; }
        public int MarginLeft { get; set; }
        public int MarginRight { get; set; }
        public int MarginBottom { get; set; }

        /// <summary>
        /// The character padding between the items and the panel edge.
        /// </summary>
        public int PaddingTop { get; set; }
        public int PaddingLeft { get; set; }
        public int PaddingRight { get; set; }
        public int PaddingBottom { get; set; }

        /// <summary>
        /// The title of this panel.
        /// </summary>
        public string Title { get; set; } = null;

        /// <summary>
        /// The borders being used for this panel.
        /// </summary>
        public Border Borders { get; protected set; } = Border.Single;

        /// <summary>
        /// Sets the colour of the borders.
        /// </summary>
        public ConsoleColor BorderColour { get; protected set; } = GFCli.DefaultConsoleColour;

        /// <summary>
        /// A list of items on the panel.
        /// </summary>
        public List<IPanelItem> Items { get; protected set; } = new List<IPanelItem>();

        /// <summary>
        /// The alignment of this panel within it's parent panel.
        /// </summary>
        public Alignment Align { get; set; }

        //Methods.

        /// <summary>
        /// Builds the panel as a list of string lines.
        /// </summary>
        public ColouredString Build()
        {
            //Set up locals.
            int maxWidth = -1;
            var builtString = new ColouredString();

            //Build.
            var builds = BuildAll(Items).ToList();

            //Loop over and add lines to the built string.
            while (builds.Count != 0)
            {
                for (int i = 0; i < builds[0].Lines.Count(); i++) 
                {
                    //Add the new line.
                    var line = builds[0].Lines.ElementAt(i);
                    builtString.AddLine(line);

                    //Is it bigger than the current max width?
                    if (line.Length > maxWidth)
                    {
                        maxWidth = line.Length;
                    }
                }
                builds.RemoveAt(0);
            }
            builtString.TrimNewLines(true);

            //Create final build.
            var finalBuild = new ColouredString();
            int internalWidth = maxWidth + PaddingLeft + PaddingRight;

            //TOP PREFIX LINES
            finalBuild.NewLine(MarginTop); //margin top
            finalBuild.Append("".PadRight(MarginLeft)); //margin left
            finalBuild.Append(Borders.BuildHorizontal(internalWidth, true), BorderColour); //top border
            finalBuild.Append("".PadRight(MarginRight)); //margin right

            //PADDING TOP
            for (int i=0; i<PaddingTop; i++)
            {
                finalBuild.NewLine();
                finalBuild.Append("".PadRight(MarginLeft)); //margin left
                finalBuild.Append(Borders.Vertical + "".PadRight(internalWidth) + Borders.Vertical); //pad
                finalBuild.Append("".PadRight(MarginRight)); //margin right
            }

            //MAIN BODY
            var mainBodyLines = builtString.Lines.ToList();
            for (int i = 0; i < mainBodyLines.Count; i++)
            {
                finalBuild.NewLine();

                //margin left, vertical
                finalBuild.Append("".PadRight(MarginLeft) + Borders.Vertical.ToString(), BorderColour);

                //Calculate inner string right pad based on length.
                var innerString = new ColouredString("".PadRight(PaddingLeft));
                innerString.Append(mainBodyLines[i]);
                innerString.Append("".PadRight(PaddingRight));
                innerString.Append("".PadRight(internalWidth - innerString.Length));

                //add inner, vertical, and margin right
                finalBuild.Append(innerString);
                finalBuild.Append(Borders.Vertical.ToString() + "".PadRight(MarginRight), BorderColour);
            }

            //PADDING BOTTOM
            for (int i = 0; i < PaddingBottom; i++)
            {
                finalBuild.NewLine();
                finalBuild.Append("".PadRight(MarginLeft)); //margin left
                finalBuild.Append(Borders.Vertical + "".PadRight(internalWidth) + Borders.Vertical); //pad
                finalBuild.Append("".PadRight(MarginRight)); //margin right
            }

            //BOTTOM POSTFIX LINES
            finalBuild.NewLine();
            finalBuild.Append("".PadRight(MarginLeft)); //margin left
            finalBuild.Append(Borders.BuildHorizontal(internalWidth, false), BorderColour); //top border
            finalBuild.Append("".PadRight(MarginRight)); //margin right
            finalBuild.NewLine(MarginBottom); //margin bottom

            //Return the build.
            return finalBuild;
        }

        /// <summary>
        /// Builds all panel items, and returns them as a list.
        /// </summary>
        private List<ColouredString> BuildAll(IEnumerable<IPanelItem> left)
        {
            List<ColouredString> strings = new List<ColouredString>();
            foreach (var item in left)
            {
                strings.Add(item.Build());
            }

            return strings;
        }

        /// <summary>
        /// Shows this panel on the console, and waits for input (if necessary).
        /// </summary>
        public void Show()
        {
            //Are there any selectable items on this panel?
            if (Items.FindIndex(x => x is ISelectable) != -1)
            {
                //Execute in input mode.
                ShowInputMode();
            }
            else
            {
                //Show static.
                Display();
            }
        }

        /// <summary>
        /// Displays the static panel on console, with input disabled.
        /// </summary>
        public void Display()
        {
            //Make sure the output mode is unicode.
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            //Print lines.
            foreach (var line in Build().Lines)
            {
                foreach (var part in line.Parts)
                {
                    Console.ForegroundColor = part.Colour;
                    Console.Write(part.Text);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Shows this panel on the console with input enabled.
        /// </summary>
        private void ShowInputMode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the borders on this panel to the given set of borders.
        /// </summary>
        public T SetBorders(Border b)
        {
            Borders = b;
            return Self();
        }

        /// <summary>
        /// Sets the margins on this panel to a given amount of characters.
        /// </summary>
        public T SetMargins(int m, SpacingMode mode = SpacingMode.All)
        {
            //Set based on mode.
            switch (mode)
            {
                case SpacingMode.Top:
                    MarginTop = m;
                    break;
                case SpacingMode.Left:
                    MarginLeft = m;
                    break;
                case SpacingMode.Right:
                    MarginRight = m;
                    break;
                case SpacingMode.Bottom:
                    MarginBottom = m;
                    break;
                case SpacingMode.Vertical:
                    SetMargins(m, SpacingMode.Top);
                    SetMargins(m, SpacingMode.Bottom);
                    break;
                case SpacingMode.Horizontal:
                    SetMargins(m, SpacingMode.Left);
                    SetMargins(m, SpacingMode.Right);
                    break;
                case SpacingMode.All:
                    SetMargins(m, SpacingMode.Vertical);
                    SetMargins(m, SpacingMode.Horizontal);
                    break;
            }

            return Self();
        }

        /// <summary>
        /// Sets the all round padding on this panel to the given amount of characters.
        /// </summary>
        public T SetPadding(int p, SpacingMode mode = SpacingMode.All)
        {
            //Set based on mode.
            switch (mode)
            {
                case SpacingMode.Top:
                    PaddingTop = p;
                    break;
                case SpacingMode.Left:
                    PaddingLeft = p;
                    break;
                case SpacingMode.Right:
                    PaddingRight = p;
                    break;
                case SpacingMode.Bottom:
                    PaddingBottom = p;
                    break;
                case SpacingMode.Vertical:
                    SetPadding(p, SpacingMode.Top);
                    SetPadding(p, SpacingMode.Bottom);
                    break;
                case SpacingMode.Horizontal:
                    SetPadding(p, SpacingMode.Left);
                    SetPadding(p, SpacingMode.Right);
                    break;
            }

            return Self();
        }

        /// <summary>
        /// Adds an item to the list of panel items in the list.
        /// </summary>
        public T AddItem(IPanelItem item, int insertIndex=-1)
        {
            if (insertIndex == -1)
            {
                Items.Add(item);
            }
            else
            {
                Items.Insert(insertIndex, item);
            }
            return Self();
        }
    }
}
