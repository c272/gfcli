using System;
using System.Linq;

namespace gfcli
{
    class Program
    {
        static void Main(string[] args)
        {
            //Try and make a selected button.
            var btn = new Button("Testing!", null);

            //Make a panel, display it.
            var panel = new Panel().SetBorders(Border.Double)
                                   .AddLabel("I ♥ GFCli.")
                                   .SetPadding(1, SpacingMode.Horizontal)
                                   .SetMargins(1);
            panel.AddItem(btn);

            panel.Display();
            btn.Selected = true;
            panel.Display();
            btn.Selected = false;
            panel.Display();
        }
    }
}
