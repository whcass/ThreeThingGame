using System;
using System.Collections.Generic;
using System.Xml;

namespace SailAway
{
    public static class Program
    {
        
        static void Main(string[] args)
        {
            using (var game = new SailAway())
                game.Run();
        }
    }
}