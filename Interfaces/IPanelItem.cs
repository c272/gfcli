using System.Collections.Generic;

namespace gfcli.Interfaces
{
    /// <summary>
    /// Represents a single item within a panel on the CLI.
    /// </summary>
    public interface IPanelItem
    {
        /// <summary>
        /// Alignment of the item in the parent panel.
        /// </summary>
        Alignment Align { get; set; }

        /// <summary>
        /// Margin on all sides of this item.
        /// </summary>
        int Margin { get; set; }

        /// <summary>
        /// Builds the panel item into a list of lines to be used.
        /// </summary>
        ColouredString Build();
    }

    /// <summary>
    /// Alignment of a panel item on the panel.
    /// In order of how they're drawn onto the panel, left to right.
    /// </summary>
    public enum Alignment
    {
        Left = 0,
        Center = 1,
        Right = 2
    }
}