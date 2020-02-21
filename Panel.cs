using gfcli.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace gfcli
{
    
    /// <summary>
    /// A fluent interface CLI panel, which can contain other CLI elements.
    /// </summary>
    class Panel : IPanel<Panel>
    {
        //Returns this panel.
        protected override Panel Self() { return this; }

        /// <summary>
        /// Adds a button to the panel.
        /// </summary>
        public Panel AddButton(ColouredString text, Action callback)
        {
            base.Items.Add(new Button(text, callback));
            return this;
        }

        /// <summary>
        /// Adds a label to the panel.
        /// </summary>
        public Panel AddLabel(ColouredString text)
        {
            base.Items.Add(new Label(text));
            return this;
        }

        /// <summary>
        /// Adds an option to the panel.
        /// </summary>
        public Panel AddOption()
        {
            throw new NotImplementedException();
        }
    }
}
