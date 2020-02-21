using gfcli.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace gfcli
{
    /// <summary>
    /// Represents a single label on the CLI.
    /// </summary>
    public class Label : IPanelItem
    {
        //The text on this label.
        public ColouredString Text;
        public Label(ColouredString text)
        {
            this.Text = text;
        }

        //The margins of this label.
        public int MarginTop { get; set; } = 0;
        public int MarginLeft { get; set; } = 0;
        public int MarginRight { get; set; } = 0;
        public int MarginBottom { get; set; } = 0;

        public ColouredString Build()
        {
            return Text;
        }
    }
}
