﻿using System;
using System.Collections.Generic;
using System.Text;

namespace gfcli.Interfaces
{
    /// <summary>
    /// Represents a selectable item on the CLI.
    /// </summary>
    public interface ISelectable : IPanelItem
    {
        public Action Callback { get; set; }
        public bool Selected { get; set; }
    }
}
