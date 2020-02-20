using gfcli.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace gfcli
{
    /// <summary>
    /// A single button, placeable on a panel.
    /// </summary>
    public class Button : IPanel<Button>, ISelectable<string>
    {
        //Wrapper for fluent interfacing.
        protected override Button Self() { return this; }

        /// <summary>
        /// Whether the button is selected or not.
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// The callback for when this button is pressed.
        /// </summary>
        public Action<string> Callback { get; set; }

        /// <summary>
        /// Creates a button with the given text on it.
        /// </summary>
        public Button(ColouredString text, Action<string> callback)
        {
            //Add the button label.
            base.AddItem(new Label(text));
            Callback = callback;
        }
    }
}
