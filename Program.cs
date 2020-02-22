using System;
using System.Linq;

namespace gfcli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("testzn\n\n\nirjsiufehdj\n\n");

            //Make a panel, display it.
            var panel = new Panel().SetBorders(Border.Single)
                                   .AddLabel("I ♥ GFCli.\n")
                                   .AddOption("Button1", doThing)
                                   .AddOption("Button2", null)
                                   .SetPadding(1, SpacingMode.Horizontal)
                                   .SetMargins(1);

            panel.Show();
        }

        private static void doThing()
        {
            Console.WriteLine("You chose button 1!");
        }
    }
}
