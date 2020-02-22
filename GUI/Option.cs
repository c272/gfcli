using gfcli.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace gfcli
{
    /// <summary>
    /// Represents a single label on the CLI.
    /// </summary>
    public class Option : IPanelItem, ISelectable
    {
        //The text on this label.
        public ColouredString Text;
        private ColouredString originalText;

        public Option(ColouredString text, Action callback)
        {
            Text = text;
            originalText = text;
            Callback = callback;
        }

        //The margins of this option.
        public int MarginTop { get; set; } = 0;
        public int MarginLeft { get; set; } = 0;
        public int MarginRight { get; set; } = 0;
        public int MarginBottom { get; set; } = 0;

        /// <summary>
        /// Callback for when this option is selected.
        /// </summary>
        public Action Callback { get; set; }

        /// <summary>
        /// Whether the option is selected or not.
        /// </summary>
        public bool Selected
        {
            get
            {
                return iSelected;
            }
            set
            {
                //Turn on/off selection and colours.
                if (value)
                {
                    Text = GFCli.ItemSelectedPrefix + Text;
                    Text.SetColour(GFCli.ItemSelectedColour);
                }
                else
                {
                    if (Text.ToString().StartsWith(GFCli.ItemSelectedPrefix))
                    {
                        Text = originalText;
                    }
                }

                iSelected = value;
            }
        }
        private bool iSelected;

        public ColouredString Build()
        {
            return Text;
        }
    }
}
