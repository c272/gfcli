using gfcli.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace gfcli
{
    /// <summary>
    /// A single button, placeable on a panel.
    /// </summary>
    public class Button : IPanel<Button>, ISelectable
    {
        //Wrapper for fluent interfacing.
        protected override Button Self() { return this; }
        private Label label;

        /// <summary>
        /// Whether the button is selected or not.
        /// </summary>
        public bool Selected
        {
            get
            {
                return iSelected;
            }
            set
            {
                if (value)
                {
                    label.Text = GFCli.ItemSelectedChar + " " + label.Text;
                }
                else
                {
                    if (label.Text.ToString().StartsWith(GFCli.ItemSelectedChar))
                    {
                        label.Text = label.Text.Substring(2);
                    }
                }
            }
        }
        private bool iSelected;

        /// <summary>
        /// The callback for when this button is pressed.
        /// </summary>
        public Action Callback { get; set; }

        /// <summary>
        /// Creates a button with the given text on it.
        /// </summary>
        public Button(ColouredString text, Action callback)
        {
            //Add the button label.
            label = new Label(text);
            base.AddItem(label);
            Callback = callback;

            //Set the default padding (1).
            PaddingLeft = 1;
            PaddingRight = 1;
        }
    }
}
