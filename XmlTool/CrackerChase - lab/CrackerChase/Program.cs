using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CrackerChase
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string filename = args[0];
                XmlDocument inputFile = new XmlDocument();
                inputFile.Load(filename);
                using (var game = new Game1(inputFile))
                    game.Run();
            }
            else
            {
                Console.WriteLine("No command line args passed from debug tab.");
            }

            
        }
    }
#endif
}
