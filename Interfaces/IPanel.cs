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
        public int Margin { get; set; }

        /// <summary>
        /// The character padding between the items and the panel edge.
        /// </summary>
        public int Padding { get; set; }

        /// <summary>
        /// The title of this panel.
        /// </summary>
        public string Title { get; set; } = null;

        /// <summary>
        /// The borders being used for this panel.
        /// </summary>
        public Border Borders { get; protected set; }

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
            //Get rows of items to build.
            var rows = new List<List<IPanelItem>>();
            var row = new List<IPanelItem>();
            for (int i = 0; i < Items.Count; i++)
            {
                //Get the builds for this row.
                if (Items[i].Align == row.LastOrDefault().Align)
                {
                    rows.Add(row);
                    row = new List<IPanelItem>();
                }

                row.Add(Items[i]);
            }

            //aaaaaargh
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
        public T SetMargin(int m)
        {
            Margin = m;
            return Self();
        }

        /// <summary>
        /// Sets the padding on this panel to the given amount of characters.
        /// </summary>
        public T SetPadding(int p)
        {
            Padding = p;
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
