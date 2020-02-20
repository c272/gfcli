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
        private ColouredString text;
        public Label(ColouredString text)
        {
            this.text = text;
        }

        //The alignment and margin of this label.
        public Alignment Align { get; set; } = Alignment.Left;
        public int Margin { get; set; } = 0;

        public ColouredString Build()
        {
            return text;
        }
    }
}
