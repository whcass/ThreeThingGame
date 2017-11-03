using System;
using System.Collections.Generic;
using System.Xml;

namespace SailAway
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string filename = args[0];
            XmlDocument inputFile = new XmlDocument();
            inputFile.Load(filename);
            using (var game = new SailAway(inputFile))
                game.Run();
        }
    }
}