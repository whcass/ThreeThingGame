using System;
using System.Collections.Generic;

namespace SailAway
{
    public static class Program
    {
        static void Main()
        {
            using (var game = new SailAway())
                game.Run();
        }
    }
}
