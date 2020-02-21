using System;
using System.Linq;

namespace gfcli
{
    class Program
    {
        static void Main(string[] args)
        {
            //Try and make a substring.
            var cs = new ColouredString("Hello ").Append("World", ConsoleColor.Red);
            var ss = cs.Substring(12321);
            Console.WriteLine((string)ss);

            //Make a panel, display it.
            var panel = new Panel().SetBorders(Border.Double)
                                   .AddLabel("I ♥ GFCli.")
                                   .SetPadding(1, SpacingMode.Horizontal)
                                   .SetMargins(1);

            panel.Display();
        }
    }
}
