using System;
using System.Linq;

namespace gfcli
{
    class Program
    {
        static void Main(string[] args)
        {
            //Make a panel, display it.
            var panel = new Panel().SetBorders(Border.Double)
                                   .AddLabel("I ♥ GFCli.")
                                   .SetPadding(1, SpacingMode.Horizontal)
                                   .SetMargins(1);

            panel.Display();
        }
    }
}
